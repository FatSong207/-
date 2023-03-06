using System;
using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.Core.Dtos;
using System.Threading.Tasks;
using Yuebon.Commons.Pages;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義業務工作日誌服務接口
    /// </summary>
    public interface ISalesWorkLogService:IService<SalesWorkLog,SalesWorkLogOutputDto, string>
    {
        Task<PageResult<SalesWorkLogOutputDto>> FindWithPagerSearchAsync(SearchSalesWorkLogModel search);
    }
}
