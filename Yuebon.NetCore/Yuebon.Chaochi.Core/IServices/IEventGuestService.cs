using System;
using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface IEventGuestService:IService<EventGuest,EventGuestOutputDto, string>
    {
        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        Task<PageResult<EventGuestOutputDto>> FindWithPagerSearchAsync(SearchInputDto<EventGuestOutputDto> search);
    }
}
