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
    public class AreaOutputDto
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
        public string ParentId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? Layers { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string EnCode { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(400)]
        public string FullName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(200)]
        public string SimpleSpelling { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(600)]
        public string FullIdPath { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? IsLast { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? SortCode { get; set; }

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
