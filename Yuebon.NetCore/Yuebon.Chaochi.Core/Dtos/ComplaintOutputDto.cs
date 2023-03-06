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
    public class ComplaintOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取客訴單編號
        /// </summary>
        [MaxLength(100)]
        public string CCode { get; set; }
        /// <summary>
        /// 設置或獲取投訴人姓名
        /// </summary>
        [MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 設置或獲取投訴人聯絡電話
        /// </summary>
        [MaxLength(100)]
        public string ContactPhone { get; set; }
        /// <summary>
        /// 設置或獲取投訴人聯絡信箱
        /// </summary>
        [MaxLength(100)]
        public string ContactMail { get; set; }
        /// <summary>
        /// 設置或獲取投訴人可連絡時段
        /// </summary>
        [MaxLength(100)]
        public string ContactDatetime { get; set; }
        /// <summary>
        /// 設置或獲取客訴類別
        /// </summary>
        [MaxLength(100)]
        public string ComplaintType { get; set; }
        /// <summary>
        /// 設置或獲取客訴部門/公司
        /// </summary>
        [MaxLength(100)]
        public string ComplaintDept { get; set; }
        /// <summary>
        /// 設置或獲取被客訴人
        /// </summary>
        [MaxLength(100)]
        public string Complainee { get; set; }
        /// <summary>
        /// 設置或獲取有糾紛的合約編號
        /// </summary>
        [MaxLength(100)]
        public string CId { get; set; }
        /// <summary>
        /// 設置或獲取客訴事由
        /// </summary>
        [MaxLength(600)]
        public string ComplaintReason { get; set; }
        /// <summary>
        /// 設置或獲取投訴人意見提供
        /// </summary>
        [MaxLength(600)]
        public string ProvideAdvice { get; set; }
        /// <summary>
        /// 設置或獲取單據狀態
        /// </summary>
        [MaxLength(100)]
        public string State { get; set; }
        /// <summary>
        /// 設置或獲取客服人員
        /// </summary>
        [MaxLength(100)]
        public string HandleUser { get; set; }

        public string HandleUserName { get; set; }
        /// <summary>
        /// 設置或獲取填單日期
        /// </summary>
        public string ComplaintCreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取送審日期
        /// </summary>
        public string SendTrialTime { get; set; }
        /// <summary>
        /// 設置或獲取結案日期
        /// </summary>
        public string EndCaseTime { get; set; }
        /// <summary>
        /// 設置或獲取客服人員處理備註
        /// </summary>
        [MaxLength(600)]
        public string HandleNote { get; set; }
        /// <summary>
        /// 設置或獲取累積發信次數
        /// </summary>
        public int? SendMailCount { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }

    }
}
