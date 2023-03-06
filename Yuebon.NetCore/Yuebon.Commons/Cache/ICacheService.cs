using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Commons.Cache
{
    /// <summary>
    /// 緩存服務接口
    /// </summary>
    public interface ICacheService
    {
        #region  驗證緩存項是否存在
        /// <summary>
        /// 驗證緩存項是否存在
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <returns></returns>
        bool Exists(string key);


        #endregion

        #region  添加緩存
        /// <summary>
        /// 添加緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <param name="value">緩存Value</param>
        /// <returns></returns>
        bool Add(string key, object value);


        /// <summary>
        /// 添加緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <param name="value">緩存Value</param>
        /// <param name="expiresSliding">滑動過期時長（如果在過期時間內有操作，則以當前時間點延長過期時間）</param>
        /// <param name="expiressAbsoulte">絕對過期時長</param>
        /// <returns></returns>
        bool Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);


        /// <summary>
        /// 添加緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <param name="value">緩存Value</param>
        /// <param name="expiresIn">緩存時長</param>
        /// <param name="isSliding">是否滑動過期（如果在過期時間內有操作，則以當前時間點延長過期時間）</param>
        /// <returns></returns>
        bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false);

        #endregion

        #region  刪除緩存
        /// <summary>
        /// 刪除緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <returns></returns>
        bool Remove(string key);

        /// <summary>
        /// 批量刪除緩存
        /// </summary>
        /// <param name="keys">緩存Key集合</param>
        /// <returns></returns>
        void RemoveAll(IEnumerable<string> keys);


        /// <summary>
        /// 使用通配符找出所有的key然后逐個刪除
        /// </summary>
        /// <param name="pattern">通配符</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// 刪除所有緩存
        /// </summary>
        void RemoveCacheAll();
        #endregion

        #region  獲取緩存
        /// <summary>
        /// 獲取緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <returns></returns>
        T Get<T>(string key) where T : class;


        /// <summary>
        /// 獲取緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <returns></returns>
        object Get(string key);


        /// <summary>
        /// 獲取緩存集合
        /// </summary>
        /// <param name="keys">緩存Key集合</param>
        /// <returns></returns>
        IDictionary<string, object> GetAll(IEnumerable<string> keys);

        #endregion

        #region  修改緩存
        /// <summary>
        /// 修改緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <param name="value">新的緩存Value</param>
        /// <returns></returns>
        bool Replace(string key, object value);


        /// <summary>
        /// 修改緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <param name="value">新的緩存Value</param>
        /// <param name="expiresSliding">滑動過期時長（如果在過期時間內有操作，則以當前時間點延長過期時間）</param>
        /// <param name="expiressAbsoulte">絕對過期時長</param>
        /// <returns></returns>
        bool Replace(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);


        /// <summary>
        /// 修改緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <param name="value">新的緩存Value</param>
        /// <param name="expiresIn">緩存時長</param>
        /// <param name="isSliding">是否滑動過期（如果在過期時間內有操作，則以當前時間點延長過期時間）</param>
        /// <returns></returns>
        bool Replace(string key, object value, TimeSpan expiresIn, bool isSliding = false);

        #endregion

    }
}
