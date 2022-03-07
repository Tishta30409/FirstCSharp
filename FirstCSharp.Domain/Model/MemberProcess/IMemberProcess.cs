

namespace FirstCSharp.Domain.Model.MemberProcess
{
    public enum MemberProcessType
    {
        [EnumDisplay("1. 新增會員資料")]
        MemberAdd = 1,
        [EnumDisplay("2. 讀取會員資料")]
        MemberQuery = 2,
        [EnumDisplay("3. 更新會員資料")]
        MemberUpdate = 3,
        [EnumDisplay("4. 刪除會員資料")]
        MemberDelete = 4,
        [EnumDisplay("5. 批次新增會員資料")]
        MemberBatchAdd = 5
    }

    public interface IMemberProcess
    {
        bool Execute();
    }
}
