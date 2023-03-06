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
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;
using Yuebon.Security.IServices;
using System.Linq;
using Yuebon.Commons.Tree;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;

namespace Yuebon.WebApi.Areas.Security.Controllers
{
    /// <summary>
    /// 數據字典接口
    /// </summary>
    [ApiController]
    [Route("api/Security/[controller]")]
    public class ItemsController : AreaApiController<Items, ItemsOutputDto, ItemsInputDto, IItemsService,string>
    {

        private readonly IItemsDetailService itemsDetailService;
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_itemsDetailService"></param>
        public ItemsController(IItemsService _iService, IItemsDetailService _itemsDetailService) : base(_iService)
        {
            iService = _iService;
            itemsDetailService=_itemsDetailService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Items info)
        {            
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.DeleteMark = false;
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
        protected override void OnBeforeUpdate(Items info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Items info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }



        /// <summary>
        /// 異步新增數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(ItemsInputDto tinfo)
        {
            CommonResult result = new CommonResult();
            Items isExsit = await iService.GetByEnCodAsynce(tinfo.EnCode);
            if (isExsit != null)
            {
                result.ErrMsg = "字典分類編碼不能重復";
                return ToJsonContent(result);
            }
            Items info = tinfo.MapTo<Items>();
            OnBeforeInsert(info);
            long ln = await iService.InsertAsync(info).ConfigureAwait(false);
            if (ln > 0)
            {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.ErrMsg = ErrCode.err43001;
                result.ErrCode = "43001";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 按字典分類編碼查詢樹形展開，新增、修改需要
        /// </summary>
        /// <param name="itemCode">數據字典分類編碼</param>
        /// <returns></returns>
        [HttpGet("FindTreeSelectJson")]
        [NoPermissionRequired]
        public async Task<IActionResult> FindTreeSelectJson(string itemCode)
        {
            CommonResult result = new CommonResult();
            List<ItemsDetailOutputDto> list = await itemsDetailService.GetItemDetailsByItemCode(itemCode);
            var treeList = new List<TreeSelectModel>();
            foreach (ItemsDetailOutputDto item in list)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.ItemCode;
                treeModel.text = item.ItemName;
                treeModel.parentId = item.ParentId;
                treeList.Add(treeModel);
            }
            result.ErrCode = ErrCode.err0;
            result.ResData = treeList.TreeSelectJson();
            return ToJsonContent(result);
        }

        /// <summary>
        /// 按字典分類編碼查詢,Vue element select新增、修改需要
        /// </summary>
        /// <param name="itemCode">數據字典分類編碼</param>
        /// <returns></returns>
        [HttpGet("GetListByItemCode")] 
        [NoPermissionRequired]
        public async Task<IActionResult> GetListByItemCode(string itemCode)
        {
            CommonResult result = new CommonResult();
            IEnumerable<ItemsDetailOutputDto> list = await itemsDetailService.GetItemDetailsByItemCode(itemCode);
            result.ErrCode = ErrCode.successCode;
            result.ResData = list;
            return ToJsonContent(result);
        }


        /// <summary>
        /// 異步更新數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(ItemsInputDto tinfo)
        {
            CommonResult result = new CommonResult();
            Items isExsit = await iService.GetByEnCodAsynce(tinfo.EnCode, tinfo.Id);
            if (isExsit != null)
            {
                result.ErrMsg = "字典分類編碼不能重復";
                return ToJsonContent(result);
            }
            Items info = iService.Get(tinfo.Id);
            info.FullName = tinfo.FullName;
            info.EnCode = tinfo.EnCode;
            info.ParentId = tinfo.ParentId;
            info.EnabledMark = tinfo.EnabledMark;
            info.IsTree = tinfo.IsTree;
            info.SortCode = tinfo.SortCode;
            info.Description = tinfo.Description;


            OnBeforeUpdate(info);
            bool bl = await iService.UpdateAsync(info, tinfo.Id).ConfigureAwait(false);
            if (bl)
            {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return ToJsonContent(result);
        }
        /// <summary>
        /// 獲取功能菜單適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllItemsTreeTable")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> GetAllItemsTreeTable()
        {
            CommonResult result = new CommonResult();
            try
            {
                List<ItemsOutputDto> list = await iService.GetAllItemsTreeTable();
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
    }
}