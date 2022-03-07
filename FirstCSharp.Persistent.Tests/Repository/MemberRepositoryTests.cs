namespace FirstCSharp.Persistent.Tests.Repository
{
    using FirstCSharp.Domain.Repository;
    using FirstCSharp.Persistent.Repository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Data.SqlClient;
    using Dapper;
    using FirstCSharp.Domain.Model;

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
        public void 新增分數測試()
        {
            var addResult = this.repo.Insert(new Member() { f_name = "test000", f_price = 90000, f_descript = "t_des" });

            Assert.IsNull(addResult.exception);
            Assert.IsNotNull(addResult.member);
            Assert.AreEqual(addResult.member.f_name, "test000");
            Assert.AreEqual(addResult.member.f_price, 90000);
            Assert.AreEqual(addResult.member.f_descript, "t_des");
        }
    }
}