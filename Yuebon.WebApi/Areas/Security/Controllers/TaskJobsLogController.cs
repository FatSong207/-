using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.ViewModel;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Quartz.Dtos;
using Yuebon.Quartz.IServices;
using Yuebon.Quartz.Models;

namespace Yuebon.WebApi.Areas.Security.Controllers
{
    /// <summary>
    /// 定時任務執行日志接口
    /// </summary>
    [ApiController]
    [Route("api/Security/[controller]")]
    public class TaskJobsLogController : AreaApiController<TaskJobsLog, TaskJobsLogOutputDto,TaskJobsLogInputDto,ITaskJobsLogService,string>
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        public TaskJobsLogController(ITaskJobsLogService _iService) : base(_iService)
        {
            iService = _iService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(TaskJobsLog info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
        }
        
        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(TaskJobsLog info)
        {
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(TaskJobsLog info)
        {
        }


        /// <summary>
        /// 根據任務Id查詢最新的30條日志
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("FindWithByTaskIdAsync")]
        [YuebonAuthorize("List")]
        public  async Task<IActionResult> FindWithByTaskIdAsync([FromQuery] SearchModel search)
        {
            CommonResult result = new CommonResult();
            string where = "";
            if (!string.IsNullOrEmpty(search.Keywords))
            {
                where += string.Format(" TaskId ='{0}' ", search.Keywords);
            }
            where += " order by CreatorTime desc";
            IEnumerable<TaskJobsLog> list = await iService.GetListTopWhereAsync(40,where);
            List<TaskJobsLogVueTimelineOutputDto> resultList = list.MapTo<TaskJobsLogVueTimelineOutputDto>();
            result.ResData = resultList;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }
    }
}