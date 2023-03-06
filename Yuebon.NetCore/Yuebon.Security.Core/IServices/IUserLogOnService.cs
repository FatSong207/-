using System;
using System.Threading.Tasks;
using Yuebon.Commons.IServices;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    public interface IUserLogOnService:IService<UserLogOn, UserLogOnOutputDto, string>
    {

        /// <summary>
        /// 根據會員ID獲取用戶登錄信息實體
        /// </summary>
        /// <param name="userId">用戶Id</param>
        /// <returns></returns>
        UserLogOn GetByUserId(string userId);

        /// <summary>
        /// 根據會員ID獲取用戶登錄信息實體
        /// </summary>
        /// <param name="info">主題配置信息</param>
        /// <param name="userId">用戶Id</param>
        /// <returns></returns>
        Task<bool> SaveUserTheme(UserThemeInputDto info, string userId);
    }
}
