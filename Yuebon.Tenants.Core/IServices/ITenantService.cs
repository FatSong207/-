using System;
using System.Threading.Tasks;
using Yuebon.Commons.IServices;
using Yuebon.Tenants.Dtos;
using Yuebon.Tenants.Models;

namespace Yuebon.Tenants.IServices
{
    /// <summary>
    /// 定義租戶服務接口
    /// </summary>
    public interface ITenantService:IService<Tenant,TenantOutputDto, string>
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

        /// <summary>
        /// 租戶登陸驗證。
        /// </summary>
        /// <param name="userName">用戶名</param>
        /// <param name="password">密碼（第一次md5加密后）</param>
        /// <returns>驗證成功返回用戶實體，驗證失敗返回null|提示消息</returns>
        Task<Tuple<Tenant, string>> Validate(string userName, string password);
    }
}
