
using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Json;

namespace Yuebon.Commons.Cache
{
    /// <summary>
    /// Redis緩存操作
    /// </summary>
    public class RedisCacheService : ICacheService
    {
        /// <summary>
        /// 
        /// </summary>
        protected IDatabase _cache;
        /// <summary>
        /// 
        /// </summary>
        private ConnectionMultiplexer _connection;
        /// <summary>
        /// 
        /// </summary>
        private readonly string _instance;

        private readonly JsonSerializerOptions _jsonOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="database"></param>
        /// <param name="jsonOptions"></param>
        public RedisCacheService(RedisCacheOptions options, JsonSerializerOptions jsonOptions, int database = 0)
        {
            _connection = ConnectionMultiplexer.Connect(options.Configuration);
            _cache = _connection.GetDatabase(database);
            _instance = options.InstanceName;
            _jsonOptions = jsonOptions;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetKeyForRedis(string key)
        {
            return _instance + key;
        }
        #region 驗證緩存項是否存在
        /// <summary>
        /// 驗證緩存項是否存在
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _cache.KeyExists(GetKeyForRedis(key));
        }
        /// <summary>
        /// 驗證緩存項是否存在
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <returns></returns>
        public Task<bool> ExistsAsync(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _cache.KeyExistsAsync(GetKeyForRedis(key));
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
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _cache.StringSet(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, _jsonOptions)));
        }
        /// <summary>
        /// 添加緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <param name="value">緩存Value</param>
        /// <param name="expiresSliding">滑動過期時長（如果在過期時間內有操作，則以當前時間點延長過期時間,Redis中無效）</param>
        /// <param name="expiressAbsoulte">絕對過期時長</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _cache.StringSet(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, _jsonOptions)), expiressAbsoulte);
        }
        /// <summary>
        /// 添加緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <param name="value">緩存Value</param>
        /// <param name="expiresIn">緩存時長</param>
        /// <param name="isSliding">是否滑動過期（如果在過期時間內有操作，則以當前時間點延長過期時間,Redis中無效）</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }


            return _cache.StringSet(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, _jsonOptions)), expiresIn);
        }


        public Task<bool> AddAsync(string key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _cache.StringSetAsync(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, _jsonOptions)));
        }

        public Task<bool> AddAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _cache.StringSetAsync(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, _jsonOptions)), expiressAbsoulte);
        }

        public Task<bool> AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _cache.StringSetAsync(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, _jsonOptions)), expiresIn);
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
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _cache.KeyDelete(GetKeyForRedis(key));
        }
        /// <summary>
        /// 批量刪除緩存
        /// </summary>
        /// <param name="key">緩存Key集合</param>
        /// <returns></returns>
        public void RemoveAll(IEnumerable<string> keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            keys.ToList().ForEach(item => Remove(item));
        }


        public Task<bool> RemoveAsync(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _cache.KeyDeleteAsync(GetKeyForRedis(key));
        }

        public Task RemoveAllAsync(IEnumerable<string> keys)
        {
            //if (keys == null)
            //{
            throw new ArgumentNullException(nameof(keys));
            //}

            //keys.ToList().ForEach(item => RemoveAsync(item));
        }
        /// <summary>
        /// 使用通配符找出所有的key然后逐個刪除
        /// </summary>
        /// <param name="pattern">通配符</param>
        public virtual void RemoveByPattern(string pattern)
        {
            foreach (var ep in _connection.GetEndPoints())
            {
                var server = _connection.GetServer(ep);
                var keys = server.Keys(pattern: "*" + pattern + "*", database: _cache.Database);
                foreach (var key in keys)
                    _cache.KeyDelete(key);
            }
        }


        /// <summary>
        /// 刪除所有緩存
        /// </summary>
        public void RemoveCacheAll()
        {
            RemoveByPattern("");
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
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var value = _cache.StringGet(GetKeyForRedis(key));

            if (!value.HasValue)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(value, _jsonOptions);
        }
        /// <summary>
        /// 獲取緩存
        /// </summary>
        /// <param name="key">緩存Key</param>
        /// <returns></returns>
        public object Get(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var value = _cache.StringGet(GetKeyForRedis(key));

            if (!value.HasValue)
            {
                return null;
            }

            return JsonSerializer.Deserialize<object>(value, _jsonOptions);
            //string json = value.ToString();
            //return Newtonsoft.Json.JsonConvert.DeserializeObject(json);


        }
        /// <summary>
        /// 獲取緩存集合
        /// </summary>
        /// <param name="keys">緩存Key集合</param>
        /// <returns></returns>
        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }
            var dict = new Dictionary<string, object>();

            keys.ToList().ForEach(item => dict.Add(item, Get(GetKeyForRedis(item))));

            return dict;
        }


        public Task<T> GetAsync<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<string, object>> GetAllAsync(IEnumerable<string> keys)
        {

            throw new ArgumentNullException(nameof(keys));
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
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (Exists(key))
                if (!Remove(key))
                    return false;

            return Add(key, value);

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
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (Exists(key))
                if (!Remove(key))
                    return false;

            return Add(key, value, expiresSliding, expiressAbsoulte);
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
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (Exists(key))
                if (!Remove(key)) return false;

            return Add(key, value, expiresIn, isSliding);
        }


        public Task<bool> ReplaceAsync(string key, object value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void Dispose()
        {
            if (_connection != null)
                _connection.Dispose();
            GC.SuppressFinalize(this);
        }


    }
}