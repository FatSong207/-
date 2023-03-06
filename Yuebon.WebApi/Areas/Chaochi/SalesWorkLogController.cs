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
using Yuebon.AspNetCore.Mvc;
using System.Data;
using Yuebon.Commons.IDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Yuebon.Chaochi.Core.Dtos;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 業務工作日誌接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class SalesWorkLogController : AreaApiController<SalesWorkLog, SalesWorkLogOutputDto, SalesWorkLogInputDto, ISalesWorkLogService, string>
    {
        private readonly IDbContextCore _ybContext;
        private readonly ISalesWorkLogDetailsService _salesWorkLogDetailsService;
        private readonly IOrganizeService _organizeService;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="ybContext"></param>
        /// <param name="salesWorkLogDetailsService"></param>
        /// <param name="organizeService"></param>
        public SalesWorkLogController(ISalesWorkLogService _iService, IDbContextCore ybContext, ISalesWorkLogDetailsService salesWorkLogDetailsService, Security.IServices.IOrganizeService organizeService) : base(_iService)
        {
            iService = _iService;
            _ybContext = ybContext;
            _salesWorkLogDetailsService = salesWorkLogDetailsService;
            _organizeService = organizeService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(SalesWorkLog info)
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
        protected override void OnBeforeUpdate(SalesWorkLog info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        public override async Task<IActionResult> InsertAsync(SalesWorkLogInputDto tinfo)
        {
            var result = new CommonResult();
            var info = tinfo.MapTo<SalesWorkLog>();
            var details = tinfo.SalesWorkLogDetails;
            var deals = tinfo.Deals;

            OnBeforeInsert(info);

            //加工details
            foreach (var item in details) {
                item.SalesWorkLogId = info.Id;
                item.CreatorTime = DateTime.Now;
                item.CreatorUserId = CurrentUser.UserId;
                item.LastModifyUserId = CurrentUser.UserId;
                item.LastModifyTime = DateTime.Now;
            }

            //加工deals
            foreach (var deal in deals) {
                switch (deal.Category) {
                    case "SH":
                        info.SH_Continue = deal.Continue;
                        info.SH_Match = deal.Match;
                        info.SH_New = deal.New;
                        info.SH_Nothing = deal.Nothing;
                        info.SH_Performance = deal.Performance;
                        info.SH_Transfer = deal.Transfer;
                        break;
                    case "NH":
                        info.NH_Continue = deal.Continue;
                        info.NH_Match = deal.Match;
                        info.NH_New = deal.New;
                        info.NH_Nothing = deal.Nothing;
                        info.NH_Performance = deal.Performance;
                        info.NH_Transfer = deal.Transfer;
                        break;
                    case "ST":
                        info.ST_Continue = deal.Continue;
                        info.ST_Match = deal.Match;
                        info.ST_New = deal.New;
                        info.ST_Nothing = deal.Nothing;
                        info.ST_Performance = deal.Performance;
                        info.ST_Transfer = deal.Transfer;
                        break;
                    default:
                        break;
                }
            }

            var userdeptRecp = CurrentUser.ManagerId;
            info.SalesName = CurrentUser.Name;
            info.SalesAccount = CurrentUser.Account;
            info.LogDate = DateTime.Now;
            info.AuditSupervisor = userdeptRecp;
            info.BelongDept = CurrentUser.DeptId;
            info.States ??= "first";
            long ln = await iService.InsertAsync(info).ConfigureAwait(false);
            _salesWorkLogDetailsService.InsertRange(details);
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
        /// <param name="inInfo"></param>
        /// <returns></returns>
        [HttpPost("UpdateAsync")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> UpdateAsync2(SalesWorkLogInputDto inInfo)
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
                //加工deals
                foreach (var deal in inInfo.Deals) {
                    switch (deal.Category) {
                        case "SH":
                            info.SH_Continue = deal.Continue;
                            info.SH_Match = deal.Match;
                            info.SH_New = deal.New;
                            info.SH_Nothing = deal.Nothing;
                            info.SH_Performance = deal.Performance;
                            info.SH_Transfer = deal.Transfer;
                            break;
                        case "NH":
                            info.NH_Continue = deal.Continue;
                            info.NH_Match = deal.Match;
                            info.NH_New = deal.New;
                            info.NH_Nothing = deal.Nothing;
                            info.NH_Performance = deal.Performance;
                            info.NH_Transfer = deal.Transfer;
                            break;
                        case "ST":
                            info.ST_Continue = deal.Continue;
                            info.ST_Match = deal.Match;
                            info.ST_New = deal.New;
                            info.ST_Nothing = deal.Nothing;
                            info.ST_Performance = deal.Performance;
                            info.ST_Transfer = deal.Transfer;
                            break;
                        default:
                            break;
                    }
                }
                var isSuccess = await iService.UpdateAsync(info, inInfo.Id, conn, tran);
                await _salesWorkLogDetailsService.DeleteConnWhereAsync($"SalesWorkLogId = '{inInfo.Id}'", conn, tran);

                foreach (var item in inInfo.SalesWorkLogDetails) {
                    item.SalesWorkLogId = info.Id;
                    item.CreatorTime = DateTime.Now;
                    item.CreatorUserId = CurrentUser.UserId;
                    item.LastModifyUserId = CurrentUser.UserId;
                    item.LastModifyTime = DateTime.Now;
                }

                _salesWorkLogDetailsService.InsertRange(inInfo.SalesWorkLogDetails);
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
                Log4NetHelper.Error("更新失敗", ex);
                result.ErrMsg = "更新失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
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
        public async Task<IActionResult> FindWithPagerSearchAsync(SearchSalesWorkLogModel search)
        {
            CommonResult<PageResult<SalesWorkLogOutputDto>> result = new CommonResult<PageResult<SalesWorkLogOutputDto>>();
            result.ResData = await iService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<CommonResult<SalesWorkLogOutputDto>> GetById(string id)
        {
            var result = new CommonResult<SalesWorkLogOutputDto>();
            var info = await iService.GetOutDtoAsync(id);
            info.SalesWorkLogDetails = _salesWorkLogDetailsService.GetListWhere($"SalesWorkLogId = '{id}'").ToList();
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
        /// <returns></returns>
        [HttpGet("GetLastDeals")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetLastDeals()
        {
            var result = new CommonResult();
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            var dataList = await iService.GetListWhereAsync($"CreatorUserId = '{CurrentUser.UserId}'");
            if (dataList.Count() is not 0) {
                var lastData = dataList.OrderByDescending(x => x.CreatorTime).FirstOrDefault();
                result.ResData = lastData;
            }
            return ToJsonContent(result);

        }
    }
}