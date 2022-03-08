using FirstCSharp.Domain.Model;
using System;
using System.Collections.Generic;

namespace FirstCSharp.Domain.Repository
{
    public interface IMemberRepository
    {


        /// <summary>
        /// 批次新增分數
        /// </summary>
        /// <param name="scores"></param>
        /// <returns></returns>
        (Exception exception, Member member) Insert(Member member);

        /// <summary>
        /// 批次新增分數
        /// </summary>
        /// <param name="scores"></param>
        /// <returns></returns>
        (Exception exception, IEnumerable<Member> members) BatchInsert(IEnumerable<Member> members);

        

        /// <summary>
        /// 取得所有分數
        /// </summary>
        /// <param name="sujectId"></param>
        /// <returns></returns>
        (Exception exception, IEnumerable<Member> members) Query();

        /// <summary>
        /// 新增分數
        /// </summary>
        /// <param name="sujectId"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        (Exception exception, Member member) Update(Member member);

        /// <summary>
        /// 移除分數
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        (Exception exception, Member member) Delete(int id);
    }
}
