/*
 * CoordValueControl.cs - part of CNC Controls library
 *
 * v0.01 / 2019-05-14 / Io Engineering (Terje Io)
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
    public partial class CoordValueControl : UserControl
    {
        protected string format;
        protected bool metric = true, allow_dp = true, allow_sign = false;
        protected int precision = 3;
        protected NumberStyles styles = NumberStyles.AllowDecimalPoint;

        public CoordValueControl()
        {
            InitializeComponent();

            unit.Text = "mm";

            data.KeyPress += new KeyPressEventHandler(wTextBox_KeyPress);
        }

        public int Precision { get { return Math.Abs(precision); } }

        public bool Metric
        {
            get
            {
                return metric;
            }
            set
            {
                if(metric != value)
                    unit.Text = unit.Text.Replace(metric ? "mm" : "in", metric ? "in" : "mm");
                metric = value;
                Format = metric ? GrblConstants.FORMAT_METRIC : GrblConstants.FORMAT_IMPERIAL;
            }
        }

        public string Label {
            get { return label.Text; }
            set {
                int dx = TextRenderer.MeasureText(value, label.Font).Width -
                          TextRenderer.MeasureText(label.Text, label.Font).Width;
                label.Text = value;
                label.Width += dx;
                label.Location = new Point(label.Location.X - dx, label.Location.Y);
            }
        }

        public string Unit {
            get { return unit.Text; }
            set {
                int dx = TextRenderer.MeasureText(value, unit.Font).Width -
                         TextRenderer.MeasureText(unit.Text, unit.Font).Width;
                unit.Text = value;
                unit.Width += dx;
           //     unit.Location = new Point(unit.Location.X - dx, unit.Location.Y);
            }
        }
        public override string Text {
            get {
                return Value.ToString(format, CultureInfo.InvariantCulture);
            }
            set {
                double v = 0.0d;
                double.TryParse(value, styles, CultureInfo.InvariantCulture, out v);
                data.Text = v.ToString(format, CultureInfo.InvariantCulture);
            }
        }
        public Control Field { get { return data; } }

        public double Value // always metric
        {
            get
            {
                double value = 0.0d;
                double.TryParse(data.Text, styles, CultureInfo.InvariantCulture, out value);

                return Math.Round(value * (metric ? 1.0d : 25.4d), 3);
            }
            set
            {
                data.Text = Math.Round(value / (metric ? 1.0d : 25.4d), metric ? 3 : 4).ToString(format, CultureInfo.InvariantCulture);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Format
        {
            get
            {
                return format;
            }
            set
            {
                allow_dp = value.Contains('.');
                allow_sign = value.StartsWith("-");
                styles = (allow_dp ? NumberStyles.AllowDecimalPoint : NumberStyles.None) |
                          (allow_sign ? NumberStyles.AllowLeadingSign : NumberStyles.None);
                format = allow_sign ? value.Substring(1) : value;
                precision = allow_dp ? format.Substring(format.LastIndexOf('.')).Length - 1 : 0;
                if (allow_sign)
                    precision = -precision;
                data.MaxLength = format.Length;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double ValueImperial
        {
            get
            {
                return Math.Round(Value / 25.4d, 4);
            }
            set
            {
                Value = value * 25.4d;
            }
        }

        private void wTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            UIUtils.ProcessMask((Control)sender, e, precision);
        }
    }
}
