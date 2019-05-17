/*
 * LatheOptions.cs - part of CNC Controls library for Grbl
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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace CNC_Controls
{
    public partial class LatheOptions : Form
    {
        WizardConfig options;

        UserControl parent;
        BindingSource bSource = new BindingSource();

        private string last_rpm = "", last_css = "";

        public LatheOptions(UserControl parent, WizardConfig options, CNC_App.UIMode uiMode)
        {
            InitializeComponent();

            this.options = options;
            this.parent = parent;
            this.Text += " - " + options.Name;
            this.Load += new EventHandler(LatheOptions_Load);

            cvFirstCut.Value = options.passdepth_first;
            cvLastCut.Value = options.passdepth_last;
            cvMinCut.Value = options.passdepth_min;
            cvXClearance.Value = options.xclear;

            cvFeedRate.Visible = cvFeedRateLast.Visible = chkCSS.Visible = uiMode != CNC_App.UIMode.G76Threading;

            btnRadius.Checked = options.xmode == LatheMode.Radius;
            btnDiameter.Checked = options.xmode == LatheMode.Diameter;
            btnRadius.Enabled = btnDiameter.Enabled = !options.xmodelock;

            btnOk.Click += new EventHandler(btnOk_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            btnAddProfile.Click += new EventHandler(btnAddProfile_Click);
            chkCSS.CheckedChanged += new EventHandler(chkCSS_CheckedChanged);

            bSource.DataSource = options.Profiles;
            cbxProfile.ValueMember = "Id";
            cbxProfile.DisplayMember = "Name";
            cbxProfile.SelectedIndexChanged += new EventHandler(cbxProfile_SelectedIndexChanged);
            cbxProfile.DataSource = bSource;
            cbxProfile.SelectedIndex = options.Current.Id;
            cbxProfile.TextChanged += new EventHandler(cbxProfile_TextChanged);

            UIUtils.SetMask(txtSpindleRPM, "###0");
            UIUtils.SetMask(txtCSSMaxRPM, "###0");
            UIUtils.GroupBoxCaptionBold(groupBox1);
            UIUtils.GroupBoxCaptionBold(groupBox2);
            UIUtils.GroupBoxCaptionBold(groupBox3);
        }

        void chkCSS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCSS.Checked)
                last_rpm = txtSpindleRPM.Text;
            else
                last_css = txtSpindleRPM.Text;

            txtCSSMaxRPM.Visible = lblCSSMaxRPM.Visible = unitCSSMaxRPM.Visible = chkCSS.Checked;
            unitSpindleRPM.Text = chkCSS.Checked ? "m/min" : "RPM";
            lblSpindleRPM.Text = chkCSS.Checked ? "Speed:" : "Spindle:";
            txtSpindleRPM.Text = chkCSS.Checked ? last_css : last_rpm;
        }

        void cbxProfile_TextChanged(object sender, EventArgs e)
        {
            btnAddProfile.Enabled = cbxProfile.SelectedValue == null;
        }

        void btnAddProfile_Click(object sender, EventArgs e)
        {
            if (cbxProfile.SelectedValue == null)
            {
                ProfileData profile = options.Add(cbxProfile.Text);

                assignValues(profile);

                bSource.ResetBindings(false);
                cbxProfile.SelectedIndex = profile.Id;
            }
        }

        void cbxProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProfileData profile = options.Profiles[(int)cbxProfile.SelectedValue];

//            if(profile != options.Current) // TODO: Add?
//                assignValues(options.Current);

            last_css = profile.CSS && profile.RPM != 0.0d ? profile.RPM.ToString(CultureInfo.InvariantCulture) : "";
            last_rpm = profile.CSS || profile.RPM == 0.0d ? "" : profile.RPM.ToString(CultureInfo.InvariantCulture);

            cvFirstCut.Value = profile.PassDepthFirst;
            cvLastCut.Value = profile.PassDepthLast;
            cvMinCut.Value = profile.PassDepthMin;
            cvXClearance.Value = profile.XClearance;
            cvZClearance.Value = profile.ZClearance;
            cvFeedRate.Value = profile.Feedrate;
            cvFeedRateLast.Value = profile.FeedrateLast;
            chkCSS.Checked = profile.CSS;
            txtSpindleRPM.Text = chkCSS.Checked ? last_css : last_rpm;
            txtCSSMaxRPM.Text = profile.CSSMaxRPM == 0.0d || !profile.CSS  ? "" : profile.CSSMaxRPM.ToString(CultureInfo.InvariantCulture);

            chkCSS_CheckedChanged(null, null);
        }

        void LatheOptions_Load(object sender, EventArgs e)
        {
            Control parent = getRoot(this.parent);

            this.Left = parent.Left + (parent.Width - this.Width) / 2;
            this.Top = parent.Top + (parent.Height - this.Height) / 2;
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnOk_Click(object sender, EventArgs e)
        {
            LatheMode xmode = btnRadius.Checked ? LatheMode.Radius : LatheMode.Diameter;
            ProfileData profile = null;

            if(cbxProfile.SelectedValue != null) {
                profile = options.Profiles[(int)cbxProfile.SelectedValue];
                assignValues(options.Profiles[(int)cbxProfile.SelectedValue]);
            }

            options.Update(profile, cvXClearance.Value, xmode);

            this.Close();
        }

        private Control getRoot(Control p)
        {
            if (p.Parent != null)
                p = getRoot(p.Parent);

            return p;
        }

        private void assignValues(ProfileData profile)
        {
            profile.PassDepthFirst = cvFirstCut.Value;
            profile.PassDepthLast = cvLastCut.Value;
            profile.PassDepthMin = cvMinCut.Value;
            profile.XClearance = cvXClearance.Value;

            profile.ZClearance = cvZClearance.Value;
            profile.Feedrate = cvFeedRate.Value;
            profile.FeedrateLast = cvFeedRateLast.Value;
            double.TryParse(txtSpindleRPM.Text, out profile.RPM);
            if ((profile.CSS = chkCSS.Checked))
                double.TryParse(txtCSSMaxRPM.Text, out profile.CSSMaxRPM);
            else
                profile.CSSMaxRPM = 0.0d;
        }
    }
}
