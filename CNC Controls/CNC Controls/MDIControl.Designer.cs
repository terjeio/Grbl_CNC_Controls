namespace CNC_Controls
{
    partial class MDIControl
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
            this.btnSend = new System.Windows.Forms.Button();
            this.lblMDI = new System.Windows.Forms.Label();
            this.txtMDI = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(400, 4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(41, 21);
            this.btnSend.TabIndex = 0;
            this.btnSend.TabStop = false;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // lblMDI
            // 
            this.lblMDI.AutoSize = true;
            this.lblMDI.Location = new System.Drawing.Point(7, 7);
            this.lblMDI.Name = "lblMDI";
            this.lblMDI.Size = new System.Drawing.Size(30, 13);
            this.lblMDI.TabIndex = 1;
            this.lblMDI.Text = "MDI:";
            // 
            // txtMDI
            // 
            this.txtMDI.Location = new System.Drawing.Point(43, 4);
            this.txtMDI.Name = "txtMDI";
            this.txtMDI.Size = new System.Drawing.Size(350, 20);
            this.txtMDI.TabIndex = 1;
            // 
            // MDIControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.txtMDI);
            this.Controls.Add(this.lblMDI);
            this.Controls.Add(this.btnSend);
            this.Name = "MDIControl";
            this.Size = new System.Drawing.Size(450, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblMDI;
        private System.Windows.Forms.TextBox txtMDI;
    }
}
