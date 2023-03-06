using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.Enums;

namespace Yuebon.Commons.Core.DataManager
{
    /// <summary>
    /// 定義主數據和從數據庫配置選項
    /// </summary>
    public class DbConnections
    {
        /// <summary>
        /// 主數據庫
        /// </summary>
        public DbConnectionOptions MassterDB {get;set; }

        /// <summary>
        /// 從數據庫
        /// </summary>
        public List<DbConnectionOptions> ReadDB { get; set; }
    }


    /// <summary>
    /// 數據庫配置選項,定義數據庫連接字符串、數據庫類型和訪問權重
    /// </summary>
    public class DbConnectionOptions
    {
        /// <summary>
        /// 數據庫連接字符
        /// </summary>
        public string ConnectionString { get;set; }

        /// <summary>
        /// 數據庫類型
        /// </summary>
        public DatabaseType DatabaseType { get; set; }

        /// <summary>
        /// 訪問權重，值越大權重越低
        /// </summary>
        public int DbLevel { get; set; }
    }
}
