using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Pages;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 用戶服務接口
    /// </summary>
    public interface IUserDService:IService<UserD, UserOutputDto, string>
    {

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<UserD> GetByUserName(string userName);

        /// <summary>
        /// 根據用戶手機號碼查詢用戶信息
        /// </summary>
        /// <param name="mobilePhone">手機號碼</param>
        /// <returns></returns>
        Task<UserD> GetUserByMobilePhone(string mobilePhone);
        /// <summary>
        /// 根據Email、Account、手機號查詢用戶信息
        /// </summary>
        /// <param name="account">登錄帳號</param>
        /// <returns></returns>
        Task<UserD> GetUserByLogin(string account);

        /// <summary>
        /// 檢查用戶是否存在
        /// </summary>
        /// <param name="account">用戶帳號</param>
        /// <returns></returns>
        bool CheckUserExist(string account, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        bool Insert(UserD entity, UserLogOnD userLogOnEntity, IDbTransaction trans = null);
        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        Task<bool> InsertAsync(UserD entity, UserLogOnD userLogOnEntity, IDbTransaction trans = null);
        /// <summary>
        /// 註冊用戶,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="userOpenIds"></param>
        /// <param name="trans"></param>
        bool Insert(UserD entity, UserLogOnD userLogOnEntity, UserOpenIds userOpenIds,IDbTransaction trans = null);
        /// <summary>
        /// 根據第三方OpenId查詢用戶信息
        /// </summary>
        /// <param name="openIdType">第三方類型</param>
        /// <param name="openId">OpenId值</param>
        /// <returns></returns>
        UserD GetUserByOpenId(string openIdType, string openId);

        /// <summary>
        /// 根據微信UnionId查詢用戶信息
        /// </summary>
        /// <param name="unionId">UnionId值</param>
        /// <returns></returns>
        UserD GetUserByUnionId(string unionId);
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
        bool UpdateUserByOpenId(UserD entity, UserLogOnD userLogOnEntity, UserOpenIds userOpenIds, IDbTransaction trans = null);

        /// <summary>
        /// 獲取可訪問的組織部門
        /// </summary>
        /// <param name="deptList"></param>
        /// <returns></returns>
        List<string> GetPermissionDepts(string deptList);

        /// <summary>
        /// 找出登入者的所屬店長
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        string GetStoreManager(string account);

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

        /// <summary>
        /// 取要被停用的用戶
        /// </summary>
        /// <param name="userImportList">匯入的用戶清單</param>
        /// <returns></returns>
        List<UserD> GetObsoleteUserList(List<UserD> userImportList, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 取要被停用的用戶ID清單
        /// </summary>
        /// <param name="userIdImportList">匯入的用戶ID清單</param>
        /// <returns></returns>
        public List<string> GetObsoleteUserIdList(List<string> userIdImportList, IDbConnection conn = null, IDbTransaction trans = null);
    }
}
