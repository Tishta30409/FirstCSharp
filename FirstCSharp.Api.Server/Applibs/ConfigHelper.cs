

namespace FirstCSharp.Api.Server.Applibs
{
    using System.Configuration;
    class ConfigHelper
    {
        public static string ServiceUrl
        {
            get
                => $"http://localhost:8085";
        }

        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["FirstCSharp"].ConnectionString;
    }
}
