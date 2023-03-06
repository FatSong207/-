using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義合約-承租人服務接口
    /// </summary>
    public interface IContractRenterService:IService<ContractRenter,ContractRenterOutputDto, string>
    {
        /// <summary>
        /// 根據合約編號查詢承租人資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<ContractRenter> GetByCID(string cid);
    }
}
