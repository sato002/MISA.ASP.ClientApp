using MISA.ASP.ClientApp.Utils.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MISA.ASP.ClientApp.Models.EtaxCrawler
{
    public class TaxDeclarationSubmitted
    {
        [JsonIgnore]
        public string Order { get; set; }
        public string TaxCode { get; set; }
        public string TransactionID { get; set; }
        public string HideTransactionID { get; set; }
        public string ParentTransactionID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TaxPeriod { get; set; }
        public string DeclarationType { get; set; }
        public string SubmitTimes { get; set; }
        public string AdditionalTimes { get; set; }
        public string SubmitDate { get; set; }
        public string TaxAgencyCode { get; set; }
        public string TaxAgencyName { get; set; }
        public string DebitAmount { get; set; }
        public string CreditAmount { get; set; }
        public string FileName { get; set; }
        public string State { get; set; }
        [JsonConverter(typeof(BoolConverter))]
        public bool IsHideDownloadLink { get; set; }
        [JsonConverter(typeof(BoolConverter))]
        public bool IsHideNotificationLink { get; set; }
        [JsonConverter(typeof(BoolConverter))]
        public bool IsHideTransactionID { get; set; }
        public List<TaxDecNotification> Notifications { get; set; }
        [JsonIgnore]
        public string DisplayTransactionID 
        { 
            get
            {
                return !String.IsNullOrEmpty(TransactionID) ? TransactionID : HideTransactionID;
            }
        }
    }
}
