namespace ictProject3
{
    partial class DeelVenster
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
            this.btnToevoegen = new System.Windows.Forms.Button();
            this.btnVerwijderen = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lstGebruikers = new System.Windows.Forms.ListBox();
            this.lstGeselecteerdeGebruikers = new System.Windows.Forms.ListBox();
            this.checkBoxPubliekDelen = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnToevoegen
            // 
            this.btnToevoegen.Location = new System.Drawing.Point(236, 128);
            this.btnToevoegen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnToevoegen.Name = "btnToevoegen";
            this.btnToevoegen.Size = new System.Drawing.Size(124, 43);
            this.btnToevoegen.TabIndex = 0;
            this.btnToevoegen.Text = "Toevoegen";
            this.btnToevoegen.UseVisualStyleBackColor = true;
            this.btnToevoegen.Click += new System.EventHandler(this.btnToevoegen_Click);
            // 
            // btnVerwijderen
            // 
            this.btnVerwijderen.Location = new System.Drawing.Point(236, 178);
            this.btnVerwijderen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVerwijderen.Name = "btnVerwijderen";
            this.btnVerwijderen.Size = new System.Drawing.Size(124, 43);
            this.btnVerwijderen.TabIndex = 1;
            this.btnVerwijderen.Text = "Verwijderen";
            this.btnVerwijderen.UseVisualStyleBackColor = true;
            this.btnVerwijderen.Click += new System.EventHandler(this.btnVerwijderen_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(360, 378);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 43);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(456, 378);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 43);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lstGebruikers
            // 
            this.lstGebruikers.FormattingEnabled = true;
            this.lstGebruikers.ItemHeight = 16;
            this.lstGebruikers.Location = new System.Drawing.Point(33, 54);
            this.lstGebruikers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstGebruikers.Name = "lstGebruikers";
            this.lstGebruikers.Size = new System.Drawing.Size(159, 292);
            this.lstGebruikers.TabIndex = 4;
            // 
            // lstGeselecteerdeGebruikers
            // 
            this.lstGeselecteerdeGebruikers.FormattingEnabled = true;
            this.lstGeselecteerdeGebruikers.ItemHeight = 16;
            this.lstGeselecteerdeGebruikers.Location = new System.Drawing.Point(397, 54);
            this.lstGeselecteerdeGebruikers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstGeselecteerdeGebruikers.Name = "lstGeselecteerdeGebruikers";
            this.lstGeselecteerdeGebruikers.Size = new System.Drawing.Size(159, 292);
            this.lstGeselecteerdeGebruikers.TabIndex = 5;
            // 
            // checkBoxPubliekDelen
            // 
            this.checkBoxPubliekDelen.AutoSize = true;
            this.checkBoxPubliekDelen.Location = new System.Drawing.Point(243, 265);
            this.checkBoxPubliekDelen.Name = "checkBoxPubliekDelen";
            this.checkBoxPubliekDelen.Size = new System.Drawing.Size(117, 21);
            this.checkBoxPubliekDelen.TabIndex = 6;
            this.checkBoxPubliekDelen.Text = "Publiek Delen";
            this.checkBoxPubliekDelen.UseVisualStyleBackColor = true;
            this.checkBoxPubliekDelen.CheckedChanged += new System.EventHandler(this.checkBoxPubliekDelen_CheckedChanged);
            // 
            // DeelVenster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 436);
            this.Controls.Add(this.checkBoxPubliekDelen);
            this.Controls.Add(this.lstGeselecteerdeGebruikers);
            this.Controls.Add(this.lstGebruikers);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnVerwijderen);
            this.Controls.Add(this.btnToevoegen);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DeelVenster";
            this.Text = "DeelVenster";
            this.Load += new System.EventHandler(this.DeelVenster_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnToevoegen;
        private System.Windows.Forms.Button btnVerwijderen;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lstGebruikers;
        private System.Windows.Forms.ListBox lstGeselecteerdeGebruikers;
        private System.Windows.Forms.CheckBox checkBoxPubliekDelen;
    }
}