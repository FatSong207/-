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
    [AutoMap(typeof(EventSatisfaction))]
    [Serializable]
    public class EventSatisfactionInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取活動Id
        /// </summary>
        public string EventId { get; set; }
        /// <summary>
        /// 設置或獲取問卷類型
        /// </summary>
        public string QType { get; set; }
        /// <summary>
        /// 設置或獲取問卷代號
        /// </summary>
        public string QCode { get; set; }
        /// <summary>
        /// 設置或獲取所屬活動名稱
        /// </summary>
        public string EventName { get; set; }
        /// <summary>
        /// 設置或獲取所屬公司
        /// </summary>
        public string BelongCompany { get; set; }
        /// <summary>
        /// 設置或獲取問卷連結
        /// </summary>
        public string QUrl { get; set; }
        /// <summary>
        /// 設置或獲取問卷QRCode圖片
        /// </summary>
        public string QRcodeImg { get; set; }

    }
}
