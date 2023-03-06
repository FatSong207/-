using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.Core.App;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IoC;
using Yuebon.Commons.Mapping;
using Yuebon.Security.Dtos;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.Security.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class UserApp
    {
        IUserService service = App.GetService<IUserService>();
        IUserLogOnService userLogOnService = App.GetService<IUserLogOnService>();
        IRoleService roleService = App.GetService<IRoleService>();
        /// <summary>
        /// 獲取所有用戶信息
        /// </summary>        
        /// <returns></returns>
        public IEnumerable<User> GetAll()
        {
            return service.GetAll();
        }

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User> GetByUserName(string userName)
        {
            return await service.GetByUserName(userName);
        }

        /// <summary>
        /// 根據第三方OpenId查詢用戶信息
        /// </summary>
        /// <param name="openIdType">第三方類型</param>
        /// <param name="openId">OpenId值</param>
        /// <returns></returns>
        public UserOutputDto GetUserOutDtoByOpenId(string openIdType, string openId)
        {
            return service.GetUserByOpenId(openIdType, openId).MapTo<UserOutputDto>();
        }
        /// <summary>
        /// 根據第三方OpenId查詢用戶信息
        /// </summary>
        /// <param name="openIdType">第三方類型</param>
        /// <param name="openId">OpenId值</param>
        /// <returns></returns>
        public User GetUserByOpenId(string openIdType, string openId)
        {
            return service.GetUserByOpenId(openIdType, openId);
        }

        /// <summary>
        /// 更新用戶
        /// </summary>
        /// <param name="user">用戶信息</param>
        /// <returns></returns>
        public bool UpdateUser(User user)
        {
            return service.Update(user, user.Id);
        }
        /// <summary>
        /// 根據用戶ID獲取頭像
        /// </summary>
        /// <param name="userid">用戶ID</param>
        /// <returns></returns>
        public string GetHeadIconById(string userid)
        {
            User user = service.Get(userid);

            if (user != null)
            {
                return user.HeadIcon;
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 查詢用戶信息
        /// </summary>
        /// <param name="id">用戶Id</param>
        /// <returns></returns>
        public User GetUserById(string id)
        {
            return service.Get(id);
        }
        /// <summary>
        /// 根據用戶id和第三方類型查詢
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="openIdType"></param>
        /// <returns></returns>
        public UserOpenIds GetUserOpenIdById(string userId, string openIdType)
        {
            return service.GetUserOpenIdByuserId(openIdType,userId);
        }
        /// <summary>
        /// 根據微信統一ID（UnionID）查詢用戶
        /// </summary>
        /// <param name="unionId">UnionID</param>
        /// <returns></returns>
        public User GetUserByUnionId(string unionId)
        {
            return service.GetUserByUnionId(unionId);
        }

        /// <summary>
        /// 統計用戶數
        /// </summary>
        /// <returns></returns>
        public int GetCountTotal()
        {
            return service.GetCountByWhere("1=1");
        }
    }
}
