/*
 * MPGPending.cs - part of CNC Controls library for Grbl
 *
 * v0.01 / 2019-04-17 / Io Engineering (Terje Io)
 *
 */

/*

Copyright (c) 2019, Io Engineering (Terje Io)
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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CNC_Controls;
using CNC_App;

namespace CNC_Controls
{
    public partial class MPGPending : Form
    {
        private delegate void CloseCallback();

        private GrblStatusParameters parameters = new GrblStatusParameters();

        public MPGPending()
        {
            InitializeComponent();

            Cancelled = true;

            this.FormClosing += new FormClosingEventHandler(MPGPending_FormClosing);
            Comms.com.DataReceived += new DataReceivedHandler(DataReceived);
        }

        public bool Cancelled { get; private set; }

        void MPGPending_FormClosing(object sender, FormClosingEventArgs e)
        {
            Comms.com.DataReceived -= DataReceived; 
        }

        private void ClosedDialog()
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new CloseCallback(this.Close), new object[] { });
            else
                this.Close();
        }

        private void DataReceived(string data)
        {
            if (data.Length > 1 && data.Substring(0, 1) == "<")
            {
                bool parseState = true;
                data = data.Remove(data.Length - 1);

                string[] elements = data.Split('|');

                foreach (string e in elements)
                {
                    string[] pair = e.Split(':');

                    if (parseState)
                    {
                        parseState = false;
                    }
                    else if (parameters.Set(pair[0], pair[1]))
                    {
                        if (pair[0] == "MPG" && pair[1] == "0")
                        {
                            Cancelled = false;
                            this.ClosedDialog();
                        }
                    }
                }
            }
        }
    }
}
