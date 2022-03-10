using FirstCSharp.Api.Server.Controller;
using FirstCSharp.Domain.Model;
using FirstCSharp.Domain.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Net;

namespace FirstCSharp.Api.Server.Tests
{
    [TestClass]
    public class MemberControllerTests
    {
        [TestMethod]
        public void PostTest()
        {
            var repo = new Mock<IMemberRepository>();

            repo.Setup(p => p.Insert(It.IsAny<Member>()))
                .Returns((null, new Member()
                {
                    f_id = 1,
                    f_name = "TEST001",
                    f_price = 1000,
                    f_descript = "TEST002"
                }));

            var controller = new MemberController(repo.Object);
            var postResult = controller.Post(new MemberAddDto()
            {
                MemberID = 1,
                MemberName = "TEST001",
                MemberPrice = 1000,
                MemberDescrip = "TEST002"
            });

            var result = JsonConvert.DeserializeObject<Member>(postResult.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(postResult.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(result);
        }
    }
}
