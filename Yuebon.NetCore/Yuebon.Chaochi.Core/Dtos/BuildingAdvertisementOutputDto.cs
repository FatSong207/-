using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 建物廣告管理輸出對象模型
    /// </summary>
    [Serializable]
    public class BuildingAdvertisementOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取建物地址
        /// </summary>
        [MaxLength(100)]
        public string BAdd { get; set; }
        /// <summary>
        /// 設置或獲取建物廣告狀態
        /// </summary>
        [MaxLength(50)]
        public string BAStatus { get; set; }
        /// <summary>
        /// 設置或獲取待招租起始日
        /// </summary>
        public DateTime? BRStartDate { get; set; }        /// <summary>
        /// 設置或獲取廣告刊登日
        /// </summary>
        public DateTime? BAStartDate { get; set; }        /// <summary>
        /// 設置或獲取上架天數
        /// </summary>
        public string BADays { get; set; }
        /// <summary>
        /// 設置或獲取招租天數
        /// </summary>
        public string BRDays { get; set; }
        /// <summary>
        /// 設置或獲取廣告網址
        /// </summary>
        [MaxLength(2100)]
        public string BAURL { get; set; }
        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取創建用戶ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬組織ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserOrgId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬部門ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserDeptId { get; set; }

        /// <summary>
        /// 設置或獲取創建人所屬部門名稱
        /// </summary>        
        public string CreatorUserDeptName { get; set; }
        /// <summary>
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取最後修改用戶ID
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }

        // 建物狀態
        public string BState { get; set; }

        // 房/廳/衛
        public string BInfo { get; set; }
        // 坪數
        public string BRealPING { get; set; }        // 租金
        public string BTRent { get; set; }        // 業務        public string Sales { get; set; }        public string BPropoty { get; set; }
    }
}
