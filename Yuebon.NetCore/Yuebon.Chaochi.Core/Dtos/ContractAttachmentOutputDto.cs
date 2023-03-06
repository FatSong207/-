using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約附件輸出對象模型
    /// </summary>
    [Serializable]
    public class ContractAttachmentOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取合約編號
        /// </summary>
        [MaxLength(100)]
        public string CID { get; set; }
        /// <summary>
        /// 設置或獲取要件名稱
        /// </summary>
        [MaxLength(200)]
        public string FormName { get; set; }
        /// <summary>
        /// 設置或獲取表單歸屬
        /// </summary>
        [MaxLength(40)]
        public string ArchiveTo { get; set; }

        /// <summary>
        /// 設置或獲取表單歸屬ID
        /// </summary>
        [MaxLength(100)]
        public string ArchiveID { get; set; }
        /// <summary>
        /// 設置或獲取附件狀況
        /// </summary>
        [MaxLength(30)]
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
