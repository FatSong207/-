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
    /// 業務拜訪記錄輸入對象模型
    /// </summary>
    [AutoMap(typeof(VisitingRecord))]
    [Serializable]
    public class VisitingRecordInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取潛在客ID
        /// </summary>
        public string PID { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string VisitTime { get; set; }
        /// <summary>
        /// 設置或獲取承租人當前狀態
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 設置或獲取當前業務
        /// </summary>
        public string Sales { get; set; }
        /// <summary>
        /// 設置或獲取客戶備註
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬組織ID
        /// </summary>
        public string CreatorUserOrgId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬部門ID
        /// </summary>
        public string CreatorUserDeptId { get; set; }

    }
}
