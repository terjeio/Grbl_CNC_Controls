/*
 * SDCardControl.cs - part of CNC Controls library for Grbl
 *
 * v0.01 / 2018-10-08 / Io Engineering (Terje Io)
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
using System.IO;

namespace CNC_Controls
{
    public partial class SDCardControl : UserControl, CNC_App.IRenderer
    {
        BindingSource source = new BindingSource();

        public delegate void FileSelectedHandler(string filename);
        public event FileSelectedHandler FileSelected;

        public SDCardControl()
        {
            InitializeComponent();

            this.GrblFileView.AutoGenerateColumns = false;
            this.GrblFileView.SelectionChanged += new EventHandler(GrblFileView_SelectionChanged);
            this.GrblFileView.CellDoubleClick += new DataGridViewCellEventHandler(GrblFileView_CellDoubleClick);
        }

        void GrblFileView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow row = ((DataRowView)this.GrblFileView.Rows[e.RowIndex].DataBoundItem).Row;

            if ((bool)row["Invalid"])
            {
                MessageBox.Show(string.Format("File: \"{0}\"\r\r!,?,~ and SPACE is not supported in filenames, please rename.", (string)row["Name"]), "Unsupported characters in filename",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                JobTimer.Start();
                Comms.com.WriteCommand(GrblConstants.CMD_SDCARD_RUN + (string)row["Name"]);

                if (FileSelected != null)
                    FileSelected("SDCard:" + (string)row["Name"]);
            }
     //       GCode_Sender.UserUI.ui.
        }

        public CNC_App.UIMode mode { get { return CNC_App.UIMode.SDCard; } }

        void GrblFileView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.SelectedRows.Count == 1)
            {
                DataRow row = ((DataRowView)dgv.SelectedRows[0].DataBoundItem).Row;
//                this.txtDescription.Text = (string)row["Description"];
            }
        }

        public void Activate(bool activate, CNC_App.UIMode chgMode)
        {
            if (activate)
            {
                GrblSDCard.Load();
                source.DataSource = GrblSDCard.data;

                this.GrblFileView.DataSource = source;
                this.GrblFileView.Refresh();
            }
        }

        public void CloseFile()
        {
        }
    }

    public static class GrblSDCard
    {
        public static DataTable data;

        private static bool mounted = false;

        static GrblSDCard()
        {
            GrblSDCard.data = new DataTable("Filelist");

            GrblSDCard.data.Columns.Add("Dir", typeof(string));
            GrblSDCard.data.Columns.Add("Name", typeof(string));
            GrblSDCard.data.Columns.Add("Size", typeof(int));
            GrblSDCard.data.Columns.Add("Invalid", typeof(bool));
 //           GrblSDCard.data.PrimaryKey = new DataColumn[] { GrblSDCard.data.Columns["Id"] };
        }

        public static bool Loaded { get { return GrblSDCard.data.Rows.Count > 0; } }

        public static void Load()
        {
            GrblSDCard.data.Clear();

            Comms.com.PurgeQueue();

            if (!GrblSDCard.mounted)
            {
                Comms.com.WriteCommand(GrblConstants.CMD_SDCARD_MOUNT);

                while (Comms.com.CommandState == Comms.State.AwaitAck)
                    Application.DoEvents();

                GrblSDCard.mounted = Comms.com.CommandState == Comms.State.ACK;
            }

            if (GrblSDCard.mounted)
            {
                Comms.com.DataReceived += new DataReceivedHandler(GrblSDCard.Process);

                Comms.com.WriteCommand(GrblConstants.CMD_SDCARD_DIR);

                while (Comms.com.CommandState == Comms.State.DataReceived || Comms.com.CommandState == Comms.State.AwaitAck)
                    Application.DoEvents();

                Comms.com.DataReceived -= GrblSDCard.Process;

                GrblSDCard.data.AcceptChanges();
            }
        }

        private static void Process(string data)
        {
            string filename = "";
            int filesize = 0;
            bool invalid = false;

            if (data.StartsWith("[FILE:"))
            {
                string[] parameters = data.TrimEnd(']').Split('|');
                foreach (string parameter in parameters)
                {
                    string[] valuepair = parameter.Split(':');
                    switch (valuepair[0])
                    {
                        case "[FILE":
                            filename = valuepair[1];
                            break;

                        case "SIZE":
                            filesize = int.Parse(valuepair[1]);
                            break;

                        case "INVALID":
                            invalid = true;
                            break;
                    }
                }
                GrblSDCard.data.Rows.Add(new object[] { "", filename, filesize, invalid });
            }
        }
    }
}
