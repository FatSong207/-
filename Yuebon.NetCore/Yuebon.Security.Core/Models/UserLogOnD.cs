using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 用戶登錄信息，數據實體對象
    /// </summary>
    [Table("Sys_UserLogOnD")]
    [Serializable]
    public class UserLogOnD:BaseEntity<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string UserSecretkey { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? AllowStartTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? AllowEndTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LockStartDate { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LockEndDate { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? FirstVisitTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? PreviousVisitTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastVisitTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? ChangePasswordDate { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? MultiUserLogin { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? LogOnCount { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? UserOnLine { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Question { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string AnswerQuestion { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? CheckIPAddress { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Theme { get; set; }

    }
}
