using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Tenants.Dtos
{
    /// <summary>
    /// 用戶登錄信息輸出對象模型
    /// </summary>
    [Serializable]
    public class TenantLogonOutputDto
    {
        /// <summary>
        /// 設置或獲取
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取
        /// </summary>
        [MaxLength(50)]
        public string TenantId { get; set; }

        /// <summary>
        /// 設置或獲取密碼
        /// </summary>
        [MaxLength(50)]
        public string TenantPassword { get; set; }

        /// <summary>
        /// 設置或獲取加密密鑰
        /// </summary>
        [MaxLength(50)]
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
        [MaxLength(50)]
        public string Question { get; set; }

        /// <summary>
        /// 設置或獲取
        /// </summary>
        [MaxLength(500)]
        public string AnswerQuestion { get; set; }

        /// <summary>
        /// 設置或獲取
        /// </summary>
        public bool? CheckIPAddress { get; set; }

        /// <summary>
        /// 設置或獲取軟件語言
        /// </summary>
        [MaxLength(50)]
        public string Language { get; set; }

        /// <summary>
        /// 設置或獲取軟件風格設置信息
        /// </summary>
        [MaxLength(16777215)]
        public string Theme { get; set; }


    }
}
