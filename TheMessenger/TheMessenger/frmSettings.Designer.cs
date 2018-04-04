namespace TheMessenger
{
    partial class frmSettings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpAudio = new System.Windows.Forms.GroupBox();
            this.chkAudio = new System.Windows.Forms.CheckBox();
            this.grpTime = new System.Windows.Forms.GroupBox();
            this.chkDisplayTime = new System.Windows.Forms.CheckBox();
            this.chkTime24h = new System.Windows.Forms.CheckBox();
            this.grpAudio.SuspendLayout();
            this.grpTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(113, 226);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(197, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpAudio
            // 
            this.grpAudio.Controls.Add(this.chkAudio);
            this.grpAudio.Location = new System.Drawing.Point(13, 13);
            this.grpAudio.Name = "grpAudio";
            this.grpAudio.Size = new System.Drawing.Size(200, 54);
            this.grpAudio.TabIndex = 2;
            this.grpAudio.TabStop = false;
            this.grpAudio.Text = "Audio";
            // 
            // chkAudio
            // 
            this.chkAudio.AutoSize = true;
            this.chkAudio.Location = new System.Drawing.Point(7, 20);
            this.chkAudio.Name = "chkAudio";
            this.chkAudio.Size = new System.Drawing.Size(152, 17);
            this.chkAudio.TabIndex = 0;
            this.chkAudio.Text = "Enable Notification Chimes";
            this.chkAudio.UseVisualStyleBackColor = true;
            // 
            // grpTime
            // 
            this.grpTime.Controls.Add(this.chkTime24h);
            this.grpTime.Controls.Add(this.chkDisplayTime);
            this.grpTime.Location = new System.Drawing.Point(13, 74);
            this.grpTime.Name = "grpTime";
            this.grpTime.Size = new System.Drawing.Size(200, 100);
            this.grpTime.TabIndex = 3;
            this.grpTime.TabStop = false;
            this.grpTime.Text = "Time";
            // 
            // chkDisplayTime
            // 
            this.chkDisplayTime.AutoSize = true;
            this.chkDisplayTime.Location = new System.Drawing.Point(7, 19);
            this.chkDisplayTime.Name = "chkDisplayTime";
            this.chkDisplayTime.Size = new System.Drawing.Size(114, 17);
            this.chkDisplayTime.TabIndex = 1;
            this.chkDisplayTime.Text = "Display Timestamp";
            this.chkDisplayTime.UseVisualStyleBackColor = true;
            this.chkDisplayTime.CheckedChanged += new System.EventHandler(this.chkDisplayTime_CheckedChanged);
            // 
            // chkTime24h
            // 
            this.chkTime24h.AutoSize = true;
            this.chkTime24h.Location = new System.Drawing.Point(7, 43);
            this.chkTime24h.Name = "chkTime24h";
            this.chkTime24h.Size = new System.Drawing.Size(74, 17);
            this.chkTime24h.TabIndex = 2;
            this.chkTime24h.Text = "24h Clock";
            this.chkTime24h.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.grpTime);
            this.Controls.Add(this.grpAudio);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.grpAudio.ResumeLayout(false);
            this.grpAudio.PerformLayout();
            this.grpTime.ResumeLayout(false);
            this.grpTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpAudio;
        private System.Windows.Forms.CheckBox chkAudio;
        private System.Windows.Forms.GroupBox grpTime;
        private System.Windows.Forms.CheckBox chkDisplayTime;
        private System.Windows.Forms.CheckBox chkTime24h;
    }
}