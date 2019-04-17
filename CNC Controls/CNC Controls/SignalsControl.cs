/*
 * SignalsControl.cs - part of CNC Controls library
 *
 * 2018-10-03 / Io Engineering (Terje Io)
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
    public partial class SignalsControl : UserControl
    {
        Color LEDOn = Color.Red, LEDOff = Color.LightGray;

        private const string baseSignals = "HSRDP";

        public SignalsControl()
        {
            InitializeComponent();
            this.Config("XYZ" + SignalsControl.baseSignals);
            this.Set("");
        }

        public void setLatheMode()
        {
            this.Config("XZ" + SignalsControl.baseSignals);
            this.Set("");
        }

        public void setNumAxes(int numAxes)
        {
            if (numAxes <= 3 || numAxes > 6)
                return;

            string axes = "XYZ";

            for (int axis = 3; axis < numAxes; axis++)
                axes += GrblInfo.AxisLetters.Substring(axis, 1);

            this.Config(axes + SignalsControl.baseSignals);
            this.Set("");
        }

        public void Config(string signals)
        {
            foreach (Control c in this.groupBox.Controls)
                if(c.Tag != null)
                    c.Visible = signals.Contains(((string)c.Tag)[0]);
        }

        public void Set (string signals)
        {
            foreach (Control c in this.groupBox.Controls)
                if (c.GetType() == typeof(Button) && c.Visible)
                    c.BackColor = signals.Contains(((string)c.Tag)[0]) ? LEDOn : LEDOff;
        }
    }
}
