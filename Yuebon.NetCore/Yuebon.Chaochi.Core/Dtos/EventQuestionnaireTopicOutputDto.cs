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
    public class EventQuestionnaireTopicOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取問卷代號
        /// </summary>
        [MaxLength(100)]
        public string QCode { get; set; }

        /// <summary>
        /// 設置或獲取題序
        /// </summary>
        public int? Sequence { get; set; }

        /// <summary>
        /// 設置或獲取問卷題目
        /// </summary>
        [MaxLength(100)]
        public string Topic { get; set; }

        /// <summary>
        /// 設置或獲取題型(單選，複選，問答，日期)
        /// </summary>
        [MaxLength(100)]
        public string Type { get; set; }

        /// <summary>
        /// 設置或獲取必填
        /// </summary>
        [MaxLength(100)]
        public string Required { get; set; }

        /// <summary>
        /// 設置或獲取結束(最後一題)
        /// </summary>
        [MaxLength(100)]
        public string Last { get; set; }

        /// <summary>
        /// 設置或獲取選項(使用;分隔)
        /// </summary>
        [MaxLength(100)]
        public string Options { get; set; }


    }
}
