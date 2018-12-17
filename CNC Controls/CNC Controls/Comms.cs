/*
 * Comms.cs - part of CNC Controls library
 *
 * 2018-12-10 / Io Engineering (Terje Io)
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
//#define RESPONSELOG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace CNC_Controls
{
    public delegate void DataReceivedHandler (string data);

    public class Comms
    {
        public enum State
        {
            AwaitAck,
            DataReceived,
            ACK,
            NAK
        }

        public const int TXBUFFERSIZE = 4096, RXBUFFERSIZE = 1024;

        public static StreamComms com = null;
    }

    public interface StreamComms
    {

        bool IsOpen { get; }
        int OutCount { get; }
        string Reply { get; }
        Comms.State CommandState { get; set; }

        void Close();
        void WriteByte(byte data);
        void WriteBytes(byte[] bytes, int len);
        void WriteString(string data);
        void WriteCommand (string command);
        string getReply(string command);
        void PurgeQueue();

        event DataReceivedHandler DataReceived;
    }

#if USEELTIMA
    public class SerialComms : StreamComms
    {

        private SPortLib.SPortAx SerialPort;
        private StringBuilder input = new StringBuilder(300);
        private volatile Comms.State state = Comms.State.ACK;

        public event DataReceivedHandler DataReceived;

        public SerialComms (string PortParams)
        {
            Comms.com = this;
            this.Reply = "";

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
 //           this.SerialPort.HandShake = 0x08; // Cannot be used with ESP32
            this.SerialPort.FlowReplace = 0x80;
            this.SerialPort.CharEvent = 10;
            this.SerialPort.InBufferSize = Comms.RXBUFFERSIZE;
            this.SerialPort.OutBufferSize = Comms.TXBUFFERSIZE;
            this.SerialPort.BlockMode = false;


            this.SerialPort.Open(PortParams.Substring(0, PortParams.IndexOf(":")));
            this.SerialPort.OnRxFlag += new SPortLib._ISPortAxEvents_OnRxFlagEventHandler(this.SerialRead);

            System.Threading.Thread.Sleep(500);

            if (this.SerialPort.IsOpened)
            {
                /* For resetting ESP32, use DTR for Arduino
                this.SerialPort.RtsEnable = true;
                this.SerialPort.RtsEnable = false;
                System.Threading.Thread.Sleep(300);
                */
                this.SerialPort.PurgeQueue();
                this.SerialPort.OnRxFlag += new SPortLib._ISPortAxEvents_OnRxFlagEventHandler(this.SerialRead);
            }
        }

        ~SerialComms()
        {
            this.Close();
        }

        public Comms.State CommandState { get { return this.state; } set { this.state = value; } }
        public string Reply { get; private set; }
        public bool IsOpen { get { return this.SerialPort.IsOpened; } }
        public int OutCount { get { return this.SerialPort.OutCount; } }

        public void PurgeQueue()
        {
            this.SerialPort.PurgeQueue();
        }

        public void Close()
        {
            if(this.IsOpen)
                this.SerialPort.Close();                
        }

        public void WriteByte(byte data)
        {
            this.SerialPort.Write(ref data, 1);
        }

        public void WriteBytes(byte[] bytes, int len)
        {
            this.SerialPort.Write(ref bytes[0], len);
        }

        public void WriteString(string data)
        {
            this.SerialPort.WriteStr(data);
        }

        public void WriteCommand (string command)
        {
            this.state = Comms.State.AwaitAck;

            if (command.Length > 1 || command == GrblConstants.CMD_PROGRAM_DEMARCATION)
                command += "\r";

            this.SerialPort.WriteStr(command);
        }

        public string getReply(string command)
        {
            this.Reply = "";
            this.WriteCommand(command);

            while (this.state == Comms.State.AwaitAck)
                Application.DoEvents();

            return this.Reply;
        }

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
                    this.state = this.Reply == "ok" ? Comms.State.ACK : (this.Reply.StartsWith("error") ? Comms.State.NAK : Comms.State.DataReceived);
                    if (this.Reply.Length != 0 && this.DataReceived != null)
                        this.DataReceived(this.Reply);
                }
            }
        }
    }

