using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Commons.Attributes
{
    /// <summary>
    /// 數據庫上下文配置
    /// </summary>
    public class AppDbContextFactoryAttribute : Attribute
    {
        /// <summary>
        /// 數據庫配置名稱
        /// </summary>
        public string DbConfigName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConfigName">數據庫配置名稱</param>
        public AppDbContextFactoryAttribute(string dbConfigName)
        {
            DbConfigName = dbConfigName;
        }
    }

}
