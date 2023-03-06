using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Cache
{
    /// <summary>
    /// 緩存提供模式，使用Redis或MemoryCache
    /// </summary>
    public class CacheProvider
    {
        private bool _isUseRedis;
        
        private string _connectionString;
        private string _instanceName;
        /// <summary>
        /// 是否使用Redis
        /// </summary>
        public bool IsUseRedis { get => _isUseRedis; set => _isUseRedis = value; }
        /// <summary>
        /// Redis連接
        /// </summary>
        public string ConnectionString { get => _connectionString; set => _connectionString = value; }
        /// <summary>
        /// Redis實例名稱
        /// </summary>
        public string InstanceName { get => _instanceName; set => _instanceName = value; }
    }
}
