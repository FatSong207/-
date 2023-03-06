using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// ，數據實體對象
    /// </summary>
    [Table("Chaochi_Contractform")]
    [Serializable]
    public class Contractform:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 設置或獲取表單編號
        /// </summary>
        public string FormId { get; set; }
        /// <summary>
        /// 設置或獲取表單版本
        /// </summary>
        public string Vno { get; set; }
        /// <summary>
        /// 設置或獲取表單名稱
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 設置或獲取合約分類Id
        /// </summary>
        public string CateId { get; set; }        /// <summary>
        /// 設置或獲取合約分類
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 設置或獲取創建日期
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
        public string CreatorUserDeptId { get; set; }        /// <summary>
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取最後修改用戶ID
        /// </summary>
        public string LastModifyUserId { get; set; }
        /// <summary>
        /// 設置或獲取刪除標誌
        /// </summary>
        public bool? DeleteMark { get; set; }        /// <summary>
        /// 設置或獲取刪除時間
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 設置或獲取刪除用戶ID
        /// </summary>
        public virtual string DeleteUserId { get; set; }
    }
}
