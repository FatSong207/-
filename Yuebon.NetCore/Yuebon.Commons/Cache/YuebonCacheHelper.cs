using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Core.App;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IoC;
using Yuebon.Commons.Json;
using Yuebon.Commons.Options;

namespace Yuebon.Commons.Cache
{
    /// <summary>
    /// 緩存操作幫助類
    /// </summary>
    public class YuebonCacheHelper
    {
        /// <summary>
        /// 緩存提供模式
        /// </summary>
        private static CacheProvider cacheProvider;
        /// <summary>
        /// 緩存接口
        /// </summary>
        private ICacheService cacheservice;
        /// <summary>
        /// 
        /// </summary>
        public YuebonCacheHelper()
        {

            cacheProvider = App.GetService<CacheProvider>();
            if (cacheProvider == null)
            {
                throw new ArgumentNullException(nameof(cacheProvider));
            }
            else
            {
                cacheservice= App.GetService<ICacheService>();
            }
        }

        /// <summary>
        /// 使用MemoryCache緩存操作
        /// </summary>
        /// <param name="isMemoryCache">是否使用MemoryCache</param>
        public YuebonCacheHelper(bool isMemoryCache=false)
        {

            cacheProvider = App.GetService<CacheProvider>();
            if (cacheProvider == null)
            {
                throw new ArgumentNullException(nameof(cacheProvider));
            }
            else
            {
                if (isMemoryCache)
                {
                    cacheservice = App.GetService<MemoryCacheService>();
                }
                else
                {
                    cacheservice = App.GetService<ICacheService>();

                }
            }
        }
        #region 驗證緩存項是否存在
        /// <summary>
        /// 驗證緩存項是否存在,TryGetValue 來檢測 Key是否存在的
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return cacheservice.Exists(key);
        }
        #endregion

        #region 添加緩存

        /// <summary>
        /// 添加緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <param name="value">緩存Value</param>
        /// <returns></returns>
        public bool Add(string key, object value)
        {
            return cacheservice.Add(key, value);
        }
        /// <summary>
        /// 添加緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <param name="value">緩存Value</param>
        /// <param name="expiresSliding">滑動過期時長（如果在過期時間內有操作，則以當前時間點延長過期時間）</param>
        /// <param name="expiressAbsoulte">絕對過期時長</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            return cacheservice.Add(key, value, expiresSliding, expiressAbsoulte);
        }
        /// <summary>
        /// 添加緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <param name="value">緩存Value</param>
        /// <param name="expiresIn">緩存時長</param>
        /// <param name="isSliding">是否滑動過期（如果在過期時間內有操作，則以當前時間點延長過期時間）</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            return cacheservice.Add(key,value,expiresIn,isSliding);
        }
        #endregion

        #region 刪除緩存

        /// <summary>
        /// 刪除緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return cacheservice.Remove(key);
        }
        /// <summary>
        /// 批量刪除緩存
        /// </summary>
        /// <param name="keys">緩存Key集合</param>
        /// <returns></returns>
        public void RemoveAll(IEnumerable<string> keys)
        {
            cacheservice.RemoveAll(keys);
        }
        /// <summary>
        /// 刪除匹配到的緩存
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public void RemoveByPattern(string pattern)
        {
            cacheservice.RemoveByPattern(pattern);
        }
        /// <summary>
        /// 刪除所有緩存
        /// </summary>
        public void RemoveCacheAll()
        {
            cacheservice.RemoveCacheAll();
        }
        #endregion

        #region 獲取緩存
        /// <summary>
        /// 獲取緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <returns></returns>
        public T Get<T>(string key) where T : class
        {
            return cacheservice.Get<T>(key);
        }
        /// <summary>
        /// 獲取緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <returns></returns>
        public object Get(string key)
        {
            //避免程式開發 不斷遺失SysSetting
            if(key == "SysSetting")
            {
                if(cacheservice.Get(key) == null)
                {
                    YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                    SysSetting sysSetting = XmlConverter.Deserialize<SysSetting>("xmlconfig/sys.config");
                    if (sysSetting != null)
                    {
                        cacheservice.Add("SysSetting", sysSetting);
                    }
                }
            }

            return cacheservice.Get(key);
        }
        /// <summary>
        /// 獲取緩存集合
        /// </summary>
        /// <param name="keys">緩存Key集合</param>
        /// <returns></returns>
        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            return cacheservice.GetAll(keys);
        }

        #endregion

        #region 修改緩存
        /// <summary>
        /// 修改緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <param name="value">新的緩存Value</param>
        /// <returns></returns>
        public bool Replace(string key, object value)
        {
            return cacheservice.Replace(key, value);

        }
        /// <summary>
        /// 修改緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <param name="value">新的緩存Value</param>
        /// <param name="expiresSliding">滑動過期時長（如果在過期時間內有操作，則以當前時間點延長過期時間）</param>
        /// <param name="expiressAbsoulte">絕對過期時長</param>
        /// <returns></returns>
        public bool Replace(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            return cacheservice.Replace(key, value, expiresSliding, expiressAbsoulte);
        }
        /// <summary>
        /// 修改緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <param name="value">新的緩存Value</param>
        /// <param name="expiresIn">緩存時長</param>
        /// <param name="isSliding">是否滑動過期（如果在過期時間內有操作，則以當前時間點延長過期時間）</param>
        /// <returns></returns>
        public bool Replace(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            return cacheservice.Replace(key,value,expiresIn,isSliding);
        }
        #endregion

    }
}
