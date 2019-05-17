/*
 * UIUtils.cs - part of CNC Controls library
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CNC_Controls
{
    class UIUtils
    {
        public static void ProcessMask(Control control, KeyPressEventArgs e, int precision)
        {
            int dp = control.Text.IndexOf("."),  cp = control is TextBox ? ((TextBox)control).SelectionStart : ((ComboBox)control).SelectionStart;
            bool has_dp = control is TextBox ? ((TextBox)control).SelectedText.Contains('.') : ((ComboBox)control).SelectedText.Contains('.');
            bool allow_sign = precision < 0;
            if (allow_sign)
                precision = -precision;

            e.Handled = (dp >= 0 && dp < cp && control.Text.Length - dp > precision && char.IsDigit(e.KeyChar) && !has_dp) ||
                         !(char.IsDigit(e.KeyChar) ||
                            char.IsControl(e.KeyChar) ||
                             (dp >= 0 && control.Text.Length - dp > precision && char.IsDigit(e.KeyChar) && !has_dp) ||
                              (precision > 0 && e.KeyChar == '.' && (dp == -1 || has_dp)) ||
                               (allow_sign && cp == 0 && e.KeyChar == '-' && !control.Text.StartsWith("-")));

        }

        private static void wField_KeyPress(object sender, KeyPressEventArgs e)
        {
            int precision = ((String)((Control)sender).Tag).Contains('.') ? ((String)((Control)sender).Tag).Substring(((String)((Control)sender).Tag).LastIndexOf('.')).Length - 1 : 0;

            UIUtils.ProcessMask((Control)sender, e, precision);
        }

        public static void SetMask(Control control, String format)
        {
            control.Tag = format;
            if (control is TextBox)
                ((TextBox)control).MaxLength = format.Length;
            else if (control is ComboBox)
                ((ComboBox)control).MaxLength = format.Length;
            control.KeyPress += new KeyPressEventHandler(UIUtils.wField_KeyPress);
        }

        public static void GroupBoxCaptionBold(GroupBox groupBox)
        {
            foreach (Control c in groupBox.Controls)
                c.Font = groupBox.Parent.Font;

            groupBox.Font = new Font(groupBox.Font.Name, groupBox.Font.SizeInPoints, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }
}
