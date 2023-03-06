using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class EventCostOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }

        /// <summary>
        /// 類別(成本or 收入)
        /// </summary>
        public virtual string Category { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string EventId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Type { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? Amount { get; set; }

    }
}
