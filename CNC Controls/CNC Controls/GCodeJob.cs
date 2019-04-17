/*
 * GCodeJob.cs - part of CNC Controls library for Grbl
 *
 * v0.01 / 2019-04-17 / Io Engineering (Terje Io)
 *
 */

/*

Copyright (c) 2018-2019, Io Engineering (Terje Io)
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
using System.Globalization;

namespace CNC_Controls
{
    public partial class GCodeJob : UserControl
    {
        public const int
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101;

        private enum JogMode
        {
            Step = 0,
            Slow,
            Fast,
            None // must be last!
        }

        private volatile int serialUsed = 0;
        private JogMode jogMode = JogMode.None;
        private volatile Keys[] axisjog = new Keys[3] { Keys.None, Keys.None, Keys.None };
        private double[] jogDistance = new double[3] { 0.05, 500.0, 500.0};
        private double[] jogSpeed = new double[3] { 100.0, 200.0, 500.0 };
        private volatile StreamingState streamingState = StreamingState.NoFile;
        private GrblState grblState;
        private GrblStatusParameters parameters = new GrblStatusParameters();
        private GCode file = new GCode();

        private int PollInterval = 200, serialSize = 128, CurrLine = 0, PendingLine = 0, PgmEndLine = -1, ACKPending = 0;
        private bool initOK = false, pgmStarted = false, pgmComplete = false;
        private PollGrbl poller = null;
        private Thread polling = null;
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
            System.Threading.Thread.Sleep(100);
      //      while (!polling.IsAlive);
        }

        public GrblStates state { get { return grblState.State; } } 

        public GCode gcode { get { return file; } }
        
        public StreamingState StreamingState { get { return streamingState; } }

        public bool canJog { get { return grblState.State == GrblStates.Idle || grblState.State == GrblStates.Tool || grblState.State == GrblStates.Jog; } }

        public bool JobPending { get { return this.file.Loaded && !JobTimer.IsRunning; } }

        // Configure to match Grbl settings (if loaded)
        public bool Config ()
        {
            if (GrblSettings.Loaded)
            {
                double val;
                if (!(val = GrblSettings.GetDouble(GrblSetting.JogStepDistance)).Equals(double.NaN))
                    jogDistance[(int)JogMode.Step] = val;
                if (!(val = GrblSettings.GetDouble(GrblSetting.JogSlowDistance)).Equals(double.NaN))
                    jogDistance[(int)JogMode.Slow] = val;
                if (!(val = GrblSettings.GetDouble(GrblSetting.JogFastDistance)).Equals(double.NaN))
                    jogDistance[(int)JogMode.Fast] = val;
                if (!(val = GrblSettings.GetDouble(GrblSetting.JogStepSpeed)).Equals(double.NaN))
                    jogSpeed[(int)JogMode.Step] = val;
                if (!(val = GrblSettings.GetDouble(GrblSetting.JogSlowSpeed)).Equals(double.NaN))
                    jogSpeed[(int)JogMode.Slow] = val;
                if (!(val = GrblSettings.GetDouble(GrblSetting.JogFastSpeed)).Equals(double.NaN))
                    jogSpeed[(int)JogMode.Fast] = val;
            }

            return GrblSettings.Loaded;
        }

        #region UIevents

        public bool ProcessKeypress (ref Message msg)
        {
            Keys keycode = (Keys)msg.WParam.ToInt32();
            string command = "";

            if (keycode == Keys.Space && this.grblState.State != GrblStates.Idle)
            {
                SendRTCommand(GrblConstants.CMD_FEED_HOLD);
                return true;
            }

            if (Control.ModifierKeys == Keys.Alt)
            {
                if (keycode == Keys.S)
                {
                    SetStreamingState(StreamingState.Stop);
                    return true;
                }

                if (keycode == Keys.R)
                {
                    this.CycleStart();
                    return true;
                }
            }

            bool isJogging = this.jogMode != JogMode.None;

            if (msg.Msg == WM_KEYUP && (isJogging || this.grblState.State == GrblStates.Jog))
            {
                bool cancel = false;
                
                isJogging = false;

                for (int i = 0; i < 3; i++)
                {
                    if (axisjog[i] == keycode)
                    {
                        axisjog[i] = Keys.None;
                        cancel = true;
                    }
                    else
                        isJogging = isJogging || (axisjog[i] != Keys.None);
                }

                if (cancel && !isJogging && this.jogMode != JogMode.Step)
                    this.JogCancel();
            }

            if (!isJogging && Comms.com.OutCount != 0)
                return true;

//            if ((keycode == Keys.ShiftKey || keycode == Keys.ControlKey) && !this.isJogging)
//                return false;

            if (msg.Msg == WM_KEYDOWN && this.canJog)
            {
                // Do not respond to autorepeats!
                if ((msg.LParam.ToInt32() & (1 << 30)) != 0)
                    return true;

                switch (keycode)
                {
                    case Keys.PageUp:
                        isJogging = axisjog[2] != Keys.PageUp;
                        axisjog[2] = Keys.PageUp;
                        break;

                    case Keys.PageDown:
                        isJogging = axisjog[2] != Keys.PageDown;
                        axisjog[2] = Keys.PageDown;
                        break;

                    case Keys.Left:
                        isJogging = axisjog[0] != Keys.Left;
                        axisjog[0] = Keys.Left;
                        break;

                    case Keys.Up:
                        isJogging = axisjog[1] != Keys.Up;
                        axisjog[1] = Keys.Up;
                        break;

                    case Keys.Right:
                        isJogging = axisjog[0] != Keys.Right;
                        axisjog[0] = Keys.Right;
                        break;

                    case Keys.Down:
                        isJogging = axisjog[1] != Keys.Down;
                        axisjog[1] = Keys.Down;
                        break;
                }
            }

            if (isJogging)
            {
                if (GrblInfo.LatheMode)
                {
                    for (int i = 0; i < 2; i++) switch (axisjog[i])
                        {
                            case Keys.Left:
                                command += "Z-{0}";
                                break;

                            case Keys.Up:
                                command += "X-{0}";
                                break;

                            case Keys.Right:
                                command += "Z{0}";
                                break;

                            case Keys.Down:
                                command += "X{0}";
                                break;
                        }
                }
                else
                {
                    for (int i = 0; i < 3; i++) switch (axisjog[i])
                        {
                            case Keys.PageUp:
                                command += "Z{0}";
                                break;

                            case Keys.PageDown:
                                command += "Z-{0}";
                                break;

                            case Keys.Left:
                                command += "X-{0}";
                                break;

                            case Keys.Up:
                                command += "Y{0}";
                                break;

                            case Keys.Right:
                                command += "X{0}";
                                break;

                            case Keys.Down:
                                command += "Y-{0}";
                                break;
                        }
                }

                if ((isJogging = command != ""))
                {
                    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                        this.jogMode = JogMode.Step;
                    else if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                        this.jogMode = JogMode.Fast;
                    else
                        this.jogMode = JogMode.Slow;

                    this.SendJogCommand("$J=G91" + string.Format(command + "F{1}",
                                                    jogDistance[(int)this.jogMode].ToString(CultureInfo.InvariantCulture),
                                                     jogSpeed[(int)this.jogMode].ToString(CultureInfo.InvariantCulture)));
                }
            }

            return isJogging;
        }

        void btnRewind_Click(object sender, EventArgs e)
        {
             RewindFile();
             SetStreamingState(this.streamingState);
        }

        void btnHold_Click(object sender, EventArgs e)
        {
            SendRTCommand(GrblConstants.CMD_FEED_HOLD);
        }

        void btnStop_Click(object sender, EventArgs e)
        {
            SetStreamingState(StreamingState.Stop);
        }

        void btnStart_Click(object sender, EventArgs e)
        {
            this.CycleStart();
        }

        void UserUI_DragEnter(object sender, DragEventArgs e)
        {
            bool allow = this.streamingState == StreamingState.Idle || this.streamingState == StreamingState.NoFile;

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

          //      this.ppiControl.Speed = this.file.max_feed;

                this.GCodeView.DataSource = file.Source;

                this.GCodeView.Refresh();

                this.CurrLine = 0;
                this.PendingLine = 0;
                this.PgmEndLine = this.file.Data.Rows.Count - 1;

                SetStreamingState(file.Loaded ? StreamingState.Idle : StreamingState.NoFile);

                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        public void CloseFile()
        {
            this.file.CloseFile();
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
                Comms.com.DataReceived += new DataReceivedHandler(DataReceived);
                poller.SetState(this.PollInterval);
            }
            else
            {
                poller.SetState(0);
                Comms.com.DataReceived -= DataReceived;
            }
        }

        public void CycleStart()
        {
            if (this.grblState.State == GrblStates.Hold || this.grblState.State == GrblStates.Tool)
                SendRTCommand(GrblConstants.CMD_CYCLE_START);
            else if (this.file.Loaded)
            {
                txtRunTime.Text = "";
                this.ACKPending = this.CurrLine = this.serialUsed = 0;
                this.pgmStarted = false;
                System.Threading.Thread.Sleep(250);
                Comms.com.PurgeQueue();
                if (GrblMessage != null)
                    GrblMessage("");
                this.nextRow = file.Data.Rows[0];
                JobTimer.Start();
                SetStreamingState(StreamingState.Send);
                //         this.DataReceived("!start");
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
            this.streamingState = StreamingState.Idle;
            while (Comms.com.OutCount != 0)
                Application.DoEvents(); //??
            Comms.com.WriteByte((byte)GrblConstants.CMD_JOG_CANCEL); // Cancel jog
            this.jogMode = JogMode.None;
        }

        public void SendJogCommand(string command)
        {
            if (this.streamingState == StreamingState.Jogging || this.grblState.State == GrblStates.Jog)
            {
                while (Comms.com.OutCount != 0)
                    Application.DoEvents(); //??
                Comms.com.WriteByte((byte)GrblConstants.CMD_JOG_CANCEL); // Cancel current jog
            }
            this.streamingState = StreamingState.Jogging;
            Comms.com.WriteCommand(command);
        }

        public void SendRTCommand(string command)
        {
            Comms.com.WriteByte((byte)command[0]);
        }

        public void SendMDICommand(string command)
        {
            if (GrblMessage != null)
                GrblMessage("");

            if (command.Length == 1)
                SendRTCommand(command);
            else if (this.streamingState == StreamingState.Idle || this.streamingState == StreamingState.NoFile || this.streamingState == StreamingState.ToolChange || command == GrblConstants.CMD_UNLOCK)
            {
//                command = command.ToUpper();
                try
                {
                    file.ParseBlock(command + "\r", false);
                    file.commands.Enqueue(command);
                    if (this.streamingState != StreamingState.SendMDI)
                    {
                        this.streamingState = StreamingState.SendMDI;
                        this.DataReceived("ok");
                    }
                }
                catch
                {
                }
            }
        }

        public void RewindFile()
        {
            this.pgmComplete = false;

            if (this.file.Loaded)
            {
                this.GCodeView.DataSource = null;

                foreach (DataRow row in this.file.Data.Rows)
                    row["Sent"] = "";

                this.GCodeView.DataSource = file.Source;
                this.GCodeView.FirstDisplayedScrollingRowIndex = 0;
                this.GCodeView.Refresh();

                this.CurrLine = this.PendingLine = 0;
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
                    this.btnStart.Enabled = file.Loaded;
                    this.btnStop.Enabled = false;
                    this.btnHold.Enabled = true;
                    this.btnRewind.Enabled = file.Loaded && this.CurrLine != 0;
                    break;

                case StreamingState.Send:
                    this.btnStart.Enabled = false;
                    this.btnHold.Enabled = true;
                    this.btnStop.Enabled = true;
                    this.btnRewind.Enabled = false;
                    if (file.Loaded)
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

                case StreamingState.ToolChange:
                    this.btnStart.Enabled = true;
                    this.btnHold.Enabled = false;
                    break;

                case StreamingState.Stop:
                    this.btnStart.Enabled = false;
                    this.btnStop.Enabled = false;
                    this.btnRewind.Enabled = true;
                    Comms.com.WriteByte((byte)GrblConstants.CMD_STOP);
                    if (JobTimer.IsRunning)
                        JobTimer.Stop();
                    break;
            }

            this.streamingState = newState;

            if (StreamingStateChanged != null)
                StreamingStateChanged(this.streamingState);
        }

        void SetGRBLState(string newState, int substate, bool force)
        {
            GrblStates newstate = (GrblStates)Enum.Parse(typeof(GrblStates), newState);

            if (newstate != this.grblState.State || substate != this.grblState.Substate || force)
            {
                switch (newstate)
                {
                    case GrblStates.Idle:
                        this.grblState.Color = Color.White;
                        if (this.pgmComplete)
                        {
                            JobTimer.Stop();
                            this.txtRunTime.Text = JobTimer.RunTime;
                            RewindFile();
                        }
                        if (JobTimer.IsRunning)
                            JobTimer.Pause = true;
                        else
                            SetStreamingState(StreamingState.Idle);
                        break;

                    case GrblStates.Run:
                        if (JobTimer.IsPaused)
                            JobTimer.Pause = false;
                        this.grblState.Color = Color.LightGreen;
        //                if (this.grblState.State == GrblStates.Hold || this.grblState.State == GrblStates.Door || this.grblState.State == GrblStates.Tool)
                        SetStreamingState(StreamingState.Send);
                        break;

                    case GrblStates.Alarm:
                        this.grblState.Color = Color.Red;
                        break;

                    case GrblStates.Jog:
                        this.grblState.Color = Color.Yellow;
                        break;

                    case GrblStates.Tool:
                        this.grblState.Color = Color.LightSalmon;
                        SetStreamingState(StreamingState.ToolChange);
                        Comms.com.WriteByte((byte)GrblConstants.CMD_TOOL_ACK);
                        break;

                    case GrblStates.Hold:
                        this.grblState.Color = Color.LightSalmon;
                        SetStreamingState(StreamingState.FeedHold);
                        break;

                    case GrblStates.Door:
                        if (substate > 0)
                        {
                            this.grblState.Color = grblState.Substate == 1 ? Color.Red : Color.LightSalmon;
                            if (this.streamingState == StreamingState.Send)
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
                    string line = file.StripSpaces((string)currentRow["Data"]);
                    currentRow["Sent"] = "*";
                    if (line == "%")
                    {
                        if (!(this.pgmStarted = !this.pgmStarted))
                            this.PgmEndLine = this.CurrLine;
                    }
                    else if ((bool)currentRow["ProgramEnd"])
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
                this.BeginInvoke(new GcodeCallback(DataReceived), new object[] { data });

            else if (data.Substring(0, 1) == "<")
            {
                bool parseState = true;
                data = data.Remove(data.Length - 1);

                if (!data.Contains("|Pn:"))
                    data += "|Pn:";

                string[] elements = data.Split('|');

                foreach (string e in elements)
                {
                    string[] pair = e.Split(':');

                    if (parseState)
                    {
                        SetGRBLState(pair[0].Substring(1), pair.Count() == 1 ? -1 : int.Parse(pair[1]), false);
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

                if (JobTimer.IsRunning && !JobTimer.IsPaused)
                    this.txtRunTime.Text = JobTimer.RunTime;
            }
            else if (data.StartsWith("ALARM"))
            {
                SetGRBLState("Alarm", -1, false);
            }
            else if (data.StartsWith("[GC:"))
            {
                GrblParserState.Process(data);
            }
            else if (data.StartsWith("["))
            {
                if (!this.file.Loaded && data == "[MSG:Pgm End]")
                    SetStreamingState(StreamingState.NoFile);

                if (GrblMessage != null)
                    GrblMessage(data);
            }
            else if (this.streamingState != StreamingState.Jogging)
            {
                if (data != "ok" && GrblMessage != null)
                    GrblMessage(data.StartsWith("error:") ? GrblErrors.GetMessage(data.Substring(6)) : data);

                if (this.ACKPending > 0 && this.streamingState == StreamingState.Send)
                {
                    this.ACKPending--;
                    if ((string)file.Data.Rows[this.PendingLine]["Sent"] == "*")
                        this.serialUsed -= (int)file.Data.Rows[this.PendingLine]["Length"];
                    if (this.serialUsed < 0)
                        this.serialUsed = 0;
                    file.Data.Rows[this.PendingLine]["Sent"] = data;

                    if (this.PendingLine > 5)
                        this.GCodeView.FirstDisplayedScrollingRowIndex = this.PendingLine - 5;

                    if (this.streamingState == StreamingState.Send)
                    {
                        if (data.StartsWith("error"))
                            SetStreamingState(StreamingState.Halted);
                        else if ((this.pgmComplete = this.PgmEndLine == this.PendingLine))
                        {
                            this.ACKPending = this.CurrLine = 0;
                            if(this.grblState.State == GrblStates.Idle)
                                SetGRBLState(GrblStates.Idle.ToString(), -1, true);
                        }
                        else
                            SendNextLine();
                    }
                    this.PendingLine++;
                }

                switch (this.streamingState)
                {
                    case StreamingState.Send:
                        SendNextLine();
                        break;

                    case StreamingState.SendMDI:
                        if (file.commands.Count > 0)
                            Comms.com.WriteCommand(file.commands.Dequeue());
                        if (file.commands.Count == 0)
                            this.streamingState = StreamingState.Idle;
                        break;

                    case StreamingState.Reset:
                        Comms.com.WriteCommand(GrblConstants.CMD_UNLOCK);
                        this.streamingState = StreamingState.AwaitResetAck;
                        break;

                    case StreamingState.AwaitResetAck:
                        SetStreamingState(this.file.Loaded ? StreamingState.Idle : StreamingState.NoFile);
                        break;
                }
            }
        }
    }
}
