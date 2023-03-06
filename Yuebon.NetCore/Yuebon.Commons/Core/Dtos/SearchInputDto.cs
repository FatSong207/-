using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Yuebon.Commons.Pages;

namespace Yuebon.Commons.Dtos
{
    /// <summary>
    /// 查詢條件公共實體類
    /// </summary>
    [Serializable]
    [DataContract]
    public class SearchInputDto<T> : PagerInfo
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

        /// <summary>
        /// 查詢條件
        /// </summary>
        public T Filter { get; set; }
    }
}
