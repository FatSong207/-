using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// ，數據實體對象
    /// </summary>
    [Table("Chaochi_Bankinfo")]
    [Serializable]
    public class Bankinfo:BaseEntity<string>
    {
        /// <summary>
        /// 設置或獲取銀行名稱
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 設置或獲取銀行代碼
        /// </summary>
        public string BankNo { get; set; }
        /// <summary>
        /// 設置或獲取分行名稱
        /// </summary>
        public string BranchName { get; set; }
        /// <summary>
        /// 設置或獲取分行代碼
        /// </summary>
        public string BranchNo { get; set; }

    }
}
