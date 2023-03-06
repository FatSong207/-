using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 建物廣告管理，數據實體對象
    /// </summary>
    [Table("Chaochi_BuildingAdvertisement")]
    [Serializable]
    public class BuildingAdvertisement:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取建物地址
        /// </summary>
        public string BAdd { get; set; }
        /// <summary>
        /// 設置或獲取建物廣告狀態
        /// </summary>
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
        public string BAURL { get; set; }
        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取創建用戶ID
        /// </summary>
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬組織ID
        /// </summary>
        public string CreatorUserOrgId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬部門ID
        /// </summary>
        public string CreatorUserDeptId { get; set; }
        /// <summary>
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取最後修改用戶ID
        /// </summary>
        public string LastModifyUserId { get; set; }

    }
}
