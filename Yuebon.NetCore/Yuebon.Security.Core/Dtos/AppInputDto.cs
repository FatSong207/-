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
    [AutoMap(typeof(APP))]
    [Serializable]
    public class APPInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string EncodingAESKey { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool IsOpenAEKey { get; set; }

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
