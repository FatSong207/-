using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Messages.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class MemberMessageBoxOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取消息內容Id
        /// </summary>
        public long? ContentId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(200)]
        public string MsgContent { get; set; }

        /// <summary>
        /// 設置或獲取發送者
        /// </summary>
        [MaxLength(100)]
        public string Sernder { get; set; }

        /// <summary>
        /// 設置或獲取接受者
        /// </summary>
        [MaxLength(100)]
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
