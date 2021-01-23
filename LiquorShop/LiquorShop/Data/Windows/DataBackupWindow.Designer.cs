namespace EvanDangol.Data
{
    partial class DataBackupWindow
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
            this.DatabasecomboBox = new System.Windows.Forms.ComboBox();
            this.ServercomboBox = new System.Windows.Forms.ComboBox();
            this.simpleButton3 = new System.Windows.Forms.Button();
            this.simpleButton1 = new System.Windows.Forms.Button();
            this.labelControl2 = new System.Windows.Forms.Label();
            this.labelControl1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DatabasecomboBox
            // 
            this.DatabasecomboBox.FormattingEnabled = true;
            this.DatabasecomboBox.Location = new System.Drawing.Point(260, 125);
            this.DatabasecomboBox.Name = "DatabasecomboBox";
            this.DatabasecomboBox.Size = new System.Drawing.Size(161, 21);
            this.DatabasecomboBox.TabIndex = 18;
            this.DatabasecomboBox.Text = "sms";
            // 
            // ServercomboBox
            // 
            this.ServercomboBox.FormattingEnabled = true;
            this.ServercomboBox.Location = new System.Drawing.Point(260, 83);
            this.ServercomboBox.Name = "ServercomboBox";
            this.ServercomboBox.Size = new System.Drawing.Size(161, 21);
            this.ServercomboBox.TabIndex = 17;
            this.ServercomboBox.SelectedIndexChanged += new System.EventHandler(this.ServercomboBox_SelectedIndexChanged);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(346, 168);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(75, 23);
            this.simpleButton3.TabIndex = 16;
            this.simpleButton3.Text = "Restore";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(260, 168);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 15;
            this.simpleButton1.Text = "Backup";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(94, 133);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(122, 13);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "Select Database Name";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(91, 91);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(125, 13);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Select Server Name ";
            // 
            // DataBackupWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 320);
            this.Controls.Add(this.DatabasecomboBox);
            this.Controls.Add(this.ServercomboBox);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "DataBackupWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DataBackupWindow";
            this.Load += new System.EventHandler(this.DataBackupWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox DatabasecomboBox;
        private System.Windows.Forms.ComboBox ServercomboBox;
        private System.Windows.Forms.Button simpleButton3;
        private System.Windows.Forms.Button simpleButton1;
        private System.Windows.Forms.Label labelControl2;
        private System.Windows.Forms.Label labelControl1;
    }
}