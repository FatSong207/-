using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using Yuebon.Tenants.Dtos;
using Yuebon.Tenants.IServices;
using Yuebon.Tenants.Models;

namespace Yuebon.SecurityApi.Areas.Security.Controllers
{
    /// <summary>
    /// 租戶接口
    /// </summary>
    [ApiController]
    [Route("api/Tenants/[controller]")]
    public class TenantController : AreaApiController<Tenant, TenantOutputDto,TenantInputDto,ITenantService,string>
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        public TenantController(ITenantService _iService) : base(_iService)
        {
            iService = _iService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Tenant info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.CompanyId = CurrentUser.OrganizeId;
            info.DeptId = CurrentUser.DeptId;
            info.DeleteMark = false;
        }
        
        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(Tenant info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Tenant info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }


        /// <summary>
        /// 異步更新數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(TenantInputDto tinfo)
        {
            CommonResult result = new CommonResult();
            if (!tinfo.TenantName.ToLower().IsAlphanumeric())
            {
                result.ErrMsg = "名稱只能是字母和數字";
                result.ErrCode = "43002";
                return ToJsonContent(result);
            }
            Tenant info = iService.Get(tinfo.Id);
            info.TenantName = tinfo.TenantName;
            info.CompanyName = tinfo.CompanyName;
            info.HostDomain = tinfo.HostDomain;
            info.DataSource = tinfo.DataSource;
            info.LinkMan = tinfo.LinkMan;
            info.Telphone = tinfo.Telphone;
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
    }
}