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
    public class ContractformService: BaseService<Contractform,ContractformOutputDto, string>, IContractformService
    {
		private readonly IContractformRepository _repository;
        public ContractformService(IContractformRepository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據表單編號查詢表單
        /// </summary>
        /// <param name="formId">表單編號</param>
        /// <returns></returns>
        public async Task<ContractformOutputDto> GetByFormId(string formId)
        {
            Contractform contractForm = await _repository.GetWhereAsync(string.Format("FormId = '{0}'", formId));
            ContractformOutputDto outputDto = contractForm.MapTo<ContractformOutputDto>();

            return outputDto;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public override async Task<PageResult<ContractformOutputDto>> FindWithPagerAsync(SearchInputDto<Contractform> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            var filter = search.Filter;

            if (filter != null) {
                // 表單編號
                if (!string.IsNullOrWhiteSpace(filter.FormId)) {
                    where += string.Format(" and FormId like '%{0}%'", filter.FormId);
                }

                // 表單名稱
                if (!string.IsNullOrWhiteSpace(filter.FormName)) {
                    where += string.Format(" and FormName like '%{0}%'", filter.FormName);
                }

                // 合約分類
                if (!string.IsNullOrWhiteSpace(filter.CateId)) {
                    where += string.Format(" and CateId like '%{0}%'", filter.CateId);
                }
            }
           
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Contractform> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<ContractformOutputDto> resultList = list.MapTo<ContractformOutputDto>();

            PageResult<ContractformOutputDto> pageResult = new PageResult<ContractformOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = resultList,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}