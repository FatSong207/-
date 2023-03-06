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
using Yuebon.Security.IServices;
using Yuebon.Commons.Extensions;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Yuebon.Commons.Json;
using Yuebon.Chaochi.Core.Dtos;
using System.Data;
using Yuebon.Commons.IDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/Chaochi/[controller]")]
    public class BlackListController : AreaApiController<BlackList, BlackListOutputDto, BlackListInputDto, IBlackListService, string>
    {
        private readonly IDbContextCore _ybdbcontext;
        private readonly ISendMailInfoService _sendMailInfoService;
        private readonly IToDoListService _toDoListService;
        private readonly IOrganizeService _organizeService;
        private readonly Security.IServices.IUserService _userService;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="sendMailInfoService"></param>
        /// <param name="toDoListService"></param>
        /// <param name="userService"></param>
        /// <param name="organizeService"></param>
        public BlackListController(IBlackListService _iService, IDbContextCore ybdbcontext, ISendMailInfoService sendMailInfoService, IToDoListService toDoListService, Security.IServices.IUserService userService, Security.IServices.IOrganizeService organizeService) : base(_iService)
        {
            iService = _iService;
            _ybdbcontext = ybdbcontext;
            _sendMailInfoService = sendMailInfoService;
            _toDoListService = toDoListService;
            _organizeService = organizeService;
            _userService = userService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(BlackList info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.CreatorUserDeptId = CurrentUser.DeptId;
            info.Age = (DateTime.Now.Year - (Convert.ToInt32(info.Birthday.Split("-")[0]) + 1911)).ToString();
            info.ReportDept = _organizeService.GetAsync(CurrentUser.DeptId).Result.FullName;
            info.ResultState = "資料蒐集中";
            info.LastModifyTime = DateTime.Now;
            info.LastModifyUserId = CurrentUser.UserId;
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
        protected override void OnBeforeUpdate(BlackList info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(BlackList info)
        {
            //info.DeleteMark = true;
            //info.DeleteTime = DateTime.Now;
            //info.DeleteUserId = CurrentUser.UserId;
        }

        public override async Task<IActionResult> InsertAsync(BlackListInputDto tinfo)
        {
            var result = new CommonResult();
            var info = tinfo.MapTo<BlackList>();
            OnBeforeInsert(info);
            var CRMresult = await iService.SendApproval(info);
            var b_Id = CRMresult.ToObject<CRMAPIResult>().index;
            if (b_Id == -1) {
                result.Success = false;
                result.ErrCode = ErrCode.err1;
                result.ErrMsg = "請求入居者審查API失敗 請連絡相關人員";
                return ToJsonContent(result);
            }
            info.B_Id = b_Id;
            long ln = await iService.InsertAsync(info).ConfigureAwait(false);
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
        /// 更新
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        public override async Task<IActionResult> UpdateAsync(BlackListInputDto inInfo)
        {
            var result = new CommonResult();
            var blackList_DB = await iService.GetAsync(inInfo.Id);
            //var blackList = inInfo.MapTo(blackList_DB);
            blackList_DB.TrialNote = inInfo.TrialNote;
            blackList_DB.ResultState = inInfo.ResultState;
            try {
                OnBeforeUpdate(blackList_DB);
                await iService.UpdateAsync(blackList_DB, blackList_DB.Id);

                result.Success = true;
                result.ErrCode = ErrCode.successCode;
            } catch (Exception ex) {
                Log4NetHelper.Error("blacklist新增失敗", ex);
                result.Success = false;
                result.ErrCode = ErrCode.err43001;
                result.ErrMsg = ex.Message;
                throw;
            }
            return ToJsonContent(result);
        }

        public override async Task<CommonResult<BlackListOutputDto>> GetById(string id)
        {
            var result = new CommonResult<BlackListOutputDto>();
            var info = await iService.GetOutDtoAsync(id);
            var org = await _organizeService.GetWhereAsync($"FullName = '{info.ReportDept}'");
            info.TrialUserId = org.ManagerId;
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
        /// 審查結果
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateTenantReviewResult")]
        [AllowAnonymous]
        public async Task<UpdateTenantReviewResultModel> UpdateTenantReviewResult(UpdateTenantReviewInputDtos updateTenantReviewInputDtos)
        {
            var result = new UpdateTenantReviewResultModel();
            try {
                string token = Request.Headers.GetValue("API-Token").FirstOrDefault();
                if (token != "8f3f7d89711186853e8065368") {
                    result.StatusCode = "404";
                    result.Message = "無法使用";
                    return result;
                } else {
                    var blackList = await iService.GetWhereAsync($" B_Id = '{updateTenantReviewInputDtos.B_Id}' ");
                    if (blackList is null) {
                        result.StatusCode = "404";
                        result.Message = "查無此index資料";
                        return result;
                    } else {
                        blackList.CriminalWanted = updateTenantReviewInputDtos.Details.CriminalWanted;
                        blackList.CatchingFugitives = updateTenantReviewInputDtos.Details.CatchingFugitives;
                        blackList.CriminalLib = updateTenantReviewInputDtos.Details.CriminalLib;
                        blackList.Domestic = updateTenantReviewInputDtos.Details.Domestic;
                        blackList.TraficFines = updateTenantReviewInputDtos.Details.TraficFines;
                        blackList.FuelPenaltyExpireds = updateTenantReviewInputDtos.Details.FuelPenaltyExpireds;
                        blackList.ConsumerDebt = updateTenantReviewInputDtos.Details.ConsumerDebt;
                        blackList.RentSubsidy = updateTenantReviewInputDtos.Details.RentSubsidy;
                        blackList.LawBank = updateTenantReviewInputDtos.Details.LawBank;
                        //回傳包含吸毒字樣直接黑名單
                        var jsonString = updateTenantReviewInputDtos.ToJson();
                        if (jsonString.Contains("吸毒")) {
                            blackList.ResultState = "黑名單";
                        } else if (blackList.CriminalWanted == "查無資料" && blackList.CatchingFugitives == "查無資料" && blackList.CriminalLib == "查無資料" && blackList.Domestic == "查無資料" && blackList.TraficFines == "查無資料" && blackList.FuelPenaltyExpireds == "查無資料" && blackList.ConsumerDebt == "查無資料" && blackList.RentSubsidy == "查無資料" && blackList.LawBank == "查無資料") {
                            blackList.ResultState = "白名單"; //回傳資料若全部都="查無資料" 直接白名單
                        } else {
                            blackList.ResultState = "灰名單";
                        }
                        blackList.LastModifyTime = DateTime.Now;
                        var isSuccess = await iService.UpdateAsync(blackList, blackList.Id);
                        //var applicationUser = await _userService.GetAsync(blackList.CreatorUserId); //業務
                        //if (blackList.ResultState == "灰名單") {
                        //    //新增至待辦事項

                        //    var usSuccess2 = await _toDoListService.InsertAsync(new ToDoList() {
                        //        Type = "黑名單審核",
                        //        TypeId = blackList.Id,
                        //        Name = $"[簽核]*黑名單管理【申請人:{applicationUser.RealName}，審核對象:{blackList.Name}】",
                        //        Status = "待審核",
                        //        Account = _userService.GetStoreManager(applicationUser.Account),
                        //        CreatorTime = DateTime.Now,
                        //        CreatorUserId = blackList.CreatorUserId
                        //    });
                        //    //灰名單要寄信給店長，請店長審核
                        //    await _sendMailInfoService.InsertAsync(new SendMailInfo() {
                        //        RefKey = blackList.Id,
                        //        Subject = $"[簽核]*入居者審查：【申請人：{applicationUser.RealName}，審查結果：請協助判定名單】",
                        //        Recipient = _userService.Get(_userService.GetStoreManager(applicationUser.Account)).Email,
                        //        Enable = true,
                        //        SendTime = DateTime.Now,
                        //        Body = AddMailBodyAsHTMLTemplate_Grey(blackList),
                        //    });
                        //} else if (blackList.ResultState == "黑名單") {
                        //    //黑名單直接寄給業務
                        //    await _sendMailInfoService.InsertAsync(new SendMailInfo() {
                        //        RefKey = blackList.Id,
                        //        Subject = $"[回函]*入居者審查：【申請人：{applicationUser.RealName}，審查結果：黑名單】",
                        //        Recipient = applicationUser.Email,
                        //        Enable = true,
                        //        SendTime = DateTime.Now,
                        //        Body = AddMailBodyAsHTMLTemplate_Black(blackList),
                        //    });
                        //}
                        if (isSuccess) {
                            result.StatusCode = "200";
                            result.Message = "OK";
                        } else {
                            result.StatusCode = "400";
                            result.Message = "更新失敗";
                        }

                    }
                }
            } catch (Exception) {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 評鑑結果 只更新Note欄位跟Rating欄位
        /// </summary>
        /// <returns></returns>
        [HttpPost("TenantReviewScoreCommentResult")]
        [AllowAnonymous]
        public async Task<UpdateTenantReviewResultModel> TenantReviewScoreCommentResult(UpdateTenantReviewScoreCommentInputDtos updateTenantReviewScoreCommentInputDtos)
        {
            var result = new UpdateTenantReviewResultModel();
            string token = Request.Headers.GetValue("API-Token").FirstOrDefault();
            if (token != "8f3f7d89711186853e8065368") {
                result.StatusCode = "404";
                result.Message = "無法使用";
                return result;
            } else {
                var blackList = await iService.GetWhereAsync($" B_Id = '{updateTenantReviewScoreCommentInputDtos.B_Id}' ");
                if (blackList is null) {
                    result.StatusCode = "404";
                    result.Message = "查無此index資料";
                    return result;
                } else {
                    blackList.LastModifyTime = DateTime.Now;
                    blackList.Note = updateTenantReviewScoreCommentInputDtos.Comment;
                    blackList.Rating = updateTenantReviewScoreCommentInputDtos.Score;


                    using IDbConnection conn = _ybdbcontext.GetDatabase().GetDbConnection();
                    conn.Open();
                    using var eftran = _ybdbcontext.GetDatabase().BeginTransaction();
                    var tran = eftran.GetDbTransaction();

                    try {
                        var isSuccess = await iService.UpdateAsync(blackList, blackList.Id, conn, tran);
                        var applicationUser = await _userService.GetAsync(blackList.CreatorUserId); //業務
                        if (blackList.ResultState == "灰名單") {
                            //新增至待辦事項

                            var usSuccess2 = await _toDoListService.InsertAsync(new ToDoList() {
                                Type = "入居者審查",
                                TypeId = blackList.Id,
                                Name = $"[簽核]*黑名單管理【申請人:{applicationUser.RealName}，審核對象:{blackList.Name}】",
                                Status = "待審核",
                               // Account = _userService.GetStoreManager(applicationUser.Account),
                                Account = applicationUser.ManagerId,
                                CreatorTime = DateTime.Now,
                                CreatorUserId = blackList.CreatorUserId
                            }, conn, tran);
                            //灰名單要寄信給店長，請店長審核
                            var manager = await _userService.GetAsync(applicationUser.ManagerId);
                            if (manager == null) {
                                tran.Rollback();
                                result.StatusCode = "400";
                                result.Message = "此筆資料對應業務無ManagerId!";
                                return result;
                            }
                            await _sendMailInfoService.InsertAsync(new SendMailInfo() {
                                RefKey = blackList.Id,
                                Subject = $"[簽核]*入居者審查：【申請人：{applicationUser.RealName}，審查結果：請協助判定名單】",
                                Recipient = manager.Email,
                                Enable = true,
                                SendTime = DateTime.Now,
                                Body = AddMailBodyAsHTMLTemplate_Grey(blackList),
                            }, conn, tran);
                        } else if (blackList.ResultState == "黑名單") {
                            //黑名單直接寄給業務
                            await _sendMailInfoService.InsertAsync(new SendMailInfo() {
                                RefKey = blackList.Id,
                                Subject = $"[回函]*入居者審查：【申請人：{applicationUser.RealName}，審查結果：黑名單】",
                                Recipient = applicationUser.Email,
                                Enable = true,
                                SendTime = DateTime.Now,
                                Body = AddMailBodyAsHTMLTemplate_Black(blackList),
                            }, conn, tran);
                        }
                        if (isSuccess) {
                            tran.Commit();
                            result.StatusCode = "200";
                            result.Message = "OK";
                        } else {
                            tran.Rollback();
                            result.StatusCode = "400";
                            result.Message = "更新失敗";
                        }
                    } catch (Exception) {
                        tran.Rollback();
                        throw;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 產生EmailBody內文html模板
        /// </summary>
        /// <param name="blackList"></param>
        /// <returns></returns>
        public string AddMailBodyAsHTMLTemplate_Black(BlackList blackList)
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
        <td width='70%' style='padding:8px;border-bottom:1px solid #fff'> {"黑名單"}
           </td>
           </tr>
          <tr>
           </td>
           </tr>
           </tbody>
<p><h3 style='color: red; font-size:20px'><b>您送入居審查資料結果為黑名單</b></h3></p>
           </table>
<p><h5>您已經瞭解並同意以下事項：<br/>
審查結果僅適用於本公司及與本公司直接或間接關係企業用於租賃關係管理、辨識身分、行銷廣宣或入居者審查等，或主管機關依法要求提供者。</h5></p>
<p><h3 style='color: red; font-size:20px'><b>***此信由系統發出，請勿直接回信！***</b></h3></p>";
            return html;
        }

        /// <summary>
        /// 產生EmailBody內文html模板
        /// </summary>
        /// <param name="blackList"></param>
        /// <returns></returns>
        public string AddMailBodyAsHTMLTemplate_Grey(BlackList blackList)
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
        <td width='70%' style='padding:8px;border-bottom:1px solid #fff'> {"請協助判定名單"}
           </td>
           </tr>
          <tr>
           </td>
           </tr>
           </tbody>
<p><h3 style='color: red; font-size:20px'><b>您有待審核表單，請至待辦事項審核</b></h3></p>
           </table>
<p><h3 style='color: red; font-size:20px'><b>***此信由系統發出，請勿直接回信！***</b></h3></p>";
            return html;
        }

        /// <summary>
        /// 
        /// </summary>
        public class UpdateTenantReviewResultModel
        {
            /// <summary>
            /// 狀態碼
            /// </summary>
            public string StatusCode { get; set; }

            /// <summary>
            /// 訊息
            /// </summary>
            public string Message { get; set; }
        }

        public class UpdateTenantReviewScoreCommentInputDtos
        {
            /// <summary>
            /// BKey
            /// </summary>
            public int B_Id { get; set; }

            /// <summary>
            /// 綜合評分
            /// </summary>
            public string Score { get; set; }

            /// <summary>
            /// 備註
            /// </summary>
            public string Comment { get; set; }
        }

        /// <summary>
        /// 更新用
        /// </summary>
        /// 
        [Serializable]
        public class UpdateTenantReviewInputDtos
        {
            /// <summary>
            /// BKey
            /// </summary>
            public int B_Id { get; set; }

            /// <summary>
            /// 結果
            /// </summary>
            public Details Details { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Details
        {
            /// <summary>
            /// 刑事通緝犯
            /// </summary>
            public string CriminalWanted { get; set; }

            /// <summary>
            /// 查捕中逃犯
            /// </summary>
            public string CatchingFugitives { get; set; }

            /// <summary>
            /// 查緝專刊
            /// </summary>
            public string CriminalLib { get; set; }

            /// <summary>
            /// /監護補助
            /// </summary>
            public string Domestic { get; set; }

            /// <summary>
            /// /交通罰鍰
            /// </summary>
            public string TraficFines { get; set; }

            /// <summary>
            /// 汽機車燃料稅
            /// </summary>
            public string FuelPenaltyExpireds { get; set; }

            /// <summary>
            /// 消債事件
            /// </summary>
            public string ConsumerDebt { get; set; }

            /// <summary>
            /// 租金補貼
            /// </summary>
            public string RentSubsidy { get; set; }

            /// <summary>
            /// 法源網
            /// </summary>
            public string LawBank { get; set; }
            /* 原始資料 不使用了
            /// <summary>
            /// 
            /// </summary>
            public ConsumerDebt[] ConsumerDebts { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public CriminalRecord[] CriminalRecords { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public CriminalCrawler[] CriminalCrawlers { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public CurrentWanted[] CurrentWanteds { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public Domestic[] Domestics { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public FuelPenaltyBasic[] FuelPenaltyBasics { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public FuelPenaltyExpired[] FuelPenaltyExpireds { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public TraficPenalty[] TraficPenaltys { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public Wanted[] Wanteds { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string StatusCode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string Message { get; set; }

            */
        }

        #region 子類

        /// <summary>
        /// 
        /// </summary>
        public class ConsumerDebt
        {
            /// <summary>
            /// 法庭
            /// </summary>
            public string Court { get; set; }

            /// <summary>
            /// 標題
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// NT發生日期
            /// </summary>
            public string Date { get; set; }

            /// <summary>
            /// NT內容
            /// </summary>
            public string Content { get; set; }

            /// <summary>
            /// 建立日期
            /// </summary>
            public string CreatedDate { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class CriminalRecord
        {
            /// <summary>
            /// 判決日
            /// </summary>
            public string JudgeDate { get; set; }//

            /// <summary>
            /// 理由
            /// </summary>
            public string Reason { get; set; }//

            /// <summary>
            /// 標題
            /// </summary>
            public string Title { get; set; }//

            /// <summary>
            /// 建立日期
            /// </summary>
            public string CreatedDate { get; set; }//
        }

        /// <summary>
        /// 
        /// </summary>
        public class CriminalCrawler
        {
            /// <summary>
            /// 狀態 ps: normal代表不是查捕中通緝犯
            ///abnormal代表是,此時理由才會有資料
            /// </summary>
            public string Status { get; set; }

            /// <summary>
            /// 理由
            /// </summary>
            public string Reason { get; set; }//

            /// <summary>
            /// 建立日期
            /// </summary>
            public string CreatedDate { get; set; }//
        }

        /// <summary>
        /// 
        /// </summary>
        public class CurrentWanted
        {
            /// <summary>
            /// 姓名
            /// </summary>
            public string Name { get; set; }//

            /// <summary>
            /// 身份證字號
            /// </summary>
            public string IdNumber { get; set; }//

            /// <summary>
            /// 理由
            /// </summary>
            public string Reason { get; set; }//

            /// <summary>
            /// 建立日期
            /// </summary>
            public string CreatedDate { get; set; }//
        }

        /// <summary>
        /// 
        /// </summary>
        public class Domestic
        {
            /// <summary>
            /// 法庭
            /// </summary>
            public string Court { get; set; }//

            /// <summary>
            /// 標題
            /// </summary>
            public string Title { get; set; }//

            /// <summary>
            /// 張貼日期
            /// </summary>
            public string PostDate { get; set; }//

            /// <summary>
            /// 發布日期
            /// </summary>
            public string PublishDate { get; set; }//

            /// <summary>
            /// 內容
            /// </summary>
            public string Content { get; set; }//

            /// <summary>
            /// 建立日期
            /// </summary>
            public string CreatedDate { get; set; }//
        }

        /// <summary>
        /// 
        /// </summary>
        public class FuelPenaltyBasic
        {
            /// <summary>
            /// 交通工具
            /// </summary>
            public string Transportation { get; set; }//

            /// <summary>
            /// 車號
            /// </summary>
            public string CarNumber { get; set; }//

            /// <summary>
            /// 期間
            /// </summary>
            public string Period { get; set; }//

            /// <summary>
            /// 應付日期
            /// </summary>
            public string ShouldPaidDate { get; set; }//

            /// <summary>
            /// 監理站
            /// </summary>
            public string SupervisoryDepartment { get; set; }//

            /// <summary>
            /// 費用
            /// </summary>
            public string Amount { get; set; }//

            /// <summary>
            /// 備註
            /// </summary>
            public string Comment { get; set; }//

            /// <summary>
            /// 建立日期
            /// </summary>
            public string CreatedDate { get; set; }//
        }

        /// <summary>
        /// 
        /// </summary>
        public class FuelPenaltyExpired
        {
            /// <summary>
            /// 交通工具
            /// </summary>
            public string Transportation { get; set; }//

            /// <summary>
            /// 車號
            /// </summary>
            public string CarNumber { get; set; }//

            /// <summary>
            /// 帳單號
            /// </summary>
            public string BillNumber { get; set; }//

            /// <summary>
            /// 期間
            /// </summary>
            public string Period { get; set; }//

            /// <summary>
            /// 應付日期
            /// </summary>
            public string ShouldPaidDate { get; set; }//

            /// <summary>
            /// 監理站
            /// </summary>
            public string SupervisoryDepartment { get; set; }//

            /// <summary>
            /// 費用
            /// </summary>
            public string Amount { get; set; }//

            /// <summary>
            /// 備註
            /// </summary>
            public string Comment { get; set; }//

            /// <summary>
            /// 建立日期
            /// </summary>
            public string CreatedDate { get; set; }//
        }

        /// <summary>
        /// 
        /// </summary>
        public class TraficPenalty
        {
            /// <summary>
            /// 發生日期
            /// </summary>
            public string ViolationDate { get; set; }//

            /// <summary>
            /// 應付日期
            /// </summary>
            public string ShouldPaidDate { get; set; }//

            /// <summary>
            /// 內容
            /// </summary>
            public string Content { get; set; }//

            /// <summary>
            /// 費用
            /// </summary>
            public string Amount { get; set; }//

            /// <summary>
            /// 建立日期
            /// </summary>
            public string CreatedDate { get; set; }//
        }

        /// <summary>
        /// 
        /// </summary>
        public class Wanted
        {
            /// <summary>
            /// 狀態(是否有被通緝結果分為1.查無資料2有資料)
            /// </summary>
            public string Status { get; set; }//

            /// <summary>
            /// 建立日期
            /// </summary>
            public string CreatedDate { get; set; }//
        }

        #endregion

    }
}