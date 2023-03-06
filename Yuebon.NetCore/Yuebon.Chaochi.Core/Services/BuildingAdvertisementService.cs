using System.Threading.Tasks;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Data;
using Yuebon.Commons.Pages;
using System.Collections.Generic;
using System;
using System.Security.Claims;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Helpers;
using System.Linq;
using System.Text.Json;
using Yuebon.Commons.Json;
using Yuebon.Commons.Extensions;
using NPOI.OpenXmlFormats;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 建物廣告管理服務接口實現
    /// </summary>
    public class BuildingAdvertisementService: BaseService<BuildingAdvertisement,BuildingAdvertisementOutputDto, string>, IBuildingAdvertisementService
    {
		private readonly IBuildingAdvertisementRepository _repository;
        private readonly Security.IServices.IUserService _userService;
        private readonly Security.IServices.IOrganizeService _orgService;
        public BuildingAdvertisementService(IBuildingAdvertisementRepository repository, Security.IServices.IUserService userService, Security.IServices.IOrganizeService orgService) : base(repository)
        {
			_repository=repository;
            _userService = userService;
            _orgService = orgService;
        }

        /// <summary>
        /// 根據建物地址查詢建物廣告資訊
        /// </summary>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        public Task<BuildingAdvertisement> GetByBAdd(string BAdd)
        {
            return _repository.GetByBAdd(BAdd);
        }

        /// <summary>
        /// 分頁查詢包含建物資訊
        /// </summary>
        /// <param name="condition">查詢條件字段需要加表別名</param>
        /// <param name="info">分頁信息</param>
        /// <param name="fieldToSort">排序字段，也需要加表別名</param>
        /// <param name="desc">排序方式</param>
        /// <param name="trans">事務</param>
        /// <returns></returns>
        public async Task<PageResult<BuildingAdvertisementOutputDto>> FindWithPagerAsync(SearchBuildingAdvertisementModel search, IDbConnection conn = null, IDbTransaction trans = null) {
            bool order = search.Order == "asc" ? false : true;
            var filter = search.Filter;

            var identities = HttpContextHelper.HttpContext.User.Identities;
            var claimsIdentity = identities.First<ClaimsIdentity>();
            List<Claim> claimlist = claimsIdentity.Claims as List<Claim>;

            string where = GetBuildingAdvertisementPrivilege(claimlist);

            // 建物類型
            if (!string.IsNullOrWhiteSpace(search.BPropoty)) {
                where += string.Format(" and t2.BPropoty = '{0}'", search.BPropoty);
            }

            // 建物狀態
            //if (!string.IsNullOrWhiteSpace(search.BState) && !claimlist[2].Value.Contains("sales")) {
            
            if (!string.IsNullOrWhiteSpace(search.BState)) {
                where += string.Format(" and t2.BState = '{0}'", search.BState);
            } else {
                if (claimlist[2].Value.Contains("sales")) {
                    where += $" and (t2.BState = '招租中' or t2.BState = '已收訂')";
                }
            }

            // 租金           
            if (search.BTRentMax > 0) {                
                where += string.Format(" and t2.BTRent <= {0}", search.BTRentMax);
            } else if(search.BTRentMin > 0) {                
                where += string.Format(" and t2.BTRent >= {0}", search.BTRentMin);
            } else if(search.BTRentMax > 0 && search.BTRentMin > 0) {
                where += string.Format(" and t2.BTRent between {0} and {1}", search.BTRentMin, search.BTRentMax);
            }

            // 坪數
            string realping = string.Empty;
            if (search.BRealPINGMax > 0) {
                realping = String.Format("{0:F4}", search.BRealPINGMax);
                where += string.Format(" and t2.BRealPING <= {0}", realping);
            } else if (search.BRealPINGMin > 0) {
                realping = String.Format("{0:F4}", search.BTRentMin);
                where += string.Format(" and t2.BRealPING >= {0}", realping);
            } else if (search.BRealPINGMax > 0 && search.BRealPINGMin > 0) {
                realping = String.Format("{0:F4}", search.BTRentMin);
                string realping2 = String.Format("{0:F4}", search.BTRentMax);
                where += string.Format(" and t2.BRealPING between {0} and {1}", realping, realping2);
            }

            // 格局
            if (!string.IsNullOrWhiteSpace(search.BPatten)) {
                switch(search.BPatten) {
                    case "套房":
                        where += " and t2.BPatten1S = '/Yes'";
                        break;
                    case "一房":
                        where += " and t2.BPatten1R = '/Yes'";
                        break;
                    case "二房":
                        where += " and t2.BPatten2R = '/Yes'";
                        break;
                    case "三房":
                        where += " and t2.BPatten3Rup = '/Yes'";
                        break;
                    case "四房以上":
                        where += " and t2.BPatten4Rup = '/Yes'";
                        break;
                }
            }

            // 電梯
            if (!string.IsNullOrWhiteSpace(search.BElevator)) {
                switch (search.BElevator) {
                    case "有":
                        where += " and t2.Belevator_Y = '/Yes'";
                        break;
                    case "無":
                        where += " and t2.Belevator_N = '/Yes'";
                        break;
                }
            }

            if (filter != null) {
                // 建物廣告狀態
                if (!string.IsNullOrWhiteSpace(filter.BAStatus))
                    where += string.Format(" and t1.BAStatus = '{0}'", filter.BAStatus);

                // 已廣告天數大於
                if (!string.IsNullOrWhiteSpace(filter.BADays)) {
                    where += string.Format(" and t1.BADays > {0}", filter.BADays);
                }

                // 已出租天數大於
                if (!string.IsNullOrWhiteSpace(filter.BRDays)) {
                    where += string.Format(" and t1.BRDays > {0}", filter.BRDays);
                }

                // 建物區域
                if (!string.IsNullOrWhiteSpace(filter.BAdd)) {
                    where += string.Format(" and t1.BAdd like '%{0}%'", filter.BAdd);
                }

                // 歸屬店
                if (!string.IsNullOrWhiteSpace(filter.CreatorUserDeptId)) {
                    var organizes = await _orgService.GetListWhereAsync($"FullName like '%{filter.CreatorUserDeptId}%'");
                    if(organizes != null && organizes.Count() > 0) {
                        where += string.Format(" and t1.CreatorUserDeptId in ('{0}')", organizes.ToList().Select(item => item.Id).Join(",").Trim(',').Replace(",", "','"));
                        //where += string.Format(" and t1.CreatorUserDeptId in ('{0}')", childDeptIdList.ToArray().Join(",").Trim(',').Replace(",", "','"));
                    }
                    //var childDeptIdList = await _orgService.GetChildOrganizeIdList(filter.CreatorUserDeptId);
                    //childDeptIdList.Add(filter.CreatorUserDeptId);
                    
                    //where += string.Format(" and t1.CreatorUserDeptId in ('{0}')", filter.CreatorUserDeptId);
                }
            }

            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };

            var list = await _repository.FindWithPagerRelationBuildingAsync(where, pagerInfo, search.Sort, order);

            List<BuildingAdvertisementOutputDto> resultList = new List<BuildingAdvertisementOutputDto>();
            if (list != null && list.Count > 0) {
                BuildingAdvertisementOutputDto outputDto = null;
                foreach (var item in list) {
                    var d = item as IDictionary<string, object>;
                    outputDto = new BuildingAdvertisementOutputDto();
                    outputDto.Id = $@"{d["Id"]}";
                    outputDto.BPropoty = $@"{d["BPropoty"]}";
                    outputDto.BState = $@"{d["BState"]}";
                    outputDto.BAStatus = $@"{d["BAStatus"]}";
                    outputDto.BAdd = $@"{d["BAdd"]}";
                    string bInfo = string.Empty;
                    if (d["BRoom"] != null) {
                        bInfo += d["BRoom"].ToString();
                    }
                    if (d["BLD"] != null) {
                        bInfo += $@"/{d["BLD"]}";
                    }
                    if (d["BBath"] != null) {
                        bInfo += $@"/{d["BBath"]}";
                    }
                    outputDto.BInfo = bInfo;
                    outputDto.BRealPING = $@"{d["BRealPING"]}";
                    outputDto.BTRent = $@"{d["BTRent"]}";
                    outputDto.CreatorUserDeptName = (d["dept"] is not null && !string.IsNullOrEmpty(d["dept"].ToString())) ? _orgService.Get(d["dept"].ToString()).ShortName : string.Empty;
                    outputDto.Sales = (d["sales"] is not null && !string.IsNullOrEmpty(d["sales"].ToString())) ? _userService.Get(d["sales"].ToString()).RealName : "";
                    outputDto.BADays = $@"{d["BADays"]}";
                    outputDto.BRDays = $@"{d["BRDays"]}";
                    outputDto.BAURL = $@"{d["BAURL"]}";
                    resultList.Add(outputDto);
                }
            }

            PageResult<BuildingAdvertisementOutputDto> pageResult = new PageResult<BuildingAdvertisementOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = resultList,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };

            return pageResult;
        }

        /// <summary>
        /// 新增建物廣告
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        public async Task<bool> InsertAsync(BuildingAdvertisement entity, IDbTransaction trans = null)
        {
            return await _repository.InsertAsync(entity, trans);
        }

        /// <summary>
        /// 更新建物廣告狀態
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="Id">主鍵值</param>
        /// <param name="status">建物廣告狀態</param>
        /// <returns></returns> 
        public async Task<bool> UpdateBAStatus(string userId, string Id, string status, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;

            BuildingAdvertisement info = repository.Get(Id, conn, trans);

            if (info != null) {
                info.BAStatus = status;
                info.LastModifyUserId = userId;
                info.LastModifyTime = DateTime.Now;

                result = await repository.UpdateAsync(info, Id, conn, trans);
            }

            return result;
        }

        /// <summary>
        /// 更新建物廣告網址
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="Id">主鍵值</param>
        /// <param name="URL">建物廣告址</param>
        /// <returns></returns> 
        public async Task<bool> UpdateBAURL(string userId, string Id, string URL, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;

            BuildingAdvertisement info = repository.Get(Id, conn, trans);
            if (info is not null) {
                info.BAURL = URL;
                info.BAStatus = "廣告已上架";
                info.BAStartDate = DateTime.Today;
                info.LastModifyUserId = userId;
                info.LastModifyTime = DateTime.Now;

                result = await repository.UpdateAsync(info, Id, conn, trans);
            }

            return result;
        }

        /// <summary>
        /// 更新建物廣告出租天數
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="Id">主鍵值</param>
        /// <param name="brDays">廣告出租天數</param>
        /// <returns></returns> 
        public bool UpdateBRDays(string userId, string Id, string brDays, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;

            BuildingAdvertisement info = repository.Get(Id, conn, trans);
            if (info is not null) {
                info.BRDays = brDays;
                info.LastModifyUserId = userId;
                info.LastModifyTime = DateTime.Now;

                result = repository.Update(info, Id, trans);
            }

            return result;
        }

        /// <summary>
        /// 更新建物廣告天數
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="Id">主鍵值</param>
        /// <param name="baDays">廣告天數</param>
        /// <returns></returns> 
        public bool UpdateBADays(string userId, string Id, string baDays, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;

            BuildingAdvertisement info = repository.Get(Id, conn, trans);
            if (info is not null) {
                info.BADays = baDays;
                info.LastModifyUserId = userId;
                info.LastModifyTime = DateTime.Now;

                result = repository.Update(info, Id, trans);
            }

            return result;
        }

        /// <summary>
        /// 更新建物廣告出租天數和廣告天數
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="Id">主鍵值</param>
        /// <param name="brDays">廣告出租天數</param>
        /// <param name="baDays">廣告天數</param>
        /// <returns></returns> 
        public bool UpdateBDays(string userId, string Id, string brDays, string baDays, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;

            BuildingAdvertisement info = repository.Get(Id, conn, trans);
            if (info is not null) {                
                info.BRDays = brDays;
                info.BADays = baDays;
                info.LastModifyUserId = userId;
                info.LastModifyTime = DateTime.Now;

                result = repository.Update(info, Id, trans);
            }

            return result;
        }

        /// <summary>
        /// 更新建物廣告開始刊登日期
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="Id">主鍵值</param>
        /// <param name="brStartDate">廣告天數</param>
        /// <returns></returns> 
        public async Task<bool> UpdateBRStartDate(string userId, string Id, DateTime brStartDate, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;

            BuildingAdvertisement info = repository.Get(Id, conn, trans);
            if (info is not null) {
                info.BRStartDate = brStartDate;
                info.LastModifyUserId = userId;
                info.LastModifyTime = DateTime.Now;

                result = await repository.UpdateAsync(info, Id, conn, trans);
            }

            return result;
        }

        /// <summary>
        /// 獲取建物廣告管理的數據訪問權限
        /// </summary>
        /// <returns></returns>
        private string GetBuildingAdvertisementPrivilege(List<Claim> claimlist)
        {
            string where = "1=1";
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();

            if (!claimlist[2].Value.Contains("administrators")) {
                // 1.所有業務僅能查詢「招租中」的建物
                //if (claimlist[2].Value.Contains("sales")) {
                //    where += $" and (t2.BState = '招租中' or t2.BState = '已收訂')";
                //    // 2.所有業助僅能查詢自己所屬店的所有建物
                //    //} else if (claimlist[2].Value.Contains("Business assistance") || claimlist[2].Value.Contains("storemanager")) {
                //} else if (claimlist[2].Value.Contains("Business assistance")) {
                if (claimlist[2].Value.Contains("Business assistance")) {
                    var list = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + claimlist[0].Value).ToJson());

                    //var listArr = string.Join(",", list.ToArray());
                    //var userIdList = repository.GetUserPermissionUser(string.Format(" 1=1 and DepartmentId in ('{0}')", listArr.Replace(",","','")));
                    if (list.Count() > 0) {
                        string likeScript = "";
                        for (int i = 0; i < list.Count(); i++) {
                            likeScript += $" t1.CreatorUserDeptId like '%{list[i]}%' or";
                        }
                        likeScript = likeScript.Substring(0, likeScript.Length - 2);
                        //string DataFilterCondition = String.Join(",", list.ToArray());
                        if (!string.IsNullOrEmpty(likeScript)) {
                            where += string.Format(" and ({0} or t1.CreatorUserId like '%{1}%')", likeScript, claimlist[0].Value);
                        }
                    } else {
                        where += string.Format(" and t1.CreatorUserId='{0}'", claimlist[0].Value);
                    }
                }
            }

            return where;
        }
    }
}