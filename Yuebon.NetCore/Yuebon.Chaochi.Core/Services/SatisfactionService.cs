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
using System.Data;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class SatisfactionService: BaseService<Satisfaction,SatisfactionOutputDto, string>, ISatisfactionService
    {
		private readonly ISatisfactionRepository _repository;
        private readonly Security.IServices.IUserService _userService;

        public SatisfactionService(ISatisfactionRepository repository,Yuebon.Security.IServices.IUserService userService) : base(repository)
        {
			_repository=repository;
            _userService = userService;
        }

        public override async Task<PageResult<SatisfactionOutputDto>> FindWithPagerAsync(SearchInputDto<Satisfaction> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Satisfaction> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order, conn, trans);
            PageResult<SatisfactionOutputDto> pageResult = new PageResult<SatisfactionOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<SatisfactionOutputDto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            foreach (var item in pageResult.Items) {
                item.LastModifyUserName =_userService.Get(item.LastModifyUserId).RealName;
            }
            return pageResult;
        }

        public async Task<dynamic> GetTableDataForXLSX(string eventId)
        {
            var e = await _repository.GetAsync(eventId);
            var data = await _repository.GetTableDataForXLSX(eventId, "");
            return data;
        }

        public async Task<List<string>> GetTableTitleArr(string eventId)
        {
            return await _repository.GetTableTitleArr(eventId);
        }

        public async Task<int> GetFinishCount(string questTopicId)
        {
           return await _repository.GetFinishCount(questTopicId);
        }
    }
}