using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義行政工作日誌明細倉儲接口
    /// </summary>
    public interface IAdministrativeWorkLogDetailsRepository:IRepository<AdministrativeWorkLogDetails, string>
    {
    }
}