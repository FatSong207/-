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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq;
using Yuebon.Commons.Extensions;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Chaochi.Core.IServices;
using System.Text;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;
using IUserService = Yuebon.Security.IServices.IUserService;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 潛在客管理接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class PotentialCustomersController : AreaApiController<PotentialCustomers, PotentialCustomersOutputDto,PotentialCustomersInputDto,IPotentialCustomersService,string>
    {
        private readonly ICustomerLCService lcService;
        private readonly ICustomerLNService lnService;
        private readonly ILandLordBelongingService landLordBelongingService;
        private readonly ICustomerRNService rnService;
        private readonly ICustomerRCService rcService;
        private readonly IBuildingService buildingService;
        private readonly IBuilding1Service building1Service;
        private readonly IBuildingAdvertisementService buildingAdvertisementService;
        private readonly IVisitingRecordService vrService;
        private readonly IDbContextCore ybContext;
        private readonly IOrganizeService orgService;
        private readonly IUserService userService;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_lcService"></param>
        /// <param name="_lnService"></param>
        /// <param name="_landLordBelongingService"></param>
        /// <param name="_rnService"></param>
        /// <param name="_rcService"></param>
        /// <param name="_buildingService"></param>
        /// <param name="_vrService"></param>
        /// <param name="_building1Service"></param>
        /// <param name="_buildingAdvertisementService"></param>
        /// <param name="_ybContext"></param>
        /// <param name="_orgService"></param>
        /// <param name="_userService"></param>
        public PotentialCustomersController(
            IPotentialCustomersService _iService, 
            ICustomerLCService _lcService, 
            ICustomerLNService _lnService,
            ILandLordBelongingService _landLordBelongingService,
            ICustomerRNService _rnService,
            ICustomerRCService _rcService,
            IBuildingService _buildingService, 
            IBuilding1Service _building1Service,
            IBuildingAdvertisementService _buildingAdvertisementService,
            IVisitingRecordService _vrService, 
            IDbContextCore _ybContext,
            IOrganizeService _orgService,
            IUserService _userService) : base(_iService)
        {
            lcService = _lcService;
            lnService = _lnService;
            landLordBelongingService = _landLordBelongingService;
            rnService = _rnService;
            rcService = _rcService;
            buildingService = _buildingService;
            building1Service = _building1Service;
            buildingAdvertisementService = _buildingAdvertisementService;
            iService = _iService;
            vrService = _vrService;
            ybContext = _ybContext;
            orgService = _orgService;
            userService = _userService;
        }

        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(PotentialCustomers info)
        {
            //info.Id = GuidUtils.CreateNo();
            // 潛在客編號:出租人/承租人+手機號碼＋所屬店
            info.PID = GeneratePID(info);
            // 未結案
            info.IsClosed = "否";
            // 未轉正
            info.IsTransfer = "否";
            // 狀態
            info.Status = "新建立";
            //// 
            //info.ReportBackCounts = 0;
            // 產生日期(民國年)
            info.CreateDate = DateTime.Now.AddYears(-1911).ToString("yyy-MM-dd");          
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
        protected override void OnBeforeUpdate(PotentialCustomers info)
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
        public override async Task<IActionResult> InsertAsync(PotentialCustomersInputDto input)
        {
            CommonResult result = new CommonResult();

            PotentialCustomers info = input.MapTo<PotentialCustomers>();

            OnBeforeInsert(info);

            if (!string.IsNullOrEmpty(input.Cell)) {
                if (input.Cell.Length == 10) {
                    info.Cell = info.Cell;
                }
            }

            bool checkCellExist = await IsCellExist(info);
            if (checkCellExist) {
                result.Success = false;
                result.ErrMsg = $"{info.Area_2}已有手機({info.Cell})資料";
                return ToJsonContent(result);
            } 

            // 指派業務
            if ("Y".Equals(input.HavePrivilege)) {
                info.Sales = CurrentUser.Account;
            } else {
                info.Sales = string.Empty;
            }

            // 電話
            info.Tel = input.Tel_1 + "-" + input.Tel_2;

            // 房屋戶籍地址
            if ("承租人".Equals(info.Identity)) {
                info.RAdd = Utils.memergeAddFix(
                        info.RAdd_1,
                        info.RAdd_1_1,
                        info.RAdd_1_2,
                        info.RAdd_2,
                        info.RAdd_2_1,
                        info.RAdd_2_2,
                        info.RAdd_2_3,
                        info.RAdd_2_4,
                        info.RAdd_3,
                        info.RAdd_3_1,
                        info.RAdd_3_2,
                        info.RAdd_4,
                        info.RAdd_5,
                        info.RAdd_6,
                        info.RAdd_7,
                        info.RAdd_8,
                        info.RAdd_9
                    );
            }

            // 出租房屋地址
            if ("出租人".Equals(info.Identity)) {
                info.BAdd = Utils.memergeAddFix(
                        info.BAdd_1,
                        info.BAdd_1_1,
                        info.BAdd_1_2,
                        info.BAdd_2,
                        info.BAdd_2_1,
                        info.BAdd_2_2,
                        info.BAdd_2_3,
                        info.BAdd_2_4,
                        info.BAdd_3,
                        info.BAdd_3_1,
                        info.BAdd_3_2,
                        info.BAdd_4,
                        info.BAdd_5,
                        info.BAdd_6,
                        info.BAdd_7,
                        info.BAdd_8,
                        info.BAdd_9
                    );
            }

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {                
                long pcCount = await iService.InsertAsync(info, conn, tran).ConfigureAwait(false);
                //long vrCount = 0;

                // 拜訪記錄
                //List<VisitingRecordInputDto> vrList = input.VisitingRecordList;
                //if (vrList.Count > 0) {
                //    foreach (VisitingRecordInputDto recordInput in vrList) {
                //        VisitingRecord vrInfo = recordInput.MapTo<VisitingRecord>();
                //        vrInfo.CreatorTime = CreatorTime;
                //        vrInfo.CreatorUserId = CreatorUserId;

                //        vrCount += await vrService.InsertAsync(vrInfo, conn, tran).ConfigureAwait(false);
                //    }
                //}

                //if (pcCount > 0 && vrCount > 0) {
                if (pcCount > 0) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    eftran.Rollback();
                    result.ErrMsg = "新增潛在客單失敗";
                    result.ErrCode = ErrCode.err43001;
                    result.Success = false;
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("新增潛在客失敗", ex);                
                result.ErrMsg = "新增潛在客失敗";
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
        public override async Task<IActionResult> UpdateAsync(PotentialCustomersInputDto input)
        {
            CommonResult result = new CommonResult();

            string CreatorUserId = CurrentUser?.UserId;
            DateTime CreatorTime = DateTime.Now;

            PotentialCustomers PC_DB = iService.Get(input.Id);

            bool sameCell = false;
            if (!string.IsNullOrEmpty(input.Cell)) {
                if (input.Cell.Length == 10) {                    
                    sameCell = PC_DB.Cell.Equals(input.Cell);
                }                
            }

            PotentialCustomers info = input.MapTo(PC_DB);
            OnBeforeUpdate(info);

            if (!sameCell) {
                bool checkCellExist = await IsCellExist(info);
                if (checkCellExist) {
                    result.Success = false;
                    result.ErrMsg = $"{info.Area_2}已有手機({info.Cell})資料";
                    return ToJsonContent(result);
                }
            }

            // 電話
            info.Tel = input.Tel_1 + "-" + input.Tel_2;

            // 房屋戶籍地址
            if ("承租人".Equals(info.Identity)) {
                info.RAdd = Utils.memergeAddFix(
                        info.RAdd_1,
                        info.RAdd_1_1,
                        info.RAdd_1_2,
                        info.RAdd_2,
                        info.RAdd_2_1,
                        info.RAdd_2_2,
                        info.RAdd_2_3,
                        info.RAdd_2_4,
                        info.RAdd_3,
                        info.RAdd_3_1,
                        info.RAdd_3_2,
                        info.RAdd_4,
                        info.RAdd_5,
                        info.RAdd_6,
                        info.RAdd_7,
                        info.RAdd_8,
                        info.RAdd_9
                    );
            }

            // 出租房屋地址
            if ("出租人".Equals(info.Identity)) {
                info.BAdd = Utils.memergeAddFix(
                        info.BAdd_1,
                        info.BAdd_1_1,
                        info.BAdd_1_2,
                        info.BAdd_2,
                        info.BAdd_2_1,
                        info.BAdd_2_2,
                        info.BAdd_2_3,
                        info.BAdd_2_4,
                        info.BAdd_3,
                        info.BAdd_3_1,
                        info.BAdd_3_2,
                        info.BAdd_4,
                        info.BAdd_5,
                        info.BAdd_6,
                        info.BAdd_7,
                        info.BAdd_8,
                        info.BAdd_9
                    );
            }

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {

                bool updateResult = await iService.UpdateAsync(info, input.Id, conn, tran).ConfigureAwait(false);

                if (updateResult == true) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    result.ErrMsg = "更新潛在客失敗";
                    result.ErrCode = ErrCode.err43002;
                    result.Success = false;
                }

            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("更新潛在客失敗", ex);
                result.ErrMsg = "更新潛在客失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 指派業務
        /// 
        /// </summary>
        /// <param name="input">潛在客編號名單</param>
        /// <returns></returns> 
        [HttpPost("AssignSales")]
        [YuebonAuthorize("StoreManager")]
        public async Task<IActionResult> AssignSales(PotentialCustomersInputDto input)
        {
            CommonResult result = new CommonResult();
            bool updateResult = false;

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                if (input.Ids.Length > 0) {
                    foreach (string pid in input.Ids) {
                        updateResult = await iService.AssignSales(CurrentUser.UserId, pid, input.Sales, conn, tran).ConfigureAwait(false);
                    }
                }

                if (updateResult) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    result.ErrMsg = "指派業務失敗";
                    result.ErrCode = ErrCode.err43002;
                    result.Success = false;
                }

            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("指派業務失敗", ex);
                result.ErrMsg = "指派業務失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 轉店
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("CopyPotentialCustomerData")]
        [YuebonAuthorize("StoreManager")]
        public async Task<IActionResult> CopyPotentialCustomerData(PotentialCustomersInputDto input)
        {
            CommonResult result = new CommonResult();
            StringBuilder msg = new StringBuilder();
            // A店轉B店
            // 情況一：B店沒有相同手機，則把潛在客基本資料帶過去，拜訪記錄清除、業務資料清除，A店該筆資料仍然保存
            // 情況二：B店有相同手機，則不覆蓋B店資料，A店該筆資料仍然保存
            // 轉店沒有次數等於複製
            // 如果兩家店都保存，到時候就看誰先轉正式客戶，建物就是誰的
            if (input.Ids.Length > 0) {
                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();

                for (var i = 0; i < input.Ids.Length; i++) {
                    string pid = input.Ids[i];
                    string storeName = input.AArea_2;
                    string cell = input.Cells[i];
                    string allowCopy = input.AllowCopy[i];
                    bool isStoreExist =  await iService.IsStoreRecordExist(storeName, cell);
                    if (allowCopy == "N") {
                        // 不能複製己結案或己轉正的潛在客資料
                        msg.AppendLine($"{pid}己結案或己轉正，無法進行轉店");
                        msg.AppendLine();
                    } else if (isStoreExist) {
                        // 情況二：B店有相同手機，則不覆蓋B店資料，A店該筆資料仍然保存
                        msg.AppendLine($"分店：{storeName}己有重複資料{cell}");
                        msg.AppendLine();
                        //result.ErrCode = ErrCode.err50002;
                        //result.Success = false;

                        //return ToJsonContent(result);                        
                    } else {
                        // 情況一：B店沒有相同手機，則把潛在客基本資料帶過去，拜訪記錄清除、業務資料清除，A店該筆資料仍然保存                                                   
                        PotentialCustomersOutputDto dbInfo = await iService.GetByPID(pid);
                        if (dbInfo != null) {
                            PotentialCustomers info = new PotentialCustomers();

                            info.Identity = dbInfo.Identity;
                            info.Identity2 = dbInfo.Identity2;                            
                            info.Source = dbInfo.Source;
                            info.Area = input.AArea_2;
                            info.Area_1 = string.IsNullOrEmpty(input.AArea_1) ? String.Empty : input.AArea_1;
                            info.Area_2 = input.AArea_2;
                            info.HouseType = dbInfo.HouseType;
                            info.HouseInterior = dbInfo.HouseInterior;
                            info.Name = dbInfo.Name;
                            info.Tel = dbInfo.Tel;
                            info.Tel_1 = dbInfo.Tel_1;
                            info.Tel_2 = dbInfo.Tel_2;
                            info.Cell = dbInfo.Cell;
                            info.CountyCity = dbInfo.CountyCity;
                            info.ResidentCount = dbInfo.ResidentCount;
                            info.RAdd = dbInfo.RAdd;
                            info.RAdd_1 = dbInfo.RAdd_1;
                            info.RAdd_1_1 = dbInfo.RAdd_1_1;
                            info.RAdd_1_2 = dbInfo.RAdd_1_2;
                            info.RAdd_2 = dbInfo.RAdd_2;
                            info.RAdd_2_1 = dbInfo.RAdd_2_1;
                            info.RAdd_2_2 = dbInfo.RAdd_2_2;
                            info.RAdd_2_3 = dbInfo.RAdd_2_3;
                            info.RAdd_2_4 = dbInfo.RAdd_2_4;
                            info.RAdd_3 = dbInfo.RAdd_3;
                            info.RAdd_3_1 = dbInfo.RAdd_3_1;
                            info.RAdd_3_2 = dbInfo.RAdd_3_2;
                            info.RAdd_4 = dbInfo.RAdd_4;
                            info.RAdd_5 = dbInfo.RAdd_5;
                            info.RAdd_6 = dbInfo.RAdd_6;
                            info.RAdd_7 = dbInfo.RAdd_7;
                            info.RAdd_8 = dbInfo.RAdd_8;
                            info.RAdd_9 = dbInfo.RAdd_9;
                            info.BAdd = dbInfo.BAdd;
                            info.BAdd_1 = dbInfo.BAdd_1;
                            info.BAdd_1_1 = dbInfo.BAdd_1_1;
                            info.BAdd_1_2 = dbInfo.BAdd_1_2;
                            info.BAdd_2 = dbInfo.BAdd_2;
                            info.BAdd_2_1 = dbInfo.BAdd_2_1;
                            info.BAdd_2_2 = dbInfo.BAdd_2_2;
                            info.BAdd_2_3 = dbInfo.BAdd_2_3;
                            info.BAdd_2_4 = dbInfo.BAdd_2_4;
                            info.BAdd_3 = dbInfo.BAdd_3;
                            info.BAdd_3_1 = dbInfo.BAdd_3_1;
                            info.BAdd_3_2 = dbInfo.BAdd_3_2;
                            info.BAdd_4 = dbInfo.BAdd_4;
                            info.BAdd_5 = dbInfo.BAdd_5;
                            info.BAdd_6 = dbInfo.BAdd_6;
                            info.BAdd_7 = dbInfo.BAdd_7;
                            info.BAdd_8 = dbInfo.BAdd_8;
                            info.BAdd_9 = dbInfo.BAdd_9;
                            info.BPropoty = dbInfo.BPropoty;
                            info.ExpectRent = dbInfo.ExpectRent;
                            info.AnticipateRent = dbInfo.AnticipateRent;
                            info.HavePet = dbInfo.HavePet;
                            info.AllowPet = dbInfo.AllowPet;
                            info.Note = dbInfo.Note;
                            info.Sales = string.Empty;
                            info.ReportBackCounts = string.Empty;
                            OnBeforeInsert(info);
                            Organize orgInfo = await orgService.GetByName(info.Area);
                            info.CreatorUserDeptId = (orgInfo != null) ? orgInfo.Id : string.Empty;
                            string storeManager = (orgInfo != null) ? userService.GetManagerByDepartmentId(orgInfo.Id) : string.Empty;
                            info.CreatorUserId = (!string.IsNullOrEmpty(storeManager)) ? storeManager : string.Empty;

                            try {
                                long pcCount = await iService.InsertAsync(info, conn, tran).ConfigureAwait(false);

                                if (pcCount > 0) {
                                    msg.Append($"分店資料：{storeName}複製成功");
                                    msg.AppendLine();
                                    eftran.Commit();
                                    //result.ErrCode = ErrCode.successCode;
                                    //result.ErrMsg = msg.ToString();
                                    //result.Success = true;
                                } else {
                                    //result.ErrMsg = "轉店時新增潛在客失敗";
                                    //result.ErrCode = ErrCode.err43001;
                                    //result.Success = false;
                                    msg.Append($"轉店時新增潛在客失敗：{pid}");
                                    msg.AppendLine();
                                }
                            } catch (Exception ex) {
                                eftran.Rollback();
                                Log4NetHelper.Error("轉店時新增潛在客失敗", ex);
                                result.ErrMsg = "轉店時新增潛在客失敗";
                                result.ErrCode = ErrCode.err43001;
                                result.Success = false;
                                // 觸發ExceptionHandlingAttribute.OnException
                                throw;
                            }
                        }
                    }
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = msg.ToString();
                    result.Success = true;
                }
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步批次物理刪除
        /// </summary>
        /// <param name="input"></param>
        [HttpDelete("DeleteBatchAsync")]
        //[YuebonAuthorize("Delete")]
        [YuebonAuthorize("StoreManager")]
        public override async Task<IActionResult> DeleteBatchAsync(Commons.Core.Dtos.DeletesInputDto input)
        {
            CommonResult result = new CommonResult();
            //string where = "id in ('" + input.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            string where = "PID in ('" + input.Ids.Join(",").Trim(',').Replace(",", "','") + "')";

            if (!string.IsNullOrEmpty(where)) {
                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();

                try {
                    bool vrResult = await vrService.DeleteBatchWhereAsync(where, conn, tran).ConfigureAwait(false);

                    bool pcResult = await iService.DeleteBatchWhereAsync(where, conn, tran).ConfigureAwait(false);

                    if (vrResult && pcResult) {
                        eftran.Commit();

                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;
                    } else {
                        result.ErrCode = "43003";
                        result.ErrMsg = "刪除潛在客失敗";
                        result.Success = false;
                    }
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("刪除潛在客失敗", ex);
                    result.ErrMsg = "刪除潛在客失敗";
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
        [HttpDelete("DeleteVRBatchAsync")]
        [YuebonAuthorize("Delete")]
        public async Task<IActionResult> DeleteVRBatchAsync(DeleteInputDto input)
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
                    bool vrResult = await vrService.DeleteBatchWhereAsync(where, conn, tran).ConfigureAwait(false);

                    if (vrResult) {
                        eftran.Commit();

                        string where2 = "PID = '" + input.Id + "'";
                        // 回報次數
                        int vrCount = await vrService.GetCountByWhereAsync(where2, "*", conn, tran);
                        // 最近訪談記錄的當前狀態
                        VisitingRecordOutputDto vrODto = await vrService.GetLatestByPID(input.Id);

                        // 如果有刪除的事實，刪去的序號為最新，則刪除成功後，要將剩餘序號最新的狀態，一併更新回列表頁
                        bool statusUpdateResult = false;
                        if (vrODto != null) {
                            // 更新回報狀態
                            statusUpdateResult = await iService.UpdatePCStatus(CurrentUser.UserId, vrODto.PID, vrODto.Status, conn, tran);
                        }

                        bool RBCountUpdateResult = false;
                        if (vrCount > 0) {
                            RBCountUpdateResult = await iService.UpdatePCReportBackCounts(CurrentUser.UserId, vrODto.PID, vrCount, conn, tran);
                        }

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

        /// <summary>
        /// 根據潛在客編號查詢潛在客資訊
        /// </summary>
        /// <param name="input"></param>
        [HttpPost("GetByPID")]
        [YuebonAuthorize("List")]
        public async Task<CommonResult<PotentialCustomersOutputDto>> GetByPID(PotentialCustomersInputDto input)
        {
            CommonResult<PotentialCustomersOutputDto> result = new CommonResult<PotentialCustomersOutputDto>();
            PotentialCustomersOutputDto info = await iService.GetByPID(input.PID);

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
        /// 檢查隸屬店下是否有重複手機號碼
        /// </summary>
        /// <param name="info"></param>
        private async Task<bool> IsCellExist(PotentialCustomers info)
        {
            bool result = false;

            //string where = string.Format("PID = '{0}' and Area_2 = '{1}' and Cell = '{2}'", info.PID, info.Area_2, info.Cell);
            string where = string.Format("Area_2 = '{0}' and Cell = '{1}'", info.Area_2, info.Cell);
            PotentialCustomers pcs = await iService.GetWhereAsync(where);
            PotentialCustomersOutputDto pcsOutputDto = pcs.MapTo<PotentialCustomersOutputDto>();

            if (pcsOutputDto != null) {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 潛在客轉正式客戶
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("TransferClient")]
        [YuebonAuthorize("Add")]
        public async Task<IActionResult> TransferClient(PotentialCustomersInputDto input)
        {
            CommonResult result = new CommonResult();

            string CreatorUserId = CurrentUser?.UserId;
            DateTime CreatorTime = DateTime.Now;
          
            string name = string.Empty;
            string address = string.Empty;
            string[] addressArray = new string[17];
            bool updateResult = false;
            bool transferUpdateResult = false;

            string errMsg = string.Empty;

            var lnInfos = await lnService.GetAllAsync();
            var lnIDList = lnInfos.Select(o => o.LNID).ToArray();
            var lnUserIDList = lnInfos.Select(o => o.CreatorUserId).ToArray();
            var lcInfos = await lcService.GetAllAsync();
            var lcIDList = lcInfos.Select(o => o.LCID).ToArray();
            var lcUserIDList = lcInfos.Select(o => o.CreatorUserId).ToArray();
            var rnInfos = await rnService.GetAllAsync();
            var rnIDList = rnInfos.Select(o => o.RNID).ToArray();
            var rcInfos = await rcService.GetAllAsync();
            var rcIDList = rcInfos.Select(o => o.RCID).ToArray();
            var rnUserIDList = rnInfos.Select(o => o.CreatorUserId).ToArray();
            var rcUserIDList = rcInfos.Select(o => o.CreatorUserId).ToArray();
            var bBuilding = await buildingService.GetAllAsync();
            var bBAddList = bBuilding.Select(o => o.BAdd).ToArray();

            input.BAdd = Utils.memergeAddFix(
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

            bool bIsExist = bBAddList.Contains(input.BAdd);

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();
            string[] baddArray = new string[17];

            try {
                switch (input.Identity) {
                    case "出租人":
                        baddArray[0] = input.BAdd_1;
                        baddArray[1] = input.BAdd_1_1;
                        baddArray[2] = input.BAdd_1_2;
                        baddArray[3] = input.BAdd_2;
                        baddArray[4] = input.BAdd_2_1;
                        baddArray[6] = input.BAdd_2_3;
                        baddArray[7] = input.BAdd_2_4;
                        baddArray[8] = input.BAdd_3;
                        baddArray[9] = input.BAdd_3_1;
                        baddArray[10] = input.BAdd_3_2;
                        baddArray[11] = input.BAdd_4;
                        baddArray[12] = input.BAdd_5;
                        baddArray[13] = input.BAdd_6;
                        baddArray[14] = input.BAdd_7;
                        baddArray[15] = input.BAdd_8;
                        baddArray[16] = input.BAdd_9;

                        if ("自然人".Equals(input.Identity2)) {
                            string LNID = input.LRID;
                            if (lnIDList.Contains(LNID)) {
                                if (!string.IsNullOrEmpty(input.BAdd) && !bIsExist) {
                                    // 如果房東已存在但建物不存在，且該業務已經有該房東的存取權，就能新增建物
                                    if (Array.Exists(lnUserIDList, element => element.Contains(input.Sales))) {
                                        // 同步新增建物
                                        updateResult = await BuildingInsertAsync(input, LNID, tran);
                                    } else {
                                        //如果房東已存在但建物不存在（表示拿到房東的另一間新建物而新增，這時要提醒業務先到請求存取權的頁面去取得存取權），但不算已轉正
                                        result.Success = false;
                                        result.ErrMsg = "此身份證字號/居留證號已存在，請到請求存取權頁面取得存取權";
                                        return ToJsonContent(result);
                                    }
                                } else {
                                    result.Success = false;
                                    result.ErrMsg = "此房東己擁有此建物";
                                    return ToJsonContent(result);
                                }
                            } else {
                                CustomerLN info = new CustomerLN();
                                info.LNID = LNID;
                                info.LNName = input.Name;

                                //電話
                                info.LNTel = input.Tel;
                                info.LNTel_1 = input.Tel_1;
                                info.LNTel_2 = input.Tel_2;

                                // 手機
                                if (!string.IsNullOrEmpty(input.Cell)) {
                                    if (input.Cell.Length == 10 ) {
                                        info.LNCell = input.Cell;       
                                    }
                                }

                                // 身分證字號
                                var idSplit = LNID.ToCharArray();
                                info.LNID_1_1 = idSplit[0].ToString();
                                info.LNID_1_2 = idSplit[1].ToString();
                                info.LNID_1_3 = idSplit[2].ToString();
                                info.LNID_1_4 = idSplit[3].ToString();
                                info.LNID_1_5 = idSplit[4].ToString();
                                info.LNID_1_6 = idSplit[5].ToString();
                                info.LNID_1_7 = idSplit[6].ToString();
                                info.LNID_1_8 = idSplit[7].ToString();
                                info.LNID_1_9 = idSplit[8].ToString();
                                info.LNID_1_10 = idSplit[9].ToString();

                                //// 地址
                                //if (!string.IsNullOrEmpty(input.BAdd)) {
                                //    info.LNAdd = input.BAdd;
                                //    info.LNAdd_1 = input.BAdd_1;
                                //    info.LNAdd_1_1 = input.BAdd_1_1;
                                //    info.LNAdd_1_2 = input.BAdd_1_2;
                                //    info.LNAdd_2 = input.BAdd_2;
                                //    info.LNAdd_2_1 = input.BAdd_2_1;
                                //    info.LNAdd_2_2 = input.BAdd_2_2;
                                //    info.LNAdd_2_3 = input.BAdd_2_3;
                                //    info.LNAdd_2_4 = input.BAdd_2_4;    
                                //    info.LNAdd_3 = input.BAdd_3;
                                //    info.LNAdd_3_1 = input.BAdd_3_1;    
                                //    info.LNAdd_3_2 = input.BAdd_3_2;
                                //    info.LNAdd_4 = input.BAdd_4;
                                //    info.LNAdd_5 = input.BAdd_5;    
                                //    info.LNAdd_6 = input.BAdd_6;
                                //    info.LNAdd_7 = input.BAdd_7;
                                //    info.LNAdd_8 = input.BAdd_8;
                                //    info.LNAdd_9 = input.BAdd_9;
                                //}

                                info.CreatorTime = CreatorTime;
                                info.CreatorUserId = CreatorUserId;
                                info.CreatorUserOrgId = CurrentUser.OrganizeId;
                                info.CreatorUserDeptId = CurrentUser.DeptId;
                                landLordBelongingService.Insert(new LandLordBelonging
                                {
                                    LId = info.LNID,
                                    SalesId = CurrentUser.UserId,
                                });

                                long ln = await lnService.InsertAsync(info, conn, tran).ConfigureAwait(false);
                                updateResult = ln > 0;

                                // 建物
                                if (!string.IsNullOrEmpty(input.BAdd) && !bIsExist) {
                                    // 同步新增建物
                                    updateResult = await BuildingInsertAsync(input, LNID, tran);
                                }
                            }
                        } else {
                            string LCID = input.LRID;
                            if (lcIDList.Contains(LCID)) {
                                if (!string.IsNullOrEmpty(input.BAdd) && !bIsExist) {
                                    // 如果房東已存在但建物不存在，且該業務已經有該房東的存取權，就能新增建物
                                    if (Array.Exists(lcUserIDList, element => element.Contains(input.Sales))) {
                                        updateResult = await BuildingInsertAsync(input, LCID, tran);
                                    } else {
                                    //如果房東已存在但建物不存在（表示拿到房東的另一間新建物而新增，這時要提醒業務先到請求存取權的頁面去取得存取權），但不算已轉正
                                    result.Success = false;
                                    result.ErrMsg = "此統一編號已存在，請到請求存取權頁面取得存取權";
                                    return ToJsonContent(result);
                                    }
                                } else {
                                    result.Success = false;
                                    result.ErrMsg = "此房東己擁有此建物";
                                    return ToJsonContent(result);
                                }
                            } else {
                                CustomerLC info = new CustomerLC();

                                info.LCID = LCID;
                                info.LCName = input.Name;

                                // 電話
                                info.LCTel = input.Tel;
                                info.LCTel_1 = input.Tel_1;
                                info.LCTel_2 = input.Tel_2;

                                // 統一編號
                                var idSplit = LCID.ToCharArray();
                                info.LCID_1_1 = idSplit[0].ToString();
                                info.LCID_1_2 = idSplit[1].ToString();
                                info.LCID_1_3 = idSplit[2].ToString();
                                info.LCID_1_4 = idSplit[3].ToString();
                                info.LCID_1_5 = idSplit[4].ToString();
                                info.LCID_1_6 = idSplit[5].ToString();
                                info.LCID_1_7 = idSplit[6].ToString();
                                info.LCID_1_8 = idSplit[7].ToString();

                                // 地址
                                if (!string.IsNullOrEmpty(input.BAdd)) {
                                    info.LCAdd = input.BAdd;
                                    info.LCAdd_1 = input.BAdd_1;
                                    info.LCAdd_1_1 = input.BAdd_1_1;
                                    info.LCAdd_1_2 = input.BAdd_1_2;
                                    info.LCAdd_2 = input.BAdd_2;
                                    info.LCAdd_2_1 = input.BAdd_2_1;
                                    info.LCAdd_2_2 = input.BAdd_2_2;
                                    info.LCAdd_2_3 = input.BAdd_2_3;
                                    info.LCAdd_2_4 = input.BAdd_2_4;
                                    info.LCAdd_3 = input.BAdd_3;
                                    info.LCAdd_3_1 = input.BAdd_3_1;
                                    info.LCAdd_3_2 = input.BAdd_3_2;
                                    info.LCAdd_4 = input.BAdd_4;
                                    info.LCAdd_5 = input.BAdd_5;
                                    info.LCAdd_6 = input.BAdd_6;
                                    info.LCAdd_7 = input.BAdd_7;
                                    info.LCAdd_8 = input.BAdd_8;
                                    info.LCAdd_9 = input.BAdd_9;
                                }

                                info.CreatorTime = CreatorTime;
                                info.CreatorUserId = CreatorUserId;
                                info.CreatorUserOrgId = CurrentUser.OrganizeId;
                                info.CreatorUserDeptId = CurrentUser.DeptId;
                                landLordBelongingService.Insert(new LandLordBelonging
                                {
                                    LId = info.LCID,
                                    SalesId = CurrentUser.UserId,
                                });

                                // 建物
                                if (!string.IsNullOrEmpty(input.BAdd) && !bIsExist) {
                                    // 同步新增建物
                                    updateResult = await BuildingInsertAsync(input, LCID, tran);
                                }
                            }
                        }
                    break;
                case "承租人":
                    //房客已存在，且該業務已經有該房客的存取權（反之秀出業務先到請求存取權的頁面去取得存取權）                    
                    baddArray = new string[17];
                    baddArray[0] = input.RAdd_1;
                    baddArray[1] = input.RAdd_1_1;
                    baddArray[2] = input.RAdd_1_2;
                    baddArray[3] = input.RAdd_2;
                    baddArray[4] = input.RAdd_2_1;
                    baddArray[6] = input.RAdd_2_3;
                    baddArray[7] = input.RAdd_2_4;
                    baddArray[8] = input.RAdd_3;
                    baddArray[9] = input.RAdd_3_1;
                    baddArray[10] = input.RAdd_3_2;
                    baddArray[11] = input.RAdd_4;
                    baddArray[12] = input.RAdd_5;
                    baddArray[13] = input.RAdd_6;
                    baddArray[14] = input.RAdd_7;
                    baddArray[15] = input.RAdd_8;
                    baddArray[16] = input.RAdd_9;

                    if ("自然人".Equals(input.Identity2)) {
                            string RNID = input.LRID;
                            if (rnIDList.Contains(RNID)) {
                                if (Array.Exists(rnUserIDList, element => element.Contains(RNID))) {
                                    updateResult = true;
                                } else {
                                    result.Success = false;
                                    result.ErrMsg = "此身份證字號/居留證號已存在，請到請求存取權頁面取得存取權";
                                    return ToJsonContent(result);
                                }
                            } else {
                                //房客不存在（所以新增）
                                CustomerRN info = new CustomerRN();

                                info.RNID = RNID;
                                info.RNName = input.Name;

                                //電話
                                info.RNTel = input.Tel;
                                info.RNTel_1 = input.Tel_1;
                                info.RNTel_2 = input.Tel_2;

                                //手機
                                if (!string.IsNullOrEmpty(input.Cell)) {
                                    if (input.Cell.Length == 10) {
                                        info.RNCell = input.Cell;
                                    }
                                }

                                //身分證字號
                                var idSplit = RNID.ToCharArray();
                                info.RNID_1_1 = idSplit[0].ToString();
                                info.RNID_1_2 = idSplit[1].ToString();
                                info.RNID_1_3 = idSplit[2].ToString();
                                info.RNID_1_4 = idSplit[3].ToString();
                                info.RNID_1_5 = idSplit[4].ToString();
                                info.RNID_1_6 = idSplit[5].ToString();
                                info.RNID_1_7 = idSplit[6].ToString();
                                info.RNID_1_8 = idSplit[7].ToString();
                                info.RNID_1_9 = idSplit[8].ToString();
                                info.RNID_1_10 = idSplit[9].ToString();

                                input.RAdd = Utils.memergeAddFix(
                                    input.RAdd_1,
                                    input.RAdd_1_1,
                                    input.RAdd_1_2,
                                    input.RAdd_2,
                                    input.RAdd_2_1,
                                    input.RAdd_2_2,
                                    input.RAdd_2_3,
                                    input.RAdd_2_4,
                                    input.RAdd_3,
                                    input.RAdd_3_1,
                                    input.RAdd_3_2,
                                    input.RAdd_4,
                                    input.RAdd_5,
                                    input.RAdd_6,
                                    input.RAdd_7,
                                    input.RAdd_8,
                                    input.RAdd_9
                                );

                                // 戶籍地址
                                if (!string.IsNullOrEmpty(input.RAdd)) {
                                    info.RNAdd = input.RAdd;
                                    info.RNAdd_1 = input.RAdd_1;
                                    info.RNAdd_1_1 = input.RAdd_1_1;
                                    info.RNAdd_1_2 = input.RAdd_1_2;
                                    info.RNAdd_2 = input.RAdd_2;
                                    info.RNAdd_2_1 = input.RAdd_2_1;
                                    info.RNAdd_2_2 = input.RAdd_2_2;
                                    info.RNAdd_2_3 = input.RAdd_2_3;
                                    info.RNAdd_2_4 = input.RAdd_2_4;
                                    info.RNAdd_3 = input.RAdd_3;
                                    info.RNAdd_3_1 = input.RAdd_3_1;
                                    info.RNAdd_3_2 = input.RAdd_3_2;
                                    info.RNAdd_4 = input.RAdd_4;
                                    info.RNAdd_5 = input.RAdd_5;
                                    info.RNAdd_6 = input.RAdd_6;
                                    info.RNAdd_7 = input.RAdd_7;
                                    info.RNAdd_8 = input.RAdd_8;
                                    info.RNAdd_9 = input.RAdd_9;
                                }

                                info.CreatorTime = DateTime.Now;
                                info.CreatorUserId = CurrentUser.UserId;
                                info.LastModifyTime = DateTime.Now;
                                info.LastModifyUserId = CurrentUser.UserId;
                                info.CreatorUserOrgId = CurrentUser.OrganizeId;
                                info.CreatorUserDeptId = CurrentUser.DeptId;

                                long ln = await rnService.InsertAsync(info).ConfigureAwait(false);
                                updateResult = ln > 0;
                            }
                        } else {
                            string RCID = input.LRID;
                            if (rcIDList.Contains(RCID)) {
                                if (Array.Exists(rcUserIDList, element => element.Contains(RCID))) {
                                    updateResult = true;
                                } else {
                                    result.Success = false;
                                    result.ErrMsg = "此統一編號已存在，請到請求存取權頁面取得存取權";
                                    return ToJsonContent(result);
                                }
                            } else {
                                //房客不存在（所以新增）
                                CustomerRC info = new CustomerRC();

                                info.RCID = RCID;
                                info.RCName = input.Name;

                                //電話
                                info.RCTel = input.Tel;
                                info.RCTel_1 = input.Tel_1;
                                info.RCTel_2 = input.Tel_2;

                                //手機
                                if (!string.IsNullOrEmpty(input.Cell)) {
                                    if (input.Cell.Length == 10) {
                                        info.RCCell = input.Cell;
                                    }
                                }

                                //身分證字號
                                var idSplit = RCID.ToCharArray();
                                info.RCID_1_1 = idSplit[0].ToString();
                                info.RCID_1_2 = idSplit[1].ToString();
                                info.RCID_1_3 = idSplit[2].ToString();
                                info.RCID_1_4 = idSplit[3].ToString();
                                info.RCID_1_5 = idSplit[4].ToString();
                                info.RCID_1_6 = idSplit[5].ToString();
                                info.RCID_1_7 = idSplit[6].ToString();
                                info.RCID_1_8 = idSplit[7].ToString();

                                input.RAdd = Utils.memergeAddFix(
                                    input.RAdd_1,
                                    input.RAdd_1_1,
                                    input.RAdd_1_2,
                                    input.RAdd_2,
                                    input.RAdd_2_1,
                                    input.RAdd_2_2,
                                    input.RAdd_2_3,
                                    input.RAdd_2_4,
                                    input.RAdd_3,
                                    input.RAdd_3_1,
                                    input.RAdd_3_2,
                                    input.RAdd_4,
                                    input.RAdd_5,
                                    input.RAdd_6,
                                    input.RAdd_7,
                                    input.RAdd_8,
                                    input.RAdd_9
                                );

                                // 戶籍地址
                                if (!string.IsNullOrEmpty(input.RAdd)) {
                                    info.RCAdd = input.RAdd;
                                    info.RCAdd_1 = input.RAdd_1;
                                    info.RCAdd_1_1 = input.RAdd_1_1;
                                    info.RCAdd_1_2 = input.RAdd_1_2;
                                    info.RCAdd_2 = input.RAdd_2;
                                    info.RCAdd_2_1 = input.RAdd_2_1;
                                    info.RCAdd_2_2 = input.RAdd_2_2;
                                    info.RCAdd_2_3 = input.RAdd_2_3;
                                    info.RCAdd_2_4 = input.RAdd_2_4;
                                    info.RCAdd_3 = input.RAdd_3;
                                    info.RCAdd_3_1 = input.RAdd_3_1;
                                    info.RCAdd_3_2 = input.RAdd_3_2;
                                    info.RCAdd_4 = input.RAdd_4;
                                    info.RCAdd_5 = input.RAdd_5;
                                    info.RCAdd_6 = input.RAdd_6;
                                    info.RCAdd_7 = input.RAdd_7;
                                    info.RCAdd_8 = input.RAdd_8;
                                    info.RCAdd_9 = input.RAdd_9;
                                }

                                info.CreatorTime = DateTime.Now;
                                info.CreatorUserId = CurrentUser.UserId;
                                info.LastModifyTime = DateTime.Now;
                                info.LastModifyUserId = CurrentUser.UserId;
                                info.CreatorUserOrgId = CurrentUser.OrganizeId;
                                info.CreatorUserDeptId = CurrentUser.DeptId;

                                long ln = await rcService.InsertAsync(info).ConfigureAwait(false);
                                updateResult = ln > 0;
                            }
                        }
                    break;
                }
                
                if (updateResult) {
                    transferUpdateResult = await iService.UpdateTransferClientFields(CurrentUser.UserId, input.PID, input.Identity, input.Name, baddArray, conn, tran);
                }

                if (transferUpdateResult) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                }

            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("潛在客轉正失敗", ex);
                result.ErrMsg = $"潛在客轉正失敗:{ex.Message}";
                result.ErrCode = ErrCode.err43001;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 產生潛在客編號
        /// </summary>
        /// <param name="info"></param>
        private string GeneratePID(PotentialCustomers info)
        {
            // 潛在客編號:出租人/承租人+手機號碼＋所屬店            
            string identity = ("出租人".Equals(info.Identity)) ? "L" : "R";
            string identity2 = ("自然人".Equals(info.Identity2)) ? "N" : "C";
            string id = (string.IsNullOrEmpty(info.Cell)) ? GuidUtils.CreateNo() : info.Cell;
            id = (string.IsNullOrEmpty(info.Area)) ? id : id + "_" + info.Area;

            //return identity + info.Cell + "_" + info.Area;
            return identity + identity2 + "_" + id;
        }

        private async Task<bool> BuildingInsertAsync(PotentialCustomersInputDto input, string belongLID, IDbTransaction tran) {
            bool buildingInsertResult = false;
            // 同步新增建物
            var buildingInfo = input.MapTo<Building>();
            buildingInfo.Id = GuidUtils.CreateNo();
            buildingInfo.CreatorTime = DateTime.Now;
            buildingInfo.CreatorUserId = CurrentUser.UserId;
            buildingInfo.CreatorUserOrgId = CurrentUser.OrganizeId;
            buildingInfo.CreatorUserDeptId = CurrentUser.DeptId;
            buildingInfo.BState = "待招租";

            buildingInfo.BelongLID = belongLID;
            var b1info = new Building1();
            b1info.Id = buildingInfo.Id;
            b1info.BAdd = buildingInfo.BAdd;
            b1info.CreatorUserId = buildingInfo.CreatorUserId;
            b1info.CreatorTime = buildingInfo.CreatorTime;
            b1info.CreatorUserDeptId = buildingInfo.CreatorUserDeptId;
            b1info.CreatorUserOrgId = buildingInfo.CreatorUserOrgId;

            List<BuildingBelonging> buildingBelongingEntitys = new List<BuildingBelonging>();


            buildingInsertResult = await buildingService.InsertAsync(buildingInfo, buildingBelongingEntitys, tran).ConfigureAwait(false);
            buildingInsertResult = await building1Service.InsertAsync(b1info, tran).ConfigureAwait(false);

            // 建物廣告
            BuildingAdvertisement baInfo = new BuildingAdvertisement();
            baInfo.Id = buildingInfo.Id;
            baInfo.BAdd = buildingInfo.BAdd;
            baInfo.BAStatus = "廣告未上架";
            baInfo.CreatorUserId = buildingInfo.CreatorUserId;
            baInfo.CreatorTime = buildingInfo.CreatorTime;
            baInfo.CreatorUserOrgId = buildingInfo.CreatorUserOrgId;
            baInfo.CreatorUserDeptId = buildingInfo.CreatorUserDeptId;
            buildingInsertResult = await buildingAdvertisementService.InsertAsync(baInfo);

            return buildingInsertResult;
        }
    }
}