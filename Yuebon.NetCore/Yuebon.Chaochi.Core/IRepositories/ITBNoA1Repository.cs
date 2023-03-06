using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.IRepositories;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBNoA1Repository : IRepository<TBNoA1, string>
    {
        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="BID"></param>
        /// <returns></returns>
        Task<TBNoA1> GetByBID(string BID);

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="BID"></param>
        /// <returns></returns>
        Task<TBNoA1> GetByBAdd(string formId, string BAdd);

        Task<List<TBNoA1>> GetListByBAdd(string BAdd);
    }
}