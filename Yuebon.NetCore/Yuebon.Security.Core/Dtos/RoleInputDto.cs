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
    [AutoMap(typeof(Role))]
    [Serializable]
    public class RoleInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? Category { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string EnCode { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string[] Type { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? AllowEdit { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? AllowDelete { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? SortCode { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool EnabledMark { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Description { get; set; }


    }
}
