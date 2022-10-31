using MISA.ASP.ClientApp.Models;
using MISA.ASP.ClientApp.Utils.Logging;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MISA.ASP.ClientApp.Utils.Clients
{
    public class MisaCaptchaClient
    {
        private readonly static string DECAPTCHA_API_URL = ConfigurationManager.AppSettings["DeCaptcha_Api_Url"];
        private readonly static string DECAPTCHA_API_KEY = ConfigurationManager.AppSettings["DeCaptcha_Api_Key"];
        private readonly static string DECAPTCHA_PROJECT = ConfigurationManager.AppSettings["DeCaptcha_Project"];
        private readonly static string DECAPTCHA_VENDOR = ConfigurationManager.AppSettings["DeCaptcha_Vendor"];
    
        private HttpClient _client { get; set; }

        public MisaCaptchaClient()
        {
            _client = new HttpClient() { BaseAddress = new Uri(DECAPTCHA_API_URL) };
        }

        public async Task<string> Decaptcha(Stream stream)
        {
            var captcha = String.Empty;
            try
            {
                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    _client.DefaultRequestHeaders.Add("x-api-key", DECAPTCHA_API_KEY);
                    _client.DefaultRequestHeaders.Add("project", DECAPTCHA_PROJECT);

                    var fileStreamContent = new StreamContent(stream);
                    multipartFormContent.Add(fileStreamContent, name: "image", fileName: $"image_{DateTime.Now.Ticks}.jpg");
                    var message = await _client.PostAsync($"image?vendor={DECAPTCHA_VENDOR}", multipartFormContent);
                    message.EnsureSuccessStatusCode();
                    var content = await message.Content.ReadAsStringAsync();
                    captcha = JsonConvert.DeserializeObject<CaptchaResult>(content).CaptchaText;
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError(ex);
                throw ex;
            }

            return captcha;
        }
    }
}
