using MISA.ASP.ClientApp.Models.Enums;
using MISA.ASP.ClientApp.Models.EtaxCrawler;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ASP.ClientApp.Models.ActionOutput.SyncTaxDec
{
    internal class SyncTaxDecRegisteredOutput
    {
        public string ActionID { get; set; }
        public List<SyncTaxDecRegisteredOutputDetail> Details { get; set; } = new List<SyncTaxDecRegisteredOutputDetail>();
        public ActionErrorType ActionErrorType { get; set; }
    }

    internal class SyncTaxDecRegisteredOutputDetail
    {
        public int CustomerID { get; set; }
        public List<TaxDeclarationRegistered> TaxDeclarationRegistereds { get; set; }
        public CrawlErrorType CrawlErrorType { get; set; }
    }
}
