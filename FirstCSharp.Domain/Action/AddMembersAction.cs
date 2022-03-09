using FirstCSharp.Domain.KeepAliveConn;
using FirstCSharp.Domain.Model;

namespace FirstCSharp.Signalr.Server.Action
{
    public class AddMembersAction:ActionBase
    {
        public override string Action()
           => "addMembers";

        public MembersAddDto Members { get; set; }
    }
}
