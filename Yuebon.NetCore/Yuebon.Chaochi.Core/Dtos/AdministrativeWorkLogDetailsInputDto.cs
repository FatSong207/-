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
    /// 行政工作日誌明細輸入對象模型
    /// </summary>
    [AutoMap(typeof(AdministrativeWorkLogDetails))]
    [Serializable]
    public class AdministrativeWorkLogDetailsInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string AdministrativeWorkLogId { get; set; }
        /// <summary>
        /// 設置或獲取本日重點工作事項
        /// </summary>
        public string Important { get; set; }
        /// <summary>
        /// 設置或獲取完成度
        /// </summary>
        public string FinishState { get; set; }
        /// <summary>
        /// 設置或獲取備註
        /// </summary>
        public string Note { get; set; }

    }
}
