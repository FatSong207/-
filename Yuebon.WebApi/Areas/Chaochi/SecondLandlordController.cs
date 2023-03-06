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
using Yuebon.AspNetCore.Mvc;
using System.Linq;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class SecondLandlordController : AreaApiController<SecondLandlord, SecondLandlordOutputDto,SecondLandlordInputDto,ISecondLandlordService,string>
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        public SecondLandlordController(ISecondLandlordService _iService) : base(_iService)
        {
            iService = _iService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(SecondLandlord info)
        {
            info.Id = GuidUtils.CreateNo();
            //info.CreatorTime = DateTime.Now;
            //info.CreatorUserId = CurrentUser.UserId;
            //info.DeleteMark = false;
            //if (info.SortCode == null)
            //{
            //    info.SortCode = 99;
            //}
        }
        
        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(SecondLandlord info)
        {
            //info.LastModifyUserId = CurrentUser.UserId;
            //info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(SecondLandlord info)
        {
            //info.DeleteMark = true;
            //info.DeleteTime = DateTime.Now;
            //info.DeleteUserId = CurrentUser.UserId;
        }

        /// <summary>
        /// 獲取所有二房東資訊
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<CommonResult<List<SecondLandlordOutputDto>>> GetAll()
        {
            CommonResult<List<SecondLandlordOutputDto>> result = new CommonResult<List<SecondLandlordOutputDto>>();
            //var files = infoD.GetFiles().OrderByDescending(p => p.CreationTime).ToList();
            IEnumerable<SecondLandlord> list = await iService.GetAllAsync();
            List<SecondLandlordOutputDto> resultList = list.MapTo<SecondLandlordOutputDto>().OrderBy(p => p.SLName).ToList();
            result.ResData = resultList;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;

            return result;
        }
    }
}