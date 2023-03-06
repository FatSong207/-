
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 用戶登錄信息表，數據實體對象
    /// </summary>
    [Table("Sys_UserLogOn")]
    [Serializable]
    public class UserLogOn: BaseEntity<string>
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public UserLogOn()
		{
        }

        #region Property Members


        /// <summary>
        /// 用戶主鍵
        /// </summary>
        public virtual string UserId { get; set; }

        /// <summary>
        /// 用戶密碼
        /// </summary>
        public virtual string UserPassword { get; set; }

        /// <summary>
        /// 用戶秘鑰
        /// </summary>
        public virtual string UserSecretkey { get; set; }

        /// <summary>
        /// 允許登錄時間開始
        /// </summary>
        public virtual DateTime? AllowStartTime { get; set; }

        /// <summary>
        /// 允許登錄時間結束
        /// </summary>
        public virtual DateTime? AllowEndTime { get; set; }

        /// <summary>
        /// 暫停用戶開始日期
        /// </summary>
        public virtual DateTime? LockStartDate { get; set; }

        /// <summary>
        /// 暫停用戶結束日期
        /// </summary>
        public virtual DateTime? LockEndDate { get; set; }

        /// <summary>
        /// 第一次訪問時間
        /// </summary>
        public virtual DateTime? FirstVisitTime { get; set; }

        /// <summary>
        /// 上一次訪問時間
        /// </summary>
        public virtual DateTime? PreviousVisitTime { get; set; }

        /// <summary>
        /// 最后訪問時間
        /// </summary>
        public virtual DateTime? LastVisitTime { get; set; }

        /// <summary>
        /// 最后修改密碼日期
        /// </summary>
        public virtual DateTime? ChangePasswordDate { get; set; }

        /// <summary>
        /// 允許同時有多用戶登錄
        /// </summary>
        public virtual bool? MultiUserLogin { get; set; }

        /// <summary>
        /// 登錄次數
        /// </summary>
        public virtual int? LogOnCount { get; set; }

        /// <summary>
        /// 在線狀態
        /// </summary>
        public virtual bool? UserOnLine { get; set; }

        /// <summary>
        /// 密碼提示問題
        /// </summary>
        public virtual string Question { get; set; }

        /// <summary>
        /// 密碼提示答案
        /// </summary>
        public virtual string AnswerQuestion { get; set; }

        /// <summary>
        /// 是否訪問限制
        /// </summary>
        public virtual bool? CheckIPAddress { get; set; }

        /// <summary>
        /// 系統語言
        /// </summary>
        public virtual string Language { get; set; }

        /// <summary>
        /// 系統樣式
        /// </summary>
        public virtual string Theme { get; set; }

        #endregion

    }
}