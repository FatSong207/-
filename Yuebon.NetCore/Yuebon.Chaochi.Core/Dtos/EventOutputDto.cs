using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class EventOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取活動代號
        /// </summary>
        [MaxLength(100)]
        public string EventId { get; set; }

        /// <summary>
        /// 設置或獲取活動名稱
        /// </summary>
        [MaxLength(100)]
        public string EventName { get; set; }

        /// <summary>
        /// 設置或獲取活動起始日期
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 設置或獲取活動結束日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 設置或獲取活動所屬公司
        /// </summary>
        [MaxLength(100)]
        public string BelongCompany { get; set; }

        /// <summary>
        /// 設置或獲取活動管理人員
        /// </summary>
        [MaxLength(100)]
        public string Manager { get; set; }

        /// <summary>
        /// 設置或獲取活動類型
        /// </summary>
        [MaxLength(100)]
        public string EventType { get; set; }

        /// <summary>
        /// 完成數量
        /// </summary>
        public virtual int? FinishCount { get; set; }

        /// <summary>
        /// 成本與收入
        /// </summary>
        public IEnumerable<EventCost>  eventCosts { get; set; }

        /// <summary>
        /// 投入人員
        /// </summary>
        public IEnumerable<string>  eventPersonnels { get; set; }

        /// <summary>
        /// 調查問卷
        /// </summary>
        public virtual EventQuestionnaire EventQuestionnaire { get; set; }

        /// <summary>
        /// 滿意度問卷
        /// </summary>
        public virtual EventSatisfaction EventSatisfaction { get; set; }

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
