using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
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
    /// 角色權限接口
    /// </summary>
    [ApiController]
    [Route("api/Security/[controller]")]
    public class RoleAuthorizeController : AreaApiController<RoleAuthorize, RoleAuthorizeOutputDto, RoleAuthorizeInputDto, IRoleAuthorizeService,string>
    {
        private readonly IMenuService menuService;
        private readonly IRoleDataService roleDataService;
        private readonly IOrganizeService organizeService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_menuService"></param>
        /// <param name="_roleDataService"></param>
        /// <param name="_organizeService"></param>
        public RoleAuthorizeController(IRoleAuthorizeService _iService, IMenuService _menuService, IRoleDataService _roleDataService, IOrganizeService _organizeService) : base(_iService)
        {
            iService = _iService;
            menuService = _menuService;
            roleDataService = _roleDataService;
            organizeService = _organizeService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(RoleAuthorize info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            if (info.SortCode == null)
            {
                info.SortCode = 99;
            }
        }
        
        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(RoleAuthorize info)
        {
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(RoleAuthorize info)
        {
        }

        /// <summary>
        /// 角色分配權限樹
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        [HttpGet("GetRoleAuthorizeFunction")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> GetRoleAuthorizeFunction(string roleId, string itemType)
        {
            CommonResult result = new CommonResult();
            roleId = "'" + roleId + "'";
            List<string> resultlist = new List<string>();
            IEnumerable<RoleAuthorize> list= iService.GetListRoleAuthorizeByRoleId(roleId, itemType);
            foreach(RoleAuthorize info in list)
            {
                resultlist.Add(info.ItemId);
            }
            result.ResData = resultlist;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }


        /// <summary>
        /// 保存角色權限
        /// </summary>
        /// <param name="roleinfo">功能權限</param>
        /// <returns></returns>
        [HttpPost("SaveRoleAuthorize")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> SaveRoleAuthorize(RoleAuthorizeDataInputDto roleinfo)
        {
            CommonResult result = new CommonResult();
            try
            {                
                List<RoleAuthorize> inList = new List<RoleAuthorize>();
                foreach (string item in roleinfo.RoleFunctios)
                {
                    Menu menu = menuService.Get(item);
                    if (menu != null)
                    {
                        RoleAuthorize info = new RoleAuthorize();
                        info.ObjectId = roleinfo.RoleId;
                        info.ItemType = (menu.MenuType == "C" || menu.MenuType == "M") ? 1 : 2;
                        info.ObjectType = 1;
                        info.ItemId = menu.Id;
                        OnBeforeInsert(info);
                        inList.Add(info);
                    }
                }

                List<RoleData> roleDataList = new List<RoleData>();
                foreach (string item in roleinfo.RoleData)
                {
                    RoleData info = new RoleData();
                    info.RoleId = roleinfo.RoleId;
                    info.AuthorizeData = item;
                    info.DType = "dept";
                    roleDataList.Add(info);
                }
                foreach (string item in roleinfo.RoleSystem)
                {
                    RoleAuthorize info = new RoleAuthorize();
                    info.ObjectId = roleinfo.RoleId;
                    info.ItemType = 0;
                    info.ObjectType = 1;
                    info.ItemId = item;
                    OnBeforeInsert(info);
                    inList.Add(info);
                }
                result.Success = await iService.SaveRoleAuthorize(roleinfo.RoleId,inList, roleDataList);
                if (result.Success)
                {
                    result.ErrCode = ErrCode.successCode;
                }
            }
            catch (Exception ex)
            {
                result.ErrMsg = ex.Message;
            }
            return ToJsonContent(result);
        }

        private List<RoleAuthorize> SubFunction(List<ModuleFunctionOutputDto> list, string roleId)
        {
            List<RoleAuthorize> inList = new List<RoleAuthorize>();
            foreach (ModuleFunctionOutputDto item in list)
            {
                RoleAuthorize info = new RoleAuthorize();
                info.ObjectId = roleId;
                info.ItemType = 1;
                info.ObjectType = 1;
                info.ItemId = item.Id.ToString();
                OnBeforeInsert(info);
                inList.Add(info);
                inList.Concat(SubFunction(item.Children, roleId));
            }
            return inList;
        }

        /// <summary>
        /// 獲取功能菜單適用于Vue Tree樹形
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllFunctionTree")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> GetAllFunctionTree()
        {
            CommonResult result = new CommonResult();
            try
            {
                List<ModuleFunctionOutputDto> list = await iService.GetAllFunctionTree();
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = list;
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("獲取菜單異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
            }
            return ToJsonContent(result);
        }
        ///// <summary>
        ///// 獲取功能菜單適用于Vue 樹形列表
        ///// </summary>
        ///// <param name="systemTypeId">子系統Id</param>
        ///// <returns></returns>
        //[HttpGet("GetAllFunctionTreeTable")]
        //[YuebonAuthorize("List")]
        //public async Task<IActionResult> GetAllFunctionTreeTable(string systemTypeId)
        //{
        //    CommonResult result = new CommonResult();
        //    try
        //    {
        //        List<FunctionTreeTableOutputDto> list = await menuService.GetAllFunctionTreeTable(systemTypeId);
        //        result.Success = true;
        //        result.ErrCode = ErrCode.successCode;
        //        result.ResData = list;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log4NetHelper.Error("獲取菜單異常", ex);
        //        result.ErrMsg = ErrCode.err40110;
        //        result.ErrCode = "40110";
        //    }
        //    return ToJsonContent(result);
        //}
    }
}