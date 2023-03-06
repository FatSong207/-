using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 行政工作日誌輸出對象模型
    /// </summary>
    [Serializable]
    public class AdministrativeWorkLogOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取填表人姓名
        /// </summary>
        [MaxLength(30)]
        public string UserName { get; set; }
        /// <summary>
        /// 設置或獲取填表人帳號
        /// </summary>
        [MaxLength(100)]
        public string UserAccount { get; set; }


        /// <summary>
        /// 日誌日期
        /// </summary>
        public DateTime? LogDate { get; set; }
        /// <summary>
        /// 設置或獲取審核主管
        /// </summary>
        [MaxLength(100)]
        public string AuditSupervisor { get; set; }

        /// <summary>
        /// 設置或獲取審核主管
        /// </summary>
        [MaxLength(100)]
        public string AuditSupervisorName { get; set; }
        /// <summary>
        /// 設置或獲取審核日期
        /// </summary>
        public DateTime? AuditTime { get; set; }
        /// <summary>
        /// 設置或獲取主管備註
        /// </summary>
        [MaxLength(1000)]
        public string SupervisorNote { get; set; }
        /// <summary>
        /// 設置或獲取隸屬部門
        /// </summary>
        [MaxLength(100)]
        public string BelongDept { get; set; }

        /// <summary>
        /// 設置或獲取隸屬部門
        /// </summary>
        [MaxLength(100)]
        public string BelongDeptName { get; set; }


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
        [MaxLength(100)]
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
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }

        public List<AdministrativeWorkLogDetails> AdministrativeWorkLogDetails { get; set; }

    }
}
