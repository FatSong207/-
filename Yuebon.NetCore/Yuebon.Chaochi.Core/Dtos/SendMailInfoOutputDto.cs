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
    public class SendMailInfoOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }

        /// <summary>
        /// 參考key值
        /// </summary>
        public virtual string RefKey { get; set; }
        /// <summary>
        /// 設置或獲取信件主旨
        /// </summary>
        [MaxLength(20)]
        public string Subject { get; set; }
        /// <summary>
        /// 設置或獲取收件者
        /// </summary>
        [MaxLength(20)]
        public string Recipient { get; set; }
        /// <summary>
        /// 設置或獲取內文
        /// </summary>
        [MaxLength(20)]
        public string Body { get; set; }
        /// <summary>
        /// 設置或獲取附件
        /// </summary>
        [MaxLength(20)]
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
