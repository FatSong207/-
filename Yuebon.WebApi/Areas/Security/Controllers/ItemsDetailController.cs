﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Log;
using Yuebon.Commons.Models;
using Yuebon.Security.Dtos;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.WebApi.Areas.Security.Controllers
{
    /// <summary>
    /// 數據字典明細
    /// </summary>
    [Route("api/Security/[controller]")]
    [ApiController]
    public class ItemsDetailController : AreaApiController<ItemsDetail, ItemsDetailOutputDto, ItemsDetailInputDto, IItemsDetailService, string>
    {
        private readonly IItemsService itemsService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="itemsService"></param>
        public ItemsDetailController(IItemsDetailService _iService, IItemsService itemsService) : base(_iService)
        {
            iService = _iService;
            this.itemsService = itemsService;
        }


        protected override void OnBeforeInsert(ItemsDetail info)
        {
            //留給子類對參數對象進行修改
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.DeleteMark = false;
            if (info.SortCode == null)
            {
                info.SortCode = 99;
            }
            bool bltree = itemsService.Get(info.ItemId).IsTree;
            if (bltree)
            {
                if (string.IsNullOrEmpty(info.ParentId))
                {
                    info.Layers = 1;
                    info.ParentId = "";
                }
                else
                {
                    info.Layers = iService.Get(info.ParentId).Layers + 1;
                }
            }
            else
            {
                info.ParentId = "";
            }

        }

        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(ItemsDetail info)
        {
            //留給子類對參數對象進行修改 
            info.LastModifyTime = DateTime.Now;
            info.LastModifyUserId = CurrentUser.UserId;
            if (info.SortCode == null)
            {
                info.SortCode = 99;
            }
            bool bltree = itemsService.Get(info.ItemId).IsTree;
            if (bltree)
            {
                if (string.IsNullOrEmpty(info.ParentId))
                {
                    info.Layers = 1;
                    info.ParentId = "";
                }
                else
                {
                    info.Layers = iService.Get(info.ParentId).Layers + 1;
                }
            }
            else
            {
                info.ParentId = "";
            }
        }


        /// <summary>
        /// 異步更新數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(ItemsDetailInputDto tinfo)
        {
            CommonResult result = new CommonResult();

            ItemsDetail info = iService.Get(tinfo.Id);
            info.ItemName = tinfo.ItemName;
            info.ItemCode = tinfo.ItemCode;
            info.ItemId = tinfo.ItemId;
            info.IsDefault = tinfo.IsDefault;
            info.ParentId = tinfo.ParentId;
            info.EnabledMark = tinfo.EnabledMark;
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
        /// 獲取某數據字典類別下的數據適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllItemsDetailTreeTable")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> GetAllItemsDetailTreeTable(string itemId)
        {
            CommonResult result = new CommonResult();
            try
            {
                if (!string.IsNullOrEmpty(itemId))
                {
                    List<ItemsDetailOutputDto> list = await iService.GetAllItemsDetailTreeTable(itemId);
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    result.ResData = list;
                }
                else
                {
                    result.ErrMsg = "數據字典類別ID不能為空";
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("獲取數據字典異常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
            }
            return ToJsonContent(result);
        }
    }
}