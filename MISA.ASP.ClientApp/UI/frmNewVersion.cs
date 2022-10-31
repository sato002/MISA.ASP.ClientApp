using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MISA.ASP.ClientApp.UI
{
    public partial class frmNewVersion : Form
    {
        private string _downloadLink { get; set; }
        public frmNewVersion(string downloadLink)
        {
            InitializeComponent();
            this.TopMost = true;
            this.TopLevel = true;

            _downloadLink = downloadLink;
        }

        private void lbDownloadLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(_downloadLink);
        }
    }
}
