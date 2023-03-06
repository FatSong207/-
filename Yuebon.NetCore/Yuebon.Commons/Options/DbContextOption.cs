using Yuebon.Commons.Enums;

namespace Yuebon.Commons.Options
{
    /// <summary>
    /// 數據庫上下文配置
    /// </summary>
    public class DbContextOption
    {
        /// <summary>
        /// 數據庫連接字符串
        /// </summary>
        public string dbConfigName { get; set; }
        /// <summary>
        /// 實體程序集名稱
        /// </summary>
        public string ModelAssemblyName { get; set; }
        /// <summary>
        /// 數據庫類型
        /// </summary>
        public DatabaseType DbType { get; set; } = DatabaseType.SqlServer;
        /// <summary>
        /// 是否輸出Sql日志
        /// </summary>
        public bool IsOutputSql;
    }

}
