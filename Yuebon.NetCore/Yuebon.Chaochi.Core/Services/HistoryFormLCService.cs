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
    public class HistoryFormLCService : BaseService<HistoryFormLC, HistoryFormLCOutputDto, string>, IHistoryFormLCService
    {
        private readonly IHistoryFormLCRepository _repository;
        private readonly Security.IServices.IUserService _userService;
        private readonly ICustomerLCService _customerLCService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public HistoryFormLCService(IHistoryFormLCRepository repository,Security.IServices.IUserService userService, ICustomerLCService customerLCService) : base(repository)
        {
            _repository = repository;
            _userService = userService;
            _customerLCService = customerLCService;
        }

        public async Task<PageResult<HistoryFormLCOutputDto>> FindWithPagerSearchAsync(SearchHistoryFormLCModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            if (!string.IsNullOrEmpty(search.Keywords)) {
                var curLCID = _customerLCService.Get(search.Keywords).LCID;
                where += $"and IDNo='{curLCID}'";
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
            List<HistoryFormLC> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<HistoryFormLCOutputDto> resultList = list.MapTo<HistoryFormLCOutputDto>();
            List<HistoryFormLCOutputDto> listResult = new List<HistoryFormLCOutputDto>();
            foreach (HistoryFormLCOutputDto item in resultList) {
                item.CreatorUserId = string.IsNullOrEmpty(item.CreatorUserId) ? "" : _userService.Get(item.CreatorUserId).RealName;
                listResult.Add(item);
            }
            PageResult<HistoryFormLCOutputDto> pageResult = new PageResult<HistoryFormLCOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}