/*
 * TurningWizard.cs - part of CNC Controls library
 *
 * v0.01 / 2019-05-14 / Io Engineering (Terje Io)
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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using CNC_ThreadData;

namespace CNC_Controls
{
    public partial class TurningWizard : UserControl, CNC_App.IRenderer, IWizard
    {
        private double unit = 1.0d;
        private ErrorProvider error;
        private bool initOk = false, resetProfileBindings = true;
        private string last_rpm = "", last_css = "";
        private BindingSource profileSource = new BindingSource();

        public event GCodePushHandler GCodePush;

        public TurningWizard()
        {
            InitializeComponent();

            this.Load += new EventHandler(TurningWizard_Load);
        }

        void TurningWizard_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            error = new ErrorProvider(this);

            UIUtils.SetMask(txtTaper, "#0.0##");
            UIUtils.SetMask(txtSpindleRPM, "###0");
        //    cvFeedRate.Format = "###0";

            cbxProfile.ValueMember = "Id";
            cbxProfile.DisplayMember = "Name";
            cbxProfile.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxProfile.SelectedIndexChanged += new EventHandler(cbxProfile_SelectedIndexChanged);

            btnCalculate.Click += new EventHandler(btnCalculate_Click);
            btnOptions.Click += new EventHandler(btnOptions_Click);
            chkSpringPasses.CheckedChanged += new EventHandler(chkSpringPasses_CheckedChanged);
            chkTaper.CheckedChanged += new EventHandler(chkTaper_CheckedChanged);
            chkCSS.CheckedChanged += new EventHandler(chkCSS_CheckedChanged);
            txtGCode.KeyPress += new KeyPressEventHandler(txtGCode_KeyPress);

            UIUtils.GroupBoxCaptionBold(groupBox1);
            UIUtils.GroupBoxCaptionBold(groupBox2);

            toolTip.ShowAlways = true;
            toolTip.SetToolTip(cvPassDepth.Field, "Same for radius and diameter mode.");
            toolTip.SetToolTip(cvDiameter.Field, "Current measured diameter of stock.");
            toolTip.SetToolTip(txtTaper, "Degrees from centerline.");
        }

        #region Methods required by IRenderer interface

        public CNC_App.UIMode mode { get { return CNC_App.UIMode.Turning; } }

        public void Activate(bool activate, CNC_App.UIMode chgMode)
        {
            if (activate && GrblSettings.Loaded)
            {
                if (!initOk)
                {
                    initOk = true;

                    if (config == null)
                    {
                        config = new WizardConfig(this, "Turning");
                        config.Load();
                        profileSource.DataSource = config.Profiles;
                        cbxProfile.DataSource = profileSource;
                    }

                    config.Update();

                    if (fMetric != config.metric)
                    {
                        fMetric = config.metric;
                        chkCSS.Checked = config.css;
                        SetUnitLabels(this);
                    }
                }
                else
                    txtGCode.Text = "";
            }
        }

        public void CloseFile()
        {
        }

        #endregion

        public void InitUI()
        {
            txtGCode.Text = "";
            last_css = config.css && config.rpm != 0.0d ? config.rpm.ToString(CultureInfo.InvariantCulture) : "";
            last_rpm = config.css || config.rpm == 0.0d ? "" : config.rpm.ToString(CultureInfo.InvariantCulture);

            cvClearanceX.Value = config.xclear;
            cvPassDepth.Value = config.passdepth_first;
            cvPassDepthLast.Value = config.passdepth_last;
            cvFeedRate.Value = config.feedrate;
            cvFeedRateLast.Value = config.feedrate_last;
            chkCSS.Checked = config.css;
            txtSpindleRPM.Text = chkCSS.Checked ? last_css : last_rpm;

            if (resetProfileBindings)
            {
                profileSource.ResetBindings(false);
                cbxProfile.SelectedIndex = config.Current.Id;
            }
        }

        #region Attributes

        public WizardConfig config { get; private set; }

        int precision { get { return fMetric ? 3 : 4; } }

        String formstr { get { return fMetric ? "0.0##" : "0.0###"; } }

        public bool fMetric
        {
            get
            {
                return unit == 1.0d;
            }
            set
            {
                unit = value ? 1.0d : 25.4d;
            }
        }

        #endregion

        #region UIUtils

        private void SetUnitLabels(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is CoordValueControl)
                    ((CoordValueControl)control).Metric = fMetric;
                else if (control is GroupBox)
                    SetUnitLabels(control);
            }
        }

        private String FormatValue(double value)
        {
            return Math.Round(value, precision).ToString(formstr, CultureInfo.InvariantCulture);
        }

        private double UIGetDouble(Control widget)
        {
            double value = 0.0d;
            double.TryParse(widget.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value);

            return Math.Round(value, precision);
        }

        #endregion

        #region UIEvents

        void cbxProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetProfileBindings = false;
            config.SelectProfile(config.Profiles[(int)cbxProfile.SelectedValue]);
            resetProfileBindings = true;
        }

        void btnOptions_Click(object sender, EventArgs e)
        {
            LatheOptions dialog = new LatheOptions(this, config, mode);

            dialog.ShowDialog();

            dialog.Dispose();
        }

        void txtGCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) && e.KeyChar == 0x01)
                txtGCode.SelectAll();
        } 

        void chkCSS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCSS.Checked)
                last_rpm = txtSpindleRPM.Text;
            else
                last_css = txtSpindleRPM.Text;
            unitSpindleRPM.Text = chkCSS.Checked ? "m/min" : "RPM";
            lblSpindleRPM.Text = chkCSS.Checked ? "Speed:" : "Spindle:";
            txtSpindleRPM.Text = chkCSS.Checked ? last_css : last_rpm;
        }

        void chkTaper_CheckedChanged(object sender, EventArgs e)
        {
            txtTaper.ReadOnly = !chkTaper.Checked;
        }

        void chkSpringPasses_CheckedChanged(object sender, EventArgs e)
        {
            txtSpringPasses.Enabled = chkSpringPasses.Checked;
        }

        #endregion

        #region GCodeGenerator

        void btnCalculate_Click(object sender, EventArgs e)
        {
            double speed = UIGetDouble(txtSpindleRPM);
            bool css = chkCSS.Checked;

            if (cvFeedRate.Value > config.zspeed)
            {
                error.SetError(cvFeedRate.Field, "Feed rate > max allowed.");
                return;
            }

            if (cvFeedRate.Value == 0.0d)
            {
                error.SetError(cvFeedRate.Field, "Feed rate is required.");
                return;
            }

            if (speed == 0.0d)
            {
                error.SetError(cvFeedRate.Field, "Spindle RPM is required.");
                return;
            }

            if (css)
                speed = Math.Round(speed / (Math.PI * cvDiameter.Value) * (fMetric ? 1000.0d : 12.0d * 25.4d), 0);

            if (speed > config.rpm_max && config.css_max_rpm == 0.0d)
            {
                error.SetError(txtSpindleRPM, "Spindle RPM > max allowed.");
                return;
            }

            if (speed < config.rpm_min)
            {
                error.SetError(txtSpindleRPM, "Spindle RPM < min allowed.");
                return;
            }

            double passdepth = cvPassDepth.Value / unit;
            double passdepth_last = cvPassDepthLast.Value / unit;

            if (passdepth_last > passdepth)
            {
                error.SetError(cvPassDepth.Field, "Last pass cut depth must be smaller than cut depth.");
                error.SetError(cvPassDepthLast.Field, "Last pass cut depth must be smaller than cut depth.");
                return;
            }

            double zstart = cvStart.Value / unit;
            double zlength = cvLength.Value / unit;
            double ztarget = (zstart + zlength * config.zdir) / unit;;
            double xclearance = cvClearanceX.Value / unit;
            double xtarget = cvTargetX.Value / unit;
            double diameter = cvDiameter.Value / unit;

            int springpasses = 0;

            if (Math.Abs(diameter - xtarget) == 0.0d) // nothing to do...
                return;

            if (config.xmode == LatheMode.Radius)
            {
                xtarget /= 2.0d;
                diameter /= 2.0d;
            }
            else
            {
                passdepth *= 2.0d;
                passdepth_last *= 2.0d;
                xclearance *= 2.0d;
            }

            double angle = 0.0d, doc = 0.0d;
            double xstart = xtarget;
            double xclear = diameter + xclearance;
            double xdistance = diameter - xtarget;
            double xdir = xdistance > 0.0d ? -1.0d : 1.0d;
            int passes = 1;

            xdistance = Math.Round(Math.Abs(xdistance), precision);

            if (xdistance < passdepth_last)
                passdepth_last = xdistance;

            else if (xdistance < passdepth)
            {
                if (passdepth_last > 0.0d)
                    passes++;
                passdepth = xdistance - passdepth_last;
            }
            else
            {
                xdistance -= passdepth_last;
                passes = (int)Math.Floor(xdistance / passdepth);

                if (passdepth * passes < xdistance)
                {
                    passes++;
                    passdepth = Math.Round(xdistance / (double)passes, precision);
                }
                passes++; // Add last pass
            }

            if (chkTaper.Checked)
                angle = Math.Tan(Math.PI * UIGetDouble(txtTaper) / 180.0d);

            error.Clear();

            if (passes < 1)
            {
                error.SetError(cvDiameter.Field, "Starting diameter must be larger than target.");
                return;
            }

            if (chkSpringPasses.Checked)
                springpasses = (int)txtSpringPasses.Value;

            uint i = 5;
            double feedrate = cvFeedRate.Value;
            bool springpdone = false;
            double delta = 0.0d;
            double doc_prev = xclearance - 0.05d;

            String[] code = new String[5 + (passes + springpasses) * (css ? 6 : 5)];

            code[0] = String.Format("G18 G{0} G{1}", config.xmode == LatheMode.Radius ? "8" : "7", fMetric ? "21" : "20");
            code[1] = String.Format("M3S{0} G4P1", speed.ToString());
            code[2] = String.Format("G0 X{0}", FormatValue(xclear));
            code[3] = String.Format("G0 Z{0}", FormatValue(zstart + config.zclear / unit));
            code[4] = css ? String.Format(config.css_max_rpm > 0.0d ? "G96S{0}D{1}" : "G96S{0}",
                                           UIGetDouble(txtSpindleRPM), config.css_max_rpm) : "G97";

            while (passes-- > 0)
            {
                if (!springpdone) switch (passes)
                {
                    case 0: // last pass
                        doc = xdistance + passdepth_last;
                        feedrate = cvFeedRateLast.Value;
                        passes = springpasses;
                        springpdone = true;
                        break;

                    case 1: // second last pass
                        doc = xdistance;
                        break;

                    default:
                        doc = Math.Min(doc + passdepth, xdistance);
                        break;
                }

                code[i++] = string.Format("(Pass: {0}, DOC: {1} {2})", i / 5, doc, doc - delta);
                delta = doc;


               // diameter = Math.Max(diameter - passdepth, xtarget);
               // TODO: G0 to prev target to keep spindle speed constant?
                if (css)
                    code[i++] = String.Format("G0 X{0}", FormatValue(diameter - doc_prev));
                code[i++] = String.Format("G1 X{0} F{1}", FormatValue(diameter - doc), FormatValue(feedrate));
                if (angle != 0.0d)
                {
                    ztarget = doc / angle * config.zdir;
                    code[i++] = String.Format("G1 X{0} Z{1}", FormatValue(diameter), FormatValue(zstart + ztarget));
                }
                else
                    code[i++] = String.Format("G1 Z{0} F{1}", FormatValue(ztarget), FormatValue(feedrate));
                code[i++] = String.Format("G0 X{0}", FormatValue(xclear));
                code[i++] = String.Format("G0 Z{0}", FormatValue(zstart + config.zclear / unit));
                doc_prev = doc;
            }

            txtGCode.Lines = code;

            if (GCodePush != null)
            {
                GCodePush("Wizard: Turning", GCode.Action.New);

                /*
                GCodePush(String.Format("Wizard: {0}{1}", cbxThreadType.Text.Trim(), cbxThreadSize.Text == "" ? "" : ", " + cbxThreadSize.Text.Trim()), GCode.Action.New);
                GCodePush(String.Format("({0})", Uncomment(cbxThreadType.Text)), GCode.Action.Add);
                if (cbxThreadSize.Text != "")
                    GCodePush(String.Format("({0})", Uncomment(cbxThreadSize.Text)), GCode.Action.Add);
                 * */
                foreach (String s in code)
                    GCodePush(s, GCode.Action.Add);
                GCodePush("M30", GCode.Action.End);
            }
        }

        #endregion
    }

}
