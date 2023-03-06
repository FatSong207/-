using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class EqRepairOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Guid { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string CustomerID { get; set; }

        /// <summary>
        /// 合約編號
        /// </summary>
        public string CID { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string BAdd { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string ReporterName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string ReporterCell { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RepairDateTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string State { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string ApplicationDate { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string EndCaseDate { get; set; }

        /// <summary>
        /// 明細(每一個帶維修設備)
        /// </summary>
        public List<EqRepairDetail>  eqRepairDetails { get; set; }

        #region 修繕明細頁 上方唯讀欄位

        /// <summary>
        /// 合約內記載的房東姓名
        /// </summary>
        public string LSName { get; set; }

        /// <summary>
        /// 合約內記載的房東電話
        /// </summary>
        public string LSTel { get; set; }

        #endregion 修繕明細頁 上方唯讀欄位

    }
}
