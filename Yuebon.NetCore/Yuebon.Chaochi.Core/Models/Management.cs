using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 管理方數據實體對象
    /// </summary>
    [Table("Chaochi_Management")]
    [Serializable]
    public class Management:BaseEntity<string>
    {
        /// <summary>
        /// 設置或獲取統一編號
        /// </summary>
        public string MAID { get; set; }
        /// <summary>
        /// 設置或獲取公司名稱
        /// </summary>
        public string MName { get; set; }
        /// <summary>
        /// 設置或獲取負責人姓名
        /// </summary>
        public string MRep { get; set; }
        /// <summary>
        /// 設置或獲取許可字號/登記證字號
        /// </summary>
        public string MLRNo { get; set; }
        /// <summary>
        /// 設置或獲取營業地址
        /// </summary>
        public string MAdd { get; set; }
        /// <summary>
        /// 設置或獲取聯絡電話
        /// </summary>
        public string MTel { get; set; }
        /// <summary>
        /// 設置或獲取電子郵件信箱
        /// </summary>
        public string MEmail { get; set; }

    }
}
