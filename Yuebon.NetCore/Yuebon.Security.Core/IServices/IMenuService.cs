using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Models;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMenuService:IService<Menu, MenuOutputDto, string>
    {

        /// <summary>
        /// 根據用戶獲取功能菜單
        /// </summary>
        /// <param name="userId">用戶ID</param>
        /// <returns></returns>
        List<Menu> GetMenuByUser(string userId);

        /// <summary>
        /// 獲取功能菜單適用于Vue 樹形列表
        /// </summary>
        /// <param name="systemTypeId">子系統Id</param>
        /// <returns></returns>
        Task<List<MenuTreeTableOutputDto>> GetAllMenuTreeTable(string systemTypeId);


        /// <summary>
        /// 根據角色ID字符串（逗號分開)和系統類型ID，獲取對應的操作功能列表
        /// </summary>
        /// <param name="roleIds">角色ID</param>
        /// <param name="typeID">系統類型ID</param>
        /// <param name="isMenu">是否是菜單</param>
        /// <returns></returns>
        List<Menu> GetFunctions(string roleIds, string typeID,bool isMenu=false);

        /// <summary>
        /// 根據系統類型ID，獲取對應的操作功能列表
        /// </summary>
        /// <param name="typeID">系統類型ID</param>
        /// <returns></returns>
        List<Menu> GetFunctions(string typeID);

        /// <summary>
        /// 根據父級功能編碼查詢所有子集功能，主要用于頁面操作按鈕權限
        /// </summary>
        /// <param name="enCode">菜單功能編碼</param>
        /// <returns></returns>
        Task<IEnumerable<MenuOutputDto>> GetListByParentEnCode(string enCode);

        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="ids">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        CommonResult DeleteBatchWhere(DeletesInputDto ids, IDbTransaction trans = null);
        /// <summary>
        /// 異步按條件批量刪除
        /// </summary>
        /// <param name="ids">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<CommonResult> DeleteBatchWhereAsync(DeletesInputDto ids, IDbTransaction trans = null);
    }
}
