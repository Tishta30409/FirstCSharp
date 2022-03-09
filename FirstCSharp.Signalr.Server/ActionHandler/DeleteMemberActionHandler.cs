using FirstCSharp.Domain.Action;
using FirstCSharp.Domain.KeepAliveConn;
using FirstCSharp.Domain.Repository;
using FirstCSharp.Signalr.Server.Model;
using Newtonsoft.Json;
using System;

namespace FirstCSharp.Signalr.Server.Action
{
    public class DeleteMemberActionHandler : IActionHandler
    {
        private IMemberRepository repo;

        public DeleteMemberActionHandler(IMemberRepository repo)
        {
            this.repo = repo;
        }

        public (Exception exception, ActionBase actionBase) ExecuteAction(ActionModule action)
        {
            try
            {
                var content = JsonConvert.DeserializeObject<DeleteMemberAction>(action.Message);
                var delResult = this.repo.Delete(content.MemberID);

                if (delResult.exception != null)
                {
                    throw delResult.exception;
                }

                var actionResult = new MemberAction()
                {
                    Member = delResult.member
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
