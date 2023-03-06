using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約相關資料輸出對象模型
    /// </summary>
    [Serializable]
    public class ContractRelevantOutputDto
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
        /// 設置或獲取承租人租金補助
        /// </summary>
        [MaxLength(100)]
        public string RNRentSUBAFee { get; set; }
        /// <summary>
        /// 設置或獲取建物合約租金
        /// </summary>
        [MaxLength(100)]
        public string B1TaxID { get; set; }
        /// <summary>
        /// 設置或獲取承租人身份資格
        /// </summary>
        [MaxLength(20)]        public string RNQualify { get; set; }        /// <summary>
        /// 設置或獲取承租人身份資格:一般戶
        /// </summary>
        [MaxLength(100)]
        public string RNQualify1C { get; set; }
        /// <summary>
        /// 設置或獲取承租人身份資格:第一類弱勢戶
        /// </summary>
        [MaxLength(100)]
        public string RNQualify2C { get; set; }
        /// <summary>
        /// 設置或獲取承租人身份資格:第二類弱勢戶
        /// </summary>
        [MaxLength(100)]
        public string RNQualify3C { get; set; }
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
