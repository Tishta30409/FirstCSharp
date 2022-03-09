namespace FirstCSharp.Signalr.Server.Action
{
    using FirstCSharp.Domain.KeepAliveConn;
    public class AddMemberAction : ActionBase
    {
        public override string Action()
            => "addMember";

        public int MemberID { get; set; }

        public string MemberName { get; set; }

        public decimal MemberPrice { get; set; }

        public string MemberDescript { get; set; }
    }
}
