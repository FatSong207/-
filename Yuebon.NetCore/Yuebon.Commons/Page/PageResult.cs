using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;

namespace Yuebon.Commons.Pages
{
    /// <summary>
    /// 保存分頁請求的結果。
    /// </summary>
    /// <typeparam name="T">返回結果集中的POCO類型</typeparam>
    public class PageResult<T>:CommonResult
    {
        public PageResult()
        {
        }
        public PageResult(bool success, string msg, object rows)
        {
            this.Success = success;
            this.ErrMsg = msg;
            this.ResData =rows ;
        }
        public PageResult(long currentPage, long totalItems, long itemsPerPage)
        {
            CurrentPage = currentPage;
            TotalItems = totalItems;
            ItemsPerPage = itemsPerPage;
        }

        public PageResult(long currentPage, long totalPages, long totalItems, long itemsPerPage, List<T> items, object context) : this(currentPage, totalPages, totalItems)
        {
            ItemsPerPage = itemsPerPage;
            Items = items;
            Context = context;
        }

        /// <summary>
        /// 當前頁碼。
        /// </summary>
        public long CurrentPage { get; set; }

        /// <summary>
        /// 總頁碼數。
        /// </summary>
        public long TotalPages { get; set; }

        /// <summary>
        /// 記錄總數。
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        /// 每頁數量。
        /// </summary>
        public long ItemsPerPage { get; set; }

        /// <summary>
        /// 當前結果集。
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// 自定義用戶屬性。
        /// </summary>
        public object Context { get; set; }
    }
}