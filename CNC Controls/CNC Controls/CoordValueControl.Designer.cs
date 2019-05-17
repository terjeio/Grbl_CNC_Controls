namespace CNC_Controls
{
    partial class CoordValueControl
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
            this.unit = new System.Windows.Forms.Label();
            this.data = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // unit
            // 
            this.unit.AutoSize = true;
            this.unit.Location = new System.Drawing.Point(125, 3);
            this.unit.Name = "unit";
            this.unit.Size = new System.Drawing.Size(23, 13);
            this.unit.TabIndex = 32;
            this.unit.Text = "mm";
            // 
            // data
            // 
            this.data.Location = new System.Drawing.Point(69, 0);
            this.data.Name = "data";
            this.data.Size = new System.Drawing.Size(50, 20);
            this.data.TabIndex = 31;
            this.data.Text = "0";
            this.data.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Location = new System.Drawing.Point(29, 3);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(34, 13);
            this.label.TabIndex = 30;
            this.label.Text = "label:";
            this.label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CoordValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.unit);
            this.Controls.Add(this.data);
            this.Controls.Add(this.label);
            this.Name = "CoordValue";
            this.Size = new System.Drawing.Size(175, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label unit;
        private System.Windows.Forms.TextBox data;
        private System.Windows.Forms.Label label;
    }
}
