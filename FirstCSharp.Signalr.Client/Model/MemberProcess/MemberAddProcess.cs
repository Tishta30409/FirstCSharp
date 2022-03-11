using FirstCSharp.Domain.Action;
using FirstCSharp.Domain.Model.ConsoleWrapper;
using FirstCSharp.Domain.Model.MemberProcess;
using FirstCSharp.Signalr.Client.Hubs;
using FirstCSharp.Signalr.Server.Action;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace FirstCSharp.Signalr.Client.Model.MemberProcess
{
    public class MemberAddProcess : IMemberProcess
    {
        private IHubClient hubClient;

        private IConsoleWrapper console;

        public MemberAddProcess(IHubClient hubClient, IConsoleWrapper console)
        {
            this.hubClient = hubClient;
            this.console = console;
        }

        public bool Execute()
        {
            try
            {
                this.console.Clear();

                //console.Write("Execute  MemberAddProcess\n");

                //var memberName = string.Empty;

                //while (string.IsNullOrEmpty(memberName))
                //{
                //    this.console.Clear();
                //    this.console.Write("會員名稱:");
                //    memberName = this.console.ReadLine();
                //}

                //decimal memberPrice = -1;

                //console.Write("會員點數:");
                //while (!decimal.TryParse(this.console.ReadLine(), out memberPrice))
                //{
                //    console.Write("會員點數:");
                //}


                //console.Write("會員描述:");
                //string memberDes = "";
                //memberDes = this.console.ReadLine();

                var memberName = "testN";
                decimal memberPrice = 10000;
                string memberDes = "desTest";
                console.WriteLine($"{memberName} {memberPrice} {memberDes}");


                this.hubClient.SendAction(new AddMemberAction()
                {
                    MemberID = -1,
                    MemberName = memberName,
                    MemberPrice = memberPrice,
                    MemberDescript = memberDes
                });


                var second = 0;
                while (!SpinWait.SpinUntil(() => false, 1000) && this.hubClient.GetProcessState() && second <5)
                {
                    if (!this.hubClient.GetProcessState())
                    {
                        break;
                    }
                    console.WriteLine("等待執行結果...");
                    second += 1;
                }

                if (this.hubClient.GetProcessState())
                {
                    console.WriteLine("處理逾時..");
                    this.hubClient.UnlockProcess();
                }
                else
                {
                    console.WriteLine("處理完成..");
                }

                this.console.Read();
                return false;


                //var result  = this.client.GetAction(new AddMemberAction()
                //{
                //    MemberID = -1,
                //    MemberName = memberName,
                //    MemberPrice = memberPrice,
                //    MemberDescript = memberDes
                //}).Result;

                //if (result == null)
                //{
                //    throw new Exception($"{this.GetType().Name} MemberAddProcess Empty");
                //}

                //var member = JsonConvert.DeserializeObject<MemberAction>(result.Message).Member;

                //console.Write(JsonConvert.SerializeObject(member));


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
