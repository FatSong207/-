using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Models;
using Yuebon.Security.Models;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 序號編碼規則表輸入對象模型
    /// </summary>
    [AutoMap(typeof(SequenceRule))]
    [Serializable]
    public class SequenceRuleInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取編碼規則名稱
        /// </summary>
        public string SequenceName { get; set; }

        /// <summary>
        /// 設置或獲取規則排序
        /// </summary>
        public int RuleOrder { get; set; }

        /// <summary>
        /// 設置或獲取規則類別，timestamp、const、bumber
        /// </summary>
        public string RuleType { get; set; }

        /// <summary>
        /// 設置或獲取規則參數，如YYMMDD
        /// </summary>
        public string RuleValue { get; set; }

        /// <summary>
        /// 設置或獲取補齊方向，left或right
        /// </summary>
        public string PaddingSide { get; set; }

        /// <summary>
        /// 設置或獲取補齊寬度
        /// </summary>
        public int PaddingWidth { get; set; }

        /// <summary>
        /// 設置或獲取填充字符
        /// </summary>
        public string PaddingChar { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取是否可用
        /// </summary>
        public bool EnabledMark { get; set; }


    }
}
