using FirstCSharp.Domain.Action;
using FirstCSharp.Domain.KeepAliveConn;
using FirstCSharp.Domain.Repository;
using FirstCSharp.Signalr.Server.Model;
using Newtonsoft.Json;
using System;

namespace FirstCSharp.Signalr.Server.Action
{
    public class QueryMembersActionHandler: IActionHandler
    {
        private IMemberRepository repo;

        public QueryMembersActionHandler(IMemberRepository repo)
        {
            this.repo = repo;
        }

        public (Exception exception, ActionBase actionBase) ExecuteAction(ActionModule action)
        {
            try
            {
                var content = JsonConvert.DeserializeObject<QueryMembersAction>(action.Message);
                var queryResult = this.repo.Query();

                if (queryResult.exception != null)
                {
                    throw queryResult.exception;
                }

                var actionResult = new MemberAction()
                {
                    Members = queryResult.members
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
