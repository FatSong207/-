﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Models;
using Yuebon.Security.Models;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class AppOutputDto: IOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string AppId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string AppSecret { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(256)]
        public string EncodingAESKey { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(256)]
        public string RequestUrl { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(256)]
        public string Token { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? IsOpenAEKey { get; set; }

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
        [MaxLength(500)]
        public string Description { get; set; }

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
        /// <summary>
        /// 
        /// </summary>
        public virtual User UserInfo { get; set; }


    }
}
