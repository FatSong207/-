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
    public class EventSatisfactionOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取活動Id
        /// </summary>
        [MaxLength(100)]
        public string EventId { get; set; }

        /// <summary>
        /// 設置或獲取問卷類型
        /// </summary>
        [MaxLength(100)]
        public string QType { get; set; }

        /// <summary>
        /// 設置或獲取問卷代號
        /// </summary>
        [MaxLength(100)]
        public string QCode { get; set; }

        /// <summary>
        /// 設置或獲取所屬活動名稱
        /// </summary>
        [MaxLength(100)]
        public string EventName { get; set; }

        /// <summary>
        /// 設置或獲取所屬公司
        /// </summary>
        [MaxLength(100)]
        public string BelongCompany { get; set; }

        /// <summary>
        /// 設置或獲取問卷連結
        /// </summary>
        [MaxLength(100)]
        public string QUrl { get; set; }

        /// <summary>
        /// 設置或獲取問卷QRCode圖片
        /// </summary>
        [MaxLength(100)]
        public string QRcodeImg { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }


    }
}
