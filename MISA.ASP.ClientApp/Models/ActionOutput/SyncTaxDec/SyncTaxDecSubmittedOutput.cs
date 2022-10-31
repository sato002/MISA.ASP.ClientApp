using MISA.ASP.ClientApp.Models.Enums;
using MISA.ASP.ClientApp.Models.EtaxCrawler;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ASP.ClientApp.Models.ActionOutput.SyncTaxDec
{
    internal class SyncTaxDecSubmittedOutput
    {
        public string ActionID { get; set; }
        public List<SyncTaxDecSubmittedOutputDetail> Details { get; set; } = new List<SyncTaxDecSubmittedOutputDetail>();
        public ActionErrorType ActionErrorType { get; set; }
    }

    internal class SyncTaxDecSubmittedOutputDetail
    {
        public int CustomerID { get; set; }
        public List<TaxDeclarationSubmitted> TaxDeclarationSubmitteds { get; set; }
        public CrawlErrorType CrawlErrorType { get; set; }
    }
}
