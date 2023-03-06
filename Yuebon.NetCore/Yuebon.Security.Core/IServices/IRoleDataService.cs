using System;
using System.Collections.Generic;
using Yuebon.Commons.IServices;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    public interface IRoleDataService:IService<RoleData, RoleDataOutputDto, string>
    {

        /// <summary>
        /// 根據角色返回授權訪問部門數據
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        List<string> GetListDeptByRole(string roleIds);
    }
}
