using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Enums
{

    /// <summary>
    /// 數據庫類型枚舉
    /// </summary>
    public enum DatabaseType
    {
        /// <summary>
        /// SqlServer數據庫
        /// </summary>
        SqlServer = 0,
        /// <summary>
        /// Oracle數據庫
        /// </summary>
        Oracle,
        /// <summary>
        /// Access數據庫
        /// </summary>
        Access,
        /// <summary>
        /// MySql數據庫
        /// </summary>
        MySql,
        /// <summary>
        /// SQLite數據庫
        /// </summary>
        SQLite,
        /// <summary>
        /// PostgreSQL數據庫
        /// </summary>
        PostgreSQL,
        /// <summary>
        /// Npgsql數據庫
        /// </summary>
        Npgsql
    }
}
