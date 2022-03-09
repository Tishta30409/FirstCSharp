using FirstCSharp.Domain.KeepAliveConn;
using FirstCSharp.Domain.Model;
using System.Collections.Generic;

namespace FirstCSharp.Domain.Action
{
    public class MemberAction : ActionBase
    {
        public override string Action()
            => "member";
        
        public Member Member { get; set; }

        public IEnumerable<Member> Members { get; set; }
    }
}
