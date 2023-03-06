using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserRepository:IRepository<User, string>
    {
        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<User> GetByUserName(string userName);
        /// <summary>
        /// 根據用戶手機號碼查詢用戶信息
        /// </summary>
        /// <param name="mobilePhone">手機號碼</param>
        /// <returns></returns>
        Task<User> GetUserByMobilePhone(string mobilePhone);
        /// <summary>
        /// 根據Email、Account、手機號查詢用戶信息
        /// </summary>
        /// <param name="account">登錄帳號</param>
        /// <returns></returns>
        Task<User> GetUserByLogin(string account);
        /// <summary>
        /// 根據Email查詢用戶信息
        /// </summary>
        /// <param name="email">email</param>
        /// <returns></returns>
       Task<User> GetUserByEmail(string email);
        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        bool Insert(User entity, Security.Models.UserLogOn userLogOnEntity, IDbTransaction trans = null);
        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        Task<bool> InsertAsync(User entity, Security.Models.UserLogOn userLogOnEntity, IDbTransaction trans = null);
        /// <summary>
        /// 註冊用戶,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        bool Insert(User entity, Security.Models.UserLogOn userLogOnEntity, Security.Models.UserOpenIds userOpenIds, IDbTransaction trans = null);
        /// <summary>
        /// 根據第三方OpenId查詢用戶信息
        /// </summary>
        /// <param name="openIdType">第三方類型</param>
        /// <param name="openId">OpenId值</param>
        /// <returns></returns>
        User GetUserByOpenId(string openIdType, string openId);

        /// <summary>
        /// 根據微信UnionId查詢用戶信息
        /// </summary>
        /// <param name="unionId">UnionId值</param>
        /// <returns></returns>
        User GetUserByUnionId(string unionId);
        /// <summary>
        /// 根據userId查詢用戶信息
        /// </summary>
        /// <param name="openIdType">第三方類型</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        Security.Models.UserOpenIds GetUserOpenIdByuserId(string openIdType, string userId);
        /// <summary>
        /// 更新用戶信息,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        bool UpdateUserByOpenId(User entity, Security.Models.UserLogOn userLogOnEntity, Security.Models.UserOpenIds userOpenIds, IDbTransaction trans = null);

        /// <summary>
        /// 根據用戶ID得到名片信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        //UserNameCardOutPutDto GetUserNameCardInfo(string userid);

        /// <summary>
        /// 保存名片
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="headicon"></param>
        /// <param name="nickName"></param>
        /// <param name="name"></param>
        /// <param name="company"></param>
        /// <param name="position"></param>
        /// <param name="weburl"></param>
        /// <param name="mobile"></param>
        /// <param name="email"></param>
        /// <param name="wx"></param>
        /// <param name="wximg"></param>
        /// <param name="industry"></param>
        /// <param name="area"></param>
        /// <param name="address"></param>
        /// <param name="openflag"></param>
        /// <returns></returns>
        ////bool SaveNameCard(string userid,string headicon, string nickName, string name, string company, string position,
        //    string weburl, string mobile, string email, string wx, string wximg,
        //    string industry, string area, string address, int openflag);


        /// <summary>
        /// 所有用戶信息用于關注
        /// </summary>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        IEnumerable<UserAllListFocusOutPutDto> GetUserAllListFocusByPage(string currentpage,
            string pagesize, string userid);
    }
}