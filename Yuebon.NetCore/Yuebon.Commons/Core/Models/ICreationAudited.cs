using System;

namespace Yuebon.Commons.Models
{
    /// <summary>
    /// 定義創建審計信息：給實體添加創建時的 創建人CreatorUserId，創建時間CreatorTime 的審計信息，這些值將在數據層執行 創建Insert 時自動賦值。
    /// </summary>
    public interface ICreationAudited
    {

        /// <summary>
        /// 獲取或設置 創建日期
        /// </summary>
        DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 獲取或設置 創建用戶主鍵
        /// </summary>
        string CreatorUserId { get; set; }
    }
}