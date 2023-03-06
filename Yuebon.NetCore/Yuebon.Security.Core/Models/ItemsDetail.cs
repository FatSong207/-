
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Yuebon.Commons.Models;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 數據字典選項明細表，數據實體對象
    /// </summary>
    [Table("Sys_ItemsDetail")]
    [Serializable]
    public class ItemsDetail: BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public ItemsDetail()
		{
            //this.Id= System.Guid.NewGuid().ToString();

 		}

        #region Property Members


        /// <summary>
        /// 主表主鍵
        /// </summary>
        public virtual string ItemId { get; set; }

        /// <summary>
        /// 父級
        /// </summary>
        public virtual string ParentId { get; set; }

        /// <summary>
        /// 編碼
        /// </summary>
        public virtual string ItemCode { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public virtual string ItemName { get; set; }

        /// <summary>
        /// 簡拼
        /// </summary>
        public virtual string SimpleSpelling { get; set; }

        /// <summary>
        /// 默認
        /// </summary>
        public virtual bool? IsDefault { get; set; }

        /// <summary>
        /// 層次
        /// </summary>
        public virtual int? Layers { get; set; }

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
        [MaxLength(50)]
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 最后修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用戶
        /// </summary>
        [MaxLength(50)]
        public virtual string LastModifyUserId { get; set; }

        /// <summary>
        /// 刪除時間
        /// </summary>
        public virtual DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 刪除用戶
        /// </summary>
        [MaxLength(50)]
        public virtual string DeleteUserId { get; set; }
        #endregion

    }
}