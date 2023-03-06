using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 多物件管理，數據實體對象
    /// </summary>
    [Table("Chaochi_MO")]
    [Serializable]
    public class MO:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 設置或獲取分租物件單編號
        /// </summary>
        public string MOID { get; set; }
        /// <summary>
        /// 設置或獲取多物件名稱
        /// </summary>
        public string MOName { get; set; }

        /// <summary>
        /// 設置或獲取原建物地址
        /// </summary>
        public string OGBuildingName { get; set; }
        /// <summary>
        /// 設置或獲取出租人-身份證號或統一編號
        /// </summary>
        public string LSID { get; set; }
        /// <summary>
        /// 設置或獲取出租人-姓名/法人名稱
        /// </summary>
        public string LSName { get; set; }
        /// <summary>
        /// 設置或獲取二房東-統一編號
        /// </summary>
        public string SLID { get; set; }
        /// <summary>
        /// 設置或獲取二房東-公司名稱
        /// </summary>
        public string SLName { get; set; }
        /// <summary>
        /// 設置或獲取合約狀態
        /// </summary>
        public string Status { get; set; }
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
        /// 設置或獲取刪除標誌
        /// </summary>
        public bool? DeleteMark { get; set; }
        /// <summary>
        /// 設置或獲取刪除時間
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 設置或獲取刪除用戶ID
        /// </summary>
        public string DeleteUserId { get; set; }
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
