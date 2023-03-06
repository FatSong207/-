using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Log;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Chaochi.Core.Dtos;
using Microsoft.AspNetCore.StaticFiles;
using Yuebon.Commons.Json;
using Yuebon.Chaochi.Core.IServices;
using System.Linq;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extensions;
using System.IO;

namespace Yuebon.WebApi.Areas.Chaochi
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class HistoryFormController : AreaApiController<HistoryFormLN, HistoryFormLNOutputDto, HistoryFormLN, IHistoryFormLNService, string>
    {
        private readonly IBuildingService _buildingService;
        private readonly ICustomerLNService _customerLNService;
        private readonly IHistoryFormLCService _historyFormLCService;
        private readonly ICustomerLCService _customerLCService;
        private readonly IHistoryFormBuildingService _historyFormBuildingService;
        private readonly ICustomerRNService _customerRNService;
        private readonly IHistoryFormRNService _historyFormRNService;
        private readonly ICustomerRCService _customerRCService;
        private readonly IHistoryFormRCService _historyFormRCService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="customerRCService"></param>
        /// <param name="historyFormRCService"></param>
        /// <param name="buildingService"></param>
        /// <param name="historyFormBuildingService"></param>
        /// <param name="customerLNService"></param>
        /// <param name="historyFormLCService"></param>
        /// <param name="customerLCService"></param>
        /// <param name="customerRNService"></param>
        /// <param name="historyFormRNService"></param>
        public HistoryFormController(IHistoryFormLNService _iService, ICustomerRCService customerRCService, IHistoryFormRCService historyFormRCService, IBuildingService buildingService, IHistoryFormBuildingService historyFormBuildingService, ICustomerLNService customerLNService, IHistoryFormLCService historyFormLCService, ICustomerLCService customerLCService, ICustomerRNService customerRNService, IHistoryFormRNService historyFormRNService) : base(_iService)
        {
            iService = _iService;
            _buildingService = buildingService;
            _customerLNService = customerLNService;
            _historyFormLCService = historyFormLCService;
            _customerLCService = customerLCService;
            _historyFormBuildingService = historyFormBuildingService;
            _customerRNService = customerRNService;
            _historyFormRNService = historyFormRNService;
            _customerRCService = customerRCService;
            _historyFormRCService = historyFormRCService;
        }
        #region LN
        /// <summary>
        /// 異步分頁查詢LN
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindLNWithPagerSearchAsync")]
        public async Task<IActionResult> FindLNWithPagerSearchAsync(SearchHistoryFormLNModel search)
        {
            CommonResult<PageResult<HistoryFormLNOutputDto>> result = new CommonResult<PageResult<HistoryFormLNOutputDto>>();
            result.ResData = await iService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        [HttpDelete("DeleteLNHistoryForm")]
        public async Task<IActionResult> DeleteLNHistoryForm(DeletesInputDto info)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            where = "id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            if (!string.IsNullOrEmpty(where)) {
                var list = await iService.GetListWhereAsync(where);
                bool bl = await iService.DeleteBatchWhereAsync(where).ConfigureAwait(false);
                if (bl) {
                    Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
                    Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
                    var path = $"{sysSetting.ChaochiFilepath}";
                    foreach (var item in list) {
                        System.IO.File.Delete(Path.Combine(path, "historyPDF", "CustomerL", "LN", item.IDNo, item.FileName));

                    }
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.ErrMsg = ErrCode.err43003;
                    result.ErrCode = "43003";
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 下載LN檔案
        /// </summary>
        /// <param name="id"></param>
        /// <param name="curCid"></param>
        /// <returns></returns>
        [HttpGet("downloadLNHistoryFormById")]
        [LogNotMethod]
        public async Task<IActionResult> downloadLNHistoryFormById(string id, string curCid)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            var fileName = iService.Get(id).FileName;
            var LNID = _customerLNService.Get(curCid).LNID;
            var pdf = System.IO.File.OpenRead(@$"{path}historyPDF\CustomerL\LN\{LNID}\{fileName}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }

        /// <summary>
        /// 取得LN檔名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetLNFileNameById")]
        [LogNotMethod]
        public async Task<IActionResult> GetLNFileNameById(string id)
        {
            var result = new CommonResult();

            try {
                result.ResData = iService.Get(id).FileName;
                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                throw ex;
            }
            return ToJsonContent(result);
        }

        #endregion LN

        #region LC
        /// <summary>
        /// 異步分頁查詢LC
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindLCWithPagerSearchAsync")]
        public async Task<IActionResult> FindLNWithPagerSearchAsync(SearchHistoryFormLCModel search)
        {
            CommonResult<PageResult<HistoryFormLCOutputDto>> result = new CommonResult<PageResult<HistoryFormLCOutputDto>>();
            result.ResData = await _historyFormLCService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        [HttpDelete("DeleteLCHistoryForm")]
        public async Task<IActionResult> DeleteLCHistoryForm(DeletesInputDto info)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            where = "id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            if (!string.IsNullOrEmpty(where)) {
                var list = await _historyFormLCService.GetListWhereAsync(where);
                bool bl = await _historyFormLCService.DeleteBatchWhereAsync(where).ConfigureAwait(false);
                if (bl) {
                    Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
                    Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
                    var path = $"{sysSetting.ChaochiFilepath}";
                    foreach (var item in list) {
                        System.IO.File.Delete(Path.Combine(path, "historyPDF", "CustomerL", "LC", item.IDNo, item.FileName));

                    }
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.ErrMsg = ErrCode.err43003;
                    result.ErrCode = "43003";
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 下載LC檔案
        /// </summary>
        /// <param name="id"></param>
        /// <param name="curCid"></param>
        /// <returns></returns>
        [HttpGet("downloadLCHistoryFormById")]
        [LogNotMethod]
        public async Task<IActionResult> downloadLCHistoryFormById(string id, string curCid)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            var fileName = _historyFormLCService.Get(id).FileName;
            var LCID = _customerLCService.Get(curCid).LCID;
            var pdf = System.IO.File.OpenRead(@$"{path}historyPDF\CustomerL\LC\{LCID}\{fileName}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }

        /// <summary>
        /// 取得LC檔名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetLCFileNameById")]
        [LogNotMethod]
        public async Task<IActionResult> GetLCFileNameById(string id)
        {
            var result = new CommonResult();

            try {
                result.ResData = _historyFormLCService.Get(id).FileName;
                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                throw ex;
            }
            return ToJsonContent(result);
        }
        #endregion LC

        #region RN
        /// <summary>
        /// 異步分頁查詢RN
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindRNWithPagerSearchAsync")]
        public async Task<IActionResult> FindRNWithPagerSearchAsync(SearchHistoryFormRNModel search)
        {
            CommonResult<PageResult<HistoryFormRNOutputDto>> result = new CommonResult<PageResult<HistoryFormRNOutputDto>>();
            result.ResData = await _historyFormRNService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        [HttpDelete("DeleteRNHistoryForm")]
        public async Task<IActionResult> DeleteRNHistoryForm(DeletesInputDto info)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            where = "id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            if (!string.IsNullOrEmpty(where)) {
                var list = await _historyFormRNService.GetListWhereAsync(where);
                bool bl = await _historyFormRNService.DeleteBatchWhereAsync(where).ConfigureAwait(false);
                if (bl) {
                    Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
                    Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
                    var path = $"{sysSetting.ChaochiFilepath}";
                    foreach (var item in list) {
                        System.IO.File.Delete(Path.Combine(path, "historyPDF", "CustomerR", "RN", item.IDNo, item.FileName));

                    }
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.ErrMsg = ErrCode.err43003;
                    result.ErrCode = "43003";
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 下載LN檔案
        /// </summary>
        /// <param name="id"></param>
        /// <param name="curCid"></param>
        /// <returns></returns>
        [HttpGet("downloadRNHistoryFormById")]
        [LogNotMethod]
        public async Task<IActionResult> DownloadRNHistoryFormById(string id, string curCid)
        {
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            var fileName = _historyFormRNService.Get(id).FileName;
            var RNID = _customerRNService.Get(curCid).RNID;
            var pdf = System.IO.File.OpenRead(@$"{path}historyPDF\CustomerR\RN\{RNID}\{fileName}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }

        /// <summary>
        /// 取得RN檔名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetRNFileNameById")]
        [LogNotMethod]
        public async Task<IActionResult> GetRNFileNameById(string id)
        {
            var result = new CommonResult();

            try {
                result.ResData = _historyFormRNService.Get(id).FileName;
                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                throw ex;
            }
            return ToJsonContent(result);
        }

        #endregion RN

        #region RC
        /// <summary>
        /// 異步分頁查詢RN
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindRCWithPagerSearchAsync")]
        public async Task<IActionResult> FindRCWithPagerSearchAsync(SearchHistoryFormRCModel search)
        {
            CommonResult<PageResult<HistoryFormRCOutputDto>> result = new CommonResult<PageResult<HistoryFormRCOutputDto>>();
            result.ResData = await _historyFormRCService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        [HttpDelete("DeleteRCHistoryForm")]
        public async Task<IActionResult> DeleteRCHistoryForm(DeletesInputDto info)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            where = "id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            if (!string.IsNullOrEmpty(where)) {
                var list = await _historyFormRCService.GetListWhereAsync(where);
                bool bl = await _historyFormRCService.DeleteBatchWhereAsync(where).ConfigureAwait(false);
                if (bl) {
                    Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
                    Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
                    var path = $"{sysSetting.ChaochiFilepath}";
                    foreach (var item in list) {
                        System.IO.File.Delete(Path.Combine(path, "historyPDF", "CustomerR", "RC", item.IDNo, item.FileName));

                    }
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.ErrMsg = ErrCode.err43003;
                    result.ErrCode = "43003";
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 下載LN檔案
        /// </summary>
        /// <param name="id"></param>
        /// <param name="curCid"></param>
        /// <returns></returns>
        [HttpGet("downloadRCHistoryFormById")]
        [LogNotMethod]
        public async Task<IActionResult> DownloadRCHistoryFormById(string id, string curCid)
        {
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            var fileName = _historyFormRCService.Get(id).FileName;
            var RCID = _customerRCService.Get(curCid).RCID;
            var pdf = System.IO.File.OpenRead(@$"{path}historyPDF\CustomerR\RC\{RCID}\{fileName}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }

        /// <summary>
        /// 取得RN檔名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetRCFileNameById")]
        [LogNotMethod]
        public async Task<IActionResult> GetRCFileNameById(string id)
        {
            var result = new CommonResult();

            try {
                result.ResData = _historyFormRCService.Get(id).FileName;
                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                throw ex;
            }
            return ToJsonContent(result);
        }

        #endregion RC

        #region Building
        /// <summary>
        /// 異步分頁查詢Building
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindBuildingWithPagerSearchAsync")]
        public async Task<IActionResult> FindBuildingWithPagerSearchAsync(SearchHistoryFormBuildingModel search)
        {
            CommonResult<PageResult<HistoryFormBuildingOutputDto>> result = new CommonResult<PageResult<HistoryFormBuildingOutputDto>>();
            result.ResData = await _historyFormBuildingService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        [HttpDelete("DeleteBuildingHistoryForm")]
        public async Task<IActionResult> DeleteBuildingHistoryForm(DeletesInputDto info)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            where = "id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            if (!string.IsNullOrEmpty(where)) {
                var list = await _historyFormBuildingService.GetListWhereAsync(where);
                bool bl = await _historyFormBuildingService.DeleteBatchWhereAsync(where).ConfigureAwait(false);
                if (bl) {
                    Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
                    Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
                    var path = $"{sysSetting.ChaochiFilepath}";
                    foreach (var item in list) {
                        System.IO.File.Delete(Path.Combine(path, "historyPDF", "Building", item.IDNo, item.FileName));

                    }
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.ErrMsg = ErrCode.err43003;
                    result.ErrCode = "43003";
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 下載Building檔案
        /// </summary>
        /// <param name="id"></param>
        /// <param name="curCid"></param>
        /// <returns></returns>
        [HttpGet("downloadBuildingHistoryFormById")]
        [LogNotMethod]
        public async Task<IActionResult> downloadBuildingHistoryFormById(string id, string curCid)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            var fileName = _historyFormBuildingService.Get(id).FileName;
            var pdf = System.IO.File.OpenRead(@$"{path}historyPDF\Building\{curCid}\{fileName}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }

        /// <summary>
        /// 檢查某建物是否有建物設備表單檔案
        /// </summary>
        /// <param name="badd"></param>
        /// <returns></returns>
        [HttpGet("CheckBuildingHasA0000801PDF")]
        public async Task<IActionResult> CheckBuildingHasA0000801PDF(string badd)
        {
            var result = new CommonResult();
            var id = _buildingService.GetIdByBAdd(badd);
            var historyForms = await _historyFormBuildingService.GetListWhereAsync($" IDNo = '{id}' and FormName = '建物設備' ");
            result.ResData = historyForms.Count() > 0;
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }


        /// <summary>
        /// 下載某建物最新的建物設備表單檔案
        /// </summary>
        /// <param name="badd"></param>
        /// <returns></returns>
        [HttpGet("downloadLatestA0000801PDF")]
        [LogNotMethod]
        public async Task<IActionResult> DownloadLatestA0000801PDF(string badd)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            var id = _buildingService.GetIdByBAdd(badd);
            var historyForms = await _historyFormBuildingService.GetListWhereAsync($" IDNo = '{id}' and FormName = '建物設備' ");
            var fileName = historyForms.OrderByDescending(x => x.UploadTime).FirstOrDefault().FileName;
            var pdf = System.IO.File.OpenRead(@$"{path}historyPDF\Building\{id}\{fileName}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }

        /// <summary>
        /// 取得Building檔名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetBuildingFileNameById")]
        [LogNotMethod]
        public async Task<IActionResult> GetBuildingFileNameById(string id)
        {
            var result = new CommonResult();

            try {
                result.ResData = _historyFormBuildingService.Get(id).FileName;
                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                throw ex;
            }
            return ToJsonContent(result);
        }
        #endregion Building
    }
}
