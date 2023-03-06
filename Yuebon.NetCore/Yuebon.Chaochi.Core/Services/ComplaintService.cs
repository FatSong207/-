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
    public class ComplaintService: BaseService<Complaint,ComplaintOutputDto, string>, IComplaintService
    {
		private readonly IComplaintRepository _repository;
        private readonly Security.IServices.IUserService _userService;

        public ComplaintService(IComplaintRepository repository,Yuebon.Security.IServices.IUserService userService) : base(repository)
        {
			_repository=repository;
            _userService = userService;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public async Task<PageResult<ComplaintOutputDto>> FindWithPagerSearchAsync(SearchInputDto<ComplaintOutputDto> search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            var filter = search.Filter;

            if (filter != null) {
                if (!string.IsNullOrEmpty(filter.State)) 
                    where += $" and State = '{filter.State}' ";
                if (!string.IsNullOrEmpty(filter.ComplaintType)) 
                    where += $" and ComplaintType = '{filter.ComplaintType}' ";
                if (!string.IsNullOrEmpty(filter.ComplaintCreatorTime)) {
                    var ComplaintCreatorTime = TranROCDateToDate(filter.ComplaintCreatorTime);
                    where += $" and ComplaintCreatorTime >= '{ComplaintCreatorTime}' ";
                }
                if (!string.IsNullOrEmpty(filter.CCode))
                    where += $" and CCode like '%{filter.CCode}%' ";
                if (!string.IsNullOrEmpty(filter.ComplaintDept))
                    where += $" and ComplaintDept like '%{filter.ComplaintDept}%' ";
                if (!string.IsNullOrEmpty(filter.Complainee))
                    where += $" and Complainee like '%{filter.Complainee}%' ";
                if (!string.IsNullOrEmpty(filter.CId))
                    where += $" and CId like '%{filter.CId}%' ";
            }

            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Complaint> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<ComplaintOutputDto> resultList = list.MapTo<ComplaintOutputDto>();
            List<ComplaintOutputDto> listResult = new List<ComplaintOutputDto>();
            foreach (ComplaintOutputDto item in resultList) {
                item.ComplaintCreatorTime = TranDateToROCDate(item.ComplaintCreatorTime);
                item.SendTrialTime = TranDateToROCDate(item.SendTrialTime);
                item.EndCaseTime = TranDateToROCDate(item.EndCaseTime);
                if (!string.IsNullOrEmpty(item.HandleUser)) {
                    item.HandleUserName = _userService.GetAsync(item.HandleUser).Result.RealName;
                }
                listResult.Add(item);
            }
            PageResult<ComplaintOutputDto> pageResult = new PageResult<ComplaintOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;

        }

    }
}