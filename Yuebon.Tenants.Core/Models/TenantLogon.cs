using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Tenants.Models
{
    /// <summary>
    /// 用戶登錄信息，數據實體對象
    /// </summary>
    [Table("sys_TenantLogon")]
    [Serializable]
    public class TenantLogon:BaseEntity<string>
    {
        /// <summary>
        /// 設置或獲取
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 設置或獲取密碼
        /// </summary>
        public string TenantPassword { get; set; }

        /// <summary>
        /// 設置或獲取加密密鑰
        /// </summary>
        public string TenantSecretkey { get; set; }

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
        public bool? TenantOnLine { get; set; }

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
        /// 設置或獲取軟件語言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 設置或獲取軟件風格設置信息
        /// </summary>
        public string Theme { get; set; }


    }
}
