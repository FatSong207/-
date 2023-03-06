using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Models
{
    /// <summary>
    /// 定義數據權限的更新，刪除狀態
    /// </summary>
    public interface IDataAuthEnabled
    {
        /// <summary>
        /// 獲取或設置 是否可更新的數據權限狀態
        /// </summary>
        bool Updatable { get; set; }

        /// <summary>
        /// 獲取或設置 是否可刪除的數據權限狀態
        /// </summary>
        bool Deletable { get; set; }
    }
}
