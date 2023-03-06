using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Security.Dtos;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;
using Yuebon.WebApi.Areas.Security.Models;

namespace Yuebon.WebApi.Areas.Security.Controllers
{
    /// <summary>
    /// 用戶接口
    /// </summary>
    [ApiController]
    [Route("api/Security/[controller]")]
    public class UserController : AreaApiController<User, UserOutputDto, UserInputDto, IUserService,string>
    {
        private IOrganizeService organizeService;
        private IRoleService roleService;
        private IUserLogOnService userLogOnService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_organizeService"></param>
        /// <param name="_roleService"></param>
        /// <param name="_userLogOnService"></param>
        public UserController(IUserService _iService, IOrganizeService _organizeService, IRoleService _roleService, IUserLogOnService _userLogOnService) : base(_iService)
        {
            iService = _iService;
            organizeService = _organizeService;
            roleService = _roleService;
            userLogOnService = _userLogOnService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(User info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.OrganizeId = organizeService.GetRootOrganize(info.DepartmentId).Id;
            info.DeleteMark = false;
            if (info.SortCode == null)
            {
                info.SortCode = 99;
            }
        }
        
        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(User info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
            info.OrganizeId = organizeService.GetRootOrganize(info.DepartmentId).Id;
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(User info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }

        /// <summary>
        /// 取的當前使用者可觸摸到的userlist
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPermissionUserList")]
        [YuebonAuthorize("CreatorId")]
        public async Task<IActionResult> GetPermissionUserList()
        {
            var result = new CommonResult();
            var yuebonCacheHelper = new YuebonCacheHelper();
            var list = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + CurrentUser.UserId).ToJson()).ToArray();
            string test = $"DepartmentId in (' {string.Join("','", list)} ')";
            var user = await iService.GetListWhereAsync($"DepartmentId in ('{string.Join("','", list)}') ");
            var nameList = new List<User>();
            if (user.Count() > 0) {
                foreach (var item in user) {
                    nameList.Add(item);
                }
            }
            result.ResData = nameList;
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 找出所屬店的所有業務和店長
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStoreSales")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetStoreSales() {
            var result = new CommonResult();
            var accountList = await iService.GetStoreSales(CurrentUser.UserId);

            result.ResData = accountList;
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 用於下拉選單
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        [HttpGet("GetUserListByDept")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetUserListByDept(string deptId)
        {
            var result = new CommonResult();
            
            try {
                var datalist = await iService.GetListWhereAsync($" DepartmentId = '{deptId}' ");
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = datalist;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取某部門USER時發生錯誤", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 用戶註冊
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        [NoPermissionRequired]
        public  async Task<IActionResult> RegisterAsync(RegisterViewModel tinfo)
        {
            CommonResult result = new CommonResult();
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            var vCode = yuebonCacheHelper.Get("ValidateCode" + tinfo.VerifyCodeKey);
            string code = vCode != null ? vCode.ToString() : "11";
            if (code!= tinfo.VerificationCode.ToUpper())
            {
                result.ErrMsg = "驗證碼錯誤";
                return ToJsonContent(result);
            }
            if (!string.IsNullOrEmpty(tinfo.Account))
            {
                if (string.IsNullOrEmpty(tinfo.Password) || tinfo.Password.Length < 6)
                {
                    result.ErrMsg = "密碼不能為空或小于6位";
                    return ToJsonContent(result);
                }
                User user =await iService.GetByUserName(tinfo.Account);
                if (user != null)
                {
                    result.ErrMsg = "登錄帳號不能重復";
                    return ToJsonContent(result);
                }
            }
            else
            {
                result.ErrMsg = "登錄帳號不能為空";
                return ToJsonContent(result);
            }
            User info = new User();
            info.Id = GuidUtils.CreateNo();
            info.Account = tinfo.Account;
            info.Email = tinfo.Email;
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = info.Id;
            info.OrganizeId = "";
            info.EnabledMark = true;
            info.IsAdministrator = false;
            info.IsMember = true;
            info.RoleId =roleService.GetRole("usermember").Id;
            info.DeleteMark = false;
            info.SortCode = 99;

            UserLogOn userLogOn = new UserLogOn();
            userLogOn.UserPassword = tinfo.Password;
            userLogOn.AllowStartTime = userLogOn.LockEndDate = userLogOn.LockStartDate = userLogOn.ChangePasswordDate = DateTime.Now;
            userLogOn.AllowEndTime = DateTime.Now.AddYears(100);
            userLogOn.MultiUserLogin = userLogOn.CheckIPAddress = false;
            userLogOn.LogOnCount = 0;
            result.Success = await iService.InsertAsync(info, userLogOn);
            if (result.Success)
            {
                yuebonCacheHelper.Remove("ValidateCode");
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.ErrMsg = ErrCode.err43001;
                result.ErrCode = "43001";
            }
            return ToJsonContent(result);
        }
        /// <summary>
        /// 異步新增數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(UserInputDto tinfo)
        {
            CommonResult result = new CommonResult();

            if (!string.IsNullOrEmpty(tinfo.Account))
            {
                string where = string.Format("Account='{0}' or Email='{0}' or MobilePhone='{0}'", tinfo.Account);
                User user = iService.GetWhere(where);
                if (user != null)
                {
                    result.ErrMsg = "登錄帳號不能重復";
                    return ToJsonContent(result);
                }
            }
            else
            {
                result.ErrMsg = "登錄帳號不能為空";
                return ToJsonContent(result);
            }
            User info = tinfo.MapTo<User>();
            OnBeforeInsert(info);
            UserLogOn userLogOn = new UserLogOn();
            userLogOn.UserPassword = "12345678";
            userLogOn.AllowStartTime =userLogOn.LockEndDate=userLogOn.LockStartDate=userLogOn.ChangePasswordDate= DateTime.Now;
            userLogOn.AllowEndTime = DateTime.Now.AddYears(100);
            userLogOn.MultiUserLogin = userLogOn.CheckIPAddress= false;
            userLogOn.LogOnCount = 0;
            result.Success = await iService.InsertAsync(info, userLogOn);
            if (result.Success)
            {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.ErrMsg = ErrCode.err43001;
                result.ErrCode = "43001";
            }
            return ToJsonContent(result);
        }
        /// <summary>
        /// 異步更新數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(UserInputDto tinfo)
        {
            CommonResult result = new CommonResult();
            if (!string.IsNullOrEmpty(tinfo.Account))
            {
                string where = string.Format(" Account='{0}'  and id!='{1}' ", tinfo.Account, tinfo.Id);
                User user = iService.GetWhere(where);
                if (user != null)
                {
                    result.ErrMsg = "登錄帳號不能重復";
                    return ToJsonContent(result);
                }
            }
            else
            {
                result.ErrMsg = "登錄帳號不能為空";
                return ToJsonContent(result);
            }
            User info = iService.Get(tinfo.Id);
            info.Account = tinfo.Account;
            info.HeadIcon = tinfo.HeadIcon;
            info.RealName = tinfo.RealName;
            info.NickName = tinfo.NickName;
            info.Gender = tinfo.Gender;
            info.Birthday = tinfo.Birthday;
            info.MobilePhone = tinfo.MobilePhone;
            info.WeChat = tinfo.WeChat;
            info.DepartmentId = tinfo.DepartmentId;
            info.RoleId = tinfo.RoleId;
            info.IsAdministrator = tinfo.IsAdministrator;
            info.Email = tinfo.Email;
            info.EnabledMark = tinfo.EnabledMark;
            info.Description = tinfo.Description;

            OnBeforeUpdate(info);
            bool bl = await iService.UpdateAsync(info, tinfo.Id).ConfigureAwait(false);
            if (bl)
            {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取指派業務的下拉選單
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSales")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetSales()
        {
            CommonResult result = new CommonResult();
            try {
                //Dictionary<string, string> salesList = new Dictionary<string, string>();
                List<User> salesList = new List<User>();
                var roles = CurrentUser.Role;
                var userId = CurrentUser.UserId;
                var userAccount = CurrentUser.Account;
                var userName = CurrentUser.Name;
                var userDeptId = CurrentUser.DeptId;
                if (!string.IsNullOrEmpty(roles)) {
                    if (roles.Contains("sales")) {
                        var self = await iService.GetUserByLogin(userAccount);                        
                        salesList.Add(self);
                    } else if (roles.Contains("storemanager")) {
                        salesList = await iService.GetStoreSales(userId, true);
                    } else if (roles.Contains("District manager")) {
                        var childOrganizeList = await organizeService.GetChildOrganizeIdList(userDeptId);
                        if (childOrganizeList != null && childOrganizeList.Count > 0) {
                            foreach (var childOrganize in childOrganizeList) {
                                var sales = await iService.GetStoreSalesByDepartmentId(childOrganize);                                
                                salesList.AddRange(sales);
                            }
                            User dummy = new User();
                            dummy.Account = "等待指派";
                            dummy.RealName = "待指派";
                            salesList.Insert(0, dummy);
                        }
                    } else if (roles.Contains("Business assistance")) {
                        salesList = await iService.GetStoreSales(userId);
                    }
                }

                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = salesList;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取指派業務異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取指派業務的下拉選單
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSalesByDepartmentId")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetSalesByDepartmentId(string departmentId)
        {
            CommonResult result = new CommonResult();
            try {
                List<User> salesList = new List<User>();
                var roles = CurrentUser.Role;
                var userAccount = CurrentUser.Account;
                var userName = CurrentUser.Name;
                if (!string.IsNullOrEmpty(roles)) {
                    if (roles.Contains("sales")) {
                        var self = await iService.GetUserByLogin(userAccount);
                        salesList.Add(self);
                    } else if (roles.Contains("storemanager") || roles.Contains("District manager")) {
                        salesList = await iService.GetStoreSalesByDepartmentId(departmentId, true);
                    } else if (roles.Contains("Business assistance")) {
                        salesList = await iService.GetStoreSalesByDepartmentId(departmentId);
                    }
                }

                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = salesList;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取指派業務異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 根據用戶登錄帳號獲取詳細信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>

        [HttpGet("GetByUserName")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            CommonResult result = new CommonResult();
            try {
                User user = await iService.GetByUserName(userName);
                result.ResData = user.MapTo<UserOutputDto>();
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取用戶異常", ex);//錯誤記錄
                result.ErrMsg = ex.Message;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 根據用戶登錄帳號獲取詳細信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>

        [HttpGet("GetByUserId")]
        //[YuebonAuthorize("Edit")]
        public async Task<IActionResult> GetByUserId(string account)
        {
            CommonResult result = new CommonResult();
            try {
                User user = await iService.GetUserByLogin(account);
                result.ResData = user.MapTo<UserOutputDto>();
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取用戶異常", ex);//錯誤記錄
                result.ErrMsg = ex.Message;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步分頁查詢
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerSearchAsync")]
        [YuebonAuthorize("List")]
        public  async Task<IActionResult> FindWithPagerSearchAsync(SearchUserModel search)
        {
            CommonResult<PageResult<UserOutputDto>> result = new CommonResult<PageResult<UserOutputDto>>();
            result.ResData = await iService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }


        /// <summary>
        /// 重置密碼
        /// </summary>
        /// <returns></returns>
        [HttpPost("ResetPassword")]
        [YuebonAuthorize("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string userId)
        {
            CommonResult result = new CommonResult();
            try
            {
                string where = string.Format("UserId='{0}'", userId);
                UserLogOn userLogOn = userLogOnService.GetWhere(where);
                Random random = new Random();
                string strRandom = random.Next(100000, 999999).ToString(); //生成編號 
                userLogOn.UserSecretkey = MD5Util.GetMD5_16(GuidUtils.NewGuidFormatN()).ToLower();
                userLogOn.UserPassword = MD5Util.GetMD5_32(DEncrypt.Encrypt(MD5Util.GetMD5_32(strRandom).ToLower(), userLogOn.UserSecretkey).ToLower()).ToLower();
                userLogOn.ChangePasswordDate = DateTime.Now;
                bool bl = await userLogOnService.UpdateAsync(userLogOn, userLogOn.Id);
                if (bl)
                {
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = strRandom;
                    result.Success = true;
                }
                else
                {
                    result.ErrMsg = ErrCode.err43002;
                    result.ErrCode = "43002";
                }
            }catch(Exception ex)
            {
                Log4NetHelper.Error("重置密碼異常", ex);//錯誤記錄
                result.ErrMsg = ex.Message;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 修改密碼
        /// </summary>
        /// <param name="oldpassword">原密碼</param>
        /// <param name="password">新密碼</param>
        /// <param name="password2">重復新密碼</param>
        /// <returns></returns>

        [HttpPost("ModifyPassword")]
        [YuebonAuthorize("ModifyPassword")]
        public async Task<IActionResult> ModifyPassword(string oldpassword,string password, string password2)
        {
            CommonResult result = new CommonResult();
            try
            {
                if (string.IsNullOrEmpty(oldpassword))
                {
                    result.ErrMsg = "原密碼不能為空！";
                }
                else if (string.IsNullOrEmpty(password))
                {
                    result.ErrMsg = "密碼不能為空！";
                }
                else if (string.IsNullOrEmpty(password2))
                {
                    result.ErrMsg = "重復輸入密碼不能為空！";
                }
                else if (password == password2)
                {
                    var userSinginEntity = userLogOnService.GetByUserId(CurrentUser.UserId);
                    string inputPassword = MD5Util.GetMD5_32(DEncrypt.Encrypt(MD5Util.GetMD5_32(oldpassword).ToLower(), userSinginEntity.UserSecretkey).ToLower()).ToLower();
                    if (inputPassword != userSinginEntity.UserPassword)
                    {
                        result.ErrMsg = "原密碼錯誤！";
                    }
                    else
                    {
                        string where = string.Format("UserId='{0}'", CurrentUser.UserId);
                        UserLogOn userLogOn = userLogOnService.GetWhere(where);

                        userLogOn.UserSecretkey = MD5Util.GetMD5_16(GuidUtils.NewGuidFormatN()).ToLower();
                        userLogOn.UserPassword = MD5Util.GetMD5_32(DEncrypt.Encrypt(MD5Util.GetMD5_32(password).ToLower(), userLogOn.UserSecretkey).ToLower()).ToLower();
                        userLogOn.ChangePasswordDate = DateTime.Now;
                        bool bl = await userLogOnService.UpdateAsync(userLogOn, userLogOn.Id);
                        if (bl)
                        {
                            result.ErrCode = ErrCode.successCode;
                        }
                        else
                        {
                            result.ErrMsg = ErrCode.err43002;
                            result.ErrCode = "43002";
                        }
                    }
                }
                else
                {
                    result.ErrMsg = "兩次輸入的密碼不一樣";
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("重置密碼異常", ex);//錯誤記錄
                result.ErrMsg = ex.Message;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 保存用戶自定義的軟件主題
        /// </summary>
        /// <param name="info">主題配置信息</param>
        /// <returns></returns>
        [HttpPost("SaveUserTheme")]
        [YuebonAuthorize("SaveUserTheme")]
        public async Task<IActionResult> SaveUserTheme(UserThemeInputDto info)
        {
            CommonResult result = new CommonResult();
            try
            {
                result.Success= await userLogOnService.SaveUserTheme(info, CurrentUser.UserId);
                result.ErrCode = ErrCode.successCode;
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("保存用戶自定義的軟件主題異常", ex);//錯誤記錄
                result.ErrMsg = ex.Message;
            }
            return ToJsonContent(result);
        }

    }
}