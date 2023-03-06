using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 業務工作日誌明細輸入對象模型
    /// </summary>
    [AutoMap(typeof(SalesWorkLogDetails))]
    [Serializable]
    public class SalesWorkLogDetailsInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string SalesWorkLogId { get; set; }
        /// <summary>
        /// 設置或獲取品項
        /// </summary>
        public string Item { get; set; }
        /// <summary>
        /// 設置或獲取型態
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 設置或獲取業績額
        /// </summary>
        public string Performance { get; set; }
        /// <summary>
        /// 設置或獲取地址
        /// </summary>
        public string Address { get; set; }

    }
}
