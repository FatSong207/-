using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Models;
using Yuebon.Quartz.Models;
using Yuebon.Security.Models;

namespace Yuebon.Quartz.Dtos
{
    /// <summary>
    /// 定時任務輸入對象模型
    /// </summary>
    [AutoMap(typeof(TaskManager))]
    [Serializable]
    public class TaskManagerInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

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
        /// 設置或獲取狀態
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 設置或獲取是否可用
        /// </summary>
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 設置或獲取是否郵件通知
        /// </summary>
        public int SendMail { get; set; }
        /// <summary>
        /// 設置或獲取接受郵件地址
        /// </summary>
        public string EmailAddress { get; set; }


    }
}
