using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ASP.ClientApp.Utils.FileHandler
{
    public static class FileUtil
    {
        public static string BASE_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? "");
    }
}
