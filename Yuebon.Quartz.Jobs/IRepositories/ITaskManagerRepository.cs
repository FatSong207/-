using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Quartz.Models;
using Yuebon.Security.Models;

namespace Yuebon.Quartz.IRepositories
{
    /// <summary>
    /// 定義定時任務倉儲接口
    /// </summary>
    public interface ITaskManagerRepository:IRepository<TaskManager, string>
    {
    }
}