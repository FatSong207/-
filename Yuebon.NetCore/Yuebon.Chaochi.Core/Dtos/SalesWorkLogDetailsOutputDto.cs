using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 業務工作日誌明細輸出對象模型
    /// </summary>
    [Serializable]
    public class SalesWorkLogDetailsOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string SalesWorkLogId { get; set; }
        /// <summary>
        /// 設置或獲取品項
        /// </summary>
        [MaxLength(100)]
        public string Item { get; set; }
        /// <summary>
        /// 設置或獲取型態
        /// </summary>
        [MaxLength(100)]
        public string Type { get; set; }
        /// <summary>
        /// 設置或獲取業績額
        /// </summary>
        [MaxLength(100)]
        public string Performance { get; set; }
        /// <summary>
        /// 設置或獲取地址
        /// </summary>
        [MaxLength(100)]
        public string Address { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }

    }
}
