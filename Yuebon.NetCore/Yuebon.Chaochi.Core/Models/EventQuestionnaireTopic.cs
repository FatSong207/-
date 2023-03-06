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
    [Table("Chaochi_EventQuestionnaireTopic")]
    [Serializable]
    public class EventQuestionnaireTopic:BaseEntity<string>
    {
        /// <summary>
        /// 設置或獲取問卷代號
        /// </summary>
        public string QCode { get; set; }

        /// <summary>
        /// 設置或獲取題序
        /// </summary>
        public int? Sequence { get; set; }

        /// <summary>
        /// 設置或獲取問卷題目
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// 設置或獲取題型(單選，複選，問答，日期)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 設置或獲取必填
        /// </summary>
        public string Required { get; set; }

        /// <summary>
        /// 設置或獲取結束(最後一題)
        /// </summary>
        public string Last { get; set; }

        /// <summary>
        /// 設置或獲取選項(使用;分隔)
        /// </summary>
        public string Options { get; set; }


    }
}
