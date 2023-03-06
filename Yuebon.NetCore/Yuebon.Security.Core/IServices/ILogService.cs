using System;
using System.Threading.Tasks;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Pages;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 日志記錄
    /// </summary>
    public interface ILogService:IService<Log, LogOutputDto, string>
    {
        /// <summary>
        /// 根據相關信息，寫入用戶的操作日志記錄
        /// 主要用于寫數據庫日志
        /// </summary>
        /// <param name="tableName">操作表名稱</param>
        /// <param name="operationType">操作類型</param>
        /// <param name="note">操作詳細表述</param>
        /// <returns></returns>
         bool OnOperationLog(string tableName, string operationType, string note);

        /// <summary>
        /// 根據相關信息，寫入用戶的操作日志記錄
        /// 主要用于寫操作模塊日志
        /// </summary>
        /// <param name="module">操作模塊名稱</param>
        /// <param name="operationType">操作類型</param>
        /// <param name="note">操作詳細表述</param>
        /// <param name="currentUser">操作用戶</param>
        /// <returns></returns>
        bool OnOperationLog(string module, string operationType,  string note, YuebonCurrentUser currentUser);
        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        Task<PageResult<LogOutputDto>> FindWithPagerSearchAsync(SearchLogModel search);
    }
}
