using FirstCSharp.Domain.KeepAliveConn;
using FirstCSharp.Domain.Model;
using FirstCSharp.Domain.Repository;
using FirstCSharp.Signalr.Server.Action;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FirstCSharp.Signalr.Server.Tests.ActionHandler
{
    [TestClass]
    public class AddMemberActionHandlerTests
    {
        [TestMethod]
        public void 新增Member測試()
        {
            var repo = new Mock<IMemberRepository>();

            repo.Setup(p => p.Insert(It.IsAny<Member>()))
                .Returns((null, new Member()
                {
                    f_name = "test000",
                    f_price = 80,
                    f_descript = "des000"
                }));

            var handler = new AddMemberActionHandler(repo.Object);

            var result = handler.ExecuteAction(new ActionModule()
            {
                Message = new AddMemberAction()
                {
                    MemberName = "test000",
                    MemberPrice = 80,
                    MemberDescript = "des000"
                }.ToString()
            });

            Assert.IsNull(result.exception);
            Assert.IsNotNull(result.actionBase);
        }
    }
}
