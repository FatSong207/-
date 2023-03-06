using System;
using System.Threading.Tasks;
using Yuebon.Commons.Json;
using Yuebon.Commons.Services;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.Security.Services
{
    public class UserLogOnDService: BaseService<UserLogOnD, UserLogOnOutputDto, string>, IUserLogOnDService
    {
        private readonly IUserLogOnDRepository _userLogOnRepository;
        private readonly ILogService _logService;
        public UserLogOnDService(IUserLogOnDRepository repository, ILogService logService) : base(repository)
        {
            _userLogOnRepository = repository;
            _logService = logService;
        }

        /// <summary>
        /// 根據會員ID獲取用戶登錄信息實體
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
       public UserLogOnD GetByUserId(string userId)
        {
           return _userLogOnRepository.GetByUserId(userId);
        }

        /// <summary>
        /// 根據會員ID獲取用戶登錄信息實體
        /// </summary>
        /// <param name="info">主題配置信息</param>
        /// <param name="userId">用戶Id</param>
        /// <returns></returns>
        public async Task<bool> SaveUserTheme(UserThemeInputDto info,string userId)
        {
            string themeJsonStr = info.ToJson();
            string where = $"UserId='{userId}'";
            return await _userLogOnRepository.UpdateTableFieldAsync("Theme",themeJsonStr, where);
        }
    }
}