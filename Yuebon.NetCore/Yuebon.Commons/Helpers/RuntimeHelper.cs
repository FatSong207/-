﻿using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using Yuebon.Commons.Options;

namespace Yuebon.Commons.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class RuntimeHelper
    {
        /// <summary>
        /// 獲取項目程序集，排除所有的系統程序集(Microsoft.***、System.***等)、Nuget下載包
        /// </summary>
        /// <returns></returns>
        public static IList<Assembly> GetAllAssemblies()
        {
            var list = new List<Assembly>();
            var deps = DependencyContext.Default;
            //排除所有的系統程序集、Nuget下載包
            var libs = deps.CompileLibraries.Where(lib => lib.Type == AssembleTypeConsts.Project);//只獲取本項目用到的包
            foreach (var lib in libs)
            {
                try
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                    list.Add(assembly);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return list;
        }
        /// <summary>
        /// 獲取項目程序集，排除所有的系統程序集(Microsoft.***、System.***等)、Nuget下載包和Yuebon.Commons.dll
        /// 獲取所有關于Yuebon的程序集
        /// </summary>
        /// <returns></returns>
        public static IList<Assembly> GetAllYuebonAssemblies()
        {
            var list = new List<Assembly>();
            var deps = DependencyContext.Default;
            //排除所有的系統程序集、Nuget下載包
            var libs = deps.CompileLibraries.Where(lib => lib.Type == AssembleTypeConsts.Project||lib.Name.StartsWith("Yuebon"));//只獲取本項目用到的包
            foreach (var lib in libs)
            {
                try
                {
                    if (lib.Name != "Yuebon.Commons")
                    {
                        var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                        list.Add(assembly);
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static Assembly GetAssembly(string assemblyName)
        {
            return GetAllYuebonAssemblies().FirstOrDefault(assembly => assembly.FullName.Contains(assemblyName));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IList<Type> GetAllTypes()
        {
            var list = new List<Type>();
            foreach (var assembly in GetAllAssemblies())
            {
                var typeInfos = assembly.DefinedTypes;
                foreach (var typeInfo in typeInfos)
                {
                    list.Add(typeInfo.AsType());
                }
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static IList<Type> GetTypesByAssembly(string assemblyName)
        {
            var list = new List<Type>();
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
            var typeInfos = assembly.DefinedTypes;
            foreach (var typeInfo in typeInfos)
            {
                list.Add(typeInfo.AsType());
            }
            return list;
        }
        /// <summary>
        /// 獲取實現類
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="baseInterfaceType"></param>
        /// <returns></returns>
        public static Type GetImplementType(string typeName, Type baseInterfaceType)
        {
            return GetAllTypes().FirstOrDefault(t =>
            {
                if (t.Name == typeName &&
                    t.GetTypeInfo().GetInterfaces().Any(b => b.Name == baseInterfaceType.Name))
                {
                    var typeInfo = t.GetTypeInfo();
                    return typeInfo.IsClass && !typeInfo.IsAbstract && !typeInfo.IsGenericType;
                }
                return false;
            });
        }
    }
}
