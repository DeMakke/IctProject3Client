﻿namespace ictProject3
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
            this.lblFilename = new System.Windows.Forms.Label();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDownloadFile
            // 
            this.btnDownloadFile.Location = new System.Drawing.Point(228, 12);
            this.btnDownloadFile.Name = "btnDownloadFile";
            this.btnDownloadFile.Size = new System.Drawing.Size(99, 42);
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
            this.lstFiles.Size = new System.Drawing.Size(210, 147);
            this.lstFiles.TabIndex = 1;
            // 
            // btnUpdateList
            // 
            this.btnUpdateList.Location = new System.Drawing.Point(229, 61);
            this.btnUpdateList.Name = "btnUpdateList";
            this.btnUpdateList.Size = new System.Drawing.Size(98, 23);
            this.btnUpdateList.TabIndex = 2;
            this.btnUpdateList.Text = "Update list";
            this.btnUpdateList.UseVisualStyleBackColor = true;
            this.btnUpdateList.Click += new System.EventHandler(this.btnUpdateList_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(13, 166);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "Upload file";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Location = new System.Drawing.Point(95, 166);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(55, 13);
            this.lblFilename.TabIndex = 4;
            this.lblFilename.Text = "Filename: ";
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Location = new System.Drawing.Point(229, 90);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(98, 23);
            this.btnDeleteItem.TabIndex = 5;
            this.btnDeleteItem.Text = "Delete Item";
            this.btnDeleteItem.UseVisualStyleBackColor = true;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 313);
            this.Controls.Add(this.btnDeleteItem);
            this.Controls.Add(this.lblFilename);
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
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Button btnDeleteItem;
    }
}

