/*
 * DROControl.cs - part of CNC Controls library
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
using System.Globalization;

namespace CNC_Controls
{

    public partial class DROControl : UserControl
    {
        const int XAXIS = 0, YAXIS = 1, ZAXIS = 2, AAXIS = 3, BAXIS = 4, CAXIS = 5;

        private delegate void SetTextCallback(int axis, double value);

        private AxisData[] axes = new AxisData[3];
        private bool useMPos = false;

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
                    case XAXIS:
                        axes[XAXIS].textBox = this.txtXPos;
                        axes[XAXIS].btnZero = this.btnXZero;
                        break;
                    case YAXIS:
                        axes[YAXIS].textBox = this.txtYPos;
                        axes[YAXIS].btnZero = this.btnYZero;
                        break;
                    case ZAXIS:
                        axes[ZAXIS].textBox = this.txtZPos;
                        axes[ZAXIS].btnZero = this.btnZZero;
                        break;
                }

                axes[i].textBox.GotFocus += new EventHandler(txtPos_GotFocus);
                axes[i].textBox.LostFocus += new EventHandler(txtPos_LostFocus);
                axes[i].textBox.KeyPress += new KeyPressEventHandler(txtPos_KeyPress);
                axes[i].btnZero.Click += new EventHandler(btnZero_Click);

                setPos(i, 0.0);
            }
        }

        private int TagToAxisIndex(string tag)
        {
            return "XYZABC".IndexOf(tag);
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

            if (DROEnabledChanged != null)
                DROEnabledChanged(false);
        }

        void txtPos_GotFocus(object sender, EventArgs e)
        {
            ((TextBox)(sender)).ReadOnly = false;
            if (DROEnabledChanged != null)
                DROEnabledChanged(true);
        }

        void btnZero_Click(object sender, EventArgs e)
        {
            if (AxisPositionChanged != null)
                AxisPositionChanged((string)((Button)sender).Tag, 0.0);
        }

        public void setPos(int axis, double value)
        {
            if (axes[axis].updateDRO || value != axes[axis].position && axes[axis].textBox.ReadOnly)
            {
                axes[axis].position = value;
                if (axes[axis].textBox.InvokeRequired)
                    this.Invoke(new SetTextCallback(setPos), new object[] { axis, value });
                else
                {
                    axes[axis].updateDRO = false;
                    axes[axis].textBox.Text = (useMPos ? axes[axis].position - axes[axis].offset : axes[axis].position).ToString(this.DisplayFormat, CultureInfo.InvariantCulture);
                }
            }
        }

        private void setOffset(int axis, double offset)
        {
            axes[axis].offset = offset;
            axes[axis].updateDRO = true;

            setPos(axis, axes[axis].position);
        }

        public double Xpos
        {
            get { return axes[XAXIS].position; }
            set { setPos(XAXIS, value); }
        }

        public double Xoffset
        {
            get { return axes[XAXIS].offset; }
            set { setOffset(XAXIS, value); }
        }

        public double Ypos
        {
            get { return axes[YAXIS].position; }
            set { setPos(YAXIS, value); }
        }

        public double Yoffset
        {
            get { return axes[YAXIS].offset; }
            set { setOffset(YAXIS, value); }
        }

        public double Zpos
        {
            get { return axes[ZAXIS].position; }
            set { setPos(ZAXIS, value); }
        }

        public double Zoffset
        {
            get { return axes[ZAXIS].offset; }
            set { setOffset(ZAXIS, value); }
        }

        public void Update(string position, bool IsMPos)
        {
            useMPos = IsMPos;
            string[] s = position.Split(',');
            for(int i = 0; i < axes.Length; i++)
                setPos(i, double.Parse(s[i], CultureInfo.InvariantCulture));
        }

        public void UpdateOffsets(string offset)
        {
            string[] s = offset.Split(',');
            for(int i = 0; i < axes.Length; i++)
                setOffset(i, double.Parse(s[i], CultureInfo.InvariantCulture));
        }
    }

    public class AxisData
    {
        public double position = 0.0;
        public double offset = 0.0;
        public bool updateDRO = false;
        public TextBox textBox;
        public Button btnZero;
    }
}
