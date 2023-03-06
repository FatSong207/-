using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.IRepositories
{
    public interface IFilterIPRepository:IRepository<FilterIP, string>
    {
        /// <summary>
        /// 驗證IP地址是否被拒絕
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        bool ValidateIP(string ip);
    }
}