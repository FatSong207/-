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
    /// 行政工作日誌輸入對象模型
    /// </summary>
    [AutoMap(typeof(AdministrativeWorkLog))]
    [Serializable]
    public class AdministrativeWorkLogInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 代辦工作事項
        /// </summary>
        public string TodoNote { get; set; }

        /// <summary>
        /// 其他事項
        /// </summary>
        public string OtherNote { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public string States { get; set; }        public List<AdministrativeWorkLogDetails> AdministrativeWorkLogDetails { get; set; }
    }
}
