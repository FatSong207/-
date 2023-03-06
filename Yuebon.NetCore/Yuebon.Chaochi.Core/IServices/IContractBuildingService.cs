using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義合約-建物服務接口
    /// </summary>
    public interface IContractBuildingService:IService<ContractBuilding,ContractBuildingOutputDto, string>
    {
        /// <summary>
        /// 根據合約編號查詢建物資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<ContractBuildingOutputDto> GetByCID(string cid);
    }
}
