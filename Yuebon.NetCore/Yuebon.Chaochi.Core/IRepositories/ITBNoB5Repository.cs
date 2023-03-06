using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBNoB5Repository : IRepository<TBNoB5, string>
    {
        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="BID"></param>
        /// <returns></returns>
        Task<TBNoB5> GetByBID(string BID);

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="BID"></param>
        /// <returns></returns>
        Task<TBNoB5> GetByBAdd(string BAdd);
    }
}