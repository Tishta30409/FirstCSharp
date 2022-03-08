

namespace FirstCSharp.Model.MemberProcess
{
    using FirstCSharp.Domain.Model;
    using FirstCSharp.Domain.Model.ConsoleWrapper;
    using FirstCSharp.Domain.Model.MemberProcess;
    using FirstCSharp.Domain.Repository;
    using System;

    public class MemberUpdateProcess : IMemberProcess
    {
        private IMemberRepository memberRepo;

        private IConsoleWrapper console;

        public MemberUpdateProcess(IMemberRepository memberRepo, IConsoleWrapper console)
        {
            this.memberRepo = memberRepo;
            this.console = console;
        }

        public bool Execute()
        {
            try
            {
                this.console.Clear();
                console.Write("Execute  MemberUpdateProcess");
                
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


                var getResult = this.memberRepo.Update(new Member()
                {
                    f_id = memberID,
                    f_name = memberName,
                    f_price = memberPrice,
                    f_descript = memberDes
                });

                this.console.Write($"更新成功 id:{getResult.member.f_id}, name:{getResult.member.f_name}, price:{getResult.member.f_price}, descript:{getResult.member.f_descript}\n");

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
