using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Yuebon.AspNetCore.ViewModel
{
    /// <summary>
    /// 數據庫連接返回結果實體
    /// </summary>
    [Serializable]
    public class DBConnResult
    {
        /// <summary>
        /// 未加密字符串
        /// </summary>
        [DataMember]
        public string ConnStr { get; set; }
        /// <summary>
        /// 數據庫名稱
        /// </summary>
        [DataMember]
        public string EncryptConnStr { get; set; }
    }
}
