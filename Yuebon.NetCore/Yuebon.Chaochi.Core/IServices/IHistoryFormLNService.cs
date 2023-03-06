using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Yuebon.Commons.Enums;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Pages;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Chaochi.Core.Dtos;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 歷史表單服務接口
    /// </summary>
    public interface IHistoryFormLNService : IService<HistoryFormLN, HistoryFormLNOutputDto, string>
    {
        Task<PageResult<HistoryFormLNOutputDto>> FindWithPagerSearchAsync(SearchHistoryFormLNModel search);
    }
}
