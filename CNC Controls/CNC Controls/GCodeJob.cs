/*
 * GCodeJob.cs - part of CNC Controls library for Grbl
 *
 * v0.01 / 2018-09-14 / Io Engineering (Terje Io)
 *
 */

/*

Copyright (c) 2018, Io Engineering (Terje Io)
All rights reserved.

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

· Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

· Redistributions in binary form must reproduce the above copyright notice, this
list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

· Neither the name of the copyright holder nor the names of its contributors may
be used to endorse or promote products derived from this software without
specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace CNC_Controls
{
    public partial class GCodeJob : UserControl
    {
        public const int
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101;

        private volatile int serialUsed = 0;
        private volatile int[] axisjog = new int[3] { 0, 0, 0 };
        private volatile StreamingState State = StreamingState.NoFile;
        private GrblState grblState;
        private GrblStatusParameters parameters = new GrblStatusParameters();
        private GCode file = new GCode();

        private int PollInterval = 200, serialSize = 128, CurrLine = 0, PendingLine = 0, PgmEndLine = -1, ACKPending = 0;
        private bool initOK = false, pgmStarted = false, pgmComplete = false;
        private PollGrbl poller = null;
        private Thread polling = null;
        private Stopwatch stopWatch = new Stopwatch();
        private DataRow currentRow = null, nextRow = null;

        private delegate void GcodeCallback(string data);

        public delegate void GrblStateChangedHandler(GrblState grblState);
        public event GrblStateChangedHandler GrblStateChanged;

        public delegate void GrblParameterChangedHandler(string parameter, string values);
        public event GrblParameterChangedHandler GrblParameterChanged;

        public delegate void StreamingStateChangedHandler(StreamingState state);
        public event StreamingStateChangedHandler StreamingStateChanged;

        public delegate void GrblMessageHandler(string message);
        public event GrblMessageHandler GrblMessage;

        public GCodeJob()
        {
            InitializeComponent();

            this.grblState.State = GrblStates.Unknown;
            this.grblState.Substate = 0;

            this.GCodeView.DragEnter += new DragEventHandler(UserUI_DragEnter);
            this.GCodeView.DragDrop += new DragEventHandler(UserUI_DragDrop);
            this.GCodeView.AutoGenerateColumns = false;

            this.btnStart.Click += new EventHandler(btnStart_Click);
            this.btnStop.Click += new EventHandler(btnStop_Click);
            this.btnHold.Click += new EventHandler(btnHold_Click);
            this.btnRewind.Click += new EventHandler(btnRewind_Click);

            this.poller = new PollGrbl();
            this.polling = new Thread(new ThreadStart(poller.run));
            polling.Start();
            while (!polling.IsAlive) ;
        }

        public GrblStates state { get { return grblState.State; } } 

        public GCode gcode { get { return file; } }
        
        public StreamingState streamingState { get { return State; } }

        public bool canJog { get { return grblState.State == GrblStates.Idle || grblState.State == GrblStates.Tool || grblState.State == GrblStates.Jog; } }

        public string runTime
        {
            get
            {
                return String.Format("{0:00}:{1:00}:{2:00}",
                                       this.stopWatch.Elapsed.Hours, this.stopWatch.Elapsed.Minutes, this.stopWatch.Elapsed.Seconds);
            }
        }

        #region UIevents

        public bool ProcessKeyJogging (ref Message msg)
        {
            bool jog = false, cancel = false;
            int keycode = msg.WParam.ToInt32();
            string command = "";

            if (this.streamingState == StreamingState.Jogging && msg.Msg == WM_KEYUP)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (axisjog[i] == keycode)
                    {
                        axisjog[i] = 0;
                        cancel = true;
                    }
                    else
                        jog = jog || axisjog[i] != 0;
                }

                if (cancel && !jog)
                    this.JogCancel();

                jog = jog & cancel;
            }

            if (Comms.com.OutCount != 0)
                return true;

            if (msg.Msg == WM_KEYDOWN && this.canJog)
            {
                switch (keycode)
                {
                    case 33: // Keys.PgUp
                        jog = axisjog[2] != 33;
                        axisjog[2] = 33;
                        break;

                    case 34: // Keys.PgDown
                        jog = axisjog[2] != 34;
                        axisjog[2] = 34;
                        break;

                    case 37: // Keys.Left
                        jog = axisjog[0] != 37;
                        axisjog[0] = 37;
                        break;

                    case 38: // Keys.Up
                        jog = axisjog[1] != 38;
                        axisjog[1] = 38;
                        break;

                    case 39: // Keys.Right
                        jog = axisjog[0] != 39;
                        axisjog[0] = 39;
                        break;

                    case 40: // Keys.Down
                        jog = axisjog[1] != 40;
                        axisjog[1] = 40;
                        break;
                }
            }

            if (jog)
            {
                for (int i = 0; i < 3; i++) switch (axisjog[i])
                {
                    case 33: // Keys.PgUp
                        command += "Z{0}";
                        break;

                    case 34: // Keys.PgDown
                        command += "Z-{0}";
                        break;

                    case 37: // Keys.Left
                        command += "X-{0}";
                        break;

                    case 38: // Keys.Up
                        command += "Y{0}";
                        break;

                    case 39: // Keys.Right
                        command += "X{0}";
                        break;

                    case 40: // Keys.Down
                        command += "Y-{0}";
                        break;
                }

                if (command != "")
                {
                    if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                        command = string.Format(command, "500") + "F1500";
                    else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                        command = string.Format(command, "0.05") + "F100";
                    else
                        command = string.Format(command, "500") + "F200";

                    this.SendJogCommand("$J=G91" + command);
                }
            }

            return command != "" || this.streamingState == StreamingState.Jogging;
        }

        void btnRewind_Click(object sender, EventArgs e)
        {
             RewindFile();
             SetStreamingState(this.State);
        }

        void btnHold_Click(object sender, EventArgs e)
        {
            SetStreamingState(StreamingState.FeedHold);
            SendRTCommand(GrblConstants.CMD_FEED_HOLD);
        }

        void btnStop_Click(object sender, EventArgs e)
        {
            SetStreamingState(StreamingState.Stop);
        }

        void btnStart_Click(object sender, EventArgs e)
        {
            if (this.State == StreamingState.FeedHold)
                SendRTCommand(GrblConstants.CMD_CYCLE_START);
            else
            {
                txtRunTime.Text = "";
                serialUsed = 0;
                this.pgmStarted = false;
                Comms.com.PurgeQueue();
                this.nextRow = file.Data.Rows[0];
                this.stopWatch.Reset();
                this.stopWatch.Start();
            }
            SetStreamingState(StreamingState.Send);
            this.DataReceived("ok");
        }

        void UserUI_DragEnter(object sender, DragEventArgs e)
        {
            bool allow = this.State == StreamingState.Idle || this.State == StreamingState.NoFile;

            if (allow && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                allow = files.Count() == 1 && (files[0].Contains(".nc") || files[0].Contains(".gcode") || files[0].Contains(".txt"));
            }

            e.Effect = allow ? DragDropEffects.All : DragDropEffects.None;
        }

        void UserUI_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (files.Count() == 1)
            {

                Cursor.Current = Cursors.WaitCursor;

                this.file.LoadFile(files[0]);

       //         CNC_App.UserUI.ui.WindowTitle = this.file.filename;

          //      this.ppiControl.Speed = this.file.max_feed;

                this.GCodeView.DataSource = file.Source;

                this.GCodeView.Refresh();

                this.CurrLine = 0;
                this.PendingLine = 0;
                this.PgmEndLine = this.file.Data.Rows.Count - 1;

                SetStreamingState(file.Data.Rows.Count > 0 ? StreamingState.Idle : StreamingState.NoFile);

                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        public void CloseFile()
        {
            this.file.CloseFile();
   //         CNC_App.UserUI.ui.WindowTitle = this.file.filename;
        }

        public void Activate(bool activate)
        {
            if (activate)
            {
                if (!initOK)
                {
                    initOK = true;
                    serialSize = Math.Min(300, (int)(GrblInfo.SerialBufferSize * 0.9f)); // size should be less than hardware handshake HWM
                }
                Comms.com.DataReceived += new Comms.DataReceivedHandler(DataReceived);
     //           CNC_App.UserUI.ui.WindowTitle = this.file.filename;
                poller.SetState(this.PollInterval);
            }
            else
            {
                poller.SetState(0);
                Comms.com.DataReceived -= DataReceived;
            }
        }

        public void SendReset ()
        {
            Comms.com.WriteByte((byte)GrblConstants.CMD_RESET);
            this.grblState.State = GrblStates.Unknown;
            grblState.Substate = 0;
        }

        public void JogCancel ()
        {
            this.State = StreamingState.Idle;
            while (Comms.com.OutCount != 0)
                Application.DoEvents(); //??
            Comms.com.WriteByte((byte)GrblConstants.CMD_JOG_CANCEL); // Cancel jog
        }

        public void SendJogCommand(string command)
        {
            if (this.State == StreamingState.Jogging)
                Comms.com.WriteByte((byte)GrblConstants.CMD_JOG_CANCEL); // Cancel current jog
            else
                this.State = StreamingState.Jogging;
            Comms.com.WriteCommand(command);
        }

        public void SendRTCommand(string command)
        {
            Comms.com.WriteByte(Encoding.Default.GetBytes(command)[0]);
        }

        public void SendMDICommand(string command)
        {
            if (GrblMessage != null)
                GrblMessage("");

            if (command.Length == 1)
                SendRTCommand(command);
            else if (this.State == StreamingState.Idle || this.State == StreamingState.NoFile || command == GrblConstants.CMD_UNLOCK)
            {
                command = command.ToUpper();
                file.commands.Enqueue(command);
                file.ParseBlock(command + "\r", false);
                if (this.State != StreamingState.SendMDI)
                {
                    this.State = StreamingState.SendMDI;
                    this.DataReceived("ok");
                }
            }
        }

        public void RewindFile()
        {
            this.pgmComplete = false;

            if (this.file.Open)
            {
                this.GCodeView.DataSource = null;

                foreach (DataRow row in this.file.Data.Rows)
                    row["Sent"] = "";

                this.GCodeView.DataSource = file.Source;
                this.GCodeView.FirstDisplayedScrollingRowIndex = 0;
                this.GCodeView.Refresh();

                this.CurrLine = 0;
                this.PendingLine = 0;
                this.PgmEndLine = this.file.Data.Rows.Count - 1;

                SetStreamingState(StreamingState.Idle);
            }
        }

        public void SetStreamingState(StreamingState newState)
        {
            switch (newState)
            {
                case StreamingState.Disabled:
                    this.Enabled = false;
                    break;

                case StreamingState.Idle:
                case StreamingState.NoFile:
                    this.Enabled = true;
                    this.btnStart.Enabled = file.Open;
                    this.btnStop.Enabled = false;
                    this.btnRewind.Enabled = file.Open && this.CurrLine != 0;
                    break;

                case StreamingState.Send:
                    this.btnStart.Enabled = false;
                    this.btnHold.Enabled = true;
                    this.btnStop.Enabled = true;
                    this.btnRewind.Enabled = false;
                    SendNextLine();
                    break;

                case StreamingState.Halted:
                    this.btnStart.Enabled = true;
                    this.btnHold.Enabled = false;
                    this.btnStop.Enabled = true;
                    break;

                case StreamingState.FeedHold:
                    this.btnStart.Enabled = true;
                    this.btnHold.Enabled = false;
                    break;

                case StreamingState.Stop:
                    this.btnStart.Enabled = false;
                    this.btnStop.Enabled = false;
                    this.btnRewind.Enabled = true;
                    Comms.com.WriteByte((byte)GrblConstants.CMD_STOP);
                    break;
            }

            this.State = newState;

            if (StreamingStateChanged != null)
                StreamingStateChanged(this.State);
        }

        void SetGRBLState(string newState, int substate)
        {
            GrblStates newstate = (GrblStates)Enum.Parse(typeof(GrblStates), newState);

            if (newstate != this.grblState.State || substate != this.grblState.Substate)
            {
                switch (newstate)
                {

                    case GrblStates.Idle:
                        this.grblState.Color = Color.White;
                        if (this.pgmComplete)
                        {
                            this.stopWatch.Stop();
                            txtRunTime.Text = this.runTime;
                            RewindFile();
                            SetStreamingState(StreamingState.Idle);
                        }
                        break;

                    case GrblStates.Run:
                        this.grblState.Color = Color.LightGreen;
                        if (this.grblState.State == GrblStates.Hold || this.grblState.State == GrblStates.Door)
                            SetStreamingState(StreamingState.Send);
                        break;

                    case GrblStates.Alarm:
                        this.grblState.Color = Color.Red;
                        break;

                    case GrblStates.Jog:
                        this.grblState.Color = Color.Yellow;
                        break;

                    case GrblStates.Tool:
                    case GrblStates.Hold:
                        this.grblState.Color = Color.LightSalmon;
                        SetStreamingState(StreamingState.FeedHold);
                        break;

                    case GrblStates.Door:
                        if (substate > 0)
                        {
                            this.grblState.Color = grblState.Substate == 1 ? Color.Red : Color.LightSalmon;
                            if (this.State == StreamingState.Send)
                                SetStreamingState(StreamingState.FeedHold);
                        }
                        break;

                    default:
                        this.grblState.Color = Color.White;
                        break;
                }

                this.grblState.State = newstate;
                this.grblState.Substate = substate;

                if (GrblStateChanged != null)
                    GrblStateChanged(grblState);
            }
        }

        void SendNextLine()
        {
            while (nextRow != null && serialUsed < (serialSize - (int)nextRow["Length"]))
            {
                if (file.commands.Count > 0)
                    Comms.com.WriteCommand(file.commands.Dequeue());
                else
                {
                    currentRow = nextRow;
                    string line = (string)currentRow["Data"];
                    line = line.Replace(" ", "");
                    currentRow["Sent"] = "*";
                    if (line == "%")
                    {
                        if (!(this.pgmStarted = !this.pgmStarted))
                            this.PgmEndLine = this.CurrLine;
                    }
                    else if (line.ToUpper() == "M30")
                        this.PgmEndLine = this.CurrLine;
                    nextRow = this.PgmEndLine == this.CurrLine ? null : file.Data.Rows[++this.CurrLine];
                    //            ParseBlock(line + "\r");
                    serialUsed += (int)currentRow["Length"];
                    Comms.com.WriteCommand(line);
                }
                this.ACKPending++;
            }
        }

        void DataReceived(string data)
        {
            if (data.Length == 0)
                return;

            if (this.GCodeView.InvokeRequired)
                this.Invoke(new GcodeCallback(DataReceived), new object[] { data });

            else if (data.Substring(0, 1) == "<")
            {
                bool parseState = true;
                data = data.Remove(data.Length - 1);

                string[] elements = data.Split('|');

                foreach (string e in elements)
                {
                    string[] pair = e.Split(':');

                    if (parseState)
                    {
                        SetGRBLState(pair[0].Substring(1), pair.Count() == 1 ? -1 : int.Parse(pair[1]));
                        parseState = false;
                    }
                    else if (parameters.Set(pair[0], pair[1]))
                    {
                        if (pair[0] == "MPG")
                            SetStreamingState(pair[1] == "1" ? StreamingState.Disabled : StreamingState.Idle);
                        else if (GrblParameterChanged != null)
                            GrblParameterChanged(pair[0], pair[1]);
                    }
                }
            }
            else if (data.StartsWith("ALARM"))
            {
                SetGRBLState("Alarm", -1);
            }
            else if (data.StartsWith("["))
            {
                if (GrblMessage != null)
                    GrblMessage(data);
            }
            else if (this.State != StreamingState.Jogging)
            {
                if (data != "ok" && GrblMessage != null)
                    GrblMessage(data.StartsWith("error:") ? GrblErrors.GetMessage(data.Substring(6)) : data);

                if (this.ACKPending > 0)
                {
                    this.ACKPending--;
                    this.serialUsed -= (int)file.Data.Rows[this.PendingLine]["Length"];
                    file.Data.Rows[this.PendingLine]["Sent"] = data;

                    if (this.PendingLine > 5)
                        this.GCodeView.FirstDisplayedScrollingRowIndex = this.PendingLine - 5;

                    if (this.State == StreamingState.Send)
                    {
                        if (data.StartsWith("error"))
                            SetStreamingState(StreamingState.Halted);
                        else if ((this.pgmComplete = this.PgmEndLine == this.PendingLine))
                            this.ACKPending = this.CurrLine = 0;
                        else
                            SendNextLine();
                    }
                    this.PendingLine++;
                }

                switch (this.State)
                {
                    case StreamingState.Send:
                        SendNextLine();
                        break;

                    case StreamingState.SendMDI:
                        if (file.commands.Count > 0)
                            Comms.com.WriteCommand(file.commands.Dequeue());
                        if (file.commands.Count == 0)
                            this.State = StreamingState.Idle;
                        break;

                    case StreamingState.Reset:
                        Comms.com.WriteCommand(GrblConstants.CMD_UNLOCK);
                        this.State = StreamingState.AwaitResetAck;
                        break;

                    case StreamingState.AwaitResetAck:
                        SetStreamingState(this.file.Open ? StreamingState.Idle : StreamingState.NoFile);
                        break;
                }
            }
        }
    }
}
