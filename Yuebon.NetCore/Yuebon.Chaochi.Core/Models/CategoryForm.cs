using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Core.Models
{
    /// <summary>
    /// 表單分類物件，數據實體對象
    /// </summary>
    [Table("Chaochi_CategoryForm")]
    [Serializable]
    public class CategoryForm : BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public CategoryForm()
        {
        }

        #region Property Members


        /// <summary>
        /// 父層Id
        /// </summary>
        public virtual string ParentId { get; set; }

        /// <summary>
        /// 層次
        /// </summary>
        public virtual int? Layers { get; set; }

        /// <summary>
        /// 分類名稱
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 排序碼
        /// </summary>
        public virtual int? SortOrder { get; set; }

        /// <summary>
        /// 刪除標誌
        /// </summary>
        public virtual bool? DeleteMark { get; set; }

        /// <summary>
        /// 有效標誌
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
        /// 最後修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最後修改用戶
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
