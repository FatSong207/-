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
    [Table("Chaochi_EventSatisfaction")]
    [Serializable]
    public class EventSatisfaction:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
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

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }


    }
}
