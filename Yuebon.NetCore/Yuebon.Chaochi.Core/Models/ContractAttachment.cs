using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 合約附件數據實體對象
    /// </summary>
    [Table("Chaochi_ContractAttachment")]
    [Serializable]
    public class ContractAttachment:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取合約編號
        /// </summary>
        public string CID { get; set; }
        /// <summary>
        /// 設置或獲取要件名稱
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 設置或獲取表單歸屬
        /// </summary>
        public string ArchiveTo { get; set; }

        /// <summary>
        /// 設置或獲取表單歸屬
        /// </summary>
        public string ArchiveID { get; set; }
        /// <summary>
        /// 設置或獲取附件狀況
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 設置或獲取綁定日期
        /// </summary>
        public DateTime? UploadTime { get; set; }

        /// <summary>
        /// 設置或獲取異動人員
        /// </summary>
        public string UploadUserId { get; set; }

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
