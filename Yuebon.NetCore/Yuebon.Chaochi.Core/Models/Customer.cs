
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 客戶表，數據實體對象
    /// </summary>
    [Table("Chaochi_Customer")]
    [Serializable]
    public class Customer : BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public Customer()
        {
            
        }

        #region Property Members
        /// <summary>
        /// 身分證號,居留證號,統一編號
        /// </summary>
        public virtual string Account { get; set; }

        /// <summary>
        /// 姓名,公司全名
        /// </summary>
        public virtual string RealName { get; set; }

        /// <summary>
        /// 暱稱,公司簡稱
        /// </summary>
        public virtual string NickName { get; set; }

        /// <summary>
        /// 性別,1=男，0=未知，2=女，99=法人
        /// </summary>
        public virtual int? Gender { get; set; }

        /// <summary>
        /// 房東標誌
        /// </summary>
        public virtual bool? LandlordMark { get; set; }

        /// <summary>
        /// 房客標誌
        /// </summary>
        public virtual bool? RenterMark { get; set; }

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