namespace ictProject3
{
    partial class AdminPage
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
            this.lstGebruikers = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstGebruikers
            // 
            this.lstGebruikers.FormattingEnabled = true;
            this.lstGebruikers.Location = new System.Drawing.Point(12, 61);
            this.lstGebruikers.Name = "lstGebruikers";
            this.lstGebruikers.Size = new System.Drawing.Size(198, 459);
            this.lstGebruikers.TabIndex = 0;
            // 
            // AdminPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 552);
            this.Controls.Add(this.lstGebruikers);
            this.Name = "AdminPage";
            this.Text = "AdminPage";
            this.Load += new System.EventHandler(this.AdminPage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstGebruikers;
    }
}