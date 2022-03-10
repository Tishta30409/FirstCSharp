using FirstCSharp.Domain.KeepAliveConn;
using FirstCSharp.Domain.Model;
using FirstCSharp.Domain.Repository;
using FirstCSharp.Signalr.Server.Action;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstCSharp.Signalr.Server.Tests.ActionHandler
{
    /// <summary>
    /// UnitTest1 的摘要說明
    /// </summary>
    [TestClass]
    public class AddMembersActionHandlerTests
    {
        #region 其他測試屬性
        //
        // 您可以使用下列其他屬性撰寫測試: 
        //
        // 執行該類別中第一項測試前，使用 ClassInitialize 執行程式碼
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在類別中的所有測試執行後，使用 ClassCleanup 執行程式碼
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在執行每一項測試之前，先使用 TestInitialize 執行程式碼 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在執行每一項測試之後，使用 TestCleanup 執行程式碼
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void 批次新增測試()
        {
            var repo = new Mock<IMemberRepository>();

            repo.Setup(p => p.BatchInsert(It.IsAny<IEnumerable<Member>>()))
                .Returns((null, Enumerable.Range(1, 100).Select(index => new Member()
                {
                    f_name = $"test{index}",
                    f_price = index * 100,
                    f_descript = $"des{index}"
                })));

            var reporResult =  Enumerable.Range(1, 100).Select(index => new Member()
            {
                f_name = $"test{index}",
                f_price = index * 100,
                f_descript = $"des{index}"
            });

            var handler = new AddMembersActionHandler(repo.Object);
            var result = handler.ExecuteAction(new ActionModule()
            {
                Message = new AddMembersAction()
                {
                    Members = new MembersAddDto()
                    {
                        Members = reporResult
                    }
                }.ToString()
            }
            ) ;

            Assert.IsNull(result.exception);
            Assert.IsNotNull(result.actionBase);
            //
            // TODO:  在此加入測試邏輯
            //
        }
    }
}
