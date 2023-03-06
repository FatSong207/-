using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 業務工作日誌明細，數據實體對象
    /// </summary>
    [Table("Chaochi_SalesWorkLogDetails")]
    [Serializable]
    public class SalesWorkLogDetails:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string SalesWorkLogId { get; set; }
        /// <summary>
        /// 設置或獲取品項
        /// </summary>
        public string Item { get; set; }
        /// <summary>
        /// 設置或獲取型態
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 設置或獲取業績額
        /// </summary>
        public string Performance { get; set; }
        /// <summary>
        /// 設置或獲取地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LastModifyUserId { get; set; }

    }
}
