
namespace FirstCSharp.Model.MemberProcess
{
    using FirstCSharp.Domain.Model.ConsoleWrapper;
    using FirstCSharp.Domain.Model.MemberProcess;
    using FirstCSharp.Domain.Repository;


    public class MemberAddProcess : IMemberProcess
    {
        //private IMemberRepository memberRepo;

        private IConsoleWrapper console;

        public MemberAddProcess(IConsoleWrapper console)
        {
            //this.memberRepo = memberRepo;
            this.console = console;
        }

        public bool Execute()
        {
            console.Write("Execute  MemberAddProcess");
            return true;
        }
    }
}
