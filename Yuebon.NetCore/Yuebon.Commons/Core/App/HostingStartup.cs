using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.Extensions;
//通過HostingStartup指定要啟動的類型
[assembly: HostingStartup(typeof(Yuebon.Commons.Core.App.HostingStartup))]
namespace Yuebon.Commons.Core.App
{
    /// <summary>
    /// 配置程序啟動時自動注入
    /// </summary>
    public sealed class HostingStartup : IHostingStartup
    {
        /// <summary>
        /// 配置應用啟動
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(IWebHostBuilder builder)
        {
            //可以添加配置
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                // 自動裝載配置
                App.AddConfigureFiles(config, hostingContext.HostingEnvironment);
            });

            //可以添加ConfigureServices
            // 自動注入 AddApp() 服務
            builder.ConfigureServices(services =>
            {
                
            });
        }
    }
}
