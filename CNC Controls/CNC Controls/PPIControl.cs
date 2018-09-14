/*
 * PPIControl.cs - part of CNC Controls library for Grbl
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
    public partial class PPIControl : UserControl
    {

        private bool tbrClicked = false;
        private double speed = 0.0;
        private string powerCommand, ppiCommand, pulseWidthCommand;

        public delegate void CommandGeneratedHandler(string command);
        public event CommandGeneratedHandler CommandGenerated;

        public PPIControl()
        {
            InitializeComponent();

            this.powerCommand = "M4S{0}";
            this.ppiCommand = "M124P{0}";
            this.pulseWidthCommand = "M123P{0}";

            this.tbrPulseWidth.MouseDown += new MouseEventHandler(tbrMouseDown);
            this.tbrPulseWidth.MouseUp += new MouseEventHandler(tbrPulseWidth_MouseUp);
            this.tbrPulseWidth.Scroll += new EventHandler(tbrPulseWidth_Scrolled);

            this.tbrMaxPower.MouseDown += new MouseEventHandler(tbrMouseDown);
            this.tbrMaxPower.Scroll += new EventHandler(tbrMaxPower_Scrolled);
            this.tbrMaxPower.MouseUp += new MouseEventHandler(tbrMaxPower_MouseUp);

            this.tbrPPI.MouseDown += new MouseEventHandler(tbrMouseDown);
            this.tbrPPI.MouseUp += new MouseEventHandler(tbrPPI_MouseUp);
            this.tbrPPI.Scroll += new EventHandler(tbrPPI_Scrolled);

        }

        public override bool Focused { get { return this.tbrPulseWidth.Focused || this.tbrMaxPower.Focused || this.tbrPPI.Focused ; } }

        public double Speed {
            get { return speed; }
            set {
                speed = value;
                this.txtSpeed.Text = speed.ToString("#");
                this.displayPulseWidth();
            }
        }

        public int Power {
            get { return this.tbrMaxPower.Value; }
            set {
                if (!this.tbrClicked)
                {
                    this.tbrMaxPower.Value = Math.Max(value, this.tbrMaxPower.Minimum);
                    this.txtPower.Text = this.tbrMaxPower.Value.ToString() + "%";
                }
            }
        }

        public int PulseWidth {
            get { return this.tbrPulseWidth.Value; }
            set {
                this.tbrPulseWidth.Value = value;
                this.txtPulseWidth.Text = value.ToString();
                this.displayPulseWidth();
            }
        }
        
        public int PPI {
            get { return this.tbrPPI.Value <= 25 ? 0 : this.tbrPPI.Value; }
            set {
                this.tbrPPI.Value = value < 25 ? 0 : value;
                this.txtPPI.Text = PPI == 0 ? "off" : PPI.ToString();
                this.tbrPulseWidth.Enabled = PPI != 0;
                this.txtPulseWidth.Text = PPI == 0 ? "-" : PulseWidth.ToString();
                this.displayPulseWidth();
            }
        }

        public string PowerCommand {
            get { return string.Format(this.powerCommand, Power); }
            set { this.powerCommand = value; }
        }
        public string PulseWidthCommand {
            get { return string.Format(this.pulseWidthCommand, PulseWidth); } 
            set { this.pulseWidthCommand = value; } 
        }
        public string PPICommand {
            get { return string.Format(this.ppiCommand, PPI); }
            set { this.ppiCommand = value; }
        }

        void tbrMouseDown(object sender, MouseEventArgs e)
        {
            this.tbrClicked = true;
        }

        void tbrPulseWidth_Scrolled(object sender, EventArgs e)
        {
            if (!this.tbrClicked)
                this.setPulseWidth();
        }

        void tbrPulseWidth_MouseUp(object sender, MouseEventArgs e)
        {
            this.tbrClicked = false;
            this.setPulseWidth();
        }

        void tbrMaxPower_Scrolled(object sender, EventArgs e)
        {
            if (!this.tbrClicked)
            {
                this.setLaserPower();
            }
        }

        void tbrMaxPower_MouseUp(object sender, MouseEventArgs e)
        {
            this.tbrClicked = false;
            this.setLaserPower();
        }

        void tbrPPI_MouseUp(object sender, MouseEventArgs e)
        {
            this.tbrClicked = false;
            this.setPPI();
        }

        void tbrPPI_Scrolled(object sender, EventArgs e)
        {
            if (!this.tbrClicked)
                this.setPPI();
        }

        void setLaserPower()
        {
            /*
                        if (laserOn && !this.zOn.Checked)
                        {
                            this.zOn.Checked = true;
                            LaserPower(true);
                        }
            */
            this.Power = this.tbrMaxPower.Value;
            if(CommandGenerated != null)
                CommandGenerated(PowerCommand);

        }
        
        void setPulseWidth()
        {
            this.PulseWidth = this.tbrPulseWidth.Value;
            if (CommandGenerated != null)
                CommandGenerated(PulseWidthCommand);
        }

        void setPPI()
        {
            this.PPI = this.tbrPPI.Value;
            if (CommandGenerated != null)
                CommandGenerated(PPICommand);
        }

        void displayPulseWidth()
        {

            double pw = 0.0;
            bool continuous = speed == 0.0 || this.PPI == 0;

            if (continuous)
                this.txtPulseWidths.Text = "---";
            else
            {
                //// 1200.0 / double.Parse(this.cmbDPI.Text)
                pw = (25.4 / this.PPI) / (this.speed / 60.0) * 1000.0;

                this.txtPulseWidths.Text = string.Format("{0} ms/pulse", pw.ToString("#.##"));
            }
            this.txtPulseWidths.ForeColor = !continuous && pw * 1000 < this.PulseWidth ? Color.Red : Color.Black;
        }

    }
}
