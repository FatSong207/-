using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義合約相關資料服務接口
    /// </summary>
    public interface IContractRelevantService:IService<ContractRelevant,ContractRelevantOutputDto, string>
    {
        /// <summary>
        /// 根據合約編號查詢相關資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<ContractRelevantOutputDto> GetByCID(string cid);
    }
}
