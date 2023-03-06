using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using System.Collections.Generic;
using Yuebon.Commons.Mapping;
using System.Threading.Tasks;
using Yuebon.Commons.Pages;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Chaochi.Core.IServices;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class HistoryFormRNService : BaseService<HistoryFormRN, HistoryFormRNOutputDto, string>, IHistoryFormRNService
    {
        private readonly IHistoryFormRNRepository _repository;
        private readonly Security.IServices.IUserService _userService;
        private readonly ICustomerRNService _customerRNService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public HistoryFormRNService(IHistoryFormRNRepository repository, Security.IServices.IUserService userService, ICustomerRNService customerRNService) : base(repository)
        {
            _repository = repository;
            _userService = userService;
            _customerRNService = customerRNService;
        }

        public async Task<PageResult<HistoryFormRNOutputDto>> FindWithPagerSearchAsync(SearchHistoryFormRNModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            if (!string.IsNullOrEmpty(search.Keywords)) {
                var curLNID = _customerRNService.Get(search.Keywords).RNID;
                where += $"and IDNo='{curLNID}'";
            }
            if (search.Filter is not null) {
                if (!string.IsNullOrEmpty(search.Filter.Note)) {
                    search.Filter.Note = search.Filter.Note.Replace(",", "' and '");
                    where += $" and UploadTime between '{search.Filter.Note} 23:59:59 '";
                }
                if (!string.IsNullOrEmpty(search.Filter.IDNo)) {
                    where += $"and IDNo='{search.Filter.IDNo}'";
                }
            }
            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<HistoryFormRN> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<HistoryFormRNOutputDto> resultList = list.MapTo<HistoryFormRNOutputDto>();
            List<HistoryFormRNOutputDto> listResult = new List<HistoryFormRNOutputDto>();
            foreach (HistoryFormRNOutputDto item in resultList) {
                item.CreatorUserId = string.IsNullOrEmpty(item.CreatorUserId) ? "" : _userService.Get(item.CreatorUserId).RealName;
                listResult.Add(item);
            }
            PageResult<HistoryFormRNOutputDto> pageResult = new PageResult<HistoryFormRNOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}