using FirstCSharp.Domain.Model;
using FirstCSharp.Domain.Model.ConsoleWrapper;
using FirstCSharp.Domain.Model.MemberProcess;
using FirstCSharp.Domain.Repository;
using System;
using System.Linq;

namespace FirstCSharp.Model.MemberProcess
{
    public class MemberBatchAddProcess : IMemberProcess
    {
        private IMemberRepository memberRepo;

        private IConsoleWrapper console;

        public MemberBatchAddProcess(IMemberRepository memberRepo, IConsoleWrapper console)
        {
            this.memberRepo = memberRepo;
            this.console = console;
        }

        public bool Execute()
        {
            try
            {
                console.Clear();
                console.Write("Execute  MemberBatchAddProcess\n");

                var insertResult = this.memberRepo.BatchInsert(Enumerable.Range(1, 100).Select(index => new Member()
                {
                    f_name = $"test{index}",
                    f_price = index * 100,
                    f_descript = $"des{index}"
                }));

                foreach (Member member in insertResult.members)
                {
                    console.WriteLine($"{member.f_id}, {member.f_name}, {member.f_price} {member.f_descript}\n");
                }

                console.Write("Execute  MemberBatchAddProcess End\n");
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
