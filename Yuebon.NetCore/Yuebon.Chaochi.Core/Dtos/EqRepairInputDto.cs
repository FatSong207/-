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
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(EqRepair))]
    [Serializable]
    public class EqRepairInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 合約編號
        /// </summary>
        public string CID { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BAdd { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ReporterName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ReporterCell { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RepairDateTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ApplicationDate { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string EndCaseDate { get; set; }

        /// <summary>
        /// 明細(每一個維修設備)
        /// </summary>
        public List<EqRepairDetail>  eqRepairDetails { get; set; }
    }
}
