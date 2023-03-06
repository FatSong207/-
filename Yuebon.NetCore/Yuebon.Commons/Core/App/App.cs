using AutoMapper.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Commons.Core.App
{
    /// <summary>
    /// 全局應用類
    /// </summary>
    public static class App
    {
        /// <summary>
        /// 全局配置選項
        /// </summary>
        public static Microsoft.Extensions.Configuration.IConfiguration Configuration;
        /// <summary>
        ///  應用服務
        /// </summary>
        public static IServiceCollection Services;
        /// <summary>
        /// 全局配置構建器
        /// </summary>
        private static IConfigurationBuilder ConfigurationBuilder;
        /// <summary>
        /// 私有環境變量，避免重復解析
        /// </summary>
        private static IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// 應用環境，如，是否是開發環境，生產環境等
        /// </summary>
        public static IWebHostEnvironment WebHostEnvironment => _webHostEnvironment ??= GetService<IWebHostEnvironment>();

        /// <summary>
        /// 應用有效程序集
        /// </summary>
        public static readonly IEnumerable<Assembly> Assemblies;

        /// <summary>
        /// 有效程序集類型
        /// </summary>
        public static readonly IEnumerable<Type> EffectiveTypes;

        /// <summary>
        /// 服務提供器
        /// </summary>
        public static IServiceProvider ServiceProvider => HttpContext?.RequestServices ?? Services.BuildServiceProvider();

        /// <summary>
        /// 獲取請求上下文
        /// </summary>
        public static HttpContext HttpContext => HttpContextLocal.Current();


        /// <summary>
        /// 獲取請求生命周期的服務
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService GetService<TService>()
            where TService : class
        {
            return GetService(typeof(TService)) as TService;
        }

        /// <summary>
        /// 獲取請求生命周期的服務
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetService(Type type)
        {
            return ServiceProvider.GetService(type);
        }

        /// <summary>
        /// 獲取請求生命周期的服務
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService GetRequiredService<TService>()
            where TService : class
        {
            return GetRequiredService(typeof(TService)) as TService;
        }

        /// <summary>
        /// 獲取請求生命周期的服務
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetRequiredService(Type type)
        {
            return ServiceProvider.GetRequiredService(type);
        }

        /// <summary>
        /// 獲取選項
        /// </summary>
        /// <typeparam name="TOptions">強類型選項類</typeparam>
        /// <returns>TOptions</returns>
        public static TOptions GetOptions<TOptions>()
            where TOptions : class, new()
        {
            return GetService<IOptions<TOptions>>()?.Value;
        }

        /// <summary>
        /// 獲取選項
        /// </summary>
        /// <typeparam name="TOptions">強類型選項類</typeparam>
        /// <returns>TOptions</returns>
        public static TOptions GetOptionsMonitor<TOptions>()
            where TOptions : class, new()
        {
            return GetService<IOptionsMonitor<TOptions>>()?.CurrentValue;
        }

        /// <summary>
        /// 獲取選項
        /// </summary>
        /// <typeparam name="TOptions">強類型選項類</typeparam>
        /// <returns>TOptions</returns>
        public static TOptions GetOptionsSnapshot<TOptions>()
            where TOptions : class, new()
        {
            return GetService<IOptionsSnapshot<TOptions>>()?.Value;
        }


        /// <summary>
        /// 構造函數
        /// </summary>
        static App()
        {
            // 編譯配置
           // Configuration =ConfigurationBuilder.Build();

        }

        #region


        /// <summary>
        /// 添加配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="env"></param>
        internal static void AddConfigureFiles(IConfigurationBuilder config, IHostEnvironment env)
        {
            // 讀取忽略的配置文件
            var ignoreConfigurationFiles = config.Build()
                .GetSection("IgnoreConfigurationFiles").Get<string[]>()
                ?? Array.Empty<string>();

            // 加載配置
            AutoAddJsonFiles(config, env, ignoreConfigurationFiles);
            AutoAddXmlFiles(config, env, ignoreConfigurationFiles);

            // 存儲配置
            ConfigurationBuilder = config;
        }
        /// <summary>
        /// 自動加載自定義 .json 配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="env"></param>
        /// <param name="ignoreConfigurationFiles"></param>
        public static void AutoAddJsonFiles(IConfigurationBuilder config, IHostEnvironment env, string[] ignoreConfigurationFiles)
        {
            // 獲取程序目錄下的所有配置文件
            var jsonFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.json", SearchOption.TopDirectoryOnly)
                .Union(
                    Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json", SearchOption.TopDirectoryOnly)
                )
                .Where(u => !excludeJsons.Contains(Path.GetFileName(u)) && !ignoreConfigurationFiles.Contains(Path.GetFileName(u)) && !runtimeJsonSuffixs.Any(j => u.EndsWith(j)));

            if (!jsonFiles.Any()) return;

            // 獲取環境變量名
            var envName = env.EnvironmentName;
            var envFiles = new List<string>();

            // 自動加載配置文件
            foreach (var jsonFile in jsonFiles)
            {
                // 處理帶環境的配置文件
                if (Path.GetFileNameWithoutExtension(jsonFile).EndsWith($".{envName}"))
                {
                    envFiles.Add(jsonFile);
                    continue;
                }

                config.AddJsonFile(jsonFile, optional: true, reloadOnChange: true);
            }

            // 配置帶環境的配置文件
            envFiles.ForEach(u => config.AddJsonFile(u, optional: true, reloadOnChange: true));
        }

        /// <summary>
        /// 自動加載自定義 .xml 配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="env"></param>
        /// <param name="ignoreConfigurationFiles"></param>
        public   static void AutoAddXmlFiles(IConfigurationBuilder config, IHostEnvironment env, string[] ignoreConfigurationFiles)
        {
            // 獲取程序目錄下的所有配置文件，必須以 .config.xml 結尾
            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)
                .Union(
                    Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml", SearchOption.TopDirectoryOnly)
                )
                .Where(u => !ignoreConfigurationFiles.Contains(Path.GetFileName(u)) && u.EndsWith(".config.xml", StringComparison.OrdinalIgnoreCase));

            if (!xmlFiles.Any()) return;

            // 獲取環境變量名
            var envName = env.EnvironmentName;
            var envFiles = new List<string>();

            // 自動加載配置文件
            foreach (var xmlFile in xmlFiles)
            {
                // 處理帶環境的配置文件
                if (Path.GetFileNameWithoutExtension(xmlFile).EndsWith($".{envName}.config"))
                {
                    envFiles.Add(xmlFile);
                    continue;
                }

                config.AddXmlFile(xmlFile, optional: true, reloadOnChange: true);
            }

            // 配置帶環境的配置文件
            envFiles.ForEach(u => config.AddXmlFile(u, optional: true, reloadOnChange: true));
        }

        /// <summary>
        /// 默認排除配置項
        /// </summary>
        private static readonly string[] excludeJsons = new[] {
            "appsettings.json",
            "appsettings.Development.json",
            "appsettings.Production.json",
            "bundleconfig.json",
            "bundleconfig.Development.json",
            "bundleconfig.Production.json",
            "compilerconfig.json",
            "compilerconfig.Development.json",
            "compilerconfig.Production.json"
        };

        /// <summary>
        /// 排除運行時 Json 后綴
        /// </summary>
        private static readonly string[] runtimeJsonSuffixs = new[]
        {
            "deps.json",
            "runtimeconfig.dev.json",
            "runtimeconfig.prod.json",
            "runtimeconfig.json"
        };

        #endregion
    }
}
