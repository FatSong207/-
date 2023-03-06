using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.Enums;

namespace Yuebon.Commons.Core.DataManager
{
    public class YuebonDbOptions
    {
        public YuebonDbOptions()
        {
        }

        /// <summary>
        /// 默認數據庫類型
        /// </summary>
        public DatabaseType DefaultDatabaseType { get; set; } = DatabaseType.SqlServer;

        /// <summary>
        /// 數據庫連接配置
        /// </summary>
        public IDictionary<string, DbConnectionOptions> DbConnections { get; set; }
    }
}
