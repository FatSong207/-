using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 合約相關資料，數據實體對象
    /// </summary>
    [Table("Chaochi_ContractRelevant")]
    [Serializable]
    public class ContractRelevant:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取合約編號
        /// </summary>
        public string CID { get; set; }
        /// <summary>
        /// 設置或獲取承租人租金補助
        /// </summary>
        public string RNRentSUBAFee { get; set; }
        /// <summary>
        /// 設置或獲取建物合約租金
        /// </summary>
        public string B1TaxID { get; set; }
        /// <summary>
        /// 設置或獲取承租人身份資格
        /// </summary>
        public string RNQualify { get; set; }        /// <summary>
        /// 設置或獲取承租人身份資格:一般戶
        /// </summary>
        public string RNQualify1C { get; set; }
        /// <summary>
        /// 設置或獲取承租人身份資格:第一類弱勢戶
        /// </summary>
        public string RNQualify2C { get; set; }
        /// <summary>
        /// 設置或獲取承租人身份資格:第二類弱勢戶
        /// </summary>
        public string RNQualify3C { get; set; }
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
