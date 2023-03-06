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
    [Table("Chaochi_SendMailInfo")]
    [Serializable]
    public class SendMailInfo:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 參考key值
        /// </summary>
        public virtual string RefKey { get; set; }

        /// <summary>
        /// 設置或獲取信件主旨
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 設置或獲取收件者
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// 設置或獲取內文
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 設置或獲取附件
        /// </summary>
        public string Attachments { get; set; }

        /// <summary>
        /// 設置或獲取是否啟用
        /// </summary>
        public bool? Enable { get; set; }

        /// <summary>
        /// 設置或獲取預計寄送日期
        /// </summary>
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// 設置或獲取完成寄送日期
        /// </summary>
        public DateTime? SendedTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LastModifyUserId { get; set; }


    }
}
