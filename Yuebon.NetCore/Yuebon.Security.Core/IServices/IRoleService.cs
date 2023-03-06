using System;
using System.Collections.Generic;
using Yuebon.Commons.IServices;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRoleService:IService<Role, RoleOutputDto, string>
    {
        /// <summary>
        /// 根據角色編碼獲取角色
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        Role GetRole(string enCode);


        /// <summary>
        /// 根據用戶角色ID獲取角色編碼
        /// </summary>
        /// <param name="ids">角色ID字符串，用“,”分格</param>
        /// <returns></returns>
        string GetRoleEnCode(string ids);


        /// <summary>
        /// 根據用戶角色ID獲取角色編碼
        /// </summary>
        /// <param name="ids">角色ID字符串，用“,”分格</param>
        /// <returns></returns>
       string GetRoleNameStr(string ids);
    }
}
