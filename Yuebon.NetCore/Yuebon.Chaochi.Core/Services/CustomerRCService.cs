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
using Yuebon.Chaochi.Core.Dtos;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class CustomerRCService: BaseService<CustomerRC,CustomerRCOutputDto, string>, ICustomerRCService
    {
		private readonly ICustomerRCRepository _repository;
        private readonly Security.IServices.IUserService _userService;

        public CustomerRCService(ICustomerRCRepository repository,Security.IServices.IUserService userService) : base(repository)
        {
			_repository=repository;
            _userService = userService;
        }

        /// <summary>
        /// 新增存取權
        /// </summary>
        /// <param name="customerLN"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> InsertAccess(CustomerRC customerRC, string userId, string userDeptId)
        {
            return await _repository.UpdateTableFieldAsync("CreatorUserId", $"{customerRC.CreatorUserId},{userId}", $"Id='{customerRC.Id}'") &&
            await _repository.UpdateTableFieldAsync("CreatorUserDeptId", $"{customerRC.CreatorUserDeptId},{userDeptId}", $"Id='{customerRC.Id}'");
        }

        /// <summary>
        /// 覆寫GetOutDtoAsync(因為家庭成員記錄在RNFOoutputDto)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<CustomerRCOutputDto> GetOutDtoAsync(string id)
        {
            var info = await repository.GetAsync(id);
            var outinfo = info.MapTo<CustomerRCOutputDto>();
            outinfo.RCFOutputDto = info.MapTo<RCFOutputDto>();
            return outinfo;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public async Task<PageResult<CustomerRCOutputDto>> FindWithPagerSearchAsync(SearchCustomerRCModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(true);
            var filter = search.Filter;

            if (filter is not null) {
                if (!string.IsNullOrEmpty(filter.RCAdd))
                    where += $" and RCAdd like '%{filter.RCAdd}%'";
                if (!string.IsNullOrEmpty(filter.RCName))
                    where += $" and RCName like '%{filter.RCName}%'";
                if (!string.IsNullOrEmpty(filter.RCRep))
                    where += $" and RCRep like '%{filter.RCRep}%'";
                if (!string.IsNullOrEmpty(filter.RCID))
                    where += $" and RCID like '%{filter.RCID}%'";
                if (!string.IsNullOrEmpty(filter.RCTel_1))
                    where += $" and RCTel_1 like '%{filter.RCTel_1}%'";
                if (!string.IsNullOrEmpty(filter.RCTel_2))
                    where += $" and RCTel_2 like '%{filter.RCTel_2}%'";
                if (!string.IsNullOrEmpty(filter.CreatorUserId)) {
                    where += $" and  CreatorUserId like '%{filter.CreatorUserId}%'";
                }
            }

            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<CustomerRC> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<CustomerRCOutputDto> resultList = list.MapTo<CustomerRCOutputDto>();
            List<CustomerRCOutputDto> listResult = new List<CustomerRCOutputDto>();
            foreach (CustomerRCOutputDto item in resultList) {
                if (item.CreatorUserId.Contains(",")) {
                    var CreatorUserNames = "";
                    foreach (var item2 in item.CreatorUserId.Split(",")) {
                        CreatorUserNames += _userService.Get(item2).RealName + ",";
                    }
                    item.CreatorUserName = CreatorUserNames;
                } else {
                    item.CreatorUserName = _userService.Get(item.CreatorUserId).RealName;
                }
                listResult.Add(item);
            }
            PageResult<CustomerRCOutputDto> pageResult = new PageResult<CustomerRCOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}