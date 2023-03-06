using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 合約審核記錄，數據實體對象
    /// </summary>
    [Table("Chaochi_ContractFlowLog")]
    [Serializable]
    public class ContractFlowLog:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取審核單號
        /// </summary>
        public string CID { get; set; }
        /// <summary>
        /// 設置或獲取合約狀態
        /// </summary>
        public string CStatus { get; set; }        /// <summary>
        /// 設置或獲取送審者
        /// </summary>
        public string Applicant { get; set; }
        /// <summary>
        /// 設置或獲取審核者
        /// </summary>
        public string Auditor { get; set; }
        /// <summary>
        /// 設置或獲取關卡動作(同意/退回)
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 設置或獲取審核恴見
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 設置或獲取送出審核時間
        /// </summary>
        public DateTime? ApplyTime { get; set; }
        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取創建用戶ID
        /// </summary>
        public string CreatorUserId { get; set; }
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
