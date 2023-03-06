using Microsoft.AspNetCore.Mvc;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.ViewModel;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using Yuebon.Quartz.Dtos;
using Yuebon.Quartz.IServices;
using Yuebon.Quartz.Jobs;
using Yuebon.Quartz.Models;
using Yuebon.Security.Application;
using Yuebon.Security.Dtos;
using Yuebon.WebApi.Areas.Security.Models;

namespace Yuebon.WebApi.Areas.Security.Controllers
{
    /// <summary>
    /// 定時任務接口
    /// </summary>
    [ApiController]
    [Route("api/Security/[controller]")]
    public class TaskManagerController : AreaApiController<TaskManager, TaskManagerOutputDto,TaskManagerInputDto,ITaskManagerService,string>
    {
        /// <summary>
        /// 
        /// </summary>
        private ISchedulerFactory schedulerFactory;
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_schedulerFactory"></param>
        public TaskManagerController(ITaskManagerService _iService, ISchedulerFactory _schedulerFactory) : base(_iService)
        {
            iService = _iService;
            schedulerFactory = _schedulerFactory;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(TaskManager info)
        {
            info.Id = new SequenceApp().GetSequenceNext("TaskManager");
            info.DeleteMark = false;
            info.RunCount = 0;
            info.Status = 0;

            CronExpression cronExpression = new CronExpression(info.Cron);
            info.NextRunTime =cronExpression.GetNextValidTimeAfter(DateTime.Now).ToDateTime();
            info.CreatorTime =info.NextRunTime=info.LastRunTime=info.LastModifyTime= DateTime.Now;
            info.CreatorUserId =info.LastModifyUserId= CurrentUser.UserId;
            info.CompanyId = CurrentUser.OrganizeId;
            info.DeptId = CurrentUser.DeptId;
        }
        
        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(TaskManager info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
            CronExpression cronExpression = new CronExpression(info.Cron);
            info.NextRunTime = cronExpression.GetNextValidTimeAfter(DateTime.Now).ToDateTime();
           
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(TaskManager info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }
        /// <summary>
        /// 異步更新數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(TaskManagerInputDto tinfo)
        {
            CommonResult result = new CommonResult();

            TaskManager info = iService.Get(tinfo.Id);
            info.TaskName = tinfo.TaskName;
            info.GroupName = tinfo.GroupName;
            info.JobCallAddress = tinfo.JobCallAddress;
            info.JobCallParams = tinfo.JobCallParams;
            info.Cron = tinfo.Cron;
            info.EnabledMark = tinfo.EnabledMark;
            info.Description = tinfo.Description;
            info.IsLocal = tinfo.IsLocal;
            info.SendMail = tinfo.SendMail;
            info.EmailAddress = tinfo.EmailAddress;
            info.StartTime = tinfo.StartTime;
            info.EndTime = tinfo.EndTime;


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
        /// 獲取本地可執行的任務列表
        /// </summary>
        [HttpGet("GetLocalHandlers")]
        [YuebonAuthorize("List")]
        public IActionResult GetLocalHandlers()
        {
            CommonResult result = new CommonResult();
            try
            {
                result.ResData = QueryLocalHandlers();
                result.ErrCode = ErrCode.successCode;
            }
            catch (Exception ex)
            {
                result.ErrCode ="500";
                result.ErrMsg = ex.InnerException?.Message ?? ex.Message;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 改變任務狀態，啟動/停止
        /// </summary>
        [HttpPost("ChangeStatus")]
        [YuebonAuthorize("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeJobStatusReq req)
        {
            CommonResult result = new CommonResult();
            try
            {
                TaskManager job = iService.Get(req.Id);
               
                if (job == null)
                {
                    throw new Exception("任務不存在");
                }
                OnBeforeUpdate(job);
                job.Status = req.Status;
                IScheduler scheduler =await  schedulerFactory.GetScheduler();
                if (job.Status == 0) //停止
                {
                    TriggerKey triggerKey = new TriggerKey(job.Id,job.GroupName);
                    // 停止觸發器
                    await scheduler.PauseTrigger(triggerKey);
                    // 移除觸發器
                    await scheduler.UnscheduleJob(triggerKey);
                    // 刪除任務
                    await scheduler.DeleteJob(new JobKey(job.Id));
                }
                else  //啟動
                {
                    IJobDetail jobDetail;
                    if (job.IsLocal)
                    {
                        var implementationAssembly = Assembly.Load("Yuebon.Quartz.Jobs");
                        var implementationTypes = implementationAssembly.DefinedTypes.Where(t => t.GetInterfaces().Contains(typeof(IJob)));
                        var tyeinfo = implementationTypes.Where(x => x.FullName == job.JobCallAddress).FirstOrDefault();
                        jobDetail = JobBuilder.Create(tyeinfo).WithIdentity(job.Id, job.GroupName).Build();
                    }
                    else
                    {
                        jobDetail = JobBuilder.Create<HttpResultfulJob>().WithIdentity(job.Id, job.GroupName).Build();
                    }
                    jobDetail.JobDataMap["OpenJob"] = job.Id;  //傳遞job信息
                    ITrigger trigger = TriggerBuilder.Create()
                        .WithCronSchedule(job.Cron)
                        .WithIdentity(job.Id, job.GroupName)
                        .WithDescription(job.Description)
                        .ForJob(job.Id, job.GroupName) //給任務指定一個分組
                        .StartNow()
                        .Build();
                    await scheduler.ScheduleJob(jobDetail, trigger);
                }
                if (job.Status == 1)
                {
                  await scheduler.Start();
                }
                iService.Update(job,job.Id);
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = "切換成功，可以在系統日志中查看運行結果";

            }
            catch (Exception ex)
            {
                result.ErrCode = "500";
                result.ErrMsg = ex.InnerException?.Message ?? ex.Message;
            }

            return ToJsonContent(result);
        }
        /// <summary>
        /// 異步批量設為數據有效性，設置為禁用時會停止已在運行的任務
        /// </summary>
        /// <param name="info">主鍵Id集合和有效標識，默認為1：即設為無效,0：有效</param>
        [HttpPost("SetEnabledMarktBatchAsync")]
        [YuebonAuthorize("Enable")]
        public override async Task<IActionResult> SetEnabledMarktBatchAsync(UpdateEnableViewModel info)
        {
            CommonResult result = new CommonResult();
            bool bl = false;
            if (info.Flag == "1")
            {
                bl = true;
            }
            string where = string.Empty;
            where = "id in ('" + info.Ids.Join(",").Replace(",", "','") + "')";
            dynamic[] jobsId = info.Ids;
            if (!bl)
            {
                foreach (var item in jobsId)
                {
                    if (string.IsNullOrEmpty(item.ToString())) continue;
                    TaskManager job = iService.Get(item.ToString());
                    if (job == null)
                    {
                        throw new Exception("任務不存在");
                    }
                    IScheduler scheduler = await schedulerFactory.GetScheduler();
                    TriggerKey triggerKey = new TriggerKey(job.Id, job.GroupName);
                    // 停止觸發器
                    await scheduler.PauseTrigger(triggerKey);
                    // 移除觸發器
                    await scheduler.UnscheduleJob(triggerKey);
                    // 刪除任務
                    await scheduler.DeleteJob(new JobKey(job.Id));
                }
            }
            if (!string.IsNullOrEmpty(where))
            {
                bool blresut = await iService.SetEnabledMarkByWhereAsync(bl, where, CurrentUser.UserId);
                if (blresut)
                {
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                }
                else
                {
                    result.ErrMsg = ErrCode.err43002;
                    result.ErrCode = "43002";
                }
            }
            return ToJsonContent(result);
        }
        /// <summary>
        /// 異步批量物理刪除
        /// </summary>
        /// <param name="info">刪除信息</param>
        [HttpDelete("DeleteBatchAsync")]
        [YuebonAuthorize("Delete")]
        public override async Task<IActionResult> DeleteBatchAsync(DeletesInputDto info)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            where = "id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            dynamic[] jobsId = info.Ids;
            foreach (var item in jobsId)
            {
                if (string.IsNullOrEmpty(item.ToString())) continue;
                TaskManager job = iService.Get(item.ToString());
                if (job == null)
                {
                    throw new Exception("任務不存在");
                }
                IScheduler scheduler = await schedulerFactory.GetScheduler();
                TriggerKey triggerKey = new TriggerKey(job.Id, job.GroupName);
                // 停止觸發器
                await scheduler.PauseTrigger(triggerKey);
                // 移除觸發器
                await scheduler.UnscheduleJob(triggerKey);
                // 刪除任務
                await scheduler.DeleteJob(new JobKey(job.Id));
            }

            if (!string.IsNullOrEmpty(where))
            {
                bool bl = await iService.DeleteBatchWhereAsync(where).ConfigureAwait(false);
                if (bl)
                {
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                }
                else
                {
                    result.ErrMsg = ErrCode.err43003;
                    result.ErrCode = "43003";
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 返回系統的job接口
        /// </summary>
        /// <returns></returns>
        private static List<LocalTaskModel> QueryLocalHandlers()
        {
            var implementationAssembly = Assembly.Load("Yuebon.Quartz.Jobs");
            var implementationTypes =implementationAssembly.DefinedTypes.Where(t =>t.GetInterfaces().Contains(typeof(IJob))).ToArray(); 
            List<string> list= implementationTypes.Select(u => u.FullName).ToList();
            List<LocalTaskModel> resulList = new List<LocalTaskModel>();
            foreach(var item in list)
            {
                LocalTaskModel localTaskModel = new LocalTaskModel();
                localTaskModel.FullName = item;
                resulList.Add(localTaskModel);
            }
            return resulList;
        }
    }
}