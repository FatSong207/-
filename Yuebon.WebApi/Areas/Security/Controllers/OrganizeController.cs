using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Models;
using Yuebon.Security.Dtos;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.WebApi.Areas.Security.Controllers
{
    /// <summary>
    /// 組織機構接口
    /// </summary>
    [ApiController]
    [Route("api/Security/[controller]")]
    public class OrganizeController : AreaApiController<Organize, OrganizeOutputDto, OrganizeInputDto, IOrganizeService, string>
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        public OrganizeController(IOrganizeService _iService) : base(_iService)
        {
            iService = _iService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Organize info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            ////info.DeleteMark = false;
            if (info.SortCode == null) {
                info.SortCode = 99;
            }
            if (string.IsNullOrEmpty(info.ParentId)) {
                info.Layers = 1;
                info.ParentId = "";
            } else {
                info.Layers = iService.Get(info.ParentId).Layers + 1;
            }

        }

        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(Organize info)
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
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Organize info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }

        [HttpGet("GetAllForSelect")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetAllForSelect()
        {
            var result = new CommonResult();
            try {
                var datalist = await iService.GetAllAsync();
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = datalist;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取組織結構異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
            }
            return ToJsonContent(result);

        }

        /// <summary>
        /// 異步更新數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(OrganizeInputDto tinfo)
        {
            CommonResult result = new CommonResult();

            Organize info = iService.Get(tinfo.Id);
            info.ParentId = tinfo.ParentId;
            info.FullName = tinfo.FullName;
            info.EnCode = tinfo.EnCode;
            info.ShortName = tinfo.ShortName;
            info.CategoryId = tinfo.CategoryId;
            info.ManagerId = tinfo.ManagerId;
            //info.AllowEdit = tinfo.AllowEdit;
            //info.AllowDelete = tinfo.AllowDelete;
            info.ManagerId = tinfo.ManagerId;
            info.EnabledMark = tinfo.EnabledMark;
            //info.DeleteMark = tinfo.DeleteMark;
            info.SortCode = tinfo.SortCode;
            info.Description = tinfo.Description;

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
        /// 獲取組織機構適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllOrganizeTreeTable")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> GetAllOrganizeTreeTable()
        {
            CommonResult result = new CommonResult();
            try {
                List<OrganizeOutputDto> list = await iService.GetAllOrganizeTreeTable();
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = list;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取組織結構異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取當前使用者可觸碰ㄉ組織機構適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPermissionOrganizeTreeTable")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetPermissionOrganizeTreeTable()
        {
            CommonResult result = new CommonResult();
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            var permissionDepts = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + CurrentUser.UserId).ToJson());
            try {
                List<OrganizeOutputDto> list = await iService.GetPermissionOrganizeTreeTable(permissionDepts);
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = list;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取組織結構異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取當前使用者可觸碰ㄉ組織機構適用于Select
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPermissionOrganizeSelect")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetPermissionOrganizeSelect()
        {
            CommonResult result = new CommonResult();
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            var permissionDepts = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + CurrentUser.UserId).ToJson());
            var data = new List<object>();
            try {

                var isManager = iService.GetWhere($" ManagerId = '{CurrentUser.UserId}' ") != null;
                if (isManager) {
                    var list = await iService.GetListWhereAsync($" id in ('{permissionDepts.Join("','")}') ");
                    result.ResData =  list.Select(x => new { id = x.Id, name = iService.Get(x.ParentId).FullName + "/" + x.FullName, layer = x.Layers }).OrderBy(y => y.layer);
                } else {
                    result.ResData = null;
                }
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取組織結構異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取當前使用者可觸碰ㄉ組織機構適用于Vue 樹形列表(潛在客管理)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPermissionOrganizeTreeTablePC")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetPermissionOrganizeTreeTablePC()
        {
            CommonResult result = new CommonResult();
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            var permissionDepts = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + CurrentUser.UserId).ToJson());
            var departmentId = permissionDepts.FirstOrDefault();
            var deptIdList = await iService.GetParentOrganizeIdList(departmentId);
            try {
                List<OrganizeOutputDto> list = await iService.GetPermissionOrganizeTreeTable2(deptIdList);
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = list;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取組織結構異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取當前使用者可觸碰ㄉ組織機構適用于Vue 樹形列表(廣告建物)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPermissionOrganizeTreeTableBA")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetPermissionOrganizeTreeTableBA()
        {
            CommonResult result = new CommonResult();
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            var permissionDepts = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + CurrentUser.UserId).ToJson());
            try {
                List<OrganizeOutputDto> list = new List<OrganizeOutputDto>();
                var roles = CurrentUser.Role;
                if (!string.IsNullOrEmpty(roles)) {
                    if (roles.Contains("sales") || roles.Contains("storemanager")) {
                        list = await iService.GetAllOrganizeTreeTable();
                    } else {
                        list = await iService.GetPermissionOrganizeTreeTable(permissionDepts);
                    }
                }


                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = list;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取組織結構異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取組織機構適用于Vue Tree樹形
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllOrganizeTree")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> GetAllOrganizeTree()
        {
            CommonResult result = new CommonResult();
            try {
                List<OrganizeOutputDto> list = await iService.GetAllOrganizeTreeTable();
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = list;
            } catch (Exception ex) {
                Log4NetHelper.Error("獲取組織結構異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
            }
            return ToJsonContent(result);
        }


        /// <summary>
        /// 異步批量物理刪除
        /// </summary>
        /// <param name="info"></param>
        [HttpDelete("DeleteBatchAsync")]
        [YuebonAuthorize("Delete")]
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
    }
}