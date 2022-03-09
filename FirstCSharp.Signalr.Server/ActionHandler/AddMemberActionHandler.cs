using FirstCSharp.Domain.Action;
using FirstCSharp.Domain.KeepAliveConn;
using FirstCSharp.Domain.Model;
using FirstCSharp.Domain.Repository;
using FirstCSharp.Signalr.Server.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCSharp.Signalr.Server.Action
{
    public class AddMemberActionHandler: IActionHandler
    {
        private IMemberRepository repo;

        public AddMemberActionHandler(IMemberRepository repo)
        {
            this.repo = repo;
        }

        public (Exception exception, ActionBase actionBase) ExecuteAction(ActionModule action)
        {
            try
            {
                var content = JsonConvert.DeserializeObject<AddMemberAction>(action.Message);
                var addResult = this.repo.Insert(new Member()
                {
                    f_name = content.MemberName,
                    f_price = content.MemberPrice,
                    f_descript = content.MemberDescript
                });

                if (addResult.exception != null)
                {
                    throw addResult.exception;
                }

                var actionResult = new MemberAction()
                {
                    Member = addResult.member
                };

                return (null, actionResult);
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }
    }
}
