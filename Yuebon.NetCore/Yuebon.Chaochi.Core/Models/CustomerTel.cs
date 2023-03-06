
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
    /// 客戶表_連絡人電話，數據實體對象
    /// </summary>
    [Table("Chaochi_CustomerTel")]
    [Serializable]
    public class CustomerTel : BaseEntity<string>, ICreationAudited, IModificationAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public CustomerTel()
        {
            
        }

        #region Property Members
        /// <summary>
        /// 身分證號,居留證號,統一編號
        /// </summary>
        public virtual string Account { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public virtual string Telephone { get; set; }

        /// <summary>
        /// 聯絡人
        /// </summary>
        public virtual string ContactPerson { get; set; }

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