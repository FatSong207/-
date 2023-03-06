using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Yuebon.Commons.Enums;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Pages;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 用戶服務接口
    /// </summary>
    public interface IUserService:IService<User, UserOutputDto, string>
    {
        /// <summary>
        /// 用戶登陸驗證。
        /// </summary>
        /// <param name="userName">用戶名</param>
        /// <param name="password">密碼（第一次md5加密后）</param>
        /// <returns>驗證成功返回用戶實體，驗證失敗返回null|提示消息</returns>
        Task<Tuple<User, string>> Validate(string userName, string password);

        /// <summary>
        /// 用戶登陸驗證。
        /// </summary>
        /// <param name="userName">用戶名</param>
        /// <param name="password">密碼（第一次md5加密后）</param>
        /// <param name="userType">用戶類型</param>
        /// <returns>驗證成功返回用戶實體，驗證失敗返回null|提示消息</returns>
        Task<Tuple<User, string>> Validate(string userName, string password, UserType userType);

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
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        bool Insert(User entity, UserLogOn userLogOnEntity, IDbTransaction trans = null);
        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        Task<bool> InsertAsync(User entity, UserLogOn userLogOnEntity, IDbTransaction trans = null);
        /// <summary>
        /// 註冊用戶,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="userOpenIds"></param>
        /// <param name="trans"></param>
        bool Insert(User entity, UserLogOn userLogOnEntity, UserOpenIds userOpenIds,IDbTransaction trans = null);
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
        UserOpenIds GetUserOpenIdByuserId(string openIdType, string userId);
        /// <summary>
        /// 更新用戶信息,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        bool UpdateUserByOpenId(User entity, UserLogOn userLogOnEntity, UserOpenIds userOpenIds, IDbTransaction trans = null);

        /// <summary>
        /// 獲取可訪問的組織部門
        /// </summary>
        /// <param name="deptList"></param>
        /// <returns></returns>
        Task<List<string>> GetPermissionDepts(string deptList);

        /// <summary>
        /// 找出登入者的所屬店長
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        string GetStoreManager(string account);

        /// <summary>
        /// 找出所屬店的店長
        /// </summary>
        /// <param name="departmentId">所屬店ID</param>
        /// <returns></returns>
        string GetManagerByDepartmentId(string departmentId);

        /// <summary>
        /// 找出所屬店的所有業務和店長
        /// </summary>
        /// <param name="account"></param>
        /// <param name="isStoreManager">是否為店長</param>
        /// <returns></returns>
        Task<List<User>> GetStoreSales(string account, bool isStoreManager = false);

        /// <summary>
        /// 找出所屬店的所有業務和店長
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="isStoreManager">是否為店長</param>
        /// <returns></returns>
        Task<List<User>> GetStoreSalesByDepartmentId(string departmentId, bool isStoreManager = false);

        /// <summary>
        /// 微信註冊普通會員用戶
        /// </summary>
        /// <param name="userInPut">第三方類型</param>
        /// <returns></returns>
        bool CreateUserByWxOpenId(UserInputDto userInPut);
        /// <summary>
        /// 更新用戶
        /// </summary>
        /// <param name="userInPut"></param>
        /// <returns></returns>
        bool UpdateUserByOpenId(UserInputDto userInPut);

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        Task<PageResult<UserOutputDto>> FindWithPagerSearchAsync(SearchUserModel search);
    }
}
