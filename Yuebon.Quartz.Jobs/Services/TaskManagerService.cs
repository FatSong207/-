using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.DependencyInjection;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Enums;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Quartz.Dtos;
using Yuebon.Quartz.IRepositories;
using Yuebon.Quartz.IServices;
using Yuebon.Quartz.Models;
using Yuebon.Security.IServices;

namespace Yuebon.Quartz.Services
{
    /// <summary>
    /// 定時任務服務接口實現
    /// </summary>
    public class TaskManagerService: BaseService<TaskManager,TaskManagerOutputDto, string>, ITaskManagerService, IScopedDependency
    {
		private readonly ITaskManagerRepository _repository;
        private readonly ILogService _logService;
        private readonly ITaskJobsLogService _taskJobsLogService;
        /// <summary>
        /// 
        /// </summary>
        private ISchedulerFactory schedulerFactory;
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logService"></param>
        /// <param name="taskJobsLogService"></param>
        public TaskManagerService(ITaskManagerRepository repository,ILogService logService, ITaskJobsLogService taskJobsLogService, ISchedulerFactory _schedulerFactory) : base(repository)
        {
			_repository=repository;
			_logService=logService;
            _taskJobsLogService = taskJobsLogService;
            schedulerFactory = _schedulerFactory;
        }

        /// <summary>
        /// 記錄任務運行結果
        /// </summary>
        /// <param name="jobId">任務Id</param>
        /// <param name="jobAction">任務執行動作</param>
        /// <param name="blresultTag">任務執行結果表示，true成功，false失敗，初始執行為true</param>
        /// <param name="msg">任務記錄描述</param>
        public void RecordRun(string jobId,JobAction jobAction, bool blresultTag = true,string msg="")
        {
            DateTime addTime = DateTime.Now;
            TaskManager job =  _repository.GetSingle(jobId);
            if (job == null)
            {
                _taskJobsLogService.Insert(new TaskJobsLog
                {
                    Id = GuidUtils.CreateNo(),
                    CreatorTime = DateTime.Now,
                    TaskId = jobId,
                    TaskName = "",
                    JobAction = jobAction.ToString(),
                    Status = false,
                    Description = $"未能找到定時任務：{jobId}"
                }) ; 
                return;
            }
            string resultStr = string.Empty,strDesc=string.Empty;
            if (!blresultTag)
            {
                job.ErrorCount++;
                job.LastErrorTime= addTime;
                strDesc = $"異常，"+msg;
               
            }
            else
            {
                strDesc = $"正常，" + msg;
            }
            if (jobAction == JobAction.開始)
            {
                job.RunCount++;
                job.LastRunTime = addTime;

                CronExpression cronExpression = new CronExpression(job.Cron);
                job.NextRunTime = cronExpression.GetNextValidTimeAfter(addTime).ToDateTime();
            }
            _repository.Update(job,jobId);

            _taskJobsLogService.Insert(new TaskJobsLog
            {
                Id = GuidUtils.CreateNo(),
                CreatorTime = DateTime.Now,
                TaskId = job.Id,
                TaskName = job.TaskName,
                JobAction = jobAction.ToString(),
                Status = blresultTag,
                Description = strDesc
            });
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public override async Task<PageResult<TaskManagerOutputDto>> FindWithPagerAsync(SearchInputDto<TaskManager> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege();
            if (!string.IsNullOrEmpty(search.Keywords))
            {
                where += string.Format(" and (TaskName like '%{0}%' or  GroupName like '%{0}%')", search.Keywords);
            };
            if (!string.IsNullOrEmpty(search.Filter?.Cron))
            {
                where += string.Format(" and Cron like '%{0}%' ", search.Filter.Cron);
            };
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<TaskManager> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            PageResult<TaskManagerOutputDto> pageResult = new PageResult<TaskManagerOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<TaskManagerOutputDto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}