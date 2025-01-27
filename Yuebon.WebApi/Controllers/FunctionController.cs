﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Models;
using Yuebon.Security.Dtos;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.WebApi.Controllers
{
    /// <summary>
    /// 功能模塊接口
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FunctionController: AreaApiController<Menu, MenuOutputDto, MenuInputDto, IMenuService, string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iService"></param>
        public FunctionController(IMenuService _iService) : base(_iService)
        {
            iService = _iService;
        }

        /// <summary>
        /// 根據父級功能編碼查詢所有子集功能，主要用于頁面操作按鈕權限
        /// </summary>
        /// <param name="enCode">菜單功能編碼</param>
        /// <returns></returns>
        [HttpGet("GetListByParentEnCode")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetListByParentEnCode(string enCode)
        {
            CommonResult result = new CommonResult();
            try
            {
                if (CurrentUser != null)
                {
                    YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                    List<MenuOutputDto> functions = new List<MenuOutputDto>();
                    functions = yuebonCacheHelper.Get("User_Function_" + CurrentUser.UserId).ToJson().ToObject<List<MenuOutputDto>>();
                    MenuOutputDto functionOutputDto = functions.Find(s => s.EnCode == enCode);
                    List<MenuOutputDto> nowFunList = new List<MenuOutputDto>();
                    if (functionOutputDto != null)
                    {
                        nowFunList = functions.FindAll(s => s.ParentId == functionOutputDto.Id && s.IsShow && s.MenuType.Equals("F")).OrderBy(s=>s.SortCode).ToList();
                    }
                    result.ErrCode = ErrCode.successCode;
                    result.ResData = nowFunList;
                }
                else
                {
                    result.ErrCode = "40008";
                    result.ErrMsg = ErrCode.err40008;
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("根據父級功能編碼查詢所有子集功能，主要用于頁面操作按鈕權限,代碼生成異常", ex);
                result.ErrCode = ErrCode.failCode;
                result.ErrMsg = "獲取模塊功能異常";
            }
            return ToJsonContent(result);
        }
    }
}