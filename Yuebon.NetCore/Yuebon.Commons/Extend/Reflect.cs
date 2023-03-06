using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Yuebon.Commons.Extend
{
    /// <summary>
    /// 根據業務對象的類型進行反射操作輔助類
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Reflect<T> where T : class
    {
        private static Hashtable ObjCache = new Hashtable();
        private static object syncRoot = new Object();

        /// <summary>
        /// 根據參數創建對象實例
        /// </summary>
        /// <param name="sName">對象全局名稱</param>
        /// <param name="sFilePath">文件路徑</param>
        /// <returns></returns>
        public static T Create(string sName, string sFilePath)
        {
            return Create(sName, sFilePath, true);
        }

        /// <summary>
        /// 根據參數創建對象實例
        /// </summary>
        /// <param name="sName">對象全局名稱</param>
        /// <param name="sFilePath">文件路徑</param>
        /// <param name="bCache">緩存集合</param>
        /// <returns></returns>
        public static T Create(string sName, string sFilePath, bool bCache)
        {
            string CacheKey = sName;
            T objType = null;
            if (bCache)
            {
                objType = (T)ObjCache[CacheKey];    //從緩存讀取 
                if (!ObjCache.ContainsKey(CacheKey))
                {
                    lock (syncRoot)
                    {
                        objType = CreateInstance(CacheKey, sFilePath);
                        ObjCache.Add(CacheKey, objType);//緩存數據訪問對象
                    }
                }
            }
            else
            {
                objType = CreateInstance(CacheKey, sFilePath);
            }

            return objType;
        }

        /// <summary>
        /// 根據全名和路徑構造對象
        /// </summary>
        /// <param name="sName">對象全名</param>
        /// <param name="sFilePath">程序集路徑</param>
        /// <returns></returns>
        private static T CreateInstance(string sName, string sFilePath)
        {
            Assembly assemblyObj = Assembly.Load(sFilePath);
            if (assemblyObj == null)
            {
                throw new ArgumentNullException("sFilePath", string.Format("無法加載sFilePath={0} 的程序集", sFilePath));
            }

            T obj = (T)assemblyObj.CreateInstance(sName); //反射創建 
            return obj;
        }
    }
}
