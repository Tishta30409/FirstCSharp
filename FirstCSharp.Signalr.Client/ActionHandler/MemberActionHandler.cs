using FirstCSharp.Domain.Action;
using FirstCSharp.Domain.KeepAliveConn;
using FirstCSharp.Domain.Model.ConsoleWrapper;
using FirstCSharp.Signalr.Client.Hubs;
using FirstCSharp.Signalr.Client.Model;
using Newtonsoft.Json;
using System;

namespace FirstCSharp.Signalr.Client.ActionHandler
{
    public class MemberActionHandler : IActionHandler
    {
        private IHubClient hubClient;

        private IConsoleWrapper console;

        public MemberActionHandler(IHubClient client, IConsoleWrapper console)
        {
            this.hubClient = client;
            this.console = console;
        }

        public bool Execute(ActionModule actionModule)
        {
            try
            {
                
                var action = JsonConvert.DeserializeObject<MemberAction>(actionModule.Message);

                if (action.Member != null)
                {
                    this.console.Write($"操作成功:{JsonConvert.SerializeObject(action.Member)}\n");
                }
                else
                {
                    this.console.Write($"操作成功:{JsonConvert.SerializeObject(action.Members)}\n");
                }

                this.hubClient.UnlockProcess();

                return true;
            }
            catch (Exception ex)
            {
                this.console.Clear();
                this.console.WriteLine(ex.Message);
                this.console.Read();

                return false;
            }
        }
    }
}
