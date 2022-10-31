using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ASP.ClientApp.Models
{
    internal class CaptchaResult
    {
        [JsonProperty("captcha-text")]
        public string CaptchaText { get; set; }
    }
}
