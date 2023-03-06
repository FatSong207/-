using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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
    /// <summary>
    /// 
    /// </summary>
    public class UploadFileService : BaseService<UploadFile, UploadFileOutputDto, string>, IUploadFileService
    {
        private readonly IUploadFileRepository _uploadFileRepository;
        private readonly ILogService _logService;
        public UploadFileService(IUploadFileRepository repository, ILogService logService) : base(repository)
        {
            _uploadFileRepository = repository;
            _logService = logService;
        }

        /// <summary>
        /// 根據應用Id和應用標識批量更新數據
        /// </summary>
        /// <param name="beLongAppId">應用Id</param>
        /// <param name="oldBeLongAppId">更新前舊的應用Id</param>
        /// <param name="belongApp">應用標識</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool UpdateByBeLongAppId(string beLongAppId, string oldBeLongAppId, string belongApp = null, IDbTransaction trans = null)
        {
            return _uploadFileRepository.UpdateByBeLongAppId(beLongAppId, oldBeLongAppId, belongApp, trans);
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public override async Task<PageResult<UploadFileOutputDto>> FindWithPagerAsync(SearchInputDto<UploadFile> search , IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            if (!string.IsNullOrEmpty(search.Keywords))
            {
                where += string.Format(" and  FileName like '%{0}%' ", search.Keywords);
            };
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<UploadFile> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            PageResult<UploadFileOutputDto> pageResult = new PageResult<UploadFileOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<UploadFileOutputDto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}
