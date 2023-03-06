using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Core.DataManager
{
    /// <summary>
    /// 數據庫連接配置特性
    /// </summary>
    public class AppDBContextAttribute : Attribute
    {
        /// <summary>
        /// 數據庫配置名稱
        /// </summary>
        public string DbConfigName { get; set; }
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="dbConfigName"></param>
        public AppDBContextAttribute(string dbConfigName)
        {
            DbConfigName = dbConfigName;
        }
    }
}
