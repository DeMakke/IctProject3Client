namespace ictProject3
{
    partial class Form1
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
            this.btnDownloadFile = new System.Windows.Forms.Button();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.btnUpdateList = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.loginButton = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            this.btnDelen = new System.Windows.Forms.Button();
            this.btnWijzigen = new System.Windows.Forms.Button();
            this.btnAdmin = new System.Windows.Forms.Button();
            this.rtxtHistory = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDownloadFile
            // 
            this.btnDownloadFile.Location = new System.Drawing.Point(227, 41);
            this.btnDownloadFile.Name = "btnDownloadFile";
            this.btnDownloadFile.Size = new System.Drawing.Size(109, 42);
            this.btnDownloadFile.TabIndex = 0;
            this.btnDownloadFile.Text = "Download selected file";
            this.btnDownloadFile.UseVisualStyleBackColor = true;
            this.btnDownloadFile.Click += new System.EventHandler(this.btnDownloadFile_Click);
            // 
            // lstFiles
            // 
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.Location = new System.Drawing.Point(12, 12);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(210, 160);
            this.lstFiles.TabIndex = 1;
            // 
            // btnUpdateList
            // 
            this.btnUpdateList.Location = new System.Drawing.Point(228, 90);
            this.btnUpdateList.Name = "btnUpdateList";
            this.btnUpdateList.Size = new System.Drawing.Size(108, 23);
            this.btnUpdateList.TabIndex = 2;
            this.btnUpdateList.Text = "Update list";
            this.btnUpdateList.UseVisualStyleBackColor = true;
            this.btnUpdateList.Click += new System.EventHandler(this.btnUpdateList_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(228, 12);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(108, 23);
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "Upload file";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Location = new System.Drawing.Point(228, 119);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(108, 23);
            this.btnDeleteItem.TabIndex = 5;
            this.btnDeleteItem.Text = "Delete Item";
            this.btnDeleteItem.UseVisualStyleBackColor = true;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // loginButton
            // 
            this.loginButton.BackColor = System.Drawing.SystemColors.Info;
            this.loginButton.Location = new System.Drawing.Point(12, 288);
            this.loginButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(56, 24);
            this.loginButton.TabIndex = 6;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = false;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // logoutButton
            // 
            this.logoutButton.BackColor = System.Drawing.SystemColors.Info;
            this.logoutButton.Location = new System.Drawing.Point(73, 288);
            this.logoutButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(56, 24);
            this.logoutButton.TabIndex = 7;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = false;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // btnDelen
            // 
            this.btnDelen.Location = new System.Drawing.Point(228, 148);
            this.btnDelen.Name = "btnDelen";
            this.btnDelen.Size = new System.Drawing.Size(108, 24);
            this.btnDelen.TabIndex = 6;
            this.btnDelen.Text = "Share";
            this.btnDelen.UseVisualStyleBackColor = true;
            this.btnDelen.Click += new System.EventHandler(this.btnDelen_Click);
            // 
            // btnWijzigen
            // 
            this.btnWijzigen.Location = new System.Drawing.Point(228, 288);
            this.btnWijzigen.Name = "btnWijzigen";
            this.btnWijzigen.Size = new System.Drawing.Size(110, 24);
            this.btnWijzigen.TabIndex = 8;
            this.btnWijzigen.Text = "Change Login";
            this.btnWijzigen.UseVisualStyleBackColor = true;
            this.btnWijzigen.Click += new System.EventHandler(this.btnWijzigen_Click);
            // 
            // btnAdmin
            // 
            this.btnAdmin.Location = new System.Drawing.Point(133, 288);
            this.btnAdmin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(90, 24);
            this.btnAdmin.TabIndex = 9;
            this.btnAdmin.Text = "Admin";
            this.btnAdmin.UseVisualStyleBackColor = true;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // rtxtHistory
            // 
            this.rtxtHistory.Location = new System.Drawing.Point(12, 204);
            this.rtxtHistory.Name = "rtxtHistory";
            this.rtxtHistory.ReadOnly = true;
            this.rtxtHistory.Size = new System.Drawing.Size(324, 79);
            this.rtxtHistory.TabIndex = 10;
            this.rtxtHistory.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Session History:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 323);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtxtHistory);
            this.Controls.Add(this.btnAdmin);
            this.Controls.Add(this.btnWijzigen);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.btnDelen);
            this.Controls.Add(this.btnDeleteItem);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnUpdateList);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.btnDownloadFile);
            this.Name = "Form1";
            this.Text = "ictProject3";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownloadFile;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Button btnUpdateList;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Button btnDeleteItem;

        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button logoutButton;

        private System.Windows.Forms.Button btnDelen;
        private System.Windows.Forms.Button btnWijzigen;
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.RichTextBox rtxtHistory;
        private System.Windows.Forms.Label label1;
    }
}

