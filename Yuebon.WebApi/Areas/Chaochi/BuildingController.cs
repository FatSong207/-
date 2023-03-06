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
using System.Collections.Generic;
using Yuebon.Commons.Json;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.StaticFiles;
using Yuebon.Chaochi.Core.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Yuebon.Commons.Repositories;
using Yuebon.Chaochi.Services;
using Yuebon.Chaochi.Repositories;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Commons.Core.Dapper;
using Yuebon.Commons.IDbContext;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Pdf.IO;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extensions;
using Yuebon.Chaochi.Core.Dtos;

namespace Yuebon.WebApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 用戶接口
    /// </summary>
    [ApiController]
    //[ApiVersionNeutral]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class BuildingController : AreaApiController<Building, BuildingOutputDto, BuildingInputDto, IBuildingService, string>
    {
        private IBuildingBelongingService iBuildingBelongingService;
        private readonly IDbContextCore ybContext;
        private Yuebon.Security.IServices.IOrganizeService organizeService;
        private Yuebon.Security.IServices.IRoleService roleService;
        private Yuebon.Security.IServices.IUserLogOnService userLogOnService;
        private readonly IHistoryFormBuildingService historyFormBuildingService;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IBuilding1Service _building1Service;
        private readonly IBuildingEqService _buildingEqService;
        private readonly IBuildingEquipmentService _buildingEquipmentService;
        private readonly IBuildingAdvertisementService _buildingAdvertisementService;
        private readonly Yuebon.Security.IServices.IUserService _userService;
        private readonly ICustomerLNService _customerLNService;
        private readonly ICustomerLCService _customerLCService;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="building1Service"></param>
        /// <param name="_ybContext"></param>
        /// <param name="buildingRepository"></param>
        /// <param name="buildingEqService"></param>
        /// <param name="buildingEquipmentService"></param>
        /// <param name="buildingAdvertisementService"></param>
        /// <param name="userService"></param>
        /// <param name="customerLNService"></param>
        /// <param name="customerLCService"></param>
        /// <param name="_historyFormBuildingService"></param>
        /// <param name="_IBuildingBelongingService"></param>
        /// <param name="_organizeService"></param>
        /// <param name="_roleService"></param>
        /// <param name="_userLogOnService"></param>
        public BuildingController(IBuildingService _iService, IBuilding1Service building1Service, IDbContextCore _ybContext, IBuildingRepository buildingRepository, IBuildingEqService buildingEqService, IBuildingEquipmentService buildingEquipmentService, IBuildingAdvertisementService buildingAdvertisementService, Yuebon.Security.IServices.IUserService userService, ICustomerLNService customerLNService, ICustomerLCService customerLCService, IHistoryFormBuildingService _historyFormBuildingService, IBuildingBelongingService _IBuildingBelongingService, Yuebon.Security.IServices.IOrganizeService _organizeService, Yuebon.Security.IServices.IRoleService _roleService, Yuebon.Security.IServices.IUserLogOnService _userLogOnService) : base(_iService)
        {
            iService = _iService;
            ybContext = _ybContext;
            _buildingRepository = buildingRepository;
            _building1Service = building1Service;
            _buildingEqService = buildingEqService;
            _buildingEquipmentService = buildingEquipmentService;
            _buildingAdvertisementService = buildingAdvertisementService;
            _userService = userService;
            _customerLNService = customerLNService;
            _customerLCService = customerLCService;
            iBuildingBelongingService = _IBuildingBelongingService;
            organizeService = _organizeService;
            roleService = _roleService;
            userLogOnService = _userLogOnService;
            historyFormBuildingService = _historyFormBuildingService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Building info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.CreatorUserOrgId = CurrentUser.OrganizeId;
            info.CreatorUserDeptId = CurrentUser.DeptId;
            info.LastModifyTime = DateTime.Now;
            info.LastModifyUserId = CurrentUser.UserId;
            info.BState = "待招租";
            info.BTransfer = "/Yes";
            info.BDMon = "2";
            //info.OrganizeId = organizeService.GetRootOrganize(info.DepartmentId).Id;
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
        protected override void OnBeforeUpdate(Building info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
            //info.OrganizeId = organizeService.GetRootOrganize(info.DepartmentId).Id;
        }

        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <param name="BuildingBelongings"></param>
        /// <returns></returns>
        protected void OnBeforeUpdate(Building info, List<BuildingBelonging> BuildingBelongings)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
            //info.OrganizeId = organizeService.GetRootOrganize(info.DepartmentId).Id;
            foreach (var Belonging in BuildingBelongings) {
                Belonging.LastModifyUserId = info.LastModifyUserId;
                Belonging.LastModifyTime = info.LastModifyTime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("DeleteBatchAsync")]
        [YuebonAuthorize("Delete")]
        public override async Task<IActionResult> DeleteBatchAsync(DeletesInputDto info)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            where = "id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            if (!string.IsNullOrEmpty(where)) {
                bool bl = await iService.DeleteBatchWhereAsync(where).ConfigureAwait(false);
                bool b2 = await _building1Service.DeleteBatchWhereAsync(where).ConfigureAwait(false);
                if (bl && b2) {
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.ErrMsg = ErrCode.err43003;
                    result.ErrCode = "43003";
                }
            }
            return ToJsonContent(result);
            //CommonResult result = new CommonResult();
            //using (var conn = _ybdbcontext.GetDatabase().GetDbConnection()) {
            //    conn.Open();
            //    using var tran = conn.BeginTransaction();
            //    try {
            //        bool bl = await iService.DeleteAsync(id, conn, tran).ConfigureAwait(false);
            //        bool b2 = await _building1Service.DeleteAsync(id, conn, tran).ConfigureAwait(false);
            //        tran.Commit();
            //        if (bl && b2) {
            //            result.ErrCode = ErrCode.successCode;
            //            result.ErrMsg = ErrCode.err0;
            //        }
            //    } catch (Exception) {
            //        tran.Rollback();
            //        result.ErrMsg = ErrCode.err43003;
            //        result.ErrCode = "43003";
            //    }
            //}
            //return ToJsonContent(result);
        }

        /// <summary>
        /// 儲存設備
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("SaveBEq")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> SaveBEq(BuildingEquipmentInPutDto buildingEquipmentInPutDto)
        {
            var result = new CommonResult();
            foreach (var item in buildingEquipmentInPutDto.BuildingEqs) {
                item.BAdd = buildingEquipmentInPutDto.BAdd;
            }

            var buildingEquipment = ListeqMergeToObj(buildingEquipmentInPutDto.BAdd, buildingEquipmentInPutDto.BuildingEqs);
            //日期
            var dateTime = DateTime.Now;
            buildingEquipment.HandOver_Y = (dateTime.Year.ToInt() - 1911).ToString();
            buildingEquipment.HandOver_M = dateTime.Month.ToString();
            buildingEquipment.HandOver_D = dateTime.Day.ToString();
            buildingEquipment.OtherDevices = buildingEquipmentInPutDto.OtherDevices;

            var id = _buildingEqService.GetIdByBAdd(buildingEquipmentInPutDto.BAdd);
            var eqId = _buildingEquipmentService.GetIdByBAdd(buildingEquipmentInPutDto.BAdd);
            if (string.IsNullOrEmpty(id)) {
                try {
                    using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                    conn.Open();
                    using var eftran = ybContext.GetDatabase().BeginTransaction();
                    //var tran = eftran.GetDbTransaction();
                    try {
                        _buildingEquipmentService.Insert(buildingEquipment);
                        _buildingEqService.InsertRange(buildingEquipmentInPutDto.BuildingEqs);

                        eftran.Commit();
                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } catch (Exception ex) {
                        eftran.Rollback();
                        Log4NetHelper.Error("BuildingEq新增失敗", ex);
                        result.ErrMsg = "BuildingEq新增失敗";
                        result.ErrCode = ErrCode.err43001;
                        throw;
                    }
                } catch (Exception ex) {
                    Log4NetHelper.Error("TransactionFailed", ex);
                    result.ErrMsg = "TransactionFailed";
                    result.ErrCode = ErrCode.err43001;
                }
                return ToJsonContent(result);
            } else {
                try {
                    using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                    conn.Open();
                    using var eftran = ybContext.GetDatabase().BeginTransaction();
                    try {

                        await _buildingEqService.DeleteBatchWhereAsync($" BAdd='{buildingEquipmentInPutDto.BAdd}' ");
                        _buildingEqService.InsertRange(buildingEquipmentInPutDto.BuildingEqs);
                        buildingEquipment.Id = eqId;
                        _buildingEquipmentService.Update(buildingEquipment, eqId);

                        eftran.Commit();

                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } catch (Exception ex) {
                        eftran.Rollback();
                        Log4NetHelper.Error("BuildingEq更新失敗", ex);
                        result.ErrMsg = "BuildingEq更新失敗";
                        result.ErrCode = ErrCode.err43002;
                    }

                } catch (Exception ex) {
                    Log4NetHelper.Error("TransactionFailed", ex);
                    result.ErrMsg = "TransactionFailed";
                    result.ErrCode = ErrCode.err43002;
                }
                return ToJsonContent(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bAdd"></param>
        /// <param name="buildingEqs"></param>
        /// <returns></returns>
        /// 
        [NonAction]
        public BuildingEquipment ListeqMergeToObj(string bAdd, List<BuildingEq> buildingEqs)
        {
            var result = new BuildingEquipment();
            result.BAdd = bAdd;
            foreach (var item in buildingEqs) {
                switch (item.EqName) {
                    case "分離式冷氣":
                        result.SAConditioner = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.SAConditionerNo = item.EqCount;
                        result.SAConditionerBrand = item.EqBrand;
                        break;
                    case "窗型冷氣":
                        result.WAConditioner = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.WAConditionerNo = item.EqCount;
                        result.WAConditionerBrand = item.EqBrand;
                        break;
                    case "電視機":
                        result.TVset = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.TVsetNo = item.EqCount;
                        result.TVsetBrand = item.EqBrand;
                        break;
                    case "電冰箱":
                        result.Refrigerator = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.RefrigeratorNo = item.EqCount;
                        result.RefrigeratorBrand = item.EqBrand;
                        break;
                    case "瓦斯爐":
                        result.GasStove = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.GasStoveNo = item.EqCount;
                        result.GasStoveBrand = item.EqBrand;
                        break;
                    case "抽油煙機":
                        result.RangeHood = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.RangeHoodNo = item.EqCount;
                        result.RangeHoodBrand = item.EqBrand;
                        break;
                    case "電熱水器":
                        result.EWaterHeater = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        //result.GWaterHeater = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.WaterHeaterNo += item.EqCount != "0" && item.EqCount != "" ? " 電:" + item.EqCount : "";
                        result.WaterHeaterBrand += item.EqCount != "0" && item.EqCount != "" ? " 電:" + item.EqBrand : "";
                        break;
                    case "瓦斯熱水器":
                        //result.EWaterHeater = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.GWaterHeater = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.WaterHeaterNo += item.EqCount != "0" && item.EqCount != "" ? " 瓦斯:" + item.EqCount : "";
                        result.WaterHeaterBrand += item.EqCount != "0" && item.EqCount != "" ? " 瓦斯:" + item.EqBrand : "";
                        break;
                    case "洗衣機":
                        result.WashingMachine = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.WashingMachineNo = item.EqCount;
                        result.WashingMachineBrand = item.EqBrand;
                        break;
                    case "一般馬桶":
                        result.Toilet = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.ToiletNo = item.EqCount;
                        break;
                    case "免治馬桶":
                        result.Washlet = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.WashletNo = item.EqCount;
                        break;
                    case "抽風機":
                        result.ExhaustFan = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.ExhaustFanNo = item.EqCount;
                        break;
                    case "暖風機":
                        result.Heater = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.HeaterNo = item.EqCount;
                        break;
                    case "水龍頭":
                        if (item.Category == "bath") {
                            result.Faucet = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                            result.FaucetNo = item.EqCount;
                        } else {
                            result.KFaucet = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                            result.KFaucetNo = item.EqCount;
                        }
                        break;
                    case "洗臉盆":
                        result.WashBasin = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.WashBasinNo = item.EqCount;
                        break;
                    case "浴鏡組":
                        result.BathMirror = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.BathMirrorNo = item.EqCount;
                        break;
                    case "浴櫃":
                        result.BathCabinet = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.BathCabinetNo = item.EqCount;
                        break;
                    case "淋浴龍頭":
                        result.Shower = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.ShowerNo = item.EqCount;
                        break;
                    case "蓮蓬頭":
                        result.ShowerHead = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.ShowerHeadNo = item.EqCount;
                        break;
                    case "浴缸":
                        result.Tub = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.TubNo = item.EqCount;
                        break;
                    case "淋浴拉門":
                        result.SlidingDoor = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.SlidingDoorNo = item.EqCount;
                        break;
                    case "毛巾架":
                        result.TowelRack = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.TowelRackNo = item.EqCount;
                        break;
                    case "流理臺":
                        result.FlowTable = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.FlowTableNo = item.EqCount;
                        break;
                    case "電器櫃":
                        result.ElectCabinet = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.ElectCabinetNo = item.EqCount;
                        break;
                    case "廚櫃":
                        result.KitchenCabinet = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.KitchenCabinetNo = item.EqCount;
                        break;
                    case "電磁爐":
                        result.InductionCooker = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.InductionCookerNo = item.EqCount;
                        break;
                    case "微波爐":
                        result.MicrowaveOven = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.MicrowaveOvenNo = item.EqCount;
                        break;
                    case "烤箱":
                        result.MicroOven = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.MicroOvenNo = item.EqCount;
                        break;
                    case "電鍋":
                        result.ElectricPot = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.ElectricPotNo = item.EqCount;
                        break;
                    case "烘碗機":
                        result.DishDryer = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.DishDryerNo = item.EqCount;
                        break;
                    case "淨水器":
                        result.WaterPurifier = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.WaterPurifierNo = item.EqCount;
                        break;
                    case "單人沙發":
                        result.SingleSofa = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.SingleSofaNo = item.EqCount;
                        break;
                    case "雙人沙發":
                        result.DoubleSofa = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.DoubleSofaNo = item.EqCount;
                        break;
                    case "三人(以上)沙發":
                        result.Couch = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.CouchNo = item.EqCount;
                        break;
                    case "茶几":
                        result.LowStool = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.LowStoolNo = item.EqCount;
                        break;
                    case "矮凳":
                        result.CoffeeTable = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.CoffeeTableNo = item.EqCount;
                        break;
                    case "鞋櫃":
                        result.ShoeBox = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.ShoeBoxNo = item.EqCount;
                        break;
                    case "餐桌":
                        result.DiningTable = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.DiningTableNo = item.EqCount;
                        break;
                    case "餐椅":
                        result.DiningChair = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.DiningChairNo = item.EqCount;
                        break;
                    case "室外大門":
                        result.OutdoorGate = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.OutdoorGateNo = item.EqCount;
                        break;
                    case "室內門":
                        result.InteriorDoor = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.InteriorDoorNo = item.EqCount;
                        break;
                    case "保全設施":
                        result.SecurityFacilities = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.SecurityFacilitiesNo = item.EqCount;
                        break;
                    case "室內照明(顆)":
                        result.IndoorLighting = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.IndoorLightingNo = item.EqCount;
                        break;
                    case "電風扇":
                        result.ElectricFan = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.ElectricFanNo = item.EqCount;
                        break;
                    case "窗簾":
                        result.Curtain = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.CurtainNo = item.EqCount;
                        break;
                    case "衣櫃":
                        result.Wardrobe = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.WardrobeNo = item.EqCount;
                        break;
                    case "置物櫃":
                        result.Locker = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.LockerNo = item.EqCount;
                        break;
                    case "床頭櫃":
                        result.BedsideTable = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.BedsideTableNo = item.EqCount;
                        break;
                    case "梳妝台":
                        result.Dresser = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.DresserNo = item.EqCount;
                        break;
                    case "書桌":
                        result.Desk = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.DeskNo = item.EqCount;
                        break;
                    case "椅子":
                        result.Chair = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.ChairNo = item.EqCount;
                        break;
                    case "單人床(組)":
                        result.SingleBed = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.SingleBedNo = item.EqCount;
                        break;
                    case "雙人床(組)":
                        result.DoubleBed = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.DoubleBedNo = item.EqCount;
                        break;
                    case "滅火器":
                        result.BFireEX = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.BFireEXNo = item.EqCount;
                        break;
                    case "偵煙警報器":
                        result.BSmokeDE = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.BSmokeDENo = item.EqCount;
                        break;
                    case "瓦斯警報器":
                        result.GasAlarm = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.GasAlarmNo = item.EqCount;
                        break;
                    case "緊急照明燈":
                        result.EmergencyLights = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.EmergencyLightsNo = item.EqCount;
                        break;
                    case "大樓鑰匙":
                        result.BuildingKey = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.BuildingKeyNo = item.EqCount;
                        break;
                    case "室外大門鑰匙":
                        result.OutdoorGateKey = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.OutdoorGateKeyNo = item.EqCount;
                        break;
                    case "室內門鑰匙":
                        result.InteriorDoorKey = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.InteriorDoorKeyNo = item.EqCount;
                        break;
                    case "信箱鑰匙":
                        result.MailboxKey = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.MailboxKeyNo = item.EqCount;
                        break;
                    case "磁扣卡":
                        result.MagneticCard = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.MagneticCardNo = item.EqCount;
                        break;
                    case "電子門鎖":
                        result.ElectricDoorlock = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.ElectricDoorlockNo = item.EqCount;
                        break;
                    case "電視遙控器":
                        result.TVRemote = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.TVRemoteNo = item.EqCount;
                        break;
                    case "冷氣遙控器":
                        result.AirConditionerRemote = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.AirConditionerRemoteNo = item.EqCount;
                        break;
                    case "網路分享器":
                        result.IPSharingRouter = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.IPSharingRouterNo = item.EqCount;
                        break;
                    case "數位盒":
                        result.DigitalBox = item.EqCount != "0" && item.EqCount != "" ? "/Yes" : "/Off";
                        result.DigitalBoxNo = item.EqCount;
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bAdd"></param>
        /// <returns></returns>
        [HttpGet("GetEq")]
        public async Task<IActionResult> GetEq(string bAdd)
        {
            var result = new CommonResult();
            var datalist = await _buildingEqService.GetListWhereAsync($" BAdd='{bAdd}' ");
            var buildingEquipment = await _buildingEquipmentService.GetWhereAsync($" BAdd='{bAdd}' ");
            var resData = new BuildingEquipmentOutPutDto {
                BuildingEqs = datalist.OrderBy(x => Convert.ToInt32(x.Sort)).ToList(),
                OtherDevices = buildingEquipment is null ? "" : buildingEquipment.OtherDevices,
            };

            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            result.ResData = resData;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步新增數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <param name="landlordId"></param>
        /// <returns></returns>
        [HttpPost("InsertAs")]
        [YuebonAuthorize("Add")]
        public async Task<IActionResult> InsertAsync(BuildingInputDto tinfo, string landlordId)
        {
            CommonResult result = new CommonResult();

            tinfo.BAdd =
                Utils.memergeAddFix(
                   tinfo.BAdd_1,
                   tinfo.BAdd_1_1,
                   tinfo.BAdd_1_2,
                   tinfo.BAdd_2,
                   tinfo.BAdd_2_1,
                   tinfo.BAdd_2_2,
                   tinfo.BAdd_2_3,
                   tinfo.BAdd_2_4,
                   tinfo.BAdd_3,
                   tinfo.BAdd_3_1,
                   tinfo.BAdd_3_2,
                   tinfo.BAdd_4,
                   tinfo.BAdd_5,
                   tinfo.BAdd_6,
                   tinfo.BAdd_7,
                   tinfo.BAdd_8,
                   tinfo.BAdd_9
                );

            if (!string.IsNullOrEmpty(tinfo.BAdd)) {
                string where = string.Format("BAdd='{0}'", tinfo.BAdd);
                Building user = iService.GetWhere(where);
                if (user != null) {
                    result.ErrMsg = "建物地址不能重復";
                    return ToJsonContent(result);
                }
            } else {
                result.ErrMsg = "建物地址不能為空";
                return ToJsonContent(result);
            }
            if (string.IsNullOrEmpty(tinfo.BPropoty)) {
                result.ErrMsg = "建物類型不能為空";
                return ToJsonContent(result);
            }
            var info = tinfo.MapTo<Building>();
            OnBeforeInsert(info);

            var LN = await _customerLNService.GetAsync(landlordId);
            var LC = await _customerLCService.GetAsync(landlordId);

            info.BelongLID = LN == null ? LC.LCID : LN.LNID;
            var b1info = new Building1();
            b1info.Id = info.Id;
            b1info.BAdd = info.BAdd;
            b1info.CreatorUserId = info.CreatorUserId;
            b1info.CreatorTime = info.CreatorTime;
            b1info.CreatorUserDeptId = info.CreatorUserDeptId;
            b1info.CreatorUserOrgId = info.CreatorUserOrgId;

            List<BuildingBelonging> buildingBelongingEntitys = new List<BuildingBelonging>();

            bool buildingInsert = await iService.InsertAsync(info, buildingBelongingEntitys);
            bool building1Insert = await _building1Service.InsertAsync(b1info);

            // 建物廣告
            BuildingAdvertisement baInfo = new BuildingAdvertisement();
            baInfo.Id = info.Id;
            baInfo.BAdd = info.BAdd;
            baInfo.BAStatus = "廣告未上架";
            // 待招租起始日
            baInfo.BRStartDate = DateTime.Today;
            baInfo.CreatorUserId = info.CreatorUserId;
            baInfo.CreatorTime = info.CreatorTime;
            //baInfo.CreatorUserDeptId = info.CreatorUserDeptId;
            //baInfo.CreatorUserOrgId = info.CreatorUserOrgId;
            baInfo.CreatorUserOrgId = info.CreatorUserOrgId;
            baInfo.CreatorUserDeptId = info.CreatorUserDeptId;
            bool buildingAdvertisementInsert = await _buildingAdvertisementService.InsertAsync(baInfo);

            result.Success = building1Insert && buildingInsert && buildingAdvertisementInsert;
            if (result.Success) {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } else {
                result.ErrMsg = ErrCode.err43001;
                result.ErrCode = "43001";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步新增數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <param name="idNo"></param>
        /// <returns></returns>
        [HttpPost("InsertFromWebForm")]
        [YuebonAuthorize("Add")]
        public async Task<IActionResult> InsertFromWebForm(BuildingInputDto tinfo, string idNo)
        {
            CommonResult result = new CommonResult();

            tinfo.BAdd =
                Utils.memergeAddFix(
                   tinfo.BAdd_1,
                   tinfo.BAdd_1_1,
                   tinfo.BAdd_1_2,
                   tinfo.BAdd_2,
                   tinfo.BAdd_2_1,
                   tinfo.BAdd_2_2,
                   tinfo.BAdd_2_3,
                   tinfo.BAdd_2_4,
                   tinfo.BAdd_3,
                   tinfo.BAdd_3_1,
                   tinfo.BAdd_3_2,
                   tinfo.BAdd_4,
                   tinfo.BAdd_5,
                   tinfo.BAdd_6,
                   tinfo.BAdd_7,
                   tinfo.BAdd_8,
                   tinfo.BAdd_9
                );

            if (!string.IsNullOrEmpty(tinfo.BAdd)) {
                string where = string.Format("BAdd='{0}'", tinfo.BAdd);
                Building building = await iService.GetWhereAsync(where);
                if (building != null) {
                    result.ErrMsg = "建物地址不能重復";
                    return ToJsonContent(result);
                }
            } else {
                result.ErrMsg = "建物地址不能為空";
                return ToJsonContent(result);
            }
            if (string.IsNullOrEmpty(tinfo.BPropoty)) {
                result.ErrMsg = "建物類型不能為空";
                return ToJsonContent(result);
            }
            var info = tinfo.MapTo<Building>();
            OnBeforeInsert(info);

            var LN = await _customerLNService.GetWhereAsync($" LNID='{idNo}' ");
            var LC = await _customerLCService.GetWhereAsync($" LCID='{idNo}' ");

            if (LN is null && LC is null) {
                result.ErrMsg = "查無此出租人，無法新增建物";
                return ToJsonContent(result);
            }

            info.BelongLID = idNo;
            var b1info = new Building1();
            b1info.Id = info.Id;
            b1info.BAdd = info.BAdd;
            b1info.CreatorUserId = info.CreatorUserId;
            b1info.CreatorTime = info.CreatorTime;
            b1info.CreatorUserDeptId = info.CreatorUserDeptId;
            b1info.CreatorUserOrgId = info.CreatorUserOrgId;

            List<BuildingBelonging> buildingBelongingEntitys = new List<BuildingBelonging>();

            bool buildingInsert = await iService.InsertAsync(info, buildingBelongingEntitys);
            bool building1Insert = await _building1Service.InsertAsync(b1info);

            // 建物廣告
            BuildingAdvertisement baInfo = new BuildingAdvertisement();
            baInfo.Id = info.Id;
            baInfo.BAdd = info.BAdd;
            baInfo.BAStatus = "廣告未上架";
            baInfo.CreatorUserId = info.CreatorUserId;
            baInfo.CreatorTime = info.CreatorTime;
            //baInfo.CreatorUserDeptId = info.CreatorUserDeptId;
            //baInfo.CreatorUserOrgId = info.CreatorUserOrgId;
            baInfo.CreatorUserOrgId = info.CreatorUserOrgId;
            baInfo.CreatorUserDeptId = info.CreatorUserDeptId;
            bool buildingAdvertisementInsert = await _buildingAdvertisementService.InsertAsync(baInfo);

            result.Success = building1Insert && buildingInsert && buildingAdvertisementInsert;

            if (result.Success) {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } else {
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
        public override async Task<IActionResult> UpdateAsync(BuildingInputDto tinfo)
        {
            CommonResult result = new CommonResult();

            //if (!string.IsNullOrEmpty(tinfo.BAdd)) {
            //    string where = string.Format(" BAdd='{0}' and id!='{1}' ", tinfo.BAdd, tinfo.Id);
            //    Building_DB = iService.GetWhere(where);
            //    if (Building_DB != null) {
            //        result.ErrMsg = "建物地址不能重復";
            //        return ToJsonContent(result);
            //    }
            //} else {
            //    result.ErrMsg = "建物地址不能為空";
            //    return ToJsonContent(result);
            //}
            var Building_DB = iService.Get(tinfo.Id);
            var info = tinfo.MapTo(Building_DB);

            var Building1_DB = _building1Service.Get(tinfo.Id);
            var b1info = tinfo.Building1.MapTo(Building1_DB);
            OnBeforeUpdate(info);
            HandleSpecialData(b1info, tinfo);
            b1info.LastModifyTime = DateTime.Now;
            b1info.LastModifyUserId = CurrentUser.UserId;
            //bool bl = await iService.UpdateAsync(info, tinfo.Id).ConfigureAwait(false);

            try {
                bool b22 = await _building1Service.UpdateAsync(b1info, tinfo.Id);
                bool bl = iService.Update(info, tinfo.Id);
                //foreach (var Belonging in tinfo.BuildingBelongings) {
                //    BuildingBelonging dbBelonging = iBuildingBelongingService.Get(Belonging.Id);
                //    if (dbBelonging == null) {
                //        Belonging.BuildingId = info.Id;
                //        Belonging.CreatorUserId = info.LastModifyUserId;
                //        Belonging.CreatorTime = info.LastModifyTime;
                //        iBuildingBelongingService.Insert(Belonging);
                //    } else {
                //        Belonging.LastModifyUserId = info.LastModifyUserId;
                //        Belonging.LastModifyTime = info.LastModifyTime;
                //        iBuildingBelongingService.Update(Belonging, Belonging.Id);
                //    }
                //}

                //匯款資料
                //foreach (var Remittance in tinfo.Remittances) {
                //    Remittance dbRemittance = IRemittanceService.Get(Remittance.Id);
                //    if (dbRemittance == null) {
                //        Remittance.Id = GuidUtils.CreateNo();
                //        Remittance.BAdd = info.BAdd;
                //        Remittance.CreatorUserId = info.LastModifyUserId;
                //        Remittance.CreatorTime = info.LastModifyTime;
                //        IRemittanceService.Insert(Remittance);
                //    } else {
                //        if ("D" == Remittance.NeedDel) {
                //            IRemittanceService.Delete(Remittance);
                //        } else {
                //            Remittance.LastModifyUserId = info.LastModifyUserId;
                //            Remittance.LastModifyTime = info.LastModifyTime;
                //            IRemittanceService.Update(Remittance, Remittance.Id);
                //        }
                //    }
                //}
                if (bl && b22) {
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.ErrMsg = ErrCode.err43002;
                    result.ErrCode = "43002";
                }
            } catch (Exception) {
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步更新出租住宅基本資料
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("UpdateBuildingRentBasic")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> UpdateBuildingRentBasic(BuildingRentBasicInputDto tinfo)
        {
            var result = new CommonResult();
            var buildingDB = await iService.GetAsync(tinfo.Id);

            //建築完成日期額外處理
            if (!string.IsNullOrEmpty(tinfo.BCDate)) {
                tinfo.BCDate_Y = tinfo.BCDate.Split("-")[0];
                tinfo.BCDate_M = tinfo.BCDate.Split("-")[1];
                tinfo.BCDate_D = tinfo.BCDate.Split("-")[2];
            }


            var building = tinfo.MapTo(buildingDB);
            OnBeforeUpdate(building);
            try {
                var isSuccess = await iService.UpdateAsync(building, tinfo.Id);
                var s2 = await _building1Service.UpdateTableFieldAsync("B1TaxID", tinfo.building1.B1TaxID ?? "", $"Id='{tinfo.Id}'");
                if (isSuccess) {
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.Success = false;
                    result.ErrMsg = ErrCode.err43002;
                    result.ErrCode = "43002";
                }
            } catch (Exception ex) {
                result.Success = false;
                result.ErrMsg = ErrCode.err43002;
                Log4NetHelper.Error("更新出租住宅基本資料錯誤!", ex);
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步更新建物現況
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("UpdateBuildingSituation")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> UpdateBuildingSituation(BuildingSituationInputDto tinfo)
        {
            var result = new CommonResult();
            var buildingDB = await iService.GetAsync(tinfo.Id);
            //建築出租起訖日期額外處理
            if (!string.IsNullOrEmpty(tinfo.BRDStart)) {
                tinfo.BRDStart_Y = tinfo.BRDStart.Split("-")[0];
                tinfo.BRDStart_M = tinfo.BRDStart.Split("-")[1];
                tinfo.BRDStart_D = tinfo.BRDStart.Split("-")[2];
            }
            if (!string.IsNullOrEmpty(tinfo.BRDTEnd)) {
                tinfo.BRDTEnd_Y = tinfo.BRDTEnd.Split("-")[0];
                tinfo.BRDTEnd_M = tinfo.BRDTEnd.Split("-")[1];
                tinfo.BRDTEnd_D = tinfo.BRDTEnd.Split('-')[2];
            }

            var building = tinfo.MapTo(buildingDB);
            OnBeforeUpdate(building);

            try {
                var isSuccess = await iService.UpdateAsync(building, tinfo.Id);
                if (isSuccess) {
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.Success = false;
                    result.ErrMsg = ErrCode.err43002;
                    result.ErrCode = "43002";
                }
            } catch (Exception ex) {
                result.Success = false;
                result.ErrMsg = ErrCode.err43002;
                Log4NetHelper.Error("更新建物現況錯誤!", ex);
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 移轉建物所屬權
        /// </summary>
        /// <param name="updateBuildingBelongingDto"></param>
        /// <returns></returns>
        [HttpPost("UpdateBuildingBelonging")]
        [YuebonAuthorize("UpdateBuildingBelonging")]
        public async Task<IActionResult> UpdateBuildingBelonging(UpdateBuildingBelongingDto updateBuildingBelongingDto)
        {
            var result = new CommonResult();
            var buildings = await iService.GetListWhereAsync($" Id in ('{updateBuildingBelongingDto.buildingIds.Join("','")}') ");
            var creatorId = buildings.Select(x => x.CreatorUserId).Distinct();
            if (creatorId.Count() > 1) {
                result.Success = false;
                result.ErrCode = ErrCode.err40110;
                result.ErrMsg = "偵測所選建物不屬於同一個業務!";
                return ToJsonContent(result);
            } else {
                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();
                try {

                    if (!string.IsNullOrEmpty(updateBuildingBelongingDto.chbelonging.destDept)) {
                        await iService.UpdateTableFieldAsync("CreatorUserId", "", $" Id in ('{updateBuildingBelongingDto.buildingIds.Join("','")}') ", conn, tran);
                        await iService.UpdateTableFieldAsync("CreatorUserDeptId", updateBuildingBelongingDto.chbelonging.destDept, $" Id in ('{updateBuildingBelongingDto.buildingIds.Join("','")}') ", conn, tran);
                        await _buildingAdvertisementService.UpdateTableFieldAsync("CreatorUserId", "", $" Id in ('{updateBuildingBelongingDto.buildingIds.Join("','")}') ", conn, tran);
                        await _buildingAdvertisementService.UpdateTableFieldAsync("CreatorUserDeptId", updateBuildingBelongingDto.chbelonging.destDept, $" Id in ('{updateBuildingBelongingDto.buildingIds.Join("','")}') ", conn, tran);
                    } else {
                        if (updateBuildingBelongingDto.chbelonging.destUserId == "不指定") {
                            await iService.UpdateTableFieldAsync("CreatorUserId", "", $" Id in ('{updateBuildingBelongingDto.buildingIds.Join("','")}') ", conn, tran);
                            await _buildingAdvertisementService.UpdateTableFieldAsync("CreatorUserId", "", $" Id in ('{updateBuildingBelongingDto.buildingIds.Join("','")}') ", conn, tran);
                        } else {
                            //檢查指定業務(destUserId)是否有此建物的房東存取權
                            var b = buildings.FirstOrDefault();
                            var llId = b.BelongLID;
                            switch (llId.Length) {
                                case 10:
                                    var ln = await _customerLNService.GetByCustomerLNID(llId);
                                    if (!ln.CreatorUserId.Contains(updateBuildingBelongingDto.chbelonging.destUserId)) {
                                        result.Success = false;
                                        result.ErrCode = ErrCode.err40110;
                                        result.ErrMsg = $"指定業務無操作此房東({ln.LNName})的權限!";
                                        return ToJsonContent(result);
                                    }
                                    break;
                                case 8:
                                    var lc = await _customerLCService.GetByLCID(llId);
                                    if (!lc.CreatorUserId.Contains(updateBuildingBelongingDto.chbelonging.destUserId)) {
                                        result.Success = false;
                                        result.ErrCode = ErrCode.err40110;
                                        result.ErrMsg = $"指定業務無操作此房東({lc.LCName})的權限!";
                                        return ToJsonContent(result);
                                    }
                                    break;
                                default:
                                    break;
                            }

                            var user = await _userService.GetAsync(updateBuildingBelongingDto.chbelonging.destUserId);
                            await iService.UpdateTableFieldAsync("CreatorUserId", user.Id, $" Id in ('{updateBuildingBelongingDto.buildingIds.Join("','")}') ", conn, tran);
                            await _buildingAdvertisementService.UpdateTableFieldAsync("CreatorUserId", user.Id, $" Id in ('{updateBuildingBelongingDto.buildingIds.Join("','")}') ", conn, tran);

                            if (updateBuildingBelongingDto.chbelonging.destLandlord != "維持原狀" && !string.IsNullOrEmpty(updateBuildingBelongingDto.chbelonging.destLandlord)) {
                                foreach (var item in buildings) {
                                    switch (updateBuildingBelongingDto.chbelonging.destLandlord.Length) {
                                        case 10:
                                            var cln = await _customerLNService.GetByCustomerLNID(updateBuildingBelongingDto.chbelonging.destLandlord);
                                            if (cln.CreatorUserId.Contains(updateBuildingBelongingDto.chbelonging.destUserId)) {
                                                await iService.UpdateTableFieldAsync("BelongLID", updateBuildingBelongingDto.chbelonging.destLandlord, $" Id = '{item.Id}' ", conn, tran);
                                                //await _buildingAdvertisementService.UpdateTableFieldAsync("BelongLID", updateBuildingBelongingDto.chbelonging.destLandlord, $" Id = '{item.Id}' ", conn, tran);
                                            } else {
                                                result.Success = false;
                                                result.ErrCode = ErrCode.err40110;
                                                result.ErrMsg = "此業務無法操作此出租人!";
                                                return ToJsonContent(result);
                                            }
                                            break;
                                        case 8:
                                            var clc = await _customerLCService.GetByLCID(updateBuildingBelongingDto.chbelonging.destLandlord);
                                            if (clc.CreatorUserId.Contains(updateBuildingBelongingDto.chbelonging.destUserId)) {
                                                await iService.UpdateTableFieldAsync("BelongLID", updateBuildingBelongingDto.chbelonging.destLandlord, $" Id = '{item.Id}' ", conn, tran);
                                                //await _buildingAdvertisementService.UpdateTableFieldAsync("BelongLID", updateBuildingBelongingDto.chbelonging.destLandlord, $" Id = '{item.Id}' ", conn, tran);
                                            } else {
                                                result.Success = false;
                                                result.ErrCode = ErrCode.err40110;
                                                result.ErrMsg = "此業務無法操作此出租人!";
                                                return ToJsonContent(result);
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    tran.Commit();
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    return ToJsonContent(result);
                } catch (Exception) {
                    tran.Rollback();
                    throw;
                }

            }
        }

        /// <summary>
        /// 移除建物所屬權
        /// </summary>
        /// <param name="updateBuildingBelongingDto"></param>
        /// <returns></returns>
        [HttpPost("RemoveBuildingBelonging")]
        [YuebonAuthorize("RemoveBuildingBelonging")]
        public async Task<IActionResult> RemoveBuildingBelonging(UpdateBuildingBelongingDto updateBuildingBelongingDto)
        {
            var result = new CommonResult();
            var buildings = await iService.GetListWhereAsync($" Id in ('{updateBuildingBelongingDto.buildingIds.Join("','")}') ");
            var hasWaitToSpe = buildings.Select(x => x.CreatorUserId).Any(a => string.IsNullOrEmpty(a));
            if (hasWaitToSpe) {
                result.Success = false;
                result.ErrCode = ErrCode.err40110;
                result.ErrMsg = "所選建物包含等待指派!";
                return ToJsonContent(result);
            } else {
                await iService.UpdateTableFieldAsync("CreatorUserId", "", $" Id in ('{updateBuildingBelongingDto.buildingIds.Join("','")}') ");
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                return ToJsonContent(result);
            }
        }

        /// <summary>
        /// 異步分頁查詢
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerSearchAsync")]
        [YuebonAuthorize("List")]
        [LogNotMethod]
        public async Task<IActionResult> FindWithPagerSearchAsync(SearchBuildingModel search)
        {
            CommonResult<PageResult<BuildingListOutputDto>> result = new CommonResult<PageResult<BuildingListOutputDto>>();
            result.ResData = await iService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 根據主鍵Id獲取一個對象信息
        /// </summary>
        /// <param name="id">主鍵Id</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        [YuebonAuthorize("List")]
        //[NoPermissionRequired]
        public override async Task<CommonResult<BuildingOutputDto>> GetById(string id)
        {
            var result = new CommonResult<BuildingOutputDto>();
            BuildingOutputDto info = await iService.GetOutDtoAsync(id);
            string landlordName;
            info.Building1 = await _building1Service.GetByBAdd(info.BAdd);
            info.BuildingRentBasic.building1 = await _building1Service.GetByBAdd(info.BAdd);
            if (info.Building1 == null) {
                info.Building1 = new Building1();
                info.BuildingRentBasic.building1 = new Building1();
            }
            ////出租住宅基本資料 (移動到GetOutDtoAsync())
            //var building = await iService.GetAsync(id);
            //var buildingRentBasic = building.MapTo<BuildingRentBasicOutputDto>();
            //var buildingSituation = building.MapTo<BuildingSituationOutputDto>();
            //info.BuildingRentBasic = buildingRentBasic;
            //info.BuildingSituation = buildingSituation;
            var lid = info.BelongLId;
            var OwnerRep = new OwnerRepOutPutDto();
            if (!string.IsNullOrEmpty(lid)) {
                if (lid.Length == 10) {
                    try {
                        var customerLN = await _customerLNService.GetByCustomerLNID(lid);
                        landlordName = customerLN.LNName;
                        OwnerRep.Name = customerLN.LNName;
                        OwnerRep.ID = customerLN.LNID;
                        OwnerRep.Tel1 = customerLN.LNTel_1;
                        OwnerRep.Tel2 = customerLN.LNTel_2;
                        OwnerRep.Address = customerLN.LNAdd;
                        OwnerRep.Cell = customerLN.LNCell;
                        info.OwnerRepOutputDto = OwnerRep;
                        //info.CustomerLN = customerLN;
                        //info.CustomerLC = new CustomerLC();
                        info.BelongLandlord = landlordName;
                        info.BuildingRentBasic.BelongLandlord = landlordName;
                    } catch (Exception ex) {
                        Log4NetHelper.Error("獲取失敗", ex);
                        result.Success = false;
                        result.ErrCode = ErrCode.err40110;
                        throw;
                    }
                } else if (lid.Length == 8) {
                    try {
                        var customerLC = await _customerLCService.GetByLCID(lid);
                        landlordName = customerLC.LCName;
                        OwnerRep.Name = customerLC.LCName;
                        OwnerRep.ID = customerLC.LCID;
                        OwnerRep.Tel1 = customerLC.LCTel_1;
                        OwnerRep.Tel2 = customerLC.LCTel_2;
                        OwnerRep.Address = customerLC.LCAdd;
                        info.OwnerRepOutputDto = OwnerRep;
                        //info.CustomerLC = customerLC;
                        //info.CustomerLN = new CustomerLN();
                        info.BelongLandlord = landlordName;
                        info.BuildingRentBasic.BelongLandlord = landlordName;
                    } catch (Exception ex) {
                        Log4NetHelper.Error("獲取失敗", ex);
                        result.Success = false;
                        result.ErrCode = ErrCode.err40110;
                        throw;
                    }
                } else {
                    result.Success = false;
                    result.ErrMsg = "請檢查房東身分證字號或是統一編號";
                    return (CommonResult<BuildingOutputDto>)ToJsonContent(result);
                }
            } else {
                result.Success = false;
                result.ErrMsg = "獲取建物所屬出租人時發生錯誤!";
                return (CommonResult<BuildingOutputDto>)ToJsonContent(result);
                //info.CustomerLN = new CustomerLN();
            }
            //var 二房東
            //{
            //二房東尚未建立底層
            //}
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
        /// 異步分頁查詢
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ShowImg")]
        //public IActionResult Get(string fileName)
        public async Task<IActionResult> Get(string fileName, string id)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

            var image = await System.IO.File.ReadAllBytesAsync($"{sysSetting.ChaochiFilepath}BuildingImage\\{id}\\{fileName}");
            return File(image, "image/jpeg");
        }


        /// <summary>
        ///  單文件上傳接口
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("ImgUpload")]
        [NoSignRequired]
        public IActionResult ImgUpload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            ////string _fileName = filelist[0].FileName;
            try {
                var buildingId = formCollection["sid"];
                foreach (var item in filelist) {
                    var address = Add(item, buildingId, belongApp, belongAppId);
                }

                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                result.Success = false;
                Log4NetHelper.Error("", ex);
                throw;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 單個上傳文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="buildingId"></param>
        /// <param name="belongApp"></param>
        /// <param name="belongAppId"></param>
        /// <returns></returns>
        private Yuebon.Security.Dtos.UploadFileResultOuputDto Add(IFormFile file, string buildingId, string belongApp, string belongAppId)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = file.FileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    var filenameExt = UploadFile(fileName, data, buildingId);

                    Yuebon.Security.Dtos.UploadFileResultOuputDto uploadFileResultOuputDto = new Yuebon.Security.Dtos.UploadFileResultOuputDto {
                        FileName = filenameExt,
                    };
                    return uploadFileResultOuputDto;
                }
            } else {
                Log4NetHelper.Info("文件過大");
                throw new Exception("文件過大");
            }
        }

        /// <summary>
        /// 實現文件上傳到服務器保存，并生成縮略圖
        /// </summary>
        /// <param name="fileName">文件名稱</param>
        /// <param name="fileBuffers">文件字節流</param>
        /// <param name="buildingId"></param>
        private string UploadFile(string fileName, byte[] fileBuffers, string buildingId)
        {

            //判斷文件是否為空
            if (string.IsNullOrEmpty(fileName)) {
                Log4NetHelper.Info("文件名不能為空");
                throw new Exception("文件名不能為空");
            }

            //判斷文件是否為空
            if (fileBuffers.Length < 1) {
                Log4NetHelper.Info("文件不能為空");
                throw new Exception("文件不能為空");
            }
            fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileName;


            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            string uploadPath = @$"{path}BuildingImage\";
            if (!Directory.Exists(uploadPath + $"{buildingId}")) {
                Directory.CreateDirectory(@$"{uploadPath}{buildingId}\");
            }
            using (var fs = new FileStream(@$"{uploadPath}{buildingId}\" + fileName, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }
            return fileName;
        }

        #region 查詢單個實體
        /// <summary>
        /// 根據主鍵Id獲取一個對象信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("GetImgById")]
        //[NoPermissionRequired]
        public virtual async Task<CommonResult<FileInfoOutputDto>> GetImgById(SearchBuildingModel search)
        {
            CommonResult<FileInfoOutputDto> result = new CommonResult<FileInfoOutputDto>();
            var info = await iService.FindBuildImagesWithPagerSearchAsync(search);
            if (info != null) {
                result.ErrCode = ErrCode.successCode;
                result.ResData = info;
            } else {
                result.ErrMsg = ErrCode.err50001;
                result.ErrCode = "50001";
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 刪除文件
        /// </summary>
        /// <param name="fileNames"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        [HttpPost("DeleteImgFile")]
        public IActionResult DeleteImgFile(List<string> fileNames, string bid)
        {
            //List<string> fileNames = new List<string>();
            CommonResult result = new CommonResult();
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

            DirectoryInfo infoD = new DirectoryInfo(sysSetting.ChaochiFilepath + @$"\BuildingImage\{bid}");

            foreach (var item in fileNames) {
                try {
                    var files = infoD.GetFiles().Where(x => x.Name == item).ToList();
                    string localpath = sysSetting.ChaochiFilepath + $"\\BuildingImage\\{bid}\\";
                    if (files != null && files.Count == 1) {
                        string filepath = localpath + item;
                        if (System.IO.File.Exists(filepath))
                            System.IO.File.Delete(filepath);

                        result.ErrCode = ErrCode.successCode;
                        result.Success = true;
                    } else {
                        result.ErrCode = ErrCode.failCode;
                        result.Success = false;
                    }
                } catch (Exception ex) {
                    Log4NetHelper.Error("", ex);
                    result.ErrCode = "500";
                    result.ErrMsg = ex.Message;
                    throw;
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取Chaochi_BuildingPropoties所有資料(用於下拉選單)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBPropoties")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetBPropoties()
        {
            var result = new CommonResult();
            var dataList = await iService.GetAllBuildingPropotiesAsync();
            result.ResData = dataList;
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result, true);
        }

        /// <summary>
        /// 更新建物狀態
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateBState")]
        [YuebonAuthorize("BA")]
        public async Task<IActionResult> UpdateBState(BuildingInputDto input)
        {
            CommonResult result = new CommonResult();
            bool updateResult = false;

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                foreach (var id in input.ids) {
                    updateResult = await iService.UpdateBState(CurrentUser.UserId, id, input.BState, conn, tran);
                }

                if (updateResult) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    eftran.Rollback();
                    result.ErrMsg = "更新建物狀態失敗";
                    result.ErrCode = ErrCode.err43002;
                    result.Success = false;
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("更新建物狀態失敗", ex);
                result.ErrMsg = "更新建物狀態失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        [HttpGet("GenPDF")]
        public IActionResult GenPDF(string id)
        {
            var result = new CommonResult();
            Document myDoc = new Document(PageSize.A4);
            var filenameTo = DateTime.Now.ToString("yyyyMMddHHmmss") + id + "合約屋況照片.pdf";
            var direinfo = new DirectoryInfo(@$"D:\zzz\image\historyPDF\Building\{id}");
            if (!direinfo.Exists) {
                direinfo.Create();
            }
            var fs = new FileStream(@$"D:\zzz\image\historyPDF\Building\{id}\{filenameTo}", FileMode.Create);
            var writer = PdfWriter.GetInstance(myDoc, fs);
            string chinese = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "KAIU.TTF");
            BaseFont baseFont = BaseFont.CreateFont(chinese, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont);
            try {
                string filesPath = @$"D:\zzz\image\BuildingImage\{id}";
                if (!Directory.Exists(filesPath)) {
                    Directory.CreateDirectory(filesPath);
                }
                var files = System.IO.Directory.GetFiles(filesPath);
                //foreach (var file in files) {
                //    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(file);
                //    //image.ScaleToFit(300f,300f);
                //    var filename = file.Split(@"\");
                //    basicTable.AddCell(new PdfPCell(new Phrase(filename[filename.Length - 1], font)));
                //    basicTable.AddCell(new PdfPCell(image, true));
                //};
                myDoc.Open();

                var title = new Paragraph("建物照片 \n \n ", font);
                title.Alignment = Element.ALIGN_CENTER;
                myDoc.Add(title);

                for (int i = 0; i < files.Length; i += 2) {
                    PdfPTable basicTable = new PdfPTable(new float[] { 1f, 1f });
                    basicTable.WidthPercentage = 100f;
                    //var filename1 = files[i].Split(@"\");
                    //PdfPCell cellfilename1 = new PdfPCell(new Phrase(filename1[filename1.Length - 1], font));
                    //basicTable.AddCell(cellfilename1);
                    //if (i + 1 != files.Length) {
                    //    var filename2 = files[i + 1].Split(@"\");
                    //    PdfPCell cellfilename2 = new PdfPCell(new Phrase(filename2[filename2.Length - 1], font));
                    //    basicTable.AddCell(cellfilename2);
                    //} else {
                    //    basicTable.AddCell(new PdfPCell(new Phrase("", font)));
                    //}

                    iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(files[i]);
                    image1.ScaleToFit(260f, 150f);
                    PdfPCell cell = new PdfPCell(image1);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.UseBorderPadding = true;
                    cell.BackgroundColor = new BaseColor(235, 235, 235);
                    cell.FixedHeight = 151f;
                    basicTable.AddCell(cell);
                    if (i + 1 != files.Length) {
                        iTextSharp.text.Image image2 = iTextSharp.text.Image.GetInstance(files[i + 1]);
                        image2.ScaleToFit(260f, 150f);
                        PdfPCell cell2 = new PdfPCell(image2);
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell2.UseBorderPadding = true;
                        cell2.BackgroundColor = new BaseColor(235, 235, 235);
                        cell2.FixedHeight = 151f;
                        basicTable.AddCell(cell2);
                    } else {
                        basicTable.AddCell(new PdfPCell(new Phrase("", font)));
                    }
                    basicTable.KeepTogether = true;
                    myDoc.Add(basicTable);//加入table

                    //最後一章不要加換行符號
                    if (!(i > 0 && i % 6 == 0)) {
                        var enter = new Paragraph(" \n ");
                        enter.Alignment = Element.ALIGN_CENTER;
                        myDoc.Add(enter);

                    }
                };
                //寫入歷史表單table
                var historyB = new HistoryFormBuilding() {
                    IDNo = id,
                    FormName = "合約屋況照片",
                    FileName = filenameTo,
                    UploadTime = DateTime.Now,
                    Note = "系統上傳",
                    CreatorUserId = CurrentUser.UserId
                };
                historyFormBuildingService.InsertAsync(historyB);
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } catch (Exception ex) {
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                Log4NetHelper.Error("製作PDF失敗", ex);
            } finally {
                if (myDoc.IsOpen()) {
                    myDoc.Close();
                    fs.Close();
                    writer.Close();
                }
            }
            return ToJsonContent(result);
        }


        /// <summary>
        /// 新增或修改時合併或分割特殊欄位(地址、生日、電話)
        /// </summary>
        /// <param name="sourceB1"></param>
        /// <param name="inputDtoBuilding"></param>
        /// <returns></returns>
        private Building1 HandleSpecialData(Building1 sourceB1, BuildingInputDto inputDtoBuilding)
        {
            #region 其他所有權人戶籍地址
            var inputDtoB1 = inputDtoBuilding.Building1;

            //權力範圍
            sourceB1.B1ExtentOfOwner = inputDtoB1.B1ExtentOfOwner_1 + "/" + inputDtoB1.B1ExtentOfOwner_2;
            sourceB1.B1ExtentOfOwner_A = inputDtoB1.B1ExtentOfOwner_A_1 + "/" + inputDtoB1.B1ExtentOfOwner_A_2;
            sourceB1.B1ExtentOfOwner_B = inputDtoB1.B1ExtentOfOwner_B_1 + "/" + inputDtoB1.B1ExtentOfOwner_B_2;
            sourceB1.B1ExtentOfOwner_C = inputDtoB1.B1ExtentOfOwner_C_1 + "/" + inputDtoB1.B1ExtentOfOwner_C_2;
            sourceB1.B1ExtentOfOwner_D = inputDtoB1.B1ExtentOfOwner_D_1 + "/" + inputDtoB1.B1ExtentOfOwner_D_2;
            sourceB1.B1ExtentOfOwner_E = inputDtoB1.B1ExtentOfOwner_E_1 + "/" + inputDtoB1.B1ExtentOfOwner_E_2;

            //其他所有權人手機
            //if (!string.IsNullOrEmpty(inputDtoB1.B1OwnerCell_A)) {
            //    if (inputDtoB1.B1OwnerCell_A.Length >= 4 && !inputDtoB1.B1OwnerCell_A.Contains("-")) {
            //        sourceB1.B1OwnerCell_A = inputDtoB1.B1OwnerCell_A.Insert(4, "-");
            //    }
            //}
            //if (!string.IsNullOrEmpty(inputDtoB1.B1OwnerCell_B)) {
            //    if (inputDtoB1.B1OwnerCell_B.Length >= 4 && !inputDtoB1.B1OwnerCell_B.Contains("-")) {
            //        sourceB1.B1OwnerCell_B = inputDtoB1.B1OwnerCell_B.Insert(4, "-");
            //    }
            //}
            //if (!string.IsNullOrEmpty(inputDtoB1.B1OwnerCell_C)) {
            //    if (inputDtoB1.B1OwnerCell_C.Length >= 4 && !inputDtoB1.B1OwnerCell_C.Contains("-")) {
            //        sourceB1.B1OwnerCell_C = inputDtoB1.B1OwnerCell_C.Insert(4, "-");
            //    }
            //}
            //if (!string.IsNullOrEmpty(inputDtoB1.B1OwnerCell_D)) {
            //    if (inputDtoB1.B1OwnerCell_D.Length >= 4 && !inputDtoB1.B1OwnerCell_D.Contains("-")) {
            //        sourceB1.B1OwnerCell_D = inputDtoB1.B1OwnerCell_D.Insert(4, "-");
            //    }
            //}
            //地址
            sourceB1.B1OwnerAdd_A = Utils.memergeAddFix(
                inputDtoB1.B1OwnerAdd_1_A,
                inputDtoB1.B1OwnerAdd_1_1_A,
                inputDtoB1.B1OwnerAdd_1_2_A,
                inputDtoB1.B1OwnerAdd_2_A,
                inputDtoB1.B1OwnerAdd_2_1_A,
                inputDtoB1.B1OwnerAdd_2_2_A,
                inputDtoB1.B1OwnerAdd_2_3_A,
                inputDtoB1.B1OwnerAdd_2_4_A,
                inputDtoB1.B1OwnerAdd_3_A,
                inputDtoB1.B1OwnerAdd_3_1_A,
                inputDtoB1.B1OwnerAdd_3_2_A,
                inputDtoB1.B1OwnerAdd_4_A,
                inputDtoB1.B1OwnerAdd_5_A,
                inputDtoB1.B1OwnerAdd_6_A,
                inputDtoB1.B1OwnerAdd_7_A,
                inputDtoB1.B1OwnerAdd_8_A,
                inputDtoB1.B1OwnerAdd_9_A);
            sourceB1.B1OwnerAdd_B = Utils.memergeAddFix(
                inputDtoB1.B1OwnerAdd_1_B,
                inputDtoB1.B1OwnerAdd_1_1_B,
                inputDtoB1.B1OwnerAdd_1_2_B,
                inputDtoB1.B1OwnerAdd_2_B,
                inputDtoB1.B1OwnerAdd_2_1_B,
                inputDtoB1.B1OwnerAdd_2_2_B,
                inputDtoB1.B1OwnerAdd_2_3_B,
                inputDtoB1.B1OwnerAdd_2_4_B,
                inputDtoB1.B1OwnerAdd_3_B,
                inputDtoB1.B1OwnerAdd_3_1_B,
                inputDtoB1.B1OwnerAdd_3_2_B,
                inputDtoB1.B1OwnerAdd_4_B,
                inputDtoB1.B1OwnerAdd_5_B,
                inputDtoB1.B1OwnerAdd_6_B,
                inputDtoB1.B1OwnerAdd_7_B,
                inputDtoB1.B1OwnerAdd_8_B,
                inputDtoB1.B1OwnerAdd_9_B);
            sourceB1.B1OwnerAdd_C = Utils.memergeAddFix(
                inputDtoB1.B1OwnerAdd_1_C,
                inputDtoB1.B1OwnerAdd_1_1_C,
                inputDtoB1.B1OwnerAdd_1_2_C,
                inputDtoB1.B1OwnerAdd_2_C,
                inputDtoB1.B1OwnerAdd_2_1_C,
                inputDtoB1.B1OwnerAdd_2_2_C,
                inputDtoB1.B1OwnerAdd_2_3_C,
                inputDtoB1.B1OwnerAdd_2_4_C,
                inputDtoB1.B1OwnerAdd_3_C,
                inputDtoB1.B1OwnerAdd_3_1_C,
                inputDtoB1.B1OwnerAdd_3_2_C,
                inputDtoB1.B1OwnerAdd_4_C,
                inputDtoB1.B1OwnerAdd_5_C,
                inputDtoB1.B1OwnerAdd_6_C,
                inputDtoB1.B1OwnerAdd_7_C,
                inputDtoB1.B1OwnerAdd_8_C,
                inputDtoB1.B1OwnerAdd_9_C);
            sourceB1.B1OwnerAdd_D = Utils.memergeAddFix(
                inputDtoB1.B1OwnerAdd_1_D,
                inputDtoB1.B1OwnerAdd_1_1_D,
                inputDtoB1.B1OwnerAdd_1_2_D,
                inputDtoB1.B1OwnerAdd_2_D,
                inputDtoB1.B1OwnerAdd_2_1_D,
                inputDtoB1.B1OwnerAdd_2_2_D,
                inputDtoB1.B1OwnerAdd_2_3_D,
                inputDtoB1.B1OwnerAdd_2_4_D,
                inputDtoB1.B1OwnerAdd_3_D,
                inputDtoB1.B1OwnerAdd_3_1_D,
                inputDtoB1.B1OwnerAdd_3_2_D,
                inputDtoB1.B1OwnerAdd_4_D,
                inputDtoB1.B1OwnerAdd_5_D,
                inputDtoB1.B1OwnerAdd_6_D,
                inputDtoB1.B1OwnerAdd_7_D,
                inputDtoB1.B1OwnerAdd_8_D,
                inputDtoB1.B1OwnerAdd_9_D);
            sourceB1.B1OwnerAdd_E = Utils.memergeAddFix(
                inputDtoB1.B1OwnerAdd_1_E,
                inputDtoB1.B1OwnerAdd_1_1_E,
                inputDtoB1.B1OwnerAdd_1_2_E,
                inputDtoB1.B1OwnerAdd_2_E,
                inputDtoB1.B1OwnerAdd_2_1_E,
                inputDtoB1.B1OwnerAdd_2_2_E,
                inputDtoB1.B1OwnerAdd_2_3_E,
                inputDtoB1.B1OwnerAdd_2_4_E,
                inputDtoB1.B1OwnerAdd_3_E,
                inputDtoB1.B1OwnerAdd_3_1_E,
                inputDtoB1.B1OwnerAdd_3_2_E,
                inputDtoB1.B1OwnerAdd_4_E,
                inputDtoB1.B1OwnerAdd_5_E,
                inputDtoB1.B1OwnerAdd_6_E,
                inputDtoB1.B1OwnerAdd_7_E,
                inputDtoB1.B1OwnerAdd_8_E,
                inputDtoB1.B1OwnerAdd_9_E);
            #endregion 其他所有權人戶籍地址
            return sourceB1;
        }

    }
}