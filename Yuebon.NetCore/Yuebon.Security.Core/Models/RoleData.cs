
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 角色的數據權限，數據實體對象
    /// </summary>
    [Table("Sys_RoleData")]
    public class RoleData:BaseEntity<string>
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public RoleData()
        {
            this.Id = GuidUtils.CreateNo();
        }

        #region Property Members

        /// <summary>
        /// 角色ID
        /// </summary>
        public virtual string RoleId { get; set; }

        /// <summary>
        /// 類型，company-公司，dept-部門，person-個人
        /// </summary>
        public virtual string DType { get; set; }

        /// <summary>
        /// 數據數據，部門ID或個人ID
        /// </summary>
        public virtual string AuthorizeData { get; set; }



        #endregion

    }
}