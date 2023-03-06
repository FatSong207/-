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
    [Table("Chaochi_EventGuest")]
    [Serializable]
    public class EventGuest:BaseEntity<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string EventId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string GuestName { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string GusetCell { get; set; }

    }
}
