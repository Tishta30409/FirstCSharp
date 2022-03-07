using FirstCSharp.Domain.Model.ConsoleWrapper;
using FirstCSharp.Domain.Model.MemberProcess;
using FirstCSharp.Domain.Repository;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace FirstCSharp.Model.MemberProcess
{
    public class MemberDeleteProcess : IMemberProcess
    {
        private IMemberRepository memberRepo;

        private IConsoleWrapper console;

        public MemberDeleteProcess(IMemberRepository memberRepo, IConsoleWrapper console)
        {
            this.memberRepo = memberRepo;
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
    
                var getMemberResult = this.memberRepo.Query();

                int memberID = -1;
                while (
                    !int.TryParse(this.console.ReadLine(), out memberID) &&
                    !getMemberResult.members.Any(p => p.f_id == memberID))
                {
                    this.console.Clear();
                    this.console.Write("移除ID:");
                }

                var delResult = this.memberRepo.Delete(memberID);

                console.Write($"移除成功 id:{memberID}, member:{JsonConvert.SerializeObject(delResult.member)}");

                //console.Write($"移除成功 id:{delResult.member.f_id},name:{delResult.member.f_name},price:{delResult.member.f_price},des:{delResult.member.f_descript}\n");
 

                console.Write("Execute  MemberDeleteProcess End\n");
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
