using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Quartz.Dtos
{

    /// <summary>
    /// 定時任務執行日志輸出對象模型
    /// </summary>
    [Serializable]
    public class TaskJobsLogVueTimelineOutputDto
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
        /// 設置或獲取顏色
        /// </summary>
        public string Color { get; set; }


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
