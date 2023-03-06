using System;
using System.Collections.Generic;
using Yuebon.Commons.Services;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.Security.Services
{
    public class RoleDataService: BaseService<RoleData, RoleDataOutputDto, string>, IRoleDataService
    {
		private readonly IRoleDataRepository _repository;
        private readonly ILogService _logService;
        public RoleDataService(IRoleDataRepository repository,ILogService logService) : base(repository)
        {
			_repository=repository;
			_logService=logService;
        }
        /// <summary>
        /// 根據角色返回授權訪問部門數據
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public List<string> GetListDeptByRole(string roleIds)
        {
            return _repository.GetListDeptByRole(roleIds);
        }
    }
}