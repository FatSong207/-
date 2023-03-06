using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Data;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義合約服務接口
    /// </summary>
    public interface IContractService:IService<Contract,ContractOutputDto, string>
    {
        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        new Task<PageResult<ContractOutputDto>> FindWithPagerAsync(SearchInputDto<Contract> search, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 建物地址是否己綁定某合約
        /// </summary>
        /// <param name="badd">建物地址</param>
        /// <returns></returns>
        Task<ContractOutputDto> GetExistContractByBAdd(string badd);

        /// <summary>
        /// 根據合約編號查詢合約
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<ContractOutputDto> GetByCID(string cid);

        /// <summary>
        /// 根據合約編號查詢合約
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<Contract> GetByCIDRaw(string cid);

        /// <summary>
        /// 根據合約編號查詢完整合約資訊
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        Task<ContractOutputDto> GetContract(string cid);

        /// <summary>
        /// 根據合約編號+合約狀態查詢合約
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <param name="status">合約狀態</param>
        /// <returns></returns>
        Task<Contract> GetByStatusRaw(string cid, string status);

        /// <summary>
        /// 更新主約狀態
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="cid">合約編號</param>
        /// <param name="status">附件狀態</param>
        /// <param name="version">目前歷史版本</param>
        /// <param name="path">掃描簽約檔路徑</param>
        /// <returns></returns> 
        Task<bool> UpdateContractStatus(string userId, string cid, string status, string version = "", IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 更新主約狀態(排程用)
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="cid">合約編號</param>
        /// <param name="status">附件狀態</param>
        /// <param name="version">目前歷史版本</param>
        /// <param name="path">掃描簽約檔路徑</param>
        /// <returns></returns> 
        bool UpdateContractStatusTask(string userId, string cid, string status, string version = "", IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 更新主約最新歷史版本
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="cid">合約編號</param>
        /// <param name="version">最新歷史版本</param>
        /// <returns></returns> 
        Task<bool> UpdateContractVersion(string userId, string cid, string version, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 更新簽約日
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="cid">合約編號</param>
        /// <param name="contractDate">簽約日</param>
        /// <returns></returns> 
        Task<bool> UpdateContractDate(string userId, string cid, string contractDate, IDbConnection conn = null, IDbTransaction trans = null);
    }
}
