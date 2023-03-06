using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 問卷個資，數據實體對象
    /// </summary>
    [Table("SignUpQuest")]
    [Serializable]
    public class SignUpQuest
    {
        public int SignUpQuestSN { get; set; }

        /// <summary>
        /// 設置或獲取問卷編號
        /// </summary>
        public int? QuestTopicSN { get; set; }
        /// <summary>
        /// 設置或獲取業務員ID
        /// </summary>
        public string AgentID { get; set; }
        /// <summary>
        /// 設置或獲取報名人員ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 設置或獲取姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 設置或獲取性別 (M = 男, F = 女)
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 設置或獲取手機
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 設置或獲取E-Mail
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 設置或獲取生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 設置或獲取居住縣市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 設置或獲取地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 設置或獲取身份
        /// </summary>
        public string Rank { get; set; }
        /// <summary>
        /// 設置或獲取填寫日期
        /// </summary>
        public DateTime? QuestDate { get; set; }
        /// <summary>
        /// 設置或獲取檢查碼
        /// </summary>
        public string Crc64 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BirthdayLog { get; set; }

    }
}
