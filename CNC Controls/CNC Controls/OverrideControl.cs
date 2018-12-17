/*
 * OverrideControl.cs - part of CNC Controls library
 *
 * 2018-09-23 / Io Engineering (Terje Io)
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
    public partial class OverrideControl : UserControl
    {
        public delegate void CommandGeneratedHandler(string command);
        public event CommandGeneratedHandler CommandGenerated;

        public OverrideControl()
        {
            InitializeComponent();

            this.btnOvReset.Click += new EventHandler(btnOverrideClick);
            this.btnOvFineMinus.Click += new EventHandler(btnOverrideClick);
            this.btnOvFinePlus.Click += new EventHandler(btnOverrideClick);
            this.btnOvCoarseMinus.Click += new EventHandler(btnOverrideClick);
            this.btnOvCoarsePlus.Click += new EventHandler(btnOverrideClick);
        }

        public byte ResetCommand { set { this.btnOvReset.Tag = ((char)value).ToString(); } }
        public byte FinePlusCommand { set { this.btnOvFinePlus.Tag = ((char)value).ToString(); } }
        public byte FineMinusCommand { set { this.btnOvFineMinus.Tag = ((char)value).ToString(); } }
        public byte CoarsePlusCommand { set { this.btnOvCoarsePlus.Tag = ((char)value).ToString(); } }
        public byte CoarseMinusCommand { set { this.btnOvCoarseMinus.Tag = ((char)value).ToString(); } }

        private int cl (Control c)
        {
            return c.Location.Y + c.Height / 2;
        }

        public bool MinusOnly
        {
            set
            {
                if (this.btnOvFinePlus.Visible & value == true)
                {
                    this.btnOvFinePlus.Hide();
                    this.btnOvCoarsePlus.Hide();
                    this.btnOvFineMinus.Location = new Point(this.btnOvFineMinus.Location.X, this.btnOvFineMinus.Location.Y - (cl(this.btnOvFineMinus) - cl(this.txtOverride)));
                    this.btnOvCoarseMinus.Location = new Point(this.btnOvCoarseMinus.Location.X, this.btnOvCoarseMinus.Location.Y - (cl(this.btnOvCoarseMinus) - cl(this.txtOverride)));

                }

            }
        }


        public int Value { set { this.txtOverride.Text = value.ToString() + "%"; } }

        void btnOverrideClick(object sender, EventArgs e)
        {
            if (CommandGenerated != null)
                CommandGenerated((string)((Button)sender).Tag);
        }
    }
}
