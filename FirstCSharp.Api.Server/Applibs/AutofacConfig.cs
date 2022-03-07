namespace FirstCSharp.Api.Server.Applibs
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Autofac.Integration.WebApi;

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
            builder.RegisterApiControllers(asm);

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
