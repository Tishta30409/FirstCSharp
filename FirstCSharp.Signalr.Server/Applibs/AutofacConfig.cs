
namespace FirstCSharp.Signalr.Server.Applibs
{
    using Autofac;
    using FirstCSharp.Signalr.Server.Model;
    using System.Linq;
    using System.Reflection;

    internal static class AutofacConfig
    {
        private static IContainer container;

        public static IContainer Container
        {
            get
            {
                if (container == null)
                {
                    Register();
                }

                return container;
            }
        }

        private static void Register()
        {
            var builder = new ContainerBuilder();
            var asm = Assembly.GetExecutingAssembly();

            // Action Handler
            builder.RegisterAssemblyTypes(asm)
                .Named<IActionHandler>(t => t.Name.Replace("ActionHandler", string.Empty).ToLower())
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            // sql ioc
            builder.RegisterAssemblyTypes(Assembly.Load("FirstCSharp.Persistent"), Assembly.Load("FirstCSharp.Domain"))
                .WithParameter("connectionString", ConfigHelper.ConnectionString)
                .Where(t => t.Namespace == "FirstCSharp.Persistent.Repository" || t.Namespace == "FirstCSharp.Domain.Repository")
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == $"I{t.Name}"))
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            container = builder.Build();
        }
    }
}
