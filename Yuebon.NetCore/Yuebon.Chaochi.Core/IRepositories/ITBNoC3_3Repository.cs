using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義倉儲接口
    /// </summary>
    public interface ITBNoC3_3Repository:IRepository<TBNoC3_3, string>
    {
        /// <summary>
        /// 根據合約編號查詢包租合約BZ欄位值(第三張表)
        /// 
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>    
        Task<TBNoC3_3> GetByCID(string cid);
    }
}