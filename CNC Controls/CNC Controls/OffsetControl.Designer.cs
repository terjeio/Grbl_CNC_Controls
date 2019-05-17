namespace CNC_Controls
{
    partial class OffsetControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgrOffsets = new System.Windows.Forms.DataGridView();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtTool = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCurrPos = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cvZOffset = new CNC_Controls.CoordValueControl();
            this.cvYOffset = new CNC_Controls.CoordValueControl();
            this.cvXOffset = new CNC_Controls.CoordValueControl();
            ((System.ComponentModel.ISupportInitialize)(this.dgrOffsets)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrOffsets
            // 
            this.dgrOffsets.AllowUserToResizeColumns = false;
            this.dgrOffsets.AllowUserToResizeRows = false;
            this.dgrOffsets.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrOffsets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgrOffsets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrOffsets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.code,
            this.x,
            this.y,
            this.z});
            this.dgrOffsets.Location = new System.Drawing.Point(31, 31);
            this.dgrOffsets.MultiSelect = false;
            this.dgrOffsets.Name = "dgrOffsets";
            this.dgrOffsets.ReadOnly = true;
            this.dgrOffsets.RowHeadersVisible = false;
            this.dgrOffsets.RowHeadersWidth = 20;
            this.dgrOffsets.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgrOffsets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrOffsets.Size = new System.Drawing.Size(259, 309);
            this.dgrOffsets.TabIndex = 0;
            // 
            // code
            // 
            this.code.DataPropertyName = "code";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.code.DefaultCellStyle = dataGridViewCellStyle2;
            this.code.HeaderText = "Offset";
            this.code.MinimumWidth = 45;
            this.code.Name = "code";
            this.code.ReadOnly = true;
            this.code.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.code.Width = 45;
            // 
            // x
            // 
            this.x.DataPropertyName = "x";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.x.DefaultCellStyle = dataGridViewCellStyle3;
            this.x.HeaderText = "X offset";
            this.x.MinimumWidth = 70;
            this.x.Name = "x";
            this.x.ReadOnly = true;
            this.x.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.x.Width = 70;
            // 
            // y
            // 
            this.y.DataPropertyName = "y";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.y.DefaultCellStyle = dataGridViewCellStyle4;
            this.y.HeaderText = "Y offset";
            this.y.MinimumWidth = 70;
            this.y.Name = "y";
            this.y.ReadOnly = true;
            this.y.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.y.Width = 70;
            // 
            // z
            // 
            this.z.DataPropertyName = "z";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.z.DefaultCellStyle = dataGridViewCellStyle5;
            this.z.HeaderText = "Z offset";
            this.z.MinimumWidth = 70;
            this.z.Name = "z";
            this.z.ReadOnly = true;
            this.z.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.z.Width = 70;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(530, 159);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // txtTool
            // 
            this.txtTool.Location = new System.Drawing.Point(604, 43);
            this.txtTool.Name = "txtTool";
            this.txtTool.ReadOnly = true;
            this.txtTool.Size = new System.Drawing.Size(45, 20);
            this.txtTool.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(567, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Offset:";
            // 
            // btnCurrPos
            // 
            this.btnCurrPos.Location = new System.Drawing.Point(692, 159);
            this.btnCurrPos.Name = "btnCurrPos";
            this.btnCurrPos.Size = new System.Drawing.Size(114, 23);
            this.btnCurrPos.TabIndex = 8;
            this.btnCurrPos.Text = "Get current position";
            this.btnCurrPos.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(611, 159);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // cvZOffset
            // 
            this.cvZOffset.Label = "Z offset:";
            this.cvZOffset.Location = new System.Drawing.Point(535, 121);
            this.cvZOffset.Metric = true;
            this.cvZOffset.Name = "cvZOffset";
            this.cvZOffset.Size = new System.Drawing.Size(175, 20);
            this.cvZOffset.TabIndex = 3;
            this.cvZOffset.Unit = "mm";
            this.cvZOffset.Value = 0D;
            // 
            // cvYOffset
            // 
            this.cvYOffset.Label = "Y offset:";
            this.cvYOffset.Location = new System.Drawing.Point(535, 95);
            this.cvYOffset.Metric = true;
            this.cvYOffset.Name = "cvYOffset";
            this.cvYOffset.Size = new System.Drawing.Size(175, 20);
            this.cvYOffset.TabIndex = 2;
            this.cvYOffset.Unit = "mm";
            this.cvYOffset.Value = 0D;
            // 
            // cvXOffset
            // 
            this.cvXOffset.Label = "X offset:";
            this.cvXOffset.Location = new System.Drawing.Point(535, 69);
            this.cvXOffset.Metric = true;
            this.cvXOffset.Name = "cvXOffset";
            this.cvXOffset.Size = new System.Drawing.Size(175, 20);
            this.cvXOffset.TabIndex = 1;
            this.cvXOffset.Unit = "mm";
            this.cvXOffset.Value = 0D;
            // 
            // OffsetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCurrPos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTool);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.cvZOffset);
            this.Controls.Add(this.cvYOffset);
            this.Controls.Add(this.cvXOffset);
            this.Controls.Add(this.dgrOffsets);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "OffsetControl";
            this.Size = new System.Drawing.Size(872, 507);
            ((System.ComponentModel.ISupportInitialize)(this.dgrOffsets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgrOffsets;
        private CoordValueControl cvXOffset;
        private CoordValueControl cvYOffset;
        private CoordValueControl cvZOffset;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtTool;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCurrPos;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn x;
        private System.Windows.Forms.DataGridViewTextBoxColumn y;
        private System.Windows.Forms.DataGridViewTextBoxColumn z;
    }
}
