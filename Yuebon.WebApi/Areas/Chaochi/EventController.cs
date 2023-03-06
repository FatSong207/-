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
using Yuebon.Commons.Dtos;
using Yuebon.AspNetCore.Mvc;
using System.Data;
using Yuebon.Commons.IDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Json;
using Microsoft.AspNetCore.StaticFiles;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extensions;
using NPOI.POIFS.Storage;
using System.Globalization;
using System.Net.Http;
using Yuebon.Commons;
using Yuebon.Chaochi.Services;
using Newtonsoft.Json;
using System.Text;
using System.Management;
using MiniExcelLibs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Dapper;
using NPOI.OpenXmlFormats.Vml;
using System.IO;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Linq;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb;
using static System.Reflection.Metadata.BlobBuilder;
using NPOI.HSSF.UserModel;
using Yuebon.Chaochi.Core.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs.Card;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class EventController : AreaApiController<Event, EventOutputDto, EventInputDto, IEventService, string>
    {
        private readonly ISatisfactionService _satisfactionService;
        private readonly IEventSatisfactionService _eventSatisfactionService;
        private readonly IEventQuestionnaireService _eventQuestionnaireService;
        private readonly IEventGuestService _eventGuestService;
        private readonly IEventPersonnelService _eventPersonnelService;
        private readonly IDbContextCore _ybContext;
        private readonly IEventCostService _eventCostService;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="satisfactionService"></param>
        /// <param name="eventSatisfactionService"></param>
        /// <param name="eventQuestionnaireService"></param>
        /// <param name="eventGuestService"></param>
        /// <param name="eventPersonnelService"></param>
        /// <param name="ybContext"></param>
        /// <param name="eventCostService"></param>
        public EventController(IEventService _iService, ISatisfactionService satisfactionService, IEventSatisfactionService eventSatisfactionService, IEventQuestionnaireService eventQuestionnaireService, IEventGuestService eventGuestService, IEventPersonnelService eventPersonnelService, IDbContextCore ybContext, IEventCostService eventCostService) : base(_iService)
        {
            iService = _iService;
            _satisfactionService = satisfactionService;
            _eventSatisfactionService = eventSatisfactionService;
            _eventQuestionnaireService = eventQuestionnaireService;
            _eventGuestService = eventGuestService;
            _eventPersonnelService = eventPersonnelService;
            _ybContext = ybContext;
            _eventCostService = eventCostService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Event info)
        {
            info.Id = GuidUtils.CreateNo();
            info.FinishCount = 0;
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
            info.LastModifyUserId = CurrentUser.UserId;
        }

        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(Event info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Event info)
        {
        }

        /// <summary>
        /// 更新調查問卷(只更新活動 調查問卷名稱 問卷開頭語 問卷結尾語)
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("UpdateQuestionnaire")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> UpdateQuestionnaire(EventQuestionnaireInputDto inputDto)
        {
            var result = new CommonResult();
            try {
                var b = await _eventQuestionnaireService.UpdateEventQuestionnaire(inputDto);
                if (b) {
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    return ToJsonContent(result);
                } else {
                    result.Success = false;
                    result.ErrMsg = ErrCode.err43002;
                    return ToJsonContent(result);
                }
            } catch (Exception ex) {
                Log4NetHelper.Error("", ex);
                result.Success = false;
                result.ErrMsg = ErrCode.err43002;
                return ToJsonContent(result);
                throw;
            }

        }


        /// <summary>
        /// 更新調查問卷
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("GenSighUp")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> GenSighUp(EventQuestionnaireInputDto inputDto)
        {
            var result = new CommonResult();
            var eventQues_DB = await _eventQuestionnaireService.GetAsync(inputDto.Id);
            var eventQues = inputDto.MapTo(eventQues_DB);
            try {
                if (string.IsNullOrEmpty(inputDto.EId)) {
                    result.ErrCode = "43001";
                    result.ErrMsg = "刪除原題目資料時發生錯誤(EId不能為空)";
                    return ToJsonContent(result);
                }
                var dSuccess = await iService.DeleteSignUP(inputDto.EId);
                if (!dSuccess) {
                    result.ErrCode = "43001";
                    result.ErrMsg = "刪除原題目資料時發生錯誤";
                    return ToJsonContent(result);
                }
                Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
                Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
                var path = $"{sysSetting.ChaochiFilepath}";

                //生成問卷
                var e = await iService.GetWhereAsync($" EventId = '{eventQues.EventId}' ");
                var cr = await SignUpHelper.GenSignUp(e.Id,eventQues.QName,eventQues.QBegingWords,eventQues.QEndWords, @$"D:\\zzz\\image\\EventQuestionnaireTopic\\{eventQues.QCode}\\{eventQues.QFileName}");
                if (cr.ResData == null) {
                    result.ErrCode = "43001";
                    result.ErrMsg = "生成問卷時發生錯誤";
                    return ToJsonContent(result);
                }

                //更新
                eventQues.QUrl = cr.ResData.ToString();
                eventQues.HasGened = "Y";
                await iService.UpdateTableFieldAsync("FinishCount", 0, $"Id = '{e.Id}'");
                await _eventQuestionnaireService.UpdateAsync(eventQues, inputDto.Id);

                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } catch (Exception ex) {
                Log4NetHelper.Error("更新失敗", ex);
                result.Success = false;
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";

                throw;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 更新人力與成本
        /// </summary>
        /// <param name="updateObj"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpPost("UpdateCostPersonnel")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> UpdateCostPersonnel(UpdateEventCostIncomePersonnelDto updateObj, string eventId)
        {
            var result = new CommonResult();
            var eventCosts = updateObj.eventCosts;
            //var eventPersonnels = updateObj.eventPersonnels;
            try {
                using IDbConnection conn = _ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = _ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();
                try {
                    //更新成本與收入
                    foreach (var item in eventCosts) {
                        item.EventId = eventId;
                    }
                    await _eventCostService.DeleteConnWhereAsync($" EventId = '{eventId}' ", conn, tran);
                    _eventCostService.InsertRange(eventCosts);

                    //更新投入人員(前端下拉選單只接收List<string>)
                    var eventPersonnels = new List<EventPersonnel>();
                    foreach (var item in updateObj.eventPersonnels) {
                        eventPersonnels.Add(new EventPersonnel { UserId = item, EventId = eventId });
                    }
                    await _eventPersonnelService.DeleteConnWhereAsync($" EventId = '{eventId}' ", conn, tran);
                    _eventPersonnelService.InsertRange(eventPersonnels);

                    //更新活動主表
                    var event_DB = await iService.GetAsync(eventId);
                    event_DB.TotalCost = updateObj.TotalCost;
                    event_DB.TotalIncome = updateObj.TotalIncome;
                    event_DB.SubTotal = updateObj.SubTotal;
                    event_DB.TotalPerson = updateObj.TotalPerson;
                    OnBeforeUpdate(event_DB);
                    await iService.UpdateAsync(event_DB, eventId, conn, tran);

                    eftran.Commit();
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("更新失敗", ex);
                    result.ErrMsg = "更新失敗";
                    result.ErrCode = ErrCode.err43002;
                    throw;
                }
            } catch (Exception ex) {
                Log4NetHelper.Error("TransactionFailed", ex);
                result.ErrMsg = "TransactionFailed";
                result.ErrCode = ErrCode.err43001;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 根據主鍵Id獲取一個對象信息
        /// </summary>
        /// <param name="id">主鍵Id</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        [YuebonAuthorize("List")]
        public override async Task<CommonResult<EventOutputDto>> GetById(string id)
        {
            CommonResult<EventOutputDto> result = new CommonResult<EventOutputDto>();
            var info = await iService.GetOutDtoAsync(id);
            info.EndDate = Convert.ToDateTime(info.EndDate).AddYears(-1911).ToString("yyy-MM-dd");
            info.StartDate = Convert.ToDateTime(info.StartDate).AddYears(-1911).ToString("yyy-MM-dd");

            //成本
            info.eventCosts = await _eventCostService.GetListWhereAsync($" EventId = '{id}' ");

            //人力
            var listdata = await _eventPersonnelService.GetListWhereAsync($" EventId = '{id}' ");
            var eventPersonnels = new List<string>();
            foreach (var item in listdata) {
                eventPersonnels.Add(item.UserId);
            }
            info.eventPersonnels = eventPersonnels;

            if (info.EventType == "開放參加") {
                //調查問卷
                info.EventQuestionnaire = await _eventQuestionnaireService.GetWhereAsync($" EventId = '{info.EventId}' ");
            } else if (info.EventType == "指定來賓") {
                //滿意度問卷
                info.EventSatisfaction = await _eventSatisfactionService.GetWhereAsync($" EventId = '{info.EventId}' ");
            }

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
        /// 根據EventId帶上業務ID組成填寫網址
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet("DirectToSignUpWithSalesId")]
        public async Task<IActionResult> DirectToSignUpWithSalesId(string eventId)
        {
            var result = new CommonResult();
            var eq = await _eventQuestionnaireService.GetWhereAsync($" EventId = '{eventId}' ");
            if (eq == null) {
                result.ErrCode = "43001";
                result.ErrMsg = "生成問卷時發生錯誤，請檢查EventId是否正確";
                return ToJsonContent(result);
            }
            var e = await iService.GetWhereAsync($"  EventId = '{eventId}'  ");

            string url = "";
            try {
                var cr = await SignUpHelper.EncryptURLAsync(e.Id, CurrentUser.UserId);
                if (cr.ResData == null) {
                    result.ErrCode = "43001";
                    result.ErrMsg = "生成問卷時發生錯誤";
                    return ToJsonContent(result);
                }
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = cr.ResData;
                return ToJsonContent(result);
            } catch (HttpRequestException ex) {
                Log4NetHelper.Error(ex.ToString());
                result.ErrCode = "43001";
                result.ErrMsg = "生成問卷時發生錯誤";
                return ToJsonContent(result);
            }
        }

        /// <summary>
        /// 獲取賓客名單分頁
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("GetGuestList")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> GetGuestList(SearchInputDto<EventGuestOutputDto> search)
        {
            var result = new CommonResult();
            var dataList = await _eventGuestService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            result.ResData = dataList;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步新增數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(EventInputDto tinfo)
        {
            CommonResult result = new CommonResult();
            if (string.IsNullOrEmpty(tinfo.EventName) || string.IsNullOrEmpty(tinfo.EventType) || string.IsNullOrEmpty(tinfo.StartDate) || string.IsNullOrEmpty(tinfo.EndDate)) {
                result.ErrMsg = "請輸入必填欄位1";
                result.Success = false;
                result.ErrCode = ErrCode.err43001;
                return ToJsonContent(result);
            } else {
                var info = tinfo.MapTo<Event>();
                info.EndDate = info.EndDate.AddYears(1911);
                info.StartDate = info.StartDate.AddYears(1911);
                OnBeforeInsert(info);

                if (tinfo.EventType == "開放參加") {
                    using IDbConnection conn = _ybContext.GetDatabase().GetDbConnection();
                    conn.Open();
                    using var eftran = _ybContext.GetDatabase().BeginTransaction();
                    var tran = eftran.GetDbTransaction();
                    try {
                        long ln = await iService.InsertAsync(info, conn, trans: tran).ConfigureAwait(false);
                        var eventQues = new EventQuestionnaire() {
                            EventId = info.EventId,
                            QCode = info.EventId + "-1",
                            QType = info.EventType,
                            EventName = info.EventName,
                            BelongCompany = info.BelongCompany,
                            HasGened = "N",
                            CreatorTime = info.CreatorTime,
                            CreatorUserId = info.CreatorUserId,
                            LastModifyTime = info.LastModifyTime,
                            LastModifyUserId = info.LastModifyUserId,
                        };
                        long ln2 = await _eventQuestionnaireService.InsertAsync(eventQues, conn, trans: tran).ConfigureAwait(false);

                        eftran.Commit();
                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } catch (Exception ex) {
                        eftran.Rollback();
                        Log4NetHelper.Error("Transaction新增失敗", ex);
                        result.ErrMsg = "Transaction新增失敗";
                        result.ErrCode = ErrCode.err43001;
                    }
                } else if (tinfo.EventType == "指定來賓") {
                    Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
                    Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
                    var path = $"{sysSetting.ChaochiFilepath}";
                    var url = "";

                    var sa = await _satisfactionService.GetWhereAsync("Type = '活動滿意度問卷'");
                    //生成問卷
                    var cr = await SignUpHelper.GenSignUp(info.Id, sa.Type, sa.Type, sa.QEndWords, @$"D:\\zzz\\image\\SatisfactionTopic\\{sa.QFileName}");
                    if (cr.ResData == null) {
                        result.ErrCode = "43001";
                        result.ErrMsg = "生成問卷時發生錯誤";
                        return ToJsonContent(result);
                    }
                    QRCoderHelper.GenQRCode(cr.ResData.ToString(), @$"{path}\EventSatisfactionQRCode\{info.EventId}.png");

                    using IDbConnection conn = _ybContext.GetDatabase().GetDbConnection();
                    conn.Open();
                    using var eftran = _ybContext.GetDatabase().BeginTransaction();
                    var tran = eftran.GetDbTransaction();
                    try {
                        long ln = await iService.InsertAsync(info, conn, trans: tran).ConfigureAwait(false);
                        var eventSatis = new EventSatisfaction() {
                            EventId = info.EventId,
                            QCode = info.EventId + "-2",
                            QUrl = url,
                            QRcodeImg = info.EventId + ".png",
                            QType = info.EventType,
                            EventName = info.EventName,
                            BelongCompany = info.BelongCompany,
                            CreatorTime = info.CreatorTime,
                            CreatorUserId = info.CreatorUserId,
                            LastModifyTime = info.LastModifyTime,
                            LastModifyUserId = info.LastModifyUserId,
                        };
                        long ln2 = await _eventSatisfactionService.InsertAsync(eventSatis, conn, trans: tran).ConfigureAwait(false);

                        eftran.Commit();
                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } catch (Exception ex) {
                        eftran.Rollback();
                        Log4NetHelper.Error("Transaction新增失敗", ex);
                        result.ErrMsg = "Transaction新增失敗";
                        result.ErrCode = ErrCode.err43001;
                    }
                } else {
                    long ln = await iService.InsertAsync(info).ConfigureAwait(false);
                    if (ln > 0) {
                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                    } else {
                        result.ErrMsg = ErrCode.err43001;
                        result.ErrCode = "43001";
                    }
                }
                return ToJsonContent(result);

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override Task<IActionResult> UpdateAsync(EventInputDto inInfo)
        {
            var result = new CommonResult();
            var eve_DB = iService.Get(inInfo.Id);
            var info = inInfo.MapTo(eve_DB);

            info.EndDate = info.EndDate.AddYears(1911);
            info.StartDate = info.StartDate.AddYears(1911);

            OnBeforeUpdate(info);
            var isSuccess = iService.Update(info, inInfo.Id);
            if (isSuccess) {
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } else {
                result.Success = false;
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return Task.FromResult(ToJsonContent(result));
        }

        /// <summary>
        /// 異步批量物理刪除
        /// </summary>
        /// <param name="info"></param>
        [HttpDelete("DeleteBatchAsync")]
        [YuebonAuthorize("Delete")]
        public override async Task<IActionResult> DeleteBatchAsync(DeletesInputDto info)
        {
            var result = new CommonResult();
            string where = string.Empty;
            if (typeof(string) == typeof(string)) {
                where = "id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            } else if (typeof(string) == typeof(int)) {
                where = "id in (" + info.Ids.Join(",") + ")";
            }
            try {
                using IDbConnection conn = _ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = _ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();
                try {
                    await iService.DeleteConnWhereAsync(where, conn, tran);
                    await _eventCostService.DeleteConnWhereAsync($" EventId in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "') ", conn, tran);
                    await _eventPersonnelService.DeleteConnWhereAsync($" EventId in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "') ", conn, tran);
                    await _eventGuestService.DeleteConnWhereAsync($" EventId in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "') ", conn, tran);

                    eftran.Commit();
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("更新失敗", ex);
                    result.ErrMsg = "更新失敗";
                    result.ErrCode = ErrCode.err43002;
                    throw;
                }
            } catch (Exception ex) {
                Log4NetHelper.Error("TransactionFailed", ex);
                result.ErrMsg = "TransactionFailed";
                result.ErrCode = ErrCode.err43001;
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
        public async Task<IActionResult> FindWithPagerSearchAsync(SearchInputDto<EventOutputDto> search)
        {
            CommonResult<PageResult<EventOutputDto>> result = new CommonResult<PageResult<EventOutputDto>>();
            result.ResData = await iService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步分頁查詢FOR eventdashboard
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerSearchAsyncED")]
        [YuebonAuthorize("EDList")]
        public async Task<IActionResult> FindWithPagerSearchAsyncED(SearchInputDto<EventDashboardOutputDto> search)
        {
            CommonResult<PageResult<EventDashboardOutputDto>> result = new CommonResult<PageResult<EventDashboardOutputDto>>();
            result.ResData = await iService.FindWithPagerSearchAsyncED(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 下載賓客維護CSV
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("DownloadGuestListById")]
        [LogNotMethod]
        public async Task<IActionResult> DownloadGuestListById(string id)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            var fileName = "賓客清單.csv";
            string streamPath;
            if (!System.IO.File.Exists(@$"{path}EventGuest\{id}\{fileName}")) {
                streamPath = @$"{path}EventGuest\{fileName}";
            } else {
                streamPath = @$"{path}EventGuest\{id}\{fileName}";
            }
            var pdf = System.IO.File.OpenRead(streamPath);
            new FileExtensionContentTypeProvider().TryGetContentType("xx.csv", out string contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }

        /// <summary>
        /// 下載調查問卷題目定義檔CSV
        /// </summary>
        /// <returns></returns>
        [HttpGet("DownloadQuestionnaireTopicByQCode")]
        [LogNotMethod]
        public async Task<IActionResult> DownloadQuestionnaireTopicByQCode(string qcode)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            var eventQuest = await _eventQuestionnaireService.GetWhereAsync($" QCode = '{qcode}' ");
            var fileName = eventQuest.QFileName;
            string streamPath;

            if (!string.IsNullOrEmpty(fileName)) {
                streamPath = @$"{path}EventQuestionnaireTopic\{qcode}\{fileName}";
            } else {
                streamPath = @$"{path}EventQuestionnaireTopic\調查問卷題目定義檔.xlsx";
            }

            var pdf = System.IO.File.OpenRead(streamPath);
            new FileExtensionContentTypeProvider().TryGetContentType("xx.xlsx", out string contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }

        /// <summary>
        /// 下載滿意度問卷QrCode
        /// </summary>
        /// <returns></returns>
        [HttpGet("DownloadSatisfactionQrCodeByQCode")]
        [LogNotMethod]
        public async Task<IActionResult> DownloadSatisfactionQrCodeByQCode(string qcode)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            var eventSatis = await _eventSatisfactionService.GetWhereAsync($" QCode = '{qcode}' ");
            var fileName = eventSatis.QRcodeImg;
            string streamPath;

            streamPath = @$"{path}EventSatisfactionQRCode\{fileName}";
            var pdf = System.IO.File.OpenRead(streamPath);
            new FileExtensionContentTypeProvider().TryGetContentType("xx.png", out string contentType);
            return File(pdf, contentType ?? "image/png");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet("ShowImg")]
        [LogNotMethod]
        //public IActionResult Get(string fileName)
        public async Task<IActionResult> ShowImg(string eventId)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var eventSatis = await _eventSatisfactionService.GetWhereAsync($"EventId = '{eventId}'");

            var image = System.IO.File.OpenRead($"{sysSetting.ChaochiFilepath}EventSatisfactionQRCode\\{eventSatis.QRcodeImg}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(eventSatis.QRcodeImg, out contentType);
            return File(image, contentType ?? "image/jpeg");
            //return File(image, "application/octet-stream");
        }

        /// <summary>
        /// 獲取表頭
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet("GenStatistic")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GenStatisticTable(string eventId)
        {
            var result = new CommonResult();
            try {
                var list = await iService.GetTableTitleArr(eventId);
                var test = await iService.GetTableData(eventId);
                if (list.Count == 0) {
                    result.Success = false;
                    result.ErrCode = "43001";
                    result.ErrMsg = "獲取題目時發生錯誤";
                    return ToJsonContent(result);
                }
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = list;
                return ToJsonContent(result);
            } catch (Exception ex) {
                result.Success = false;
                result.ErrCode = "43001";
                result.ErrMsg = "獲取題目時發生錯誤";
                return ToJsonContent(result);
                throw;
            }
        }

        /// <summary>
        /// 獲取資料
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet("GenStatisticTableData")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GenStatisticTableData(string eventId)
        {
            var result = new CommonResult();
            try {
                var test = await iService.GetTableData(eventId);
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = test;
                return ToJsonContent(result);
            } catch (Exception) {
                result.Success = false;
                result.ErrCode = "43001";
                result.ErrMsg = "獲取題目時發生錯誤";
                return ToJsonContent(result);
                throw;
            }
        }

        /// <summary>
        /// 匯出XLSX
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet("ExportToXLSX")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> ExportToXLSX(string eventId)
        {
            var result = new CommonResult();
            try {
                var list = await iService.GetTableTitleArr(eventId);
                var test = await iService.GetTableDataForXLSX(eventId);
                var js = JsonConvert.SerializeObject(test);
                var dt = (DataTable)JsonConvert.DeserializeObject<DataTable>(js);
                var e = await iService.GetWhereAsync($" Id = '{eventId}' ");
                var eq = await _eventQuestionnaireService.GetWhereAsync($" EventId = '{e.EventId}' ");
                var es = await _eventSatisfactionService.GetWhereAsync($" EventId = '{e.EventId}' ");
                var qCode = e.EventType == "開放參加" ? eq.QCode : es.QCode;

                //var ms = new MemoryStream();
                using var fs = new MemoryStream();
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet1 = workbook.CreateSheet("Sheet1");

                var columns = new List<string>();

                IRow rowf = sheet1.CreateRow(0);
                rowf.Height = 40 * 80;
                var c = rowf.CreateCell(0);
                ICellStyle notesStyle = workbook.CreateCellStyle();
                notesStyle.WrapText = true;
                //notesStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
                string title = "活動滿意度問卷統計" + "\n";
                title += $"滿意度問卷代號：{qCode}" + "\n";
                title += $"活動名稱：{e.EventName}" + "\n";
                title += $"活動代號：{e.EventId}" + "\n";
                title += $"所屬公司：{e.BelongCompany}" + "\n";
                title += $"問卷當前總份數：{e.FinishCount}" + "\n";
                c.SetCellValue(title);
                c.CellStyle = notesStyle;
                sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 4));



                IRow row = sheet1.CreateRow(1);

                //sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 10));
                var columnIndex = 0;
                //row.Height = 30 * 80;
                ICellStyle style = workbook.CreateCellStyle();
                style.BorderBottom = BorderStyle.Double;
                style.BorderLeft = BorderStyle.Double;
                style.BorderRight = BorderStyle.Double;
                style.BorderTop = BorderStyle.Double;
                foreach (DataColumn item in dt.Columns) {
                    if (item.ColumnName == "編號") {
                        continue;
                    }
                    columns.Add(item.ColumnName);
                    var tc = row.CreateCell(columnIndex);
                    tc.SetCellValue(item.ColumnName);
                    tc.CellStyle = style;
                    sheet1.AutoSizeColumn(columnIndex);
                    columnIndex++;
                }
                //row.CreateCell(0).SetCellValue("this is content");

                int dataIndex = 2;
                foreach (DataRow item in dt.Rows) {
                    row = row.Sheet.CreateRow(dataIndex);
                    int cellIndex = 0;
                    foreach (var col in columns) {
                        row.CreateCell(cellIndex).SetCellValue(item[col].ToString());
                        sheet1.AutoSizeColumn(cellIndex);
                        cellIndex++;
                    }
                    dataIndex++;
                }
                workbook.Write(fs);
                string contentType;
                new FileExtensionContentTypeProvider().TryGetContentType("xx.xlsx", out contentType);
                return File(fs.ToArray(), contentType ?? "image/jpeg");
            } catch (Exception ex) {
                Log4NetHelper.Error(ex.ToString());
                result.Success = false;
                result.ErrCode = "43001";
                result.ErrMsg = "獲取題目時發生錯誤";
                return ToJsonContent(result);
                throw;
            }
        }
    }
}