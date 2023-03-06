using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;
using System.Data;
using Yuebon.Commons.DependencyInjection;
using System.Security.Claims;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Helpers;
using System.Linq;
using System.Text.Json;
using Yuebon.Commons.Json;
using System.Text;
using Yuebon.Email;
using Yuebon.Commons.Extensions;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class ContractService: BaseService<Contract, ContractOutputDto, string>, IContractService, IScopedDependency
    {
		private IContractRepository _repository;
        private IContractLandlordService _contractLandlordService;
        private IContractRenterService _contractRenterService;
        private IContractBuildingService _contractBuildingService;
        private IContractRelevantService _contractRelevantService;
        private IContractAttachmentService _contractAttachmentService;
        private IHistoryFormContractService _contractHistoryService;
        private IContractFlowLogService _contractFlowLogService;
        private ITBNoC1Service _tbNoC1Service;
        private ITBNoC1_2Service _tbNoC1_2Service;
        private ITBNoC1_3Service _tbNoC1_3Service;
        private ITBNoC2Service _tbNoC2Service;
        private ITBNoC2_2Service _tbNoC2_2Service;
        private ITBNoC2_3Service _tbNoC2_3Service;
        private ITBNoC3Service _tbNoC3Service;
        private ITBNoC3_2Service _tbNoC3_2Service;
        private ITBNoC3_3Service _tbNoC3_3Service;
        private Security.IServices.IUserService _userService;

        public ContractService(IContractRepository repository,
            IContractLandlordService contractLandlordService,
            IContractRenterService contractRenterService,
            IContractBuildingService contractBuildingService,
            IContractRelevantService contractRelevantService,
            IContractAttachmentService contractAttachmentService,
            IHistoryFormContractService contractHistoryService,
            IContractFlowLogService contractFlowLogService,
            ITBNoC1Service tbNoC1Service,
            ITBNoC1_2Service tbNoC1_2Service,
            ITBNoC1_3Service tbNoC1_3Service,
            ITBNoC2Service tbNoC2Service,
            ITBNoC2_2Service tbNoC2_2Service,
            ITBNoC2_3Service tbNoC2_3Service,
            ITBNoC3Service tbNoC3Service,
            ITBNoC3_2Service tbNoC3_2Service,
            ITBNoC3_3Service tbNoC3_3Service,
        Security.IServices.IUserService userService) : base(repository)
        {
			_repository = repository;
            _contractLandlordService = contractLandlordService;
            _contractRenterService = contractRenterService;
            _contractBuildingService = contractBuildingService;
            _contractRelevantService = contractRelevantService;
            _contractAttachmentService = contractAttachmentService;
            _contractHistoryService = contractHistoryService;
            _contractFlowLogService = contractFlowLogService;
            _tbNoC1Service = tbNoC1Service;
            _tbNoC1_2Service = tbNoC1_2Service;
            _tbNoC1_3Service = tbNoC1_3Service;
            _tbNoC2Service = tbNoC2Service;
            _tbNoC2_2Service = tbNoC2_2Service;
            _tbNoC2_3Service = tbNoC2_3Service;
            _tbNoC3Service = tbNoC3Service;
            _tbNoC3_2Service = tbNoC3_2Service;
            _tbNoC3_3Service = tbNoC3_3Service;
            _userService = userService;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public new async Task<PageResult<ContractOutputDto>> FindWithPagerAsync(SearchInputDto<Contract> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(true);
            var filter = search.Filter;

            //// 出租自然人/法人
            //if (!string.IsNullOrWhiteSpace(search.LSID)) {
            //    where += string.Format(" and t2.LSID = '{0}'", search.LSID);
            //}
            //if (!string.IsNullOrWhiteSpace(search.LSName)) {
            //    where += string.Format(" and t2.LSName like '%{0}%'", search.LSName);
            //}

            //// 承租自然人/法人
            //if (!string.IsNullOrWhiteSpace(search.RNName)) {
            //    where += string.Format(" and t3.RNName like '%{0}%'", search.RNName);
            //}                
            //if (!string.IsNullOrWhiteSpace(search.RNID)) {
            //    where += string.Format(" and t3.RNID = '{0}'", search.RNID);
            //}

            //// 建物門牌地址
            //if (!string.IsNullOrWhiteSpace(search.BAdd)) {
            //    where += string.Format(" and t4.BAdd like '%{0}%'", search.BAdd);
            //}                

            if (filter != null) {

                // 出租自然人/法人
                if (!string.IsNullOrWhiteSpace(filter.LSID)) {
                    where += string.Format(" and t1.LSID = '{0}'", filter.LSID);
                }
                if (!string.IsNullOrWhiteSpace(filter.LSName)) {
                    where += string.Format(" and t1.LSName like '%{0}%'", filter.LSName);
                }

                // 承租自然人/法人
                if (!string.IsNullOrWhiteSpace(filter.RNName)) {
                    where += string.Format(" and t1.RNName like '%{0}%'", filter.RNName);
                }
                if (!string.IsNullOrWhiteSpace(filter.RNID)) {
                    where += string.Format(" and t1.RNID = '{0}'", filter.RNID);
                }

                // 建物門牌地址
                if (!string.IsNullOrWhiteSpace(filter.BAdd)) {
                    where += string.Format(" and t1.BAdd like '%{0}%'", filter.BAdd);
                }

                // 二房東姓名
                if (!string.IsNullOrWhiteSpace(filter.SLID))
                    where += string.Format(" and t1.SLID = '{0}'", filter.SLID);
                // 所屬業務
                if (!string.IsNullOrWhiteSpace(filter.Sales))
                    where += string.Format(" and t1.Sales = '{0}'", filter.Sales);

                // 管理方
                if (!string.IsNullOrWhiteSpace(filter.MAID))
                    where += string.Format(" and t1.MAID = '{0}'", filter.MAID);
                // 多物件單編號
                if (!string.IsNullOrWhiteSpace(filter.MOID))
                    where += string.Format(" and t1.MOID like '%{0}%'", filter.MOID);

                //string address = Utils.memergeAddSearch(
                //                    filter.BAdd_1,
                //                    filter.BAdd_1_1,
                //                    filter.BAdd_1_2,
                //                    filter.BAdd_2,
                //                    filter.BAdd_2_1,
                //                    filter.BAdd_2_2,
                //                    filter.BAdd_2_3,
                //                    filter.BAdd_2_4,
                //                    filter.BAdd_3,
                //                    filter.BAdd_3_1,
                //                    filter.BAdd_3_2,
                //                    filter.BAdd_4
                //                 );
                //if (!string.IsNullOrWhiteSpace(address))
                //    where += string.Format(" and BAdd like '%{0}%'", address);

                //if (!string.IsNullOrWhiteSpace(filter.BAdd_1))
                //    where += string.Format(" and BAdd_1 like '%{0}%'", filter.BAdd_1);
                //if (!string.IsNullOrWhiteSpace(filter.BAdd_2))
                //    where += string.Format(" and BAdd_2 like '%{0}%'", filter.BAdd_2);
                //if (!string.IsNullOrWhiteSpace(filter.BAdd_3))
                //    where += string.Format(" and BAdd_3 like '%{0}%'", filter.BAdd_3);
                //if (!string.IsNullOrWhiteSpace(filter.BAdd_4))
                //    where += string.Format(" and BAdd_4 like '%{0}%'", filter.BAdd_4);
                //if (!string.IsNullOrWhiteSpace(filter.BAdd_5))
                //    where += string.Format(" and BAdd_5 like '%{0}%'", filter.BAdd_5);
                //if (!string.IsNullOrWhiteSpace(filter.BAdd_6))
                //    where += string.Format(" and BAdd_6 like '%{0}%'", filter.BAdd_6);
                //if (!string.IsNullOrWhiteSpace(filter.BAdd_7))
                //    where += string.Format(" and BAdd_7 like '%{0}%'", filter.BAdd_7);
                //if (!string.IsNullOrWhiteSpace(filter.BAdd_8))
                //    where += string.Format(" and BAdd_8 like '%{0}%'", filter.BAdd_8);
                //if (!string.IsNullOrWhiteSpace(filter.BAdd_9))
                //    where += string.Format(" and BAdd_9 like '%{0}%'", filter.BAdd_9);


                // 媒合編號
                if (!string.IsNullOrWhiteSpace(filter.MID))
                    where += string.Format(" and t1.MID like '%{0}%' ", filter.MID);
                // 合約編號
                if (!string.IsNullOrWhiteSpace(filter.CID))
                    where += string.Format(" and t1.CID like '%{0}%' ", filter.CID);
                // 合約狀態
                if (!string.IsNullOrWhiteSpace(filter.CStatus))
                    where += string.Format(" and t1.CStatus like '%{0}%' ", filter.CStatus);
            }

            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            var list = await _repository.FindWithPagerRelationAsync(where, pagerInfo, search.Sort, order);

            List<ContractOutputDto> resultList = new List<ContractOutputDto>();
            if (list != null && list.Count > 0) {
                ContractOutputDto outputDto = null;
                foreach (var item in list) {
                    var d = item as IDictionary<string, object>;
                    outputDto = new ContractOutputDto();
                    outputDto.Id = $@"{d["Id"]}";
                    outputDto.CID = $@"{d["CID"]}";
                    outputDto.Version = $@"{d["Version"]}";
                    outputDto.LSName = $@"{d["LSName"]}";
                    outputDto.SLName = $@"{d["SLName"]}";
                    outputDto.RNName = $@"{d["RNName"]}";
                    outputDto.MName = $@"{d["MName"]}";
                    outputDto.MOID = $@"{d["MOID"]}";
                    outputDto.BAdd = $@"{d["BAdd"]}";
                    outputDto.BRDStart = $@"{d["BRDStart"]}";
                    outputDto.BRDTEnd = $@"{d["BRDTEnd"]}";
                    outputDto.BTCRent = $@"{d["BTCRent"]}";
                    outputDto.CStatus = $@"{d["CStatus"]}";
                    outputDto.SalesName = $@"{d["SalesName"]}"; ;
                    resultList.Add(outputDto);
                }
            }

            PageResult<ContractOutputDto> pageResult = new PageResult<ContractOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = resultList,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        /// <summary>
        /// 建物地址是否己綁定某合約
        /// </summary>
        /// <param name="badd">建物地址</param>
        /// <returns></returns>
        public async Task<ContractOutputDto> GetExistContractByBAdd(string badd)
        {
            Contract contract = await _repository.GetWhereAsync(string.Format("BAdd = '{0}' AND CStatus = '效期中'", badd));
            ContractOutputDto outputDto = contract.MapTo<ContractOutputDto>();

            return outputDto;
        }

        /// <summary>
        /// 根據合約編號查詢合約
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<ContractOutputDto> GetByCID(string cid)
        {
            Contract contract = await _repository.GetWhereAsync(string.Format("CID = '{0}'", cid));
            ContractOutputDto outputDto = contract.MapTo<ContractOutputDto>();

            return outputDto;
        }

        /// <summary>
        /// 根據合約編號查詢完整合約資訊
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<ContractOutputDto> GetContract(string cid)
        {
            Contract contract = await _repository.GetWhereAsync(string.Format("CID = '{0}'", cid));
            ContractOutputDto outputDto = contract.MapTo<ContractOutputDto>();

            TBNoCOutputDto c1ODto = new TBNoCOutputDto();
            TBNoC_2OutputDto c2ODto = new TBNoC_2OutputDto();
            TBNoC_3OutputDto c3ODto = new TBNoC_3OutputDto();
            ContractLandlord clInfo = new ContractLandlord();
            ContractRenter creInfo = new ContractRenter();

            switch (outputDto.CType.Trim()) {
                case "1":
                    // 出租人
                    clInfo = await _contractLandlordService.GetByCID(cid);
                    if (clInfo != null) {
                        outputDto.LSInfo = clInfo;
                    }
                    TBNoC1 c1bz = await _tbNoC1Service.GetByCID(cid);
                    if (c1bz != null) {
                        c1ODto = c1bz.MapTo<TBNoCOutputDto>();

                    }
                    TBNoC1_2 c1_2bz = await _tbNoC1_2Service.GetByCID(cid);
                    if (c1_2bz != null) {
                        c2ODto = c1_2bz.MapTo<TBNoC_2OutputDto>();

                    }
                    TBNoC1_3 c1_3bz = await _tbNoC1_3Service.GetByCID(cid);
                    if (c1_3bz != null) {
                        c3ODto = c1_3bz.MapTo<TBNoC_3OutputDto>();

                    }
                    break;
                case "2":
                    // 承租人
                    creInfo = await _contractRenterService.GetByCID(cid);
                    if (creInfo != null) {
                        outputDto.RNInfo = creInfo;
                    }
                    TBNoC2 c2bz = await _tbNoC2Service.GetByCID(cid);
                    if (c2bz != null) {
                        c1ODto = c2bz.MapTo<TBNoCOutputDto>();
                    }
                    TBNoC2_2 c2_2bz = await _tbNoC2_2Service.GetByCID(cid);
                    if (c2_2bz != null) {
                        c2ODto = c2_2bz.MapTo<TBNoC_2OutputDto>();

                    }
                    TBNoC2_3 c2_3bz = await _tbNoC2_3Service.GetByCID(cid);
                    if (c2_3bz != null) {
                        c3ODto = c2_3bz.MapTo<TBNoC_3OutputDto>();

                    }
                    break;
                case "3":
                    // 出租人
                    clInfo = await _contractLandlordService.GetByCID(cid);
                    if (clInfo != null) {
                        outputDto.LSInfo = clInfo;
                    }
                    // 承租人
                    creInfo = await _contractRenterService.GetByCID(cid);
                    if (creInfo != null) {
                        outputDto.RNInfo = creInfo;
                    }
                    TBNoC3 c3bz = await _tbNoC3Service.GetByCID(cid);
                    if (c3bz != null) {
                        c1ODto = c3bz.MapTo<TBNoCOutputDto>();
                    }
                    TBNoC3_2 c3_2bz = await _tbNoC3_2Service.GetByCID(cid);
                    if (c3_2bz != null) {
                        c2ODto = c3_2bz.MapTo<TBNoC_2OutputDto>();

                    }
                    TBNoC3_3 c3_3bz = await _tbNoC3_3Service.GetByCID(cid);
                    if (c3_3bz != null) {
                        c3ODto = c3_3bz.MapTo<TBNoC_3OutputDto>();

                    }
                    break;
            }

            outputDto.BZOutputDto = c1ODto;
            outputDto.BZ2OutputDto = c2ODto;
            outputDto.BZ3OutputDto = c3ODto;

            // 建物
            ContractBuildingOutputDto cbInfo = await _contractBuildingService.GetByCID(cid);
            if (cbInfo != null) {
                outputDto.BuildingInfo = cbInfo;
            }

            // 合約相關資料
            ContractRelevantOutputDto crInfo = await _contractRelevantService.GetByCID(cid);
            if (crInfo != null) {
                outputDto.ContractRelevantInfo = crInfo;
            }

            // 合約必要附件
            List<ContractAttachmentOutputDto> majorAttachmentInfoList = await _contractAttachmentService.GetMajorByCID(cid);
            if (majorAttachmentInfoList != null && majorAttachmentInfoList.Count > 0) {
                List<ContractAttachmentOutputDto> tempList = new List<ContractAttachmentOutputDto>();
                tempList.Add(majorAttachmentInfoList.Single(m => "主約".Equals(m.ArchiveTo)));
                tempList.AddRange(majorAttachmentInfoList.Where(m => !"主約".Equals(m.ArchiveTo)).OrderBy(m => m.FormName).ToList<ContractAttachmentOutputDto>());
                outputDto.MajorAttachmentList = tempList;
            }

            // 合約其他附件
            List<ContractAttachmentOutputDto> minorAttachmentInfoList = await _contractAttachmentService.GetMinorByCID(cid);
            if (minorAttachmentInfoList != null && minorAttachmentInfoList.Count > 0) {
                outputDto.MinorAttachmentList = minorAttachmentInfoList.OrderBy(m => m.FormName).ToList<ContractAttachmentOutputDto>();
            }

            // 合約歷史
            List<HistoryFormContractOutputDto> historyInfoList = await _contractHistoryService.GetByCID(cid);
            if (historyInfoList != null && historyInfoList.Count > 0) {
                outputDto.ContractHistoryList = historyInfoList;
            }

            // 審核記錄
            string flowId = $"{cid}";
            List<ContractFlowLogOutputDto> flowLogInfoList = await _contractFlowLogService.GetByCID(flowId);
            if (flowLogInfoList != null && flowLogInfoList.Count > 0) {
                outputDto.ContractFlowLogList = flowLogInfoList;
            }

            return outputDto;
        }

        /// <summary>
        /// 根據合約編號查詢合約
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<Contract> GetByCIDRaw(string cid)
        {
            Contract contract = await _repository.GetWhereAsync(string.Format("CID = '{0}'", cid));

            return contract;
        }

        /// <summary>
        /// 根據合約編號+合約狀態查詢合約
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <param name="status">合約狀態</param>
        /// <returns></returns>
        public async Task<Contract> GetByStatusRaw(string cid, string status)
        {
            Contract contract = await _repository.GetWhereAsync(string.Format("CID = '{0}' AND CStatus = '{1}'", cid, status));

            return contract;
        }

        /// <summary>
        /// 更新主約狀態()
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="cid">合約編號</param>
        /// <param name="status">附件狀態</param>
        /// <param name="version">目前歷史版本</param>
        /// <returns></returns> 
        public async Task<bool> UpdateContractStatus(string userId, string cid, string status, string version = "", IDbConnection conn = null, IDbTransaction trans = null)
        {
            DateTime time = DateTime.Now;

            bool result = false;
            Contract info = await _repository.GetWhereAsync($"CID = '{cid}'", conn, trans);

            if (info != null) {
                info.CStatus = status;
                info.LastModifyTime = time;
                info.LastModifyUserId = userId;

                result = await repository.UpdateAsync(info, info.Id, conn, trans);
            }

            return result;
        }

        /// <summary>
        /// 更新主約狀態(排程用)
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="cid">合約編號</param>
        /// <param name="status">附件狀態</param>
        /// <param name="version">目前歷史版本</param>
        /// <returns></returns> 
        public bool UpdateContractStatusTask(string userId, string cid, string status, string version = "", IDbConnection conn = null, IDbTransaction trans = null)
        {
            DateTime time = DateTime.Now;

            bool result = false;
            Contract info = _repository.GetWhere($"CID = '{cid}'", conn, trans);

            if (info != null) {
                info.CStatus = status;
                info.LastModifyTime = time;
                info.LastModifyUserId = userId;

                result = repository.Update(info, info.Id, trans);
            }

            return result;
        }

        /// <summary>
        /// 更新主約最新歷史版本
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="cid">合約編號</param>
        /// <param name="version">最新歷史版本</param>
        /// <returns></returns> 
        public async Task<bool> UpdateContractVersion(string userId, string cid, string version, IDbConnection conn = null, IDbTransaction trans = null)
        {
            DateTime time = DateTime.Now;

            bool result = false;
            Contract info = await _repository.GetWhereAsync($"CID = '{cid}'", conn, trans);

            if (info != null) {
                info.Version = version;
                info.LastModifyTime = time;
                info.LastModifyUserId = userId;

                result = await repository.UpdateAsync(info, info.Id, conn, trans);
            }

            return result;
        }

        /// <summary>
        /// 更新簽約日
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="cid">合約編號</param>
        /// <param name="contractDate">簽約日</param>
        /// <returns></returns> 
        public async Task<bool> UpdateContractDate(string userId, string cid, string contractDate, IDbConnection conn = null, IDbTransaction trans = null)
        {
            DateTime time = DateTime.Now;

            bool result = false;
            Contract info = await _repository.GetWhereAsync($"CID = '{cid}'", conn, trans);

            if (info != null && !string.IsNullOrEmpty(contractDate)) {
                string[] contractDateArray = contractDate.Split("-");
                info.ContractDate = contractDate;
                if (contractDateArray.Length == 3) {
                    info.Contract_Y = contractDateArray[0];
                    info.Contract_M = contractDateArray[1];
                    info.Contract_D = contractDateArray[2];
                }
                info.LastModifyTime = time;
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

        public static bool sendMail(string cid, string version, string CCObjectNo, string badd, string recipient, string type, string emailAddress)
        {
            List<string> recipients = new List<string>();
            recipients = emailAddress.Split(",").ToList();

            string subject = $"[簽核]*合約管理【申請人：{recipient}(內勤)，物件編號：{CCObjectNo}，簽核結果：待簽核】";
            StringBuilder body = new StringBuilder();

            switch (type) {
                case "Sales":
                    body.AppendFormat("執行時間：{0}<br>", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                    body.AppendFormat("簽核單號：{0}-{1}<br>", cid, version);
                    body.AppendFormat("建物地址：{0}<br>", badd);
                    body.Append("簽核結果：待簽核<br>");
                    body.Append("<font size=\"20px\" color=\"blue\">您有待審核表單，請至待辦事項審核</font><br>");
                    body.Append("<font size=\"14px\" color=\"red\">您已經瞭解並同意以下事項：</font><br>");
                    body.Append("<font size=\"14px\" color=\"red\">1. 所有客戶資料都不得未經當事人-房東/房客同意，擅自提供給他人。</font><br>");
                    body.Append("<font size=\"14px\" color=\"red\">2. 公告定型化契約之審閱期間，消費者保護法第11條之1即有明文。例如買賣方透過仲介<br>交易所簽訂的「不動產委託銷售契約書」及「成屋買賣契約書」，依規定之契約審閱期<br>至少3日，「預售屋買賣契約書」則至少5日。</font><br>");
                    body.Append("※請注意：此郵件是系統自動傳送，請勿直接回覆此郵件。");
                    break;
                case "Manager":
                    body.AppendFormat("執行時間：{0}<br>", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                    body.AppendFormat("簽核單號：{0}-{1}<br>", cid, version);
                    body.AppendFormat("建物地址：{0}<br>", badd);
                    body.Append("簽核結果：待簽核<br>");
                    body.Append("<font size=\"20px\" color=\"blue\">您有待審核表單，請至待辦事項審核</font><br>");
                    body.Append("<font size=\"14px\" color=\"red\">您已經瞭解並同意以下事項：</font><br>");
                    body.Append("<font size=\"14px\" color=\"red\">所有客戶資料都不得未經當事人-房東/房客同意，擅自提供給他人。</font><br>");
                    body.Append("※請注意：此郵件是系統自動傳送，請勿直接回覆此郵件。");
                    break;
            }

            var mailBodyEntity = new MailBodyEntity()
            {
                Body = body.ToString(),
                Recipients = recipients,
                Subject = subject
            };

            SendResultEntity sendResult = SendMailHelper.SendMail(mailBodyEntity);
            return sendResult.ResultStatus;
        }
    }
}