using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義業務工作日誌明細倉儲接口
    /// </summary>
    public interface ISalesWorkLogDetailsRepository:IRepository<SalesWorkLogDetails, string>
    {
    }
}