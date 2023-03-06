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

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class RemittanceLController : AreaApiController<RemittanceL, RemittanceLOutputDto,RemittanceLInputDto,IRemittanceLService,string>
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        public RemittanceLController(IRemittanceLService _iService) : base(_iService)
        {
            iService = _iService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(RemittanceL info)
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
        protected override void OnBeforeUpdate(RemittanceL info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(RemittanceL info)
        {
        }


        /// <summary>
        /// 新建出租人匯款資料
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        /// 
        [HttpPost("Insert")]
        [YuebonAuthorize("")]
        public override async Task<IActionResult> InsertAsync(RemittanceLInputDto tinfo)
        {
            var result = new CommonResult();
            var remittLN = tinfo.MapTo<RemittanceL>();
            //檢查是否已存在
            var dataList = await iService.GetListWhereAsync($" CustomerLId = '{tinfo.CustomerLId}' ");
            var isExists = dataList.Any(x => x.LANo == remittLN.LANo && x.LAName == remittLN.LAName);
            if (isExists) {
                result.Success = false;
                result.ErrMsg = "此帳戶已存在";
                return ToJsonContent(result);
            } else {
                try {
                    remittLN.CreatorUserId = CurrentUser.UserId;
                    remittLN.CreatorTime = DateTime.Now;
                    remittLN.LastModifyTime = DateTime.Now;
                    remittLN.LastModifyUserId = CurrentUser.UserId;
                    remittLN.CustomerLId = tinfo.CustomerLId;
                    remittLN.IDNo = tinfo.IDNo;
                    var inserSucess = await iService.InsertAsync(remittLN).ConfigureAwait(false);
                    if (inserSucess > 0) {
                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } else {
                        result.Success = false;
                        result.ErrMsg = "新建匯款資料錯誤!";
                    }
                } catch (Exception ex) {
                    Log4NetHelper.Error("新建匯款資料錯誤!", ex);
                    result.Success = false;
                    result.ErrMsg = ex.Message;
                    return ToJsonContent(result);
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 更新出租人匯款資料
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        /// 
        [HttpPost("Update")]
        [YuebonAuthorize("")]
        public override async Task<IActionResult> UpdateAsync(RemittanceLInputDto inInfo)
        {
            var result = new CommonResult();
            var remittanceDB = await iService.GetAsync(inInfo.Id);
            var remittanceLN = inInfo.MapTo(remittanceDB);
            try {
                var isSuccess = await iService.UpdateAsync(remittanceLN, inInfo.Id);
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
                Log4NetHelper.Error("更新匯款資料錯誤!", ex);
            }
            return ToJsonContent(result);
        }

        [HttpGet("GetById")]
        [YuebonAuthorize("")]
        public override Task<CommonResult<RemittanceLOutputDto>> GetById(string id)
        {
            return base.GetById(id);
        }

        /// <summary>
        /// 根據身分證字號或統編取得匯款資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRemittancesByIDNo")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetRemittancesByIDNo(string idNo)
        {
            var result = new CommonResult();
            if (idNo != null) {
                var dataList = await iService.GetListWhereAsync($"IDNo = '{idNo}'");
                result.ResData = dataList;
            }
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }


        /// <summary>
        /// 取得此出租人的匯款資料(根據流水號)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRemittancesByCustomerLId")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetRemittancesByCustomerLId(string customerLId)
        {
            var result = new CommonResult();
            if (customerLId != null) {
                var dataList = await iService.GetListWhereAsync($"CustomerLId = '{customerLId}'");
                result.ResData = dataList;
            }
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }
    }
}