using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// ，數據實體對象
    /// </summary>
    [Table("Chaochi_EventQuestionnaire")]
    [Serializable]
    public class EventQuestionnaire:BaseEntity<string>, ICreationAudited, IModificationAudited
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
        /// 設置或獲取問卷連結
        /// </summary>
        public string QUrl { get; set; }

        /// <summary>
        /// 設置或獲取所屬活動
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// 設置或獲取所屬公司
        /// </summary>
        public string BelongCompany { get; set; }

        /// <summary>
        /// 設置或獲取調查問卷名稱
        /// </summary>
        public string QName { get; set; }

        /// <summary>
        /// 設置或獲取問卷開頭語
        /// </summary>
        public string QBegingWords { get; set; }

        /// <summary>
        /// 設置或獲取問卷結尾語
        /// </summary>
        public string QEndWords { get; set; }

        /// <summary>
        /// 設置或獲取當前問卷題數
        /// </summary>
        public string QuestionCount { get; set; }

        /// <summary>
        /// 設置或獲取當前問卷定義檔
        /// </summary>
        public string QFileName { get; set; }

        /// <summary>
        /// 設置或獲取是否已產生問卷
        /// </summary>
        [MaxLength(100)]
        public string HasGened { get; set; }

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
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LastModifyUserId { get; set; }


    }
}
