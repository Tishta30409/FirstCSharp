using Dapper;
using FirstCSharp.Domain.Model;
using FirstCSharp.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCSharp.Persistent.Repository
{
    public class MemberRepository : IMemberRepository
    {
        /// <summary>
        /// 連線字串
        /// </summary>
        private string connectionString;

        public MemberRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// 新增分數
        /// </summary>
        /// <param name="sujectId"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// (Exception exception, Member members) Insert(Member member);
        public (Exception exception, Member member) Insert(Member member)
        {
            try
            {
                using (var cn = new SqlConnection(this.connectionString))
                {
                    var result = cn.QueryFirstOrDefault<Member>(
                        "pro_memberAdd",
                        //參數名稱為PROCEDURE中宣告的變數名稱
                        new
                        {
                            @userName = member.f_name,
                            @price = member.f_price,
                            @descript = member.f_descript
                        },
                        commandType: CommandType.StoredProcedure) ;

                    return (null, result);
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        /// <summary>
        /// 批次新增分數
        /// </summary>
        /// <param name="scores"></param>
        /// <returns></returns>
        public (Exception exception, IEnumerable<Member> members) BatchInsert(IEnumerable<Member> members)
        {
            try
            {
                using (var cn = new SqlConnection(this.connectionString))
                {
                    var udt = new DataTable();
                    udt.Columns.Add(nameof(Member.f_name), typeof(string));
                    udt.Columns.Add(nameof(Member.f_price), typeof(decimal));
                    udt.Columns.Add(nameof(Member.f_descript), typeof(string));

                    foreach (var member in members)
                    {
                        var dr = udt.NewRow();
                        dr[nameof(member.f_name)] = member.f_name;
                        dr[nameof(member.f_price)] = member.f_price;
                        dr[nameof(member.f_descript)] = member.f_descript;

                        udt.Rows.Add(dr);
                    }

                    var result = cn.Query<Member>(
                        "pro_memberAddBatch",
                        new
                        {
                            type_member = udt.AsTableValuedParameter("type_member")
                        },
                        commandType: CommandType.StoredProcedure);

                    return (null, result);
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        /// <summary>
        /// 取得所有分數
        /// </summary>
        /// <param name="sujectId"></param>
        /// <returns></returns>
        public (Exception exception, IEnumerable<Member> members) Query()
        {
            try
            {
                using (var cn = new SqlConnection(this.connectionString))
                {
                    var result = cn.Query<Member>(
                        "pro_memberQuery",
                        commandType: CommandType.StoredProcedure);

                    return (null, result);
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        /// <summary>
        /// 更新科目
        /// </summary>
        /// <param name="suject"></param>
        /// <returns></returns>
        public (Exception exception, Member member) Update(Member member)
        {
            try
            {
                using (var cn = new SqlConnection(this.connectionString))
                {
                    var result = cn.QueryFirstOrDefault<Member>(
                        "pro_memberUpdate",
                        new
                        {
                            @id = member.f_id,
                            @userName = member.f_name,
                            @price = member.f_price,
                            @descript = member.f_descript
                        },
                        commandType: CommandType.StoredProcedure);

                    return (null, result);
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        /// <summary>
        /// 移除分數
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public (Exception exception, Member member) Delete(int id)
        {
            try
            {
                using (var cn = new SqlConnection(this.connectionString))
                {
                    var result = cn.QueryFirstOrDefault<Member>(
                        "pro_memberDelete",
                        new
                        {
                            @id = id
                        },
                        commandType: CommandType.StoredProcedure);

                    return (null, result);
                }
            }
            catch (Exception ex)
            {
                return (ex, null);

            }
        }
    }

    



}
