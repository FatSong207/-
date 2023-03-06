using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using Yuebon.WebApi.Areas.Security.Models;
using System.Collections.Generic;
using Yuebon.Commons.Json;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.StaticFiles;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Chaochi.Core.IServices;
using Yuebon.Chaochi.Core.IRepositories;

namespace Yuebon.WebApi.Areas.Chaochi
{
    [ApiController]
    //[ApiVersionNeutral]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class OpenDataRoadController : AreaApiController<OpenDataRoad, OpenDataRoad, OpenDataRoad, IOpenDataRoadService, string>
    {
        private readonly IOpenDataRoadService openDataRoadService;
        private readonly IOpenDataRoadRepository openDataRoadRepository;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="_openDataRoadService"></param>
        /// <param name="_openDataRoadRepository"></param>
        public OpenDataRoadController(IOpenDataRoadService _openDataRoadService, IOpenDataRoadRepository _openDataRoadRepository) : base(_openDataRoadService)
        {
            openDataRoadService = _openDataRoadService;
            openDataRoadRepository = _openDataRoadRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTowns")]
        public IActionResult GetAllTowns()
        {
            CommonResult result = new CommonResult();
            var list = openDataRoadService.GetAllByField("city,site_id").ToList();
            result.ResData = list;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;
            return ToJsonContent(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllStreets")]
        public IActionResult GetAllStreetsByQuery(string city, string site_id, string query)
        {
            CommonResult result = new CommonResult();
            var list = openDataRoadService.GetStreetByQuery(city, site_id, query).ToList();
            result.ResData = list;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="city"></param>
        /// <param name="town"></param>
        /// <returns></returns>
        [HttpGet("GetStreetsByCityTown")]
        public async Task<IActionResult> GetStreetsByCityTown(string city, string town)
        {
            var list =await openDataRoadService.GetListWhereAsync($" city='{city}' and site_id ='{town}' ");
            return ToJsonContent(new CommonResult() {
                ResData = list.Distinct().Select(x => new { x.road }),
                ErrCode = ErrCode.successCode, 
                ErrMsg = ErrCode.err0 
            });
        }

        /// <summary>
        /// 下載當前套用CSV
        /// </summary>
        /// <returns></returns>
        [HttpGet("downloadCurrentOpenDataRoadFile")]
        [LogNotMethod]
        public async Task<IActionResult> downloadCurrentOpenDataRoadFile()
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            var di = new DirectoryInfo(@$"{path}OpenDataRoad/");
            var fileName = di.GetFiles().Where(x => x.Extension == ".csv").OrderByDescending(x => x.LastWriteTime).FirstOrDefault().Name;

            var pdf = System.IO.File.OpenRead(@$"{path}OpenDataRoad/{fileName}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.csv", out contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }
    }
}
