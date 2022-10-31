using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ASP.ClientApp.Models.ActionInput.SynTaxDec
{
    internal class SyncTaxDecRegisteredInput : BaseASPInput
    {
        public List<SyncTaxDecRegisteredInputDetail> Details { get; set; }
    }

    public class SyncTaxDecRegisteredInputDetail
    {
        public int CustomerID { get; set; }
        public string TaxCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
