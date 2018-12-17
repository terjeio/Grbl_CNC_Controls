namespace CNC_Controls
{
    partial class DROBase
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
            this.btnScaled = new System.Windows.Forms.Button();
            this.btnZero = new System.Windows.Forms.Button();
            this.txtReadout = new System.Windows.Forms.TextBox();
            this.lblAxis = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnScaled
            // 
            this.btnScaled.Enabled = false;
            this.btnScaled.FlatAppearance.BorderSize = 0;
            this.btnScaled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScaled.Location = new System.Drawing.Point(172, 2);
            this.btnScaled.Margin = new System.Windows.Forms.Padding(0);
            this.btnScaled.Name = "btnScaled";
            this.btnScaled.Size = new System.Drawing.Size(10, 29);
            this.btnScaled.TabIndex = 0;
            this.btnScaled.TabStop = false;
            this.btnScaled.Text = " ";
            this.btnScaled.UseVisualStyleBackColor = true;
            // 
            // btnZero
            // 
            this.btnZero.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZero.Location = new System.Drawing.Point(137, 2);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(29, 29);
            this.btnZero.TabIndex = 0;
            this.btnZero.TabStop = false;
            this.btnZero.Tag = "?";
            this.btnZero.Text = "0";
            this.btnZero.UseVisualStyleBackColor = true;
            // 
            // txtReadout
            // 
            this.txtReadout.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadout.Location = new System.Drawing.Point(28, 2);
            this.txtReadout.Name = "txtReadout";
            this.txtReadout.ReadOnly = true;
            this.txtReadout.Size = new System.Drawing.Size(103, 29);
            this.txtReadout.TabIndex = 12;
            this.txtReadout.TabStop = false;
            this.txtReadout.Tag = "X";
            this.txtReadout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAxis
            // 
            this.lblAxis.AutoSize = true;
            this.lblAxis.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAxis.Location = new System.Drawing.Point(2, 6);
            this.lblAxis.Name = "lblAxis";
            this.lblAxis.Size = new System.Drawing.Size(19, 23);
            this.lblAxis.TabIndex = 0;
            this.lblAxis.Text = "?";
            // 
            // DROBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnScaled);
            this.Controls.Add(this.btnZero);
            this.Controls.Add(this.txtReadout);
            this.Controls.Add(this.lblAxis);
            this.Name = "DROBase";
            this.Size = new System.Drawing.Size(193, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScaled;
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.TextBox txtReadout;
        private System.Windows.Forms.Label lblAxis;
    }
}
