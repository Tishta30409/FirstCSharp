using FirstCSharp.Domain.KeepAliveConn;
using FirstCSharp.Domain.Model;
using FirstCSharp.Domain.Repository;
using FirstCSharp.Signalr.Server.Action;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace FirstCSharp.Signalr.Server.Tests.ActionHandler
{
    [TestClass]
    public class QueryMembersActionHandlerTests
    {
        [TestMethod]
        public void 取得Members資料()
        {
            var repo = new Mock<IMemberRepository>();

            repo.Setup(p => p.Query())
                .Returns((null, Enumerable.Range(1, 3).Select(index => new Member()
                {
                    f_id = index + 1,
                    f_name = $"{index + 1}",
                    f_price = (index + 1) * 10,
                    f_descript = $"des {index + 1}"
                })));

            var handler = new QueryMembersActionHandler(repo.Object);

            var result = handler.ExecuteAction(new ActionModule()
            {
                Message = new QueryMembersAction()
                {}.ToString()
            });

            Assert.IsNull(result.exception);
            Assert.IsNotNull(result.actionBase);
        }
    }
}
