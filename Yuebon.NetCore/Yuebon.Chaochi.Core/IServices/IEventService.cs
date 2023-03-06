using System;
using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;
using System.Collections.Generic;
using static Yuebon.Chaochi.Repositories.EventRepository;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface IEventService:IService<Event,EventOutputDto, string>
    {

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        Task<PageResult<EventOutputDto>> FindWithPagerSearchAsync(SearchInputDto<EventOutputDto> search);

        Task<PageResult<EventDashboardOutputDto>> FindWithPagerSearchAsyncED(SearchInputDto<EventDashboardOutputDto> search);

        Task<List<string>> GetTableTitleArr(string eventId);

        Task<dynamic> GetTableData(string eventId);

        Task<dynamic> GetTableDataForXLSX(string eventId);

        Task<bool> DeleteSignUP(string eId);
    }
}
