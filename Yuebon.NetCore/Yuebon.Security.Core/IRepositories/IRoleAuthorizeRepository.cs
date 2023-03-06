using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.IRepositories
{
    public interface IRoleAuthorizeRepository:IRepository<RoleAuthorize,string>
    {
        /// <summary>
        /// 保存角色授權
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleAuthorizesList">角色功能模塊</param>
        /// <param name="roleDataList">角色可訪問數據</param>
        /// <param name="trans"></param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        Task<bool> SaveRoleAuthorize(string roleId,List<RoleAuthorize> roleAuthorizesList, List<RoleData> roleDataList,
           IDbTransaction trans = null);
    }
}