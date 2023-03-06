using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.Cache;
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
    public class APPService : BaseService<APP, AppOutputDto, string>, IAPPService
    {
        private readonly IAPPRepository _appRepository;
        private readonly ILogService _logService;
        public APPService(IAPPRepository repository, ILogService logService) : base(repository)
        {
            _appRepository = repository;
            _logService = logService;
        }

        /// <summary>
        /// 同步新增實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public override long Insert(APP entity, IDbConnection conn = null, IDbTransaction trans = null)
        {
            long result = repository.Insert(entity, conn, trans);
            this.UpdateCacheAllowApp();
            return result;
        }

        /// <summary>
        /// 異步更新實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="id">主鍵ID</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public override async Task<bool> UpdateAsync(APP entity, string id, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = await repository.UpdateAsync(entity, id, conn, trans);
            this.UpdateCacheAllowApp();
            return result;
        }
        /// <summary>
        /// 異步步新增實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public override async Task<long> InsertAsync(APP entity, IDbConnection conn = null, IDbTransaction trans = null)
        {
            long result = await repository.InsertAsync(entity, conn, trans);
            this.UpdateCacheAllowApp();
            return result;
        }
        /// <summary>
        /// 獲取app對象
        /// </summary>
        /// <param name="appid">應用ID</param>
        /// <param name="secret">應用密鑰AppSecret</param>
        /// <returns></returns>
        public APP GetAPP(string appid, string secret)
        {
            return _appRepository.GetAPP(appid, secret);
        }
        /// <summary>
        /// 獲取app對象
        /// </summary>
        /// <param name="appid">應用ID</param>
        /// <returns></returns>
        public APP GetAPP(string appid)
        {
            return _appRepository.GetAPP(appid);
        }
        public IList<AppOutputDto> SelectApp()
        {
            return _appRepository.SelectApp();
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public override async Task<PageResult<AppOutputDto>> FindWithPagerAsync(SearchInputDto<APP> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            if (!string.IsNullOrEmpty(search.Keywords)) {
                where += string.Format(" and (AppId like '%{0}%' or RequestUrl like '%{0}%')", search.Keywords);
            };
            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<APP> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order, conn, trans);
            PageResult<AppOutputDto> pageResult = new PageResult<AppOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<AppOutputDto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
        public void UpdateCacheAllowApp(IDbConnection conn = null, IDbTransaction trans = null)
        {
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            IEnumerable<APP> appList = repository.GetAllByIsNotDeleteAndEnabledMark(null, conn, trans);
            yuebonCacheHelper.Add("AllowAppId", appList);
        }
    }
}