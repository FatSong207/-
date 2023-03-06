using System;

namespace Yuebon.Commons.Models
{
    /// <summary>
    ///  定義邏輯刪除功能審計信息
    /// </summary>
    public interface IDeleteAudited 
    {
        /// <summary>
        /// 獲取或設置 邏輯刪除標記
        /// </summary>
        bool? DeleteMark { get; set; }

        /// <summary>
        /// 獲取或設置 刪除實體的用戶
        /// </summary>
        string DeleteUserId { get; set; }

        /// <summary>
        /// 獲取或設置 刪除實體時間
        /// </summary>
        DateTime? DeleteTime { get; set; } 
    }
}