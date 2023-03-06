using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Models
{
    /// <summary>
    /// 定義可過期性，包含生效時間和過期時間：給實體添加 生效時間BeginTime，過期時間EndTime 的屬性
    /// </summary>
    public interface IExpirable
    {
        /// <summary>
        /// 獲取或設置 生效時間
        /// </summary>
        DateTime? BeginTime { get; set; }

        /// <summary>
        /// 獲取或設置 過期時間
        /// </summary>
        DateTime? EndTime { get; set; }
    }
}