#else

    public class SerialComms : StreamComms
    {

        private SerialPort SerialPort = null;
        private StringBuilder input = new StringBuilder(400);
        private volatile Comms.State state = Comms.State.ACK;

        public event DataReceivedHandler DataReceived;

#if RESPONSELOG
        StreamWriter log = null;
#endif

        public SerialComms(string PortParams)
        {
            Comms.com = this;
            this.Reply = "";

            string[] parameter = PortParams.Substring(PortParams.IndexOf(":") + 1).Split(',');

            if (parameter.Count() < 4)
            {
                MessageBox.Show("Unable to open serial port: " + PortParams, "GCode Sender");
                System.Environment.Exit(2);
            }

            this.SerialPort = new SerialPort();
            this.SerialPort.PortName = PortParams.Substring(0, PortParams.IndexOf(":"));
            this.SerialPort.BaudRate = int.Parse(parameter[0]);
            this.SerialPort.Parity = ParseParity(parameter[1]);
            this.SerialPort.DataBits = int.Parse(parameter[2]);
            this.SerialPort.StopBits = int.Parse(parameter[3]) == 1 ? StopBits.One : StopBits.Two;
//            if (parameter.Count() > 4) // Cannot be used With ESP32
//                this.SerialPort.Handshake = parameter[4] == "X" ? Handshake.XOnXOff : Handshake.RequestToSend; 
            this.SerialPort.Handshake = Handshake.None;
            this.SerialPort.ReceivedBytesThreshold = 1;
            this.SerialPort.ReadTimeout = 5000;
            this.SerialPort.NewLine = "\r\n";
            this.SerialPort.ReadBufferSize = Comms.RXBUFFERSIZE;
            this.SerialPort.WriteBufferSize = Comms.TXBUFFERSIZE;

            try
            {
                this.SerialPort.Open();
            }
            catch
            {
            }

            if (this.SerialPort.IsOpen)
            {
                /* For resetting ESP32, use DTR for Arduino */    
                this.SerialPort.RtsEnable = true;
                this.SerialPort.RtsEnable = false;
                           System.Threading.Thread.Sleep(2000);

                this.PurgeQueue();
                this.SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
#if RESPONSELOG
                this.log = new StreamWriter(@"D:\grbl.txt");
#endif
            }
        }

        ~SerialComms()
        {
#if RESPONSELOG
            this.log.Close();
#endif
            this.Close();
        }

        public Comms.State CommandState { get { return this.state; } set { this.state = value; } }
        public string Reply { get; private set; }
        public bool IsOpen { get { return this.SerialPort != null && this.SerialPort.IsOpen; } }
        public int OutCount { get { return this.SerialPort.BytesToWrite; } }

        public void PurgeQueue()
        {
            this.SerialPort.DiscardInBuffer();
            this.SerialPort.DiscardOutBuffer();
            this.Reply = "";
        }

        private Parity ParseParity(string parity)
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

        public void Close()
        {
            if (this.IsOpen)
            {
                try
                {
                    this.SerialPort.DataReceived -= SerialPort_DataReceived;
                    this.SerialPort.DtrEnable = false;
                    this.SerialPort.RtsEnable = false;
                    this.SerialPort.DiscardInBuffer();
                    this.SerialPort.DiscardOutBuffer();
                    System.Threading.Thread.Sleep(100);
                    this.SerialPort.Close();
                    this.SerialPort = null;
                }
                catch { }
            }
        }

        public void WriteByte(byte data)
        {
            this.SerialPort.Write(new byte[1] { data }, 0, 1);
        }

        public void WriteBytes(byte[] bytes, int len)
        {
            this.SerialPort.Write(bytes, 0, len);
        }

        public void WriteString(string data)
        {
            this.SerialPort.Write(data);
        }

        public void WriteCommand(string command)
        {
            this.state = Comms.State.AwaitAck;

            if (command.Length > 1 || command == GrblConstants.CMD_PROGRAM_DEMARCATION)
                command += "\r";

            this.SerialPort.Write(command);
        }

        public string getReply(string command)
        {
            this.Reply = "";
            this.WriteCommand(command);

            while (this.state == Comms.State.AwaitAck)
                Application.DoEvents();

            return this.Reply;
        }

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
#if RESPONSELOG
                    this.log.WriteLine(this.Reply);
#endif
                    this.state = this.Reply == "ok" ? Comms.State.ACK : (this.Reply.StartsWith("error") ? Comms.State.NAK : Comms.State.DataReceived);
                    if (this.Reply.Length != 0 && this.DataReceived != null)
                        this.DataReceived(this.Reply);
                }
            }
        }
    }
