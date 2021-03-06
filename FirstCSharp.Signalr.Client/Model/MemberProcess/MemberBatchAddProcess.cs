using FirstCSharp.Domain.Action;
using FirstCSharp.Domain.Model;
using FirstCSharp.Domain.Model.ConsoleWrapper;
using FirstCSharp.Domain.Model.MemberProcess;
using FirstCSharp.Signalr.Client.Hubs;
using FirstCSharp.Signalr.Server.Action;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;

namespace FirstCSharp.Signalr.Client.Model.MemberProcess
{
    public class MemberBatchAddProcess: IMemberProcess
    {
        private IHubClient hubClient;

        private IConsoleWrapper console;

        public MemberBatchAddProcess(IHubClient hubClient, IConsoleWrapper console)
        {
            this.hubClient = hubClient;
            this.console = console;
        }

        public bool Execute()
        {
            try
            {
                console.Clear();
                console.Write("Execute  MemberBatchAddProcess\n");

                this.hubClient.SendAction(new AddMembersAction()
                {
                    Members = new MembersAddDto()
                    {
                        Members = Enumerable.Range(1, 100).Select(index => new Member()
                        {
                            f_name = $"test{index}",
                            f_price = index * 100,
                            f_descript = $"des{index}"
                        })
                    }
                });

                var second = 0;
                while (!SpinWait.SpinUntil(() => false, 1000) && this.hubClient.GetProcessState() && second < 5)
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

                //var batchResult = this.hubClient.GetAction(new AddMembersAction()
                //{
                //    Members = new MembersAddDto()
                //    {
                //        Members = Enumerable.Range(1, 100).Select(index => new Member()
                //        {
                //            f_name = $"test{index}",
                //            f_price = index * 100,
                //            f_descript = $"des{index}"
                //        })
                //    }
                //}).Result;

                //if (batchResult == null)
                //{
                //    throw new Exception($"{this.GetType().Name} MembersList Empty");
                //}

                //var memberAction = JsonConvert.DeserializeObject<MemberAction>(batchResult.Message);

                //foreach (Member member in memberAction.Members)
                //{
                //    console.WriteLine($"{JsonConvert.SerializeObject(member)}\n");
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
