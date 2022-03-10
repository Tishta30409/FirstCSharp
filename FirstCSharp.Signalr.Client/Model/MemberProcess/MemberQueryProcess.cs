using FirstCSharp.Domain.Action;
using FirstCSharp.Domain.Model;
using FirstCSharp.Domain.Model.ConsoleWrapper;
using FirstCSharp.Domain.Model.MemberProcess;
using FirstCSharp.Signalr.Client.Hubs;
using FirstCSharp.Signalr.Server.Action;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCSharp.Signalr.Client.Model.MemberProcess
{
    public class MemberQueryProcess: IMemberProcess
    {
        private IHubClient hubClient;

        private IConsoleWrapper console;

        public MemberQueryProcess(IHubClient hubClient, IConsoleWrapper console)
        {
            this.hubClient = hubClient;
            this.console = console;
        }

        public bool Execute()
        {
            try
            {
                this.console.Clear();
                console.Write("Execute  MemberQueryProcess\n");

                console.Write("查詢結果為::\n");

                this.hubClient.SendAction(new QueryMembersAction() { });

                //var queryResult = this.hubClient.GetAction(new QueryMembersAction() { }).Result;

                //if(queryResult == null)
                //{
                //    throw new Exception($"{this.GetType()} queryResult is Empty");
                //}

                //var members = JsonConvert.DeserializeObject<MemberAction>(queryResult.Message).Members;

                //foreach (var member in members)
                //{
                //    console.Write($"{JsonConvert.SerializeObject(member)}\n");
                //}

                console.Read();
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
