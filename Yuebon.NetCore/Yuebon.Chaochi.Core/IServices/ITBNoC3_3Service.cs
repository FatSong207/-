using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface ITBNoC3_3Service:IService<TBNoC3_3,TBNoC3_3OutputDto, string>
    {
        /// <summary>
        /// 根據合約編號查詢相關資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<TBNoC3_3> GetByCID(string cid);
    }
}
