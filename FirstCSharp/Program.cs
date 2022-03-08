

namespace FirstCSharp
{
    using FirstCSharp.Domain.Model.ConsoleWrapper;
    using System.Linq;
    using Autofac;
    using FirstCSharp.Domain.Model.MemberProcess;
    using Autofac.Features.Indexed;
    using FirstCSharp.Domain.Model;
    using System;

    internal class Program
    {
        static void Main(string[] args)
        {
            var console = Applibs.AutofacConfig.Container.Resolve<IConsoleWrapper>();

            var legalTypes = new MemberProcessType[]
            {
                MemberProcessType.MemberAdd,
                MemberProcessType.MemberQuery,
                MemberProcessType.MemberUpdate,
                MemberProcessType.MemberDelete,
                MemberProcessType.MemberBatchAdd
            };

            var legalTypesFormat = legalTypes.Select(t => $"{((int)t)}");
            //使用擴充的方法
            var legalTypesDisplay = legalTypes.Select(t => t.ToDisplay());

            //var testKey = (MemberProcessType)Convert.ToInt32(1);
            //console.Write(Convert.ToString(testKey));


            try
            {
                // new IFirstProcess?
                var processSets = Applibs.AutofacConfig.Container.Resolve<IIndex<MemberProcessType, IMemberProcess>>();

                string cmd = string.Empty;

                while (cmd.ToLower() != "exit")
                {
                    //console.Clear();

                    if (legalTypesFormat.Any(p => p == cmd) &&
                        processSets.TryGetValue((MemberProcessType)Convert.ToInt32(cmd), out IMemberProcess process) &&
                        !process.Execute())
                    {
                        console.WriteLine("Finished!");
                    }

                    console.WriteLine(string.Join("\r\n", legalTypesDisplay));

                    cmd = console.ReadLine();
                }

            }
            catch (Exception ex)
            {
                console.Clear();
                console.WriteLine(ex.Message);
                console.Read();
            }

            console.Clear();
            console.WriteLine("Finished!");
            console.Read();
        }
    }
}
