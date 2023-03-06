
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Quartz.Models
{
    /// <summary>
    /// 定時任務，數據實體對象
    /// </summary>
    [Table("Sys_TaskManager")]
    [Serializable]
    public class TaskManager:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        public TaskManager()
        {
            this.RunCount = 0;
            this.ErrorCount = 0;
            this.NextRunTime = DateTime.Now;
            this.LastRunTime = DateTime.Now;
            this.JobCallParams = string.Empty;
            this.Cron = string.Empty;
            this.Status = 0;
            this.CompanyId = "";
            this.DeptId = "";
            this.DeleteUserId = "";
        }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [Key]
        public override string Id { get; set; }
        /// <summary>
        /// 設置或獲取任務名稱
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 設置或獲取任務分組
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 設置或獲取結束時間
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 設置或獲取開始時間
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 設置或獲取CRON表達式
        /// </summary>
        public string Cron { get; set; }

        /// <summary>
        /// 設置或獲取是否是本地任務1：本地任務；0：外部接口任務
        /// </summary>
        public bool IsLocal { get; set; }

        /// <summary>
        /// 設置或獲取遠程調用接口url
        /// </summary>
        public string JobCallAddress { get; set; }

        /// <summary>
        /// 設置或獲取任務參數，JSON格式
        /// </summary>
        public string JobCallParams { get; set; }

        /// <summary>
        /// 設置或獲取最后一次執行時間
        /// </summary>
        public DateTime? LastRunTime { get; set; }

        /// <summary>
        /// 設置或獲取最后一次失敗時間
        /// </summary>
        public DateTime? LastErrorTime { get; set; }

        /// <summary>
        /// 設置或獲取下次執行時間
        /// </summary>
        public DateTime? NextRunTime { get; set; }

        /// <summary>
        /// 設置或獲取執行次數
        /// </summary>
        public int RunCount { get; set; }

        /// <summary>
        /// 設置或獲取異常次數
        /// </summary>
        public int ErrorCount { get; set; }

        /// <summary>
        /// 設置或獲取描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取狀態，0-暫停，1-啟用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 設置或獲取是否郵件通知
        /// </summary>
        public int SendMail { get; set; }
        /// <summary>
        /// 設置或獲取接受郵件地址
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 設置或獲取是否可用
        /// </summary>
        public bool? EnabledMark { get; set; }

        /// <summary>
        /// 設置或獲取刪除標記
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 設置或獲取創建人
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取創建人組織
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 設置或獲取部門
        /// </summary>
        public string DeptId { get; set; }

        /// <summary>
        /// 設置或獲取修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 設置或獲取修改人
        /// </summary>
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 設置或獲取刪除時間
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 設置或獲取刪除人
        /// </summary>
        public string DeleteUserId { get; set; }


    }
}
