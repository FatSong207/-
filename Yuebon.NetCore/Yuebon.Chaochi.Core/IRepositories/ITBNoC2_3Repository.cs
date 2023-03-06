using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義倉儲接口
    /// </summary>
    public interface ITBNoC2_3Repository:IRepository<TBNoC2_3, string>
    {
        /// <summary>
        /// 根據合約編號查詢轉租合約BZ欄位值
        /// 
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<TBNoC2_3> GetByCID(string cid);
    }
}