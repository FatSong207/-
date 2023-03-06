using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Chaochi.Core.IServices;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Models;

namespace Yuebon.WebApi.Areas.Chaochi
{
    /// <summary>
    /// 表單分類接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class CategoryFormController : AreaApiController<CategoryForm, CategoryFormOutputDto, CategoryFormInputDto, ICategoryFormService, string>
    {
        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="_iService"></param>
        public CategoryFormController(ICategoryFormService _iService) : base(_iService)
        {
            iService = _iService;
        }
        /// <summary>
        /// 新增前處理資料
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(CategoryForm info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.DeleteMark = false;
            if (info.SortOrder == null) {
                info.SortOrder = 99;
            }
            if (string.IsNullOrEmpty(info.ParentId)) {
                info.Layers = 1;
                info.ParentId = "";
            } else {
                info.Layers = iService.Get(info.ParentId).Layers + 1;
            }

        }

        /// <summary>
        /// 在更新資料前對資料的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(CategoryForm info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
            if (string.IsNullOrEmpty(info.ParentId)) {
                info.Layers = 1;
                info.ParentId = "";
            } else {
                info.Layers = iService.Get(info.ParentId).Layers + 1;
            }
        }

        /// <summary>
        /// 在軟刪除資料前對資料的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(CategoryForm info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }


        /// <summary>
        /// 非同步更新數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        //[YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(CategoryFormInputDto tinfo)
        {
            CommonResult result = new CommonResult();

            CategoryForm info = iService.Get(tinfo.Id);
            info.ParentId = tinfo.ParentId;
            info.Name = tinfo.Name;
            info.EnabledMark = tinfo.EnabledMark;
            info.DeleteMark = tinfo.DeleteMark;
            info.SortOrder = tinfo.SortOrder;

            OnBeforeUpdate(info);
            bool bl = await iService.UpdateAsync(info, tinfo.Id).ConfigureAwait(false);
            if (bl) {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } else {
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return ToJsonContent(result);
        }
        /// <summary>
        /// 獲取表單分類適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllCategoryTreeTable")]
        //[YuebonAuthorize("List")]
        public async Task<IActionResult> GetAllCategoryTreeTable()
        {
            CommonResult result = new CommonResult();
            try {
                List<CategoryFormOutputDto> list = await iService.GetAllCategoryTreeTable();
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = list;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取表單分類結構異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return ToJsonContent(result);
        }


        /// <summary>
        /// 獲取表單分類適用于Vue Tree樹形
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllCategoryTree")]
        //[YuebonAuthorize("List")]
        public async Task<IActionResult> GetAllCategoryTree()
        {
            CommonResult result = new CommonResult();
            try {
                List<CategoryFormOutputDto> list = await iService.GetAllCategoryTreeTable();
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = list;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取組織結構異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return ToJsonContent(result);
        }


        /// <summary>
        /// 異步批量物理刪除
        /// </summary>
        /// <param name="info"></param>
        [HttpDelete("DeleteBatchAsync")]
        //[YuebonAuthorize("Delete")]
        public override async Task<IActionResult> DeleteBatchAsync(DeletesInputDto info)
        {
            CommonResult result = new CommonResult();

            if (info.Ids.Length > 0) {
                result = await iService.DeleteBatchWhereAsync(info).ConfigureAwait(false);
                if (result.Success) {
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.ErrCode = "43003";
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 下載當前套用CSV
        /// </summary>
        /// <returns></returns>
        [HttpGet("downloadCurrentCategoryFormFile")]
        //[YuebonAuthorize("List")]
        [LogNotMethod]
        public async Task<IActionResult> downloadCurrentCategoryFormFile()
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            var di = new DirectoryInfo(@$"{path}CategoryForm/");
            var fileName = di.GetFiles().Where(x => x.Extension == ".csv").OrderByDescending(x => x.LastWriteTime).FirstOrDefault().Name;

            var pdf = System.IO.File.OpenRead(@$"{path}CategoryForm/{fileName}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.csv", out contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }
    }
}
