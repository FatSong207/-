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
    /// 內部表單服務接口實現
    /// </summary>
    public class SecurityFormService: BaseService<SecurityForm,SecurityFormOutputDto, string>, ISecurityFormService
    {
		private readonly ISecurityFormRepository _repository;
        public SecurityFormService(ISecurityFormRepository repository) : base(repository)
        {
			_repository=repository;
        }

        public async Task<PageResult<SecurityFormOutputDto>> FindWithPagerSearchAsync(SearchInputDto<SecurityForm> search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);

            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<SecurityForm> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<SecurityFormOutputDto> resultList = list.MapTo<SecurityFormOutputDto>();
            List<SecurityFormOutputDto> listResult = new List<SecurityFormOutputDto>();
            foreach (SecurityFormOutputDto item in resultList) {
                listResult.Add(item);
            }
            PageResult<SecurityFormOutputDto> pageResult = new PageResult<SecurityFormOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}