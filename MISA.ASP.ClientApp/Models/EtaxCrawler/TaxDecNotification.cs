using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ASP.ClientApp
{
    public class TaxDecNotification
    {
        public string NotificationID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string SendDate { get; set; }
        public string SendBy { get; set; }
        public string XMLTenTBao { get; set; }
        public string XMLTrangThai { get; set; }
        public string FileName { get; set; }
    }
}
