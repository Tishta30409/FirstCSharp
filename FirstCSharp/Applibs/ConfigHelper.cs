namespace FirstCSharp.Applibs
{
    using System.Configuration;

    internal static class ConfigHelper
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["FirstCSharp"].ConnectionString;
    }
}
