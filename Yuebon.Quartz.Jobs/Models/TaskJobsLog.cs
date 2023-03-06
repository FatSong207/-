using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Quartz.Models
{
    /// <summary>
    /// 定時任務執行日志，數據實體對象
    /// </summary>
    [Table("Sys_TaskJobsLog")]
    [Serializable]
    public class TaskJobsLog:BaseEntity<string>
    {
        /// <summary>
        /// 設置或獲取任務Id
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// 設置或獲取任務名稱
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 設置或獲取執行動作
        /// </summary>
        public string JobAction { get; set; }

        /// <summary>
        /// 設置或獲取執行狀態 正常、異常
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 設置或獲取結果描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime CreatorTime { get; set; }


    }
}
