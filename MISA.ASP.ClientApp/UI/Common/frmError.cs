using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MISA.ASP.ClientApp.UI.Common
{
    public partial class frmError : Form
    {
        public frmError(string message = "")
        {
            InitializeComponent();
            this.TopMost = true;
            this.TopLevel = true;
            if(!String.IsNullOrEmpty(message))
            {
                SetMessage(message);
            }
        }

        #region Thao tác với Form Control
        private void SetMessage(string message)
        {
            if(InvokeRequired)
            {
                BeginInvoke(new Action<string>(SetMessage), message);
            }
            else
            {
                lbMessage.Text = message;
            }
        }
        #endregion
    }
}
