using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約匯款帳號維護輸出對象模型
    /// </summary>
    [Serializable]
    public class ContractRemittanceOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取類型(社宅、一般宅..)
        /// </summary>
        [MaxLength(100)]
        public string Type { get; set; }
        /// <summary>
        /// 設置或獲取戶名
        /// </summary>
        [MaxLength(100)]
        public string AccountName { get; set; }
        /// <summary>
        /// 設置或獲取使用單位
        /// </summary>
        [MaxLength(100)]
        public string UseCounty { get; set; }
        /// <summary>
        /// 設置或獲取銀行名稱
        /// </summary>
        [MaxLength(100)]
        public string BankName { get; set; }
        /// <summary>
        /// 設置或獲取銀行代碼
        /// </summary>
        [MaxLength(100)]
        public string BankNo { get; set; }
        /// <summary>
        /// 設置或獲取分行名稱
        /// </summary>
        [MaxLength(100)]
        public string BranchName { get; set; }
        /// <summary>
        /// 設置或獲取分行代碼
        /// </summary>
        [MaxLength(100)]
        public string BranchNo { get; set; }

    }
}
