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
using System.Data;
using System.Linq;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 業務拜訪記錄服務接口實現
    /// </summary>
    public class VisitingRecordService: BaseService<VisitingRecord,VisitingRecordOutputDto, string>, IVisitingRecordService
    {
        private readonly Security.IServices.IUserService _userService;
        private readonly IVisitingRecordRepository _repository;

        public VisitingRecordService(IVisitingRecordRepository repository, Security.IServices.IUserService userService) : base(repository)
        {
			_repository=repository;
            _userService = userService;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// 查詢條件變換時請重寫該方法。
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public override async Task<PageResult<VisitingRecordOutputDto>> FindWithPagerAsync(SearchInputDto<VisitingRecord> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege();
            var filter = search.Filter;

            if (filter != null) {
                // 姓名
                if (!string.IsNullOrWhiteSpace(filter.PID))
                    where += string.Format(" and PID = '{0}'", filter.PID);
            }

            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<VisitingRecord> list = await _repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order, conn, trans);

            List<VisitingRecordOutputDto> resultList = new List<VisitingRecordOutputDto>();
            if (list != null && list.Count > 0) {
                resultList = list.MapTo<VisitingRecordOutputDto>();
                foreach (VisitingRecordOutputDto item in resultList) {
                    var sales = _userService.GetByUserName(item.CreatorUserId).Result;
                    item.SalesName = (sales != null) ? sales.RealName : $"{item.CreatorUserId}(帳號己不存在)";
                }
            }

            PageResult<VisitingRecordOutputDto> pageResult = new PageResult<VisitingRecordOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = resultList,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        /// <summary>
        /// 根據潛在客編號查詢最新訪談記錄
        /// </summary>
        /// <param name="pid">潛在客編號</param>
        /// <param name="conn">DB連線</param>
        /// <param name="trans">DB交易</param>
        /// <returns></returns>
        public async Task<VisitingRecordOutputDto> GetLatestByPID(string pid, IDbConnection conn = null, IDbTransaction trans = null)
        {
            string where = string.Format("PID = '{0}' order by CreatorTime,LastModifyTime desc", pid);

            VisitingRecordOutputDto vrODto = new VisitingRecordOutputDto();
            IEnumerable<VisitingRecord> vrList = await _repository.GetListWhereAsync(where, conn, trans);
            if (vrList.Count() > 0) {
                VisitingRecord visitingRecord = vrList.FirstOrDefault();
                vrODto = visitingRecord.MapTo<VisitingRecordOutputDto>();
            }

            return vrODto;
        }
    }
}