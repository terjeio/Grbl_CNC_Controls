/*
 * DROControl.cs - part of CNC Controls library
 *
 * v0.01 / 2018-05-13 / Io Engineering (Terje Io)
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

    public partial class DROControl : UserControl
    {
        private delegate void SetTextCallback(int axis, double value);

        private AxisData[] axes = new AxisData[6];
        private bool useMPos = false, hasFocus = false;

        public string DisplayFormat { get; private set; }

        public delegate void AxisPositionChangedHandler(string axis, double position);
        public event AxisPositionChangedHandler AxisPositionChanged;

        public delegate void DROEnabledChangedHandler(bool enabled);
        public event DROEnabledChangedHandler DROEnabledChanged;

        public DROControl()
        {
            InitializeComponent();

            this.DisplayFormat = "F3";

            this.btnZeroAll.Click += new EventHandler(btnZero_Click);

            for (int i = 0; i < axes.Length; i++)
            {
                axes[i] = new AxisData();

                switch (i)
                {
                    case GrblConstants.X_AXIS:
                        axes[GrblConstants.X_AXIS].DRO = this.droX;
                        break;
                    case GrblConstants.Y_AXIS:
                        axes[GrblConstants.Y_AXIS].DRO = this.droY;
                        break;
                    case GrblConstants.Z_AXIS:
                        axes[GrblConstants.Z_AXIS].DRO = this.droZ;
                        break;
                    case GrblConstants.A_AXIS:
                        axes[GrblConstants.A_AXIS].DRO = this.droA;
                        break;
                    case GrblConstants.B_AXIS:
                        axes[GrblConstants.B_AXIS].DRO = this.droB;
                        break;
                    case GrblConstants.C_AXIS:
                        axes[GrblConstants.C_AXIS].DRO = this.droC;
                        break;
                }

                axes[i].DRO.Label = GrblInfo.AxisLetters.Substring(i, 1);
                axes[i].visible = i < 3;
                axes[i].DRO.Readout.GotFocus += new EventHandler(txtPos_GotFocus);
                axes[i].DRO.Readout.LostFocus += new EventHandler(txtPos_LostFocus);
                axes[i].DRO.Readout.KeyPress += new KeyPressEventHandler(txtPos_KeyPress);
                axes[i].DRO.Zero.Click += new EventHandler(btnZero_Click);

                setPos(i, 0.0);
            }
        }

        public override bool Focused { get { return this.hasFocus; } }

        private int TagToAxisIndex(string tag)
        {
            return GrblInfo.AxisLetters.IndexOf(tag);
        }

        private void txtPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) ||
                           char.IsControl(e.KeyChar) ||
                            (e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) ||
                             (e.KeyChar == '-' && !((TextBox)sender).Text.Contains("-")));

            if (e.KeyChar == '\r')
            {
                if (AxisPositionChanged != null)
                {
                    double position;
                    TextBox txt = (TextBox)sender;
                    if (double.TryParse(txt.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out position))
                    {
                        if (position != axes[TagToAxisIndex((string)txt.Tag)].position)
                            AxisPositionChanged((string)txt.Tag, position);
                        txt.ReadOnly = true;
                    }
                }
                if (DROEnabledChanged != null)
                    DROEnabledChanged(false);

            }
        }

        void txtPos_LostFocus(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int axis = TagToAxisIndex((string)txt.Tag);

            txt.ReadOnly = true;

            axes[axis].updateDRO = true;
            setPos(axis, axes[axis].position);
            this.hasFocus = false;

            if (DROEnabledChanged != null)
                DROEnabledChanged(false);
        }

        void txtPos_GotFocus(object sender, EventArgs e)
        {
            ((TextBox)(sender)).ReadOnly = false;
            this.hasFocus = true;

            if (DROEnabledChanged != null)
                DROEnabledChanged(true);
        }

        void btnZero_Click(object sender, EventArgs e)
        {
            if (AxisPositionChanged != null)
                AxisPositionChanged((string)((Button)sender).Tag, 0.0);
        }

        public void setLatheMode()
        {
            if (axes[GrblConstants.Y_AXIS].visible)
            {
                int yOffset = axes[GrblConstants.Y_AXIS].DRO.Location.Y - axes[GrblConstants.X_AXIS].DRO.Location.Y;
                axes[GrblConstants.Y_AXIS].visible = false;
                axes[GrblConstants.Y_AXIS].DRO.Hide();
                axes[GrblConstants.Z_AXIS].DRO.Location = axes[GrblConstants.Y_AXIS].DRO.Location;
                btnZeroAll.Location = new Point(btnZeroAll.Location.X, btnZeroAll.Location.Y - yOffset);
                grpDRO.Height -= yOffset;
                this.Height -= yOffset;
            }
        }

        public void setXMode(LatheMode xmode)
        {
            this.lblXMode.Visible = true;
            this.lblXMode.Text = xmode == LatheMode.Radius ? "Radius" : "Diameter";
        }

        public void setNumAxes(int numAxes)
        {
            if (numAxes <= 3 || numAxes > 6)
                return;

            int yOffset = 0, yDist = axes[GrblConstants.Y_AXIS].DRO.Location.Y - axes[GrblConstants.X_AXIS].DRO.Location.Y;

            for (int axis = 3; axis < numAxes; axis++)
            {
                if (!axes[axis].visible)
                {
                    axes[axis].visible = true;
                    axes[axis].DRO.Location = new Point(axes[axis].DRO.Location.X, axes[axis].DRO.Location.Y + yOffset);
                    axes[axis].DRO.Show();
                    yOffset += yDist;
                }
            }
            this.Height += yOffset;
            grpDRO.Height += yOffset;
            btnZeroAll.Location = new Point(btnZeroAll.Location.X, btnZeroAll.Location.Y + yOffset);
        }

        public void setPos(int axis, double value)
        {
            if (axes[axis].visible && (axes[axis].updateDRO || value != axes[axis].position && axes[axis].DRO.Readout.ReadOnly))
            {
                axes[axis].position = value;
                if (axes[axis].DRO.Readout.InvokeRequired)
                    this.Invoke(new SetTextCallback(setPos), new object[] { axis, value });
                else
                {
                    axes[axis].updateDRO = false;
                    axes[axis].DRO.Value = (useMPos ? axes[axis].position - axes[axis].offset : axes[axis].position).ToString(this.DisplayFormat, CultureInfo.InvariantCulture);
                }
            }
        }

        public void setOffset(int axis, double offset)
        {
            if (axes[axis].visible)
            {
                axes[axis].offset = offset;
                axes[axis].updateDRO = true;

                setPos(axis, axes[axis].position);
            }
        }

        public void Update(string position, bool IsMPos)
        {
            double pos;
            useMPos = IsMPos;
            string[] s = position.Split(',');
            for (int i = 0; i < s.Length; i++)
            {
                if (double.TryParse(s[i], NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out pos))
                    setPos(i, pos);

            }
        }

        public void UpdateOffsets(string offset)
        {
            double pos;
            string[] s = offset.Split(',');
            for (int i = 0; i < s.Length; i++)
            {
                if (double.TryParse(s[i], NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out pos))
                    setOffset(i, pos);
            }
        }

        private void setScaled(int scaled)
        {
            for (int i = 0; i < axes.Length; i++)
            {
                axes[i].DRO.Scaled = axes[i].scaled = (scaled & 0x01) == 1;
                scaled >>= 1;
            }
        }

        private void setScaled(string scaled)
        {
            foreach(AxisData axis in axes)
                axis.scaled = false;

            for (int i = 0; i < scaled.Length; i++)
                axes[TagToAxisIndex(scaled.Substring(i, 1))].scaled = true;

            foreach(AxisData axis in axes)
                axis.DRO.Scaled = axis.scaled;
        }

        public void UpdateScaled(string scaled)
        {
            setScaled(scaled);
            /*
            string[] s = scaled.Split(',');
            if (s.Length == 1)
                setScaled(int.Parse(s[0], CultureInfo.InvariantCulture));
            else for (int i = 0; i < axes.Length; i++)
                setOffset(i, double.Parse(s[i], CultureInfo.InvariantCulture));
             * */
        }
    }

    public class AxisData
    {
        public double position = 0.0;
        public double offset = 0.0;
        public bool visible = false;
        public bool scaled = false;
        public bool updateDRO = false;
        public DROBase DRO;
    }
}
