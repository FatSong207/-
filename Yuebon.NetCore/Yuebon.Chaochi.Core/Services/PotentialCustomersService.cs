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
using Yuebon.Commons.Helpers;
using System;
using System.Security.Claims;
using Yuebon.Commons.Cache;
using System.Text.Json;
using Yuebon.Commons.Json;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Extensions;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 潛在客管理服務接口實現
    /// </summary>
    public class PotentialCustomersService: BaseService<PotentialCustomers,PotentialCustomersOutputDto, string>, IPotentialCustomersService
    {
        private readonly Security.IServices.IUserService _userService;
        private readonly IPotentialCustomersRepository _repository;
        private readonly IVisitingRecordRepository _vrRepository;
        public PotentialCustomersService(IPotentialCustomersRepository repository, Security.IServices.IUserService userService, IVisitingRecordRepository vrRepository) : base(repository)
        {
            _repository = repository;
            _userService = userService;
            _vrRepository = vrRepository;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public override async Task<PageResult<PotentialCustomersOutputDto>> FindWithPagerAsync(SearchInputDto<PotentialCustomers> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(true);
            var filter = search.Filter;

            if (filter != null) {
                // 姓名
                if (!string.IsNullOrWhiteSpace(filter.Name))
                    where += string.Format(" and Name like '%{0}%'", filter.Name);

                // 手機號碼
                if (!string.IsNullOrWhiteSpace(filter.Cell))
                    where += string.Format(" and Cell like '%{0}%'", filter.Cell);

                // 身份
                if (!string.IsNullOrWhiteSpace(filter.Identity))
                    where += string.Format(" and [Identity] like '%{0}%'", filter.Identity);

                // 自然人或法人
                if (!string.IsNullOrWhiteSpace(filter.Identity2))
                    where += string.Format(" and Identity2 like '%{0}%'", filter.Identity2);

                // 客源
                if (!string.IsNullOrWhiteSpace(filter.Source)) {
                    where += string.Format(" and Source like '%{0}%'", filter.Source);
                }

                // 當前狀態
                if (!string.IsNullOrWhiteSpace(filter.Status))
                    where += string.Format(" and Status like '%{0}%'", filter.Status);

                // 回報次數
                if (!string.IsNullOrWhiteSpace(filter.ReportBackCounts))
                    where += string.Format(" and ReportBackCounts = '{0}'", filter.ReportBackCounts);

                // 產生日期
                if (!string.IsNullOrWhiteSpace(filter.CreateDate))
                    where += string.Format(" and CreateDate like '%{0}%'", filter.CreateDate);

                // 隸屬區/店
                if (!string.IsNullOrWhiteSpace(filter.Area))
                    where += string.Format(" and Area like '%{0}%'", filter.Area);
                //if (!string.IsNullOrWhiteSpace(filter.Area2))
                //    where += string.Format(" and Area2 like '%{0}%'", filter.Area2);

                // 指派業務
                if (!string.IsNullOrWhiteSpace(filter.Sales)) {
                    if(filter.Sales == "待指派") {
                        where += string.Format(" and Sales = ''", filter.Sales);
                    } else {
                        where += string.Format(" and Sales like '%{0}%'", filter.Sales);
                    }
                }

                // 是否結案
                if (!string.IsNullOrWhiteSpace(filter.IsClosed))
                    where += string.Format(" and IsClosed = '{0}'", filter.IsClosed);

                // 是否轉正
                if (!string.IsNullOrWhiteSpace(filter.IsTransfer))
                    where += string.Format(" and IsTransfer = '{0}'", filter.IsTransfer);

            }

            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<PotentialCustomers> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);

            List<PotentialCustomersOutputDto> resultList = new List<PotentialCustomersOutputDto>();
            if(list != null && list.Count > 0) {
                resultList = list.MapTo<PotentialCustomersOutputDto>();
                foreach (PotentialCustomersOutputDto item in resultList) {
                    if(!string.IsNullOrEmpty(item.Sales)) {
                        var sales = _userService.GetByUserName(item.Sales).Result;
                        item.SalesName = (sales != null) ? sales.RealName : $"{item.Sales}(帳號己不存在)";
                    }                    
                }
            }

            PageResult<PotentialCustomersOutputDto> pageResult = new PageResult<PotentialCustomersOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = resultList,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };

            return pageResult;
        }

        /// <summary>
        /// 根據潛在客編號查詢潛在客資料
        /// </summary>
        /// <param name="pid">潛在客編號</param>
        /// <returns></returns>
        public async Task<PotentialCustomersOutputDto> GetByPID(string pid)
        {
            string where = string.Format("PID = '{0}'", pid);
            PotentialCustomers pcs = await _repository.GetWhereAsync(where);
            PotentialCustomersOutputDto pcsOutputDto = pcs.MapTo<PotentialCustomersOutputDto>();

            // 訪談記錄
            List<VisitingRecordOutputDto> vrODtoList = new List<VisitingRecordOutputDto>();
            IEnumerable<VisitingRecord> vrList = await _vrRepository.GetListWhereAsync(where);
            if (vrList.Count() > 0) {
                foreach (VisitingRecord visitingRecord in vrList) {
                    VisitingRecordOutputDto vrOutputDto = visitingRecord.MapTo<VisitingRecordOutputDto>();
                    vrODtoList.Add(vrOutputDto);
                }
            }

            if (vrODtoList != null && vrODtoList.Count > 0) {
                pcsOutputDto.VisitingRecordList = vrODtoList.OrderBy(m => m.CreatorTime).ToList<VisitingRecordOutputDto>();
            }

            return pcsOutputDto;
        }        

        /// <summary>
        /// 更新潛在客管理的當前狀態
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="pid">潛在客編號</param>
        /// <param name="status">當前狀態</param>
        /// <returns></returns> 
        public async Task<bool> UpdatePCStatus(string userId, string pid, string status, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;
            PotentialCustomers info = await _repository.GetWhereAsync($"PID = '{pid}'", conn, trans);

            if (info != null) {
                info.Status = status;
                info.LastModifyTime = DateTime.Now;
                info.LastModifyUserId = userId;

                result = await repository.UpdateAsync(info, info.Id, conn, trans);
            }

            return result;
        }

        /// <summary>
        /// 更新潛在客管理的結案狀態
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="pid">潛在客編號</param>
        /// <param name="isClosed">是否結案</param>
        /// <returns></returns> 
        public async Task<bool> UpdatePCIsClosed(string userId, string pid, string isClosed, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;
            PotentialCustomers info = _repository.GetWhere($"PID = '{pid}'", conn, trans);

            if (info != null) {
                info.IsClosed = isClosed;
                info.LastModifyTime = DateTime.Now;
                info.LastModifyUserId = userId;

                result = await repository.UpdateAsync(info, info.Id, conn, trans);
            }

            return result;
        }

        /// <summary>
        /// 更新潛在客管理的回報次數
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="pid">潛在客編號</param>
        /// <param name="reportBackCounts">回報次數</param>
        /// <returns></returns> 
        public async Task<bool> UpdatePCReportBackCounts(string userId, string pid, int reportBackCounts, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;
            PotentialCustomers info = await _repository.GetWhereAsync($"PID = '{pid}'", conn, trans);

            if (info != null) {
                info.ReportBackCounts = reportBackCounts.ToString();
                info.LastModifyTime = DateTime.Now;
                info.LastModifyUserId = userId;

                result = await repository.UpdateAsync(info, info.Id, conn, trans);
            }

            return result;
        }

        /// <summary>
        /// 更新潛在客管理的結案狀態
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="pid">潛在客編號</param>
        /// <param name="identity">身份別(出租人/承租人)</param>
        /// <param name="name">姓名</param>
        /// <param name="addressArray">地址陣列</param>
        /// <returns></returns> 
        public async Task<bool> UpdateTransferClientFields(string userId, string pid, string identity, string name, string[] addressArray, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;
            PotentialCustomers info = await _repository.GetWhereAsync($"PID = '{pid}'", conn, trans);

            if (info != null) {
                if (!string.IsNullOrEmpty(name)) {
                    info.Name = name;
                }                

                switch (identity) {
                    case "出租人":
                        if (addressArray.Length > 0 && addressArray.Length == 17) {
                            string wholeAddress = Utils.memergeAddFix(
                                addressArray[0],
                                addressArray[1],
                                addressArray[2],
                                addressArray[3],
                                addressArray[4],
                                addressArray[5],
                                addressArray[6],
                                addressArray[7],
                                addressArray[8],
                                addressArray[9],
                                addressArray[10],
                                addressArray[11],
                                addressArray[12],
                                addressArray[13],
                                addressArray[14],
                                addressArray[15],
                                addressArray[16]
                            );
                            info.BAdd = wholeAddress;
                            info.BAdd_1 = addressArray[0];
                            info.BAdd_1_1 = addressArray[1];
                            info.BAdd_1_2 = addressArray[2];
                            info.BAdd_2 = addressArray[3];
                            info.BAdd_2_1 = addressArray[4];
                            info.BAdd_2_2 = addressArray[5];
                            info.BAdd_2_3 = addressArray[6];
                            info.BAdd_2_4 = addressArray[7];
                            info.BAdd_3 = addressArray[8];
                            info.BAdd_3_1 = addressArray[9];
                            info.BAdd_3_2 = addressArray[10];
                            info.BAdd_4 = addressArray[11];
                            info.BAdd_5 = addressArray[12];
                            info.BAdd_6 = addressArray[13];
                            info.BAdd_7 = addressArray[14];
                            info.BAdd_8 = addressArray[15];
                            info.BAdd_9 = addressArray[16];                          
                        }                            
                        break;
                    case "承租人":
                        if (addressArray.Length > 0 && addressArray.Length == 17) {
                            string wholeAddress = Utils.memergeAddFix(
                                addressArray[0],
                                addressArray[1],
                                addressArray[2],
                                addressArray[3],
                                addressArray[4],
                                addressArray[5],
                                addressArray[6],
                                addressArray[7],
                                addressArray[8],
                                addressArray[9],
                                addressArray[10],
                                addressArray[11],
                                addressArray[12],
                                addressArray[13],
                                addressArray[14],
                                addressArray[15],
                                addressArray[16]
                            );
                            info.RAdd = wholeAddress;
                            info.RAdd_1 = addressArray[0];
                            info.RAdd_1_1 = addressArray[1];
                            info.RAdd_1_2 = addressArray[2];
                            info.RAdd_2 = addressArray[3];
                            info.RAdd_2_1 = addressArray[4];
                            info.RAdd_2_2 = addressArray[5];
                            info.RAdd_2_3 = addressArray[6];
                            info.RAdd_2_4 = addressArray[7];
                            info.RAdd_3 = addressArray[8];
                            info.RAdd_3_1 = addressArray[9];
                            info.RAdd_3_2 = addressArray[10];
                            info.RAdd_4 = addressArray[11];
                            info.RAdd_5 = addressArray[12];
                            info.RAdd_6 = addressArray[13];
                            info.RAdd_7 = addressArray[14];
                            info.RAdd_8 = addressArray[15];
                            info.RAdd_9 = addressArray[16];
                        }
                        break;
                }

                info.IsClosed = "是";
                info.IsTransfer = "是";
                info.LastModifyTime = DateTime.Now;
                info.LastModifyUserId = userId;

                result = await repository.UpdateAsync(info, info.Id, conn, trans);
            }

            return result;
        }

        /// <summary>
        /// 指派業務
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="pid">潛在客編號</param>
        /// <param name="sales">指派業務</param>
        /// <returns></returns> 
        public async Task<bool> AssignSales(string userId, string pid, string sales, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;
            PotentialCustomers info = await _repository.GetWhereAsync($"PID = '{pid}'", conn, trans);

            if (info != null) {
                info.Sales = sales;
                info.LastModifyUserId = userId;
                info.LastModifyTime = DateTime.Now;

                result = await repository.UpdateAsync(info, info.Id, conn, trans);
            }

            return result;
        }

        /// <summary>
        /// 轉店時要檢查此店是否己存在
        /// 
        /// </summary>
        /// <param name="store">店名</param>
        /// <param name="cell">手機號碼</param>
        /// <returns></returns> 
        public async Task<bool> IsStoreRecordExist(string store, string cell, IDbConnection conn = null, IDbTransaction trans = null)
        {
            PotentialCustomers info = await _repository.GetWhereAsync($"Area_2 = '{store}' AND Cell = '{cell}'", conn, trans);

            return info != null;
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
                            where += $" and CreatorUserId like '%{claimlist[0].Value}%' or Sales like '%{claimlist[1].Value}%'";
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
                                    //where += string.Format(" or {0}", likeScript, claimlist[0].Value);
                                    where += string.Format(" and ({0})", likeScript);
                                }
                            } else {
                                where += string.Format(" and CreatorUserId='{0}'", claimlist[0].Value);
                            }
                        }
                    }
                }

                //if (!claimlist[2].Value.Contains("administrators")) {
                //    if (claimlist[2].Value == "sales,") {
                //        where += $" and (CreatorUserId like '%{claimlist[0].Value}%' or Sales like '%{claimlist[1].Value}%')";
                //    } else {
                //        var list = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + claimlist[0].Value).ToJson());

                //        //var listArr = string.Join(",", list.ToArray());
                //        //var userIdList = repository.GetUserPermissionUser(string.Format(" 1=1 and DepartmentId in ('{0}')", listArr.Replace(",","','")));
                //        if (list.Count() > 0) {
                //            string likeScript = "";
                //            for (int i = 0; i < list.Count(); i++) {
                //                likeScript += $" CreatorUserDeptId like '%{list[i]}%' or";
                //            }
                //            likeScript = likeScript.Substring(0, likeScript.Length - 2);
                //            string DataFilterCondition = String.Join(",", list.ToArray());
                //            if (!string.IsNullOrEmpty(likeScript)) {
                //                where += string.Format(" and ({0} or CreatorUserId like '%{1}%')", likeScript, claimlist[0].Value);
                //            }
                //        } else {
                //            where += string.Format(" and CreatorUserId='{0}'", claimlist[0].Value);
                //        }
                //    }
                //}
            }
            return where;
        }
    }
}