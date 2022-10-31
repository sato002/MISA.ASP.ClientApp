namespace MISA.ASP.ClientApp.UI
{
    partial class frmNewVersion
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbDownloadLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(154, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Có version mới nè";
            // 
            // lbDownloadLink
            // 
            this.lbDownloadLink.AutoSize = true;
            this.lbDownloadLink.Location = new System.Drawing.Point(251, 66);
            this.lbDownloadLink.Name = "lbDownloadLink";
            this.lbDownloadLink.Size = new System.Drawing.Size(39, 13);
            this.lbDownloadLink.TabIndex = 1;
            this.lbDownloadLink.TabStop = true;
            this.lbDownloadLink.Text = "tại đây";
            this.lbDownloadLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbDownloadLink_LinkClicked);
            // 
            // frmNewVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 221);
            this.Controls.Add(this.lbDownloadLink);
            this.Controls.Add(this.label1);
            this.Name = "frmNewVersion";
            this.Text = "frmNewVersion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lbDownloadLink;
    }
}