using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
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
    /// 定時任務執行日志服務接口實現
    /// </summary>
    public class TaskJobsLogService: BaseService<TaskJobsLog,TaskJobsLogOutputDto, string>, ITaskJobsLogService
    {
		private readonly ITaskJobsLogRepository _repository;
        private readonly ILogService _logService;
        public TaskJobsLogService(ITaskJobsLogRepository repository,ILogService logService) : base(repository)
        {
			_repository=repository;
			_logService=logService;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public override async Task<PageResult<TaskJobsLogOutputDto>> FindWithPagerAsync(SearchInputDto<TaskJobsLog> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            if (!string.IsNullOrEmpty(search.Keywords))
            {
                where += string.Format(" and (TaskId like '%{0}%' or  TaskName like '%{0}%')", search.Keywords);
            };
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<TaskJobsLog> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            PageResult<TaskJobsLogOutputDto> pageResult = new PageResult<TaskJobsLogOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<TaskJobsLogOutputDto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}