using System;
using Yuebon.Commons.IServices;
using Yuebon.Tenants.Dtos;
using Yuebon.Tenants.Models;

namespace Yuebon.Tenants.IServices
{
    /// <summary>
    /// 定義用戶登錄信息服務接口
    /// </summary>
    public interface ITenantLogonService:IService<TenantLogon,TenantLogonOutputDto, string>
    {

    }
}
