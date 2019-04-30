/*
 * FlowControl.cs - part of CNC Controls library
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

namespace CNC_Controls
{

    public partial class FlowControl : UserControl
    {

        public bool Silent = false;
        
        public delegate void CommandGeneratedHandler(string command);
        public event CommandGeneratedHandler CommandGenerated;

        public FlowControl()
        {
            InitializeComponent();

            this.chkExhaust.Tag = ((char)GrblConstants.CMD_COOLANT_FLOOD_OVR_TOGGLE).ToString();
            this.chkAir.Tag = ((char)GrblConstants.CMD_COOLANT_MIST_OVR_TOGGLE).ToString();
            this.chkCoolant.Tag = "M125P2Q{0}";

            this.chkAir.CheckedChanged += new EventHandler(chkAir_CheckedChanged);
            this.chkExhaust.CheckedChanged += new EventHandler(chkExhaust_CheckedChanged);
            this.chkCoolant.CheckedChanged += new EventHandler(chkCoolant_CheckedChanged);
        }

        public bool EnableControl
        {
            get { return this.chkCoolant.Enabled; }
            set {
                this.chkCoolant.Enabled = value;
                this.chkExhaust.TabStop = value;
                this.chkAir.TabStop = value;
            }
        }

        public bool ExhaustOn { get { return this.chkExhaust.Checked; } set { this.chkExhaust.Checked = value; } }
        public bool CoolantOn { get { return this.chkCoolant.Checked; } set { this.chkCoolant.Checked = value; } }
        public bool AirAssistOn { get { return this.chkAir.Checked; } set { this.chkAir.Checked = value; } }

        public string ExhaustCommand { get { return string.Format((string)this.chkExhaust.Tag, this.ExhaustOn ? "1" : "0"); } set { this.chkExhaust.Tag = value; } }
        public string CoolantCommand { get { return string.Format((string)this.chkCoolant.Tag, this.CoolantOn ? "1" : "0"); } set { this.chkCoolant.Tag = value; } }
        public string AirAssistCommand { get { return string.Format((string)this.chkAir.Tag, this.AirAssistOn ? "1" : "0"); } set { this.chkAir.Tag = value; } }

        private void chkExhaust_CheckedChanged(object sender, EventArgs e)
        {
            if (!Silent && this.chkExhaust.Enabled)
                CommandGenerated(this.ExhaustCommand);
        }

        void chkCoolant_CheckedChanged(object sender, EventArgs e)
        {
            if (!Silent)
                CommandGenerated(this.CoolantCommand);
        }

        void chkAir_CheckedChanged(object sender, EventArgs e)
        {
            if (!Silent)
                CommandGenerated(AirAssistCommand);
        }

    }
}
