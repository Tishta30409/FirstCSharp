using FirstCSharp.Domain.Action;
using FirstCSharp.Domain.Model.ConsoleWrapper;
using FirstCSharp.Domain.Model.MemberProcess;
using FirstCSharp.Signalr.Client.Hubs;
using FirstCSharp.Signalr.Server.Action;
using Newtonsoft.Json;
using System;

namespace FirstCSharp.Signalr.Client.Model.MemberProcess
{
    public class MemberAddProcess : IMemberProcess
    {
        private IHubClient client;

        private IConsoleWrapper console;

        public MemberAddProcess(IHubClient client, IConsoleWrapper console)
        {
            this.client = client;
            this.console = console;
        }

        public bool Execute()
        {
            try
            {
                this.console.Clear();

                console.Write("Execute  MemberAddProcess\n");

                var memberName = string.Empty;

                while (string.IsNullOrEmpty(memberName))
                {
                    this.console.Clear();
                    this.console.Write("會員名稱:");
                    memberName = this.console.ReadLine();
                }

                decimal memberPrice = -1;

                console.Write("會員點數:");
                while (!decimal.TryParse(this.console.ReadLine(), out memberPrice))
                {
                    console.Write("會員點數:");
                }


                console.Write("會員描述:");
                string memberDes = "";
                memberDes = this.console.ReadLine();


                //this.client.SendAction(new AddMemberAction()
                //{
                //    MemberID = -1,
                //    MemberName = memberName,
                //    MemberPrice = memberPrice,
                //    MemberDescript = memberDes
                //});

                var result  = this.client.GetAction(new AddMemberAction()
                {
                    MemberID = -1,
                    MemberName = memberName,
                    MemberPrice = memberPrice,
                    MemberDescript = memberDes
                }).Result;

                if (result == null)
                {
                    throw new Exception($"{this.GetType().Name} MemberAddProcess Empty");
                }

                var member = JsonConvert.DeserializeObject<MemberAction>(result.Message).Member;

                console.Write(JsonConvert.SerializeObject(member));

                this.console.WriteLine("SendAction AddMemberAction");

                console.Write("Execute  MemberAddProcess End\n");
                console.ReadLine();
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
