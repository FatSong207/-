using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義轉租合約BZ欄位服務接口
    /// </summary>
    public interface ITBNoC2Service:IService<TBNoC2, TBNoC2OutputDto, string>
    {
        /// <summary>
        /// 根據合約編號查詢相關資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<TBNoC2> GetByCID(string cid);
    }
}
