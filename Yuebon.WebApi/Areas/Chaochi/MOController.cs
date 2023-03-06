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
using Yuebon.AspNetCore.Mvc;
using Yuebon.Commons.IDbContext;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using Yuebon.Commons.Extensions;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Pages;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 分租物件管理接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class MOController : AreaApiController<MO, MOOutputDto,MOInputDto,IMOService,string>
    {
        private readonly ICustomerLCService lcService;
        private readonly ICustomerLNService lnService;
        private readonly ILandLordBelongingService landLordBelongingService;
        private readonly IBuildingService buildingService;
        private readonly IBuilding1Service building1Service;
        private readonly IBuildingAdvertisementService buildingAdvertisementService;
        private readonly IMOBuildingService moBuildingService;
        private readonly IDbContextCore ybContext;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_lcService"></param>
        /// <param name="_lnService"></param>
        /// <param name="_landLordBelongingService"></param>
        /// <param name="_moBuildingService"></param>
        /// <param name="_buildingService"></param>
        /// <param name="_building1Service"></param>
        /// <param name="_buildingAdvertisementService"></param>
        /// <param name="_ybContext"></param>
        public MOController(
            IMOService _iService,
            ICustomerLCService _lcService,
            ICustomerLNService _lnService,
            ILandLordBelongingService _landLordBelongingService,
            IBuildingService _buildingService,
            IBuilding1Service _building1Service,
            IBuildingAdvertisementService _buildingAdvertisementService,
            IMOBuildingService _moBuildingService,
            IDbContextCore _ybContext) : base(_iService)
        {
            lcService = _lcService;
            lnService = _lnService;
            landLordBelongingService = _landLordBelongingService;
            iService = _iService;
            buildingService = _buildingService;
            building1Service = _building1Service;
            buildingAdvertisementService = _buildingAdvertisementService;
            moBuildingService = _moBuildingService;
            ybContext = _ybContext;
        }

        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(MO info)
        {
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.CreatorUserOrgId = CurrentUser.OrganizeId;
            info.CreatorUserDeptId = CurrentUser.DeptId;
        }
        
        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(MO info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 異步分頁查詢
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerSearchAsync")]
        [YuebonAuthorize("List")]
        [LogNotMethod]
        public async Task<IActionResult> FindWithPagerSearchAsync(SearchMOModel search)
        {
            CommonResult<PageResult<MOOutputDto>> result = new CommonResult<PageResult<MOOutputDto>>();
            result.ResData = await iService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步新增數據
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(MOInputDto input)
        {
            CommonResult result = new CommonResult();

            string CreatorUserId = CurrentUser?.UserId;
            DateTime CreatorTime = DateTime.Now;

            MO info = input.MapTo<MO>();

            OnBeforeInsert(info);

            var lnInfos = await lnService.GetAllAsync();
            var lnIDList = lnInfos.Select(o => o.LNID).ToArray();
            var lcInfos = await lcService.GetAllAsync();
            var lcIDList = lcInfos.Select(o => o.LCID).ToArray();

            var building_DB = await moBuildingService.GetAllAsync();
            var moBAddList  = building_DB.Select(o => o.BAdd).ToArray();
            var bBuilding = await buildingService.GetAllAsync();
            var bBAddList = bBuilding.Select(o => o.BAdd).ToArray();

            bool insertResult = false;

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();            
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                long moCount = await iService.InsertAsync(info, conn, tran).ConfigureAwait(false);
                insertResult = moCount > 0;

                // 房東
                if (info.LSID.Length == 10) {
                    string LNID = input.LSID;
                    if (!lnIDList.Contains(LNID)) {
                        CustomerLN lnInfo = new CustomerLN();
                        lnInfo.LNID = LNID;
                        lnInfo.LNName = input.LSName;

                        // 身分證字號
                        var idSplit = LNID.ToCharArray();
                        lnInfo.LNID_1_1 = idSplit[0].ToString();
                        lnInfo.LNID_1_2 = idSplit[1].ToString();
                        lnInfo.LNID_1_3 = idSplit[2].ToString();
                        lnInfo.LNID_1_4 = idSplit[3].ToString();
                        lnInfo.LNID_1_5 = idSplit[4].ToString();
                        lnInfo.LNID_1_6 = idSplit[5].ToString();
                        lnInfo.LNID_1_7 = idSplit[6].ToString();
                        lnInfo.LNID_1_8 = idSplit[7].ToString();
                        lnInfo.LNID_1_9 = idSplit[8].ToString();
                        lnInfo.LNID_1_10 = idSplit[9].ToString();

                        lnInfo.CreatorTime = CreatorTime;
                        lnInfo.CreatorUserId = CreatorUserId;
                        lnInfo.CreatorUserOrgId = CurrentUser.OrganizeId;
                        lnInfo.CreatorUserDeptId = CurrentUser.DeptId;
                        landLordBelongingService.Insert(new LandLordBelonging
                        {
                            LId = LNID,
                            SalesId = CurrentUser.UserId,
                        });

                        long ln = await lnService.InsertAsync(lnInfo, conn, tran).ConfigureAwait(false);
                        insertResult = ln > 0;
                    }
                } else if(info.LSID.Length == 8) {
                    string LCID = input.LSID;
                    if (!lcIDList.Contains(LCID)) {
                        CustomerLC lcInfo = new CustomerLC();
                        lcInfo.LCID = LCID;
                        lcInfo.LCName = input.LSName;

                        // 統一編號
                        var idSplit = LCID.ToCharArray();
                        lcInfo.LCID_1_1 = idSplit[0].ToString();
                        lcInfo.LCID_1_2 = idSplit[1].ToString();
                        lcInfo.LCID_1_3 = idSplit[2].ToString();
                        lcInfo.LCID_1_4 = idSplit[3].ToString();
                        lcInfo.LCID_1_5 = idSplit[4].ToString();
                        lcInfo.LCID_1_6 = idSplit[5].ToString();
                        lcInfo.LCID_1_7 = idSplit[6].ToString();
                        lcInfo.LCID_1_8 = idSplit[7].ToString();

                        lcInfo.CreatorTime = CreatorTime;
                        lcInfo.CreatorUserId = CreatorUserId;
                        lcInfo.CreatorUserOrgId = CurrentUser.OrganizeId;
                        lcInfo.CreatorUserDeptId = CurrentUser.DeptId;
                        landLordBelongingService.Insert(new LandLordBelonging
                        {
                            LId = LCID,
                            SalesId = CurrentUser.UserId,
                        });

                        long ln = await lcService.InsertAsync(lcInfo, conn, tran).ConfigureAwait(false);
                        insertResult = ln > 0;
                    }
                }

                // 物件
                List<MOBuildingInputDto> buildingList = input.BuildingList;
                if (buildingList.Count > 0) {
                    MOBuilding moBuildingInfo = new MOBuilding();
                    foreach (MOBuildingInputDto building in buildingList) {
                        //string BAdd = Utils.memergeAddSearch(
                        //    building.BAdd_1,
                        //    building.BAdd_1_1,
                        //    building.BAdd_1_2,
                        //    building.BAdd_2,
                        //    building.BAdd_2_1,
                        //    building.BAdd_2_2,
                        //    building.BAdd_2_3,
                        //    building.BAdd_2_4,
                        //    building.BAdd_3,
                        //    building.BAdd_3_1,
                        //    building.BAdd_3_2,
                        //    building.BAdd_4
                        //);

                        // 判斷是否己有此建物
                        if (!string.IsNullOrEmpty(building.BAdd)) {
                            //MOBuildingOutputDto building_DB = await moBuildingService.GetByMOBuilding(input.MOID, building.BAdd, null, tran).ConfigureAwait(false);
                            //string where = string.Format("BAdd='{0}'", building.BAdd);
                            //Building BBuilding = await buildingService.GetWhereAsync(where, null, tran).ConfigureAwait(false);

                            if (moBAddList.Contains(building.BAdd) || bBAddList.Contains(building.BAdd)) {
                                result.Success = false;
                                result.ErrMsg = "物件地址不能重復";
                                return ToJsonContent(result);
                            } else {
                                moBuildingInfo = new MOBuilding();
                                moBuildingInfo.MOID = input.MOID;
                                moBuildingInfo.BAdd = building.BAdd;
                                moBuildingInfo.BAdd_1 = building.BAdd_1;
                                moBuildingInfo.BAdd_1_1 = building.BAdd_1_1;
                                moBuildingInfo.BAdd_1_2 = building.BAdd_1_2;
                                moBuildingInfo.BAdd_2 = building.BAdd_2;
                                moBuildingInfo.BAdd_2_1 = building.BAdd_2_1;
                                moBuildingInfo.BAdd_2_2 = building.BAdd_2_2;
                                moBuildingInfo.BAdd_2_3 = building.BAdd_2_3;
                                moBuildingInfo.BAdd_2_4 = building.BAdd_2_4;
                                moBuildingInfo.BAdd_3 = building.BAdd_3;
                                moBuildingInfo.BAdd_3_1 = building.BAdd_3_1;
                                moBuildingInfo.BAdd_3_2 = building.BAdd_3_2;
                                moBuildingInfo.BAdd_4 = building.BAdd_4;
                                moBuildingInfo.BAdd_5 = building.BAdd_5;
                                moBuildingInfo.BAdd_6 = building.BAdd_6;
                                moBuildingInfo.BAdd_7 = building.BAdd_7;
                                moBuildingInfo.BAdd_8 = building.BAdd_8;
                                moBuildingInfo.BAdd_9 = building.BAdd_9;
                                moBuildingInfo.BPropoty = building.BPropoty;
                                moBuildingInfo.CreatorTime = CreatorTime;
                                moBuildingInfo.CreatorUserId = CreatorUserId;

                                long moBCount = await moBuildingService.InsertAsync(moBuildingInfo, conn, tran).ConfigureAwait(false);
                                insertResult = moBCount > 0;

                                // 同步新增建物
                                var buildingInfo = moBuildingInfo.MapTo<Building>();
                                buildingInfo.Id = GuidUtils.CreateNo();
                                buildingInfo.CreatorTime = DateTime.Now;
                                buildingInfo.CreatorUserId = CurrentUser.UserId;
                                buildingInfo.CreatorUserOrgId = CurrentUser.OrganizeId;
                                buildingInfo.CreatorUserDeptId = CurrentUser.DeptId;
                                buildingInfo.BState = "待招租";
                                buildingInfo.MOID = moBuildingInfo.MOID;

                                buildingInfo.BelongLID = input.LSID;
                                var b1info = new Building1();
                                b1info.Id = buildingInfo.Id;
                                b1info.BAdd = buildingInfo.BAdd;
                                b1info.CreatorUserId = buildingInfo.CreatorUserId;
                                b1info.CreatorTime = buildingInfo.CreatorTime;
                                b1info.CreatorUserDeptId = buildingInfo.CreatorUserDeptId;
                                b1info.CreatorUserOrgId = buildingInfo.CreatorUserOrgId;

                                List<BuildingBelonging> buildingBelongingEntitys = new List<BuildingBelonging>();

                                insertResult = await buildingService.InsertAsync(buildingInfo, buildingBelongingEntitys, tran).ConfigureAwait(false);
                                insertResult = await building1Service.InsertAsync(b1info, tran).ConfigureAwait(false);

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
                    }
                }

                if(insertResult) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    eftran.Rollback();
                    result.ErrMsg = "新增分租物件單失敗";
                    result.ErrCode = ErrCode.err43001;
                    result.Success = false;
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("新增分租物件單失敗", ex);
                result.ErrMsg = "新增分租物件單失敗";
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
        public override async Task<IActionResult> UpdateAsync(MOInputDto input)
        {
            CommonResult result = new CommonResult();

            string CreatorUserId = CurrentUser?.UserId;
            DateTime CreatorTime = DateTime.Now;

            MO MO_DB = iService.Get(input.Id);
            MO info = input.MapTo(MO_DB);
            OnBeforeUpdate(info);

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                // 更新
                bool updateResult = false;

                var lnInfos = await lnService.GetAllAsync(conn, tran);
                var lnIDList = lnInfos.Select(o => o.LNID).ToArray();
                var lcInfos = await lcService.GetAllAsync(conn, tran);
                var lcIDList = lcInfos.Select(o => o.LCID).ToArray();

                var building_DB = await moBuildingService.GetAllAsync(conn, tran);
                var moBAddList = building_DB.Select(o => o.BAdd).ToArray();
                var bBuilding = await buildingService.GetAllAsync(conn, tran);
                var bBAddList = bBuilding.Select(o => o.BAdd).ToArray();

                // 房東
                if (info.LSID.Length == 10) {
                    string LNID = input.LSID;
                    if (!lnIDList.Contains(LNID)) {
                        CustomerLN lnInfo = new CustomerLN();
                        lnInfo.LNID = LNID;
                        lnInfo.LNName = input.LSName;

                        // 身分證字號
                        var idSplit = LNID.ToCharArray();
                        lnInfo.LNID_1_1 = idSplit[0].ToString();
                        lnInfo.LNID_1_2 = idSplit[1].ToString();
                        lnInfo.LNID_1_3 = idSplit[2].ToString();
                        lnInfo.LNID_1_4 = idSplit[3].ToString();
                        lnInfo.LNID_1_5 = idSplit[4].ToString();
                        lnInfo.LNID_1_6 = idSplit[5].ToString();
                        lnInfo.LNID_1_7 = idSplit[6].ToString();
                        lnInfo.LNID_1_8 = idSplit[7].ToString();
                        lnInfo.LNID_1_9 = idSplit[8].ToString();
                        lnInfo.LNID_1_10 = idSplit[9].ToString();

                        lnInfo.CreatorTime = CreatorTime;
                        lnInfo.CreatorUserId = CreatorUserId;
                        lnInfo.CreatorUserOrgId = CurrentUser.OrganizeId;
                        lnInfo.CreatorUserDeptId = CurrentUser.DeptId;
                        landLordBelongingService.Insert(new LandLordBelonging
                        {
                            LId = LNID,
                            SalesId = CurrentUser.UserId,
                        });

                        long ln = await lnService.InsertAsync(lnInfo, conn, tran).ConfigureAwait(false);
                        updateResult = ln > 0;
                    }
                } else if (info.LSID.Length == 8) {
                    string LCID = input.LSID;
                    if (!lcIDList.Contains(LCID)) {
                        CustomerLC lcInfo = new CustomerLC();
                        lcInfo.LCID = LCID;
                        lcInfo.LCName = input.LSName;

                        // 統一編號
                        var idSplit = LCID.ToCharArray();
                        lcInfo.LCID_1_1 = idSplit[0].ToString();
                        lcInfo.LCID_1_2 = idSplit[1].ToString();
                        lcInfo.LCID_1_3 = idSplit[2].ToString();
                        lcInfo.LCID_1_4 = idSplit[3].ToString();
                        lcInfo.LCID_1_5 = idSplit[4].ToString();
                        lcInfo.LCID_1_6 = idSplit[5].ToString();
                        lcInfo.LCID_1_7 = idSplit[6].ToString();
                        lcInfo.LCID_1_8 = idSplit[7].ToString();

                        lcInfo.CreatorTime = CreatorTime;
                        lcInfo.CreatorUserId = CreatorUserId;
                        lcInfo.CreatorUserOrgId = CurrentUser.OrganizeId;
                        lcInfo.CreatorUserDeptId = CurrentUser.DeptId;
                        landLordBelongingService.Insert(new LandLordBelonging
                        {
                            LId = LCID,
                            SalesId = CurrentUser.UserId,
                        });

                        long ln = await lcService.InsertAsync(lcInfo, conn, tran).ConfigureAwait(false);
                        updateResult = ln > 0;
                    }
                }

                // 物件
                List<MOBuildingInputDto> buildingList = input.BuildingList;
                if (buildingList.Count > 0) {
                    MOBuilding moBuildingInfo = new MOBuilding();
                    foreach (MOBuildingInputDto building in buildingList) {
                        // 判斷是否己有此建物
                        if (!string.IsNullOrEmpty(building.BAdd)) {
                            if (!moBAddList.Contains(building.BAdd) && !bBAddList.Contains(building.BAdd)) {
                                moBuildingInfo = new MOBuilding();
                                moBuildingInfo.MOID = input.MOID;
                                moBuildingInfo.BAdd = building.BAdd;
                                moBuildingInfo.BAdd_1 = building.BAdd_1;
                                moBuildingInfo.BAdd_1_1 = building.BAdd_1_1;
                                moBuildingInfo.BAdd_1_2 = building.BAdd_1_2;
                                moBuildingInfo.BAdd_2 = building.BAdd_2;
                                moBuildingInfo.BAdd_2_1 = building.BAdd_2_1;
                                moBuildingInfo.BAdd_2_2 = building.BAdd_2_2;
                                moBuildingInfo.BAdd_2_3 = building.BAdd_2_3;
                                moBuildingInfo.BAdd_2_4 = building.BAdd_2_4;
                                moBuildingInfo.BAdd_3 = building.BAdd_3;
                                moBuildingInfo.BAdd_3_1 = building.BAdd_3_1;
                                moBuildingInfo.BAdd_3_2 = building.BAdd_3_2;
                                moBuildingInfo.BAdd_4 = building.BAdd_4;
                                moBuildingInfo.BAdd_5 = building.BAdd_5;
                                moBuildingInfo.BAdd_6 = building.BAdd_6;
                                moBuildingInfo.BAdd_7 = building.BAdd_7;
                                moBuildingInfo.BAdd_8 = building.BAdd_8;
                                moBuildingInfo.BAdd_9 = building.BAdd_9;
                                moBuildingInfo.BPropoty = building.BPropoty;
                                moBuildingInfo.CreatorTime = CreatorTime;
                                moBuildingInfo.CreatorUserId = CreatorUserId;

                                long moBCount = await moBuildingService.InsertAsync(moBuildingInfo, conn, tran).ConfigureAwait(false);
                                updateResult = moBCount > 0;

                                // 同步新增建物
                                var buildingInfo = moBuildingInfo.MapTo<Building>();
                                buildingInfo.Id = GuidUtils.CreateNo();
                                buildingInfo.CreatorTime = DateTime.Now;
                                buildingInfo.CreatorUserId = CurrentUser.UserId;
                                buildingInfo.CreatorUserOrgId = CurrentUser.OrganizeId;
                                buildingInfo.CreatorUserDeptId = CurrentUser.DeptId;
                                buildingInfo.BState = "待招租";

                                buildingInfo.BelongLID = input.LSID;
                                var b1info = new Building1();
                                b1info.Id = buildingInfo.Id;
                                b1info.BAdd = buildingInfo.BAdd;
                                b1info.CreatorUserId = buildingInfo.CreatorUserId;
                                b1info.CreatorTime = buildingInfo.CreatorTime;
                                b1info.CreatorUserDeptId = buildingInfo.CreatorUserDeptId;
                                b1info.CreatorUserOrgId = buildingInfo.CreatorUserOrgId;

                                List<BuildingBelonging> buildingBelongingEntitys = new List<BuildingBelonging>();

                                updateResult = await buildingService.InsertAsync(buildingInfo, buildingBelongingEntitys, tran).ConfigureAwait(false);
                                updateResult = await building1Service.InsertAsync(b1info, tran).ConfigureAwait(false);

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
                                updateResult = await buildingAdvertisementService.InsertAsync(baInfo);
                            }
                        } else {
                            result.ErrMsg = "物件地址不能為空";
                            return ToJsonContent(result);
                        }
                    }
                }

                updateResult = await iService.UpdateAsync(info, input.Id, conn, tran).ConfigureAwait(false);

                if (updateResult) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    result.ErrMsg = "更新分租物件單失敗";
                    result.ErrCode = ErrCode.err43002;
                    result.Success = false;
                }

            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("更新分租物件單失敗", ex);
                result.ErrMsg = "更新分租物件單失敗";
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
        public override async Task<IActionResult> DeleteBatchAsync(Commons.Core.Dtos.DeletesInputDto input)
        {
            CommonResult result = new CommonResult();
            //string where = "id in ('" + input.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            string where = "MOID in ('" + input.Ids.Join(",").Trim(',').Replace(",", "','") + "')";

            if (!string.IsNullOrEmpty(where)) {
                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();

                try {
                    //foreach (var moid in input.Ids) {                        
                    //    List<MOBuildingOutputDto> mobList = await moBuildingService.GetByMOID(moid.ToString()).ConfigureAwait(false);
                    //    string whereb = "BAdd in ('" + String.Join(",", mobList.Select(o => o.BAdd)).Trim(',').Replace(",", "','") + "')";

                    //    bool b1 = await buildingService.DeleteBatchWhereAsync(whereb, conn, tran).ConfigureAwait(false);
                    //    bool b2 = await building1Service.DeleteBatchWhereAsync(whereb, conn, tran).ConfigureAwait(false);                        
                    //}

                    bool mobl = await moBuildingService.DeleteBatchWhereAsync(where, conn, tran).ConfigureAwait(false);

                    bool bl = await iService.DeleteBatchWhereAsync(where, conn, tran).ConfigureAwait(false);

                    if (mobl && bl) {
                        eftran.Commit();

                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;
                    } else {
                        result.ErrCode = "43003";
                        result.ErrMsg = "刪除分租物件單失敗";
                        result.Success = false;
                    }
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("刪除分租物件單失敗", ex);
                    result.ErrMsg = "刪除分租物件單失敗";
                    result.ErrCode = ErrCode.err43003;
                    result.Success = false;
                    // 觸發ExceptionHandlingAttribute.OnException
                    throw;
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 根據分租物件單號查詢分租物件資訊
        /// </summary>
        /// <param name="input"></param>
        [HttpDelete("GetByMOID")]
        [YuebonAuthorize("List")]
        public async Task<CommonResult<MOOutputDto>> GetByMOID(MOInputDto input)
        {
            CommonResult<MOOutputDto> result = new CommonResult<MOOutputDto>();
            MOOutputDto info = await iService.GetByMOID(input.MOID);

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
        /// 判斷建物是否己存在
        /// </summary>
        /// <param name="input"></param>
        [HttpPost("IsBuildingExist")]
        [YuebonAuthorize("List")]
        public async Task<CommonResult> IsBuildingExist(Commons.Core.Dtos.DeletesInputDto input)
        {
            CommonResult result = new CommonResult();

            string where = "BAdd in ('" + input.Ids.Join(",").Trim(',').Replace(",", "','") + "')";

            int mobCount = await moBuildingService.GetCountByWhereAsync(where);
            int bCount = await buildingService.GetCountByWhereAsync(where);

            if (mobCount > 0 || bCount > 0) {
                result.ErrCode = ErrCode.successCode;
                result.ResData = true;
                result.Success = true;
            } else {
                result.ErrCode = ErrCode.successCode;
                result.ResData = false;
                result.Success = true;
            }

            return result;
        }
    }
}