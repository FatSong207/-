using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約審核記錄輸出對象模型
    /// </summary>
    [Serializable]
    public class ContractFlowLogOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取審核單號
        /// </summary>
        [MaxLength(100)]
        public string CID { get; set; }

        /// <summary>
        /// 設置或獲取合約狀態
        /// </summary>
        [MaxLength(30)]
        public string CStatus { get; set; }
        /// <summary>
        /// 設置或獲取送審者
        /// </summary>
        [MaxLength(100)]
        public string Applicant { get; set; }
        /// <summary>
        /// 設置或獲取審核者
        /// </summary>
        [MaxLength(100)]
        public string Auditor { get; set; }
        /// <summary>
        /// 設置或獲取關卡動作(同意/退回/修正)
        /// </summary>
        [MaxLength(20)]
        public string Action { get; set; }
        /// <summary>
        /// 設置或獲取審核恴見
        /// </summary>
        [MaxLength(4000)]
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
        [MaxLength(100)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取最後修改用戶ID
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }

    }
}
