using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Quartz.Models;
using Yuebon.Security.Models;

namespace Yuebon.Quartz.IRepositories
{
    /// <summary>
    /// 定義定時任務執行日志倉儲接口
    /// </summary>
    public interface ITaskJobsLogRepository:IRepository<TaskJobsLog, string>
    {
    }
}