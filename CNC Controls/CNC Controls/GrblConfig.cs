/*
 * GrblConfig.cs - part of CNC Controls library for Grbl
 *
 * v0.01 / 2018-10-22 / Io Engineering (Terje Io)
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
using System.Windows.Forms;
using System.Globalization;

namespace CNC_Controls
{
    public partial class GrblConfig : UserControl, CNC_App.IRenderer
    {

        private Widget curSetting = null;
        public GrblConfig()
        {
            BindingSource source = new BindingSource();

            InitializeComponent();

            this.GrblSettingsView.AutoGenerateColumns = false;
            this.GrblSettingsView.SelectionChanged += new EventHandler(GrblSettingsView_SelectionChanged);
            this.btnReload.Click += new EventHandler(btnReload_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnBackup.Click += new EventHandler(btnBackup_Click);

            source.DataSource = GrblSettings.data;

            this.GrblSettingsView.DataSource = source;
            this.GrblSettingsView.Refresh();

        }

        public CNC_App.UIMode mode { get { return CNC_App.UIMode.GRBLConfig; } }

        #region UIEvents

        void btnSave_Click(object sender, EventArgs e)
        {
            if (curSetting != null)
                curSetting.Assign();
            GrblSettings.Save();
        }

        void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                GrblSettings.Load();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        void btnBackup_Click(object sender, EventArgs e)
        {
            GrblSettings.Backup(string.Format("{0}\\settings.txt", Application.StartupPath));
        }

        void GrblSettingsView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.SelectedRows.Count == 1) {
                DataRow row = ((DataRowView)dgv.SelectedRows[0].DataBoundItem).Row;
                this.txtDescription.Text = ((string)row["Description"]).Replace("\\n", "\r\n");
                if (curSetting != null)
                {
                    curSetting.Assign();
                    curSetting.Dispose();
                }
                curSetting = new Widget(row, this);
                curSetting.Enabled = true;
                this.txtDescription.Location = new System.Drawing.Point(this.txtDescription.Location.X, curSetting.yPos + 20);
            }
        }
        #endregion

        public void Activate(bool activate, CNC_App.UIMode chgMode)
        {
        }

        public void CloseFile()
        {
        }
    }

    public class FPFormat
    {
        public int l1;
        public int l2;
        public bool hasDP;

        public FPFormat (string value)
        {
            this.l1 = value.Length;
            if ((this.hasDP = value.Contains('.')))
            {
                this.l2 = this.l1 - value.LastIndexOf('.') - 1;
                this.l1 -= (this.l2 + 1);
            } else
                this.l2 = 0;
         }
    }

    public class Widget : IDisposable
    {
        private enum DataType {
            BOOL = 0,
            BITFIELD,
            RADIOBUTTONS,
            INTEGER,
            FLOAT,
            TEXT
        };

        private const int PPU = 8;
        private bool enabled = false, disposed = false, Modified = false, validated = false;
        private int PropertyId;
        private FPFormat fp = null;
        private string format, orgText;
        private Container components = new Container();
        private DataRow Property;
        private DataType dataType;
        private ErrorProvider errorProvider = null;

        public System.Windows.Forms.Label wLabel = null, wUnit = null;
        public System.Windows.Forms.TextBox wTextBox = null;
        public System.Windows.Forms.CheckBox wCheckBox = null;
        public System.Windows.Forms.RadioButton wRadiobutton = null;

        private List<System.Windows.Forms.CheckBox> checkboxes = null;
        private List<System.Windows.Forms.RadioButton> radiobuttons = null;
        private UserControl Canvas;

        public Widget(DataRow Property, UserControl Canvas)
        {
            this.format = (String)Property["DataFormat"];
            int x = 570, y = 40;
            this.yPos = y;

            this.Canvas = Canvas;
            this.PropertyId = (int)Property["Id"];
            this.Property = Property;

            try
            {
                this.dataType = (DataType)Enum.Parse(typeof(DataType), ((string)Property["DataType"]).ToUpperInvariant());
            }
            catch
            {
                this.dataType = DataType.TEXT;
            }

            switch (this.dataType)
            {
                case DataType.BOOL:
                    this.wCheckBox = new System.Windows.Forms.CheckBox();
                    this.wCheckBox.Location = new System.Drawing.Point(x, y - 2);
                    this.wCheckBox.Name = "_bool";
                    //             this.wCheckBox.TabIndex = Canvas.Row;
                    this.wCheckBox.Size = new System.Drawing.Size(Canvas.Width - x - 15, 17);
                    this.wCheckBox.Text = ((string)Property["Name"]).Trim();
                    this.wCheckBox.Enabled = false;
                    this.wCheckBox.CheckedChanged += new EventHandler(wWidget_TextChanged);
                    this.components.Add(this.wCheckBox);
                    Canvas.Controls.Add(this.wCheckBox);
                    this.yPos += 20;
                    break;

                case DataType.BITFIELD:
                    this.checkboxes = new List<CheckBox>();
                    bool axes = (string)Property["DataFormat"] == "axes";
                    string[] format = (axes ? "X-Axis,Y-Axis,Z-Axis,A-Axis,B-Axis,C-Axis" : (string)Property["DataFormat"]).Split(',');
                    for (int i = 0; i < (axes ? 3 : format.Length); i++)
                    {
                        this.wCheckBox = new System.Windows.Forms.CheckBox();
                        this.wCheckBox.Location = new System.Drawing.Point(x, this.yPos + 4);
                        //  this.wCheckBox.BackColor = Color.Azure;
                        this.wCheckBox.Name = string.Format("_bitmask{0}", i);
                        //  this.wCheckBox.TabIndex = Canvas.Row;
                        this.wCheckBox.Size = new System.Drawing.Size(Canvas.Width - x - 15, 17);
                        this.wCheckBox.Text = format[i].Trim();
                        this.wCheckBox.Enabled = false;
                        this.wCheckBox.CheckedChanged += new EventHandler(wWidget_TextChanged);
                        this.checkboxes.Add(this.wCheckBox);
                        this.components.Add(this.wCheckBox);
                        Canvas.Controls.Add(this.wCheckBox);
                        this.wCheckBox = null;
                        this.yPos += 20;
                    }
                    break;

                case DataType.RADIOBUTTONS:
                    this.radiobuttons = new List<RadioButton>();
                    string[] rformat = ((string)Property["DataFormat"]).Split(',');
                    for (int i = 0; i < rformat.Length; i++)
                    {
                        this.wRadiobutton = new System.Windows.Forms.RadioButton();
                        this.wRadiobutton.Location = new System.Drawing.Point(x, this.yPos + 4);
                        this.wRadiobutton.Name = string.Format("_radiobutton{0}", i);
                        this.wRadiobutton.Size = new System.Drawing.Size(Canvas.Width - x - 15, 17);
                        this.wRadiobutton.Text = rformat[i].Trim();
                        this.wRadiobutton.Enabled = false;
                        this.wRadiobutton.CheckedChanged += new EventHandler(wWidget_TextChanged);
                        this.radiobuttons.Add(this.wRadiobutton);
                        this.components.Add(this.wRadiobutton);
                        Canvas.Controls.Add(this.wRadiobutton);
                        this.wRadiobutton = null;
                        this.yPos += 20;
                    }
                    break;

                default:
                    this.format.Replace(" ", "");
                    this.wTextBox = new System.Windows.Forms.TextBox();
                    this.wTextBox.Location = new System.Drawing.Point(x, y);
                    this.wTextBox.Name = "tb_name_xxx";
                    //           this.wTextBox.TabIndex = Canvas.Row;
                    this.wTextBox.MaxLength = this.format.Length;
                    this.wTextBox.Enabled = this.Enabled;
                    // Buggy - does not work	this.wTextBox.ModifiedChanged += new EventHandler(wWidget_ModifiedChanged);
                    this.wTextBox.TextChanged += new EventHandler(wWidget_TextChanged);
                    this.components.Add(this.wTextBox);
                    Canvas.Controls.Add(this.wTextBox);
                    if (this.dataType == DataType.INTEGER || this.dataType == DataType.FLOAT)
                    {
                        this.fp = new FPFormat(this.format);
                        this.wTextBox.TextAlign = HorizontalAlignment.Right;
                        this.wTextBox.Size = TextRenderer.MeasureText("".PadRight(this.format.Length, '9'), this.wTextBox.Font);
                        this.wTextBox.KeyPress += new KeyPressEventHandler(wTextBox_KeyPress);
                        if ((double)Property["Min"] != double.NaN || (double)Property["Max"] != double.NaN)
                        {
                            this.validated = true;
                            this.errorProvider = new ErrorProvider();
                            this.wTextBox.Validating += new CancelEventHandler(wTextBox_Validating);
                        }
                    }
                    else if (this.dataType == DataType.TEXT && this.format.StartsWith("x("))
                    {
                        int length = 8;
                        int.TryParse(this.format.Substring(2).Replace(")", ""), out length);
                        this.wTextBox.MaxLength = length;
                        this.wTextBox.Size = new System.Drawing.Size(Math.Min(length * PPU, Canvas.Width - x - 15), 20);
                    }
                    else
                        this.wTextBox.Size = new System.Drawing.Size(this.format.Length * PPU, 20);

                    this.yPos += 20;
                    break;
            }

            if (this.dataType != DataType.BOOL)
            {
                if ((string)Property["Name"] != "")
                {
                    this.wLabel = new System.Windows.Forms.Label();
                    this.wLabel.Location = new System.Drawing.Point(10, y);
                    this.wLabel.Name = "label_xx";
                    this.wLabel.Size = new System.Drawing.Size(x - 12, 20);
                    this.wLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                    this.wLabel.Text = (string)Property["Name"] + ":";
                    this.wLabel.Visible = true;
                    this.components.Add(this.wLabel);
                    Canvas.Controls.Add(this.wLabel);
                }

                if (this.dataType != DataType.BITFIELD && this.dataType != DataType.RADIOBUTTONS && (string)Property["Unit"] != "")
                {
                    this.wUnit = new System.Windows.Forms.Label();
                    this.wUnit.Location = new System.Drawing.Point(x + this.format.Length * PPU + 3, y);
                    this.wUnit.Name = "unit_xxx";
                    this.wUnit.Size = new System.Drawing.Size(Canvas.Width - x - this.format.Length - 35, 20);
                    this.wUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    this.wUnit.Text = (string)Property["Unit"];
                    this.components.Add(this.wUnit);
                    Canvas.Controls.Add(this.wUnit);
                }
            }
            this.Text = (string)Property["Value"];
        }

        private bool isValid()
        {
            CancelEventArgs e = new CancelEventArgs();

            e.Cancel = false;

            if(this.validated)
                wTextBox_Validating(this.wTextBox, e);

            return !e.Cancel;
        }

        #region Attributes

        public int yPos { get; private set; }

        public bool Enabled
        {
            get
            {
                return (enabled);
            }
            set
            {
                this.enabled = value;

                if (this.checkboxes != null)
                    foreach (CheckBox bitfield in this.checkboxes)
                        bitfield.Enabled = true;
                else if (this.radiobuttons != null)
                    foreach (RadioButton radiobutton in this.radiobuttons)
                        radiobutton.Enabled = true;
                else if (this.wCheckBox != null)
                    this.wCheckBox.Enabled = this.enabled;
                else if (this.wTextBox != null)
                    this.wTextBox.Enabled = this.enabled;
            }
        }

        public string Text
        {
            get
            {
                string value = "";
                switch (this.dataType)
                {
                    case DataType.BITFIELD:
                        int val = 0;
                        for (int i = this.checkboxes.Count; i > 0; i--)
                        {
                            val <<= 1;
                            if (this.checkboxes[i - 1].Checked)
                                val |= 0x01;
                        }
                        value = val.ToString();
                        break;

                    case DataType.RADIOBUTTONS:
                        int rval = 0;
                        for (rval = this.radiobuttons.Count - 1; rval >= 0; rval--)
                        {
                            if (this.radiobuttons[rval].Checked)
                                break;
                        }
                        value = rval.ToString();
                        break;

                    case DataType.FLOAT:
                        value = GrblSettings.FormatFloat(this.wTextBox.Text, this.format);
                        break;

                    case DataType.BOOL:
                        value = this.wCheckBox.Checked ? "1" : "0";
                        break;

                    default:
                        value = this.wTextBox.Text;
                        break;
                }
                return value.Trim();
            }
            set
            {
                this.Modified = false;
                switch (this.dataType)
                {
                    case DataType.BITFIELD:
                        int val = int.Parse(value);
                        foreach (CheckBox bitfield in this.checkboxes)
                        {
                            bitfield.Checked = (val & 0x01) == 1;
                            val >>= 1;
                        }
                        break;

                    case DataType.RADIOBUTTONS:
                        int rval = int.Parse(value), i = 0;
                        foreach (RadioButton radiobutton in this.radiobuttons)
                        {
                            radiobutton.Checked = rval == i;
                            i++;
                        }
                        break;

                    case DataType.BOOL:
                        this.wCheckBox.Checked = value == "1";
                        break;

                    default:
                        this.orgText = value;
                        this.wTextBox.Text = value;
                        break;
                }
            }
        }
        #endregion

        #region UIEvents

        private void wTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) ||
                           char.IsControl(e.KeyChar) ||
                            (this.fp.hasDP && e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")));
//                             (e.KeyChar == '-' && !((TextBox)sender).Text.Contains("-")));

            if (e.KeyChar == '\r')
                this.Assign();
        }

        /* Hack for bug in API */
        public void wWidget_TextChanged(object sender, System.EventArgs e)
        {
            bool changed;
            /*
                        if (this.fp != null)
                        {
                            FPFormat k = new FPFormat(((TextBox)sender).Text);
                            if (k.l1 > this.fp.l1 ||k.l2 > this.fp.l2)
                            {
                                this.wTextBox.TextChanged -= wWidget_TextChanged;
                                this.wTextBox.Text = this.orgText;
                                this.wTextBox.TextChanged += wWidget_TextChanged;
                            }
                        }
            */
            changed = this.Modified ? (string)Property["Value"] == this.Text : (string)Property["Value"] != this.Text;
            if (changed)
                this.Modified = !this.Modified;
            this.orgText = this.Text;
        }
        /*  End Hack for bug in API */
        void wTextBox_Validating(object sender, CancelEventArgs e)
        {
            float fval;
            string message = "";
            if (float.TryParse(this.wTextBox.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out fval))
            {
                if ((e.Cancel = (double)Property["Min"] != double.NaN && (double)Property["Min"] > fval))
                    message = String.Format("Minimum value is {0}", (double)Property["Min"]);
                else if ((e.Cancel = (double)Property["Max"] != double.NaN && (double)Property["Max"] < fval))
                    message = String.Format("Maximum value is {0}", (double)Property["Max"]); ;
            }
            else
            {
                message = "Invalid input";
                e.Cancel = true; // Should never happen
            }
            this.errorProvider.SetError(this.wTextBox, message);
        }

        ~Widget()
        {
            this.Dispose(false);
        }

        #endregion

        public void Assign()
        {
            if (this.Modified && isValid())
            {
                this.Modified = false;
                Property.BeginEdit();
                Property["Value"] = this.Text;
                Property.EndEdit();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                this.components.Dispose();
              //  this.Canvas.Controls.Remove(this);
            }
            this.disposed = true;
        }
    }

}
