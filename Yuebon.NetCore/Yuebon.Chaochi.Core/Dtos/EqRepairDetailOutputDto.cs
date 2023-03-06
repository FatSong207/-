using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class EqRepairDetailOutputDto
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
        public string EqRepairId { get; set; }

        /// <summary>
        /// 設置或獲取設備名稱
        /// </summary>
        [MaxLength(100)]
        public string EqName { get; set; }

        /// <summary>
        /// 設置或獲取設備品牌(規格)
        /// </summary>
        [MaxLength(100)]
        public string EqBrand { get; set; }

        /// <summary>
        /// 設置或獲取修繕原因
        /// </summary>
        [MaxLength(100)]
        public string RepairReason { get; set; }

        /// <summary>
        /// 設置或獲取處理備註
        /// </summary>
        [MaxLength(100)]
        public string HandleNote { get; set; }

        /// <summary>
        /// 設置或獲取當前狀態
        /// </summary>
        [MaxLength(100)]
        public string CurrentState { get; set; }

        /// <summary>
        /// 設置或獲取成本
        /// </summary>
        [MaxLength(100)]
        public string Cost { get; set; }

        /// <summary>
        /// 設置或獲取報價
        /// </summary>
        [MaxLength(100)]
        public string Quote { get; set; }

        /// <summary>
        /// 設置或獲取修繕廠商
        /// </summary>
        [MaxLength(100)]
        public string RepairCompany { get; set; }

        /// <summary>
        /// 設置或獲取修繕前近照
        /// </summary>
        [MaxLength(100)]
        public string BeforeRepairClose { get; set; }

        /// <summary>
        /// 設置或獲取修繕前遠照
        /// </summary>
        [MaxLength(100)]
        public string BeforeRepairFar { get; set; }

        /// <summary>
        /// 設置或獲取修繕中近照
        /// </summary>
        [MaxLength(100)]
        public string RepairingClose { get; set; }

        /// <summary>
        /// 設置或獲取修繕中遠照
        /// </summary>
        [MaxLength(100)]
        public string RepairingFar { get; set; }

        /// <summary>
        /// 設置或獲取修繕後近照
        /// </summary>
        [MaxLength(100)]
        public string AfterRepairClose { get; set; }

        /// <summary>
        /// 設置或獲取修繕後遠照
        /// </summary>
        [MaxLength(100)]
        public string AfterRepairFar { get; set; }

        /// <summary>
        /// 照片數量
        /// </summary>
        public string PhotoCount { get; set; }
    }
}
