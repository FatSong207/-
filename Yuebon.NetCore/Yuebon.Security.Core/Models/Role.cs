
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 角色表，數據實體對象
    /// </summary>
    [Table("Sys_Role")]
    public class Role: BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public Role()
		{
            //this.Id= System.Guid.NewGuid().ToString();

 		}

        #region Property Members

        

        /// <summary>
        /// 組織主鍵
        /// </summary>
        public virtual string OrganizeId { get; set; }
        /// <summary>
        /// 分類:1-角色2-崗位
        /// </summary>
        public virtual int? Category { get; set; }

        /// <summary>
        /// 角色編碼
        /// </summary>
        public virtual string EnCode { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public virtual string FullName { get; set; }

        /// <summary>
        /// 類型
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 允許編輯
        /// </summary>
        public virtual bool? AllowEdit { get; set; }

        /// <summary>
        /// 允許刪除
        /// </summary>
        public virtual bool? AllowDelete { get; set; }

        /// <summary>
        /// 排序碼
        /// </summary>
        public virtual int? SortCode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 刪除標志
        /// </summary>
        public virtual bool? DeleteMark { get; set; }

        /// <summary>
        /// 有效標志
        /// </summary>
        public virtual bool EnabledMark { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 最后修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用戶
        /// </summary>
        public virtual string LastModifyUserId { get; set; }

        /// <summary>
        /// 刪除時間
        /// </summary>
        public virtual DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 刪除用戶
        /// </summary>
        public virtual string DeleteUserId { get; set; }
        #endregion

    }
}