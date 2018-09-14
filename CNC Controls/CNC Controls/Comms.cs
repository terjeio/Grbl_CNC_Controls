/*
 * Comms.cs - part of CNC Controls library
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

//#define USEELTIMA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace CNC_Controls
{
    public class Comms
    {
        public enum State
        {
            AwaitAck,
            DataReceived,
            ACK,
            NAK
        }

        const int TXBUFFERSIZE = 4096, RXBUFFERSIZE = 1024;

        public static Comms com = null;

#if USEELTIMA
        private SPortLib.SPortAx SerialPort;
#else
        private SerialPort SerialPort;
#endif
        private StringBuilder input = new StringBuilder(100);

        public delegate void DataReceivedHandler (string data);
        public event DataReceivedHandler DataReceived;

        private volatile State state = State.ACK;

        public Comms (string PortParams)
        {
            this.Reply = "";

#if USEELTIMA
            try
            {
                this.SerialPort = new SPortLib.SPortAx();
            }
            catch
            {
                MessageBox.Show("Failed to load serial port driver.", "GCode Sender");
                System.Environment.Exit(1);
            }

            this.SerialPort.InitString(PortParams.Substring(PortParams.IndexOf(":") + 1));
            this.SerialPort.HandShake = 0x08;
            this.SerialPort.FlowReplace = 0x80;
            this.SerialPort.CharEvent = 10;
            this.SerialPort.InBufferSize = RXBUFFERSIZE;
            this.SerialPort.OutBufferSize = TXBUFFERSIZE;
            this.SerialPort.BlockMode = false;

            this.SerialPort.OnRxFlag += new SPortLib._ISPortAxEvents_OnRxFlagEventHandler(this.SerialRead);

            this.SerialPort.Open(PortParams.Substring(0, PortParams.IndexOf(":")));

            if (this.SerialPort.IsOpened)
            {
                this.SerialPort.PurgeQueue();
                this.SerialPort.OnRxFlag += new SPortLib._ISPortAxEvents_OnRxFlagEventHandler(this.SerialRead);
            }

#else
            string[] parameter = PortParams.Substring(PortParams.IndexOf(":") + 1).Split(',');

            if (parameter.Count() < 4)
            {
          //      this.disableUI();
                MessageBox.Show("Unable to open printer: " + PortParams, "GCode Sender");
                System.Environment.Exit(2);
            }

            this.SerialPort = new SerialPort();
            this.SerialPort.PortName = PortParams.Substring(0, PortParams.IndexOf(":"));
            this.SerialPort.BaudRate = int.Parse(parameter[0]);
            this.SerialPort.Parity = ParseParity(parameter[1]);
            this.SerialPort.DataBits = int.Parse(parameter[2]);
            this.SerialPort.StopBits = int.Parse(parameter[3]) == 1 ? StopBits.One : StopBits.Two;
            if (parameter.Count() > 4)
                this.SerialPort.Handshake = parameter[4] == "X" ? Handshake.XOnXOff : Handshake.RequestToSend;
            this.SerialPort.ReceivedBytesThreshold = 1;
            this.SerialPort.NewLine = "\r\n";
            this.SerialPort.ReadBufferSize = RXBUFFERSIZE;
            this.SerialPort.WriteBufferSize = TXBUFFERSIZE;

            try
            {
                this.SerialPort.Open();
            }
            catch
            {
            }

            if (this.SerialPort.IsOpen)
            {
                this.PurgeQueue();
                this.SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
            }
#endif
            Comms.com = this;
        }

        ~Comms()
        {
            this.Close();
        }

        public State CommandState { get { return this.state; } set { this.state = value; } }

#if USEELTIMA
        public bool IsOpen { get { return this.SerialPort.IsOpened; } }
        public int OutCount { get { return this.SerialPort.OutCount; } }

        public void PurgeQueue()
        {
            this.SerialPort.PurgeQueue();
        }
#else
        public bool IsOpen { get { return this.SerialPort.IsOpen; } }
        public int OutCount { get { return this.SerialPort.BytesToWrite; } }

        public void PurgeQueue()
        {
            this.SerialPort.DiscardInBuffer();
            this.SerialPort.DiscardOutBuffer();
        }

        Parity ParseParity(string parity)
        {
            Parity res = Parity.None;

            switch (parity)
            {

                case "E":
                    res = Parity.Even;
                    break;

                case "O":
                    res = Parity.Odd;
                    break;

                case "M":
                    res = Parity.Mark;
                    break;

                case "S":
                    res = Parity.Space;
                    break;
            }

            return res;
        }
#endif


//        public SPortLib.SPortAx Port { get { return this.SerialPort; } }
 
        public string Reply { get; private set; }

        public void Close()
        {
            if(this.IsOpen)
                this.SerialPort.Close();                
        }

        public void WriteByte(byte data)
        {
#if USEELTIMA
            this.SerialPort.Write(ref data, 1);
#else
            this.SerialPort.Write(new byte[1] { data }, 0, 1);
#endif
        }

        public void WriteBytes(byte[] bytes, int len)
        {
#if USEELTIMA
            this.SerialPort.Write(ref bytes[0], len);
#else
            this.SerialPort.Write(bytes, 0, len);
#endif
        }

        public void WriteString(string data)
        {
#if USEELTIMA
            this.SerialPort.WriteStr(data);
#else
            this.SerialPort.Write(data);
#endif
        }

        public void WriteCommand (string command)
        {
            this.state = Comms.State.AwaitAck;

            if (command.Length > 1 || command == "%")
                command += "\r";
#if USEELTIMA
            this.SerialPort.WriteStr(command);
#else
            this.SerialPort.Write(command);
#endif
        }

        public string getReply(string command)
        {
            this.Reply = "";
            this.WriteCommand(command);

            while (this.state == Comms.State.AwaitAck)
                Application.DoEvents();

            return this.Reply;
        }

#if USEELTIMA
        private void SerialRead()
        {
            int pos = 0;

            lock (this.input)
            {
                this.input.Append(this.SerialPort.ReadStr());

                while (this.input.Length > 0 && (pos = this.input.ToString().IndexOf('\n')) > 0)
                {
                    this.Reply = this.input.ToString(0, pos - 1);
                    this.input.Remove(0, pos + 1);
                    this.state = this.Reply == "ok" ? State.ACK : (this.Reply == "FAILED" ? State.NAK : State.DataReceived);
                    if (this.Reply.Length != 0 && this.DataReceived != null)
                        this.DataReceived(this.Reply);
                }
            }
        }
#else
        void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int pos = 0;

            lock (this.input)
            {
                this.input.Append(this.SerialPort.ReadExisting());

                while (this.input.Length > 0 && (pos = this.input.ToString().IndexOf('\n')) > 0)
                {
                    this.Reply = this.input.ToString(0, pos - 1);
                    this.input.Remove(0, pos + 1);
                    this.state = this.Reply == "ok" ? State.ACK : (this.Reply == "FAILED" ? State.NAK : State.DataReceived);
                    if (this.Reply.Length != 0 && this.DataReceived != null)
                        this.DataReceived(this.Reply);
                }
            }
        }
#endif
    }
}
