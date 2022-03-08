namespace FirstCSharp.Persistent.Tests.Repository
{
    using FirstCSharp.Domain.Repository;
    using FirstCSharp.Persistent.Repository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Data.SqlClient;
    using Dapper;
    using FirstCSharp.Domain.Model;
    using System.Linq;

    [TestClass]
    public class MemberRepositoryTests
    {

        private const string connectionString = @"Data Source=localhost;database=FirstCSharp;Integrated Security=True";

        private IMemberRepository repo;

        [TestInitialize]
        public void Init()
        {
            var sqlStr = @"TRUNCATE TABLE t_member";

            using (var cn = new SqlConnection(connectionString))
            {
                cn.Execute(sqlStr);
            }

            this.repo = new MemberRepository(connectionString);
        }

        [TestMethod]
        public void 新增會員測試()
        {
            var addResult = this.repo.Insert(new Member() { f_name = "test000", f_price = 90000, f_descript = "t_des" });

            Assert.IsNull(addResult.exception);
            Assert.IsNotNull(addResult.member);
            Assert.AreEqual(addResult.member.f_name, "test000");
            Assert.AreEqual(addResult.member.f_price, 90000);
            Assert.AreEqual(addResult.member.f_descript, "t_des");
        }

        [TestMethod]
        public void 批次新增會員測試()
        {
            var insertResult = this.repo.BatchInsert(Enumerable.Range(1, 5).Select(index => new Member()
            {
                f_name = $"test{index}",
                f_price = index * 100,
                f_descript = $"des{index}"
            }));

            Assert.IsNull(insertResult.exception);
            Assert.IsNotNull(insertResult.members);
            Assert.AreEqual(insertResult.members.Count(), 5);
        }

        [TestMethod]
        public void 刪除會員測試()
        {
            var addResult = this.repo.Insert(new Member() { f_name = "test000", f_price = 90000, f_descript = "t_des" });

            Assert.IsNull(addResult.exception);
            Assert.IsNotNull(addResult.member);
            Assert.AreEqual(addResult.member.f_name, "test000");
            Assert.AreEqual(addResult.member.f_price, 90000);
            Assert.AreEqual(addResult.member.f_descript, "t_des");


            var deleteResult = this.repo.Delete(1);

            Assert.IsNull(deleteResult.exception);
            Assert.IsNotNull(deleteResult.member);
            Assert.AreEqual(deleteResult.member.f_name, "test000");
            Assert.AreEqual(deleteResult.member.f_price, 90000);
            Assert.AreEqual(deleteResult.member.f_descript, "t_des");
        }

        [TestMethod]
        public void 查詢會員測試()
        {
            var insertResult = this.repo.BatchInsert(Enumerable.Range(1, 100).Select(index => new Member()
            {
                f_name = $"test{index}",
                f_price = index * 100,
                f_descript = $"des{index}"
            }));

            Assert.IsNull(insertResult.exception);
            Assert.IsNotNull(insertResult.members);
            Assert.AreEqual(insertResult.members.Count(), 100);


            var queryResult = this.repo.Query();

            Assert.IsNull(queryResult.exception);
            Assert.IsNotNull(queryResult.members);
            Assert.AreEqual(queryResult.members.Count(), 100);

        }

        [TestMethod]
        public void 更新會員測試()
        {
            var addResult = this.repo.Insert(new Member() { f_name = "test000", f_price = 90000, f_descript = "t_des" });

            Assert.IsNull(addResult.exception);
            Assert.IsNotNull(addResult.member);
            Assert.AreEqual(addResult.member.f_name, "test000");
            Assert.AreEqual(addResult.member.f_price, 90000);
            Assert.AreEqual(addResult.member.f_descript, "t_des");

            var update = this.repo.Update(new Member() {f_id = 1, f_name = "test100", f_price = 50000, f_descript = "t_des_up" });
            Assert.IsNull(update.exception);
            Assert.IsNotNull(update.member);
            Assert.AreEqual(update.member.f_name, "test100");
            Assert.AreEqual(update.member.f_price, 50000);
            Assert.AreEqual(update.member.f_descript, "t_des_up");

        }
    }
}