using System;
using Yuebon.Commons.Services;
using Yuebon.Security.IRepositories;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;
using System.Data;
using Yuebon.Security.Dtos;
using System.Collections.Generic;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Mapping;
using System.Threading.Tasks;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Pages;
using System.Linq;

namespace Yuebon.Security.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserDService : BaseService<UserD, UserOutputDto, string>, IUserDService
    {
        private readonly IUserDRepository _userRepository;
        private readonly IUserLogOnDRepository _userSigninRepository;
        private readonly ILogService _logService;
        private readonly IRoleService _roleService;
        private IOrganizeService _organizeService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userLogOnRepository"></param>
        /// <param name="logService"></param>
        /// <param name="roleService"></param>
        /// <param name="organizeService"></param>
        public UserDService(IUserDRepository repository, IUserLogOnDRepository userLogOnRepository, ILogService logService, IRoleService roleService, IOrganizeService organizeService) : base(repository)
        {
            _userRepository = repository;
            _userSigninRepository = userLogOnRepository;
            _logService = logService;
            _roleService = roleService;
            _organizeService = organizeService;
        }



        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<UserD> GetByUserName(string userName)
        {
            return await _userRepository.GetByUserName(userName);
        }
        /// <summary>
        /// 根據用戶手機號碼查詢用戶信息
        /// </summary>
        /// <param name="mobilephone">手機號碼</param>
        /// <returns></returns>
        public async Task<UserD> GetUserByMobilePhone(string mobilephone)
        {
            return await _userRepository.GetUserByMobilePhone(mobilephone);
        }
        /// <summary>
        /// 根據Email、Account、手機號查詢用戶信息
        /// </summary>
        /// <param name="account">登錄帳號</param>
        /// <returns></returns>
        public async Task<UserD> GetUserByLogin(string account)
        {
            return await _userRepository.GetUserByLogin(account);
        }

        /// <summary>
        /// 檢查用戶是否存在
        /// </summary>
        /// <param name="account">用戶帳號</param>
        /// <returns></returns>
        public bool CheckUserExist(string account, IDbConnection conn = null, IDbTransaction trans = null)
        {
            UserD org = _userRepository.GetWhere($"Account = {account}", conn, trans);
            return org != null;
        }

        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public bool Insert(UserD entity, UserLogOnD userLogOnEntity, IDbTransaction trans = null)
        {
            return _userRepository.Insert(entity, userLogOnEntity, trans);
        }

        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public async Task<bool> InsertAsync(UserD entity, UserLogOnD userLogOnEntity, IDbTransaction trans = null)
        {
            return await _userRepository.InsertAsync(entity, userLogOnEntity, trans);
        }
        /// <summary>
        /// 註冊用戶,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public bool Insert(UserD entity, UserLogOnD userLogOnEntity, UserOpenIds userOpenIds, IDbTransaction trans = null)
        {
            return _userRepository.Insert(entity, userLogOnEntity, userOpenIds, trans);
        }

        /// <summary>
        /// 根據第三方OpenId查詢用戶信息
        /// </summary>
        /// <param name="openIdType">第三方類型</param>
        /// <param name="openId">OpenId值</param>
        /// <returns></returns>
        public UserD GetUserByOpenId(string openIdType, string openId)
        {
            return _userRepository.GetUserByOpenId(openIdType, openId);
        }
        /// <summary>
        /// 根據userId查詢用戶信息
        /// </summary>
        /// <param name="openIdType">第三方類型</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public UserOpenIds GetUserOpenIdByuserId(string openIdType, string userId)
        {
            return _userRepository.GetUserOpenIdByuserId(openIdType, userId);
        }
        /// <summary>
        /// 更新用戶信息,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="userOpenIds"></param>
        /// <param name="trans"></param>
        public bool UpdateUserByOpenId(UserD entity, UserLogOnD userLogOnEntity, UserOpenIds userOpenIds, IDbTransaction trans = null)
        {
            return _userRepository.UpdateUserByOpenId(entity, userLogOnEntity, userOpenIds, trans);
        }

        /// <summary>
        /// 根據微信UnionId查詢用戶信息
        /// </summary>
        /// <param name="unionId">UnionId值</param>
        /// <returns></returns>
        public UserD GetUserByUnionId(string unionId)
        {
            return _userRepository.GetUserByUnionId(unionId);
        }


        /// <summary>
        /// 微信註冊普通會員用戶
        /// </summary>
        /// <param name="userInPut">第三方類型</param>
        /// <returns></returns>
        public bool CreateUserByWxOpenId(UserInputDto userInPut)
        {

            UserD user = userInPut.MapTo<UserD>();
            UserLogOnD userLogOnEntity = new UserLogOnD();
            UserOpenIds userOpenIds = new UserOpenIds();

            user.Id = user.CreatorUserId = GuidUtils.CreateNo();
            user.Account = "Wx" + GuidUtils.CreateNo();
            user.CreatorTime = userLogOnEntity.FirstVisitTime = DateTime.Now;
            user.IsAdministrator = false;
            user.EnabledMark = true;
            user.Description = "第三方註冊";
            user.IsMember = true;
            user.UnionId = userInPut.UnionId;
            user.ReferralUserId = userInPut.ReferralUserId;
            if (userInPut.NickName == "游客")
            {
                user.RoleId = _roleService.GetRole("guest").Id;
            }
            else
            {
                user.RoleId = _roleService.GetRole("usermember").Id;
            }

            userLogOnEntity.UserId = user.Id;

            userLogOnEntity.UserPassword = GuidUtils.NewGuidFormatN() + new Random().Next(100000, 999999).ToString();
            userLogOnEntity.Language = userInPut.language;

            userOpenIds.OpenId = userInPut.OpenId;
            userOpenIds.OpenIdType = userInPut.OpenIdType;
            userOpenIds.UserId = user.Id;
            return _userRepository.Insert(user, userLogOnEntity, userOpenIds);
        }
        /// <summary>
        /// 更新用戶
        /// </summary>
        /// <param name="userInPut"></param>
        /// <returns></returns>
        public bool UpdateUserByOpenId(UserInputDto userInPut)
        {
            UserD user = GetUserByOpenId(userInPut.OpenIdType, userInPut.OpenId);
            user.HeadIcon = userInPut.HeadIcon;
            user.Country = userInPut.Country;
            user.Province = userInPut.Province;
            user.City = userInPut.City;
            user.Gender = userInPut.Gender;
            user.NickName = userInPut.NickName;
            user.UnionId = userInPut.UnionId;
            return _userRepository.Update(user, user.Id);
        }


        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public  async Task<PageResult<UserOutputDto>> FindWithPagerSearchAsync(SearchUserModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);

            if (!string.IsNullOrEmpty(search.Keywords))
            {
                where += string.Format(" and (NickName like '%{0}%' or Account like '%{0}%' or RealName  like '%{0}%' or MobilePhone like '%{0}%')", search.Keywords);
            }

            if (!string.IsNullOrEmpty(search.RoleId))
            {
                where += string.Format(" and RoleId like '%{0}%'", search.RoleId);
            }
            if (!string.IsNullOrEmpty(search.CreatorTime1))
            {
                where += " and CreatorTime >='" + search.CreatorTime1 + " 00:00:00'";
            }
            if (!string.IsNullOrEmpty(search.CreatorTime2))
            {
                where += " and CreatorTime <='" + search.CreatorTime2 + " 23:59:59'";
            }
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<UserD> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<UserOutputDto> resultList = list.MapTo<UserOutputDto>();
            List<UserOutputDto> listResult = new List<UserOutputDto>();
            foreach (UserOutputDto item in resultList)
            {
                if (!string.IsNullOrEmpty(item.OrganizeId))
                {
                    item.OrganizeName = _organizeService.Get(item.OrganizeId).FullName;
                }
                if (!string.IsNullOrEmpty(item.RoleId))
                {
                    item.RoleName = _roleService.GetRoleNameStr(item.RoleId);
                }
                if (!string.IsNullOrEmpty(item.DepartmentId))
                {
                    item.DepartmentName = _organizeService.Get(item.DepartmentId).FullName;
                }
                //if (!string.IsNullOrEmpty(item.DutyId))
                //{
                //    item.DutyName = _roleService.Get(item.DutyId).FullName;
                //}
                listResult.Add(item);
            }
            PageResult<UserOutputDto> pageResult = new PageResult<UserOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        /// <summary>
        /// 獲取可訪問的組織部門
        /// </summary>
        /// <param name="deptList"></param>
        /// <returns></returns>
        public List<string> GetPermissionDepts(string deptList)
        {
            return _userRepository.GetPermissionDepts(deptList);
        }

        /// <summary>
        /// 找出登入者的所屬店長
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string GetStoreManager(string account)
        {
            return _userRepository.GetStoreManager(account);
        }

        /// <summary>
        /// 取要被停用的用戶
        /// </summary>
        /// <param name="userImportList">匯入的用戶清單</param>
        /// <returns></returns>
        public List<UserD> GetObsoleteUserList(List<UserD> userImportList, IDbConnection conn = null, IDbTransaction trans = null)
        {
            IEnumerable<UserD> userDBList = _userRepository.GetAll(conn, trans);
            List<UserD> obsoleteUsers = userDBList.Except(userImportList, new UserDComparer()).ToList();
            List<UserD> obsoleteUsers2 = userImportList.Except(userDBList).ToList();

            return obsoleteUsers;
        }

        /// <summary>
        /// 取要被停用的用戶ID清單
        /// </summary>
        /// <param name="userIdImportList">匯入的用戶ID清單</param>
        /// <returns></returns>
        public List<string> GetObsoleteUserIdList(List<string> userIdImportList, IDbConnection conn = null, IDbTransaction trans = null)
        {
            List<string> userDBList = _userRepository.GetAll(conn, trans).ToList(x => x.Id);
            List<string> obsoleteUsers = userDBList.Except(userIdImportList).ToList();

            return obsoleteUsers;
        }


    }
}