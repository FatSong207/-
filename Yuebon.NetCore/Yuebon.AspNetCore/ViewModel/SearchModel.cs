using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Yuebon.Commons.Pages;

namespace Yuebon.AspNetCore.ViewModel
{
    /// <summary>
    /// 查詢條件公共實體類
    /// </summary>
    [Serializable]
    [DataContract]
    public class SearchModel: PagerInfo
    {
        /// <summary>
        /// 關鍵詞
        /// </summary>
        public string Keywords
        {
            get; set;
        }
        /// <summary>
        /// 編碼/代碼
        /// </summary>
        public string EnCode
        {
            get; set;
        }
        /// <summary>
        /// 排序方式 默認asc 
        /// </summary>
        public string Order
        {
            get; set;
        }
        /// <summary>
        /// 排序字段 默認Id
        /// </summary>
        public string Sort
        {
            get; set;
        }
    }
}
