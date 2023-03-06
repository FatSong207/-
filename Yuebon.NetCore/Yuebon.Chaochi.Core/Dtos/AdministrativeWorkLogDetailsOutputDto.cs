using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 行政工作日誌明細輸出對象模型
    /// </summary>
    [Serializable]
    public class AdministrativeWorkLogDetailsOutputDto
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
        public string AdministrativeWorkLogId { get; set; }
        /// <summary>
        /// 設置或獲取本日重點工作事項
        /// </summary>
        [MaxLength(100)]
        public string Important { get; set; }
        /// <summary>
        /// 設置或獲取完成度
        /// </summary>
        [MaxLength(100)]
        public string FinishState { get; set; }
        /// <summary>
        /// 設置或獲取備註
        /// </summary>
        [MaxLength(100)]
        public string Note { get; set; }
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
