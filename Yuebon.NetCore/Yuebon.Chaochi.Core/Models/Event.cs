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
    [Table("Chaochi_Event")]
    [Serializable]
    public class Event:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取活動代號
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// 設置或獲取活動名稱
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// 設置或獲取活動起始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 設置或獲取活動結束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 設置或獲取活動所屬公司
        /// </summary>
        public string BelongCompany { get; set; }

        /// <summary>
        /// 設置或獲取活動管理人員
        /// </summary>
        public string Manager { get; set; }

        /// <summary>
        /// 設置或獲取活動類型
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// 合計費用
        /// </summary>
        public virtual int? TotalCost { get; set; }

        /// <summary>
        /// 合計收入
        /// </summary>
        public virtual int? TotalIncome { get; set; }

        /// <summary>
        /// 收支小計
        /// </summary>
        public virtual int? SubTotal { get; set; }

        /// <summary>
        /// 人員數量
        /// </summary>
        public virtual int? TotalPerson { get; set; }

        /// <summary>
        /// 賓客數量
        /// </summary>
        public virtual int? GuestCount { get; set; }

        /// <summary>
        /// 完成數量
        /// </summary>
        public virtual int? FinishCount { get; set; }

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
