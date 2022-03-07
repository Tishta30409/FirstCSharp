using FirstCSharp.Domain.Model.ConsoleWrapper;
using FirstCSharp.Domain.Model.MemberProcess;
using FirstCSharp.Domain.Repository;

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
            console.Write("Execute  MemberQueryProcess");
            return true;
        }
    }
}
