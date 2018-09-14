/*
 * ProfileControl.cs - part of CNC Controls library for Grbl
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
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace CNC_Controls
{

    public partial class ProfileControl : UserControl
    {

        private DataView viewProfile = null;

        public delegate void ProfileChangedHandler(DataRow profile);
        public delegate void ProfileUpdateHandler(string Action, DataRow profile);

        public event ProfileChangedHandler ProfileChanged = null;
        public event ProfileUpdateHandler ProfileUpdated = null;

        private string ModeName = null;

        public ProfileControl()
        {
            InitializeComponent();
            this.btnProfile.Click += new EventHandler(btnProfile_Click);
            this.mnuProfileContext.ItemClicked += new ToolStripItemClickedEventHandler(mnuProfileContext_ItemClicked);

            this.viewProfile = new DataView(Profile.data);

            this.cmbProfile.DataSource = this.viewProfile;
            this.cmbProfile.DisplayMember = "Name";
            this.cmbProfile.ValueMember = "ProfileId";
            this.cmbProfile.SelectedValueChanged += new EventHandler(cmbProfile_SelectedValueChanged);

        }

        public override bool Focused { get { return this.cmbProfile.Focused; } }

        public void LoadProfiles(CNC_App.UIMode Mode)
        {

            this.ModeName = Mode.ToString();
            this.viewProfile.RowFilter = "Mode='" + this.ModeName + "'";
            this.cmbProfile.Refresh();
            if(this.cmbProfile.Text != "")
                cmbProfile_SelectedValueChanged(null, EventArgs.Empty);
        }

        public void SetProfile(int id)
        {
            this.cmbProfile.SelectedValue = id;
        }

        void mnuProfileContext_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            DataRow row;

            switch (e.ClickedItem.ToString())
            {

                case "Add":
                    row = Profile.data.Rows.Add(new object[] { ++Profile.dataId, null, this.cmbProfile.Text, this.ModeName, 0, 0, 0, 0, 0, 0, 0, false, false });
                    row.BeginEdit();
                    if(ProfileUpdated != null)
                        ProfileUpdated("Add", row);
                    row.EndEdit();
                    row.AcceptChanges();
                    this.cmbProfile.SelectedValue = Profile.dataId;
                    break;

                case "Update":
                    row = Profile.data.Select("ProfileId=" + this.cmbProfile.SelectedValue.ToString())[0];
                    row.BeginEdit();
                    if (ProfileUpdated != null)
                        ProfileUpdated("Update", row);
                    row.EndEdit();
                    row.AcceptChanges();
                    break;

                case "Delete":
                    row = Profile.data.Select("ProfileId=" + this.cmbProfile.SelectedValue.ToString())[0];
                    row.Delete();
                    break;

            }

            Profile.Save();

        }

        void cmbProfile_SelectedValueChanged(object sender, EventArgs e)
        {
            if(ProfileChanged != null)
                ProfileChanged(Profile.data.Select("ProfileId=" + this.cmbProfile.SelectedValue.ToString())[0]);
        }

        void btnProfile_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);

            if (this.cmbProfile.SelectedValue == null)
            {
                this.mnuProfileContext.Items[0].Enabled = true;
                this.mnuProfileContext.Items[1].Enabled = false;
                this.mnuProfileContext.Items[2].Enabled = false;
            }
            else
            {
                this.mnuProfileContext.Items[0].Enabled = false;
                this.mnuProfileContext.Items[1].Enabled = true;
                this.mnuProfileContext.Items[2].Enabled = !((int)this.cmbProfile.SelectedValue == 1 || (int)this.cmbProfile.SelectedValue == 2);
            }

            this.mnuProfileContext.Show(ptLowerLeft);
        }
    }

    public static class Profile
    {

        public static int dataId = -1, GrblToolNumber = -1;
        public static DataTable data;
        private static string profileFile = null;

        static Profile()
        {

            Profile.data = new DataTable("Profile");

            Profile.data.Columns.Add("ProfileId", typeof(int));
            Profile.data.Columns.Add("ToolNumber", typeof(int));
            Profile.data.Columns.Add("Name", typeof(string));
            Profile.data.Columns.Add("Mode", typeof(string));
            Profile.data.Columns.Add("IsEngraving", typeof(bool));
            Profile.data.Columns.Add("Speed", typeof(int));
            Profile.data.Columns.Add("MinPower", typeof(int));
            Profile.data.Columns.Add("Power", typeof(int));
            Profile.data.Columns.Add("DPI", typeof(int));
            Profile.data.Columns.Add("DutyCycle", typeof(int));
            Profile.data.Columns.Add("PPI", typeof(int));
            Profile.data.Columns.Add("PulseWidth", typeof(int));
            Profile.data.Columns.Add("AirAssist", typeof(bool));
            Profile.data.Columns.Add("Exhaust", typeof(bool));
            Profile.data.PrimaryKey = new DataColumn[] { Profile.data.Columns["ProfileId"] };
        }

        public static void Load (string profileFile)
        {

            bool updated = false;

            FileInfo file = new FileInfo(profileFile);
            if (file.Exists)
            {
                Profile.data.ReadXml(file.FullName);
            }

            Profile.profileFile = profileFile;

            if (Profile.data.Rows.Count == 0)
            {
                Profile.data.Columns.Remove("IsEngraving");
                Profile.data.Rows.Add(new object[] { ++Profile.dataId, null, "<default>", CNC_App.UIMode.Engraving.ToString(), 100, 10, 0, 600, 50, 600, 0, false, false });
                Profile.data.Rows.Add(new object[] { ++Profile.dataId, null, "<default>", CNC_App.UIMode.Mach3.ToString(), 100, 10, 0, 600, 25, 600, 2500, false, false });
                Profile.data.Rows.Add(new object[] { ++Profile.dataId, ++Profile.GrblToolNumber, "<default>", CNC_App.UIMode.GRBL.ToString(), 100, 10, 0, 600, 25, 600, 2500, false, false });
            }
            else
            {
                Profile.dataId = (int)Profile.data.Rows[Profile.data.Rows.Count - 1]["ProfileId"];
                foreach (DataRow row in Profile.data.Rows)
                {
                    if ((string)row["Mode"] == "GRBL")
                    {
                        if (row.IsNull("ToolNumber"))
                        {
                            updated = true;
                            row["ToolNumber"] = ++Profile.GrblToolNumber;
                        }
                        else
                            Profile.GrblToolNumber = Math.Max(Profile.GrblToolNumber, (int)(row["ToolNumber"]));
                    }
                    if (row.IsNull("MinPower"))
                        row["MinPower"] = 0;
                    if (row.IsNull("Exhaust"))
                        row["Exhaust"] = true;
                    if (row.IsNull("Mode"))
                        row["Mode"] = bool.Parse(row["IsEngraving"].ToString()) ? CNC_App.UIMode.Engraving.ToString() : CNC_App.UIMode.Mach3.ToString();
                }
                Profile.data.Columns.Remove("IsEngraving");

                if (Profile.data.Select("Mode='" + CNC_App.UIMode.GRBL.ToString() + "'").Count() == 0)
                    Profile.data.Rows.Add(new object[] { Profile.dataId++, 0, "<default>", CNC_App.UIMode.GRBL.ToString(), 100, 0, 10, 600, 25, 600, 2500, false, false });

                if (updated)
                    Profile.Save();
            
            }
        }

        public static void Save ()
        {
            if(Profile.profileFile != null)
                Profile.data.WriteXml(Profile.profileFile);
        }

    }
    
}
