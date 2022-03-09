using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCSharp.Signalr.Client.Applibs
{
    internal static class ConfigHelper
    {
        public static string SignalrUrl
        {
            get
                => $"http://{ConfigurationManager.AppSettings["SignalrUrl"]}:8085/signalr";
        }
    }
}
