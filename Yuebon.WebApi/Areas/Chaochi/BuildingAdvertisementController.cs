using Microsoft.AspNetCore.Mvc;
using System;
using Yuebon.AspNetCore.Controllers;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.IServices;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Commons.IDbContext;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Yuebon.Commons.Log;
using Senparc.Weixin.MP.Entities;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 建物廣告管理接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class BuildingAdvertisementController : AreaApiController<BuildingAdvertisement, BuildingAdvertisementOutputDto,BuildingAdvertisementInputDto,IBuildingAdvertisementService,string>
    {
        private readonly IDbContextCore ybContext;
        private readonly IBuildingService buildingService;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_buildingService"></param>
        /// <param name="_ybContext"></param>
        public BuildingAdvertisementController(IBuildingAdvertisementService _iService, IBuildingService _buildingService, IDbContextCore _ybContext) : base(_iService)
        {
            iService = _iService;
            buildingService = _buildingService;
            ybContext = _ybContext;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(BuildingAdvertisement info)
        {            
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;

        }
        
        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(BuildingAdvertisement info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 根據主鍵Id獲取一個對象信息
        /// </summary>
        /// <param name="id">主鍵Id</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        [YuebonAuthorize("List")]
        //[NoPermissionRequired]
        public override async Task<CommonResult<BuildingAdvertisementOutputDto>> GetById(string id)
        {
            CommonResult<BuildingAdvertisementOutputDto> result = new CommonResult<BuildingAdvertisementOutputDto>();
            BuildingAdvertisementOutputDto info = await iService.GetOutDtoAsync(id);
            if (info != null) {
                Building bInfo = buildingService.Get(id);
                if(bInfo != null) {
                    info.BState = bInfo.BState;
                }
                result.ErrCode = ErrCode.successCode;
                result.ResData = info;
            } else {
                result.ErrMsg = ErrCode.err50001;
                result.ErrCode = "50001";
            }
            return result;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerAsync2")]
        [YuebonAuthorize("List")]
        public async Task<CommonResult<PageResult<BuildingAdvertisementOutputDto>>> FindWithPagerAsync(SearchBuildingAdvertisementModel search)
        {
            CommonResult<PageResult<BuildingAdvertisementOutputDto>> result = new CommonResult<PageResult<BuildingAdvertisementOutputDto>>();
            result.ResData = await iService.FindWithPagerAsync(search);
            result.ErrCode = ErrCode.successCode;
            return result;
        }

        /// <summary>
        /// 異步更新數據
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(BuildingAdvertisementInputDto input)
        {
            CommonResult result = new CommonResult();
            bool updateBStateResult = false;
            

            BuildingAdvertisement BA_DB = iService.Get(input.Id);
            if (!string.IsNullOrEmpty(input.BAURL)) {
                BA_DB.BAURL = input.BAURL;
                //BA_DB.BRStartDate = DateTime.Now;
                OnBeforeUpdate(BA_DB);
            }

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {                
                if (!string.IsNullOrEmpty(input.BState)) {

                    updateBStateResult = await buildingService.UpdateBState(CurrentUser.UserId, input.Id, input.BState, conn, tran).ConfigureAwait(false);
                    if ("招租中".Equals(input.BState)) {
                        BA_DB.BAStartDate = DateTime.Today;
                    } else if("待招租".Equals(input.BState)) {
                        BA_DB.BAStartDate = null;
                        BA_DB.BRStartDate = DateTime.Today;
                        BA_DB.BADays = null;
                    } else if("無委託".Equals(input.BState)) {
                        BA_DB.BAStartDate = null;
                        BA_DB.BRStartDate = null;
                        BA_DB.BADays = null;
                        BA_DB.BRDays = null;
                    }
                }

                bool updateResult = await iService.UpdateAsync(BA_DB, input.Id, conn, tran).ConfigureAwait(false);

                if (updateBStateResult && updateResult) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    result.ErrMsg = "更新建物廣告失敗";
                    result.ErrCode = ErrCode.err43002;
                    result.Success = false;
                }

            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("更新建物廣告失敗", ex);
                result.ErrMsg = "更新建物廣告失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 更新建物廣告狀態
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateBAStatus")]
        [YuebonAuthorize("BA")]
        public async Task<IActionResult> UpdateBAStatus(BuildingAdvertisementInputDto input)
        {
            CommonResult result = new CommonResult();

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                bool updateResult = await iService.UpdateBAStatus(CurrentUser.UserId, input.Id, input.BAStatus, conn, tran);

                if (updateResult) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    eftran.Rollback();
                    result.ErrMsg = "更新建物廣告狀態失敗";
                    result.ErrCode = ErrCode.err43002;
                    result.Success = false;
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("更新建物廣告狀態失敗", ex);
                result.ErrMsg = "更新建物廣告狀態失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 更新建物廣告網址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateBAURL")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> UpdateBAURL(BuildingAdvertisementInputDto input)
        {
            CommonResult result = new CommonResult();

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                bool updateResult = await iService.UpdateBAURL(CurrentUser.UserId, input.Id, input.BAURL, conn, tran);

                if (updateResult) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    eftran.Rollback();
                    result.ErrMsg = "更新建物廣告網址失敗";
                    result.ErrCode = ErrCode.err43002;
                    result.Success = false;
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("更新建物廣告網址失敗", ex);
                result.ErrMsg = "更新建物廣告網址失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }
    }
}