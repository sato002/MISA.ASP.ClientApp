using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ASP.ClientApp.Models.ActionInput
{
    public class BaseASPInput
    {
        public string AccessToken { get; set; }
        public string MisaID { get; set; }
        public int EmployeeID { get; set; }
        public int ProfileID { get; set; }
    }
}
