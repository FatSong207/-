using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Quartz.Dtos
{
    /// <summary>
    /// 定時任務執行日志輸出對象模型
    /// </summary>
    [Serializable]
    public class TaskJobsLogOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取任務Id
        /// </summary>
        [MaxLength(50)]
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
        /// 設置或獲取執行狀態 成功、失敗
        /// </summary>
        public bool Status { get; set; }


        /// <summary>
        /// 設置或獲取結果描述
        /// </summary>
        [MaxLength(2147483647)]
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime CreatorTime { get; set; }


    }
}
