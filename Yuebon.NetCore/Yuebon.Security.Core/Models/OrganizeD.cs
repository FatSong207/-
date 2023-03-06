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
    [Table("Sys_OrganizeD")]
    [Serializable]
    public class OrganizeD:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string ParentId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public int? Layers { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string EnCode { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string FullName { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string ShortName { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string CategoryId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string ManagerId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string TelePhone { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string MobilePhone { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string WeChat { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Fax { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Email { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string AreaId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Address { get; set; }

        /// 設置或獲取 
        /// </summary>
        public bool? AllowEdit { get; set; }

        /// 設置或獲取 
        /// </summary>
        public bool? AllowDelete { get; set; }

        /// 設置或獲取 
        /// </summary>
        public int? SortCode { get; set; }

        /// 設置或獲取 
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// 設置或獲取 
        /// </summary>
        public bool? EnabledMark { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Description { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string CreatorUserId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string LastModifyUserId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string DeleteUserId { get; set; }

    }
}