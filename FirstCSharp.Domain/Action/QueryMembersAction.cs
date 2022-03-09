using FirstCSharp.Domain.KeepAliveConn;

namespace FirstCSharp.Signalr.Server.Action
{
    public class QueryMembersAction:ActionBase
    {
        public override string Action()
            => "queryMembers";
    }
}
