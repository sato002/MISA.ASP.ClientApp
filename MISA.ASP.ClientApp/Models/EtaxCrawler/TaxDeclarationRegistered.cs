using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MISA.ASP.ClientApp.Models.EtaxCrawler
{
    public class TaxDeclarationRegistered
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public string TaxPeriodType { get; set; }

        public string BeginTaxPeriod { get; set; }
    }
}
