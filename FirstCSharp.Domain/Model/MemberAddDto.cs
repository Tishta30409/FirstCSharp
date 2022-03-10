using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCSharp.Domain.Model
{
    public class MemberAddDto
    {
        public int MemberID { get; set; }

        public string MemberName { get; set; }

        public decimal MemberPrice { get; set; }

        public string MemberDescrip { get; set; }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
