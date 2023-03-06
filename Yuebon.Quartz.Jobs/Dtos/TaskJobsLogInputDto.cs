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
    /// 定時任務執行日志輸入對象模型
    /// </summary>
    [AutoMap(typeof(TaskJobsLog))]
    [Serializable]
    public class TaskJobsLogInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取任務Id
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// 設置或獲取任務名稱
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 設置或獲取任務執行動作開始、暫停、結束
        /// </summary>
        public string JobAction { get; set; }
        /// <summary>
        /// 設置或獲取執行狀態 成功、是啊比阿
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
