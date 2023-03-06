using System;

namespace Yuebon.Commons.Models
{
    /// <summary>
    /// 定義更新審計的信息
    /// </summary>
    public interface IModificationAudited
    {
        /// <summary>
        /// 獲取或設置 最后修改用戶
        /// </summary>
        string LastModifyUserId { get; set; }
        /// <summary>
        /// 獲取或設置 最后修改時間
        /// </summary>
        DateTime? LastModifyTime { get; set; }
    }
}