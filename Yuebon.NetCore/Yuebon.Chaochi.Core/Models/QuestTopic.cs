using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 問卷，數據實體對象
    /// </summary>
    [Table("QuestTopic")]
    [Serializable]
    public class QuestTopic
    {
        public int QuestTopicSN { get; set; }

        /// <summary>
        /// 設置或獲取問卷代號
        /// </summary>
        public string QuestTopicId { get; set; }
        /// <summary>
        /// 設置或獲取問卷說明
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 設置或獲取開頭語
        /// </summary>
        public string RemarkTop { get; set; }
        /// <summary>
        /// 設置或獲取預設結尾語
        /// </summary>
        public string RemarkEnd { get; set; }
        /// <summary>
        /// 設置或獲取問卷後網址
        /// </summary>
        public string EndUrl { get; set; }
        /// <summary>
        /// 設置或獲取問卷後網址標題
        /// </summary>
        public string EndTitle { get; set; }
        /// <summary>
        /// 設置或獲取必填個資欄位 (以 , 逗號分隔)
        /// </summary>
        public string SignUpColumns { get; set; }
        /// <summary>
        /// 設置或獲取建立日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 設置或獲取是否指定結尾語(Y)
        /// </summary>
        public string IsEndMsg { get; set; }
        /// <summary>
        /// 設置或獲取選填個資欄位 (以 , 逗號分隔)
        /// </summary>
        public string OptionColumns { get; set; }

    }
}
