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
using Yuebon.AspNetCore.Mvc;
using System.Linq;
using System.Data;
using Yuebon.Commons.IDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extensions;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class SendMailInfoController : AreaApiController<SendMailInfo, SendMailInfoOutputDto, SendMailInfoInputDto, ISendMailInfoService, string>
    {
        private readonly IDbContextCore _ybContext;
        private readonly IComplaintService _complaintService;
        private readonly IComplaintNoticeMailService _complaintNoticeMailService;
        private readonly Security.IServices.IUserService _userService;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="ybContext"></param>
        /// <param name="complaintService"></param>
        /// <param name="complaintNoticeMailService"></param>
        /// <param name="userService"></param>
        public SendMailInfoController(ISendMailInfoService _iService, IDbContextCore ybContext, IComplaintService complaintService, IComplaintNoticeMailService complaintNoticeMailService, Yuebon.Security.IServices.IUserService userService) : base(_iService)
        {
            iService = _iService;
            _ybContext = ybContext;
            _complaintService = complaintService;
            _complaintNoticeMailService = complaintNoticeMailService;
            _userService = userService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(SendMailInfo info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(SendMailInfo info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(SendMailInfo info)
        {

        }

        /// <summary>
        /// 異步新增數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <param name="ccode"></param>
        /// <returns></returns>
        [HttpPost("ComplaintInsert")]
        //[YuebonAuthorize("Add")]
        public async Task<IActionResult> ComplaintInsert(SendMailInfoInputDto tinfo, string ccode)
        {
            CommonResult result = new CommonResult();
            var info = tinfo.MapTo<SendMailInfo>();
            IEnumerable<string> userIdList = _complaintNoticeMailService.GetListWhereAsync($" CCode = '{ccode}' ").Result.Select(x => x.UserId);
            List<string> emails = new List<string>();
            foreach (var item in userIdList) {
                var user = await _userService.GetWhereAsync($" Id = '{item}' ");
                if (user != null) {
                    emails.Add(user.Email);
                }
            }
            var recipient = string.Join(",", emails.ToArray());
            info.Recipient = recipient;
            info.Enable = true;
            info.SendTime = DateTime.Now;
            OnBeforeInsert(info);

            try {
                using IDbConnection conn = _ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = _ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();
                try {
                    var complaint = await _complaintService.GetWhereAsync($" CCode = '{ccode}' ", conn, tran);
                    info.Body = AddMailBodyAsHTMLTemplate(complaint);
                    await iService.InsertAsync(info, conn, tran).ConfigureAwait(false);
                    //更新Complaint的SensMailCount
                    if (complaint is not null) {
                        complaint.SendMailCount = complaint.SendMailCount + 1;
                        await _complaintService.UpdateAsync(complaint, complaint.Id, conn, tran);
                        eftran.Commit();
                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                    } else {
                        eftran.Rollback();
                        Log4NetHelper.Error("查無此客訴單");
                        result.ErrMsg = ErrCode.err43001;
                        result.ErrCode = "43001";
                    }
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("新增失敗", ex);
                    result.ErrMsg = ErrCode.err43001;
                    result.ErrCode = "43001";
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
        /// 產生EmailBody內文html模板
        /// </summary>
        /// <param name="complaint"></param>
        /// <returns></returns>
        public string AddMailBodyAsHTMLTemplate(Complaint complaint)
        {
            var userName = _userService.Get(complaint.HandleUser).RealName;
            if (string.IsNullOrEmpty(userName)) {
                userName = "";
            }
            string html = @$"<h3 style='color:#eb6100;font-size:16px'> 兆基通知信
</h3>這份通知是在提醒收件者，請留意此客訴單號，及處理結果，有任何問題，請聯絡客訴處理員：{userName}
<table width='100%' border='0' align='left' cellpadding='0' cellspacing='0' bgcolor='#f7f7f7' style='border-top:2px solid #c4b993;font-family:'\005fae\008edf\006b63\009ed1\009ad4',Arial,Helvetica,Verdana'>
<tbody>
  <tr>
  <td width='30%' style='background-color:#eaeaea;padding:8px;border-bottom:1px solid #fff'> 投訴人姓名：
</td>
<td width='70%' style='padding:8px;border-bottom:1px solid #fff'> {complaint.Name}
   </td>
   </tr>
   <tr>
   <td width='30%' style='background-color:#eaeaea;padding:8px;border-bottom:1px solid #fff'> 投訴類別：
</td>
<td width='70%' style='padding:8px;border-bottom:1px solid #fff' > {complaint.ComplaintType}
   </td>
   </tr>
  <tr>
  <td width='30%' style='background-color:#eaeaea;padding:8px;border-bottom:1px solid #fff'> 投訴部門\公司：
</td>
<td width='70%' style='padding:8px;border-bottom:1px solid #fff'> {complaint.ComplaintDept}
   </td>
   </tr>
  <tr>
  <td width='30%' style='background-color:#eaeaea;padding:8px;border-bottom:1px solid #fff'> 投訴對象姓名：
</td>
<td width='70%' style='padding:8px;border-bottom:1px solid #fff'> {complaint.Complainee}
   </td>
   </tr>
  <tr>
  <td width='30%' style='background-color:#eaeaea;padding:8px;border-bottom:1px solid #fff'> 有糾紛的合約編號：
</td>
<td width='70%' style='padding:8px;border-bottom:1px solid #fff'> {complaint.CId}
   </td>
   </tr>
  <tr>
  <td width='30%' style='background-color:#eaeaea;padding:8px;border-bottom:1px solid #fff'> 投訴事由：
</td>
<td width='70%' style='padding:8px;border-bottom:1px solid #fff'> {complaint.ComplaintReason}
   </td>
   </tr>
  <tr>
  <td width='30%' style='background-color:#eaeaea;padding:8px;border-bottom:1px solid #fff'> 投訴人意見提供：
</td>
<td width='70%' style='padding:8px;border-bottom:1px solid #fff'> {complaint.ProvideAdvice}
   </td>
   </tr>
  <tr>
  <td width='30%' style='background-color:#eaeaea;padding:8px;border-bottom:1px solid #fff'> 客服人員處理備註：
</td>
<td width='70%' style='padding:8px;border-bottom:1px solid #fff'> {complaint.HandleNote}
   </td>
   </tr>
   </tbody>
   </table> <p><h3 style='color: red; font-size:20px'><b>***此信由系統發出，請勿直接回信！***</b></h3></p>";
            return html;
        }
    }
}