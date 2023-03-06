using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Yuebon.Commons.Core.App;

namespace Yuebon.Commons.IoC
{
    /// <summary>
    ///IOC 容器
    /// </summary>
    public class IoCContainer
    {
        /// <summary>
        /// 從容器中獲取對象 Resolve an instance of the default requested type from the container
        /// <para>
        /// 該方法即將廢棄，請使用App.GetService<T>();
        /// </para>
        /// </summary>
        /// <typeparam name="T">類型</typeparam>
        /// <returns></returns>
        public static T Resolve<T>() where T : class
        {
            return App.GetService<T>();
        }

    }
}