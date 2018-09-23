/*
 * CoolantControl.cs - part of CNC Controls library
 *
 * 2018-09-22 / Io Engineering (Terje Io)
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

namespace CNC_Controls
{
    public partial class CoolantControl : UserControl
    {

        public bool Silent = false;

        public delegate void CommandGeneratedHandler(string command);
        public event CommandGeneratedHandler CommandGenerated;

        private string floodCommand, mistCommand;

        public CoolantControl()
        {
            InitializeComponent();

            this.floodCommand = ((char)GrblConstants.CMD_COOLANT_FLOOD_OVR_TOGGLE).ToString();
            this.mistCommand = ((char)GrblConstants.CMD_COOLANT_MIST_OVR_TOGGLE).ToString();

            this.chkMist.CheckedChanged += new EventHandler(chkMist_CheckedChanged);
            this.chkFlood.CheckedChanged += new EventHandler(chkCoolant_CheckedChanged);
        }

        public bool EnableControl
        {
            get { return this.chkFlood.Enabled; }
            set
            {
                this.chkFlood.Enabled = this.chkMist.Enabled = value;
            }
        }

        public bool MistOn { get { return this.chkMist.Checked; } set { this.chkMist.Checked = value; } }
        public bool FloodOn { get { return this.chkFlood.Checked; } set { this.chkFlood.Checked = value; } }

        public string FloodCommand { get { return string.Format(this.floodCommand, FloodOn ? "1" : "0"); } set { this.floodCommand = value; } }
        public string MistCommand { get { return string.Format(this.mistCommand, MistOn ? "1" : "0"); } set { this.mistCommand = value; } }

        void chkCoolant_CheckedChanged(object sender, EventArgs e)
        {
            if (!Silent)
                CommandGenerated(this.floodCommand);
        }

        void chkMist_CheckedChanged(object sender, EventArgs e)
        {
            if (!Silent)
                CommandGenerated(this.MistCommand);
        }
    }
}
