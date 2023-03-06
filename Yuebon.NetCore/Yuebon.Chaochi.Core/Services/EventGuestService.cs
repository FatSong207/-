using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class EventGuestService: BaseService<EventGuest,EventGuestOutputDto, string>, IEventGuestService
    {
		private readonly IEventGuestRepository _repository;
        public EventGuestService(IEventGuestRepository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public async Task<PageResult<EventGuestOutputDto>> FindWithPagerSearchAsync(SearchInputDto<EventGuestOutputDto> search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            where += $" and EventId = '{search.Keywords}' ";

            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<EventGuest> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, true);
            List<EventGuestOutputDto> resultList = list.MapTo<EventGuestOutputDto>();
            List<EventGuestOutputDto> listResult = new List<EventGuestOutputDto>();
            foreach (EventGuestOutputDto item in resultList) {
                listResult.Add(item);
            }
            PageResult<EventGuestOutputDto> pageResult = new PageResult<EventGuestOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;

        }

    }
}