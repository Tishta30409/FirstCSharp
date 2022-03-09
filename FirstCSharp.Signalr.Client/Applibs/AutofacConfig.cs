

namespace FirstCSharp.Signalr.Client.Applibs
{
    using Autofac;
    using FirstCSharp.Domain.Model.ConsoleWrapper;
    using FirstCSharp.Domain.Model.MemberProcess;
    using FirstCSharp.Signalr.Client.Hubs;
    using FirstCSharp.Signalr.Client.Model;
    using System;
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
        public static void Register()
        {
            var builder = new ContainerBuilder();
            var asm = Assembly.GetExecutingAssembly();

            // 指定處理client指令的handler
            builder.RegisterAssemblyTypes(asm)
                .Where(t => t.IsAssignableTo<IActionHandler>())
                .Named<IActionHandler>(t => t.Name.Replace("ActionHandler", string.Empty).ToLower())
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            builder.RegisterAssemblyTypes(asm)
               .Where(t => t.IsAssignableTo<IMemberProcess>())
               .Keyed<IMemberProcess>(p => (MemberProcessType)Enum.Parse(typeof(MemberProcessType), p.Name.Replace("Process", string.Empty), true))
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
               .SingleInstance();

            // console wrapper
            builder.RegisterType<ConsoleWrapper>()
                .As<IConsoleWrapper>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            // hubclient
            builder.RegisterType<HubClient>()
                .WithParameter("url", ConfigHelper.SignalrUrl)
                .WithParameter("hubName", "FirstCSharpHub")
                .As<IHubClient>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            container = builder.Build();
        }
    }
}
