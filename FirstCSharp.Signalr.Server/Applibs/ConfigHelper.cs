
namespace FirstCSharp.Signalr.Server.Applibs
{
    using System.Configuration;

    internal static class ConfigHelper
    {
        public static string SignalrUrl
        {
            get
                => $"http://localhost:8085";
        }

        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["FirstCSharp"].ConnectionString;
    }
}
