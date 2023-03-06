﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.Security.Services
{
    public class FilterIPService: BaseService<FilterIP, FilterIPOutputDto, string>, IFilterIPService
    {
        private readonly IFilterIPRepository _repository;
        private readonly ILogService _logService;
        public FilterIPService(IFilterIPRepository repository, ILogService logService) : base(repository)
        {
            _repository = repository;
            _logService = logService;
        }
        /// <summary>
        /// 驗證IP地址是否被拒絕
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool ValidateIP(string ip)
        {
          return  _repository.ValidateIP(ip);
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public override async Task<PageResult<FilterIPOutputDto>> FindWithPagerAsync(SearchInputDto<FilterIP> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            if (!string.IsNullOrEmpty(search.Keywords))
            {
                where += string.Format(" and (StartIP like '%{0}%' or EndIP like '%{0}%')", search.Keywords);
            };
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<FilterIP> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            PageResult<FilterIPOutputDto> pageResult = new PageResult<FilterIPOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<FilterIPOutputDto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}