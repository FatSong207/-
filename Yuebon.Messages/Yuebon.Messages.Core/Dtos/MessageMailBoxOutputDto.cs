﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Messages.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class MessageMailBoxOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(500)]
        public string Title { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(1073741823)]
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取是否短信提醒
        /// </summary>
        public bool? IsMsgRemind { get; set; }

        /// <summary>
        /// 設置或獲取是否發送
        /// </summary>
        public bool? IsSend { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime SendDate { get; set; }

        /// <summary>
        /// 設置或獲取是否是強制消息
        /// </summary>
        public bool? IsCompel { get; set; }

        /// <summary>
        /// 設置或獲取是否發送
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string CompanyId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string DeptId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string DeleteUserId { get; set; }


    }
}
