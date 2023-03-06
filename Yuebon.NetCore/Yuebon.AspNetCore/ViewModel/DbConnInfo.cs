using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Yuebon.AspNetCore.ViewModel
{
    /// <summary>
    /// 數據庫連接字符串實體
    /// </summary>
    [Serializable]
    public class DbConnInfo
    {
        /// <summary>
        /// 訪問地址
        /// </summary>
        [DataMember]
        public string DbAddress { get; set; }
        /// <summary>
        /// 端口，默認SQLServer為1433；Mysql為3306
        /// </summary>
        [DataMember]
        public int DbPort { get; set; }
        /// <summary>
        /// 數據庫名稱
        /// </summary>
        [DataMember]
        public string DbName { get; set; }
        /// <summary>
        /// 用戶名
        /// </summary>
        [DataMember]
        public string DbUserName { get; set; }
        /// <summary>
        /// 訪問密碼
        /// </summary>
        [DataMember]
        public string DbPassword { get; set; }
        /// <summary>
        /// 數據庫類型
        /// </summary>
        [DataMember]
        public string DbType { get; set; }

    }
}
