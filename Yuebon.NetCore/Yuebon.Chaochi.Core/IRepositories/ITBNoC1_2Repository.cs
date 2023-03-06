using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義倉儲接口
    /// </summary>
    public interface ITBNoC1_2Repository:IRepository<TBNoC1_2, string>
    {
        /// <summary>
        /// 根據合約編號查詢包租合約BZ欄位值
        /// 
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>    
        Task<TBNoC1_2> GetByCID(string cid);
    }
}