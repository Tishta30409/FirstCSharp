using FirstCSharp.Domain.Model.ConsoleWrapper;
using FirstCSharp.Domain.Model.MemberProcess;
using FirstCSharp.Domain.Repository;

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
            console.Write("Execute  MemberBatchAddProcess");
            return true;
        }
    }
}
