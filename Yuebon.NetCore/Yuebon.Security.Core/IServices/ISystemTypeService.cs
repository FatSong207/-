using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.IServices;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 子系統服務接口
    /// </summary>
    public interface ISystemTypeService : IService<SystemType, SystemTypeOutputDto, string>
    {

        /// <summary>
        /// 根據系統編碼查詢系統對象
        /// </summary>
        /// <param name="appkey">系統編碼</param>
        /// <returns></returns>
        SystemType GetByCode(string appkey);

        /// <summary>
        /// 根據角色獲取可以訪問子系統
        /// </summary>
        /// <param name="roleIds">角色Id，用','隔開</param>
        /// <returns></returns>
        List<SystemTypeOutputDto> GetSubSystemList(string roleIds);
    }
}
