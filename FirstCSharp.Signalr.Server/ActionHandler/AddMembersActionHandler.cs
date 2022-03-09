using FirstCSharp.Domain.Action;
using FirstCSharp.Domain.KeepAliveConn;
using FirstCSharp.Domain.Model;
using FirstCSharp.Domain.Repository;
using FirstCSharp.Signalr.Server.Model;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace FirstCSharp.Signalr.Server.Action
{
    public class BatchAddMemberActionHandler: IActionHandler
    {

        private IMemberRepository repo;

        public BatchAddMemberActionHandler(IMemberRepository repo)
        {
            this.repo = repo;
        }

        public (Exception exception, ActionBase actionBase) ExecuteAction(ActionModule action)
        {
            try
            {
                var content = JsonConvert.DeserializeObject<AddMembersAction>(action.Message);

                var insertResult = this.repo.BatchInsert(Enumerable.Range(1, 5).Select(index => new Member()
                {
                    f_name = $"test{index}",
                    f_price = index * 100,
                    f_descript = $"des{index}"
                }));


                if (insertResult.exception != null)
                {
                    throw insertResult.exception;
                }

                var actionResult = new MemberAction()
                {
                    Members = insertResult.members
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
