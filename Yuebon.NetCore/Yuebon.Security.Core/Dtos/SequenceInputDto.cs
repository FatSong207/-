using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 單據編碼輸入對象模型
    /// </summary>
    [AutoMap(typeof(Sequence))]
    [Serializable]
    public class SequenceInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取名稱
        /// </summary>
        public string SequenceName { get; set; }

        /// <summary>
        /// 設置或獲取分隔符
        /// </summary>
        public string SequenceDelimiter { get; set; }

        /// <summary>
        /// 設置或獲取序號重置規則
        /// </summary>
        public string SequenceReset { get; set; }

        /// <summary>
        /// 設置或獲取步長
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// 設置或獲取當前值
        /// </summary>
        public int CurrentNo { get; set; }

        /// <summary>
        /// 設置或獲取當前編碼
        /// </summary>
        public string CurrentCode { get; set; }

        /// <summary>
        /// 設置或獲取當前重置依賴
        /// </summary>
        public string CurrentReset { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取是否可用
        /// </summary>
        public bool? EnabledMark { get; set; }
    }
}
