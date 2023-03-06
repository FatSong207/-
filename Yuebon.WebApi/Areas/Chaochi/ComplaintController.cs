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
using Yuebon.Security.IServices;
using System.Linq;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Dtos;
using Yuebon.AspNetCore.Mvc.Filter;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class ComplaintController : AreaApiController<Complaint, ComplaintOutputDto,ComplaintInputDto,IComplaintService,string>
    {
        private readonly IComplaintNoticeMailService _complaintNoticeMailService;
        private readonly IOrganizeService _organizeService;
        private readonly Security.IServices.IUserService _userService;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="complaintNoticeMailService"></param>
        /// <param name="organizeService"></param>
        /// <param name="userService"></param>
        public ComplaintController(IComplaintService _iService,IComplaintNoticeMailService complaintNoticeMailService,Yuebon.Security.IServices.IOrganizeService organizeService,Yuebon.Security.IServices.IUserService userService) : base(_iService)
        {
            iService = _iService;
            _complaintNoticeMailService = complaintNoticeMailService;
            _organizeService = organizeService;
            _userService = userService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Complaint info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
        }
        
        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(Complaint info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Complaint info)
        {

        }

        /// <summary>
        /// 異步分頁查詢
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerSearchAsync")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> FindWithPagerSearchAsync(SearchInputDto<ComplaintOutputDto> search)
        {
            CommonResult<PageResult<Complaint>> result = new CommonResult<PageResult<Complaint>>();
            result.ResData = await iService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(ComplaintInputDto inInfo)
        {
            var result = new CommonResult();
            var complaint_DB =await iService.GetAsync(inInfo.Id);
            bool isNew;

            if (string.IsNullOrEmpty(complaint_DB.HandleUser)) {
                isNew = true;
            } else {
                isNew = false;
            }
            var complaint = inInfo.MapTo(complaint_DB);
            if (isNew) {
                complaint.HandleUser = CurrentUser.UserId;
            }

            OnBeforeUpdate(complaint);

            try {
                complaint.State = "處理中";
                await iService.UpdateAsync(complaint,inInfo.Id);

                result.Success = true;
                result.ErrCode = ErrCode.successCode;
            } catch (Exception ex) {
                Log4NetHelper.Error("更新失敗", ex);
                result.Success=false;
                result.ErrCode = ErrCode.err43002;
                result.ErrMsg = "更新失敗";
            }
            return ToJsonContent(result);
        }


        /// <summary>
        /// 送審
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        [HttpPost("SendTrial")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> SendTrial(ComplaintInputDto inInfo)
        {
            var result = new CommonResult();
            var complaint_DB = await iService.GetAsync(inInfo.Id);
            bool isNew;

            if (string.IsNullOrEmpty(complaint_DB.HandleUser)) {
                isNew = true;
            } else {
                isNew = false;
            }
            var complaint = inInfo.MapTo(complaint_DB);
            if (isNew) {
                complaint.HandleUser = CurrentUser.UserId;
            }

            OnBeforeUpdate(complaint);

            try {
                complaint.State = "已處理";
                complaint.SendTrialTime = DateTime.Now;
                await iService.UpdateAsync(complaint, inInfo.Id);

                result.Success = true;
                result.ErrCode = ErrCode.successCode;
            } catch (Exception ex) {
                Log4NetHelper.Error("更新失敗", ex);
                result.Success = false;
                result.ErrCode = ErrCode.err43002;
                result.ErrMsg = "更新失敗";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 結案
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        [HttpPost("EndCase")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> EndCase(ComplaintInputDto inInfo)
        {
            var result = new CommonResult();
            var complaint_DB = await iService.GetAsync(inInfo.Id);
            var complaint = inInfo.MapTo(complaint_DB);

            OnBeforeUpdate(complaint);

            try {
                complaint.State = "已結案";
                complaint.EndCaseTime = DateTime.Now;
                await iService.UpdateAsync(complaint, inInfo.Id);

                result.Success = true;
                result.ErrCode = ErrCode.successCode;
            } catch (Exception ex) {
                Log4NetHelper.Error("更新失敗", ex);
                result.Success = false;
                result.ErrCode = ErrCode.err43002;
                result.ErrMsg = "更新失敗";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("AddNoticeMail")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> AddNoticeMail(ComplaintNoticeMailInputDto inputDto)
        {
            var result = new CommonResult();
            //檢查是否已存在
            var list =await _complaintNoticeMailService.GetListWhereAsync($" CCode = '{inputDto.CCode}' ");
            var isExists = list.Any(x => x.UserId == inputDto.UserId);
            if (isExists) {
                result.ErrCode = ErrCode.err43001;
                result.Success = false;
                result.ErrMsg = "不允許有重複資料";
                return ToJsonContent(result);
            }
            try {
                var noticeMail = inputDto.MapTo<ComplaintNoticeMail>();
                var user =await _userService.GetAsync(noticeMail.UserId);
                var org = await _organizeService.GetAsync(user.DepartmentId);
                noticeMail.UserName = user.RealName;
                noticeMail.DeptId = org.Id;
                noticeMail.DeptName = org.FullName;
                await _complaintNoticeMailService.InsertAsync(noticeMail);
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
            } catch (Exception ex) {
                Log4NetHelper.Error("新增錯誤!", ex);
                result.ErrCode = ErrCode.err43001;
                result.Success = false;
                result.ErrMsg = "新增錯誤";
                throw;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ccode"></param>
        /// <returns></returns>
        [HttpGet("GetNoticeMailListByCCode")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> GetNoticeMailListByCCode(string ccode)
        {
            var result = new CommonResult();
            
            try {
                var dataList = await _complaintNoticeMailService.GetListWhereAsync($" CCode = '{ccode}' ");
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = dataList;
            } catch (Exception ex) {
                Log4NetHelper.Error("列表獲取錯誤!", ex);
                result.ErrCode = ErrCode.err40110;
                result.Success = false;
                result.ErrMsg = "列表獲取錯誤";
                throw;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 刪除NoticeMail(原本是刪除ComplaintNoticeMail，但她不需要刪除的功能所以給通知信table)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("DeleteComplaintNoticeMail")]
        public async Task<IActionResult> DeleteComplaintNoticeMail(DeletesInputDto info)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            if (typeof(string) == typeof(string)) {
                where = "id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            } else if (typeof(string) == typeof(int)) {
                where = "id in (" + info.Ids.Join(",") + ")";
            }
            if (!string.IsNullOrEmpty(where)) {
                bool bl = await _complaintNoticeMailService.DeleteBatchWhereAsync(where).ConfigureAwait(false);
                if (bl) {
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.ErrMsg = ErrCode.err43003;
                    result.ErrCode = "43003";
                }
            }
            return ToJsonContent(result);
        }
    }
}