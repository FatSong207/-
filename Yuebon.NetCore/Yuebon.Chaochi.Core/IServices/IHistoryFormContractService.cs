using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義合約歷史服務接口
    /// </summary>
    public interface IHistoryFormContractService:IService<HistoryFormContract,HistoryFormContractOutputDto, string>
    {
        /// <summary>
        /// 根據合約編號查詢合約歷史
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<List<HistoryFormContractOutputDto>> GetByCID(string cid);

        /// <summary>
        /// 根據合約編號+合約歷史版號查詢合約歷史
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <param name="version">合約歷史版號</param>
        /// <returns></returns>
        Task<HistoryFormContractOutputDto> GetByVersion(string cid, string version);

        /// <summary>
        /// 根據合約編號查詢合約歷史最大版號
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<int> GetMaxVersionByCID(string cid);

        /// <summary>
        /// 更新最新版掃描檔路徑
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="cid">合約編號</param>
        /// <param name="version">目前歷史版本</param>
        /// <param name="path">掃描簽約檔路徑</param>
        /// <returns></returns> 
        Task<bool> UpdateSignedContractPath(string userId, string cid, string version, string path, IDbConnection conn = null, IDbTransaction trans = null);
    }
}
