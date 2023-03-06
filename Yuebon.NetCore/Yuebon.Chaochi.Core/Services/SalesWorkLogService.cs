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
using Yuebon.Commons.Helpers;
using System.Security.Claims;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Json;
using System.Linq;
using System.Text.Json;
using System.Data;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Security.IServices;
using Yuebon.Commons.Extensions;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 業務工作日誌服務接口實現
    /// </summary>
    public class SalesWorkLogService : BaseService<SalesWorkLog, SalesWorkLogOutputDto, string>, ISalesWorkLogService
    {
        private readonly ISalesWorkLogRepository _repository;
        private readonly IOrganizeService _organizeService;
        private readonly Security.IServices.IUserService _userService;

        public SalesWorkLogService(ISalesWorkLogRepository repository, IOrganizeService organizeService, Security.IServices.IUserService userService) : base(repository)
        {
            _repository = repository;
            _organizeService = organizeService;
            _userService = userService;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public async Task<PageResult<SalesWorkLogOutputDto>> FindWithPagerSearchAsync(SearchSalesWorkLogModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(true);
            var filter = search.Filter;

            if (!string.IsNullOrEmpty(search.logDateRange)) {
                var t = search.logDateRange.Replace(",", "' and '");
                where += $" and LogDate between '{t} 23:59:59 '";
            }

            if (!string.IsNullOrEmpty(search.AuditDateRange)) {
                var t = search.AuditDateRange.Replace(",", "' and '");
                where += $" and AuditTime between '{t} 23:59:59 '";
            }

            if (!string.IsNullOrEmpty(search.StoreManagerNote)) {
                where += $" and StoreManagerNote like '%{search.StoreManagerNote}%' ";
            }

            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            var list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            var resultList = list.MapTo<SalesWorkLogOutputDto>();
            var listResult = new List<SalesWorkLogOutputDto>();
            foreach (SalesWorkLogOutputDto item in resultList) {
                var deptName = _organizeService.Get(item.BelongDept)?.FullName;
                item.BelongDeptName = deptName;
                var user = _userService.Get(item.AuditSupervisor)?.RealName;
                item.AuditSupervisorName = user;
                listResult.Add(item);
            }
            PageResult<SalesWorkLogOutputDto> pageResult = new PageResult<SalesWorkLogOutputDto> {
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
                var roleType = JsonSerializer.Deserialize<String[]>(yuebonCacheHelper.Get("User_RoleType_" + claimlist[0].Value).ToJson());
                if (roleType != null && roleType.Length > 0) {
                    var roleTypeString = roleType.Join(",");

                    if (!roleTypeString.Contains("3")) {
                        // 角色類型：個人角色
                        if (roleTypeString.Contains("1")) {
                            where += $" and CreatorUserId like '%{claimlist[0].Value}%'";
                        }
                        // 角色類型：部門角色
                        if (roleTypeString.Contains("2")) {
                            var list = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + claimlist[0].Value).ToJson());

                            //var listArr = string.Join(",", list.ToArray());
                            //var userIdList = repository.GetUserPermissionUser(string.Format(" 1=1 and DepartmentId in ('{0}')", listArr.Replace(",","','")));
                            if (list.Count() > 0) {
                                string likeScript = "";
                                for (int i = 0; i < list.Count(); i++) {
                                    likeScript += $" CreatorUserDeptId like '%{list[i]}%' or";
                                }
                                likeScript = likeScript.Substring(0, likeScript.Length - 2);
                                //string DataFilterCondition = String.Join(",", list.ToArray());
                                if (!string.IsNullOrEmpty(likeScript)) {
                                    //where += string.Format(" and ({0} or CreatorUserId like '%{1}%')", likeScript, claimlist[0].Value);
                                    where += string.Format(" and ( {0} ) and  States <> 'first'", likeScript, claimlist[0].Value);
                                }
                            } else {
                                where += string.Format(" and CreatorUserId='{0}'", claimlist[0].Value);
                            }
                        }
                    }
                }
                //    if (!claimlist[2].Value.Contains("administrators")) {
                //        if (claimlist[2].Value == "sales,") {
                //            where += $" and CreatorUserId like '%{claimlist[0].Value}%'";
                //        } else {
                //            var list = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + claimlist[0].Value).ToJson());
                //            if (list.Count > 0) {
                //                string likeScript = "";
                //                for (int i = 0; i < list.Count; i++) {
                //                    likeScript += $" CreatorUserDeptId like '%{list[i]}%' or";
                //                }
                //                likeScript = likeScript.Substring(0, likeScript.Length - 2);
                //                if (!string.IsNullOrEmpty(likeScript)) {
                //                    where += string.Format(" and ( ( {0} ) and  States <> 'first'  or CreatorUserId like '%{1}%')", likeScript, claimlist[0].Value);
                //                }
                //            } else {
                //                where += string.Format(" and CreatorUserId='{0}'", claimlist[0].Value);
                //            }
                //        }
                //    }
            }
            return where;

        }
    }
}