using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義包租合約BZ倉儲接口
    /// </summary>
    public interface ITBNoC1Repository:IRepository<TBNoC1, string>
    {
        /// <summary>
        /// 根據合約編號查詢包租合約BZ欄位值
        /// 
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<TBNoC1> GetByCID(string cid);
    }
}