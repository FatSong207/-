using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 二房東數據實體對象
    /// </summary>
    [Table("Chaochi_SecondLandlord")]
    [Serializable]
    public class SecondLandlord:BaseEntity<string>
    {
        /// <summary>
        /// 設置或獲取統一編號
        /// </summary>
        public string SLID { get; set; }
        /// <summary>
        /// 設置或獲取公司名稱
        /// </summary>
        public string SLName { get; set; }
        /// <summary>
        /// 設置或獲取負責人姓名
        /// </summary>
        public string SLRep { get; set; }
        /// <summary>
        /// 設置或獲取許可字號/登記證字號
        /// </summary>
        public string SLLRNo { get; set; }
        /// <summary>
        /// 設置或獲取營業地址
        /// </summary>
        public string SLAdd { get; set; }
        /// <summary>
        /// 設置或獲取聯絡電話
        /// </summary>
        public string SLTel { get; set; }

    }
}
