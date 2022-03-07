

namespace FirstCSharp.Model.MemberProcess
{
    using FirstCSharp.Domain.Model.ConsoleWrapper;
    using FirstCSharp.Domain.Model.MemberProcess;
    using FirstCSharp.Domain.Repository;

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
            console.Write("Execute  MemberUpdateProcess");
            return true;
        }
    }
}
