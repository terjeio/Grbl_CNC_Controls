/*
 * GrblStatusControl.cs - part of CNC Controls library for Grbl
 *
 * 2018-09-14 / Io Engineering (Terje Io)
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
    public partial class GrblStatusControl : UserControl
    {
        private delegate void SetTextCallback(string text, Color color);

        public GCodeJob GCodeSender;

        public GrblStatusControl()
        {
            InitializeComponent();

            this.btnReset.Click += new EventHandler(btnReset_Click);
            this.btnHome.Click += new EventHandler(btnHome_Click);
            this.btnUnlock.Click += new EventHandler(btnUnlock_Click);
            this.chkCheckMode.CheckedChanged += new EventHandler(chkCheckMode_CheckedChanged);
        }

        public bool HomingEnabled { get; set; }

        void chkCheckMode_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked ? GCodeSender.state == GrblStates.Idle : GCodeSender.state == GrblStates.Check)
                GCodeSender.SendMDICommand(GCodeSender.state == GrblStates.Idle ? GrblConstants.CMD_CHECK : ((char)GrblConstants.CMD_RESET).ToString());
        }

        void btnUnlock_Click(object sender, EventArgs e)
        {
            GCodeSender.SendMDICommand(GrblConstants.CMD_UNLOCK);
        }

        void btnHome_Click(object sender, EventArgs e)
        {
            GCodeSender.SendMDICommand(GrblConstants.CMD_HOMING);

            // G90 G10 L20 P0 X0 Y0 Z0
        }

        void btnReset_Click(object sender, EventArgs e)
        {
            GCodeSender.SendReset();
        }

        public void Enable(bool enable)
        {
            this.btnHome.Enabled = enable && HomingEnabled;
        }

        public void CheckModeEnable(bool enable)
        {
            this.chkCheckMode.Enabled = enable;
        }

        public void setCheckMode(bool check)
        {
            if (chkCheckMode.Checked != check && GCodeSender.StreamingState == StreamingState.Idle)
                chkCheckMode.Checked = check;
        }

        public void DisplayState(string s, Color color)
        {
            if (this.txtState.InvokeRequired)
                this.Invoke(new SetTextCallback(DisplayState), new object[] { s, color });
            else
            {
                this.txtState.Text = s;
                this.txtState.BackColor = color;
            }
        }
    }
}
