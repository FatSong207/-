using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using Yuebon.WebApi.Areas.Security.Models;

namespace Yuebon.WebApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 用戶接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class UserController : AreaApiController<User, UserOutputDto, UserInputDto, IUserService,string>
    {
        private Yuebon.Security.IServices.IOrganizeService organizeService;
        private Yuebon.Security.IServices.IRoleService roleService;
        private Yuebon.Security.IServices.IUserLogOnService userLogOnService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_organizeService"></param>
        /// <param name="_roleService"></param>
        /// <param name="_userLogOnService"></param>
        public UserController(IUserService _iService, Yuebon.Security.IServices.IOrganizeService _organizeService, Yuebon.Security.IServices.IRoleService _roleService, Yuebon.Security.IServices.IUserLogOnService _userLogOnService) : base(_iService)
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
            Yuebon.Security.Models.UserLogOn userLogOn = new Yuebon.Security.Models.UserLogOn();
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


    }
}