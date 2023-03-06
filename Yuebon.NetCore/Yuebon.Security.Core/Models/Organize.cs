using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 組織機構，數據實體對象
    /// </summary>
    [Table("Sys_Organize")]
    [Serializable]
    public class Organize : BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? Layers { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string EnCode { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ManagerId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string TelePhone { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? AllowEdit { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? AllowDelete { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? SortCode { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? EnabledMark { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string DeleteUserId { get; set; }


    }
}
