using System;
using System.Collections.Generic;
using Yuebon.Commons.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.IRepositories
{
    public interface IRoleDataRepository:IRepository<RoleData, string>
    {
        /// <summary>
        /// 根據角色返回授權訪問部門數據
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
       List<string> GetListDeptByRole(string roleIds);
    }
}