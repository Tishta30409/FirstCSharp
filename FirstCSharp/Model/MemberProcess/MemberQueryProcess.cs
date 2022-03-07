using FirstCSharp.Domain.Model.ConsoleWrapper;
using FirstCSharp.Domain.Model.MemberProcess;
using FirstCSharp.Domain.Repository;
using System;

namespace FirstCSharp.Model.MemberProcess
{
    public class MemberQueryProcess : IMemberProcess
    {
        private IMemberRepository memberRepo;

        private IConsoleWrapper console;

        public MemberQueryProcess(IMemberRepository memberRepo, IConsoleWrapper console)
        {
            this.memberRepo = memberRepo;
            this.console = console;
        }

        public bool Execute()
        {
            try
            {
                this.console.Clear();
                console.Write("Execute  MemberQueryProcess\n");

                console.Write("查詢結果為::\n");
                var queryResult = this.memberRepo.Query();

                foreach(var member in queryResult.members)
                {
                    console.Write($"id:{member.f_id},name:{member.f_name},price:{member.f_price},descript:{member.f_descript}\n");
                }

                console.Write("Execute  MemberQueryProcess End\n");
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
