using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義合約-出租人服務接口
    /// </summary>
    public interface IContractLandlordService:IService<ContractLandlord,ContractLandlordOutputDto, string>
    {
        /// <summary>
        /// 根據合約編號查詢出租人資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<ContractLandlord> GetByCID(string cid);
    }
}
