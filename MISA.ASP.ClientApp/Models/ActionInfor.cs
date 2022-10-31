using MISA.ASP.ClientApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ASP.ClientApp.Models
{
    internal class ActionInfor
    {
        public string ID { get; set; }
        public ActionType ActionType { get; set; }
        public string Data { get; set; }
    }
}
