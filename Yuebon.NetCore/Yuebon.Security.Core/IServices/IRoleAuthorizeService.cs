using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.IServices;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRoleAuthorizeService:IService<RoleAuthorize, RoleAuthorizeOutputDto, string>
    {
        /// <summary>
        /// 根據角色和項目類型查詢權限
        /// </summary>
        /// <param name="roleIds"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        IEnumerable<RoleAuthorize> GetListRoleAuthorizeByRoleId(string roleIds, string itemType);


        /// <summary>
        /// 獲取功能菜單適用于Vue Tree樹形
        /// </summary>
        /// <returns></returns>
        Task<List<ModuleFunctionOutputDto>> GetAllFunctionTree();

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
