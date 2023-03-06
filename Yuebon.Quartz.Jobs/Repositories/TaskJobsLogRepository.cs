using System;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Quartz.IRepositories;
using Yuebon.Quartz.Models;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Quartz.Repositories
{
    /// <summary>
    /// 定時任務執行日志倉儲接口的實現
    /// </summary>
    public class TaskJobsLogRepository : BaseRepository<TaskJobsLog, string>, ITaskJobsLogRepository
    {
		public TaskJobsLogRepository()
        {
        }

        public TaskJobsLogRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}