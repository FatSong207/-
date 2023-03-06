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
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.IServices;
using Yuebon.Commons.IDbContext;
using Yuebon.AspNetCore.Mvc;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Yuebon.Commons.Extensions;
using Yuebon.Chaochi.Core.Dtos;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 業務拜訪記錄接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class VisitingRecordController : AreaApiController<VisitingRecord, VisitingRecordOutputDto,VisitingRecordInputDto,IVisitingRecordService,string>
    {
        private readonly IPotentialCustomersService pcService;
        private readonly IDbContextCore ybContext;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_pcService"></param>
        /// <param name="_ybContext"></param>
        public VisitingRecordController(IVisitingRecordService _iService, IPotentialCustomersService _pcService, IDbContextCore _ybContext) : base(_iService)
        {
            iService = _iService;
            pcService = _pcService;
            ybContext = _ybContext;
        }

        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(VisitingRecord info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.CreatorUserOrgId = CurrentUser.OrganizeId;
            info.CreatorUserDeptId = CurrentUser.DeptId;
            info.DeleteMark = false;
        }
        
        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(VisitingRecord info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 異步新增數據
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(VisitingRecordInputDto input)
        {
            CommonResult result = new CommonResult();

            VisitingRecord info = input.MapTo<VisitingRecord>();
            OnBeforeInsert(info);
            
            string where = "PID = '" + info.PID + "'";
            int vrCount = await iService.GetCountByWhereAsync(where);

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                long vsCount = await iService.InsertAsync(info, conn, tran).ConfigureAwait(false);

                // 更新回報狀態
                bool statusUpdateResult = await pcService.UpdatePCStatus(CurrentUser.UserId, info.PID, info.Status, conn, tran);

                if (statusUpdateResult) {
                    // 是否結案
                    // 當潛在客 = 承租人，且「當前狀態」若為「已填申請書、無意願、待簽約、已重複開發」，視之為已結案，要更新回列表頁
                    // 當潛在客 = 出租人，且「當前狀態」若為「已簽委託 / 申請書、無意願、已出租、已重複開發」，視之為已結案，要更新回列表頁
                    string[] lClosedStatus = new string[] { "已簽委託/申請書", "無意願", "已出租", "已重複開發" };
                    string[] rClosedStatus = new string[] { "已填申請書", "無意願", "待簽約", "已重複開發" };
                    string isClosed = string.Empty;
                    string identity = info.PID.Substring(0, 1);

                    switch (identity) {
                        case "L":
                            isClosed = (lClosedStatus.Contains(info.Status)) ? "是" : "否";
                            break;
                        case "R":
                            isClosed = (rClosedStatus.Contains(info.Status)) ? "是" : "否";
                            break;
                    }

                    if (!string.IsNullOrEmpty(isClosed)) {
                        bool updateResult = await pcService.UpdatePCIsClosed(CurrentUser.UserId, info.PID, isClosed, conn, tran);
                    }
                }

                // 更新回報次數
                bool RBCountUpdateResult = false;
                if (vrCount >= 0) {
                    vrCount += 1;
                    RBCountUpdateResult = await pcService.UpdatePCReportBackCounts(CurrentUser.UserId, info.PID, vrCount, conn, tran);
                }

                if (vsCount > 0 && statusUpdateResult && RBCountUpdateResult) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    result.ErrMsg = "新增拜訪記錄失敗";
                    result.ErrCode = ErrCode.err43001;
                    result.Success = false;
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("新增拜訪記錄失敗", ex);
                result.ErrMsg = "新增拜訪記錄失敗";
                result.ErrCode = ErrCode.err43001;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步更新數據
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(VisitingRecordInputDto input)
        {
            CommonResult result = new CommonResult();

            VisitingRecord VR_DB = iService.Get(input.Id);
            VisitingRecord info = input.MapTo(VR_DB);
            OnBeforeUpdate(info);

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                bool updateResult = await iService.UpdateAsync(info, input.Id, conn, tran);

                //當潛在客 = 承租人，且「當前狀態」若為「已填申請書、無意願、待簽約、已重複開發」，視之為已結案，要更新回列表頁
                //當潛在客 = 出租人，且「當前狀態」若為「已簽委託 / 申請書、無意願、已出租、已重複開發」，視之為已結案，要更新回列表頁
                string[] lClosedStatus = new string[] { "已簽委託/申請書", "無意願", "已出租", "已重複開發"};
                string[] rClosedStatus = new string[] { "已填申請書", "無意願", "待簽約", "已重複開發" };
                string isClosed = string.Empty;
                string identity = info.PID.Substring(0, 1);

                switch (identity) {
                    case "L":
                        isClosed = (lClosedStatus.Contains(info.Status)) ? "是" : "否";
                        break;                        
                    case "R":
                        isClosed = (rClosedStatus.Contains(info.Status)) ? "是" : "否";
                        break;
                }

                if(!string.IsNullOrEmpty(isClosed)) {
                    updateResult = await pcService.UpdatePCIsClosed(CurrentUser.UserId, info.PID, isClosed, conn, tran);
                }

                // 最近訪談記錄的當前狀態
                VisitingRecordOutputDto vrODto = await iService.GetLatestByPID(info.PID, conn, tran);
                if (vrODto != null) {
                    // 更新回報狀態
                    updateResult = await pcService.UpdatePCStatus(CurrentUser.UserId, info.PID, vrODto.Status, conn, tran);
                }

                if (updateResult) {
                    eftran.Commit();

                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    result.ErrMsg = "更新拜訪記錄失敗";
                    result.ErrCode = ErrCode.err43002;
                    result.Success = false;
                }

            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("更新拜訪記錄失敗", ex);
                result.ErrMsg = "更新拜訪記錄失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步批次物理刪除
        /// </summary>
        /// <param name="input"></param>
        [HttpDelete("DeleteBatchAsync")]
        [YuebonAuthorize("Delete")]
        public async Task<IActionResult> DeleteBatchAsync(DeleteInputDto input)
        {
            CommonResult result = new CommonResult();
            
            string where = "id in ('" + input.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            //string where = "PID in ('" + input.Ids.Join(",").Trim(',').Replace(",", "','") + "')";

            if (!string.IsNullOrEmpty(where)) {
                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();

                try {
                    bool vrResult = await iService.DeleteBatchWhereAsync(where, conn, tran).ConfigureAwait(false);

                    string where2 = "PID = '" + input.Id + "'";
                    // 回報次數
                    int vrCount = await iService.GetCountByWhereAsync(where2);
                    // 最近訪談記錄的當前狀態
                    VisitingRecordOutputDto vrODto = await iService.GetLatestByPID(input.Id);

                    // 如果有刪除的事實，刪去的序號為最新，則刪除成功後，要將剩餘序號最新的狀態，一併更新回列表頁
                    bool statusUpdateResult = false;
                    if (vrODto != null) {
                        // 更新回報狀態
                        statusUpdateResult = await pcService.UpdatePCStatus(CurrentUser.UserId, vrODto.PID, vrODto.Status, conn, tran);
                    }

                    bool RBCountUpdateResult = false;
                    if (vrCount > 0) {
                        RBCountUpdateResult = await pcService.UpdatePCReportBackCounts(CurrentUser.UserId, vrODto.PID, vrCount, conn, tran);
                    }

                    if (vrResult) {
                        eftran.Commit();

                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;
                    } else {
                        result.ErrCode = "43003";
                        result.ErrMsg = "刪除拜訪記錄失敗";
                        result.Success = false;
                    }
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("刪除拜訪記錄失敗", ex);
                    result.ErrMsg = "刪除拜訪記錄失敗";
                    result.ErrCode = ErrCode.err43003;
                    result.Success = false;
                    // 觸發ExceptionHandlingAttribute.OnException
                    throw;
                }
            }
            return ToJsonContent(result);
        }
    }
}