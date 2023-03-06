using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Reflection;
using Yuebon.Commons.Core.App;
using Yuebon.Commons.DbContextCore;
using Yuebon.Commons.DependencyInjection;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.IoC;
using Yuebon.Commons.IRepositories;
using Yuebon.Commons.Models;
using Yuebon.Commons.Options;
using Yuebon.Commons.Repositories;

namespace Yuebon.Commons.Extensions
{
    /// <summary>
    /// IServiceCollection自定義擴展
    /// </summary>
    public static class AppServiceCollectionExtensions
    {
        #region 用DI批量注入接口程序集中對應的實現類，接口和實現類在一個程序集中。
        /// <summary>
        /// 用DI批量注入接口程序集中對應的實現類。
        /// 針對每一次服務提供請求，IServiceProvider對象總是創建一個新的服務實例
        /// <para>
        /// 需要注意的是，這里有如下約定：
        /// IUserService --> UserService, IUserRepository --> UserRepository.
        /// </para>
        /// </summary>
        /// <param name="service"></param>
        /// <param name="interfaceAssemblyName">接口程序集的名稱（不包含文件擴展名）</param>
        /// <returns></returns>
        public static IServiceCollection AddTransientAssembly(this IServiceCollection service, string interfaceAssemblyName)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (string.IsNullOrEmpty(interfaceAssemblyName))
                throw new ArgumentNullException(nameof(interfaceAssemblyName));

            var assembly = RuntimeHelper.GetAssembly(interfaceAssemblyName);
            if (assembly == null)
            {
                throw new DllNotFoundException($"the dll \"{interfaceAssemblyName}\" not be found");
            }

            //過濾掉非接口及泛型接口
            var types = assembly.GetTypes().Where(t => t.GetTypeInfo().IsInterface && !t.GetTypeInfo().IsGenericType);

            foreach (var type in types)
            {
                var implementTypeName = type.Name.Substring(1);
                var implementType = RuntimeHelper.GetImplementType(implementTypeName, type);
                if (implementType != null)
                    service.AddTransient(type, implementType);
            }
            return service;
        }
        /// <summary>
        /// 用DI批量注入接口程序集中對應的實現類。
        /// 在同一個作用域內只初始化一個實例 ，可以理解為每一個請求只創建一個實例，同一個請求會在一個作用域內。在Scooped的生存周期內，如果容器釋放 它也就被釋放了
        /// <para>
        /// 需要注意的是，這里有如下約定：
        /// IUserService --> UserService, IUserRepository --> UserRepository.
        /// </para>
        /// </summary>
        /// <param name="service"></param>
        /// <param name="interfaceAssemblyName">接口程序集的名稱（不包含文件擴展名）</param>
        /// <returns></returns>
        public static IServiceCollection AddScopedAssembly(this IServiceCollection service, string interfaceAssemblyName)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (string.IsNullOrEmpty(interfaceAssemblyName))
                throw new ArgumentNullException(nameof(interfaceAssemblyName));

            var assembly = RuntimeHelper.GetAssembly(interfaceAssemblyName);
            if (assembly == null)
            {
                throw new DllNotFoundException($"the dll \"{interfaceAssemblyName}\" not be found");
            }

            //過濾掉非接口及泛型接口
            var types = assembly.GetTypes().Where(t => t.GetTypeInfo().IsInterface && !t.GetTypeInfo().IsGenericType);

