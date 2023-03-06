using System;
using System.Data.Common;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Repositories;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using System.Data;
using Yuebon.Chaochi.Dtos;
using System.Collections.Generic;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Mapping;
using System.Threading.Tasks;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Enums;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerService : BaseService<Customer, UserOutputDto, string>, ICustomerService
    {
        private readonly ICustomerRepository _userRepository;
        private readonly Security.IRepositories.IUserLogOnRepository _userSigninRepository;
        private readonly Security.IServices.ILogService _logService;
        private readonly Security.IServices.IRoleService _roleService;
        private Security.IServices.IOrganizeService _organizeService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userLogOnRepository"></param>
        /// <param name="logService"></param>
        /// <param name="roleService"></param>
        /// <param name="organizeService"></param>
        public CustomerService(ICustomerRepository repository, Security.IRepositories.IUserLogOnRepository userLogOnRepository, Security.IServices.ILogService logService, Security.IServices.IRoleService roleService, Security.IServices.IOrganizeService organizeService) : base(repository)
        {
            _userRepository = repository;
            _userSigninRepository = userLogOnRepository;
            _logService = logService;
            _roleService = roleService;
            _organizeService = organizeService;
        }



        /// <summary>
        /// 用戶登陸驗證。
        /// </summary>
        /// <param name="userName">用戶名</param>
        /// <param name="password">密碼（第一次md5加密后）</param>
        /// <returns>驗證成功返回用戶實體，驗證失敗返回null|提示消息</returns>
        public async Task<Tuple<User, string>> Validate(string userName, string password)
        {
            var userEntity = await _userRepository.GetUserByLogin(userName);

            if (userEntity == null)
            {
                return new Tuple<User, string>(null, "系統不存在該用戶，請重新確認。");
            }

            if (!userEntity.EnabledMark)
            {
                return new Tuple<User, string>(null, "該用戶已被禁用，請聯系管理員。");
            }

            var userSinginEntity = _userSigninRepository.GetByUserId(userEntity.Id);

            string inputPassword = MD5Util.GetMD5_32(DEncrypt.Encrypt(MD5Util.GetMD5_32(password).ToLower(), userSinginEntity.UserSecretkey).ToLower()).ToLower();

            if (inputPassword != userSinginEntity.UserPassword)
            {
                return new Tuple<User, string>(null, "密碼錯誤，請重新輸入。");
            }
            else
            {
                Security.Models.UserLogOn userLogOn = _userSigninRepository.GetWhere("UserId='"+ userEntity.Id+ "'");
                if (userLogOn.AllowEndTime < DateTime.Now)
                {
                    return new Tuple<User, string>(null, "您的帳號已過期，請聯系系統管理員！");
                }
                if (userLogOn.LockEndDate >DateTime.Now)
                {
                    string dateStr=userLogOn.LockEndDate.ToEasyStringDQ();
                    return new Tuple<User, string>(null, "當前被鎖定，請"+ dateStr + "登錄");
                }
                if (userLogOn.FirstVisitTime == null)
                {
                    userLogOn.FirstVisitTime =userLogOn.PreviousVisitTime= DateTime.Now;
                }
                else
                {
                    userLogOn.PreviousVisitTime = DateTime.Now;
                }
                userLogOn.LogOnCount++;
                userLogOn.LastVisitTime = DateTime.Now;
                userLogOn.UserOnLine = true;
                await  _userSigninRepository.UpdateAsync(userLogOn,userLogOn.Id);
                return new Tuple<User, string>(userEntity, "");
            }
        }

        /// <summary>
        /// 用戶登陸驗證。
        /// </summary>
        /// <param name="userName">用戶名</param>
        /// <param name="password">密碼（第一次md5加密后）</param>
        /// <param name="userType">用戶類型</param>
        /// <returns>驗證成功返回用戶實體，驗證失敗返回null|提示消息</returns>
        public async Task<Tuple<User, string>> Validate(string userName, string password, UserType userType)
        {
            var userEntity = await _userRepository.GetUserByLogin(userName);

            if (userEntity == null)
            {
                return new Tuple<User, string>(null, "系統不存在該用戶，請重新確認。");
            }

            if (!userEntity.EnabledMark)
            {
                return new Tuple<User, string>(null, "該用戶已被禁用，請聯系管理員。");
            }

            var userSinginEntity = _userSigninRepository.GetByUserId(userEntity.Id);

            string inputPassword = MD5Util.GetMD5_32(DEncrypt.Encrypt(MD5Util.GetMD5_32(password).ToLower(), userSinginEntity.UserSecretkey).ToLower()).ToLower();

            if (inputPassword != userSinginEntity.UserPassword)
            {
                return new Tuple<User, string>(null, "密碼錯誤，請重新輸入。");
            }
            else
            {
                Security.Models.UserLogOn userLogOn = _userSigninRepository.GetWhere("UserId='" + userEntity.Id + "'");
                if (userLogOn.AllowEndTime < DateTime.Now)
                {
                    return new Tuple<User, string>(null, "您的帳號已過期，請聯系系統管理員！");
                }
                if (userLogOn.LockEndDate > DateTime.Now)
                {
                    string dateStr = userLogOn.LockEndDate.ToEasyStringDQ();
                    return new Tuple<User, string>(null, "當前被鎖定，請" + dateStr + "登錄");
                }
                if (userLogOn.FirstVisitTime == null)
                {
                    userLogOn.FirstVisitTime = userLogOn.PreviousVisitTime = DateTime.Now;
                }
                else
                {
                    userLogOn.PreviousVisitTime = DateTime.Now;
                }
                userLogOn.LogOnCount++;
                userLogOn.LastVisitTime = DateTime.Now;
                userLogOn.UserOnLine = true;
                await _userSigninRepository.UpdateAsync(userLogOn, userLogOn.Id);
                return new Tuple<User, string>(userEntity, "");
            }
        }

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User> GetByUserName(string userName)
        {
            return await _userRepository.GetByUserName(userName);
        }
        /// <summary>
        /// 根據用戶手機號碼查詢用戶信息
        /// </summary>
        /// <param name="mobilephone">手機號碼</param>
        /// <returns></returns>
        public async Task<User> GetUserByMobilePhone(string mobilephone)
        {
            return await _userRepository.GetUserByMobilePhone(mobilephone);
        }
        /// <summary>
        /// 根據Email、Account、手機號查詢用戶信息
        /// </summary>
        /// <param name="account">登錄帳號</param>
        /// <returns></returns>
        public async Task<User> GetUserByLogin(string account)
        {
            return await _userRepository.GetUserByLogin(account);
        }
        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public bool Insert(User entity, Security.Models.UserLogOn userLogOnEntity, IDbTransaction trans = null)
        {
            return _userRepository.Insert(entity, userLogOnEntity, trans);
        }

        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public async Task<bool> InsertAsync(User entity, Security.Models.UserLogOn userLogOnEntity, IDbTransaction trans = null)
        {
            return await _userRepository.InsertAsync(entity, userLogOnEntity, trans);
        }
        /// <summary>
        /// 註冊用戶,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public bool Insert(User entity, Security.Models.UserLogOn userLogOnEntity, Security.Models.UserOpenIds userOpenIds, IDbTransaction trans = null)
        {
            return _userRepository.Insert(entity, userLogOnEntity, userOpenIds, trans);
        }

        /// <summary>
        /// 根據第三方OpenId查詢用戶信息
        /// </summary>
        /// <param name="openIdType">第三方類型</param>
        /// <param name="openId">OpenId值</param>
        /// <returns></returns>
        public User GetUserByOpenId(string openIdType, string openId)
        {
            return _userRepository.GetUserByOpenId(openIdType, openId);
        }
        /// <summary>
        /// 根據userId查詢用戶信息
        /// </summary>
        /// <param name="openIdType">第三方類型</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public Security.Models.UserOpenIds GetUserOpenIdByuserId(string openIdType, string userId)
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
        public bool UpdateUserByOpenId(User entity, Security.Models.UserLogOn userLogOnEntity, Security.Models.UserOpenIds userOpenIds, IDbTransaction trans = null)
        {
            return _userRepository.UpdateUserByOpenId(entity, userLogOnEntity, userOpenIds, trans);
        }

        /// <summary>
        /// 根據微信UnionId查詢用戶信息
        /// </summary>
        /// <param name="unionId">UnionId值</param>
        /// <returns></returns>
        public User GetUserByUnionId(string unionId)
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

            User user = userInPut.MapTo<User>();
            Security.Models.UserLogOn userLogOnEntity = new Security.Models.UserLogOn();
            Security.Models.UserOpenIds userOpenIds = new Security.Models.UserOpenIds();

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
        //public bool UpdateUserByOpenId(UserInputDto userInPut)
        //{
        //    User user = GetUserByOpenId(userInPut.OpenIdType, userInPut.OpenId);
        //    user.HeadIcon = userInPut.HeadIcon;
        //    user.Country = userInPut.Country;
        //    user.Province = userInPut.Province;
        //    user.City = userInPut.City;
        //    user.Gender = userInPut.Gender;
        //    user.NickName = userInPut.NickName;
        //    user.UnionId = userInPut.UnionId;
        //    return _userRepository.Update(user, user.Id);
        //}


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
            List<Customer> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
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

    }
}