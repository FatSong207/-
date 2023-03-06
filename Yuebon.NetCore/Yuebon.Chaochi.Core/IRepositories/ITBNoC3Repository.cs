using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義代管合約BZ倉儲接口
    /// </summary>
    public interface ITBNoC3Repository:IRepository<TBNoC3, string>
    {
        /// <summary>
        /// 根據合約編號查詢代管合約BZ欄位值
        /// 
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<TBNoC3> GetByCID(string cid);
    }
}