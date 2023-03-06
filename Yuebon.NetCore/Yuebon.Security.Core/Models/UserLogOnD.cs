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

        /// 設置或獲取 
        /// </summary>
        public string UserId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string UserPassword { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string UserSecretkey { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? AllowStartTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? AllowEndTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? LockStartDate { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? LockEndDate { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? FirstVisitTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? PreviousVisitTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? LastVisitTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? ChangePasswordDate { get; set; }

        /// 設置或獲取 
        /// </summary>
        public bool? MultiUserLogin { get; set; }

        /// 設置或獲取 
        /// </summary>
        public int? LogOnCount { get; set; }

        /// 設置或獲取 
        /// </summary>
        public bool? UserOnLine { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Question { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string AnswerQuestion { get; set; }

        /// 設置或獲取 
        /// </summary>
        public bool? CheckIPAddress { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Language { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Theme { get; set; }

    }
}