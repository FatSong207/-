using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Data;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義潛在客管理服務接口
    /// </summary>
    public interface IPotentialCustomersService:IService<PotentialCustomers,PotentialCustomersOutputDto, string>
    {
        /// <summary>
        /// 根據潛在客編號查詢潛在客資料
        /// </summary>
        /// <param name="pid">潛在客編號</param>
        /// <returns></returns>
        Task<PotentialCustomersOutputDto> GetByPID(string pid);

        /// <summary>
        /// 更新潛在客管理的當前狀態
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="pid">潛在客編號</param>
        /// <param name="status">當前狀態</param>
        /// <returns></returns> 
        Task<bool> UpdatePCStatus(string userId, string pid, string status, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 更新潛在客管理的結案狀態
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="pid">潛在客編號</param>
        /// <param name="isClosed">是否結案</param>
        /// <returns></returns> 
        Task<bool> UpdatePCIsClosed(string userId, string pid, string isClosed, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 更新潛在客管理的回報次數        
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="userName">使用者名稱</param>
        /// <param name="pid">潛在客編號</param>
        /// <param name="reportBackCounts">回報次數</param>
        /// <returns></returns> 
        Task<bool> UpdatePCReportBackCounts(string userId, string pid, int reportBackCounts, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 更新潛在客管理的結案狀態
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="pid">潛在客編號</param>
        /// <param name="identity">身份別(出租人/承租人)</param>
        /// <param name="name">姓名</param>
        /// <param name="addressArray">地址陣列</param>
        /// <returns></returns> 
        Task<bool> UpdateTransferClientFields(string userId, string pid, string identity, string name, string[] addressArray, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 指派業務
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="pid">潛在客編號</param>
        /// <param name="sales">指派業務</param>
        /// <returns></returns> 
        Task<bool> AssignSales(string userId, string pid, string sales, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 轉店時要檢查此店是否己存在
        /// 
        /// </summary>
        /// <param name="store">店名</param>
        /// <param name="cell">手機號碼</param>
        /// <returns></returns> 
        Task<bool> IsStoreRecordExist(string store, string cell, IDbConnection conn = null, IDbTransaction trans = null);
    }
}
