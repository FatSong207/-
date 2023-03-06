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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using Yuebon.Commons.Extensions;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class MOBuildingController : AreaApiController<MOBuilding, MOBuildingOutputDto,MOBuildingInputDto,IMOBuildingService,string>
    {
        private readonly IBuildingService buildingService;
        private readonly IBuilding1Service building1Service;
        private readonly IBuildingAdvertisementService buildingAdvertisementService;
        private readonly IDbContextCore ybContext;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_buildingService"></param>
        /// <param name="_building1Service"></param>
        /// <param name="_buildingAdvertisementService"></param>
        /// <param name="_moBuildingService"></param>
        /// <param name="_ybContext"></param>
        public MOBuildingController(IMOBuildingService _iService, IBuildingService _buildingService, IBuilding1Service _building1Service, IBuildingAdvertisementService _buildingAdvertisementService, IMOBuildingService _moBuildingService, IDbContextCore _ybContext) : base(_iService)
        {
            buildingService = _buildingService;
            building1Service = _building1Service;
            buildingAdvertisementService = _buildingAdvertisementService;
            iService = _iService;
            ybContext = _ybContext;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(MOBuilding info)
        {
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
        }
        
        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(MOBuilding info)
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
        public override async Task<IActionResult> InsertAsync(MOBuildingInputDto input)
        {
            CommonResult result = new CommonResult();

            string CreatorUserId = CurrentUser?.UserId;
            DateTime CreatorTime = DateTime.Now;

            bool insertResult = false;

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                // 物件                
                string BAdd = Utils.memergeAddFix(
                    input.BAdd_1,
                    input.BAdd_1_1,
                    input.BAdd_1_2,
                    input.BAdd_2,
                    input.BAdd_2_1,
                    input.BAdd_2_2,
                    input.BAdd_2_3,
                    input.BAdd_2_4,
                    input.BAdd_3,
                    input.BAdd_3_1,
                    input.BAdd_3_2,
                    input.BAdd_4,
                    input.BAdd_5,
                    input.BAdd_6,
                    input.BAdd_7,
                    input.BAdd_8,
                    input.BAdd_9
                );

                // 判斷是否己有此建物
                if (!string.IsNullOrEmpty(BAdd)) {
                    MOBuildingOutputDto building_DB = await iService.GetByMOBuilding(input.MOID, BAdd);
                    string where = string.Format("BAdd='{0}'", BAdd);
                    Building BBuilding = buildingService.GetWhere(where);

                    if (building_DB != null || BBuilding != null) {
                        result.Success = false;
                        result.ErrMsg = "物件地址不能重復";
                        return ToJsonContent(result);
                    } else {
                        MOBuilding info = input.MapTo<MOBuilding>();
                        OnBeforeInsert(info);

                        info.BAdd = BAdd;

                        long moBCount = await iService.InsertAsync(info, conn, tran).ConfigureAwait(false);
                        insertResult = moBCount > 0;

                        // 同步新增建物
                        Building buildingInfo = info.MapTo<Building>();
                        buildingInfo.Id = GuidUtils.CreateNo();
                        buildingInfo.CreatorTime = DateTime.Now;
                        buildingInfo.CreatorUserId = CurrentUser.UserId;
                        buildingInfo.CreatorUserOrgId = CurrentUser.OrganizeId;
                        buildingInfo.CreatorUserDeptId = CurrentUser.DeptId;
                        buildingInfo.BState = "待招租";

                        buildingInfo.BelongLID = input.LSID;
                        Building1 b1info = new Building1();
                        b1info.Id = buildingInfo.Id;
                        b1info.BAdd = buildingInfo.BAdd;
                        b1info.CreatorUserId = buildingInfo.CreatorUserId;
                        b1info.CreatorTime = buildingInfo.CreatorTime;
                        b1info.CreatorUserDeptId = buildingInfo.CreatorUserDeptId;
                        b1info.CreatorUserOrgId = buildingInfo.CreatorUserOrgId;

                        List<BuildingBelonging> buildingBelongingEntitys = new List<BuildingBelonging>();

                        insertResult = await buildingService.InsertAsync(buildingInfo, buildingBelongingEntitys);
                        insertResult = await building1Service.InsertAsync(b1info);

                        // 建物廣告
                        BuildingAdvertisement baInfo = new BuildingAdvertisement();
                        baInfo.Id = buildingInfo.Id;
                        baInfo.BAdd = buildingInfo.BAdd;
                        baInfo.BAStatus = "廣告未上架";
                        baInfo.CreatorUserId = buildingInfo.CreatorUserId;
                        baInfo.CreatorTime = buildingInfo.CreatorTime;
                        //baInfo.CreatorUserDeptId = info.CreatorUserDeptId;
                        //baInfo.CreatorUserOrgId = info.CreatorUserOrgId;
                        baInfo.CreatorUserOrgId = buildingInfo.CreatorUserDeptId;
                        baInfo.CreatorUserDeptId = buildingInfo.CreatorUserOrgId;
                        insertResult = await buildingAdvertisementService.InsertAsync(baInfo);
                    }
                } else {
                    result.ErrMsg = "物件地址不能為空";
                    return ToJsonContent(result);
                }

                if (insertResult) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    eftran.Rollback();
                    result.ErrMsg = "新增物件失敗";
                    result.ErrCode = ErrCode.err43001;
                    result.Success = false;
                }

            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("新增物件失敗", ex);
                result.ErrMsg = "新增物件失敗";
                result.ErrCode = ErrCode.err43001;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步物理刪除
        /// </summary>
        /// <param name="moid"></param>
        /// <param name="badd"></param>
        [HttpDelete("DeleteAsync")]
        [YuebonAuthorize("Delete")]
        public async Task<IActionResult> DeleteAsync(string moid, string badd)
        {
            CommonResult result = new CommonResult();
            string where = string.Format("MOID = '%{0}%' AND BAdd = '{1}'", moid, badd);


            if (!string.IsNullOrEmpty(where)) {
                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();

                try {
                    bool bl = await iService.DeleteBatchWhereAsync(where, conn, tran).ConfigureAwait(false);

                    if (bl) {
                        eftran.Commit();

                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;
                    } else {
                        result.ErrCode = "43003";
                        result.ErrMsg = "刪除物件失敗";
                        result.Success = false;
                    }
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("刪除物件失敗", ex);
                    result.ErrMsg = "刪除物件失敗";
                    result.ErrCode = ErrCode.err43003;
                    result.Success = false;
                    // 觸發ExceptionHandlingAttribute.OnException
                    throw;
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步批次物理刪除
        /// </summary>
        /// <param name="input"></param>
        [HttpDelete("DeleteBatchAsync")]
        [YuebonAuthorize("Delete")]
        public override async Task<IActionResult> DeleteBatchAsync(Commons.Core.Dtos.DeletesInputDto input)
        {
            CommonResult result = new CommonResult();
            string where = "id in ('" + input.Ids.Join(",").Trim(',').Replace(",", "','") + "')";

            if (!string.IsNullOrEmpty(where)) {
                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();

                try {
                    bool bl = await iService.DeleteBatchWhereAsync(where, conn, tran).ConfigureAwait(false);

                    if (bl) {
                        eftran.Commit();

                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;
                    } else {
                        result.ErrCode = "43003";
                        result.ErrMsg = "刪除物件失敗";
                        result.Success = false;
                    }
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("刪除物件失敗", ex);
                    result.ErrMsg = "刪除物件失敗";
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