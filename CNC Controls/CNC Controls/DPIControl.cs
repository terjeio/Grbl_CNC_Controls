/*
 * DPIControl.cs - part of CNC Controls library
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
    public partial class DPIControl : UserControl
    {

        private bool tbrClicked = false;
        private int _realSpeed = 100;
        private string dpiCommand = "", powerCommand = "", dutyCycleCommand = "", speedCommand = "";

        public double StepsPerMM = 47.0;

        public delegate void CommandGeneratedHandler(string command);
        public event CommandGeneratedHandler CommandGenerated;

        public DPIControl()
        {
            InitializeComponent();

            this.tbrDutyCycle.MouseDown += new MouseEventHandler(tbrMouseDown);
            this.tbrDutyCycle.MouseUp += new MouseEventHandler(tbrDutyCycle_MouseUp);
            this.tbrDutyCycle.Scroll += new EventHandler(tbrDutyCycle_Scrolled);

            this.tbrMinPower.MouseDown += new MouseEventHandler(tbrMouseDown);
            this.tbrMinPower.Scroll += new EventHandler(tbrMinPower_Scrolled);
            this.tbrMinPower.MouseUp += new MouseEventHandler(tbrMinPower_MouseUp);

            this.tbrMaxPower.MouseDown += new MouseEventHandler(tbrMouseDown);
            this.tbrMaxPower.Scroll += new EventHandler(tbrMaxPower_Scrolled);
            this.tbrMaxPower.MouseUp += new MouseEventHandler(tbrMaxPower_MouseUp);

            this.tbrSpeed.MouseDown += new MouseEventHandler(tbrMouseDown);
            this.tbrSpeed.MouseUp += new MouseEventHandler(tbrSpeed_MouseUp);
            this.tbrSpeed.Scroll += new EventHandler(tbrSpeed_Scrolled);

            this.cmbDPI.Text = "600";
        }

        public ComboBox cbxDPI { get { return this.cmbDPI; } }
        public int DPI { get { return int.Parse(this.cmbDPI.Text); } set { this.cmbDPI.Text = value.ToString(); } }
        
        public int RealSpeed
        {
            get { return this._realSpeed; }
            set
            {
                this._realSpeed = value;
                displayPulseWidth();
            }
        }

        public int MinPower {
            get { return this.tbrMinPower.Value; }
            set {
                this.tbrMinPower.Value = value;
                setLaserPower();
            }
        }

        public int MaxPower {
            get {
                return this.tbrMaxPower.Value;
            }
            set {
                this.tbrMaxPower.Value = Math.Max(value, this.tbrMaxPower.Minimum);
                setLaserPower();
            }
        }

        public int Speed {
            get { return this.tbrSpeed.Value; }
            set {
                this.tbrSpeed.Value = value;
                this.txtSpeed.Text = value.ToString() + "%";
                displayPulseWidth();
            }
        }

        public int DutyCycle {
            get { return this.tbrDutyCycle.Value; }
            set {
                this.tbrDutyCycle.Value = value;
                this.txtDutyCycle.Text = value.ToString() + "%";
                displayPulseWidth();
            }
        }

        public string DPICommand { get { return string.Format(this.dpiCommand, this.DPI.ToString()); } set { this.dpiCommand = value; } }
        public string PowerCommand { get { return string.Format(this.powerCommand, MinPower, MaxPower); } set { this.powerCommand = value; } }
        public string DutyCycleCommand { get { return string.Format(this.dutyCycleCommand, DutyCycle); } set { this.dutyCycleCommand = value; } }
        public string SpeedCommand { get { return string.Format(this.speedCommand, Speed); } set { this.speedCommand = value; } }

        void cmbDPI_TextChanged(object sender, EventArgs e)
        {
            displayPulseWidth();
        }

        void tbrMouseDown(object sender, MouseEventArgs e)
        {
            this.tbrClicked = true;
        }

        void tbrDutyCycle_Scrolled(object sender, EventArgs e)
        {
            if (!this.tbrClicked)
                this.setDutyCycle();
        }

        void tbrDutyCycle_MouseUp(object sender, MouseEventArgs e)
        {
            this.tbrClicked = false;
            this.setDutyCycle();
        }

        void tbrMinPower_Scrolled(object sender, EventArgs e)
        {
            if (!this.tbrClicked)
            {
                if (this.tbrMinPower.Value > this.tbrMaxPower.Value)
                    this.tbrMaxPower.Value = this.tbrMinPower.Value + 1;
                this.setLaserPower();
            }
        }

        void tbrMinPower_MouseUp(object sender, MouseEventArgs e)
        {
            this.tbrClicked = false;
            if (this.tbrMinPower.Value > this.tbrMaxPower.Value)
                this.tbrMaxPower.Value = this.tbrMinPower.Value + 1;
            this.setLaserPower();
        }

        void tbrMaxPower_Scrolled(object sender, EventArgs e)
        {
            if (!this.tbrClicked)
            {
                if (this.tbrMinPower.Value > this.tbrMaxPower.Value)
                    this.tbrMinPower.Value = this.tbrMaxPower.Value - 1;
                this.setLaserPower();
            }
        }

        void tbrMaxPower_MouseUp(object sender, MouseEventArgs e)
        {
            this.tbrClicked = false;
            if (this.tbrMinPower.Value > this.tbrMaxPower.Value)
                this.tbrMinPower.Value = this.tbrMaxPower.Value - 1;
            this.setLaserPower();
        }

        void tbrSpeed_MouseUp(object sender, MouseEventArgs e)
        {
            this.tbrClicked = false;
            this.setSpeed();
        }

        void tbrSpeed_Scrolled(object sender, EventArgs e)
        {
            if (!this.tbrClicked)
                this.setSpeed();
        }

        void displayPulseWidth()
        {

            //// 1200.0 / double.Parse(this.cmbDPI.Text)
            double pw = 1 / (this.StepsPerMM * ((this._realSpeed * ((double)this.Speed) / 100.0) / ((double)this.DutyCycle / 100.0))) * 1000.0;

            this.txtPulseWidth.Text = this.DutyCycle == 100 ? "" : string.Format("{0} ms/pixel", pw.ToString("#.##"));
            this.txtPulseWidth.ForeColor = pw < 1.0 || this.tbrDutyCycle.Value >= 90 ? Color.Red : Color.Black;
        }

        void setDutyCycle()
        {
            this.DutyCycle = this.tbrDutyCycle.Value;
            if (CommandGenerated != null)
                CommandGenerated(string.Format("DutyCycle:" + this.tbrDutyCycle.Value.ToString()));
        }

        void setSpeed()
        {
            displayPulseWidth();
            this.Speed = this.tbrSpeed.Value;
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
            this.txtMaxPower.Text = this.tbrMaxPower.Value.ToString() + "%";
            this.txtMinPower.Text = this.tbrMinPower.Value.ToString() + "%";
            if(CommandGenerated != null)
                CommandGenerated(PowerCommand);
        }

    }
}
