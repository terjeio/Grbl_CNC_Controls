/*
 * OffsetControl.cs - part of CNC Controls library
 *
 * v0.01 / 2019-05-15 / Io Engineering (Terje Io)
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

namespace CNC_Controls
{
    public partial class OffsetControl : UserControl, CNC_App.IRenderer
    {

        CoordinateSystem selectedOffset = null;
        private GrblStatusParameters parameters = new GrblStatusParameters();
        private volatile bool awaitCoord = false;

        public OffsetControl()
        {
            InitializeComponent();

            this.Load += new EventHandler(OffsetControl_Load);

            dgrOffsets.AutoGenerateColumns = false;
        }

        #region Methods and properties required by IRenderer interface

        public CNC_App.UIMode mode { get { return CNC_App.UIMode.Offsets; } }

        public void Activate(bool activate, CNC_App.UIMode chgMode)
        {
            if (activate)
            {
                Comms.com.DataReceived += DataReceived;
                //  Comms.com.WriteByte(GrblLegacy.ConvertRTCommand(GrblConstants.CMD_STATUS_REPORT));
                //                dgrTools.DataSource = GrblWorkParameters.tool.Select(Tool => new {Tool.code, Tool.x}).ToList();

                dgrOffsets.DataSource = GrblWorkParameters.offset;
            }
            else
                Comms.com.DataReceived -= DataReceived;
        }

        public void CloseFile()
        {
        }

        #endregion

        #region UIEvents

        void OffsetControl_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            cvXOffset.Format = "-" + cvXOffset.Format;
            cvYOffset.Format = "-" + cvYOffset.Format;
            cvZOffset.Format = "-" + cvZOffset.Format;

            btnClear.Click += new EventHandler(btnClear_Click);
            btnUpdate.Click += new EventHandler(btnUpdate_Click);
            btnCurrPos.Click += new EventHandler(btnCurrPos_Click);

            dgrOffsets.Height = dgrOffsets.ColumnHeadersHeight + 2 + 22 * GrblWorkParameters.offset.Count();
            dgrOffsets.SelectionChanged += new EventHandler(dgrTools_SelectionChanged); 
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            cvXOffset.Value = 0.0d;
            cvYOffset.Value = 0.0d;
            cvZOffset.Value = 0.0d;
        }

        void btnCurrPos_Click(object sender, EventArgs e)
        {
            if(parameters.WPos == "") // If not blank then MPG is polling
                Comms.com.WriteByte(GrblLegacy.ConvertRTCommand(GrblConstants.CMD_STATUS_REPORT_ALL));
            
            awaitCoord = true;

            while (awaitCoord)
                Application.DoEvents(); // TODO: add timeout?

            //System.Threading.Thread.Sleep(500);

            double[] position;

            if (parameters.MPos == "")
            {
                position = Parse.Decimals(parameters.WPos);
                double[] offset = Parse.Decimals(parameters.WCO);
                for (int i = 0; i < offset.Length; i++)
                    position[i] += offset[i];
            }
            else
                position = Parse.Decimals(parameters.MPos);

            if (position.Length >= 3)
            {
                cvXOffset.Value = position[0];
                cvYOffset.Value = position[1];
                cvZOffset.Value = position[2];
            }

            parameters.Clear();
        }

        // G10 L1 P- axes <R- I- J- Q-> Set Tool Table
        // L10 - ref G5x + G92 - useful for probe (G38)
        // L11 - ref g59.3 only
        // Q: 1 - 8: 1: 135, 2: 45, 3: 315, 4: 225, 5: 180, 6: 90, 7: 0, 8: 270

        void btnUpdate_Click(object sender, EventArgs e)
        {
            string s;

            if (selectedOffset != null)
            {
                selectedOffset.x = cvXOffset.Value;
                selectedOffset.y = cvYOffset.Value;
                selectedOffset.z = cvZOffset.Value;
                dgrOffsets.Refresh();

                if (selectedOffset.id == 0)
                {
                    string code = selectedOffset.code == "G28" || selectedOffset.code == "G30" ? selectedOffset.code + ".1" : selectedOffset.code;
                    s = string.Format("G90{0}X{1}Y{2}Z{3}", code, cvXOffset.Text, cvYOffset.Text, cvZOffset.Text);
                }
                else
                    s = string.Format("G90G10L2P{0}X{1}Y{2}Z{3}", selectedOffset.id, cvXOffset.Text, cvYOffset.Text, cvZOffset.Text);

                Comms.com.WriteCommand(s);
            }
        }

        void dgrTools_SelectionChanged(object sender, EventArgs e)
        {
            var rows = dgrOffsets.SelectedRows;

            if (rows.Count == 1)
            {
                selectedOffset = (CoordinateSystem)(rows[0].DataBoundItem);

                txtTool.Text = selectedOffset.code;
                cvXOffset.Value = selectedOffset.x;
                cvYOffset.Value = selectedOffset.y;
                cvZOffset.Value = selectedOffset.z;
            }
        }

        #endregion

        private void DataReceived(string data)
        {
            if (data.Length > 1 && data.Substring(0, 1) == "<")
            {
                bool parseState = true;
                data = data.Remove(data.Length - 1);

                string[] elements = data.Split('|');

                foreach (string e in elements)
                {
                    if (parseState)
                        parseState = false;
                    else
                    {
                        string[] pair = e.Split(':');
                        parameters.Set(pair[0], pair[1]);
                    }
                }
                if(parameters.WCO != "")
                    awaitCoord = false;
            }
        }
    }
}
