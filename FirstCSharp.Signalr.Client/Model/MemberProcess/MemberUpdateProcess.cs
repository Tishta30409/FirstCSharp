using FirstCSharp.Domain.Action;
using FirstCSharp.Domain.Model;
using FirstCSharp.Domain.Model.ConsoleWrapper;
using FirstCSharp.Domain.Model.MemberProcess;
using FirstCSharp.Signalr.Client.Hubs;
using FirstCSharp.Signalr.Server.Action;
using Newtonsoft.Json;
using System;

namespace FirstCSharp.Signalr.Client.Model.MemberProcess
{
    public class MemberUpdateProcess: IMemberProcess
    {
        private IHubClient hubClient;

        private IConsoleWrapper console;

        public MemberUpdateProcess(IHubClient hubClient, IConsoleWrapper console)
        {
            this.hubClient = hubClient;
            this.console = console;
        }

        public bool Execute()
        {
            try
            {
                this.console.Clear();
                console.Write("Execute  MemberUpdateProcess\n");

                var memberID = int.MinValue;

                console.Write("會員編號:");
                while (!int.TryParse(this.console.ReadLine(), out memberID))
                {
                    console.Write("會員編號:");
                }

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


                var updateResult = this.hubClient.GetAction(new UpdateMemberAction()
                {
                    MemberID = memberID,
                    MemberName = memberName,
                    MemberPrice = memberPrice,
                    MemberDescript = memberDes
                }).Result;

                if(updateResult == null)
                {
                    throw new Exception($"{this.GetType()} updateResult is Empty");
                }

                this.console.WriteLine("SendAction AddMemberAction");

                var member = JsonConvert.DeserializeObject<MemberAction>(updateResult.Message).Member;

                this.console.Write($"更新完成 {JsonConvert.SerializeObject(member)}");

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
