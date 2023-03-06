using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.IServices;
using Yuebon.Commons.Dtos;
using Yuebon.AspNetCore.Mvc.Filter;
using System.Data;
using Yuebon.Commons.IDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Yuebon.Commons.Log;
using Yuebon.Chaochi.Services;
using System.Globalization;
using Yuebon.Commons.Mapping;
using Yuebon.AspNetCore.Mvc;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 待辦事項接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class ToDoListController : AreaApiController<ToDoList, ToDoListOutputDto, ToDoListInputDto, IToDoListService, string>
    {
        private readonly ISendMailInfoService _sendMailInfoService;
        private readonly IBlackListService _blackListService;
        private readonly IContractService contractService;
        private IContractAttachmentService contractAttachmentService;
        private IContractFlowLogService contractFlowLogService;
        private IHistoryFormContractService contractHistoryService;
        private IBuildingService buildingService;
        private Security.IServices.IUserService userService;
        private readonly IDbContextCore ybContext;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="sendMailInfoService"></param>
        /// <param name="blackListService"></param>
        /// <param name="_contractService"></param>
        /// <param name="_contractAttachmentService"></param>
        /// <param name="_contractFlowLogService"></param>
        /// <param name="_contractHistoryService"></param>
        /// <param name="_buildingService"></param>
        /// <param name="_userService"></param>
        /// <param name="_ybContext"></param>
        public ToDoListController(
            IToDoListService _iService,
            ISendMailInfoService sendMailInfoService,
            IBlackListService blackListService,
            IContractService _contractService,
            IContractAttachmentService _contractAttachmentService,
            IContractFlowLogService _contractFlowLogService,
            IHistoryFormContractService _contractHistoryService,
            IBuildingService _buildingService,
            Security.IServices.IUserService _userService,
            IDbContextCore _ybContext
        ) : base(_iService)
        {
            iService = _iService;
            _sendMailInfoService = sendMailInfoService;
            _blackListService = blackListService;
            contractService = _contractService;
            contractAttachmentService = _contractAttachmentService;
            contractFlowLogService = _contractFlowLogService;
            contractHistoryService = _contractHistoryService;
            buildingService = _buildingService;
            userService = _userService;
            ybContext = _ybContext;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(ToDoList info)
        {
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
        }

        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(ToDoList info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }
        
        [HttpGet("GetPersonalTodoListCount")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetPersonalTodoListCount(string account)
        {
            var result = new CommonResult();
            result.ResData = await iService.GetCountByWhereAsync($" Account = '{account}' ");
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerAsync")]
        [NoPermissionRequired]
        public override async Task<CommonResult<PageResult<ToDoListOutputDto>>> FindWithPagerAsync(SearchInputDto<ToDoList> search)
        {
            CommonResult<PageResult<ToDoListOutputDto>> result = new CommonResult<PageResult<ToDoListOutputDto>>();
            result.ResData = await iService.FindWithPagerAsync(search);
            result.ErrCode = ErrCode.successCode;
            return result;
        }

        /// <summary>
        /// 根據主鍵Id獲取一個對象信息
        /// </summary>
        /// <param name="id">主鍵Id</param>
        /// <returns></returns>
        [HttpGet("GetById2")]
        [NoPermissionRequired]
        public async Task<CommonResult> GetById2(string id)
        {
            var result = new CommonResult();
            ToDoListOutputDto info = await iService.GetOutDtoAsync(id);

            if (info != null) {
                switch (info.Type) {
                    case "合約":
                        result = new CommonResult<ContractOutputDto>();
                        string cid = ToDoListService.getCID(info.TypeId); ;

                        // 合約資訊
                        ContractOutputDto contractInfo = await contractService.GetContract(cid);
                        contractInfo.TodoType = "合約";

                        if (contractInfo != null) {
                            result.ErrCode = ErrCode.successCode;
                            result.ResData = contractInfo;
                        } else {
                            result.ErrMsg = ErrCode.err50001;
                            result.ErrCode = "50001";
                        }
                        break;
                    case "入居者審查":
                        var blackList =await _blackListService.GetAsync(info.TypeId);
                        if (blackList != null) {
                            var outPutResult = blackList.MapTo<BlackListOutputDto>();
                            outPutResult.CreatorUserName = userService.Get(outPutResult.CreatorUserId).RealName;
                            result.ErrCode = ErrCode.successCode;
                            result.ResData = outPutResult;
                        } else {
                            result.ErrMsg = ErrCode.err50001;
                            result.ErrCode = "50001";
                        }
                        break;
                }
            } else {
                result.ErrMsg = ErrCode.err50001;
                result.ErrCode = "50001";
            }

            return result;
        }

        /// <summary>
        /// 根據功能模組ID和功能模組資料獲取待辦事項
        /// </summary>
        /// <param name="typeId">功能模組ID</param>
        /// <returns></returns>
        [HttpGet("GetByTypeID")]
        [NoPermissionRequired]
        public async Task<CommonResult> GetByTypeID(string typeId)
        {
            var result = new CommonResult();
            ToDoListOutputDto info = await iService.GetByTypeID(typeId);


            if (info != null) {
                result.ErrCode = ErrCode.successCode;
                result.ResData = info;
            } else {
                result.ErrMsg = "Empty";
                result.ErrCode = ErrCode.successCode;
            }

            return result;
        }

        /// <summary>
        /// 根據功能模組ID和功能模組資料獲取待辦事項
        /// </summary>
        /// <param name="typeId">功能模組ID</param>
        /// <param name="typeData">功能模組資料</param>
        /// <returns></returns>
        [HttpGet("GetByTypeData")]
        [NoPermissionRequired]
        public async Task<CommonResult> GetByTypeData(string typeId, string typeData)
        {
            var result = new CommonResult();
            ToDoListOutputDto info = await iService.GetByTypeData(typeId, typeData);
            //ToDoListOutputDto info = await iService.GetByTypeID(typeId);
            

            if (info != null) {
                result.ErrCode = ErrCode.successCode;
                result.ResData = info;
            } else {
                result.ErrMsg = "Empty";
                result.ErrCode = ErrCode.successCode;
            }

            return result;
        }

        /// <summary>
        /// 入居者審查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        [HttpPost("BlackListSubmit")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> BlackListSubmit(ToDoListInputDto input)
        {
            var result = new CommonResult();
            var blackList_DB = await _blackListService.GetAsync(input.BlackListInputDto.Id);

            blackList_DB.TrialNote = input.BlackListInputDto.TrialNote;
            blackList_DB.ResultState = input.BlackListInputDto.ResultState;
            blackList_DB.LastModifyTime = DateTime.Now;
            blackList_DB.LastModifyUserId = CurrentUser.UserId;


            try {
                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();
                try {
                    var sales = await userService.GetAsync(blackList_DB.CreatorUserId);
                    await _blackListService.UpdateAsync(blackList_DB, blackList_DB.Id,conn,tran);
                    await iService.DeleteConnWhereAsync($" Id = '{input.Id}' ",conn,tran);
                    await _sendMailInfoService.InsertAsync(new SendMailInfo() {
                        RefKey = blackList_DB.Id,
                        Subject = $"[回函]*入居者審查：【申請人：{sales.RealName}，審查結果：{blackList_DB.ResultState}】",
                        Recipient = sales.Email,
                        Enable = true,
                        SendTime = DateTime.Now,
                        Body = AddMailBodyAsHTMLTemplate_Result(blackList_DB)
                    },conn,tran);
                    //await iService.UpdateTableFieldAsync("Status","已審核",$" Id = '{input.Id}' ", conn, tran);

                    tran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } catch (Exception ex) {
                    tran.Rollback();
                    Log4NetHelper.Error("審核失敗", ex);
                    result.ErrMsg = "審核失敗";
                    result.ErrCode = ErrCode.err50001;
                    result.Success = false;
                    // 觸發ExceptionHandlingAttribute.OnException
                    throw;
                }
            } catch (Exception ex) {
                Log4NetHelper.Error("審核失敗", ex);
                result.Success = false;
                result.ErrCode = ErrCode.err50001;
                result.ErrMsg = "審核失敗";
                throw;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 審核
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("FlowSubmit")]
        [NoPermissionRequired]
        public async Task<CommonResult> FlowSubmit(ToDoListInputDto input)
        {
            CommonResult result = new CommonResult();

            string userId = input.UserId;

            string cid = ToDoListService.getCID(input.TypeId);

            Contract contractInfo = await contractService.GetByCIDRaw(cid);

            Security.Models.User applicant = await userService.GetWhereAsync($"Id = '{contractInfo.CreatorUserId}'");
            string applicantName = (applicant != null) ? applicant.RealName : string.Empty;
            Security.Models.User auditor = await userService.GetWhereAsync($"Id = '{userId}'");
            string auditorName = (auditor != null) ? auditor.RealName : string.Empty;

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();
            bool updateContractStatusResult = true;
            bool deleteToDoListResult = false;
            bool updateBStateResult = true;
            long flowLogCount = 0L;
            string action = string.Empty;

            if (contractInfo != null) {
                try {
                    if ("同意".Equals(input.Action)) {
                        //switch (contractInfo.CStatus) {
                        switch (input.TypeData) {
                            case "備審未簽名":
                                // 改變合約狀態
                                updateContractStatusResult = await contractService.UpdateContractStatus(userId, contractInfo.CID, "已簽名待放行", "", conn, tran);
                                break;
                            case "已簽名待放行":
                                // 合約起始日期大於系統日期，合約狀態為「已簽約未生效」;小於等於系統日期，合約狀態為「合約已生效」
                                if (!string.IsNullOrWhiteSpace(contractInfo.ContractDate)) {
                                    DateTime today = DateTime.Now;
                                    CultureInfo culture = new CultureInfo("zh-TW");
                                    culture.DateTimeFormat.Calendar = new TaiwanCalendar();
                                    DateTime contractEffiectiveDate = DateTime.Parse(contractInfo.ContractDate, culture);
                                    int timeCompareResult = DateTime.Compare(contractEffiectiveDate, today);

                                    // 更新合約狀態
                                    updateContractStatusResult = (timeCompareResult <= 0) ? await contractService.UpdateContractStatus(userId, contractInfo.CID, "效期中", "", conn, tran) : await contractService.UpdateContractStatus(userId, contractInfo.CID, "待生效", "", conn, tran);
                                } else {
                                    result.ErrCode = ErrCode.err40110;
                                    result.ErrMsg = "合約生效日格式錯誤";
                                    result.Success = false;

                                    return result;
                                }

                                    // 改變合約狀態
                                    //updateContractStatusResult = await contractService.UpdateContractStatus(userId, contractInfo.CID, "待生效", "", conn, tran);
                                //} else {
                                //    // 改變合約狀態
                                //    updateContractStatusResult = await contractService.UpdateContractStatus(userId, contractInfo.CID, "備審未簽名", "", conn, tran);
                                //}
                                break;
                            case "解約":
                                // 改變合約狀態
                                updateContractStatusResult = await contractService.UpdateContractStatus(userId, contractInfo.CID, "已解約", "", conn, tran);

                                // 改變建物狀態
                                string badd = contractInfo.BAdd;
                                if (!badd.Contains(";") && !badd.Contains("\n")) {
                                    string bId = buildingService.GetIdByBAdd(badd);
                                    // 建物狀態需改為[待招租]
                                    updateBStateResult = await buildingService.UpdateBState(userId, bId, "待招租", conn, tran).ConfigureAwait(false);
                                } else {
                                    updateBStateResult = false;
                                }
                                break;
                        }
                    }

                    // 刪除待辦事項
                    ToDoList deleteInfo = new ToDoList();
                    deleteInfo.Id = input.Id;
                    deleteInfo.TypeId = input.TypeId;
                    //deleteInfo.Name = input.Name;
                    deleteToDoListResult = await iService.DeleteAsync(deleteInfo, tran);

                    // 審核記錄
                    ContractFlowLog logInfo = new ContractFlowLog();
                    logInfo.CID = input.TypeId;
                    logInfo.CStatus = contractInfo.CStatus;
                    logInfo.Applicant = auditorName;
                    logInfo.Auditor = applicantName;
                    logInfo.Action = input.Action;
                    logInfo.Comment = input.Memo;
                    logInfo.ApplyTime = DateTime.Now;
                    logInfo.CreatorTime = DateTime.Now;
                    logInfo.CreatorUserId = userId;
                    flowLogCount = await contractFlowLogService.InsertAsync(logInfo, conn, tran);


                    if (updateContractStatusResult && deleteToDoListResult && flowLogCount > 0 && updateBStateResult) {
                        eftran.Commit();
                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;
                    } else {
                        result.ErrCode = ErrCode.err50001;
                        result.ErrMsg = "審核失敗";
                        result.Success = false;
                    }

                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("審核失敗", ex);
                    result.ErrMsg = "審核失敗";
                    result.ErrCode = ErrCode.err50001;
                    result.Success = false;
                    // 觸發ExceptionHandlingAttribute.OnException
                    throw;
                }
            } else {
                result.Success = false;
                result.ErrMsg = "查無此合約";
            }

            return result;
        }

        /// <summary>
        /// 產生EmailBody內文html模板
        /// </summary>
        /// <param name="blackList"></param>
        /// <returns></returns>
        public string AddMailBodyAsHTMLTemplate_Result(BlackList blackList)
        {
            string html = @$"<h3 style='color:#eb6100;font-size:16px'> 兆基通知信
        </h3>這份通知是在提醒收件者，請留意此審查結果
        <table width='100%' border='0' align='left' cellpadding='0' cellspacing='0' bgcolor='#f7f7f7' style='border-top:2px solid #c4b993;font-family:'\005fae\008edf\006b63\009ed1\009ad4',Arial,Helvetica,Verdana'>
        <tbody>
          <tr>
          <td width='30%' style='background-color:#eaeaea;padding:8px;border-bottom:1px solid #fff'> 執行時間：
        </td>
        <td width='70%' style='padding:8px;border-bottom:1px solid #fff'> {blackList.LastModifyTime}
           </td>
           </tr>
           <tr>
           <td width='30%' style='background-color:#eaeaea;padding:8px;border-bottom:1px solid #fff'> 簽核單號：
        </td>
        <td width='70%' style='padding:8px;border-bottom:1px solid #fff' > {blackList.Id}
           </td>
           </tr>
          <tr>
          <td width='30%' style='background-color:#eaeaea;padding:8px;border-bottom:1px solid #fff'> 被審查名單：
        </td>
        <td width='70%' style='padding:8px;border-bottom:1px solid #fff'> {blackList.Name}
           </td>
           </tr>
          <tr>
          <td width='30%' style='background-color:#eaeaea;padding:8px;border-bottom:1px solid #fff'> 審查結果：
        </td>
        <td width='70%' style='padding:8px;border-bottom:1px solid #fff'> {blackList.ResultState}
           </td>
           </tr>
          <tr>
           </td>
           </tr>
           </tbody>
<p><h3 style='color: red; font-size:20px'><b>您送入居審查資料結果經主管判定後為{blackList.ResultState}</b></h3></p>
           </table>
<p><h5>您已經瞭解並同意以下事項：<br/>
審查結果僅適用於本公司及與本公司直接或間接關係企業用於租賃關係管理、辨識身分、行銷廣宣或入居者審查等，或主管機關依法要求提供者。</h5></p>
<p><h3 style='color: red; font-size:20px'><b>***此信由系統發出，請勿直接回信！***</b></h3></p>";
            return html;
        }

        ///// <summary>
        ///// 核准放行
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpPost("Approval")]
        //[NoPermissionRequired]
        //public async Task<CommonResult> Approval(ToDoListInputDto input)
        //{
        //    CommonResult result = new CommonResult();

        //    string userId = input.UserId;
        //    //string userId = CurrentUser?.UserId;
        //    DateTime currentTime = DateTime.Now;

        //    ToDoList TODOLIST_DB = await iService.GetAsync(input.Id);

        //    if (TODOLIST_DB != null) {
        //        //ToDoList info = input.MapTo(TODOLIST_DB);
        //        ToDoList todoListItem = input.MapTo<ToDoList>();
        //        ToDoList info = Merger.CloneAndMerge<ToDoList>(TODOLIST_DB, todoListItem);
        //        info.Status = "己放行";
        //        info.LastModifyTime = currentTime;
        //        info.LastModifyUserId = userId;

        //        using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
        //        conn.Open();
        //        using var eftran = ybContext.GetDatabase().BeginTransaction();
        //        var tran = eftran.GetDbTransaction();

        //        try {
        //            bool DMLResult = await iService.UpdateAsync(info, input.Id, conn, tran).ConfigureAwait(false);

        //            DMLResult = await contractService.UpdateContractStatus(userId, info.TypeId, "已簽約待進版", "", conn, tran);

        //            if (DMLResult == true) {
        //                eftran.Commit();
        //                result.ErrCode = ErrCode.successCode;
        //                result.Success = true;
        //            } else {
        //                result.ErrMsg = "放行失敗";
        //                result.ErrCode = "50001";
        //            }

        //        } catch (Exception ex) {
        //            eftran.Rollback();
        //            Log4NetHelper.Error("放行失敗", ex);
        //            result.ErrMsg = "放行失敗";
        //            result.ErrCode = ErrCode.err50001;
        //            result.Success = false;
        //            // 觸發ExceptionHandlingAttribute.OnException
        //            throw;
        //        }
        //    } else {
        //        result.Success = false;
        //        result.ErrMsg = "查無此待辦事項";
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// 駁回
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpPost("Disapproval")]
        //[NoPermissionRequired]
        //public async Task<CommonResult> Disapproval(ToDoListInputDto input)
        //{
        //    CommonResult result = new CommonResult();

        //    string userId = input.UserId;
        //    //string userId = CurrentUser?.UserId;
        //    DateTime currentTime = DateTime.Now;

        //    using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
        //    conn.Open();
        //    using var eftran = ybContext.GetDatabase().BeginTransaction();
        //    var tran = eftran.GetDbTransaction();            

        //    try {
        //        bool DMLResult = await iService.DeleteAsync(input.Id, conn, tran).ConfigureAwait(false);

        //        //DMLResult = await contractService.UpdateContractStatus(userId, info.TypeId, "已簽約待進版");

        //        if (DMLResult == true) {
        //            eftran.Commit();
        //            result.ErrCode = ErrCode.successCode;
        //            result.Success = true;
        //        } else {
        //            result.ErrMsg = "駁回失敗";
        //            result.ErrCode = "50001";
        //        }

        //    } catch (Exception ex) {
        //        eftran.Rollback();
        //        Log4NetHelper.Error("駁回失敗", ex);
        //        result.ErrMsg = "駁回失敗";
        //        result.ErrCode = ErrCode.err50001;
        //        result.Success = false;
        //        // 觸發ExceptionHandlingAttribute.OnException
        //        throw;
        //    }

        //    return result;
        //}
    }
}