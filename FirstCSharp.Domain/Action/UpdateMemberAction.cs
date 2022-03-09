using FirstCSharp.Domain.KeepAliveConn;

namespace FirstCSharp.Signalr.Server.Action
{
    public class UpdateMemberAction : ActionBase
    {
        public override string Action()
           => "updateMember";

        public int MemberID { get; set; }

        public string MemberName { get; set; }

        public decimal MemberPrice { get; set; }

        public string MemberDescript { get; set; }
    }
}
