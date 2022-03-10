using FirstCSharp.Domain.KeepAliveConn;
using FirstCSharp.Domain.Model;
using System.Collections.Generic;

namespace FirstCSharp.Domain.Action
{
    public class MemberAction : ActionBase
    {
        public override string Action()
            => "member";

        //連線標示
        public int PackageNum { get; set; }

        //單一資料
        public Member Member { get; set; }

        //負數資料
        public IEnumerable<Member> Members { get; set; }
    }
}
