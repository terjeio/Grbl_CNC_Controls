namespace CNC_Controls
{
    partial class ToolControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgrTools = new System.Windows.Forms.DataGridView();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.r = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtTool = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCurrPos = new System.Windows.Forms.Button();
            this.cvTipRadius = new CNC_Controls.CoordValueControl();
            this.cvZOffset = new CNC_Controls.CoordValueControl();
            this.cvYOffset = new CNC_Controls.CoordValueControl();
            this.cvXOffset = new CNC_Controls.CoordValueControl();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgrTools)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrTools
            // 
            this.dgrTools.AllowUserToResizeColumns = false;
            this.dgrTools.AllowUserToResizeRows = false;
            this.dgrTools.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrTools.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgrTools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrTools.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.code,
            this.x,
            this.y,
            this.z,
            this.r});
            this.dgrTools.Location = new System.Drawing.Point(31, 31);
            this.dgrTools.MultiSelect = false;
            this.dgrTools.Name = "dgrTools";
            this.dgrTools.ReadOnly = true;
            this.dgrTools.RowHeadersVisible = false;
            this.dgrTools.RowHeadersWidth = 20;
            this.dgrTools.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgrTools.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrTools.Size = new System.Drawing.Size(320, 207);
            this.dgrTools.TabIndex = 0;
            // 
            // code
            // 
            this.code.DataPropertyName = "code";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.code.DefaultCellStyle = dataGridViewCellStyle2;
            this.code.HeaderText = "Tool";
            this.code.MinimumWidth = 30;
            this.code.Name = "code";
            this.code.ReadOnly = true;
            this.code.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.code.Width = 30;
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
            // r
            // 
            this.r.DataPropertyName = "r";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.r.DefaultCellStyle = dataGridViewCellStyle6;
            this.r.HeaderText = "Tip radius";
            this.r.MinimumWidth = 76;
            this.r.Name = "r";
            this.r.ReadOnly = true;
            this.r.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.r.Width = 76;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(535, 183);
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
            this.txtTool.Size = new System.Drawing.Size(25, 20);
            this.txtTool.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(567, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Tool:";
            // 
            // btnCurrPos
            // 
            this.btnCurrPos.Location = new System.Drawing.Point(697, 183);
            this.btnCurrPos.Name = "btnCurrPos";
            this.btnCurrPos.Size = new System.Drawing.Size(114, 23);
            this.btnCurrPos.TabIndex = 8;
            this.btnCurrPos.Text = "Get current position";
            this.btnCurrPos.UseVisualStyleBackColor = true;
            // 
            // cvTipRadius
            // 
            this.cvTipRadius.Label = "Tip radius:";
            this.cvTipRadius.Location = new System.Drawing.Point(535, 147);
            this.cvTipRadius.Metric = true;
            this.cvTipRadius.Name = "cvTipRadius";
            this.cvTipRadius.Size = new System.Drawing.Size(175, 20);
            this.cvTipRadius.TabIndex = 4;
            this.cvTipRadius.Unit = "mm";
            this.cvTipRadius.Value = 0D;
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
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(616, 183);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // ToolControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCurrPos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTool);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.cvTipRadius);
            this.Controls.Add(this.cvZOffset);
            this.Controls.Add(this.cvYOffset);
            this.Controls.Add(this.cvXOffset);
            this.Controls.Add(this.dgrTools);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ToolControl";
            this.Size = new System.Drawing.Size(872, 507);
            ((System.ComponentModel.ISupportInitialize)(this.dgrTools)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgrTools;
        private CoordValueControl cvXOffset;
        private CoordValueControl cvYOffset;
        private CoordValueControl cvZOffset;
        private CoordValueControl cvTipRadius;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtTool;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn x;
        private System.Windows.Forms.DataGridViewTextBoxColumn y;
        private System.Windows.Forms.DataGridViewTextBoxColumn z;
        private System.Windows.Forms.DataGridViewTextBoxColumn r;
        private System.Windows.Forms.Button btnCurrPos;
        private System.Windows.Forms.Button btnClear;
    }
}
