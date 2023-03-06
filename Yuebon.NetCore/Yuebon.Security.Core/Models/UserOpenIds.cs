
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
    /// 第三方登錄與用戶綁定表，數據實體對象
    /// </summary>
    [Table("Sys_UserOpenIds")]
    public class UserOpenIds:BaseEntity<string>
    {
        #region Property Members
        /// <summary>
        /// 用戶編號
        /// </summary>
        public virtual string UserId { get; set; }

        /// <summary>
        /// 第三方類型
        /// </summary>
        public virtual string OpenIdType { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        public virtual string OpenId { get; set; }

        #endregion

    }
}