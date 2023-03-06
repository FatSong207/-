using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Messages.Models
{
    /// <summary>
    /// ，數據實體對象
    /// </summary>
    [Table("Sys_MemberMessageBox")]
    [Serializable]
    public class MemberMessageBox:BaseEntity<string>
    {
        /// <summary>
        /// 設置或獲取消息內容Id
        /// </summary>
        public long? ContentId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string MsgContent { get; set; }

        /// <summary>
        /// 設置或獲取發送者
        /// </summary>
        public string Sernder { get; set; }

        /// <summary>
        /// 設置或獲取接受者
        /// </summary>
        public string Accepter { get; set; }

        /// <summary>
        /// 設置或獲取是否已讀
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? ReadDate { get; set; }


    }
}
