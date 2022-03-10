using FirstCSharp.Domain.KeepAliveConn;

namespace FirstCSharp.Signalr.Server.Action
{
    public class DeleteMemberAction:ActionBase
    {
        public override string Action()
            => "deleteMember";

        public int MemberID { get; set; }


    }
}
