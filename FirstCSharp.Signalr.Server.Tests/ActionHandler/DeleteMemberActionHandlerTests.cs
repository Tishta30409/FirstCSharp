using FirstCSharp.Domain.KeepAliveConn;
using FirstCSharp.Domain.Model;
using FirstCSharp.Domain.Repository;
using FirstCSharp.Signalr.Server.Action;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FirstCSharp.Signalr.Server.Tests.ActionHandler
{
    [TestClass]
    public class DeleteMemberActionHandlerTests
    {
        [TestMethod]
        public void 刪除Member測試()
        {
            var repo = new Mock<IMemberRepository>();

            repo.Setup(p => p.Delete(1))
                .Returns((null, new Member()
                {
                    f_id = 1,
                    f_name = "test000",
                    f_price = 80,
                    f_descript = "des000"
                }));

            var handler = new DeleteMemberActionHandler(repo.Object);

            var result = handler.ExecuteAction(new ActionModule()
            {
                Message = new DeleteMemberAction()
                {
                    MemberID = 1,
                }.ToString()
            });

            Assert.IsNull(result.exception);
            Assert.IsNotNull(result.actionBase);
        }
    }
}
