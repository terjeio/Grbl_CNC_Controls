namespace CNC_Controls
{
    partial class GrblStatusControl
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
            this.chkCheckMode = new System.Windows.Forms.CheckBox();
            this.txtState = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkCheckMode
            // 
            this.chkCheckMode.AutoSize = true;
            this.chkCheckMode.Location = new System.Drawing.Point(126, 6);
            this.chkCheckMode.Name = "chkCheckMode";
            this.chkCheckMode.Size = new System.Drawing.Size(57, 17);
            this.chkCheckMode.TabIndex = 44;
            this.chkCheckMode.TabStop = false;
            this.chkCheckMode.Text = "Check";
            this.chkCheckMode.UseVisualStyleBackColor = true;
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(45, 3);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(74, 20);
            this.txtState.TabIndex = 43;
            this.txtState.TabStop = false;
            this.txtState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(7, 6);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(32, 13);
            this.lblState.TabIndex = 42;
            this.lblState.Text = "State";
            // 
            // btnUnlock
            // 
            this.btnUnlock.Location = new System.Drawing.Point(64, 29);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(55, 32);
            this.btnUnlock.TabIndex = 41;
            this.btnUnlock.TabStop = false;
            this.btnUnlock.Text = "Unlock";
            this.btnUnlock.UseVisualStyleBackColor = true;
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(3, 29);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(55, 32);
            this.btnHome.TabIndex = 40;
            this.btnHome.TabStop = false;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Red;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(125, 29);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(58, 32);
            this.btnReset.TabIndex = 39;
            this.btnReset.TabStop = false;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = false;
            // 
            // GrblStatusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkCheckMode);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.btnReset);
            this.Name = "GrblStatusControl";
            this.Size = new System.Drawing.Size(190, 66);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkCheckMode;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnReset;
    }
}
