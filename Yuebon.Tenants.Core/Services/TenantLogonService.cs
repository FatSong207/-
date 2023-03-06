﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Tenants.IRepositories;
using Yuebon.Tenants.IServices;
using Yuebon.Tenants.Dtos;
using Yuebon.Tenants.Models;

namespace Yuebon.Tenants.Services
{
    /// <summary>
    /// 用戶登錄信息服務接口實現
    /// </summary>
    public class TenantLogonService : BaseService<TenantLogon,TenantLogonOutputDto, string>, ITenantLogonService
    {
		private readonly ITenantLogonRepository _repository;
        public TenantLogonService(ITenantLogonRepository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}