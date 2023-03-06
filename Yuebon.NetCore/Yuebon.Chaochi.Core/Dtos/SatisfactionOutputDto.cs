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
    public class SatisfactionOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取問卷類別
        /// </summary>
        [MaxLength(100)]
        public string Type { get; set; }
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
        /// 設置或獲取問卷開頭語
        /// </summary>
        [MaxLength(100)]
        public string QBeginWords { get; set; }
        /// <summary>
        /// 設置或獲取問卷結尾語
        /// </summary>
        [MaxLength(100)]
        public string QEndWords { get; set; }

        /// <summary>
        /// 問卷填寫網址
        /// </summary>
        public string QUrl { get; set; }
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
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }


        public string LastModifyUserName { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

    }
}
