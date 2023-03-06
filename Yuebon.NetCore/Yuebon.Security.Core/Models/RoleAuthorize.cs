
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 角色授權表，數據實體對象
    /// </summary>
    [Table("Sys_RoleAuthorize")]
    [Serializable]
    public class RoleAuthorize: BaseEntity<string>, ICreationAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public RoleAuthorize()
		{
            this.Id = GuidUtils.CreateNo();

 		}

        #region Property Members


        /// <summary>
        /// 項目類型功能標識 0-子系統 1-標識菜單/模塊，2標識按鈕功能
        /// </summary>
        public virtual int? ItemType { get; set; }

        /// <summary>
        /// 項目主鍵
        /// </summary>
        public virtual string ItemId { get; set; }

        /// <summary>
        /// 對象分類/類型1-角色，2-部門，3-用戶
        /// </summary>
        public virtual int? ObjectType { get; set; }

        /// <summary>
        /// 對象主鍵，對象分類/類型為角色時就是角色ID，部門就是部門ID，用戶就是用戶ID
        /// </summary>
        public virtual string ObjectId { get; set; }

        /// <summary>
        /// 排序碼
        /// </summary>
        public virtual int? SortCode { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public virtual string CreatorUserId { get; set; }

        #endregion

    }
}