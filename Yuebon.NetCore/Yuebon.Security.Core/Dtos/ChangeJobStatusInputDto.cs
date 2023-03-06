using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 定時任務改變狀態
    /// </summary>
    [Serializable]
    public class ChangeJobStatusReq
    {
        /// <summary>
        /// 任務ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 改變任務狀態
        /// 0：停止；1：啟動（任務變成正在運行）
        /// </summary>
        public int Status { get; set; }
    }
}