            foreach (var type in types)
            {
                var implementTypeName = type.Name.Substring(1);
                var implementType = RuntimeHelper.GetImplementType(implementTypeName, type);
                if (implementType != null)
                    service.AddScoped(type, implementType);
            }
            return service;
        }

        /// <summary>
        /// 用DI批量注入接口程序集中對應的實現類。
        /// 整個應用程序生命周期以內只創建一個實例，后續每個請求都使用相同的實例。如果應用程序需要單例行為，建議讓服務容器管理服務的生命周期，而不是在自己的類中實現單例模式。
        /// <para>
        /// 需要注意的是，這里有如下約定：
        /// IUserService --> UserService, IUserRepository --> UserRepository.
        /// </para>
        /// </summary>
        /// <param name="service"></param>
        /// <param name="interfaceAssemblyName">接口程序集的名稱（不包含文件擴展名）</param>
        /// <returns></returns>
        public static IServiceCollection AddSingletonAssembly(this IServiceCollection service, string interfaceAssemblyName)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (string.IsNullOrEmpty(interfaceAssemblyName))
                throw new ArgumentNullException(nameof(interfaceAssemblyName));

            var assembly = RuntimeHelper.GetAssembly(interfaceAssemblyName);
            if (assembly == null)
            {
                throw new DllNotFoundException($"the dll \"{interfaceAssemblyName}\" not be found");
            }

            //過濾掉非接口及泛型接口
            var types = assembly.GetTypes().Where(t => t.GetTypeInfo().IsInterface && !t.GetTypeInfo().IsGenericType);

            foreach (var type in types)
            {
                var implementTypeName = type.Name.Substring(1);
                var implementType = RuntimeHelper.GetImplementType(implementTypeName, type);
                if (implementType != null)
                    service.AddSingleton(type, implementType);
            }
            return service;
        }
        #endregion

        #region 用DI批量注入接口程序集中對應的實現類，接口和實現類在獨立的程序集中。
        /// <summary>
        /// 用DI批量注入接口程序集中對應的實現類。
        /// </summary>
        /// <param name="service"></param>
        /// <param name="interfaceAssemblyName">接口程序集的名稱（不包含文件擴展名）</param>
        /// <param name="implementAssemblyName">實現程序集的名稱（不包含文件擴展名）</param>
        /// <returns></returns>
        public static IServiceCollection AddScopedAssembly(this IServiceCollection service, string interfaceAssemblyName, string implementAssemblyName)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (string.IsNullOrEmpty(interfaceAssemblyName))
                throw new ArgumentNullException(nameof(interfaceAssemblyName));
            if (string.IsNullOrEmpty(implementAssemblyName))
                throw new ArgumentNullException(nameof(implementAssemblyName));

            var interfaceAssembly = RuntimeHelper.GetAssembly(interfaceAssemblyName);
            if (interfaceAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{interfaceAssemblyName}\" not be found");
            }

            var implementAssembly = RuntimeHelper.GetAssembly(implementAssemblyName);
            if (implementAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{implementAssemblyName}\" not be found");
            }

            //過濾掉非接口及泛型接口
            var types = interfaceAssembly.GetTypes().Where(t => t.GetTypeInfo().IsInterface && !t.GetTypeInfo().IsGenericType);

            foreach (var type in types)
            {
                //過濾掉抽象類、泛型類以及非class
                var implementType = implementAssembly.DefinedTypes
                    .FirstOrDefault(t => t.IsClass && !t.IsAbstract && !t.IsGenericType &&
                                         t.GetInterfaces().Any(b => b.Name == type.Name));
                if (implementType != null)
                {
                    service.AddScoped(type, implementType.AsType());
                }
            }

            return service;
        }

        /// <summary>
        /// 用DI批量注入接口程序集中對應的實現類。
        /// </summary>
        /// <param name="service"></param>
        /// <param name="interfaceAssemblyName">接口程序集的名稱（不包含文件擴展名）</param>
        /// <param name="implementAssemblyName">實現程序集的名稱（不包含文件擴展名）</param>
        /// <returns></returns>
        public static IServiceCollection AddTransientAssembly(this IServiceCollection service, string interfaceAssemblyName, string implementAssemblyName)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (string.IsNullOrEmpty(interfaceAssemblyName))
                throw new ArgumentNullException(nameof(interfaceAssemblyName));
            if (string.IsNullOrEmpty(implementAssemblyName))
                throw new ArgumentNullException(nameof(implementAssemblyName));

            var interfaceAssembly = RuntimeHelper.GetAssembly(interfaceAssemblyName);
            if (interfaceAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{interfaceAssemblyName}\" not be found");
            }

            var implementAssembly = RuntimeHelper.GetAssembly(implementAssemblyName);
            if (implementAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{implementAssemblyName}\" not be found");
            }

            //過濾掉非接口及泛型接口
            var types = interfaceAssembly.GetTypes().Where(t => t.GetTypeInfo().IsInterface && !t.GetTypeInfo().IsGenericType);

            foreach (var type in types)
            {
                //過濾掉抽象類、泛型類以及非class
                var implementType = implementAssembly.DefinedTypes
                    .FirstOrDefault(t => t.IsClass && !t.IsAbstract && !t.IsGenericType &&
                                         t.GetInterfaces().Any(b => b.Name == type.Name));
                if (implementType != null)
                {
                    service.AddTransient(type, implementType.AsType());
                }
            }

            return service;
        }

        /// <summary>
        /// 用DI批量注入接口程序集中對應的實現類。
        /// </summary>
        /// <param name="service"></param>
        /// <param name="interfaceAssemblyName">接口程序集的名稱（不包含文件擴展名）</param>
        /// <param name="implementAssemblyName">實現程序集的名稱（不包含文件擴展名）</param>
        /// <returns></returns>
        public static IServiceCollection AddSingletonAssembly(this IServiceCollection service, string interfaceAssemblyName, string implementAssemblyName)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (string.IsNullOrEmpty(interfaceAssemblyName))
                throw new ArgumentNullException(nameof(interfaceAssemblyName));
            if (string.IsNullOrEmpty(implementAssemblyName))
                throw new ArgumentNullException(nameof(implementAssemblyName));

            var interfaceAssembly = RuntimeHelper.GetAssembly(interfaceAssemblyName);
            if (interfaceAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{interfaceAssemblyName}\" not be found");
            }

            var implementAssembly = RuntimeHelper.GetAssembly(implementAssemblyName);
            if (implementAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{implementAssemblyName}\" not be found");
            }

            //過濾掉非接口及泛型接口
            var types = interfaceAssembly.GetTypes().Where(t => t.GetTypeInfo().IsInterface && !t.GetTypeInfo().IsGenericType);

            foreach (var type in types)
            {
                //過濾掉抽象類、泛型類以及非class
                var implementType = implementAssembly.DefinedTypes
                    .FirstOrDefault(t => t.IsClass && !t.IsAbstract && !t.IsGenericType &&
                                         t.GetInterfaces().Any(b => b.Name == type.Name));
                if (implementType != null)
                {
                    service.AddSingleton(type, implementType.AsType());
                }
            }

            return service;
        }

        #endregion

        #region 注入控制器Controler
        /// <summary>
        /// 注入Controler
        /// </summary>
        /// <param name="service"></param>
        /// <param name="controllerAssemblyName"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterControllers(this IServiceCollection service,
            string controllerAssemblyName)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (string.IsNullOrEmpty(controllerAssemblyName))
                throw new ArgumentNullException(nameof(controllerAssemblyName));
            var controllerAssembly = RuntimeHelper.GetAssembly(controllerAssemblyName);
            if (controllerAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{controllerAssemblyName}\" not be found");
            }

            //過濾掉非接口及泛型接口
            var types = controllerAssembly.GetTypes().Where(t =>
            {
                var typeInfo = t.GetTypeInfo();
                return typeInfo.IsClass && !typeInfo.IsAbstract && !typeInfo.IsGenericType && t.IsAssignableFrom(typeof(Controller));
            });

            foreach (var type in types)
            {
                service.AddScoped(type);
            }

            return service;
        }

        #endregion


        #region 數據庫上下文相關服務注入
        /// <summary>
        /// 註冊數據庫上下文工廠
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbContextFactory(this IServiceCollection services,
            Action<DbContextFactory> action)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            var factory = DbContextFactory.Instance;
            factory.ServiceCollection = services;
            action?.Invoke(factory);
            return factory.ServiceCollection;
        }

        /// <summary>
        /// 注入數據庫上下文
        /// </summary>
        /// <typeparam name="IT"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="option">數據庫上下文配置參數</param>
        /// <returns></returns>
        public static IServiceCollection AddDbContext<IT, T>(this IServiceCollection services, DbContextOption option) where IT : IDbContextCore where T : BaseDbContext, IT
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (option == null) throw new ArgumentNullException(nameof(option));
            services.AddSingleton(option);
            return services.AddDbContext<IT, T>(option);
        }

        /// <summary>
        /// 注入數據庫上下文
        /// </summary>
        /// <typeparam name="IT"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbContext<IT, T>(this IServiceCollection services) where IT : IDbContextCore where T : BaseDbContext, IT
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            return services.AddDbContext<IT, T>();
        }

        /// <summary>
        /// 獲取數據庫上下文
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="dbContextTagName">上下文標簽名稱</param>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static object GetDbContext(this IServiceProvider provider, string dbContextTagName, Type serviceType)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            var implService = provider.GetRequiredService(serviceType);
            var option = provider.GetServices<DbContextOption>().FirstOrDefault(m => m.dbConfigName == dbContextTagName);

            var context = Activator.CreateInstance(implService.GetType(), option);

            return context;
        }
        #endregion



        #region  註冊倉儲Repositories

        /// <summary>
        /// 註冊倉儲Repositories
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDefaultRepositories<T>(this IServiceCollection services) where T : DbContext, new()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var list = assembly.GetTypes().Where(t => t.GetCustomAttributes<DbContextAttribute>()
                                                          .Any(a => a.ContextType == typeof(T))
                                                      && !t.GetCustomAttributes<MigrationAttribute>().Any() &&
                                                      !t.FullName.Contains("Migrations")).ToList();
            if (list.Any())
            {
                foreach (var type in list)
                {
                    var pkType = GetPrimaryKeyType(type);
                    var implType = GetRepositoryType(type, pkType);
                    if (pkType != null)
                    {
                        services.TryAddScoped(typeof(IRepository<,>).MakeGenericType(type, pkType), implType);
                    }
                }
            }
            return services;
        }
        /// <summary>
        /// 獲取繼承倉儲BaseRepository的所有倉儲類型
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="primaryKeyType"></param>
        /// <returns></returns>
        private static Type GetRepositoryType(Type entityType, Type primaryKeyType)
        {
            return typeof(BaseRepository<,>).MakeGenericType(entityType, primaryKeyType);
        }


        /// <summary>
        /// 獲取繼承Entity的所有實體類型主鍵類型
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        private static Type GetPrimaryKeyType(Type entityType)
        {
            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(Entity))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }

            return null;
        }
        #endregion


        /// <summary>
        /// 添加自動掃描注入Service服務和Respository倉儲
        /// 
        /// <para>
        /// 需要注意的是，遵循如下約定：
        /// IUserService --> UserService, IUserRepository --> UserRepository.
        /// </para>
        /// </summary>
        /// <param name="services">服務集合</param>
        /// <returns>服務集合</returns>
        public static IServiceCollection AddAutoScanInjection(this IServiceCollection services)
        {
            RuntimeHelper.GetAllYuebonAssemblies().ToList().ForEach(a =>
            {
                a.GetTypes().Where(t => typeof(IPrivateDependency).IsAssignableFrom(t) && t.IsClass).ToList().ForEach(t =>
                {
                    var serviceType = t.GetInterface($"I{t.Name}");
                    if ((serviceType ?? t).GetInterface(typeof(ISingletonDependency).Name) != null)
                    {
                        if (serviceType != null)
                        {
                            services.AddSingleton(serviceType, t);
                        }
                        else
                        {
                            services.AddSingleton(t);
                        }
                    }
                    else if ((serviceType ?? t).GetInterface(typeof(IScopedDependency).Name) != null)
                    {
                        if (serviceType != null)
                        {
                            services.AddScoped(serviceType, t);
                        }
                        else
                        {
                            services.AddScoped(t);
                        }
                    }
                    else if ((serviceType ?? t).GetInterface(typeof(ITransientDependency).Name) != null)
                    {
                        if (serviceType != null)
                        {
                            services.AddTransient(serviceType, t);
                        }
                        else
                        {
                            services.AddTransient(t);
                        }
                    }
                    else
                    {
                        if (serviceType != null)
                        {
                            services.AddTransient(serviceType, t);
                        }
                        else
                        {
                            services.AddTransient(t);
                        }
                    }
                });
            });
            return services;
        }
    }
}
