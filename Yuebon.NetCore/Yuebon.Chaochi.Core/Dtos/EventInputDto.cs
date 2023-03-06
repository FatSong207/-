using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(Event))]
    [Serializable]
    public class EventInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

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
        public string StartDate { get; set; }

        /// <summary>
        /// 設置或獲取活動結束日期
        /// </summary>
        public string EndDate { get; set; }

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


    }
}
