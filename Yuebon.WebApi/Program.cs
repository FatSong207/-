using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net;
using Yuebon.Commons.Core.App;

namespace Yuebon.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 啟動程序
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {

            Console.Write("start~~~");
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            App.Configuration = config;
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //https://ithelp.ithome.com.tw/articles/10197326?sc=rss.iron
                    //https://cloud.tencent.com/developer/article/1581947
                    //.ConfigureKestrel(serverOptions =>
                    //{
                    //  serverOptions.Listen(IPAddress.Any, 5000, listenOptions =>
                    //  {
                    //      listenOptions.UseHttps(
                    //      @"D:\blog.walterlv.com\ssl\blog-walterlv-com.pfx",
                    //      "Hqh#Q*QqV%@aCnx41UB%M31H");
                    //  });
                    //});
                    //Console.Write("start~~~webBuilder.UseStartup<Startup>();");
                });
        }
    }
}