#endif

    public class IPComms : StreamComms
    {
        private TcpClient ipserver = null;
        private NetworkStream ipstream = null;
        private byte[] buffer = new byte[512];
        private volatile Comms.State state = Comms.State.ACK;
        private StringBuilder input = new StringBuilder(300);

        public event DataReceivedHandler DataReceived;

        public IPComms (string host)
        {
            Comms.com = this;
            this.Reply = "";

            string[] parameter = host.Split(':');

            if(parameter.Length == 2) try
            {
                this.ipserver = new TcpClient(parameter[0], int.Parse(parameter[1]));
                this.ipstream = ipserver.GetStream();
                this.ipstream.BeginRead(this.buffer, 0, buffer.Length, ReadComplete, this.buffer);
            }
            catch
            {
            }
        }

        ~IPComms()
        {
            this.Close();
        }

        public bool IsOpen { get { return this.ipserver != null && this.ipserver.Connected; } }
        public int OutCount { get { return 0; } }
        public Comms.State CommandState { get { return this.state; } set { this.state = value; } }
        public string Reply { get; private set; }

        public void PurgeQueue()
        {
            while(this.ipstream.DataAvailable)
                this.ipstream.ReadByte();
            this.Reply = "";
        }

        public void Close()
        {
            if (this.IsOpen)
            {
                this.ipstream.Close(300);
                this.ipstream = null;
                this.ipserver.Close();
            }
        }

        public void WriteByte(byte data)
        {
            this.ipstream.Write(new byte[1] { data }, 0, 1);
        }

        public void WriteBytes(byte[] bytes, int len)
        {
            this.ipstream.Write(bytes, 0, len);
        }

        public void WriteString(string data)
        {
            byte[] bytes = Encoding.Default.GetBytes(data);
            this.ipstream.Write(bytes, 0, bytes.Length);
        }

        public void WriteCommand(string command)
        {
            this.state = Comms.State.AwaitAck;

            if (command.Length > 1 || command == GrblConstants.CMD_PROGRAM_DEMARCATION)
                command += "\r";

            WriteString(command);
        }

        public string getReply(string command)
        {
            this.Reply = "";
            this.WriteCommand(command);

            while (this.state == Comms.State.AwaitAck)
                Application.DoEvents();

            return this.Reply;
        }

        void ReadComplete(IAsyncResult iar)
        {
            int bytesAvailable = 0;
            byte[] buffer = (byte[])iar.AsyncState;

            try
            {
                bytesAvailable = this.ipstream.EndRead(iar);
            }
            catch
            {
                // error handling required here (and many other places)...
            }

            int pos = 0;

            lock (this.input)
            {
                this.input.Append(System.Text.Encoding.ASCII.GetString(buffer, 0, bytesAvailable));

                while (this.input.Length > 0 && (pos = this.input.ToString().IndexOf('\n')) > 0)
                {
                    this.Reply = this.input.ToString(0, pos - 1);
                    this.input.Remove(0, pos + 1);
                    this.state = this.Reply == "ok" ? Comms.State.ACK : (this.Reply.StartsWith("error") ? Comms.State.NAK : Comms.State.DataReceived);
                    if (this.Reply.Length != 0 && this.DataReceived != null)
                        this.DataReceived(this.Reply);
               }
            }

            if(this.ipstream != null)
                this.ipstream.BeginRead(this.buffer, 0, buffer.Length, ReadComplete, this.buffer);
        }
    }
}
