using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senparc.NeuChar.App.AppStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Common;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.Services;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Core.App;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IoC;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Net;
using Yuebon.Commons.Options;
using Yuebon.Security.Application;
using Yuebon.Security.Dtos;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;
using Yuebon.Security.Services;

namespace Yuebon.WebApi.Controllers
{
    /// <summary>
    /// 用戶登錄接口控制器
    /// </summary>
    [ApiController]
    [ApiVersionNeutral]
    //[ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LoginController : ApiController
    {
        private Security.IServices.IUserService _userService;
        private readonly ISendMailInfoService _sendMailInfoService;
        private readonly Chaochi.IServices.IToDoListService _toDoListService;
        private readonly IOrganizeService _organizeService;
        private IUserLogOnService _userLogOnService;
        private ISystemTypeService _systemTypeService;
        private IAPPService _appService;
        private IRoleService _roleService;
        private IRoleDataService _roleDataService;
        private ILogService _logService;
        private IFilterIPService _filterIPService;
        private IMenuService _menuService;

        /// <summary>
        /// 構造函數注入服務
        /// </summary>
        /// <param name="iService"></param>
        /// <param name="toDoListService"></param>
        /// <param name="organizeService"></param>
        /// <param name="userLogOnService"></param>
        /// <param name="systemTypeService"></param>
        /// <param name="logService"></param>
        /// <param name="appService"></param>
        /// <param name="roleService"></param>
        /// <param name="filterIPService"></param>
        /// <param name="roleDataService"></param>
        /// <param name="menuService"></param>
        public LoginController(Security.IServices.IUserService iService,ISendMailInfoService sendMailInfoService, Yuebon.Chaochi.IServices.IToDoListService toDoListService, IOrganizeService organizeService, IUserLogOnService userLogOnService, ISystemTypeService systemTypeService, ILogService logService, IAPPService appService, IRoleService roleService, IFilterIPService filterIPService, IRoleDataService roleDataService, IMenuService menuService)
        {
            _userService = iService;
            _sendMailInfoService = sendMailInfoService;
            _toDoListService = toDoListService;
            _organizeService = organizeService;
            _userLogOnService = userLogOnService;
            _systemTypeService = systemTypeService;
            _logService = logService;
            _appService = appService;
            _roleService = roleService;
            _filterIPService = filterIPService;
            _roleDataService = roleDataService;
            _menuService = menuService;
        }

        /// <summary>
        /// 用戶登錄，必須要有驗證碼
        /// </summary>
        /// <returns>返回用戶User對象</returns>
        [HttpPost("GetCheckUser")]
        [NoPermissionRequired]
        public async Task<IActionResult> GetCheckUser([FromBody] LoginViewModel loginModel)
        {
            CommonResult result = new CommonResult();
            RemoteIpParser remoteIpParser = new RemoteIpParser();
            string strIp = remoteIpParser.GetClientIp(HttpContext).MapToIPv4().ToString();
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            var vCode = yuebonCacheHelper.Get("ValidateCode" + loginModel.vkey);
            string code = vCode != null ? vCode.ToString() : "11";
            //// 20220307 暫時不驗證
            //if (loginModel.vcode.ToUpper() != code)
            //{
            //    result.ErrMsg = "驗證碼錯誤";
            //    return ToJsonContent(result);
            //}
            Log logEntity = new Log();
            bool blIp = _filterIPService.ValidateIP(strIp);
            if (blIp) {
                result.ErrMsg = strIp + "該IP已被管理員禁止登錄！";
            } else {

                if (string.IsNullOrEmpty(loginModel.username)) {
                    result.ErrMsg = "用戶名不能為空！";
                } else if (string.IsNullOrEmpty(loginModel.password)) {
                    result.ErrMsg = "密碼不能為空！";
                }
                if (string.IsNullOrEmpty(loginModel.systemCode)) {

                    result.ErrMsg = ErrCode.err40006;
                } else {
                    string strHost = Request.Host.ToString();
                    APP app = _appService.GetAPP(loginModel.appId);
                    if (app == null) {
                        result.ErrCode = "40001";
                        result.ErrMsg = ErrCode.err40001;
                    } else {
                        if (!app.RequestUrl.Contains(strHost, StringComparison.Ordinal) && !strHost.Contains("localhost", StringComparison.Ordinal)) {
                            result.ErrCode = "40002";
                            result.ErrMsg = ErrCode.err40002 + "，你當前請求主機：" + strHost;
                        } else {
                            SystemType systemType = _systemTypeService.GetByCode(loginModel.systemCode);
                            if (systemType == null) {
                                result.ErrMsg = ErrCode.err40006;
                            } else {
                                Tuple<Security.Models.User, string> userLogin = await _userService.Validate(loginModel.username, loginModel.password);
                                if (userLogin != null) {
                                    string ipAddressName = IpAddressUtil.GetCityByIp(strIp);
                                    if (userLogin.Item1 != null) {
                                        result.Success = true;
                                        Security.Models.User user = userLogin.Item1;
                                        JwtOption jwtModel = App.GetService<JwtOption>();
                                        TokenProvider tokenProvider = new TokenProvider(jwtModel);
                                        TokenResult tokenResult = tokenProvider.LoginToken(user, loginModel.appId);
                                        YuebonCurrentUser currentSession = new YuebonCurrentUser {
                                            UserId = user.Id,
                                            Name = user.RealName,
                                            AccessToken = tokenResult.AccessToken,
                                            AppKey = loginModel.appId,
                                            CreateTime = DateTime.Now,
                                            Role = _roleService.GetRoleEnCode(user.RoleId),
                                            ActiveSystemId = systemType.Id,
                                            CurrentLoginIP = strIp,
                                            IPAddressName = ipAddressName,
                                            ManagerId = user.ManagerId == user.Id ? _organizeService.Get(_organizeService.Get(user.DepartmentId).ParentId).ManagerId : user.ManagerId
                                        };
                                        TimeSpan expiresSliding = DateTime.Now.AddMinutes(120) - DateTime.Now;
                                        yuebonCacheHelper.Add("login_user_" + user.Id, currentSession, expiresSliding, true);

                                        var isExists = yuebonCacheHelper.Exists("User_IP" + user.Id);
                                        Console.WriteLine("isExists:" + isExists);
                                        if (isExists) {
                                            yuebonCacheHelper.Replace("User_IP" + user.Id, currentSession.CurrentLoginIP);
                                            Console.WriteLine("User_IP:" + user.Id + currentSession.CurrentLoginIP);
                                        } else {
                                            yuebonCacheHelper.Add("User_IP" + user.Id, currentSession.CurrentLoginIP);
                                            Console.WriteLine("User_IP:" + user.Id + currentSession.CurrentLoginIP);
                                        }

                                        List<AllowCacheApp> list = yuebonCacheHelper.Get("AllowAppId").ToJson().ToList<AllowCacheApp>();
                                        if (list == null || list.Count == 0) {
                                            IEnumerable<APP> appList = _appService.GetAllByIsNotDeleteAndEnabledMark();
                                            yuebonCacheHelper.Add("AllowAppId", appList);
                                        }
                                        CurrentUser = currentSession;
                                        result.ResData = currentSession;
                                        result.ErrCode = ErrCode.successCode;
                                        result.Success = true;

                                        logEntity.Account = user.Account;
                                        logEntity.NickName = user.NickName;
                                        logEntity.Date = logEntity.CreatorTime = DateTime.Now;
                                        logEntity.IPAddress = CurrentUser.CurrentLoginIP;
                                        logEntity.IPAddressName = CurrentUser.IPAddressName;
                                        logEntity.Result = true;
                                        logEntity.ModuleName = "登錄";
                                        logEntity.Description = "登錄成功";
                                        logEntity.Type = "Login";
                                        _logService.Insert(logEntity);
                                    } else {
                                        result.ErrCode = ErrCode.failCode;
                                        result.ErrMsg = userLogin.Item2;
                                        logEntity.Account = loginModel.username;
                                        logEntity.Date = logEntity.CreatorTime = DateTime.Now;
                                        logEntity.IPAddress = strIp;
                                        logEntity.IPAddressName = ipAddressName;
                                        logEntity.Result = false;
                                        logEntity.ModuleName = "登錄";
                                        logEntity.Type = "Login";
                                        logEntity.Description = "登錄失敗，" + userLogin.Item2;
                                        _logService.Insert(logEntity);
                                    }
                                }
                            }

                        }
                    }
                }
            }
            yuebonCacheHelper.Remove("LoginValidateCode");
            return ToJsonContent(result, true);
        }

        /// <summary>
        /// 獲取登錄用戶權限信息
        /// </summary>
        /// <returns>返回用戶User對象</returns>
        [HttpGet("GetUserInfo")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetUserInfo()
        {
            CommonResult result = new CommonResult();
            Security.Models.User user = _userService.Get(CurrentUser.UserId);
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            SystemType systemType = _systemTypeService.Get(CurrentUser.ActiveSystemId);
            var userOrgManagerId = _organizeService.Get(user.DepartmentId).ManagerId;
            var roleEncode = _roleService.GetRoleEnCode(user.RoleId);
            YuebonCurrentUser currentSession = new YuebonCurrentUser {
                UserId = user.Id,
                Account = user.Account,
                Name = user.RealName,
                NickName = user.NickName,
                AccessToken = CurrentUser.AccessToken,
                AppKey = CurrentUser.AppKey,
                CreateTime = DateTime.Now,
                HeadIcon = user.HeadIcon,
                Gender = user.Gender,
                ReferralUserId = user.ReferralUserId,
                MemberGradeId = user.MemberGradeId,
                Role = roleEncode,
                MobilePhone = user.MobilePhone,
                OrganizeId = user.OrganizeId,
                DeptId = user.DepartmentId,
                CurrentLoginIP = CurrentUser.CurrentLoginIP,
                IPAddressName = CurrentUser.IPAddressName,
                TenantId = "",
                ManagerId = userOrgManagerId == user.Id ? _organizeService.Get(_organizeService.Get(user.DepartmentId).ParentId).ManagerId : userOrgManagerId,
                TodoListCount = await _toDoListService.GetCountByWhereAsync($" Account = '{user.Account}' "),
                StoreManagerId = roleEncode.Contains("sales") ? _userService.GetStoreManager(user.Account) : ""
            };
            CurrentUser = currentSession;

            CurrentUser.ActiveSystemId = systemType.Id;
            CurrentUser.ActiveSystem = systemType.FullName;
            CurrentUser.ActiveSystemUrl = systemType.Url;

            List<MenuOutputDto> listFunction = new List<MenuOutputDto>();
            MenuApp menuApp = new MenuApp();
            if (Permission.IsAdmin(CurrentUser)) {
                CurrentUser.SubSystemList = _systemTypeService.GetAllByIsNotDeleteAndEnabledMark().MapTo<SystemTypeOutputDto>();
                //取得用戶可使用的授權功能信息，并存儲在緩存中
                listFunction = menuApp.GetFunctionsBySystem(CurrentUser.ActiveSystemId);
                CurrentUser.MenusRouter = menuApp.GetVueRouter("", systemType.EnCode);
            } else {
                CurrentUser.SubSystemList = _systemTypeService.GetSubSystemList(user.RoleId);
                //取得用戶可使用的授權功能信息，并存儲在緩存中
                listFunction = menuApp.GetFunctionsByUser(user.Id, CurrentUser.ActiveSystemId);
                CurrentUser.MenusRouter = menuApp.GetVueRouter(user.RoleId, systemType.EnCode);
                /**** 測試 IP 隱藏授權***/
                /*
                RemoteIpParser remoteIpParser = new RemoteIpParser();
                string strIp = remoteIpParser.GetClientIp(HttpContext).MapToIPv4().ToString();
                foreach (var menu in CurrentUser.MenusRouter)
                {
                    menu.hidden = true;
                }
                */
                /**** 測試 IP 隱藏授權***/
            }
            UserLogOn userLogOn = _userLogOnService.GetByUserId(CurrentUser.UserId);
            CurrentUser.UserTheme = userLogOn.Theme == null ? "default" : userLogOn.Theme;
            TimeSpan expiresSliding = DateTime.Now.AddMinutes(120) - DateTime.Now;
            yuebonCacheHelper.Add("User_Function_" + user.Id, listFunction, expiresSliding, true);
            List<string> listModules = new List<string>();
            foreach (MenuOutputDto item in listFunction) {
                listModules.Add(item.EnCode);
            }
            CurrentUser.Modules = listModules;
            yuebonCacheHelper.Add("login_user_" + user.Id, CurrentUser, expiresSliding, true);
            //該用戶的數據權限(獲取此腳色可訪問的組織ID)
            var roleRawList = await _roleService.GetListWhereAsync($"id in ('{user.RoleId.Replace(",", "','")}')");
            var roleTypeList = roleRawList.Select(item => item.Type);
            yuebonCacheHelper.Add("User_RoleType_" + user.Id, roleTypeList, expiresSliding, true);
            List<String> roleDateList = _roleDataService.GetListDeptByRole(user.RoleId);
            yuebonCacheHelper.Add("User_RoleData_" + user.Id, roleDateList, expiresSliding, true);
            var orgList = user.DepartmentId + "," + user.DepartmentIdSec + "," + user.DepartmentIdThird;
            var userPermissionDepts = await _userService.GetPermissionDepts(user.Id);
            yuebonCacheHelper.Add("User_PermissionDepts" + user.Id, userPermissionDepts, expiresSliding, true);
            //string userStoreManagerId = _userService.GetStoreManager(user.Account);
            //userStoreManagerId = (string.IsNullOrEmpty(userStoreManagerId)) ? "root" : userStoreManagerId;
            //yuebonCacheHelper.Add("User_StoreManagerID" + user.Id, userStoreManagerId, expiresSliding, true);
            result.ResData = CurrentUser;
            result.ErrCode = ErrCode.successCode;
            result.Success = true;
            return ToJsonContent(result, true);
        }

        /// <summary>
        /// 忘記密碼
        /// </summary>
        /// <returns>返回用戶User對象</returns>
        [HttpGet("ForgetPassword")]
        [NoPermissionRequired]
        public async Task<IActionResult> ForgetPassword(string userAccount)
        {
            var result = new CommonResult();
            try {
                var u = await _userService.GetByUserName(userAccount);
                if (u == null) {
                    result.Success = false;
                    result.ErrCode = ErrCode.err1;
                    result.ErrMsg = "查無此帳號!";
                    return ToJsonContent(result);
                }
                var ul = _userLogOnService.GetByUserId(u.Id);
                Random random = new Random();
                string newPwd = random.Next(100000, 999999).ToString(); //生成編號 


                ul.UserSecretkey = MD5Util.GetMD5_16(GuidUtils.NewGuidFormatN()).ToLower();
                ul.UserPassword = MD5Util.GetMD5_32(DEncrypt.Encrypt(MD5Util.GetMD5_32(newPwd).ToLower(), ul.UserSecretkey).ToLower()).ToLower();
                ul.ChangePasswordDate = DateTime.Now;
                bool bl = await _userLogOnService.UpdateAsync(ul, ul.Id);
                if (!bl) {
                    result.Success = false;
                    result.ErrCode = ErrCode.err1;
                    result.ErrMsg = "忘記密碼失敗!";
                    return ToJsonContent(result);
                }
                await _sendMailInfoService.InsertAsync(new SendMailInfo {
                    Subject = $"[密碼重製]*您的密碼已重製",
                    Recipient = u.Email,
                    Enable = true,
                    SendTime = DateTime.Now,
                    Body = $"<h3>您的密碼已重製，請勿透漏給其他人：</h3>{newPwd}",
                });
            } catch (Exception ex) {
                Log4NetHelper.Error("",ex);
                result.Success = false;
                result.ErrCode = ErrCode.err1;
                result.ErrMsg = "忘記密碼失敗!";
                return ToJsonContent(result);
                throw;
            }
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 用戶登錄，無驗證碼，主要用于app登錄
        /// </summary>
        /// <param name="username">用戶名</param>
        /// <param name="password">密碼</param>
        /// <param name="appId">AppId</param>
        /// <param name="systemCode">系統編碼</param>
        /// <returns>返回用戶User對象</returns>
        [HttpGet("UserLogin")]
        [ApiVersion("2.0")]
        [NoPermissionRequired]
        public async Task<IActionResult> UserLogin(string username, string password, string appId, string systemCode)
        {
            CommonResult result = new CommonResult();
            RemoteIpParser remoteIpParser = new RemoteIpParser();
            string strIp = remoteIpParser.GetClientIp(HttpContext).MapToIPv4().ToString();
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            Log logEntity = new Log();
            bool blIp = _filterIPService.ValidateIP(strIp);
            if (blIp) {
                result.ErrMsg = strIp + "該IP已被管理員禁止登錄！";
            } else {
                if (string.IsNullOrEmpty(username)) {
                    result.ErrMsg = "用戶名不能為空！";
                } else if (string.IsNullOrEmpty(password)) {
                    result.ErrMsg = "密碼不能為空！";
                }
                if (string.IsNullOrEmpty(systemCode)) {

                    result.ErrMsg = ErrCode.err40006;
                } else {
                    string strHost = Request.Host.ToString();
                    APP app = _appService.GetAPP(appId);
                    if (app == null) {
                        result.ErrCode = "40001";
                        result.ErrMsg = ErrCode.err40001;
                    } else {
                        if (!app.RequestUrl.Contains(strHost, StringComparison.Ordinal) && !strHost.Contains("localhost", StringComparison.Ordinal)) {
                            result.ErrCode = "40002";
                            result.ErrMsg = ErrCode.err40002 + "，你當前請求主機：" + strHost;
                        } else {
                            SystemType systemType = _systemTypeService.GetByCode(systemCode);
                            if (systemType == null) {
                                result.ErrMsg = ErrCode.err40006;
                            } else {
                                Tuple<Security.Models.User, string> userLogin = await this._userService.Validate(username, password);
                                if (userLogin != null) {

                                    string ipAddressName = IpAddressUtil.GetCityByIp(strIp);
                                    if (userLogin.Item1 != null) {
                                        result.Success = true;

                                        Security.Models.User user = userLogin.Item1;

                                        JwtOption jwtModel = App.GetService<JwtOption>();
                                        TokenProvider tokenProvider = new TokenProvider(jwtModel);
                                        TokenResult tokenResult = tokenProvider.LoginToken(user, appId);
                                        YuebonCurrentUser currentSession = new YuebonCurrentUser {
                                            UserId = user.Id,
                                            Name = user.RealName,
                                            AccessToken = tokenResult.AccessToken,
                                            AppKey = appId,
                                            CreateTime = DateTime.Now,
                                            Role = _roleService.GetRoleEnCode(user.RoleId),
                                            ActiveSystemId = systemType.Id,
                                            CurrentLoginIP = strIp,
                                            IPAddressName = ipAddressName

                                        };
                                        TimeSpan expiresSliding = DateTime.Now.AddMinutes(120) - DateTime.Now;
                                        yuebonCacheHelper.Add("login_user_" + user.Id, currentSession, expiresSliding, true);
                                        CurrentUser = currentSession;
                                        result.ResData = currentSession;
                                        result.ErrCode = ErrCode.successCode;
                                        result.Success = true;

                                        logEntity.Account = user.Account;
                                        logEntity.NickName = user.NickName;
                                        logEntity.Date = logEntity.CreatorTime = DateTime.Now;
                                        logEntity.IPAddress = CurrentUser.CurrentLoginIP;
                                        logEntity.IPAddressName = CurrentUser.IPAddressName;
                                        logEntity.Result = true;
                                        logEntity.ModuleName = "登錄";
                                        logEntity.Description = "登錄成功";
                                        logEntity.Type = "Login";
                                        _logService.Insert(logEntity);
                                    } else {
                                        result.ErrCode = ErrCode.failCode;
                                        result.ErrMsg = userLogin.Item2;
                                        logEntity.Account = username;
                                        logEntity.Date = logEntity.CreatorTime = DateTime.Now;
                                        logEntity.IPAddress = strIp;
                                        logEntity.IPAddressName = ipAddressName;
                                        logEntity.Result = false;
                                        logEntity.ModuleName = "登錄";
                                        logEntity.Type = "Login";
                                        logEntity.Description = "登錄失敗，" + userLogin.Item2;
                                        _logService.Insert(logEntity);
                                    }
                                }
                            }

                        }
                    }
                }
            }
            return ToJsonContent(result, true);
        }


        /// <summary>
        /// 退出登錄
        /// </summary>
        /// <returns></returns>
        [HttpGet("Logout")]
        [YuebonAuthorize("")]
        public IActionResult Logout()
        {
            CommonResult result = new CommonResult();
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            yuebonCacheHelper.Remove("login_user_" + CurrentUser.UserId);
            yuebonCacheHelper.Remove("User_Function_" + CurrentUser.UserId);
            UserLogOn userLogOn = _userLogOnService.GetWhere("UserId='" + CurrentUser.UserId + "'");
            userLogOn.UserOnLine = false;
            _userLogOnService.Update(userLogOn, userLogOn.Id);
            CurrentUser = null;
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = "成功退出";
            return ToJsonContent(result);
        }

        /// <summary>
        /// 子系統切換登錄
        /// </summary>
        /// <param name="openmf">憑據</param>
        /// <param name="appId">應用Id</param>
        /// <param name="systemCode">子系統編碼</param>
        /// <returns>返回用戶User對象</returns>
        [HttpGet("SysConnect")]
        [AllowAnonymous]
        [NoPermissionRequired]
        public IActionResult SysConnect(string openmf, string appId, string systemCode)
        {
            CommonResult result = new CommonResult();
            RemoteIpParser remoteIpParser = new RemoteIpParser();
            string strIp = remoteIpParser.GetClientIp(HttpContext).MapToIPv4().ToString();
            if (string.IsNullOrEmpty(openmf)) {
                result.ErrMsg = "切換參數錯誤！";
            }

            bool blIp = _filterIPService.ValidateIP(strIp);
            if (blIp) {
                result.ErrMsg = strIp + "該IP已被管理員禁止登錄！";
            } else {
                string ipAddressName = IpAddressUtil.GetCityByIp(strIp);
                if (string.IsNullOrEmpty(systemCode)) {
                    result.ErrMsg = ErrCode.err40006;
                } else {
                    string strHost = Request.Host.ToString();
                    APP app = _appService.GetAPP(appId);
                    if (app == null) {
                        result.ErrCode = "40001";
                        result.ErrMsg = ErrCode.err40001;
                    } else {
                        if (!app.RequestUrl.Contains(strHost, StringComparison.Ordinal) && !strHost.Contains("localhost", StringComparison.Ordinal)) {
                            result.ErrCode = "40002";
                            result.ErrMsg = ErrCode.err40002 + "，你當前請求主機：" + strHost;
                        } else {
                            SystemType systemType = _systemTypeService.GetByCode(systemCode);
                            if (systemType == null) {
                                result.ErrMsg = ErrCode.err40006;
                            } else {
                                YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                                object cacheOpenmf = yuebonCacheHelper.Get("openmf" + openmf);
                                yuebonCacheHelper.Remove("openmf" + openmf);
                                if (cacheOpenmf == null) {
                                    result.ErrCode = "40007";
                                    result.ErrMsg = ErrCode.err40007;
                                } else {
                                    Security.Models.User user = _userService.Get(cacheOpenmf.ToString());
                                    if (user != null) {
                                        result.Success = true;
                                        JwtOption jwtModel = App.GetService<JwtOption>();
                                        TokenProvider tokenProvider = new TokenProvider(jwtModel);
                                        TokenResult tokenResult = tokenProvider.LoginToken(user, appId);
                                        YuebonCurrentUser currentSession = new YuebonCurrentUser {
                                            UserId = user.Id,
                                            Name = user.RealName,
                                            AccessToken = tokenResult.AccessToken,
                                            AppKey = appId,
                                            CreateTime = DateTime.Now,
                                            Role = _roleService.GetRoleEnCode(user.RoleId),
                                            ActiveSystemId = systemType.Id,
                                            CurrentLoginIP = strIp,
                                            IPAddressName = ipAddressName,
                                            ActiveSystemUrl = systemType.Url

                                        };
                                        TimeSpan expiresSliding = DateTime.Now.AddMinutes(120) - DateTime.Now;
                                        yuebonCacheHelper.Add("login_user_" + user.Id, currentSession, expiresSliding, true);
                                        CurrentUser = currentSession;
                                        result.ResData = currentSession;
                                        result.ErrCode = ErrCode.successCode;
                                        result.Success = true;
                                    } else {
                                        result.ErrCode = ErrCode.failCode;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ToJsonContent(result);
        }
    }
}