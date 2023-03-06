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
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Json;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using System.Linq;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class SLMAController : AreaApiController<SLMA, SLMAOutputDto,SLMAInputDto,ISLMAService,string>
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        public SLMAController(ISLMAService _iService) : base(_iService)
        {
            iService = _iService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(SLMA info)
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
        protected override void OnBeforeUpdate(SLMA info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 獲取二房東/管理方資料清單
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSLMAList")]
        //[YuebonAuthorize("List")]
        public async Task<IActionResult> GetSLMAList(string slid)
        {
            CommonResult result = new CommonResult();

            try {
                IEnumerable<SLMA> resultList = await iService.GetDistinctByFieldAsync("Address", $"SLMAID = '{slid}'");
                List<SLMAOutputDto> finalList = resultList.MapTo<SLMAOutputDto>();

                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = finalList;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取二房東/管理方資訊異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取二房東/管理方電話清單
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSLMATel")]
        //[YuebonAuthorize("List")]
        public async Task<IActionResult> GetSLMATel(string slid, string address)
        {
            CommonResult result = new CommonResult();

            try {
                IEnumerable<SLMA> resultList = await iService.GetDistinctByFieldAsync("Tel", $"SLMAID = '{slid}' AND Address = N'{address}'");
                List<SLMAOutputDto> finalList = resultList.MapTo<SLMAOutputDto>();

                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = finalList;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取二房東/管理方電話清單異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取租賃住宅管理人員資訊清單
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSIList")]
        //[YuebonAuthorize("List")]
        public async Task<IActionResult> GetSIList(string slid, string address)
        {
            CommonResult result = new CommonResult();

            try {
                IEnumerable<SLMA> resultList = await iService.GetDistinctByFieldAsync("SIName,SILRNo", $"SLMAID = '{slid}' AND Address = N'{address}'");
                List<SLMAOutputDto> finalList = resultList.MapTo<SLMAOutputDto>();

                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = finalList;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取租賃住宅管理人員清單異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取不動產經紀人資訊清單
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAGList")]
        //[YuebonAuthorize("List")]
        public async Task<IActionResult> GetAGList(string slid, string address)
        {
            CommonResult result = new CommonResult();

            try {
                IEnumerable<SLMA> resultList = await iService.GetDistinctByFieldAsync("AGName,AGLRNo", $"SLMAID = '{slid}' AND Address = N'{address}'");
                List<SLMAOutputDto> finalList = resultList.MapTo<SLMAOutputDto>();

                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = finalList;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取不動產經紀人資訊清單異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取租賃住宅管理人員資訊
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSuperintendent")]
        //[YuebonAuthorize("List")]
        public async Task<IActionResult> GetSuperintendent(string mname)
        {
            CommonResult result = new CommonResult();

            try {
                SLMA info = await iService.GetWhereAsync($"SIName = '{mname}'");
                SLMAOutputDto finalInfo = info.MapTo<SLMAOutputDto>();

                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = finalInfo;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取租賃住宅管理人員資訊", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取不動產經紀人資訊
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAgent")]
        //[YuebonAuthorize("List")]
        public async Task<IActionResult> GetAgent(string sname)
        {
            CommonResult result = new CommonResult();

            try {
                SLMA info = await iService.GetWhereAsync($"AGName = '{sname}'");
                SLMAOutputDto finalInfo = info.MapTo<SLMAOutputDto>();

                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = finalInfo;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取不動產經紀人資訊", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return ToJsonContent(result);
        }

        // Address,MName,SName

        /// <summary>
        /// 下載當前套用CSV
        /// </summary>
        /// <returns></returns>
        [HttpGet("DownloadCurrentSLMAFile")]
        //[YuebonAuthorize("List")]
        [LogNotMethod]
        public async Task<IActionResult> DownloadCurrentSLMAFile()
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            var di = new DirectoryInfo(@$"{path}SLMA/");
            var fileName = di.GetFiles().Where(x => x.Extension == ".csv").OrderByDescending(x => x.LastWriteTime).FirstOrDefault().Name;

            var pdf = System.IO.File.OpenRead(@$"{path}SLMA/{fileName}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.csv", out contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllCompanyName")]
        public async Task<IActionResult> GetAllCompanyName()
        {
            var result = new CommonResult();
            try {
                var data = await iService.GetDistinctByFieldAsync(" Name ", " 1=1 ");
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = data;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        [HttpGet("GetAddressByCompanyName")]
        public async Task<IActionResult> GetAddressByCompanyName(string companyName)
        {
            var result = new CommonResult();
            try {
                var data = await iService.GetDistinctByFieldAsync(" Address ", $" Name='{companyName}' ");
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = data;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpGet("GetTelByCompanyNameAndAddress")]
        public async Task<IActionResult> GetTelByCompanyNameAndAddress(string companyName, string address)
        {
            var result = new CommonResult();
            try {
                var data = await iService.GetDistinctByFieldAsync(" Tel ", $" Name='{companyName}' and Address='{address}' ");
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = data;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return ToJsonContent(result);
        }

        public class test
        {
            public string companyName { get; set; }
            public string address { get; set; }
            public string Tel { get; set; }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="companyName"></param>
        ///// <param name="address"></param>
        ///// <param name="Tel"></param>
        ///// <returns></returns>
        //[HttpPost("GetDataByCAT")]
        //public async Task<IActionResult> GetDataByCAT(test test)
        //{
        //    var result = new CommonResult();
        //    try {
        //        var data =await iService.GetWhereAsync($" Name='{test.companyName}' and Address='{test.address}' and Tel='{test.Tel}'");
        //        //var data = await iService.GetDistinctByFieldAsync(" Tel ", $" Name='{companyName}' and Address='{address}' ");
        //        result.Success = true;
        //        result.ErrCode = ErrCode.successCode;
        //        result.ResData = data;
        //    } catch (Exception ex) {
        //        Log4NetHelper.Error("獲取異常", ex);
        //        result.ErrMsg = ErrCode.err40110;
        //        result.ErrCode = "40110";
        //    }
        //    return ToJsonContent(result);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="address"></param>
        /// <param name="tel"></param>
        /// <returns></returns>
        [HttpGet("GetDataByCAT")]
        public async Task<IActionResult> GetDataByCAT(string companyName, string address, string tel)
        {
            var result = new CommonResult();
            try {
                var data = await iService.GetWhereAsync($" Name='{companyName}' and Address='{address}' and Tel='{tel}'");
                //var data = await iService.GetDistinctByFieldAsync(" Tel ", $" Name='{companyName}' and Address='{address}' ");
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = data;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return ToJsonContent(result);
        }
    }
}