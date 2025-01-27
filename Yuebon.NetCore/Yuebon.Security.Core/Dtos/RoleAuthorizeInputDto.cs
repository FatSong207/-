﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Models;
using Yuebon.Security.Models;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(RoleAuthorize))]
    [Serializable]
    public class RoleAuthorizeInputDto : IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? ItemType { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? ObjectType { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ObjectId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? SortCode { get; set; }


    }
}
