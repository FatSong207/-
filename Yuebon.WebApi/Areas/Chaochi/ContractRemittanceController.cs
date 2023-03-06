using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.IServices;
using Yuebon.AspNetCore.Mvc.Filter;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using Yuebon.Commons.Json;
using System.Linq;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.Commons.Models;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 合約匯款帳號維護接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class ContractRemittanceController : AreaApiController<ContractRemittance, ContractRemittanceOutputDto,ContractRemittanceInputDto,IContractRemittanceService,string>
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        public ContractRemittanceController(IContractRemittanceService _iService) : base(_iService)
        {
            iService = _iService;
        }

        /// <summary>
        /// 根據社宅或一般宅取得戶名
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAccountNameByType")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetAccountNameByType(string type)
        {
            var result = new CommonResult();
            if (!string.IsNullOrEmpty(type)) {
                var dataList = await iService.GetDistinctByFieldAsync("AccountName", $"Type = '{type}'");
                result.ResData = dataList;
            }
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 根據社宅或一般宅和戶名取得使用單位
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUseCountyByAccountName")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetUseCountyByAccountName(string type, string accountName)
        {
            var result = new CommonResult();
            if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(accountName)) {
                var dataList = await iService.GetDistinctByFieldAsync("UseCounty", $"Type = '{type}' AND AccountName = '{accountName}'");
                result.ResData = dataList;
            }
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 根據使用單位取得銀行
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBankNameByUseCounty")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetBankNameByUseCounty(string type, string accountName, string useCounty)
        {
            var result = new CommonResult();
            if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(useCounty)) {
                var dataList = await iService.GetListWhereAsync($"Type = '{type}' AND AccountName = '{accountName}' AND UseCounty = '{useCounty}'");
                result.ResData = dataList;
            }
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
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
            var di = new DirectoryInfo(@$"{path}ContractRemittance/");
            var fileName = di.GetFiles().Where(x => x.Extension == ".csv").OrderByDescending(x => x.LastWriteTime).FirstOrDefault().Name;

            var pdf = System.IO.File.OpenRead(@$"{path}ContractRemittance/{fileName}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.csv", out contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }

    }
}