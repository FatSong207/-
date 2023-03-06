using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 行政工作日誌明細，數據實體對象
    /// </summary>
    [Table("Chaochi_AdministrativeWorkLogDetails")]
    [Serializable]
    public class AdministrativeWorkLogDetails:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string AdministrativeWorkLogId { get; set; }
        /// <summary>
        /// 設置或獲取本日重點工作事項
        /// </summary>
        public string Important { get; set; }
        /// <summary>
        /// 設置或獲取完成度
        /// </summary>
        public string FinishState { get; set; }
        /// <summary>
        /// 設置或獲取備註
        /// </summary>
        public string Note { get; set; }
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
