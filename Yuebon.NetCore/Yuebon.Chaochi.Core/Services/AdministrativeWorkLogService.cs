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
using Yuebon.Commons.Helpers;
using System.Security.Claims;
using System.Linq;
using Yuebon.Commons.Cache;
using System.Text.Json;
using Yuebon.Commons.Json;
using Yuebon.Security.IServices;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 行政工作日誌服務接口實現
    /// </summary>
    public class AdministrativeWorkLogService : BaseService<AdministrativeWorkLog, AdministrativeWorkLogOutputDto, string>, IAdministrativeWorkLogService
    {
        private readonly IAdministrativeWorkLogRepository _repository;
        private readonly Security.IServices.IUserService _userService;
        private readonly IOrganizeService _organizeService;

        public AdministrativeWorkLogService(IAdministrativeWorkLogRepository repository, Security.IServices.IUserService userService, Security.IServices.IOrganizeService organizeService) : base(repository)
        {
            _repository = repository;
            _userService = userService;
            _organizeService = organizeService;
        }

        public override async Task<PageResult<AdministrativeWorkLogOutputDto>> FindWithPagerAsync(SearchInputDto<AdministrativeWorkLog> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege();
            var filter = search.Filter;
            if (filter is not null) {
                if (!string.IsNullOrEmpty(filter.BelongDept))
                    where += $" and BelongDept = '{filter.BelongDept}' ";
                if (!string.IsNullOrEmpty(filter.UserAccount)) {
                    where += $" and UserAccount = '{filter.UserAccount}' ";
                }
                if (!string.IsNullOrEmpty(filter.LogDate)) {
                    filter.LogDate = filter.LogDate.Replace(",", "' and '");
                    where += $" and LogDate between '{filter.LogDate} 23:59:59 '";
                }
            }
            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            var list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order, conn, trans);
            var resultList = list.MapTo<AdministrativeWorkLogOutputDto>();
            var listResult = new List<AdministrativeWorkLogOutputDto>();
            foreach (AdministrativeWorkLogOutputDto item in resultList) {
                var deptName = _organizeService.Get(item.BelongDept)?.FullName;
                item.BelongDeptName = deptName;
                var user = _userService.Get(item.AuditSupervisor)?.RealName;
                item.AuditSupervisorName = user;
                listResult.Add(item);
            }
            PageResult<AdministrativeWorkLogOutputDto> pageResult = new PageResult<AdministrativeWorkLogOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        protected override string GetDataPrivilege(bool blDeptCondition = true)
        {
            string where = "1=1";
            //開權限數據過濾
            if (blDeptCondition) {
                var identities = HttpContextHelper.HttpContext.User.Identities;
                var claimsIdentity = identities.First<ClaimsIdentity>();
                List<Claim> claimlist = claimsIdentity.Claims as List<Claim>;
                YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                if (!claimlist[2].Value.Contains("administrators")) {

                    var isManager = _organizeService.GetWhere($" ManagerId = '{claimlist[0].Value}' ");

                    if (isManager == null) {
                        where += $" and CreatorUserId like '%{claimlist[0].Value}%'";
                    } else {
                        var list = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + claimlist[0].Value).ToJson());
                        if (list.Count > 0) {
                            string likeScript = "";
                            for (int i = 0; i < list.Count; i++) {
                                likeScript += $" CreatorUserDeptId like '%{list[i]}%' or";
                            }
                            likeScript = likeScript.Substring(0, likeScript.Length - 2);
                            if (!string.IsNullOrEmpty(likeScript)) {
                                where += string.Format(" and ( ( {0} ) and  States <> 'first'  or CreatorUserId like '%{1}%')", likeScript, claimlist[0].Value);
                            }
                        } else {
                            where += string.Format(" and CreatorUserId='{0}'", claimlist[0].Value);
                        }
                    }
                    //if (claimlist[2].Value == "sales,") {
                    //    where += $" and CreatorUserId like '%{claimlist[0].Value}%'";
                    //} else {
                    //    var list = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + claimlist[0].Value).ToJson());
                    //    if (list.Count > 0) {
                    //        string likeScript = "";
                    //        for (int i = 0; i < list.Count; i++) {
                    //            likeScript += $" CreatorUserDeptId like '%{list[i]}%' or";
                    //        }
                    //        likeScript = likeScript.Substring(0, likeScript.Length - 2);
                    //        if (!string.IsNullOrEmpty(likeScript)) {
                    //            where += string.Format(" and ( ( {0} ) and  States <> 'first'  or CreatorUserId like '%{1}%')", likeScript, claimlist[0].Value);
                    //        }
                    //    } else {
                    //        where += string.Format(" and CreatorUserId='{0}'", claimlist[0].Value);
                    //    }
                    //}
                }
            }
            return where;

        }
    }
}