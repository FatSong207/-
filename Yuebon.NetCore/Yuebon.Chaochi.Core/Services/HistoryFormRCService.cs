using System;
using System.Data.Common;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Repositories;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using System.Data;
using Yuebon.Chaochi.Dtos;
using System.Collections.Generic;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Mapping;
using System.Threading.Tasks;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Enums;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Chaochi.Core.Dtos;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class HistoryFormRCService : BaseService<HistoryFormRC, HistoryFormRCOutputDto, string>, IHistoryFormRCService
    {
        private readonly IHistoryFormRCRepository _repository;
        private readonly Security.IServices.IUserService _userService;
        private readonly ICustomerRCService _customerRCService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public HistoryFormRCService(IHistoryFormRCRepository repository, Security.IServices.IUserService userService, ICustomerRCService customerRCService) : base(repository)
        {
            _repository = repository;
            _userService = userService;
            _customerRCService = customerRCService;
        }

        public async Task<PageResult<HistoryFormRCOutputDto>> FindWithPagerSearchAsync(SearchHistoryFormRCModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            if (!string.IsNullOrEmpty(search.Keywords)) {
                var curLNID = _customerRCService.Get(search.Keywords).RCID;
                where += $"and IDNo='{curLNID}'";
            }
            if (search.Filter is not null) {
                if (!string.IsNullOrEmpty(search.Filter.Note)) {
                    search.Filter.Note = search.Filter.Note.Replace(",", "' and '");
                    where += $" and UploadTime between '{search.Filter.Note} 23:59:59 '";
                }
            }
            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<HistoryFormRC> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<HistoryFormRCOutputDto> resultList = list.MapTo<HistoryFormRCOutputDto>();
            List<HistoryFormRCOutputDto> listResult = new List<HistoryFormRCOutputDto>();
            foreach (HistoryFormRCOutputDto item in resultList) {
                item.CreatorUserId = string.IsNullOrEmpty(item.CreatorUserId) ? "" : _userService.Get(item.CreatorUserId).RealName;
                listResult.Add(item);
            }
            PageResult<HistoryFormRCOutputDto> pageResult = new PageResult<HistoryFormRCOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}