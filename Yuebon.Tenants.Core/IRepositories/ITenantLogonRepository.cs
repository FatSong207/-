using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Tenants.Models;

namespace Yuebon.Tenants.IRepositories
{
    /// <summary>
    /// 定義用戶登錄信息倉儲接口
    /// </summary>
    public interface ITenantLogonRepository:IRepository<TenantLogon, string>
    {
        /// <summary>
        /// 根據租戶ID獲取租戶登錄信息實體
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        TenantLogon GetByTenantId(string tenantId);
    }
}