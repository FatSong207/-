using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 行政工作日誌，數據實體對象
    /// </summary>
    [Table("Chaochi_AdministrativeWorkLog")]
    [Serializable]
    public class AdministrativeWorkLog:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取填表人姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 設置或獲取填表人帳號
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// 日誌日期
        /// </summary>
        public string LogDate { get; set; }
        /// <summary>
        /// 設置或獲取審核主管
        /// </summary>
        public string AuditSupervisor { get; set; }
        /// <summary>
        /// 設置或獲取審核日期
        /// </summary>
        public DateTime? AuditTime { get; set; }
        /// <summary>
        /// 設置或獲取主管備註
        /// </summary>
        public string SupervisorNote { get; set; }
        /// <summary>
        /// 設置或獲取隸屬部門
        /// </summary>
        public string BelongDept { get; set; }

        /// <summary>
        /// 代辦工作事項
        /// </summary>
        public string TodoNote { get; set; }

        /// <summary>
        /// 其他事項
        /// </summary>
        public string OtherNote { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public string States { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserDeptId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LastModifyUserId { get; set; }

    }
}
