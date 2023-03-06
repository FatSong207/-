
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 用戶表，數據實體對象
    /// </summary>
    [Table("Sys_UserFocus")]
    [Serializable]
    public class UserFocus : BaseEntity<string>,ICreationAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public UserFocus()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        #region Property Members

        /// <summary>
        /// 關注的用戶ID
        /// </summary>
        public virtual string FocusUserId { get; set; }

        /// <summary>
        /// 創建用戶ID
        /// </summary>
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 關注時間
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }
        #endregion

    }
}