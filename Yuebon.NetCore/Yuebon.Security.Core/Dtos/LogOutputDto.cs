﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class LogOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Account { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string NickName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string OrganizeId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Type { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string IPAddress { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string IPAddressName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string ModuleId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string ModuleName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? Result { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(2147483647)]
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? EnabledMark { get; set; }

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
