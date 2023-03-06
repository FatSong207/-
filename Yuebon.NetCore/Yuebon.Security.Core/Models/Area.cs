using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 行政區域表，數據實體對象
    /// </summary>
    [Table("Sys_Area")]
    [Serializable]
    public class Area:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public Area()
		{
            

 		}

        #region Property Members
        ///// <summary>
        ///// 主鍵
        ///// </summary>
        //[MaxLength(50)]
        //[ExplicitKey]
        //public virtual string Id { get; set; }

        /// <summary>
        /// 父級
        /// </summary>
        [MaxLength(50)]
        public virtual string ParentId { get; set; }

        /// <summary>
        /// 層次
        /// </summary>
        public virtual int? Layers { get; set; }

        /// <summary>
        /// 編碼
        /// </summary>
        [MaxLength(50)]
        public virtual string EnCode { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        [MaxLength(100)]
        public virtual string FullName { get; set; }

        /// <summary>
        /// 簡拼
        /// </summary>
        [MaxLength(200)]
        public virtual string SimpleSpelling { get; set; }

        /// <summary>
        /// 父級路徑
        /// </summary>
        [MaxLength(600)]
        public virtual string FullIdPath { get; set; }

        /// <summary>
        /// 是否是最后一級
        /// </summary>
        public virtual bool IsLast { get; set; }

        /// <summary>
        /// 排序碼
        /// </summary>
        public virtual int? SortCode { get; set; }

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