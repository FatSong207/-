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
    [Table("Chaochi_Satisfaction")]
    [Serializable]
    public class Satisfaction:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取問卷類別
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 設置或獲取當前問卷題數
        /// </summary>
        public string QuestionCount { get; set; }

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

        /// <summary>
        /// 問卷填寫網址
        /// </summary>
        public string QUrl { get; set; }

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
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }


    }
}
