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
using Org.BouncyCastle.Asn1.Ocsp;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class HistoryFormLNService : BaseService<HistoryFormLN, HistoryFormLNOutputDto, string>, IHistoryFormLNService
    {
        private readonly IHistoryFormLNRepository _repository;
        private readonly Security.IServices.IUserService _userService;
        private readonly ICustomerLNService _customerLNService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public HistoryFormLNService(IHistoryFormLNRepository repository, Security.IServices.IUserService userService, ICustomerLNService customerLNService) : base(repository)
        {
            _repository = repository;
            _userService = userService;
            _customerLNService = customerLNService;
        }



        public async Task<PageResult<HistoryFormLNOutputDto>> FindWithPagerSearchAsync(SearchHistoryFormLNModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            if (!string.IsNullOrEmpty(search.Keywords)) {
                var curLNID =await _customerLNService.GetAsync(search.Keywords);
                where += $"and IDNo='{curLNID.LNID}'";
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
            List<HistoryFormLN> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<HistoryFormLNOutputDto> resultList = list.MapTo<HistoryFormLNOutputDto>();
            List<HistoryFormLNOutputDto> listResult = new List<HistoryFormLNOutputDto>();
            foreach (HistoryFormLNOutputDto item in resultList) {
                item.CreatorUserId = string.IsNullOrEmpty(item.CreatorUserId) ? "" : _userService.Get(item.CreatorUserId).RealName;
                listResult.Add(item);
            }
            PageResult<HistoryFormLNOutputDto> pageResult = new PageResult<HistoryFormLNOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}