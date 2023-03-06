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
    public class EventQuestionnaireOutputDto
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
        /// 設置或獲取問卷連結
        /// </summary>
        [MaxLength(100)]
        public string QUrl { get; set; }
        /// <summary>
        /// 設置或獲取所屬活動
        /// </summary>
        [MaxLength(100)]
        public string EventName { get; set; }
        /// <summary>
        /// 設置或獲取所屬公司
        /// </summary>
        [MaxLength(100)]
        public string BelongCompany { get; set; }
        /// <summary>
        /// 設置或獲取調查問卷名稱
        /// </summary>
        [MaxLength(100)]
        public string QName { get; set; }
        /// <summary>
        /// 設置或獲取問卷開頭語
        /// </summary>
        [MaxLength(100)]
        public string QBegingWords { get; set; }
        /// <summary>
        /// 設置或獲取問卷結尾語
        /// </summary>
        [MaxLength(100)]
        public string QEndWords { get; set; }
        /// <summary>
        /// 設置或獲取當前問卷題數
        /// </summary>
        [MaxLength(100)]
        public string QuestionCount { get; set; }
        /// <summary>
        /// 設置或獲取當前問卷定義檔
        /// </summary>
        [MaxLength(100)]
        public string QFileName { get; set; }

        /// <summary>
        /// 設置或獲取是否已產生問卷
        /// </summary>
        [MaxLength(100)]
        public string HasGened { get; set; }
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
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }

    }
}
