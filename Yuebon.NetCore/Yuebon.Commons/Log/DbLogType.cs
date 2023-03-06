using System.ComponentModel;

namespace Yuebon.Commons.Log
{
    /// <summary>
    /// 日志類型
    /// </summary>
    public enum DbLogType
    {
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 0,
        /// <summary>
        /// 登錄
        /// </summary>
        [Description("登錄")]
        Login = 1,
        /// <summary>
        /// 退出
        /// </summary>
        [Description("退出")]
        Exit = 2,
        /// <summary>
        /// 訪問
        /// </summary>
        [Description("訪問")]
        Visit = 3,
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Create = 4,
        /// <summary>
        /// 刪除
        /// </summary>
        [Description("刪除")]
        Delete = 5,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Update = 6,
        /// <summary>
        /// 提交
        /// </summary>
        [Description("提交")]
        Submit = 7,
        /// <summary>
        /// 異常
        /// </summary>
        [Description("異常")]
        Exception = 8,
        /// <summary>
        /// 軟刪除
        /// </summary>
        [Description("軟刪除")]
        DeleteSoft = 9,
        /// <summary>
        /// 軟刪除
        /// </summary>
        [Description("SQL語句")]
        SQL = 10,
    }
}
