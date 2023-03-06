using Microsoft.Extensions.Configuration;
using Yuebon.Commons.Core.App;
using Yuebon.Commons.IoC;

namespace Yuebon.Commons
{
    /// <summary>
    /// 配置文件讀取操作
    /// </summary>
    public class Configs
    {
        public static  IConfiguration configuration;
        static Configs()
        {
            configuration =App.GetService<IConfiguration>();
        
        }
        /// <summary>
        /// 根據Key獲取數配置內容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IConfigurationSection GetSection(string key)
        {
            return configuration.GetSection(key);
        }
        /// <summary>
        /// 根據section和key獲取配置內容
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigurationValue(string section, string key)
        {
            return GetSection(section)?[key];
        }

        /// <summary>
        /// 根據Key獲取數據庫連接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            return configuration.GetConnectionString(key);
        }
    }
}
