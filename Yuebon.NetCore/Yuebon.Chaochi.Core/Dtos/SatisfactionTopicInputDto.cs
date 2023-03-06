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
    [AutoMap(typeof(SatisfactionTopic))]
    [Serializable]
    public class SatisfactionTopicInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取滿意度問卷Id
        /// </summary>
        public string SatisfactionId { get; set; }

        /// <summary>
        /// 設置或獲取題序
        /// </summary>
        public int? Sequence { get; set; }

        /// <summary>
        /// 設置或獲取題目
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
        /// 設置或獲取選項(使用;分隔)
        /// </summary>
        public string Options { get; set; }


    }
}
