namespace CNC_Controls
{
    partial class DROControl
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
            this.lblX = new System.Windows.Forms.Label();
            this.txtXPos = new System.Windows.Forms.TextBox();
            this.txtYPos = new System.Windows.Forms.TextBox();
            this.lblY = new System.Windows.Forms.Label();
            this.txtZPos = new System.Windows.Forms.TextBox();
            this.lblZ = new System.Windows.Forms.Label();
            this.btnXZero = new System.Windows.Forms.Button();
            this.btnYZero = new System.Windows.Forms.Button();
            this.btnZZero = new System.Windows.Forms.Button();
            this.btnZeroAll = new System.Windows.Forms.Button();
            this.grpDRO = new System.Windows.Forms.GroupBox();
            this.grpDRO.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblX.Location = new System.Drawing.Point(9, 22);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(20, 23);
            this.lblX.TabIndex = 0;
            this.lblX.Text = "X";
            // 
            // txtXPos
            // 
            this.txtXPos.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtXPos.Location = new System.Drawing.Point(35, 19);
            this.txtXPos.Name = "txtXPos";
            this.txtXPos.ReadOnly = true;
            this.txtXPos.Size = new System.Drawing.Size(118, 29);
            this.txtXPos.TabIndex = 1;
            this.txtXPos.TabStop = false;
            this.txtXPos.Tag = "X";
            this.txtXPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtYPos
            // 
            this.txtYPos.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYPos.Location = new System.Drawing.Point(35, 54);
            this.txtYPos.Name = "txtYPos";
            this.txtYPos.ReadOnly = true;
            this.txtYPos.Size = new System.Drawing.Size(118, 29);
            this.txtYPos.TabIndex = 3;
            this.txtYPos.TabStop = false;
            this.txtYPos.Tag = "Y";
            this.txtYPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblY.Location = new System.Drawing.Point(9, 57);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(19, 23);
            this.lblY.TabIndex = 2;
            this.lblY.Text = "Y";
            // 
            // txtZPos
            // 
            this.txtZPos.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZPos.Location = new System.Drawing.Point(35, 89);
            this.txtZPos.Name = "txtZPos";
            this.txtZPos.ReadOnly = true;
            this.txtZPos.Size = new System.Drawing.Size(118, 29);
            this.txtZPos.TabIndex = 5;
            this.txtZPos.TabStop = false;
            this.txtZPos.Tag = "Z";
            this.txtZPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblZ
            // 
            this.lblZ.AutoSize = true;
            this.lblZ.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZ.Location = new System.Drawing.Point(9, 92);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(20, 23);
            this.lblZ.TabIndex = 4;
            this.lblZ.Text = "Z";
            // 
            // btnXZero
            // 
            this.btnXZero.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXZero.Location = new System.Drawing.Point(159, 19);
            this.btnXZero.Name = "btnXZero";
            this.btnXZero.Size = new System.Drawing.Size(29, 29);
            this.btnXZero.TabIndex = 6;
            this.btnXZero.Tag = "X";
            this.btnXZero.Text = "0";
            this.btnXZero.UseVisualStyleBackColor = true;
            // 
            // btnYZero
            // 
            this.btnYZero.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYZero.Location = new System.Drawing.Point(159, 54);
            this.btnYZero.Name = "btnYZero";
            this.btnYZero.Size = new System.Drawing.Size(29, 29);
            this.btnYZero.TabIndex = 7;
            this.btnYZero.Tag = "Y";
            this.btnYZero.Text = "0";
            this.btnYZero.UseVisualStyleBackColor = true;
            // 
            // btnZZero
            // 
            this.btnZZero.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZZero.Location = new System.Drawing.Point(159, 89);
            this.btnZZero.Name = "btnZZero";
            this.btnZZero.Size = new System.Drawing.Size(29, 29);
            this.btnZZero.TabIndex = 8;
            this.btnZZero.Tag = "Z";
            this.btnZZero.Text = "0";
            this.btnZZero.UseVisualStyleBackColor = true;
            // 
            // btnZeroAll
            // 
            this.btnZeroAll.Location = new System.Drawing.Point(35, 124);
            this.btnZeroAll.Name = "btnZeroAll";
            this.btnZeroAll.Size = new System.Drawing.Size(153, 30);
            this.btnZeroAll.TabIndex = 9;
            this.btnZeroAll.Tag = "ALL";
            this.btnZeroAll.Text = "Zero all";
            this.btnZeroAll.UseVisualStyleBackColor = true;
            // 
            // grpDRO
            // 
            this.grpDRO.Controls.Add(this.btnZeroAll);
            this.grpDRO.Controls.Add(this.btnZZero);
            this.grpDRO.Controls.Add(this.btnYZero);
            this.grpDRO.Controls.Add(this.btnXZero);
            this.grpDRO.Controls.Add(this.txtZPos);
            this.grpDRO.Controls.Add(this.lblZ);
            this.grpDRO.Controls.Add(this.txtYPos);
            this.grpDRO.Controls.Add(this.lblY);
            this.grpDRO.Controls.Add(this.txtXPos);
            this.grpDRO.Controls.Add(this.lblX);
            this.grpDRO.Location = new System.Drawing.Point(3, 3);
            this.grpDRO.Name = "grpDRO";
            this.grpDRO.Size = new System.Drawing.Size(199, 166);
            this.grpDRO.TabIndex = 10;
            this.grpDRO.TabStop = false;
            this.grpDRO.Text = "DRO";
            // 
            // DROControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpDRO);
            this.Name = "DROControl";
            this.Size = new System.Drawing.Size(202, 169);
            this.grpDRO.ResumeLayout(false);
            this.grpDRO.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.TextBox txtXPos;
        private System.Windows.Forms.TextBox txtYPos;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.TextBox txtZPos;
        private System.Windows.Forms.Label lblZ;
        private System.Windows.Forms.Button btnXZero;
        private System.Windows.Forms.Button btnYZero;
        private System.Windows.Forms.Button btnZZero;
        private System.Windows.Forms.Button btnZeroAll;
        private System.Windows.Forms.GroupBox grpDRO;
    }
}
