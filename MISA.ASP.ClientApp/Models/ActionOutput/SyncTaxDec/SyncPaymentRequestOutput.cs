using MISA.ASP.ClientApp.Models.Enums;
using MISA.ASP.ClientApp.Models.EtaxCrawler;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ASP.ClientApp.Models.ActionOutput.SyncTaxDec
{
    public class SyncPaymentRequestOutput
    {
        public string ActionID { get; set; }
        public List<SyncPaymentRequestOutputDetail> Details { get; set; } = new List<SyncPaymentRequestOutputDetail>();
        public ActionErrorType ActionErrorType { get; set; }
    }

    public class SyncPaymentRequestOutputDetail
    {
        public int CustomerID { get; set; }
        public List<PaymentRequest> PaymentRequests { get; set; }
        public CrawlErrorType CrawlErrorType { get; set; }
    }
}
