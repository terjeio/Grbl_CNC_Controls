/*
 * UserUI.cs - configuration tool for Grbl
 *
 * v0.01 / 2019-04-29 / Io Engineering (Terje Io)
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
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Xml;
using System.Text;
using CNC_Controls;
using CNC_App;

using System.Windows.Forms;

namespace Grbl_Config_App
{
    public partial class UserUI : Form
    {
        private Comms com = null;

        public UserUI()
        {
            string PortParams = "";

            InitializeComponent();

            try
            {
                XmlDocument config = new XmlDocument();

                config.Load(Application.StartupPath + "\\App.config");

                foreach (XmlNode N in config.SelectNodes("Config/*"))
                {
                    switch (N.Name)
                    {
                        case "PortParams":
                            PortParams = N.InnerText;
                            break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Config file not found or invalid.", this.Text);
                System.Environment.Exit(1);
            }

#if DEBUG
            PortParams = "com29:115200,N,8,1,P";
#endif

            new SerialComms(PortParams, Comms.ResetMode.None);

            if (!Comms.com.IsOpen)
            {
                this.com = null;
                MessageBox.Show("Unable to open serial port!", this.Text);
                System.Environment.Exit(2);
            }

            GrblLanguage.language = "en_US";

            GrblInfo.Get();
            GrblSettings.Load();

            this.Text += ", Grbl version " + GrblInfo.Version;
        }
    }
}
