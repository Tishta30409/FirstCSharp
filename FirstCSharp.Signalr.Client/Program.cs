using Autofac;
using Autofac.Features.Indexed;
using FirstCSharp.Domain.Model;
using FirstCSharp.Domain.Model.ConsoleWrapper;
using FirstCSharp.Domain.Model.MemberProcess;
using FirstCSharp.Signalr.Client.Hubs;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Linq;
using System.Threading;

namespace FirstCSharp.Signalr.Client
{
    class Program
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
            var legalTypesDisplay = legalTypes.Select(t => t.ToDisplay());

            try
            {
                var hubClient = Applibs.AutofacConfig.Container.Resolve<IHubClient>();
                hubClient.StartAsync();

                while (!SpinWait.SpinUntil(() => false, 1000) && hubClient.State != ConnectionState.Connected)
                {
                    console.Clear();
                    console.WriteLine($"HubClient State:{hubClient.State}...");
                }

                var processSets = Applibs.AutofacConfig.Container.Resolve<IIndex<MemberProcessType, IMemberProcess>>();

                string cmd = string.Empty;

                while (cmd.ToLower() != "exit")
                {
                    console.Clear();

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
            catch (System.Exception)
            {

                throw;
            }


        }
    }
}
