

namespace FirstCSharp.Applibs
{
    using Autofac;
    using FirstCSharp.Domain.Model.ConsoleWrapper;
    using FirstCSharp.Domain.Model.MemberProcess;
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

            //取得組件 其中含有正在執行的程式碼

            var asm = Assembly.GetExecutingAssembly();

            // =>lambda expression Enum.Parse 把輸入轉成 對應的鍵值?
            // 假設不想要一個一個註冊，可以用scan的方式。
            // 下面scan一個assembly所有的type，當那個type的名字最後結尾是Repository的時候，
            // 把它註冊的service設為這個class的interface
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

            var connectStr = ConfigHelper.ConnectionString;

            // sql ioc
            builder.RegisterAssemblyTypes(Assembly.Load("FirstCSharp.Persistent"), Assembly.Load("FirstCSharp.Domain"))
                .WithParameter("connectionString", ConfigHelper.ConnectionString)//建構子變數
                .Where(t => t.Namespace == "FirstCSharp.Persistent.Repository" || t.Namespace == "FirstCSharp.Domain.Repository")
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == $"I{t.Name}"))//轉映射
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            container = builder.Build();
        }
    }

    
}
