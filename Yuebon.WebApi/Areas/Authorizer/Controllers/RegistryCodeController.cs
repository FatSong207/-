using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Authorizer.Dtos;
using Yuebon.Authorizer.Models;
using Yuebon.Authorizer.IServices;
using Yuebon.Commons.Encrypt;
using Yuebon.AspNetCore.Mvc;

namespace Yuebon.WebApi.Areas.Authorizer.Controllers
{
    /// <summary>
    /// 軟件授權注冊和驗證接口
    /// </summary>
    [ApiController]
    [Route("api/Authorizer/[controller]")]
    public class RegistryCodeController : AreaApiController<RegistryCode, RegistryCodeOutputDto, RegistryCodeInputDto, IRegistryCodeService,string>
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        public RegistryCodeController(IRegistryCodeService _iService) : base(_iService)
        {
            iService = _iService;
            AuthorizeKey.ListKey = "RegistryCode/List";
            AuthorizeKey.InsertKey = "RegistryCode/Add";
            AuthorizeKey.UpdateKey = "RegistryCode/Edit";
            AuthorizeKey.UpdateEnableKey = "RegistryCode/Enable";
            AuthorizeKey.DeleteKey = "RegistryCode/Delete";
            AuthorizeKey.DeleteSoftKey = "RegistryCode/DeleteSoft";
            AuthorizeKey.ViewKey = "RegistryCode/View";
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(RegistryCode info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.DeleteMark = false;
        }
        
        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(RegistryCode info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(RegistryCode info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }
        /// <summary>
        /// 根據機器特征碼獲取注冊序列號
        /// </summary>
        /// <param name="machineCode">機器特征碼</param>
        [HttpPost("GetRegCode")]
        [YuebonAuthorize("Edit")]
        public IActionResult GetRegCode(string machineCode)
        {
            CommonResult result = new CommonResult();
            try
            {
                //RSASecurityHelper.RSAEncrypSignature(machineCode,);
            }
            catch (Exception ex)
            {
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("獲取注冊序列號異常", ex);
            }
            return ToJsonContent(result);
        }
    }
}