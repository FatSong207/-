using System;
using System.Threading.Tasks;
using Yuebon.Commons.IRepositories;
using Yuebon.Tenants.Dtos;
using Yuebon.Tenants.Models;

namespace Yuebon.Tenants.IRepositories
{
    /// <summary>
    /// 定義租戶倉儲接口
    /// </summary>
    public interface ITenantRepository:IRepository<Tenant, string>
    {
        /// <summary>
        /// 根據租戶帳號查詢租戶信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Tenant> GetByUserName(string userName);

        /// <summary>
        /// 註冊租戶戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tenantLogOnEntity"></param>
        Task<bool> InsertAsync(Tenant entity, TenantLogon tenantLogOnEntity);

    }
}