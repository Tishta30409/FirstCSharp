using FirstCSharp.Domain.Action;
using FirstCSharp.Domain.KeepAliveConn;
using FirstCSharp.Domain.Model;
using FirstCSharp.Domain.Repository;
using FirstCSharp.Signalr.Server.Model;
using Newtonsoft.Json;
using System;

namespace FirstCSharp.Signalr.Server.Action
{
    public class UpdateMembersActionHandle : IActionHandler
    {
        private IMemberRepository repo;

        public UpdateMembersActionHandle(IMemberRepository repo)
        {
            this.repo = repo;
        }

        public (Exception exception, ActionBase actionBase) ExecuteAction(ActionModule action)
        {
            try
            {
                var content = JsonConvert.DeserializeObject<AddMemberAction>(action.Message);
                var updateResult = this.repo.Update(new Member()
                {
                    f_id = content.MemberID,
                    f_name = content.MemberName,
                    f_price = content.MemberPrice,
                    f_descript = content.MemberDescript
                });

                if (updateResult.exception != null)
                {
                    throw updateResult.exception;
                }

                var actionResult = new MemberAction()
                {
                    Member = updateResult.member
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
