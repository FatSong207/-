using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.IRepositories
{
    public interface IUserLogOnDRepository:IRepository<UserLogOnD, string>
    {
        /// <summary>
        /// 根據會員ID獲取用戶登錄信息實體
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserLogOnD GetByUserId(string userId);
    }
}