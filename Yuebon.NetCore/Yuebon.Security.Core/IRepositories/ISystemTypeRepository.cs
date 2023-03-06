using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.IRepositories
{
    public interface ISystemTypeRepository : IRepository<SystemType, string>
    {
        /// <summary>
        /// 根據系統編碼查詢系統對象
        /// </summary>
        /// <param name="appkey">系統編碼</param>
        /// <returns></returns>
        SystemType GetByCode(string appkey);
    }
}
