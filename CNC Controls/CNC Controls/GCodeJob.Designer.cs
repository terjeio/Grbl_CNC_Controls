namespace CNC_Controls
{
    partial class GCodeJob
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
            this.txtRunTime = new System.Windows.Forms.Label();
            this.btnRewind = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnHold = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.GCodeView = new System.Windows.Forms.DataGridView();
            this.LineNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GCodeView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRunTime
            // 
            this.txtRunTime.AutoSize = true;
            this.txtRunTime.Location = new System.Drawing.Point(346, 396);
            this.txtRunTime.Name = "txtRunTime";
            this.txtRunTime.Size = new System.Drawing.Size(49, 13);
            this.txtRunTime.TabIndex = 45;
            this.txtRunTime.Text = "00:00:00";
            // 
            // btnRewind
            // 
            this.btnRewind.Location = new System.Drawing.Point(234, 395);
            this.btnRewind.Name = "btnRewind";
            this.btnRewind.Size = new System.Drawing.Size(71, 32);
            this.btnRewind.TabIndex = 0;
            this.btnRewind.TabStop = false;
            this.btnRewind.Text = "Rewind";
            this.btnRewind.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnStop.Location = new System.Drawing.Point(157, 395);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(71, 32);
            this.btnStop.TabIndex = 0;
            this.btnStop.TabStop = false;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnHold
            // 
            this.btnHold.Location = new System.Drawing.Point(80, 395);
            this.btnHold.Name = "btnHold";
            this.btnHold.Size = new System.Drawing.Size(71, 32);
            this.btnHold.TabIndex = 0;
            this.btnHold.TabStop = false;
            this.btnHold.Text = "Hold";
            this.btnHold.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(3, 395);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(71, 32);
            this.btnStart.TabIndex = 0;
            this.btnStart.TabStop = false;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // GCodeView
            // 
            this.GCodeView.AllowDrop = true;
            this.GCodeView.AllowUserToAddRows = false;
            this.GCodeView.AllowUserToDeleteRows = false;
            this.GCodeView.AllowUserToResizeRows = false;
            this.GCodeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GCodeView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LineNum,
            this.Sent,
            this.Data});
            this.GCodeView.Location = new System.Drawing.Point(3, 3);
            this.GCodeView.MultiSelect = false;
            this.GCodeView.Name = "GCodeView";
            this.GCodeView.ReadOnly = true;
            this.GCodeView.RowHeadersVisible = false;
            this.GCodeView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GCodeView.Size = new System.Drawing.Size(392, 386);
            this.GCodeView.TabIndex = 40;
            // 
            // LineNum
            // 
            this.LineNum.DataPropertyName = "LineNum";
            this.LineNum.HeaderText = "Line";
            this.LineNum.Name = "LineNum";
            this.LineNum.ReadOnly = true;
            this.LineNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LineNum.Width = 46;
            // 
            // Sent
            // 
            this.Sent.DataPropertyName = "Sent";
            dataGridViewCellStyle1.NullValue = null;
            this.Sent.DefaultCellStyle = dataGridViewCellStyle1;
            this.Sent.HeaderText = "Sent";
            this.Sent.Name = "Sent";
            this.Sent.ReadOnly = true;
            this.Sent.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Sent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Sent.Width = 40;
            // 
            // Data
            // 
            this.Data.DataPropertyName = "Data";
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            this.Data.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Data.Width = 300;
            // 
            // GCodeJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtRunTime);
            this.Controls.Add(this.btnRewind);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnHold);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.GCodeView);
            this.Name = "GCodeJob";
            this.Size = new System.Drawing.Size(398, 431);
            ((System.ComponentModel.ISupportInitialize)(this.GCodeView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtRunTime;
        private System.Windows.Forms.Button btnRewind;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnHold;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridView GCodeView;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
    }
}
