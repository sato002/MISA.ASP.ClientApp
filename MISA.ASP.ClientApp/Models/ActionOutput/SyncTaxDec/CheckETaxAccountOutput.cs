using MISA.ASP.ClientApp.Models.Enums;
using MISA.ASP.ClientApp.Models.EtaxCrawler;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ASP.ClientApp.Models.ActionOutput.SyncTaxDec
{
    internal class CheckETaxAccountOutput
    {
        public string ActionID { get; set; }
        public List<CheckETaxAccountOutputDetail> Details { get; set; } = new List<CheckETaxAccountOutputDetail>();
        public ActionErrorType ActionErrorType { get; set; }
    }

    internal class CheckETaxAccountOutputDetail
    {
        public int CustomerID { get; set; }
        public bool IsValid { get; set; }
        public CrawlErrorType CrawlErrorType { get; set; }
    }
}
