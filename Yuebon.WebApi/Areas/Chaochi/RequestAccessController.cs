using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Chaochi.Core.IServices;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;

namespace Yuebon.WebApi.Areas.Chaochi
{
    /// <summary>
    /// 要求存取權接口
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class RequestAccessController : ApiController
    {
        private readonly ICustomerLNService _customerLNService;
        private readonly ICustomerLCService _customerLCService;
        private readonly ICustomerRNService _customerRNService;
        private readonly ICustomerRCService _customerRCService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerLNService"></param>
        /// <param name="customerRNService"></param>
        /// <param name="customerLCService"></param>
        /// <param name="customerRCService"></param>
        public RequestAccessController(ICustomerLNService customerLNService, ICustomerRNService customerRNService, ICustomerLCService customerLCService, ICustomerRCService customerRCService)
        {
            _customerLNService = customerLNService;
            _customerLCService = customerLCService;
            _customerRNService = customerRNService;
            _customerRCService = customerRCService;
        }

        /// <summary>
        /// 請求存取權
        /// </summary>
        /// <param name="requestAccessDto"></param>
        /// <returns></returns>
        [HttpPost("Request")]
        [YuebonAuthorize("")]
        public new async Task<CommonResult> Request(RequestAccessDto requestAccessDto)
        {
            var result = new CommonResult();
            var tel = requestAccessDto.Tel1 + "-" + requestAccessDto.Tel2;
            if (requestAccessDto.Role == "LandLord") {
                if (requestAccessDto.ID.Length == 10) {
                    var cln = await _customerLNService.GetByCustomerLNID(requestAccessDto.ID);
                    bool success = false;
                    if (cln != null) {
                        if (cln.LNName == requestAccessDto.Name && cln.LNBirthday == requestAccessDto.BirthDay) {
                            if (!string.IsNullOrEmpty(requestAccessDto.Cell) && requestAccessDto.Cell == cln.LNCell) {
                                if (tel == "-") {
                                    //檢查此CurrentUser是否已經擁有此客戶存取權
                                    if (!cln.CreatorUserId.Contains(CurrentUser.UserId)) {
                                        success = await _customerLNService.InsertAccess(cln, CurrentUser.UserId, CurrentUser.DeptId);
                                    }
                                } else {
                                    if (tel == cln.LNTel || tel == cln.LNTelN) {
                                        //檢查此CurrentUser是否已經擁有此客戶存取權
                                        if (!cln.CreatorUserId.Contains(CurrentUser.UserId)) {
                                            success = await _customerLNService.InsertAccess(cln, CurrentUser.UserId, CurrentUser.DeptId);
                                        }
                                    }
                                }
                            } else if (tel != "-" && (tel == cln.LNTel || tel == cln.LNTelN)) {
                                if (string.IsNullOrEmpty(requestAccessDto.Cell)) {
                                    //檢查此CurrentUser是否已經擁有此客戶存取權
                                    if (!cln.CreatorUserId.Contains(CurrentUser.UserId)) {
                                        success = await _customerLNService.InsertAccess(cln, CurrentUser.UserId, CurrentUser.DeptId);
                                    }
                                } else {
                                    if (requestAccessDto.Cell == cln.LNCell) {
                                        //檢查此CurrentUser是否已經擁有此客戶存取權
                                        if (!cln.CreatorUserId.Contains(CurrentUser.UserId)) {
                                            success = await _customerLNService.InsertAccess(cln, CurrentUser.UserId, CurrentUser.DeptId);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    ////檢查此CurrentUser是否已經擁有此客戶存取權
                    //if (!cln.CreatorUserId.Contains(CurrentUser.UserId)) {
                    //    success = await _customerLNService.InsertAccess(cln, CurrentUser.UserId, CurrentUser.DeptId);
                    //}
                    if (success) {
                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } else {
                        result.Success = false;
                        result.ErrMsg = "失敗";
                    }
                } else if (requestAccessDto.ID.Length == 8) {
                    var clc = await _customerLCService.GetByLCID(requestAccessDto.ID);
                    bool success = false;
                    if (clc != null) {
                        if (clc.LCName == requestAccessDto.Name && clc.LCID == requestAccessDto.ID && clc.LCRep == requestAccessDto.Rep && clc.LCTel == tel) {
                            //檢查此CurrentUser是否已經擁有此客戶存取權
                            if (!clc.CreatorUserId.Contains(CurrentUser.UserId)) {
                                success = await _customerLCService.InsertAccess(clc, CurrentUser.UserId, CurrentUser.DeptId);
                            }
                        }
                    }
                    if (success) {
                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } else {
                        result.Success = false;
                        result.ErrMsg = "失敗";
                    }
                } else {

                }
            } else if (requestAccessDto.Role == "Renter") {
                if (requestAccessDto.ID.Length == 10) {
                    var crnOutPut = await _customerRNService.GetCustomerByRNID(requestAccessDto.ID);
                    //因為GetCustomerByRNID() 回傳為OutPutDto，所以要經過一次轉換
                    var crn = crnOutPut.MapTo<CustomerRN>();
                    bool success = false;
                    if (crn != null) {
                        if (crn.RNName == requestAccessDto.Name && crn.RNBirthday == requestAccessDto.BirthDay) {
                            if (!string.IsNullOrEmpty(requestAccessDto.Cell) && requestAccessDto.Cell == crn.RNCell) {
                                if (tel == "-") {
                                    //檢查此CurrentUser是否已經擁有此客戶存取權
                                    if (!crn.CreatorUserId.Contains(CurrentUser.UserId)) {
                                        success = await _customerRNService.InsertAccess(crn, CurrentUser.UserId, CurrentUser.DeptId);
                                    }
                                } else {
                                    if (tel == crn.RNTel || tel == crn.RNTelN) {
                                        //檢查此CurrentUser是否已經擁有此客戶存取權
                                        if (!crn.CreatorUserId.Contains(CurrentUser.UserId)) {
                                            success = await _customerRNService.InsertAccess(crn, CurrentUser.UserId, CurrentUser.DeptId);
                                        }
                                    }
                                }
                            } else if (tel != "-" && (tel == crn.RNTel || tel == crn.RNTelN)) {
                                if (string.IsNullOrEmpty(requestAccessDto.Cell)) {
                                    //檢查此CurrentUser是否已經擁有此客戶存取權
                                    if (!crn.CreatorUserId.Contains(CurrentUser.UserId)) {
                                        success = await _customerRNService.InsertAccess(crn, CurrentUser.UserId, CurrentUser.DeptId);
                                    }
                                } else {
                                    if (requestAccessDto.Cell == crn.RNCell) {
                                        //檢查此CurrentUser是否已經擁有此客戶存取權
                                        if (!crn.CreatorUserId.Contains(CurrentUser.UserId)) {
                                            success = await _customerRNService.InsertAccess(crn, CurrentUser.UserId, CurrentUser.DeptId);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (success) {
                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } else {
                        result.Success = false;
                        result.ErrMsg = "失敗";
                    }
                } else if (requestAccessDto.ID.Length == 8) {
                    var crc = await _customerRCService.GetWhereAsync($" RCID = '{requestAccessDto.ID}' ");
                    bool success = false;
                    if (crc != null) {
                        if (crc.RCName == requestAccessDto.Name && crc.RCID == requestAccessDto.ID && crc.RCRep == requestAccessDto.Rep && crc.RCTel == tel) {
                            //檢查此CurrentUser是否已經擁有此客戶存取權
                            if (!crc.CreatorUserId.Contains(CurrentUser.UserId)) {
                                success = await _customerRCService.InsertAccess(crc, CurrentUser.UserId, CurrentUser.DeptId);
                            }
                        }
                    }
                    if (success) {
                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } else {
                        result.Success = false;
                        result.ErrMsg = "失敗";
                    }
                } else {

                }
            }
            return result;
        }
    }
}
