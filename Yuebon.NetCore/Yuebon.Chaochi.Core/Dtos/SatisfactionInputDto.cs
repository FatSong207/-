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
    [AutoMap(typeof(Satisfaction))]
    [Serializable]
    public class SatisfactionInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取問卷類別
        /// </summary>
        public string Type { get; set; }
        ///// <summary>
        ///// 設置或獲取當前問卷題數
        ///// </summary>
        //public string QuestionCount { get; set; }
        /// <summary>
        /// 設置或獲取當前問卷定義檔
        /// </summary>
        public string QFileName { get; set; }
        /// <summary>
        /// 設置或獲取問卷開頭語
        /// </summary>
        public string QBeginWords { get; set; }
        /// <summary>
        /// 設置或獲取問卷結尾語
        /// </summary>
        public string QEndWords { get; set; }

    }
}
