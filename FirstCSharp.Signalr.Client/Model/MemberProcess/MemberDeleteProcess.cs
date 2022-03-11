using FirstCSharp.Domain.Action;
using FirstCSharp.Domain.Model.ConsoleWrapper;
using FirstCSharp.Domain.Model.MemberProcess;
using FirstCSharp.Signalr.Client.Hubs;
using FirstCSharp.Signalr.Server.Action;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirstCSharp.Signalr.Client.Model.MemberProcess
{
    public class MemberDeleteProcess: IMemberProcess
    {
        private IHubClient hubClient;

        private IConsoleWrapper console;

        public MemberDeleteProcess(IHubClient hubClient, IConsoleWrapper console)
        {
            this.hubClient = hubClient;
            this.console = console;
        }

        public bool Execute()
        {
            try
            {
                this.console.Clear();
                console.Write("Execute  MemberDeleteProcess\n");

                console.Write("刪除會員資料\n");
                console.Write("移除ID:");

                var getMemberResult = this.hubClient.GetAction(new QueryMembersAction() { }).Result;

                if(getMemberResult == null)
                {
                    throw new Exception($"{this.GetType().Name} getMemberResult Empty");
                }


                //取得列表 會先檢查是否有在名單內 如果沒有才新增
                var members = JsonConvert.DeserializeObject<MemberAction>(getMemberResult.Message).Members;

                int memberID = -1;

                while (
                    !int.TryParse(this.console.ReadLine(), out memberID) &&
                    !members.Any(p => p.f_id == memberID))
                {
                    this.console.Clear();
                    this.console.Write("移除ID:");
                }

                this.hubClient.SendAction(new DeleteMemberAction()
                {
                    MemberID = memberID,
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


                //var delResult = this.hubClient.GetAction(new DeleteMemberAction()
                //{
                //    MemberID = memberID,
                //}).Result;


                //if(delResult == null)
                //{
                //    throw new Exception($"{this.GetType().Name} delResult Empty");
                //}

                //var member = JsonConvert.DeserializeObject<MemberAction>(delResult.Message).Member;


                //console.Write($"執行完成:: data::{JsonConvert.SerializeObject(member)}");

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
