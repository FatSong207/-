using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 業務拜訪記錄，數據實體對象
    /// </summary>
    [Table("Chaochi_VisitingRecord")]
    [Serializable]
    public class VisitingRecord:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 設置或獲取潛在客ID
        /// </summary>
        public string PID { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string VisitTime { get; set; }

        /// <summary>
        /// 設置或獲取當前狀態
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 設置或獲取當前業務
        /// </summary>
        public string Sales { get; set; }

        /// <summary>
        /// 設置或獲取客戶備註
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 設置或獲取創建用戶ID
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取創建人所屬組織ID
        /// </summary>
        public string CreatorUserOrgId { get; set; }

        /// <summary>
        /// 設置或獲取創建人所屬部門ID
        /// </summary>
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
        public string DeleteUserId { get; set; }

        /// <summary>
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 設置或獲取最後修改用戶ID
        /// </summary>
        public string LastModifyUserId { get; set; }


    }
}
