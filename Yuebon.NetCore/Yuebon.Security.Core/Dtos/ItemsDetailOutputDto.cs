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
    public class ItemsDetailOutputDto
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
        public string ItemId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string ParentId { get; set; }


        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string ParentName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string ItemCode { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string ItemName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(500)]
        public string SimpleSpelling { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? Layers { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public List<ItemsDetailOutputDto> Children { get; set; }
    }
}
