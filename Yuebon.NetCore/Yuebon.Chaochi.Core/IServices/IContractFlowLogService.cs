using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義合約審核記錄服務接口
    /// </summary>
    public interface IContractFlowLogService:IService<ContractFlowLog,ContractFlowLogOutputDto, string>
    {
        /// <summary>
        /// 根據合約編號查詢審核記錄
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<List<ContractFlowLogOutputDto>> GetByCID(string cid);

        /// <summary>
        /// 根據合約編號和合約狀態查詢審核記錄
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <param name="cstatus">合約狀態</param>
        /// <returns></returns>
        Task<List<ContractFlowLogOutputDto>> GetByCStatus(string cid, string cstatus);
    }
}
