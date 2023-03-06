using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Linq;
using System.Data;
using System.Security.Claims;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Helpers;
using System.Text.Json;
using Yuebon.Commons.Json;
using Yuebon.Commons.Extensions;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class MOService: BaseService<MO,MOOutputDto, string>, IMOService
    {
		private readonly IMORepository _repository;
        private readonly IMOBuildingRepository _MOBuildingRepository;
        private readonly Security.IServices.IUserService _userService;
        public MOService(IMORepository repository, IMOBuildingRepository MOBuildingRepository, Security.IServices.IUserService userService) : base(repository)
        {
			_repository=repository;
            _MOBuildingRepository = MOBuildingRepository;
            _userService = userService;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        //public async Task<PageResult<MOOutputDto>> FindWithPagerSearchAsync(SearchMOModel search)
        public async Task<PageResult<MOOutputDto>> FindWithPagerSearchAsync(SearchMOModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(true);
            var filter = search.Filter;
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<string> moidList = new List<string>();


            if (filter != null) {
                // 多物件單號
                if (!string.IsNullOrWhiteSpace(filter.MOID))
                    where += string.Format(" and MOID like '%{0}%'", filter.MOID);
                
                // 多物件名稱
                if (!string.IsNullOrWhiteSpace(filter.MOName))
                    where += string.Format(" and MOName like '%{0}%'", filter.MOName);

                // 多物件單狀態
                if (!string.IsNullOrWhiteSpace(filter.Status))
                    where += string.Format(" and Status like '%{0}%'", filter.Status);

                // 建物門牌地址
                if (!string.IsNullOrWhiteSpace(search.BAdd)) {
                    string buildingWhere = (!string.IsNullOrWhiteSpace(search.BPropoty)) ? string.Format(" BAdd like '%{0}%' and BPropoty like '%{1}%'", search.BAdd, search.BPropoty) : string.Format(" BAdd like '%{0}%'", search.BAdd);
                    IEnumerable<MOBuilding> buildingList = await _MOBuildingRepository.GetListWhereAsync(buildingWhere);
                    if (buildingList.Count() > 0) {
                        moidList.AddRange(buildingList.Select(x => x.MOID).ToList());
                        //where += string.Format(" and MOID in '({0})'", string.Join(",", buildingList.Select(x => x.MOID).ToArray));
                    }
                }

                // 建物屬性
                if (!string.IsNullOrWhiteSpace(search.BPropoty)) {
                    string buildingWhere = !string.IsNullOrWhiteSpace(search.BAdd) ? string.Format(" BAdd like '%{0}%' and BPropoty like '%{1}%'", search.BAdd, search.BPropoty) : string.Format(" BPropoty like '%{0}%'", search.BPropoty);
                    IEnumerable<MOBuilding> buildingList = await _MOBuildingRepository.GetListWhereAsync(buildingWhere);
                    if (buildingList.Count() > 0) {
                        moidList.AddRange(buildingList.Select(x => x.MOID).ToList());
                        //where += string.Format(" and MOID in '({0})'", string.Join(",", buildingList.Select(x => x.MOID).ToArray()));
                    }
                }

                if (moidList != null && moidList.Count > 0) {
                    where += string.Format(" MOID in ('{0}')", string.Join(",", moidList.Distinct().ToArray()).Trim(',').Replace(",", "','"));
                }
            }

            var list = await repository.FindWithPagerRelationUserAsync(where, pagerInfo, search.Sort, order);
            List<MOOutputDto> moList = new List<MOOutputDto>();
            if (list != null && list.Count > 0) {
                MOOutputDto moOutputDto = null;
                foreach (var item in list) {
                    var d = item as IDictionary<string, object>;
                    moOutputDto = new MOOutputDto();
                    moOutputDto.Id = d["Id"].ToString();
                    moOutputDto.MOID = d["MOID"].ToString();
                    moOutputDto.MOName = d["MOName"].ToString();
                    moOutputDto.LSName = d["LSName"].ToString();
                    moOutputDto.SLName = d["SLName"].ToString();
                    moOutputDto.RealName = d["RealName"].ToString();
                    moOutputDto.Status = d["Status"].ToString();

                    moList.Add(moOutputDto);
                }
            }

            PageResult<MOOutputDto> pageResult = new PageResult<MOOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = moList,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        /// <summary>
        /// 根據多物件單號查詢多物件資料
        /// </summary>
        /// <param name="moid">多物件單號</param>
        /// <returns></returns>
        public async Task<MOOutputDto> GetByMOID(string moid)
        {
            string where = string.Format("MOID = '{0}'", moid);
            MO mo = await _repository.GetWhereAsync(where);
            MOOutputDto moOutputDto = mo.MapTo<MOOutputDto>();

            List<MOBuildingOutputDto> buildingODtoList = new List<MOBuildingOutputDto>();
            IEnumerable<MOBuilding> buildingList = await _MOBuildingRepository.GetListWhereAsync(where);
            if (buildingList.Count() > 0) {
                foreach (MOBuilding building in buildingList) {
                    MOBuildingOutputDto mobOutputDto = building.MapTo<MOBuildingOutputDto>();
                    buildingODtoList.Add(mobOutputDto);
                }
            }

            if (buildingODtoList != null && buildingODtoList.Count > 0) {
                moOutputDto.BuildingList = buildingODtoList.OrderBy(m => m.CreatorTime).ToList<MOBuildingOutputDto>();
            }

            return moOutputDto;
        }

        /// <summary>
        /// 更新多物件單狀態
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="moid">多物件單號</param>
        /// <param name="status">多物件單狀態</param>
        /// <returns></returns> 
        public async Task<bool> UpdateMOStatus(string userId, string moid, string status, IDbConnection conn = null, IDbTransaction trans = null)
        {
            DateTime time = DateTime.Now;
            bool result = false;
            MO info = await _repository.GetWhereAsync($"MOID = '{moid}'", conn , trans);

            if (info != null) {
                info.Status = status;
                info.LastModifyTime = DateTime.Now;
                info.LastModifyUserId = userId;

                result = await repository.UpdateAsync(info, info.Id, conn, trans);
            }

            return result;
        }

        /// <summary>
        /// 獲取當前登錄用戶的數據訪問權限
        /// </summary>
        /// <param name="blDeptCondition">是否開啟，默認開啟</param>
        /// <returns></returns>
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
                            where += $" and t1.CreatorUserId like '%{claimlist[0].Value}%'";
                        }
                        // 角色類型：部門角色
                        if (roleTypeString.Contains("2")) {
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
                                    //where += string.Format(" and ({0} or CreatorUserId like '%{1}%')", likeScript, claimlist[0].Value);
                                    where += string.Format(" and ({0})", likeScript);
                                }
                            } else {
                                where += string.Format(" and t1.CreatorUserId='{0}'", claimlist[0].Value);
                            }
                        }
                    }
                }

                //if (!claimlist[2].Value.Contains("administrators")) {
                //    if (claimlist[2].Value == "sales,") {
                //        where += $" and t1.CreatorUserId like '%{claimlist[0].Value}%'";
                //    } else {
                //        var list = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + claimlist[0].Value).ToJson());

                //        //var listArr = string.Join(",", list.ToArray());
                //        //var userIdList = repository.GetUserPermissionUser(string.Format(" 1=1 and DepartmentId in ('{0}')", listArr.Replace(",","','")));
                //        if (list.Count() > 0) {
                //            string likeScript = "";
                //            for (int i = 0; i < list.Count(); i++) {
                //                likeScript += $" t1.CreatorUserDeptId like '%{list[i]}%' or";
                //            }
                //            likeScript = likeScript.Substring(0, likeScript.Length - 2);
                //            //string DataFilterCondition = String.Join(",", list.ToArray());
                //            if (!string.IsNullOrEmpty(likeScript)) {
                //                where += string.Format(" and ({0} or t1.CreatorUserId like '%{1}%')", likeScript, claimlist[0].Value);
                //            }
                //        } else {
                //            where += string.Format(" and t1.CreatorUserId='{0}'", claimlist[0].Value);
                //        }
                //    }
                //}
            }
            return where;
        }
    }
}