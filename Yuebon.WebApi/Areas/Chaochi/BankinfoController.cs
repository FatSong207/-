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
using Yuebon.AspNetCore.Mvc.Filter;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using System.Linq;
using Yuebon.Commons.Json;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class BankinfoController : AreaApiController<Bankinfo, BankinfoOutputDto,BankinfoInputDto,IBankinfoService,string>
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        public BankinfoController(IBankinfoService _iService) : base(_iService)
        {
            iService = _iService;
        }

        /// <summary>
        /// 獲取所有銀行
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllBanks")]
        public async Task<IActionResult> GetAllBanks()
        {
            CommonResult result = new CommonResult();
            var list =await iService.GetDistinctByFieldAsync("BankName,BankNo","1=1");
            list = list.OrderBy(t => t.BankNo);
            result.ResData = list;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取所有分行
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllBranchs")]
        public async Task<IActionResult> GetAllBranchs()
        {
            CommonResult result = new CommonResult();
            var list = await iService.GetDistinctByFieldAsync("BankNo,BranchName,BranchNo", "1=1");
            result.ResData = list;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;
            return ToJsonContent(result);
        }

        [HttpGet("GetAllBranchByBankName")]
        public async Task<IActionResult> GetAllBranchByBankName(string bankName)
        {
            CommonResult result = new CommonResult();
            var list = await iService.GetDistinctByFieldAsync("BranchName,BranchNo", $"BankName = '{bankName}'");
            result.ResData = list;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 下載當前套用CSV
        /// </summary>
        /// <returns></returns>
        [HttpGet("downloadCurrentFile")]
        [LogNotMethod]
        public async Task<IActionResult> downloadCurrentFile()
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            var di = new DirectoryInfo(@$"{path}Bankinfo/");
            var fileName = di.GetFiles().Where(x => x.Extension == ".csv").OrderByDescending(x => x.LastWriteTime).FirstOrDefault().Name;

            var pdf = System.IO.File.OpenRead(@$"{path}Bankinfo/{fileName}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.csv", out contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }
    }
}