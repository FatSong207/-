using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 業務拜訪記錄輸出對象模型
    /// </summary>
    [Serializable]
    public class VisitingRecordOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取潛在客ID
        /// </summary>
        [MaxLength(100)]
        public string PID { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string VisitTime { get; set; }
        /// <summary>
        /// 設置或獲取承租人當前狀態
        /// </summary>
        [MaxLength(20)]
        public string Status { get; set; }

        /// <summary>
        /// 設置或獲取當前業務
        /// </summary>
        [MaxLength(100)]
        public string Sales { get; set; }

        /// <summary>
        /// 設置或獲取客戶備註
        /// </summary>
        [MaxLength(4000)]
        public string Note { get; set; }
        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取創建用戶ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬組織ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserOrgId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬部門ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserDeptId { get; set; }
        /// <summary>
        /// 設置或獲取刪除標誌
        /// </summary>
        public bool? DeleteMark { get; set; }
        /// <summary>
        /// 設置或獲取刪除時間
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 設置或獲取刪除用戶ID
        /// </summary>
        [MaxLength(500)]
        public string DeleteUserId { get; set; }
        /// <summary>
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取最後修改用戶ID
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 設置或獲取指派業務姓名
        /// </summary>
        [MaxLength(100)]
        public string SalesName { get; set; }

    }
}
