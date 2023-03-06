using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Commons.DataManager
{
    /// <summary>
    /// 數據庫讀、寫操作枚舉
    /// </summary>
    public enum WriteAndReadEnum
    {
        /// <summary>
        /// 寫操作
        /// </summary>
        Write,
        /// <summary>
        /// 讀操作
        /// </summary>
        Read,
        /// <summary>
        /// 默認，不區分讀寫
        /// </summary>
        Default
    }
}
