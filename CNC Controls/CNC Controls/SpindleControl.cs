/*
 * SpindleControl.cs - part of CNC Controls library
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
using System.Globalization;

namespace CNC_Controls
{
    public partial class SpindleControl : UserControl
    {
        private bool silent = false;
        private SpindleState _state =  SpindleState.CW;

        public delegate void CommandGeneratedHandler(string command);
        public event CommandGeneratedHandler CommandGenerated;

        public SpindleControl()
        {
            InitializeComponent();
            this.SpindleState = SpindleState.Off;
            this.rbSpindleOff.Tag = "M5";
            this.rbSpindleOff.CheckedChanged += new EventHandler(rbSpindleState_CheckedChanged);
            this.rbSpindleCW.Tag = "M3{0}";
            this.rbSpindleCW.CheckedChanged += new EventHandler(rbSpindleState_CheckedChanged);
            this.rbSpindleCCW.Tag = "M4{0}";
            this.rbSpindleCCW.CheckedChanged += new EventHandler(rbSpindleState_CheckedChanged);
            this.txtRPM.KeyPress += new KeyPressEventHandler(txtRPM_KeyPress);

            this.overrideControl.ResetCommand = GrblConstants.CMD_SPINDLE_OVR_RESET;
            this.overrideControl.FineMinusCommand = GrblConstants.CMD_SPINDLE_OVR_FINE_MINUS;
            this.overrideControl.FinePlusCommand = GrblConstants.CMD_SPINDLE_OVR_FINE_PLUS;
            this.overrideControl.CoarseMinusCommand = GrblConstants.CMD_SPINDLE_OVR_COARSE_MINUS;
            this.overrideControl.CoarsePlusCommand = GrblConstants.CMD_SPINDLE_OVR_COARSE_PLUS;

            this.overrideControl.CommandGenerated += new OverrideControl.CommandGeneratedHandler(overrideControl_CommandGenerated);
        }

        public bool EnableControl
        {
            get { return this.txtRPM.Enabled; }
            set
            {
                this.rbSpindleOff.Enabled = this.rbSpindleCW.Enabled = this.rbSpindleCCW.Enabled = this.txtRPM.Enabled = value;
            }
        }

        public string SpindleOffCommand { get { return (string)this.rbSpindleOff.Tag; } set { this.rbSpindleOff.Tag = value; } }
        public string SpindleCWCommand { get { return (string)this.rbSpindleCW.Tag; } set { this.rbSpindleCW.Tag = value; } }
        public string SpindleCCWCommand { get { return (string)this.rbSpindleCCW.Tag; } set { this.rbSpindleCCW.Tag = value; } }

        public SpindleState SpindleState
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    silent = true;
                    rbSpindleOff.Checked = _state == SpindleState.Off;
                    rbSpindleCW.Checked = _state == SpindleState.CW;
                    rbSpindleCCW.Checked = _state == SpindleState.CCW;
                    silent = false;
                }
            }
        }

        public double RPM {
            get { return this.DesignMode ? 0 : double.Parse(this.txtRPM.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture); }
            set { this.txtRPM.Text = value.ToString(CultureInfo.InvariantCulture); }
        }
        
        public int Override { set { this.overrideControl.Value = value; } }
        public override bool Focused { get { return this.txtRPM.Focused; } }

        void rbSpindleState_CheckedChanged(object sender, EventArgs e)
        {
            if (!silent)
            {
                RadioButton rb = (RadioButton)sender;
                if(rb.Checked) {
                    _state = rb.Name == "rbSpindleCW" ? SpindleState.CW : (rb.Name == "rbSpindleCCW" ? SpindleState.CCW :  SpindleState.Off);
                    if (CommandGenerated != null)
                        CommandGenerated(string.Format((string)rb.Tag, "S" + this.txtRPM.Text));
                }
            }
            txtRPM.ReadOnly = _state != SpindleState.Off;
        }

        private void txtRPM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        void overrideControl_CommandGenerated(string command)
        {
            if (CommandGenerated != null)
                CommandGenerated(command);
        }
    }
}
