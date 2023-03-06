using System;
using System.Collections.Generic;
using Yuebon.Commons.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.IRepositories
{
    public interface IMenuRepository:IRepository<Menu, string>
    {

        /// <summary>
        /// 根據角色ID字符串（逗號分開)和系統類型ID，獲取對應的操作功能列表
        /// </summary>
        /// <param name="roleIds">角色ID</param>
        /// <param name="typeID">系統類型ID</param>
        /// <param name="isMenu">是否是菜單</param>
        /// <returns></returns>
        IEnumerable<Menu> GetFunctions(string roleIds, string typeID, bool isMenu = false);

        /// <summary>
        /// 根據系統類型ID，獲取對應的操作功能列表
        /// </summary>
        /// <param name="typeID">系統類型ID</param>
        /// <returns></returns>
        IEnumerable<Menu> GetFunctions(string typeID);
    }
}