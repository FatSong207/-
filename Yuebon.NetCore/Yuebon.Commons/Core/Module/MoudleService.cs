using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Yuebon.Commons.Repositories;
using Yuebon.Commons.IoC;
using Yuebon.Commons.Linq;
using Yuebon.Commons.Log;

namespace Yuebon.Commons.Module
{
    /// <summary>
    /// 模塊服務
    /// </summary>
    public class MoudleService
    {
        /// <summary>
        /// 實現應用模塊程序集的注冊服務
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceProvider LoaderMoudleService(IServiceCollection services)
        {
           // services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            var apps = AppContext.BaseDirectory + "Apps";
            if (!Directory.Exists(apps))
            {
                Directory.CreateDirectory(apps);
            }
            // 把 Apps 下的動態庫拷貝一份來運行，
            // 使 Apps 中的動態庫不會在運行時被占用（以便重新編譯）
            var targetPath = PrepareShadowCopies();
            // 從 Shadow Copy 目錄加載 Assembly 并注冊到 Mvc 中
            //LoadFromShadowCopies(targetPath);

            string PrepareShadowCopies()
            {
                // 準備 Shadow Copy 的目標目錄
                var target = Path.Combine(AppContext.BaseDirectory, "app_data", "apps-cache");

                if (!Directory.Exists(target))
                {
                    Directory.CreateDirectory(target);
                }
                // 找到插件目錄下 bin 目錄中的 .dll，拷貝
                 Directory.EnumerateDirectories(apps)
                    .Select(path => Path.Combine(path, "bin"))
                    .Where(Directory.Exists)
                    .SelectMany(bin => Directory.EnumerateFiles(bin, "*.dll"))
                    .ForEach(dll => File.Copy(dll, Path.Combine(target, Path.GetFileName(dll)), true));

                return target;
            }

            DirectoryInfo folder = new DirectoryInfo(targetPath);
            List<Assembly> myAssembly = new List<Assembly>();
            myAssembly.Add(Assembly.Load("Yuebon.Security.Core"));
            if (File.Exists(AppContext.BaseDirectory+ "Yuebon.Messages.Core.dll"))
            {
                myAssembly.Add(Assembly.Load("Yuebon.Messages.Core"));
            }
            foreach (FileInfo finfo in folder.GetFiles("*.Core.dll"))
            {
                try
                {
                    myAssembly.Add(Assembly.LoadFrom(finfo.FullName));
                    string dllNamespaceStr = finfo.Name.Substring(0, finfo.Name.IndexOf(".Core"));
                    IoCContainer.RegisterFrom(finfo.FullName);
                    IoCContainer.RegisterLoadFrom(finfo.FullName, dllNamespaceStr);
                    Log4NetHelper.Info("注入應用模塊" + finfo.Name + "成功");
                }
                catch (Exception ex)
                {
                    Log4NetHelper.Error("注入應用模塊" + finfo.Name + "失敗\r\n" , ex);
                }
            }

            services.AddAutoMapper(myAssembly);
            services.AddScoped<IMapper, Mapper>();

            return IoCContainer.Build(services);
        }

    }
}
