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
    [Table("Chaochi_EventCost")]
    [Serializable]
    public class EventCost:BaseEntity<string>
    {
        /// <summary>
        /// 活動Id
        /// </summary>
        public virtual string EventId { get; set; }

        /// <summary>
        /// 類別(成本or 收入)
        /// </summary>
        public virtual string Category { get; set; }


        /// <summary>
        /// 費用類別
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 費用金額
        /// </summary>
        public virtual int? Amount { get; set; }
    }
}
