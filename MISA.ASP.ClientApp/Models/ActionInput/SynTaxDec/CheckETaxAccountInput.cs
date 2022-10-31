using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ASP.ClientApp.Models.ActionInput.SynTaxDec
{
    internal class CheckETaxAccountInput : BaseASPInput
    {
        public List<CheckETaxAccountInputDetail> Details { get; set; }
    }

    public class CheckETaxAccountInputDetail
    {
        public int CustomerID { get; set; }
        public string TaxCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
