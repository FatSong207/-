using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 合約歷史版本數據實體對象
    /// </summary>
    [Table("Chaochi_HistoryFormContract")]
    [Serializable]
    public class HistoryFormContract:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取合約編號
        /// </summary>
        public string CID { get; set; }
        /// <summary>
        /// 設置或獲取版號
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 設置或獲取合約名稱
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// 設置或獲取產生日期
        /// </summary>
        public DateTime? UploadTime { get; set; }

        /// <summary>
        /// 設置或獲取產生人員
        /// </summary>
        public string UploadUser { get; set; }
        /// <summary>
        /// 設置或獲取備註
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 設置或獲取定稿合約路徑
        /// </summary>
        public string UnsignPDFPath { get; set; }
        /// <summary>
        /// 設置或獲取簽約掃描檔路徑
        /// </summary>
        public string SignedPDFPath { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 最后修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用戶
        /// </summary>
        [MaxLength(50)]
        public string LastModifyUserId { get; set; }

    }
}
