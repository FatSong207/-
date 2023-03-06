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
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.IServices;
using Yuebon.Security.IServices;
using System.Linq;
using System.Data;
using Yuebon.Commons.IDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Yuebon.AspNetCore.Mvc;
using Yuebon.Commons.Dtos;
using Yuebon.AspNetCore.Mvc.Filter;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 行政工作日誌接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class AdministrativeWorkLogController : AreaApiController<AdministrativeWorkLog, AdministrativeWorkLogOutputDto, AdministrativeWorkLogInputDto, IAdministrativeWorkLogService, string>
    {
        private readonly IDbContextCore _ybContext;
        private readonly IOrganizeService _organizeService;
        private readonly IAdministrativeWorkLogDetailsService _administrativeWorkLogDetailsService;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="ybContext"></param>
        /// <param name="organizeService"></param>
        /// <param name="administrativeWorkLogDetailsService"></param>
        public AdministrativeWorkLogController(IAdministrativeWorkLogService _iService, IDbContextCore ybContext, Security.IServices.IOrganizeService organizeService, IAdministrativeWorkLogDetailsService administrativeWorkLogDetailsService) : base(_iService)
        {
            iService = _iService;
            _ybContext = ybContext;
            _organizeService = organizeService;
            _administrativeWorkLogDetailsService = administrativeWorkLogDetailsService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(AdministrativeWorkLog info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.CreatorUserDeptId = CurrentUser.DeptId;
            //info.DeleteMark = false;
            //if (info.SortCode == null)
            //{
            //    info.SortCode = 99;
            //}
        }

        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(AdministrativeWorkLog info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        public override async Task<CommonResult<AdministrativeWorkLogOutputDto>> GetById(string id)
        {
            var result = new CommonResult<AdministrativeWorkLogOutputDto>();
            var info = await iService.GetOutDtoAsync(id);
            info.AdministrativeWorkLogDetails = _administrativeWorkLogDetailsService.GetListWhere($"AdministrativeWorkLogId = '{id}'").ToList();
            if (info != null) {
                result.ErrCode = ErrCode.successCode;
                result.ResData = info;
            } else {
                result.ErrMsg = ErrCode.err50001;
                result.ErrCode = "50001";
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        public override async Task<IActionResult> InsertAsync(AdministrativeWorkLogInputDto tinfo)
        {
            var result = new CommonResult();
            var info = tinfo.MapTo<AdministrativeWorkLog>();
            var details = tinfo.AdministrativeWorkLogDetails;

            OnBeforeInsert(info);

            foreach (var item in details) {
                item.AdministrativeWorkLogId = info.Id;
                item.CreatorTime = DateTime.Now;
                item.CreatorUserId = CurrentUser.UserId;
                item.LastModifyUserId = CurrentUser.UserId;
                item.LastModifyTime = DateTime.Now;
            }

            //string auditSupervisor;

            //var userdeptRecp = _organizeService.Get(CurrentUser.DeptId).ManagerId;
            //if (CurrentUser.UserId == userdeptRecp) {
            //    var userFatherOrgId = _organizeService.Get(CurrentUser.DeptId).ParentId;
            //    auditSupervisor = _organizeService.Get(userFatherOrgId).ManagerId;
            //} else {
            //    auditSupervisor = userdeptRecp;
            //}


            info.UserName = CurrentUser.Name;
            info.UserAccount = CurrentUser.Account;
            info.LogDate = DateTime.Now.ToString();
            info.AuditSupervisor = CurrentUser.ManagerId;
            info.BelongDept = CurrentUser.DeptId;
            info.States = info.States ?? "first";
            long ln = await iService.InsertAsync(info).ConfigureAwait(false);
            _administrativeWorkLogDetailsService.InsertRange(details);
            if (ln > 0) {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } else {
                result.ErrMsg = ErrCode.err43001;
                result.ErrCode = "43001";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public override async Task<CommonResult<PageResult<AdministrativeWorkLogOutputDto>>> FindWithPagerAsync(SearchInputDto<AdministrativeWorkLog> search)
        {
            CommonResult<PageResult<AdministrativeWorkLogOutputDto>> result = new CommonResult<PageResult<AdministrativeWorkLogOutputDto>>();
            result.ResData = await iService.FindWithPagerAsync(search);
            result.ErrCode = ErrCode.successCode;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        [HttpPost("UpdateAsync")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> UpdateAsync2(AdministrativeWorkLogInputDto_update inInfo)
        {
            var result = new CommonResult();

            var adWL = await iService.GetAsync(inInfo.Id);
            var info = inInfo.MapTo(adWL);

            using IDbConnection conn = _ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = _ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                OnBeforeUpdate(info);
                if (info.States == "approval") {
                    info.AuditTime = DateTime.Now;
                }
                var isSuccess = await iService.UpdateAsync(info, inInfo.Id, conn, tran);
                await _administrativeWorkLogDetailsService.DeleteConnWhereAsync($"AdministrativeWorkLogId = '{inInfo.Id}'", conn, tran);

                foreach (var item in inInfo.AdministrativeWorkLogDetails) {
                    item.AdministrativeWorkLogId = info.Id;
                    item.CreatorTime = DateTime.Now;
                    item.CreatorUserId = CurrentUser.UserId;
                    item.LastModifyUserId = CurrentUser.UserId;
                    item.LastModifyTime = DateTime.Now;
                }

                _administrativeWorkLogDetailsService.InsertRange(inInfo.AdministrativeWorkLogDetails);
                if (isSuccess) {
                    tran.Commit();
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.Success = false;
                    result.ErrMsg = ErrCode.err43002;
                    result.ErrCode = "43002";
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("更新承租人失敗", ex);
                result.ErrMsg = "更新承租人失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return ToJsonContent(result);
        }
    }
}