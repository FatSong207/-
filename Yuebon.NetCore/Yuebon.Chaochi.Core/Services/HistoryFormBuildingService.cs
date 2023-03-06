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
using System.Linq;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class HistoryFormBuildingService : BaseService<HistoryFormBuilding, HistoryFormBuildingOutputDto, string>, IHistoryFormBuildingService
    {
        private readonly IHistoryFormBuildingRepository _repository;
        private readonly Security.IServices.IUserService _userService;
        private readonly ICustomerLNService _customerLNService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public HistoryFormBuildingService(IHistoryFormBuildingRepository repository,Security.IServices.IUserService userService, ICustomerLNService customerLNService) : base(repository)
        {
            _repository = repository;
            _userService = userService;
            _customerLNService = customerLNService;
        }

        public async Task<PageResult<HistoryFormBuildingOutputDto>> FindWithPagerSearchAsync(SearchHistoryFormBuildingModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            // 合約用
            if (!string.IsNullOrEmpty(search.Keywords)) {
                //var curLNID = _customerLNService.Get(search.Keywords).LNID;
                where += $"and IDNo='{search.Keywords}'";
                // 過濾zip和rar
                where += $"and FileName not like '%.zip' and FileName not like '%.rar' ";
            }
            if (search.Filter is not null) {
                if (!string.IsNullOrEmpty(search.Filter.IDNo)) {
                    where += $" and IDNo = '{search.Filter.IDNo}' ";
                }
                if (!string.IsNullOrEmpty(search.Filter.Note)) {
                    search.Filter.Note = search.Filter.Note.Replace(",", "' and '");
                    where += $" and UploadTime between '{search.Filter.Note} 23:59:59 '";
                }
            }
            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<HistoryFormBuilding> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<HistoryFormBuildingOutputDto> resultList = list.MapTo<HistoryFormBuildingOutputDto>();
            List<HistoryFormBuildingOutputDto> listResult = new List<HistoryFormBuildingOutputDto>();
            foreach (HistoryFormBuildingOutputDto item in resultList) {
                item.CreatorUserId = string.IsNullOrEmpty(item.CreatorUserId) ? "" : _userService.Get(item.CreatorUserId).RealName;
                listResult.Add(item);
            }
            PageResult<HistoryFormBuildingOutputDto> pageResult = new PageResult<HistoryFormBuildingOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}