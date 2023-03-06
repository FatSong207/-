using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.Chaochi.Core.Helpers;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.IServices;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Json;
using System.IO;
using PdfSharp.Pdf.IO;
using System.Reflection;
using System.Linq;
using PdfSharp.Pdf.AcroForms;
using PdfSharp.Pdf;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Http;
using Yuebon.Chaochi.Core.IServices;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Chaochi.Core.Models;
using System.Globalization;
using System.Data;
using Yuebon.Commons.IDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Pages;
using Yuebon.Chaochi.Services;
using Merger = Yuebon.Chaochi.Core.Helpers.Merger;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.Annotations;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class ContractController : AreaApiController<Contract, ContractOutputDto, ContractInputDto, IContractService, string>
    {
        private IContractAttachmentService contractAttachmentService;
        private IHistoryFormContractService contractHistoryService;
        private IContractRelevantService contractRelevantService;
        private IContractBuildingService contractBuildingService;
        private IContractLandlordService contractLandlordService;
        private IContractRenterService contractRenterService;
        private IContractRemittanceService contractRemittanceService;
        private ISLMAService slmaService;
        private IManagementService managementService;
        private ISecondLandlordService secondLandlordService;
        private ITBNoC1Service tbNoC1Service;
        private ITBNoC1_2Service tbNoC1_2Service;
        private ITBNoC1_3Service tbNoC1_3Service;
        private ITBNoC2Service tbNoC2Service;
        private ITBNoC2_2Service tbNoC2_2Service;
        private ITBNoC2_3Service tbNoC2_3Service;
        private ITBNoC3Service tbNoC3Service;
        private ITBNoC3_2Service tbNoC3_2Service;
        private ITBNoC3_3Service tbNoC3_3Service;
        private ICategoryContractService categoryContractService;
        private ICustomerLCService customerLCService;
        private ICustomerLNService customerLNService;
        private ICustomerRNService customerRNService;
        private readonly IRemittanceRService remittanceRService;
        private readonly IRemittanceLService remittanceLService;
        private IBuildingService buildingService;
        private readonly IBuilding1Service building1Service;
        private IToDoListService todoListService;
        private IContractFlowLogService contractFlowLogService;
        private IMOService moService;
        private IMOBuildingService moBuildingService;        
        private Security.IServices.IUserService userService;
        private readonly IDbContextCore ybContext;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_contractAttachmentService"></param>
        /// <param name="_contractHistoryService"></param>
        /// <param name="_contractRelevantService"></param>
        /// <param name="_contractBuildingService"></param>
        /// <param name="_contractLandlordService"></param>
        /// <param name="_contractRenterService"></param>
        /// <param name="_contractRemittanceService"></param>
        /// <param name="_slmaService"></param>
        /// <param name="_managementService"></param>
        /// <param name="_secondLandlordService"></param>
        /// <param name="_tbNoC1Service"></param>
        /// <param name="_tbNoC1_2Service"></param>
        /// <param name="_tbNoC1_3Service"></param>
        /// <param name="_tbNoC2Service"></param>
        /// <param name="_tbNoC2_2Service"></param>
        /// <param name="_tbNoC2_3Service"></param>
        /// <param name="_tbNoC3Service"></param>
        /// <param name="_tbNoC3_2Service"></param>
        /// <param name="_tbNoC3_3Service"></param>
        /// <param name="_categoryContractService"></param>
        /// <param name="_ICustomerLCService"></param>
        /// <param name="_ICustomerLNService"></param>
        /// <param name="_ICustomerRNService"></param>
        /// <param name="_remittanceRService"></param>
        /// <param name="_remittanceLService"></param>
        /// <param name="_buildingService"></param>
        /// <param name="_building1Service"></param >
        /// <param name="_todoListService"></param>
        /// <param name="_contractFlowLogService"></param>
        /// <param name="_moBuildingService"></param>
        /// 
        /// <param name="_userService"></param>
        /// <param name="_ybContext"></param>
        public ContractController(IContractService _iService,
            IContractAttachmentService _contractAttachmentService,
            IHistoryFormContractService _contractHistoryService,
            IContractRelevantService _contractRelevantService,
            IContractBuildingService _contractBuildingService,
            IContractLandlordService _contractLandlordService,
            IContractRenterService _contractRenterService,
            IContractRemittanceService _contractRemittanceService,
            ISLMAService _slmaService,
            IManagementService _managementService,
            ISecondLandlordService _secondLandlordService,
            ITBNoC1Service _tbNoC1Service,
            ITBNoC1_2Service _tbNoC1_2Service,
            ITBNoC1_3Service _tbNoC1_3Service,
            ITBNoC2Service _tbNoC2Service,
            ITBNoC2_2Service _tbNoC2_2Service,
            ITBNoC2_3Service _tbNoC2_3Service,
            ITBNoC3Service _tbNoC3Service,
            ITBNoC3_2Service _tbNoC3_2Service,
            ITBNoC3_3Service _tbNoC3_3Service,
            ICategoryContractService _categoryContractService,
            ICustomerLCService _ICustomerLCService,
            ICustomerLNService _ICustomerLNService,
            ICustomerRNService _ICustomerRNService,
            IRemittanceRService _remittanceRService,
            IRemittanceLService _remittanceLService,
            IBuildingService _buildingService,
            IBuilding1Service _building1Service,
            IToDoListService _todoListService,
            IContractFlowLogService _contractFlowLogService,
            IMOService _moService,
            IMOBuildingService _moBuildingService,
            Security.IServices.IUserService _userService,
            IDbContextCore _ybContext
            ) : base(_iService)
        {
            iService = _iService;
            contractAttachmentService = _contractAttachmentService;
            contractHistoryService = _contractHistoryService;
            contractRelevantService = _contractRelevantService;
            contractBuildingService = _contractBuildingService;
            contractLandlordService = _contractLandlordService;
            contractRenterService = _contractRenterService;
            contractRemittanceService = _contractRemittanceService;
            slmaService = _slmaService;
            managementService = _managementService;
            secondLandlordService = _secondLandlordService;
            tbNoC1Service = _tbNoC1Service;
            tbNoC1_2Service = _tbNoC1_2Service;
            tbNoC1_3Service = _tbNoC1_3Service;
            tbNoC2Service = _tbNoC2Service;
            tbNoC2_2Service = _tbNoC2_2Service;
            tbNoC2_3Service = _tbNoC2_3Service;
            tbNoC3Service = _tbNoC3Service;
            tbNoC3_2Service = _tbNoC3_2Service;
            tbNoC3_3Service = _tbNoC3_3Service;
            categoryContractService = _categoryContractService;
            customerLCService = _ICustomerLCService;
            customerLNService = _ICustomerLNService;
            customerRNService = _ICustomerRNService;
            remittanceRService = _remittanceRService;
            remittanceLService = _remittanceLService;
            buildingService = _buildingService;
            building1Service = _building1Service;
            todoListService = _todoListService;
            contractFlowLogService = _contractFlowLogService;
            moService = _moService;
            moBuildingService = _moBuildingService;
            userService = _userService;
            ybContext = _ybContext;
        }

        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Contract info)
        {
            info.Version = "1";
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.CreatorUserOrgId = CurrentUser.OrganizeId;
            info.CreatorUserDeptId = CurrentUser.DeptId;
        }

        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(Contract info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerAsync2")]
        [YuebonAuthorize("List")]
        public async Task<CommonResult<PageResult<ContractOutputDto>>> FindWithPagerAsync(SearchContractModel search)
        {
            CommonResult<PageResult<ContractOutputDto>> result = new CommonResult<PageResult<ContractOutputDto>>();
            result.ResData = await iService.FindWithPagerAsync(search);
            result.ErrCode = ErrCode.successCode;
            return result;
        }

        /// <summary>
        /// 異步新增數據
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(ContractInputDto input)
        {
            CommonResult result = new CommonResult();

            string CreatorUserId = CurrentUser?.UserId;
            DateTime CreatorTime = DateTime.Now;

            ContractRelevant crInfo = new ContractRelevant();
            ContractLandlord clInfo = null;
            ContractRenter creInfo = null;
            ContractBuilding cbInfo = new ContractBuilding();

            SecondLandlordOutputDto secondLandlordODto = new SecondLandlordOutputDto();
            SLMAOutputDto slmaODto = new SLMAOutputDto();

            // 建物            
            string BAdd = input.BAdd;
            if (!string.IsNullOrWhiteSpace(BAdd)) {
                string BId = buildingService.GetIdByBAdd(BAdd);
                if (!string.IsNullOrWhiteSpace(BId)) {
                    Building buildingInfo = await buildingService.GetAsync(BId);                    
                    if (buildingInfo != null) {                        
                        if (buildingInfo.BState == "招租中" || buildingInfo.BState == "已收訂" || buildingInfo.BState == "已出租") {                            
                            //這建物型態是否符合（註1）＋有無合約＋是否同類型合約（註2）＋租賃起迄日是否有重疊？（註3）
                            //    A建物型態符合無簽約日＝無合約，PASS（新約）
                            //    B建物型態符合有簽約日＝有合約不同類型合約，PASS（新約）
                            //    C建物型態符合有簽約日＝有合約同類型合約租賃起迄日不重疊，PASS（新約或續約）
                            var existContract = await iService.GetExistContractByBAdd(BAdd);
                            if (existContract != null) {
                                if (input.CType == existContract.CType) {
                                    if (!string.IsNullOrEmpty(existContract.BRDStart) && !string.IsNullOrEmpty(existContract.BRDTEnd)) {
                                        CultureInfo culture = new CultureInfo("zh-TW");
                                        culture.DateTimeFormat.Calendar = new TaiwanCalendar();

                                        DateTime newContractStartDate = DateTime.Parse(input.BRDStart, culture);
                                        DateTime newContractEndDate = DateTime.Parse(input.BRDStart, culture);
                                        DateTime existContractStartDate = DateTime.Parse(existContract.BRDStart, culture);
                                        DateTime existContractEndDate = DateTime.Parse(existContract.BRDTEnd, culture);
                                        if ((newContractStartDate.Ticks > existContractStartDate.Ticks && newContractStartDate.Ticks < existContractEndDate.Ticks) || (newContractEndDate.Ticks > existContractStartDate.Ticks && newContractEndDate.Ticks < existContractEndDate.Ticks)) {
                                            result.Success = false;
                                            result.ErrMsg = "租賃起迄日重疊";
                                            return ToJsonContent(result);
                                        }
                                    }
                                }
                            }

                            Building1 building1Info = await building1Service.GetAsync(BId);
                            cbInfo = Merger.DictCloneAndMerge<ContractBuilding>(cbInfo, (buildingInfo.GetType()
                                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(buildingInfo, null))));
                            cbInfo = Merger.DictCloneAndMerge<ContractBuilding>(cbInfo, (building1Info.GetType()
                                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(building1Info, null))));
                            // 合約相關資料用
                            crInfo.B1TaxID = building1Info.B1TaxID;
                            //input.BRDStart = buildingInfo.BRDStart;
                            //input.BRDTEnd = buildingInfo.BRDTEnd;

                            // 合約負責業務
                            Security.Models.User salesInfo = await userService.GetAsync(buildingInfo.CreatorUserId);
                            input.Sales = (salesInfo != null) ? salesInfo.Account : string.Empty;
                            input.SalesName = (salesInfo != null) ? salesInfo.RealName : string.Empty;

                            // 轉租合約需要用地址查詢出租人姓名
                            if (input.CType == "2") {
                                var lnInfo = await customerLNService.GetByCustomerLNID(buildingInfo.BelongLID);
                                if (lnInfo != null) {
                                    input.LSName = lnInfo.LNName;
                                } else {
                                    var lcInfo = await customerLCService.GetByLCID(buildingInfo.BelongLID);
                                    if (lcInfo != null) {
                                        input.LSName = lcInfo.LCName;
                                    }
                                }
                            }
                        } else {
                            result.Success = false;
                            result.ErrMsg = "建物狀態錯誤";
                            return ToJsonContent(result);
                        }
                    } else {
                        result.Success = false;
                        result.ErrMsg = "查無此建物";
                        return ToJsonContent(result);
                    }
                }
            }

            switch (input.CType) {
                case "1":
                    // 包租(出租人 二房東 管理人 建物)
                    // 出租人                    
                    string lid = input.LSID;
                    if (!string.IsNullOrWhiteSpace(lid)) {
                        clInfo = new ContractLandlord();
                        if (lid.Length == 10) {
                            var customerLN = await customerLNService.GetByCustomerLNID(lid);
                            if (customerLN != null) {
                                MapLN(ref clInfo, customerLN);
                                input.LSName = customerLN.LNName;
                            } else {
                                result.Success = false;
                                result.ErrMsg = "查無此出租人";
                                return ToJsonContent(result);
                            }
                        } else if (lid.Length == 8) {
                            var customerLC = await customerLCService.GetByLCID(lid);
                            if (customerLC != null) {
                                MapLC(ref clInfo, customerLC);
                                input.LSName = customerLC.LCName;
                            } else {
                                result.Success = false;
                                result.ErrMsg = "查無此出租人";
                                return ToJsonContent(result);
                            }
                        } else {
                            result.Success = false;
                            result.ErrMsg = "請檢查出租人身分證字號或是統一編號";
                            return ToJsonContent(result);
                        }
                    }

                    // 二房東
                    secondLandlordODto = await secondLandlordService.GetById(input.SLID);
                    slmaODto = await slmaService.GetBySLMAInfo(input.SLID, input.SIAdd);
                    if (secondLandlordODto != null && slmaODto != null) {
                        MapSL(ref input, secondLandlordODto, slmaODto);
                    } else {
                        result.Success = false;
                        result.ErrMsg = "查無此二房東(or SLMA)";
                        return ToJsonContent(result);
                    }

                    break;
                case "2":
                    // 轉租(二房東 承租人 建物)
                    // 承租人
                    string rnid = input.RNID;
                    if (!string.IsNullOrWhiteSpace(rnid)) {
                        creInfo = new ContractRenter();
                        if (rnid.Length == 10) {
                            var customerRN = await customerRNService.GetCustomerByRNID(rnid);
                            if (customerRN != null) {
                                creInfo = customerRN.MapTo<ContractRenter>();
                                input.RNName = customerRN.RNName;
                                // 合約相關資料用
                                crInfo.RNRentSUBAFee = customerRN.RNRentSUBAFee;
                                crInfo.RNQualify = GetRNQualify(customerRN);
                                //crInfo.RNQualify1C = customerRN.RNQualify1C;
                                //crInfo.RNQualify2C = customerRN.RNQualify2C;
                                //crInfo.RNQualify3C = customerRN.RNQualify3C;
                            } else {
                                result.Success = false;
                                result.ErrMsg = "查無此承租人";
                                return ToJsonContent(result);
                            }
                        } else {
                            result.Success = false;
                            result.ErrMsg = "請檢查承租人身分證字號或是統一編號";
                            return ToJsonContent(result);
                        }
                    }

                    // 二房東
                    secondLandlordODto = await secondLandlordService.GetById(input.SLID);
                    slmaODto = await slmaService.GetBySLMAInfo(input.SLID, input.SIAdd);
                    if (secondLandlordODto != null && slmaODto != null) {
                        MapSL(ref input, secondLandlordODto, slmaODto);
                    } else {
                        result.Success = false;
                        result.ErrMsg = "查無此二房東(or SLMA)";
                        return ToJsonContent(result);
                    }

                    break;
                case "3":
                    // 代管(代租)(出租人 承租人 管理方 建物)
                    // 出租人
                    lid = input.LSID;
                    if (!string.IsNullOrWhiteSpace(lid)) {
                        clInfo = new ContractLandlord();
                        if (lid.Length == 10) {
                            var customerLN = await customerLNService.GetByCustomerLNID(lid);
                            if (customerLN != null) {
                                MapLN(ref clInfo, customerLN);
                                input.LSName = customerLN.LNName;
                            } else {
                                result.Success = false;
                                result.ErrMsg = "查無此出租人";
                                return ToJsonContent(result);
                            }
                        } else if (lid.Length == 8) {
                            var customerLC = await customerLCService.GetByLCID(lid);
                            if (customerLC != null) {
                                MapLC(ref clInfo, customerLC);
                                input.LSName = customerLC.LCName;
                            } else {
                                result.Success = false;
                                result.ErrMsg = "查無此出租人";
                                return ToJsonContent(result);
                            }
                        } else {
                            result.Success = false;
                            result.ErrMsg = "請檢查出租人身分證字號或是統一編號";
                            return ToJsonContent(result);
                        }
                    }

                    // 承租人
                    rnid = input.RNID;
                    if (!string.IsNullOrWhiteSpace(rnid)) {
                        creInfo = new ContractRenter();
                        if (rnid.Length == 10) {
                            var customerRN = await customerRNService.GetCustomerByRNID(rnid);
                            if (customerRN != null) {
                                creInfo = customerRN.MapTo<ContractRenter>();
                                input.RNName = customerRN.RNName;
                                // 合約相關資料用
                                crInfo.RNRentSUBAFee = customerRN.RNRentSUBAFee;
                                crInfo.RNQualify = GetRNQualify(customerRN);
                            } else {
                                result.Success = false;
                                result.ErrMsg = "查無此承租人";
                                return ToJsonContent(result);
                            }
                        } else {
                            result.Success = false;
                            result.ErrMsg = "請檢查承租人身分證字號或是統一編號";
                            return ToJsonContent(result);
                        }
                    }

                    // 管理方
                    ManagementOutputDto managementODto = await managementService.GetById(input.MAID);
                    if (managementODto != null) {
                        input.MName = managementODto.MName;
                        input.MRep = managementODto.MRep;
                        input.MLRNo = managementODto.MLRNo;
                        input.MAdd = input.SIAdd;
                        input.MTel = input.SITel;
                        string[] telArray = Utils.getTelArray(input.MTel);
                        if (telArray != null && telArray.Length > 0) {
                            input.MTel_1 = telArray[0];
                            input.MTel_2 = telArray[1];
                        }
                        slmaODto = await slmaService.GetBySLMAInfo(input.MAID, input.SIAdd);
                        if (slmaODto != null && !string.IsNullOrEmpty(slmaODto.Fax)) {
                            input.MFax = slmaODto.Fax;
                            string[] faxArray = Utils.getTelArray(slmaODto.Fax);
                            if (faxArray != null && faxArray.Length > 0) {
                                input.MFax_1 = faxArray[0];
                                input.MFax_2 = faxArray[1];
                            }
                        }
                    } else {
                        result.Success = false;
                        result.ErrMsg = "查無此管理方";
                        return ToJsonContent(result);
                    }
                    break;
            }

            // 管理人
            if (!String.IsNullOrEmpty(input.SILRNo)) {
                SLMAOutputDto slmaODtoSI = await slmaService.GetBySILRNo(input.SILRNo);
                if (slmaODtoSI != null) {
                    input.SIName = slmaODtoSI.SIName;
                    input.SIAdd = slmaODtoSI.SIAdd;
                    input.SITel = slmaODtoSI.SITel;
                    string[] telArray = Utils.getTelArray(slmaODtoSI.SITel);
                    if (telArray != null && telArray.Length > 0) {
                        input.SITel_1 = telArray[0];
                        input.SITel_2 = telArray[1];
                    }
                    input.SIEmail = slmaODtoSI.SIEmail;
                } else {
                    result.Success = false;
                    result.ErrMsg = "查無此管理人";
                    return ToJsonContent(result);
                }

            }

            // 經紀人
            if (!String.IsNullOrEmpty(input.AGLRNo)) {
                SLMAOutputDto slmaODtoAG = await slmaService.GetByAGLRNo(input.AGLRNo);
                if (slmaODtoAG != null) {
                    input.AGName = slmaODtoAG.AGName;
                    input.AGAdd = slmaODtoAG.AGAdd;
                    input.AGTel = slmaODtoAG.AGTel;
                    string[] telArray = Utils.getTelArray(slmaODtoAG.AGTel);
                    if (telArray != null && telArray.Length > 0) {
                        input.AGTel_1 = telArray[0];
                        input.AGTel_2 = telArray[1];
                    }
                    input.AGEmail = slmaODtoAG.AGEmail;
                    input.AGID = slmaODtoAG.AGID;
                } else {
                    result.Success = false;
                    result.ErrMsg = "查無此經紀人";
                    return ToJsonContent(result);
                }
            }

            input.CStatus = "備審未簽名";

            Contract info = input.MapTo<Contract>();
            OnBeforeInsert(info);

            // 合約匯款資料
            switch (input.RemittanceTarget) {
                case "出租人":
                    //RemittanceL remittanceLInfo = await remittanceLService.GetWhereAsync($"IDNo = '{input.LSID}' AND LAName = '{input.AccountNameL}'");
                    RemittanceL remittanceLInfo = await remittanceLService.GetWhereAsync($"Id = '{input.AccountNameL}'");
                    if (remittanceLInfo != null) {
                        //info.AccountName = input.AccountNameL;
                        info.AccountName = remittanceLInfo.LAName;
                        info.BankName = remittanceLInfo.LBankName;
                        info.BankNo = remittanceLInfo.LBankNo;
                        info.BranchName = remittanceLInfo.LBranchName;
                        info.BranchNo = remittanceLInfo.LBranchNo;
                        info.AccountNo = remittanceLInfo.LANo;
                    }
                    break;
                case "承租人":
                    //RemittanceL remittanceLInfo = await remittanceLService.GetWhereAsync($"IDNo = '{input.LSID}' AND LAName = '{input.AccountNameL}'");
                    RemittanceR remittanceRInfo = await remittanceRService.GetWhereAsync($"Id = '{input.AccountNameR}'");
                    if (remittanceRInfo != null) {
                        //info.AccountName = input.AccountNameL;
                        info.AccountName = remittanceRInfo.RAName;
                        info.BankName = remittanceRInfo.RBankName;
                        info.BankNo = remittanceRInfo.RBankNo;
                        info.BranchName = remittanceRInfo.RBranchName;
                        info.BranchNo = remittanceRInfo.RBranchNo;
                        info.AccountNo = remittanceRInfo.RANo;
                    }
                    break;
                case "公司":
                    info.AccountName = input.AccountNameC;
                    ContractRemittance remittanceCInfo = await contractRemittanceService.GetWhereAsync($"Type = '{input.RemittanceType}' AND AccountName = '{input.AccountNameC}' AND UseCounty = '{input.UseCounty}' AND BankName = '{input.BankName}'");
                    if (remittanceCInfo != null) {
                        info.AccountName = input.AccountNameC;
                        info.BankName = remittanceCInfo.BankName;
                        info.BankNo = remittanceCInfo.BankNo;
                        info.BranchName = remittanceCInfo.BranchName;
                        info.BranchNo = remittanceCInfo.BranchNo;
                    }
                    break;
            }

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                await iService.InsertAsync(info, conn, tran).ConfigureAwait(false);

                // 出租人
                if (clInfo != null) {
                    clInfo.Id = info.Id;
                    clInfo.CID = input.CID;
                    clInfo.CreatorUserId = CurrentUser?.UserId;
                    clInfo.CreatorTime = DateTime.Now;
                    await contractLandlordService.InsertAsync(clInfo, conn, tran).ConfigureAwait(false);
                }

                // 承租人
                if (creInfo != null) {
                    creInfo.Id = info.Id;
                    creInfo.CID = input.CID;
                    creInfo.CreatorUserId = CurrentUser?.UserId;
                    creInfo.CreatorTime = DateTime.Now;
                    await contractRenterService.InsertAsync(creInfo, conn, tran).ConfigureAwait(false);
                }

                // 建物
                if (cbInfo != null) {
                    cbInfo.Id = info.Id;
                    cbInfo.CID = input.CID;
                    cbInfo.CreatorUserId = CurrentUser?.UserId;
                    cbInfo.CreatorTime = DateTime.Now;
                    await contractBuildingService.InsertAsync(cbInfo, conn, tran).ConfigureAwait(false);
                }

                // 合約相關資料
                crInfo.Id = info.Id;
                crInfo.CID = input.CID;
                crInfo.CreatorUserId = CurrentUser?.UserId;
                crInfo.CreatorTime = DateTime.Now;
                await contractRelevantService.InsertAsync(crInfo, conn, tran).ConfigureAwait(false);

                // 合約必要附件
                ContractAttachment attachment = new ContractAttachment();
                attachment.CID = info.CID;
                attachment.FormName = info.CName;
                attachment.ArchiveTo = "主約";
                attachment.ArchiveID = string.Empty;
                attachment.Status = "未綁定";
                attachment.CreatorTime = CreatorTime;
                attachment.CreatorUserId = CreatorUserId;

                await contractAttachmentService.InsertAsync(attachment, conn, tran).ConfigureAwait(false);

                List<CategoryContractOutputDto> categoryList = await categoryContractService.GetByParent(info.CCate);
                if (categoryList != null && categoryList.Count > 0) {
                    foreach (CategoryContractOutputDto category in categoryList) {
                        attachment = new ContractAttachment();
                        attachment.CID = info.CID;
                        attachment.FormName = category.Name;
                        attachment.ArchiveTo = category.ArchiveLTo;
                        attachment.ArchiveID = GetArchiveID(input, category.ArchiveLTo);
                        attachment.Status = "未綁定";
                        attachment.CreatorTime = CreatorTime;
                        attachment.CreatorUserId = CreatorUserId;

                        await contractAttachmentService.InsertAsync(attachment, conn, tran).ConfigureAwait(false);
                    }
                }

                // 包租和轉租，新增合約時將建物狀態改為［已出租］
                if ("2".Equals(input.CType) || "3".Equals(input.CType)) {
                    string badd = info.BAdd;
                    if (!badd.Contains(";") && !badd.Contains("\n")) {
                        string bId = buildingService.GetIdByBAdd(badd);                        
                        await buildingService.UpdateBState(CurrentUser?.UserId, bId, "已出租", conn, tran).ConfigureAwait(false);
                    }
                }

                eftran.Commit();
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
                result.Success = true;
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("新增合約失敗", ex);
                result.ErrMsg = "新增合約失敗";
                result.ErrCode = ErrCode.err43001;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 更新合約相關資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateRelevant")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> UpdateRelevant(ContractRelevantInputDto input)
        {
            CommonResult result = new CommonResult();

            string userId = CurrentUser?.UserId;
            DateTime currentTime = DateTime.Now;

            ContractRelevant CR_DB = contractRelevantService.Get(input.Id);
            bool isNew = CR_DB is null;

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                bool DMLResult = false;
                if (isNew) {
                    ContractRelevant info = new ContractRelevant();
                    info.Id = input.Id;
                    info.CID = input.CID;
                    info.B1TaxID = input.B1TaxID;
                    info.RNRentSUBAFee = input.RNRentSUBAFee;
                    info.RNQualify = input.RNQualify;
                    info.CreatorTime = currentTime;
                    info.CreatorUserId = userId;

                    long count = await contractRelevantService.InsertAsync(info, conn, tran).ConfigureAwait(false);
                    DMLResult = count > 0;
                } else {
                    ContractRelevant info = input.MapTo(CR_DB);
                    info.LastModifyTime = currentTime;
                    info.LastModifyUserId = userId;
                    DMLResult = await contractRelevantService.UpdateAsync(info, input.Id, conn, tran).ConfigureAwait(false);
                }

                if (DMLResult == true) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    result.ErrMsg = "更新合約相關資料失敗";
                    result.ErrCode = ErrCode.err43002;
                    result.Success = false;
                }

            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("更新合約相關資料失敗", ex);
                result.ErrMsg = "更新合約相關資料失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 下載主約表單
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DownloadContract")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> DownloadContract(ContractInputDto input)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Security.Models.SysSetting>();

            MemoryStream PDFstream = new MemoryStream();
            using (var doc = PdfReader.Open($"{sysSetting.ChaochiFilepath}TemplatePDF/{input.CCate}.pdf", PdfDocumentOpenMode.Modify)) {
                if (doc.AcroForm == null) {
                    doc.Close();
                    CommonResult result = new CommonResult();
                    result.ErrCode = "40001";
                    result.ErrMsg = "上傳PDF檔案不含任何標籤";
                    return ToJsonContent(result, true);
                } else {
                    ContractLandlord clInfo = new ContractLandlord();
                    ContractRenter crInfo = new ContractRenter();

                    ContractOutputDto contract = await iService.GetByCID(input.CID);
                    ContractPDFDataModel PDFDataModel = new ContractPDFDataModel();
                    PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (contract.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(contract, null))));

                    // 處理BZ欄位
                    switch (contract.CType.Trim()) {
                        // 包租(出租人 二房東 管理人 建物)
                        case "1":
                            clInfo = await contractLandlordService.GetByCID(input.CID);
                            if (clInfo != null) {
                                PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (clInfo.GetType()
                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(clInfo, null))));
                            }
                            TBNoC1 c1bz = await tbNoC1Service.GetAsync(input.Id);
                            if (c1bz != null) {
                                PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (c1bz.GetType()
                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(c1bz, null))));
                            }
                            TBNoC1_2 c1bz_2 = await tbNoC1_2Service.GetAsync(input.Id);
                            if (c1bz_2 != null) {
                                PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (c1bz_2.GetType()
                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(c1bz_2, null))));
                            }
                            TBNoC1_3 c1bz_3 = await tbNoC1_3Service.GetAsync(input.Id);
                            if (c1bz_3 != null) {
                                PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (c1bz_3.GetType()
                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(c1bz_3, null))));
                            }
                            break;
                        case "2":
                            // 承租人
                            crInfo = await contractRenterService.GetByCID(input.CID);
                            if (crInfo != null) {
                                PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (crInfo.GetType()
                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(crInfo, null))));
                            }
                            TBNoC2 c2bz = await tbNoC2Service.GetAsync(input.Id);
                            if (c2bz != null) {
                                PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (c2bz.GetType()
                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(c2bz, null))));
                            }
                            TBNoC2_2 c2bz_2 = await tbNoC2_2Service.GetAsync(input.Id);
                            if (c2bz_2 != null) {
                                PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (c2bz_2.GetType()
                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(c2bz_2, null))));
                            }
                            TBNoC2_3 c2bz_3 = await tbNoC2_3Service.GetAsync(input.Id);
                            if (c2bz_3 != null) {
                                PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (c2bz_3.GetType()
                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(c2bz_3, null))));
                            }
                            break;
                        case "3":
                            // 出租人
                            clInfo = await contractLandlordService.GetByCID(input.CID);
                            if (clInfo != null) {
                                PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (clInfo.GetType()
                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(clInfo, null))));
                            }
                            // 承租人
                            crInfo = await contractRenterService.GetByCID(input.CID);
                            if (crInfo != null) {
                                PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (crInfo.GetType()
                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(crInfo, null))));
                            }
                            TBNoC3 c3bz = await tbNoC3Service.GetAsync(input.Id);
                            if (c3bz != null) {
                                PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (c3bz.GetType()
                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(c3bz, null))));
                            }
                            TBNoC3_2 c3bz_2 = await tbNoC3_2Service.GetAsync(input.Id);
                            if (c3bz_2 != null) {
                                PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (c3bz_2.GetType()
                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(c3bz_2, null))));
                            }
                            TBNoC3_3 c3bz_3 = await tbNoC3_3Service.GetAsync(input.Id);
                            if (c3bz_3 != null) {
                                PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (c3bz_3.GetType()
                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(c3bz_3, null))));
                            }
                            break;
                    }

                    // 合約相關資料
                    ContractRelevantOutputDto creInfo = await contractRelevantService.GetByCID(input.CID);
                    if (creInfo != null) {
                        //PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (creInfo.GetType()
                        //    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        //        .ToDictionary(prop => prop.Name, prop => prop.GetValue(creInfo, null))));
                        PDFDataModel.B1TaxID = creInfo.B1TaxID;
                        PDFDataModel.RNRentSUBAFee = creInfo.RNRentSUBAFee;
                    }

                    // 建物
                    ContractBuildingOutputDto cbInfo = await contractBuildingService.GetByCID(input.CID);
                    if (crInfo != null) {
                        PDFDataModel = Merger.DictCloneAndMerge<ContractPDFDataModel>(PDFDataModel, (cbInfo.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .ToDictionary(prop => prop.Name, prop => prop.GetValue(cbInfo, null))));
                    }

                    if (doc.AcroForm.Elements.ContainsKey("/NeedAppearances")) {
                        doc.AcroForm.Elements["/NeedAppearances"] = new PdfBoolean(true);
                    } else {
                        doc.AcroForm.Elements.Add("/NeedAppearances", new PdfBoolean(true));
                    }

                    Dictionary<string, object> dictFinal =
                            PDFDataModel.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .ToDictionary(prop => prop.Name, prop => prop.GetValue(PDFDataModel, null));

                    //設值
                    foreach (var name in doc.AcroForm.Fields.Names) {
                        if (doc.AcroForm.Fields[name] is PdfTextField) {
                            if (name == "FName" || name == "Vno" || name == "TBNO") {

                            } else {
                                if (doc.AcroForm.Fields[name].ReadOnly) {
                                    doc.AcroForm.Fields[name].ReadOnly = false;
                                    doc.AcroForm.Fields[name].Value = new PdfString(dictFinal.ContainsKey(name) ? (string)dictFinal[name] : "");
                                    doc.AcroForm.Fields[name].ReadOnly = true;
                                } else {
                                    doc.AcroForm.Fields[name].Value = new PdfString(dictFinal.ContainsKey(name) ? (string)dictFinal[name] : "");
                                }
                            }
                        } else if (doc.AcroForm.Fields[name].Value is PdfItem) {
                            if (doc.AcroForm.Fields[name].ReadOnly) {
                                doc.AcroForm.Fields[name].ReadOnly = false;
                                //https://stackoverflow.com/questions/33652517/pdf-checkbox-error-value-cannot-be-null-parameter-name-value
                                var ck = doc.AcroForm.Fields[name];
                                if (ck.HasKids) {
                                    foreach (var item in ck.Fields.Elements.Items) {
                                        if (dictFinal.ContainsKey(name) && (string)dictFinal[name] == "/Yes") {
                                            //  assumes you want to "check" the checkbox.  Use "/Off" if you want to uncheck.
                                            //  "/Yes" is defined in your pdf document as the checked value.May vary depending on original pdf creator.
                                            ((PdfDictionary)(((PdfReference)(item)).Value)).Elements.SetName(PdfAcroField.Keys.V, "/Yes");
                                            ((PdfDictionary)(((PdfReference)(item)).Value)).Elements.SetName(PdfAnnotation.Keys.AS, "/Yes");
                                        } else {
                                            //((PdfCheckBoxField)doc.AcroForm.Fields[name]).Checked = false;
                                            ((PdfDictionary)(((PdfReference)(item)).Value)).Elements.SetName(PdfAcroField.Keys.V, "/Off");
                                            ((PdfDictionary)(((PdfReference)(item)).Value)).Elements.SetName(PdfAnnotation.Keys.AS, "/Off");
                                        }
                                    }
                                } else {
                                    if (dictFinal.ContainsKey(name) && (string)dictFinal[name] == "/Yes") {
                                        ((PdfCheckBoxField)doc.AcroForm.Fields[name]).Checked = true;
                                    } else {
                                        ((PdfCheckBoxField)doc.AcroForm.Fields[name]).Checked = false;
                                    }
                                }
                                doc.AcroForm.Fields[name].ReadOnly = true;
                            } else {
                                //https://stackoverflow.com/questions/33652517/pdf-checkbox-error-value-cannot-be-null-parameter-name-value
                                var ck = doc.AcroForm.Fields[name];
                                if (ck.HasKids) {
                                    foreach (var item in ck.Fields.Elements.Items) {
                                        if (dictFinal.ContainsKey(name) && (string)dictFinal[name] == "/Yes") {
                                            //  assumes you want to "check" the checkbox.  Use "/Off" if you want to uncheck.
                                            //  "/Yes" is defined in your pdf document as the checked value.May vary depending on original pdf creator.
                                            ((PdfDictionary)(((PdfReference)(item)).Value)).Elements.SetName(PdfAcroField.Keys.V, "/Yes");
                                            ((PdfDictionary)(((PdfReference)(item)).Value)).Elements.SetName(PdfAnnotation.Keys.AS, "/Yes");
                                        } else {
                                            //((PdfCheckBoxField)doc.AcroForm.Fields[name]).Checked = false;
                                            ((PdfDictionary)(((PdfReference)(item)).Value)).Elements.SetName(PdfAcroField.Keys.V, "/Off");
                                            ((PdfDictionary)(((PdfReference)(item)).Value)).Elements.SetName(PdfAnnotation.Keys.AS, "/Off");
                                        }
                                    }
                                } else {
                                    if (dictFinal.ContainsKey(name) && (string)dictFinal[name] == "/Yes") {
                                        ((PdfCheckBoxField)(doc.AcroForm.Fields[name])).Checked = true;
                                    } else {
                                        ((PdfCheckBoxField)doc.AcroForm.Fields[name]).Checked = false;
                                    }
                                }
                            }

                        } else {
                            Console.WriteLine($"{name}: is not supported");
                        }
                    }

                    doc.Save(PDFstream);
                }
            }

            //https://stackoverflow.com/questions/34131326/using-mimemapping-in-asp-net-core
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
            return File(PDFstream, contentType ?? "image/jpeg");
        }

        /// <summary>
        ///  上傳主約表單
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("UploadContract")]
        [YuebonAuthorize("Edit")]
        [NoSignRequired]
        public async Task<IActionResult> UploadContract([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;

            try {
                string Id = string.Empty;
                if (formCollection.ContainsKey("Id")) {
                    Id = formCollection["Id"];
                }

                string CID = string.Empty;
                if (formCollection.ContainsKey("CID")) {
                    CID = formCollection["CID"];
                }

                string cCate = string.Empty;
                if (formCollection.ContainsKey("CCate")) {
                    cCate = formCollection["CCate"];
                }

                if (filelist == null || filelist.Count <= 0 || filelist.Count >= 2) {
                    result.ErrCode = "40001";
                    result.ErrMsg = "一次只能上傳一份PDF";
                    return ToJsonContent(result, true);
                }

                Dictionary<string, object> dictV = new Dictionary<string, object>();
                Dictionary<string, string> dictTU = new Dictionary<string, string>();
                IFormFile file = filelist[0];
                if (file != null && file.Length > 0) {
                    using (var doc = PdfReader.Open(file.OpenReadStream(), PdfDocumentOpenMode.Modify)) {
                        if (doc.AcroForm == null) {
                            doc.Close();
                            result.ErrCode = "40001";
                            result.ErrMsg = "上傳PDF不含任何標籤";
                            return ToJsonContent(result, true);
                        } else {
                            //取值
                            foreach (var name in doc.AcroForm.Fields.Names) {
                                //Console.WriteLine($"{name}:{doc.AcroForm.Fields[name].Value}");
                                if (doc.AcroForm.Fields[name].Value is PdfString) {
                                    if (doc.AcroForm.Fields[name].Value.ToString() == "<FEFF>") {
                                        //Console.WriteLine($"PdfString:{name} is <FEFF>:={((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value}");
                                        dictV.Add(name, "");
                                        dictTU.Add(name, ((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value.Trim());
                                    } else {
                                        //Console.WriteLine($"PdfString:{name}:{((PdfString)doc.AcroForm.Fields[name].Value).Value}:={((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value}");
                                        dictV.Add(name, ((PdfString)doc.AcroForm.Fields[name].Value).Value.Trim());
                                        dictTU.Add(name, ((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value.Trim());
                                    }
                                } else if (doc.AcroForm.Fields[name].Value is PdfItem) {
                                    //Console.WriteLine($"PdfString:{name}:{((PdfString)doc.AcroForm.Fields[name].Value).Value}:={((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value}");
                                    dictV.Add(name, ((PdfItem)doc.AcroForm.Fields[name].Value).ToString().Trim());
                                    dictTU.Add(name, ((PdfItem)doc.AcroForm.Fields[name]?.Elements["/TU"])?.ToString().Trim());
                                }
                            }
                        }
                    }
                }

                if (!CID.Equals(dictV["CID"])) {
                    result.ErrCode = "40001";
                    result.ErrMsg = "合約編號不一致，請確認";
                    return ToJsonContent(result, true);
                }

                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();

                try {

                    await SaveData(dictV, conn, tran);

                    Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
                    Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Security.Models.SysSetting>();

                    //DateTime dt = DateTime.Now;
                    //string FileName = dt.ToString("yyyyMMddHHmmss") + "_" + dictV["FName"];//filelist[0].FileName;
                    string FileName = CID;//filelist[0].FileName;
                    string typeName = GetTypeName(dictV["CType"].ToString());
                    string uploadPath = sysSetting.ChaochiFilepath + $"Contract/{typeName}/{cCate}/{CID}/主約/";

                    result.ResData = Utils.AddFile(filelist[0], uploadPath, FileName);

                    // 更新主約綁定狀態
                    //ContractAttachment attachment = new ContractAttachment();
                    //attachment.Id = Id;
                    //attachment.Status = "自動綁定";
                    //attachment.UploadTime = DateTime.Now;
                    //attachment.UploadUserId = CurrentUser.UserId;
                    //attachment.LastModifyTime = DateTime.Now;
                    //attachment.LastModifyUserId = CurrentUser.UserId;

                    //bool updateAttachmentStatusResult = await contractAttachmentService.UpdateAsync(attachment, Id);
                    //bool updateAttachmentStatusResult = await contractAttachmentService.UpdateTableFieldAsync("Status", "自動綁定", $"CID = {dictV["CID"]} AND Version = {dictV["Version"]}");
                    await contractAttachmentService.UpdateContractAttachmentStatus(CurrentUser.Account, CID, formCollection["CName"], "自動綁定", conn, tran);

                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("更新主約失敗", ex);
                    result.ErrMsg = "更新主約失敗";
                    result.ErrCode = ErrCode.err43002;
                    result.Success = false;
                    // 觸發ExceptionHandlingAttribute.OnException
                    throw;
                }

            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                if (ex.InnerException == null) {
                    throw;
                } else {
                    throw new Exception(ex.InnerException.Message);
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取主約必要附件
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMajorAttachments")]
        [YuebonAuthorize("List")]
        public async Task<CommonResult<List<ContractAttachmentOutputDto>>> GetMajorAttachments(string cid)
        {
            CommonResult<List<ContractAttachmentOutputDto>> result = new CommonResult<List<ContractAttachmentOutputDto>>();
            List<ContractAttachmentOutputDto> tempList = new List<ContractAttachmentOutputDto>();
            List<ContractAttachmentOutputDto> majorAttachmentInfoList = await contractAttachmentService.GetMajorByCID(cid);
            if (majorAttachmentInfoList != null && majorAttachmentInfoList.Count > 0) {                
                tempList.Add(majorAttachmentInfoList.Single(m => "主約".Equals(m.ArchiveTo)));
                tempList.AddRange(majorAttachmentInfoList.Where(m => !"主約".Equals(m.ArchiveTo)).OrderBy(m => m.FormName).ToList<ContractAttachmentOutputDto>());
            }

            result.ResData = tempList;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;            

            return result;
        }

        /// <summary>
        /// 獲取主約其他附件
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMinorAttachments")]
        [YuebonAuthorize("List")]
        public async Task<CommonResult<List<ContractAttachmentOutputDto>>> GetMinorAttachments(string cid)
        {
            CommonResult<List<ContractAttachmentOutputDto>> result = new CommonResult<List<ContractAttachmentOutputDto>>();
            List<ContractAttachmentOutputDto> minorAttachmentInfoList = await contractAttachmentService.GetMinorByCID(cid);
            if (minorAttachmentInfoList != null && minorAttachmentInfoList.Count > 0) {
                minorAttachmentInfoList = minorAttachmentInfoList.OrderBy(m => m.FormName).ToList<ContractAttachmentOutputDto>();
            }

            result.ResData = minorAttachmentInfoList;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;

            return result;
        }

        /// <summary>
        /// 獲取主約其他附件
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetContractHistory")]
        [YuebonAuthorize("List")]
        public async Task<CommonResult<List<HistoryFormContractOutputDto>>> GetContractHistory(string cid)
        {
            CommonResult<List<HistoryFormContractOutputDto>> result = new CommonResult<List<HistoryFormContractOutputDto>>();
            List<HistoryFormContractOutputDto> historyInfoList = await contractHistoryService.GetByCID(cid);

            result.ResData = historyInfoList;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;

            return result;
        }

        /// <summary>
        /// 根據合約編號獲取合約資訊
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        [HttpGet("GetContract")]
        [YuebonAuthorize("List")]
        public async Task<CommonResult<ContractOutputDto>> GetContract(string cid)
        {
            CommonResult<ContractOutputDto> result = new CommonResult<ContractOutputDto>();

            // 合約資訊
            ContractOutputDto info = await iService.GetContract(cid);

            if (info != null) {
                result.ErrCode = ErrCode.successCode;
                result.ResData = info;
            } else {
                result.ErrMsg = ErrCode.err50001;
                result.ErrCode = "50001";
            }
            return result;
        }

        /// <summary>
        /// 綁定主約必要附件
        /// </summary>
        /// <param name="input">合約編號</param>
        /// <returns></returns>
        [HttpPost("BindAttachment")]
        [YuebonAuthorize("Edit")]
        [NoSignRequired]
        public async Task<IActionResult> BindAttachment(ContractAttachmentInputDto input)
        {
            var result = new CommonResult();

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";

            //string status = await contractAttachmentService.GetStatusById(id);
            //if ("己綁定".Equals(status)) {
            //    result.ErrMsg = $"{formName}己綁定";
            //    return ToJsonContent(result);
            //}

            string typeName = GetTypeName(input.CType);
            string targetPath = @$"{path}Contract/{typeName}/{input.CCate}/{input.CID}/必要附件/";
            string fileExtension = Path.GetExtension(input.FileName).TrimStart('.').ToLowerInvariant();
            string target = @$"{targetPath}{input.FormName}.{fileExtension}";
            string source = string.Empty;

            switch (input.ArchiveTo) {
                case "建物":
                    //var fileName = historyFormBuildingService.Get(historyId).FileName;
                    //if (!formName.Equals(fileName)) {
                    //    result.ErrMsg = "綁定文件名稱不一致，請確認";
                    //    return ToJsonContent(result);
                    //} else {
                    source = @$"{path}historyPDF/Building/{input.ArchiveID}/{input.FileName}";

                    //}
                    break;
                case "出租人":
                    string type = string.Empty;
                    if (input.ArchiveID.Length == 10) {
                        type = "LN";
                    } else if (input.ArchiveID.Length == 8) {
                        type = "LC";
                    }
                    source = @$"{path}historyPDF/CustomerL/{type}/{input.ArchiveID}/{input.FileName}";
                    break;
                case "承租人":
                    type = string.Empty;
                    if (input.ArchiveID.Length == 10) {
                        type = "RN";
                    } else if (input.ArchiveID.Length == 8) {
                        type = "RC";
                    }
                    source = @$"{path}historyPDF/CustomerR/{type}/{input.ArchiveID}/{input.FileName}";
                    break;
            }

            if (!Directory.Exists(targetPath)) {
                Directory.CreateDirectory(targetPath);
            }

            System.IO.File.Copy(source, target, true);

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();

            try {

                // 更新綁定狀態
                bool updateAttachmentStatusResult = await contractAttachmentService.UpdateContractAttachmentStatus(CurrentUser.Account, input.CID, input.FormName, "已綁定");

                if (updateAttachmentStatusResult) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    result.ErrCode = ErrCode.err43002;
                    result.ErrMsg = "綁定失敗";
                    result.Success = false;
                }

            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("綁定失敗", ex);
                result.ErrMsg = "綁定失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 清空綁定主約必要附件
        /// </summary>
        /// <param name="input">合約編號</param>
        /// <returns></returns>
        [HttpPost("UnbindAttachment")]
        [YuebonAuthorize("Edit")]
        [NoSignRequired]
        public async Task<IActionResult> UnbindAttachment(ContractAttachmentInputDto input)
        {
            var result = new CommonResult();

            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";

            string typeName = GetTypeName(input.CType);
            string targetPath = @$"{path}Contract/{typeName}/{input.CCate}/{input.CID}/必要附件/";
            string targetFile = Directory.EnumerateFiles(targetPath, $"{input.FormName}.*", SearchOption.AllDirectories).FirstOrDefault();
            string fileExtension = Path.GetExtension(targetFile).TrimStart('.').ToLowerInvariant();
            string target = @$"{targetPath}{input.FormName}.{fileExtension}";
            System.IO.File.Delete(target);

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();

            try {

                // 更新綁定狀態
                bool updateAttachmentStatusResult = await contractAttachmentService.UpdateContractAttachmentStatus("", input.CID, input.FormName, "未綁定");

                if (updateAttachmentStatusResult) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    result.ErrMsg = "清空綁定失敗";
                    result.ErrCode = ErrCode.err43002;
                    result.Success = false;
                }

            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("清空綁定失敗", ex);
                result.ErrMsg = "清空綁定失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 合約初審
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("InitialContract")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> InitialContract(ContractInputDto input)
        {
            var result = new CommonResult();

            // 續約(新約起日晚於舊約(效期中)迄日)
            //if (!string.IsNullOrEmpty(input.BAdd) && !string.IsNullOrEmpty(input.ContractDate) && !string.IsNullOrEmpty(input.BRDStart)) {
            if (!string.IsNullOrEmpty(input.BAdd) && !string.IsNullOrEmpty(input.BRDStart)) {
                var existContract = await iService.GetExistContractByBAdd(input.BAdd);
                if(existContract != null && !string.IsNullOrEmpty(existContract.BRDTEnd)) {
                    CultureInfo culture = new CultureInfo("zh-TW");
                    culture.DateTimeFormat.Calendar = new TaiwanCalendar();

                    DateTime existContractEndDate = DateTime.Parse(existContract.BRDTEnd, culture);
                    DateTime contractEffiectiveDate = DateTime.Parse(input.BRDStart, culture);
                    int timeCompareResult = DateTime.Compare(contractEffiectiveDate, existContractEndDate);

                    if(timeCompareResult <= 0) {                        
                        result.Success = false;
                        result.ErrMsg = "新約的起租日早於舊約退租日，無法續約";
                        return ToJsonContent(result);
                    }
                }
            }
            
            List<BLOB> blobList = new List<BLOB>();

            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Security.Models.SysSetting>();
            var mainPath = $"{sysSetting.ChaochiFilepath}";
            string typeName = GetTypeName(input.CType);
            string cs1Path = $"{mainPath}Contract/{typeName}/{input.CCate}/{input.CID}/主約/";
            string majorAttachmentPath = $"{mainPath}Contract/{typeName}/{input.CCate}/{input.CID}/必要附件/";
            string minorAttachmentPath = $"{mainPath}Contract/{typeName}/{input.CCate}/{input.CID}/次要附件/";


                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();

                long historyCount = 0L;

            try {
                int maxVersion = await contractHistoryService.GetMaxVersionByCID(input.CID);
                int version = (maxVersion == 0) ? 1 : ("備審未簽名".Equals(input.CStatus)) ? maxVersion + 1 : maxVersion;
                string note = (version > 1) ? input.Note : "初版";

                string userName = string.Empty;
                // 歷史版本
                ContractOutputDto contractInfo = await iService.GetByCID(input.CID);
                if (contractInfo != null) {
                    HistoryFormContractOutputDto historyInfo = await contractHistoryService.GetByVersion(input.CID, version.ToString());
                    if (historyInfo == null) {
                        Security.Models.User user = await userService.GetUserByLogin(CurrentUser.Account);
                        userName = (user != null) ? user.RealName : string.Empty;
                        HistoryFormContract contractHistory = new HistoryFormContract();
                        contractHistory.CID = input.CID;
                        contractHistory.Version = version.ToString();
                        contractHistory.UploadTime = DateTime.Now;
                        contractHistory.UploadUser = userName;
                        contractHistory.CreatorTime = DateTime.Now;
                        contractHistory.CreatorUserId = CurrentUser.UserId;
                        contractHistory.Note = note;
                        contractHistory.CName = contractInfo.CName;

                        historyCount = await contractHistoryService.InsertAsync(contractHistory, conn, tran);
                    }
                }

                // 主約
                DirectoryInfo cs1Dir = new DirectoryInfo(cs1Path);
                FileInfo[] cs1FileInfo = cs1Dir.GetFiles();
                foreach (var cs1File in cs1FileInfo) {
                    //string path = $"{cs1Path}{cs1File.Name}";
                    BLOB blob = setBlobs(cs1File.FullName);
                    blobList.Add(blob);
                }

                // 要件附件
                DirectoryInfo majorAttachmentDir = new DirectoryInfo(majorAttachmentPath);
                if (majorAttachmentDir.Exists) {
                    FileInfo[] majorAttachmentInfo = majorAttachmentDir.GetFiles().OrderBy(x => x.Name).ToArray();
                    foreach (var majorAttachment in majorAttachmentInfo) {
                        //string path = $"{cs1Path}{cs1File.Name}";
                        BLOB blob = setBlobs(majorAttachment.FullName);
                        blobList.Add(blob);
                    }
                }

                // 次要附件
                DirectoryInfo minorAttachmentDir = new DirectoryInfo(minorAttachmentPath);
                if (minorAttachmentDir.Exists) {
                    FileInfo[] minorAttachmentInfo = minorAttachmentDir.GetFiles().OrderBy(x => x.Name).ToArray();
                    if (minorAttachmentInfo != null && minorAttachmentInfo.Length > 0) {
                        foreach (var minorAttachment in minorAttachmentInfo) {
                            BLOB blob = setBlobs(minorAttachment.FullName);
                            blobList.Add(blob);
                        }
                    }
                }

                PdfMerger merger = new PdfMerger();
                byte[] mergedContent = merger.Create(blobList);

                string finalizedPath = $"{mainPath}Contract/{typeName}/{input.CCate}/{input.CID}/備審未簽名/";

                //string finalizedPath = $"{mainPath}Contract/{typeName}/{input.CCate}/{input.CID}/備審未簽名/";
                if (!Directory.Exists(finalizedPath)) {
                    Directory.CreateDirectory(finalizedPath);
                }
                System.IO.File.WriteAllBytes($"{finalizedPath}{input.CID}_{version}.pdf", mergedContent);

                // 加上頁數
                merger.AddPageNumberToPDF($"{finalizedPath}{input.CID}_{version}.pdf", true);

                string flowID = $"{input.CID}-{version}";
                // 待辦事項
                Security.Models.User auditor = await userService.GetUserByLogin(input.Sales);
                string auditorEmailAddress = (auditor != null) ? auditor.Email : string.Empty;
                ToDoList info = new ToDoList();
                info.Type = "合約";
                info.TypeId = flowID;
                info.TypeData = input.CStatus;
                info.Name = $"[簽核]*合約管理【申請人：{userName}(內勤)，物件編號：{input.CCObjectNo}，簽核結果：待簽核】";
                info.Status = "待審核";
                info.Account = input.Sales;
                info.Memo = input.Comment;
                info.CreatorTime = DateTime.Now;
                info.CreatorUserId = CurrentUser.UserId;
                long todoCount = await todoListService.InsertAsync(info, conn, tran);

                // 審核記錄
                ContractFlowLog logInfo = new ContractFlowLog();
                logInfo.CID = flowID;
                logInfo.CStatus = input.CStatus;
                logInfo.Applicant = userName;
                logInfo.Auditor = input.SalesName;
                logInfo.Action = "送審";
                logInfo.Comment = input.Comment;
                logInfo.ApplyTime = DateTime.Now;
                logInfo.CreatorTime = DateTime.Now;
                logInfo.CreatorUserId = CurrentUser.UserId;
                long flowLogCount = await contractFlowLogService.InsertAsync(logInfo, conn, tran);

                // 更新合約最新歷史版本
                bool updateVersionResult = await iService.UpdateContractVersion(CurrentUser.UserId, input.CID, version.ToString(), conn, tran);

                if (historyCount > 0 && todoCount > 0 && flowLogCount > 0 && updateVersionResult) {
                    eftran.Commit();
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;

                    if (!string.IsNullOrEmpty(auditorEmailAddress)) {
                        bool sendResult = ContractService.sendMail(input.CID, version.ToString(), input.CCObjectNo, input.BAdd, userName, "Sales", auditorEmailAddress);
                    }
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("合約定稿失敗", ex);
                result.ErrMsg = "合約定稿失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 刪除合約次要附件
        /// </summary>
        /// <param name="input">次要附件編號</param>
        /// <returns></returns>
        [HttpPost("DeleteMinorAttachment")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> DeleteMinorAttachment(ContractInputDto input)
        {
            var result = new CommonResult();

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";

            string typeName = GetTypeName(input.CType);
            string targetPath = @$"{path}Contract/{typeName}/{input.CCate}/{input.CID}/次要附件/";
            string target = @$"{targetPath}{input.AttachmentFormName}";
            System.IO.File.Delete(target);

            ContractAttachment deleteInfo = new ContractAttachment();
            deleteInfo.Id = input.AID;
            deleteInfo.CID = input.CID;
            deleteInfo.FormName = input.AttachmentFormName;

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {

                bool deleteMinorAttachmentResult = await contractAttachmentService.DeleteAsync(deleteInfo, tran);
                //bool updateAttachmentStatusResult = await contractAttachmentService.UpdateContractAttachmentStatus("", cid, formName, "未綁定");

                if (deleteMinorAttachmentResult) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    result.ErrCode = ErrCode.err43003;
                    result.ErrMsg = "次要附件刪除失敗";
                    result.Success = false;
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("次要附件刪除失敗", ex);
                result.ErrMsg = "次要附件刪除失敗";
                result.ErrCode = ErrCode.err43003;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 下載定稿合約
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DownloadFinalizedPDF")]
        [YuebonAuthorize("Download")]
        public async Task<IActionResult> DownloadFinalizedPDF(ContractInputDto input)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            string typeName = GetTypeName(input.CType);
            string finalizedContractPath = $"{sysSetting.ChaochiFilepath}Contract/{typeName}/{input.CCate}/{input.CID}/備審未簽名/{input.CID}_{input.Version}.pdf";

            //https://stackoverflow.com/questions/34131326/using-mimemapping-in-asp-net-core
            //string contentType;
            //new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
            //return File(PDFstream, contentType ?? "image/jpeg");
            return File(new FileStream(finalizedContractPath, FileMode.Open, FileAccess.Read), "application/pdf");
        }

        /// <summary>
        /// 下載定稿合約
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DownloadSignedContract")]
        [YuebonAuthorize("Download")]
        public async Task<IActionResult> DownloadSignedContract(ContractInputDto input)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            string typeName = GetTypeName(input.CType);
            string finalizedContractPath = $"{sysSetting.ChaochiFilepath}Contract/{typeName}/{input.CCate}/{input.CID}/已簽名待放行/{input.CID}_{input.Version}.pdf";

            //https://stackoverflow.com/questions/34131326/using-mimemapping-in-asp-net-core
            //string contentType;
            //new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
            //return File(PDFstream, contentType ?? "image/jpeg");
            return File(new FileStream(finalizedContractPath, FileMode.Open, FileAccess.Read), "application/pdf");
        }

        /// <summary>
        /// 合約生效
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ContractEffiective")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> ContractEffiective(ContractInputDto input)
        {
            var result = new CommonResult();
            //string typeName = GetTypeName(input.CType);
            //string targetPath = @$"{path}Contract/{typeName}/{input.CCate}/{input.CID}/{input.Version}/必要附件/";
            //string target = @$"{targetPath}{formName}.pdf";
            //string source = @$"{path}historyPDF/Building/{archiveId}/{fileName}";
            //if (!System.IO.Directory.Exists(targetPath)) {
            //    System.IO.Directory.CreateDirectory(targetPath);
            //}

            //System.IO.File.Copy(source, target, true);

            // 合約起始日期大於系統日期，合約狀態為「已簽約未生效」;小於等於系統日期，合約狀態為「合約已生效」
            if (!string.IsNullOrWhiteSpace(input.ContractDate)) {
                DateTime today = DateTime.Now;
                CultureInfo culture = new CultureInfo("zh-TW");
                culture.DateTimeFormat.Calendar = new TaiwanCalendar();
                DateTime contractEffiectiveDate = DateTime.Parse(input.ContractDate, culture);
                int timeCompareResult = DateTime.Compare(contractEffiectiveDate, today);

                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();

                try {
                    // 更新合約狀態
                    bool updateContractStatusResult = (timeCompareResult <= 0) ? await iService.UpdateContractStatus(CurrentUser.UserId, input.CID, "效期中", "", conn, tran) : await iService.UpdateContractStatus(CurrentUser.UserId, input.CID, "待生效", "", conn, tran);

                    if (updateContractStatusResult) {
                        eftran.Commit();
                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;
                    } else {
                        result.ErrCode = ErrCode.err43002;
                        result.ErrMsg = "合約無法生效";
                        result.Success = false;
                    }

                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("合約無法生效", ex);
                    result.ErrMsg = "合約無法生效";
                    result.ErrCode = ErrCode.err43002;
                    // 觸發ExceptionHandlingAttribute.OnException
                    throw;
                }

            } else {
                result.ErrCode = ErrCode.err40110;
                result.ErrMsg = "合約生效日格式錯誤";
                result.Success = false;
            }



            return ToJsonContent(result);
        }

        /// <summary>
        /// 上傳最新版掃描檔
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost("UploadSignedContract")]
        [YuebonAuthorize("Edit")]
        [NoSignRequired]
        public async Task<IActionResult> UploadSignedContract([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;

            try {
                string Id = string.Empty;
                if (formCollection.ContainsKey("Id")) {
                    Id = formCollection["Id"];
                }

                string CID = string.Empty;
                if (formCollection.ContainsKey("CID")) {
                    CID = formCollection["CID"];
                }

                string version = string.Empty;
                if (formCollection.ContainsKey("Version")) {
                    version = formCollection["Version"];
                }

                string cCate = string.Empty;
                if (formCollection.ContainsKey("CCate")) {
                    cCate = formCollection["CCate"];
                }

                string cType = string.Empty;
                if (formCollection.ContainsKey("CType")) {
                    cType = formCollection["CType"];
                }

                string sales = string.Empty;
                if (formCollection.ContainsKey("Sales")) {
                    sales = formCollection["Sales"];
                }

                string needSignOnline = string.Empty;
                if (formCollection.ContainsKey("NeedSignOnline")) {
                    needSignOnline = formCollection["NeedSignOnline"];
                }

                if (filelist == null || filelist.Count <= 0 || filelist.Count >= 2) {
                    result.ErrCode = "40001";
                    result.ErrMsg = "一次只能上傳一份PDF";
                    return ToJsonContent(result, true);
                }

                //Dictionary<string, object> dictV = new Dictionary<string, object>();
                //Dictionary<string, string> dictTU = new Dictionary<string, string>();
                //IFormFile file = filelist[0];
                //if (file != null && file.Length > 0) {
                //    using (var doc = PdfReader.Open(file.OpenReadStream(), PdfDocumentOpenMode.Modify)) {
                //        if (doc.AcroForm == null) {
                //            doc.Close();
                //            result.ErrCode = "40001";
                //            result.ErrMsg = "上傳PDF不含任何標籤";
                //            return ToJsonContent(result, true);
                //        } else {
                //            //取值
                //            foreach (var name in doc.AcroForm.Fields.Names) {
                //                //Console.WriteLine($"{name}:{doc.AcroForm.Fields[name].Value}");
                //                if (doc.AcroForm.Fields[name].Value is PdfString) {
                //                    if (doc.AcroForm.Fields[name].Value.ToString() == "<FEFF>") {
                //                        //Console.WriteLine($"PdfString:{name} is <FEFF>:={((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value}");
                //                        dictV.Add(name, "");
                //                        dictTU.Add(name, ((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value.Trim());
                //                    } else {
                //                        //Console.WriteLine($"PdfString:{name}:{((PdfString)doc.AcroForm.Fields[name].Value).Value}:={((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value}");
                //                        dictV.Add(name, ((PdfString)doc.AcroForm.Fields[name].Value).Value.Trim());
                //                        dictTU.Add(name, ((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value.Trim());
                //                    }
                //                } else if (doc.AcroForm.Fields[name].Value is PdfItem) {
                //                    //Console.WriteLine($"PdfString:{name}:{((PdfString)doc.AcroForm.Fields[name].Value).Value}:={((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value}");
                //                    dictV.Add(name, ((PdfItem)doc.AcroForm.Fields[name].Value).ToString().Trim());
                //                    dictTU.Add(name, ((PdfItem)doc.AcroForm.Fields[name]?.Elements["/TU"])?.ToString().Trim());
                //                }
                //            }
                //        }
                //    }
                //}

                //if (!CID.Equals(dictV["CID"])) {
                //    result.ErrCode = "40001";
                //    result.ErrMsg = "合約編號不一致，請確認";
                //    return ToJsonContent(result, true);
                //    //} else {
                //    //    if (!version.Equals(dictV["Version"])) {
                //    //        result.ErrCode = "40001";
                //    //        result.ErrMsg = "合約版本不一致，請確認";
                //    //        return ToJsonContent(result, true);
                //    //    }
                //}

                //await SaveData(dictV);

                YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Security.Models.SysSetting>();

                //DateTime dt = DateTime.Now;
                //string FileName = dt.ToString("yyyyMMddHHmmss") + "_" + dictV["FName"];//filelist[0].FileName;
                string FileName = $"{CID}_{version}";//filelist[0].FileName;
                string typeName = GetTypeName(cType);
                string uploadPath = sysSetting.ChaochiFilepath + $"Contract/{typeName}/{cCate}/{CID}/已簽名待放行/";

                result.ResData = Utils.AddFile(filelist[0], uploadPath, FileName);

                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();

                try {
                    // 更新最新版掃描檔路徑
                    bool updateSignedContractPathResult = await contractHistoryService.UpdateSignedContractPath(CurrentUser.UserId, CID, version, uploadPath, conn, tran);

                    //// 更新合約狀態
                    ////string status = ("Y".Equals(needSignOnline)) ? "已簽約待審核" : "已簽約待進版";
                    //bool updateAttachmentStatusResult = await iService.UpdateContractStatus(CurrentUser.UserId, CID, "已簽名待放行", "", conn, tran);

                    //string flowID = $"{CID}-{version}";

                    //Security.Models.User user = await userService.GetUserByLogin(CurrentUser.Account);
                    //string userName = (user != null) ? user.RealName : string.Empty;
                    //string storeManagerId = yuebonCacheHelper.Get<string>("User_StoreManagerID" + CurrentUser.UserId);
                    ////string storeManagerId = userService.GetStoreManager(sales);
                    //string storeManagerName = string.Empty;
                    //string storeManagerEmailAddress = string.Empty;
                    //if (!string.IsNullOrEmpty(storeManagerId)) {
                    //    user = await userService.GetUserByLogin(storeManagerId);
                    //    storeManagerName = (user != null) ? user.RealName : string.Empty;
                    //    storeManagerEmailAddress = (user != null) ? user.Email : string.Empty;
                    //}

                    //// 待辦事項
                    //ToDoList info = new ToDoList();
                    //info.Type = "合約";
                    //info.TypeId = flowID;
                    //info.TypeData = status;
                    //info.Name = $"[簽核]*合約管理【申請人：{userName}(內勤)，物件編號：{CCObjectNo}，簽核結果：待簽核】";
                    //info.Status = "待審核";
                    //info.Account = storeManagerId;
                    //info.Memo = comment;
                    //info.CreatorTime = DateTime.Now;
                    //info.CreatorUserId = CurrentUser.UserId;
                    //long todoCount = await todoListService.InsertAsync(info, conn, tran);

                    //// 審核記錄
                    //ContractFlowLog logInfo = new ContractFlowLog();
                    //logInfo.CID = flowID;
                    //logInfo.CStatus = status;
                    //logInfo.Applicant = userName;
                    //logInfo.Auditor = storeManagerName;
                    //logInfo.Action = "送審";
                    //logInfo.Comment = comment;
                    //logInfo.ApplyTime = DateTime.Now;
                    //logInfo.CreatorTime = DateTime.Now;
                    //logInfo.CreatorUserId = CurrentUser.UserId;
                    //long flowLogCount = await contractFlowLogService.InsertAsync(logInfo, conn, tran);

                    //if (updateSignedContractPathResult && updateAttachmentStatusResult && todoCount > 0 && flowLogCount > 0) {
                    if (updateSignedContractPathResult) {
                        eftran.Commit();
                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;                    
                    }
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("合約無法生效", ex);
                    result.ErrMsg = "合約無法生效";
                    result.ErrCode = ErrCode.err43002;
                    // 觸發ExceptionHandlingAttribute.OnException
                    throw;
                }
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                if (ex.InnerException == null) {
                    throw;
                } else {
                    throw new Exception(ex.InnerException.Message);
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 將[己簽名待放行]的合約送審店長
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("SubmitSignedContract")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> SubmitSignedContract(ContractInputDto input)
        {
            CommonResult result = new CommonResult();

            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Security.Models.SysSetting>();

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                string CID = input.CID;
                string version = input.Version;
                string status = input.CStatus;
                string comment = input.Comment;

                

                // 更新合約簽約日
                bool updateContractDateResult = await iService.UpdateContractDate(CurrentUser.UserId, CID, input.ContractDate, conn, tran);

                // 更新合約狀態
                bool updateAttachmentStatusResult = await iService.UpdateContractStatus(CurrentUser.UserId, CID, "已簽名待放行", "", conn, tran);

                string flowID = $"{CID}-{version}";

                Security.Models.User user = await userService.GetUserByLogin(CurrentUser.Account);
                string userName = (user != null) ? user.RealName : string.Empty;
                //string storeManagerId = yuebonCacheHelper.Get<string>("User_StoreManagerID" + CurrentUser.UserId);
                string storeManagerId = CurrentUser.ManagerId;
                //string storeManagerId = userService.GetStoreManager(sales);
                string storeManagerName = string.Empty;
                string storeManagerEmailAddress = string.Empty;
                if (!string.IsNullOrEmpty(storeManagerId)) {
                    user = await userService.GetUserByLogin(storeManagerId);
                    storeManagerName = (user != null) ? user.RealName : string.Empty;
                    storeManagerEmailAddress = (user != null) ? user.Email : string.Empty;
                }

                // 待辦事項
                ToDoList info = new ToDoList();
                info.Type = "合約";
                info.TypeId = flowID;
                info.TypeData = status;
                info.Name = $"[簽核]*合約管理【申請人：{userName}(內勤)，物件編號：{input.CCObjectNo}，簽核結果：待簽核】";
                info.Status = "待審核";
                info.Account = storeManagerId;
                info.Memo = comment;
                info.CreatorTime = DateTime.Now;
                info.CreatorUserId = CurrentUser.UserId;
                long todoCount = await todoListService.InsertAsync(info, conn, tran);

                // 審核記錄
                ContractFlowLog logInfo = new ContractFlowLog();
                logInfo.CID = flowID;
                logInfo.CStatus = status;
                logInfo.Applicant = userName;
                logInfo.Auditor = storeManagerName;
                logInfo.Action = "送審";
                logInfo.Comment = comment;
                logInfo.ApplyTime = DateTime.Now;
                logInfo.CreatorTime = DateTime.Now;
                logInfo.CreatorUserId = CurrentUser.UserId;
                long flowLogCount = await contractFlowLogService.InsertAsync(logInfo, conn, tran);

                if (updateAttachmentStatusResult && updateContractDateResult && todoCount > 0 && flowLogCount > 0) {
                    eftran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;

                    bool sendResult = ContractService.sendMail(CID, version, input.CCObjectNo, input.BAdd, userName, "Manager", storeManagerEmailAddress);
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("[已簽名待放行]的合約送審失敗", ex);
                result.ErrMsg = "[已簽名待放行]的合約送審失敗";
                result.ErrCode = ErrCode.err43002;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }


        /// <summary>
        /// 合約作廢
        /// </summary>
        /// <param name="input"></param>
        [HttpPost("ContractInvalid")]
        [YuebonAuthorize("Invalid")]
        public async Task<IActionResult> ContractInvalid(ContractInputDto input)
        {
            CommonResult result = new CommonResult();
            string bId = string.Empty;

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {                
                if (!input.BAdd.Contains(";") && !input.BAdd.Contains("\n")) {
                    bId = buildingService.GetIdByBAdd(input.BAdd);

                    // 建物狀態需改為[待招租]
                    bool updateBStateResult = await buildingService.UpdateBState(CurrentUser.UserId, bId, "待招租", conn, tran).ConfigureAwait(false);

                    string where = $"CID = '{input.CID}'";
                    bool terminateContractResult = await iService.UpdateTableFieldAsync("CStatus", "已作廢", where, conn, tran);

                    if (updateBStateResult && terminateContractResult) {
                        eftran.Commit();
                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;
                    } else {
                        result.Success = false;
                        result.ErrMsg = "合約作廢失敗";
                        result.ErrCode = ErrCode.err43002;
                    }
                } else {
                    result.Success = false;
                    result.ErrMsg = "合約作廢失敗：建物地址有誤，無法將狀態改為[待招租]";
                    result.ErrCode = ErrCode.err43002;
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("合約作廢失敗", ex);
                result.ErrMsg = "合約作廢失敗";
                result.ErrCode = ErrCode.err43002;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步批次物理刪除
        /// </summary>
        /// <param name="input"></param>
        [HttpDelete("DeleteBatchAsync")]
        [YuebonAuthorize("Delete")]
        public override async Task<IActionResult> DeleteBatchAsync(DeletesInputDto input)
        {
            CommonResult result = new CommonResult();
            string where = "id in ('" + input.Ids.Join(",").Trim(',').Replace(",", "','") + "')";

            if (!string.IsNullOrEmpty(where)) {
                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();

                try {
                    YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                    Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Security.Models.SysSetting>();
                    var path = $"{sysSetting.ChaochiFilepath}";
                    IEnumerable<Contract> clist = await iService.GetListWhereAsync(where);

                    bool cl = false;
                    bool cre = false;
                    bool cb = false;
                    bool ca = false;
                    bool cr = false;
                    bool ch = false;
                    // 刪除實體檔案
                    if (clist.Count() > 0) {
                        List<Contract> list = clist.ToList();
                        foreach (Contract contract in list) {
                            string whereCID = $"CID = '{contract.CID}'";
                            cl = await contractLandlordService.DeleteBatchWhereAsync(whereCID, conn, tran).ConfigureAwait(false);
                            cre = await contractRenterService.DeleteBatchWhereAsync(whereCID, conn, tran).ConfigureAwait(false);
                            cb = await contractBuildingService.DeleteBatchWhereAsync(whereCID, conn, tran).ConfigureAwait(false);
                            ca = await contractAttachmentService.DeleteBatchWhereAsync(whereCID, conn, tran).ConfigureAwait(false);
                            cr = await contractRelevantService.DeleteBatchWhereAsync(whereCID, conn, tran).ConfigureAwait(false);
                            ch = await contractHistoryService.DeleteBatchWhereAsync(whereCID, conn, tran).ConfigureAwait(false);


                            string typeName = GetTypeName(contract.CType);
                            string targetDir = @$"{path}Contract/{typeName}/{contract.CCate}/{contract.CID}/";
                            var directory = new DirectoryInfo(targetDir);
                            if (directory.Exists) {
                                Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(targetDir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents);
                            }
                        }
                    }

                    bool bl = await iService.DeleteBatchWhereAsync(where, conn, tran).ConfigureAwait(false);


                    if (cl && cre && cb && ca && cr && ch && bl) {
                        eftran.Commit();

                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;
                    } else {
                        result.ErrCode = "43003";
                        result.ErrMsg = "刪除合約失敗";
                        result.Success = false;
                    }
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("刪除合約失敗", ex);
                    result.ErrMsg = "刪除合約失敗";
                    result.ErrCode = ErrCode.err43003;
                    result.Success = false;
                    // 觸發ExceptionHandlingAttribute.OnException
                    throw;
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 更新媒合編號
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="mid"></param>
        [HttpPost("UpdateMID")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> UpdateMID(string cid, string mid)
        {
            CommonResult result = new CommonResult();

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                await iService.UpdateTableFieldAsync("MID", mid, $"CID = '{cid}'", conn, tran);

                eftran.Commit();
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
                result.Success = true;
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("更新媒合編號失敗", ex);
                result.ErrMsg = "更新媒合編號失敗";
                result.ErrCode = ErrCode.err43002;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 根據出租建物地址查詢分租物件單號
        /// </summary>
        /// <param name="BAdd">出租建物地址</param>
        [HttpPost("GetMOIDByBAdd")]
        [YuebonAuthorize("Add")]
        public async Task<IActionResult> GetMOIDByBAdd(string BAdd)
        {
            CommonResult result = new CommonResult();
            MOBuildingOutputDto mobInfo = await moBuildingService.GetByBAdd(BAdd);
            string moid = (mobInfo != null) ? mobInfo.MOID : string.Empty ;
            MOOutputDto moInfo = await moService.GetByMOID(moid);

            result.ResData = moInfo;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;

            return ToJsonContent(result);
        }

        /// <summary>
        /// 合約解約送審(固定送主管)
        /// </summary>
        /// <param name="input">合約資料</param>
        [HttpPost("SubmitTerminateContract")]
        [YuebonAuthorize("Terminate")]
        public async Task<IActionResult> SubmitTerminateContract(ContractInputDto input)
        {
            CommonResult result = new CommonResult();

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            //string storeManagerId = yuebonCacheHelper.Get<string>("User_StoreManagerID" + CurrentUser.UserId);
            string storeManagerId = CurrentUser.ManagerId;

            if ("root".Equals(storeManagerId)) {
                result.Success = false;
                result.ErrMsg = $"審核者不存在(解約)";
                return ToJsonContent(result);
            } else {
                //string flowID = $"{input.CID}-{input.Version}";
                string flowID = $"{input.CID}";
                Security.Models.User user = await userService.GetUserByLogin(CurrentUser.Account);
                string userName = (user != null) ? user.RealName : string.Empty;
                Security.Models.User storeManager = await userService.GetUserByLogin(storeManagerId);
                string storeManagerName = (storeManager != null) ? storeManager.RealName : string.Empty;
                string storeManagerEmail = (storeManager != null) ? storeManager.Email : string.Empty;

                ToDoList info = new ToDoList();
                info.Type = "合約";
                info.TypeId = flowID;
                //info.TypeData = input.CStatus;
                info.TypeData = "解約";
                info.Name = userName;
                info.Name = $"[簽核]*合約管理【申請人：{userName}(內勤)，物件編號：{input.CCObjectNo}，簽核結果：待簽核】";
                info.Status = "待審核";
                info.Account = storeManagerId;
                info.Memo = input.Comment;
                info.CreatorTime = DateTime.Now;
                info.CreatorUserId = CurrentUser.UserId;

                // 審核記錄
                ContractFlowLog logInfo = new ContractFlowLog();
                logInfo.CID = flowID;
                logInfo.CStatus = input.CStatus;
                logInfo.Applicant = userName;
                logInfo.Auditor = storeManagerName;
                logInfo.Action = "送審";
                logInfo.Comment = input.Comment;
                logInfo.ApplyTime = DateTime.Now;
                logInfo.CreatorTime = DateTime.Now;
                logInfo.CreatorUserId = CurrentUser.UserId;                

                try {
                    long ln = await todoListService.InsertAsync(info, conn, tran);

                    long flowLogCount = await contractFlowLogService.InsertAsync(logInfo, conn, tran);

                    if (ln > 0 && flowLogCount > 0) {
                        eftran.Commit();
                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;

                        bool sendResult = ContractService.sendMail(input.CID, input.Version, input.CCObjectNo, input.BAdd, userName, "Manager", storeManagerEmail);
                    } else {
                        result.ErrMsg = "合約解約送審失敗";
                        result.ErrCode = ErrCode.err43001;
                    }
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("合約解約送審失敗", ex);
                    result.ErrMsg = "合約解約送審失敗";
                    result.ErrCode = ErrCode.err43001;
                    // 觸發ExceptionHandlingAttribute.OnException
                    throw;
                }
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 設定二進位物件
        /// </summary>
        /// <param name="path"></param>
        private BLOB setBlobs(string path)
        {
            BLOB blob = new BLOB();
            byte[] content = System.IO.File.ReadAllBytes(path);
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(path, out contentType);

            if (!string.IsNullOrWhiteSpace(contentType)) {
                blob.ContentType = contentType;
                blob.Content = content;
            }

            return blob;
        }

        /// <summary>
        /// 儲存PDF資料
        /// </summary>
        /// <param name="dictV"></param>
        /// <param name="conn"></param>
        /// <param name="tran"></param>
        private async Task SaveData(Dictionary<string, object> dictV, IDbConnection conn = null, IDbTransaction tran = null)
        {
            ContractPDFDataModel PDFDataModel_NEW = Merger.DictCloneAndMerge<ContractPDFDataModel>(new ContractPDFDataModel(), dictV);

            //string newId = GuidUtils.CreateNo();
            string CreatorUserId = CurrentUser?.UserId;
            DateTime CreatorTime = DateTime.Now;
            string LastModifyUserId = CurrentUser?.UserId;
            DateTime LastModifyTime = CreatorTime;

            //// 建物門牌
            //PDFDataModel_NEW.BAdd =
            //    Utils.memergeAddFix(
            //       PDFDataModel_NEW.BAdd_1,
            //       PDFDataModel_NEW.BAdd_1_1,
            //       PDFDataModel_NEW.BAdd_1_2,
            //       PDFDataModel_NEW.BAdd_2,
            //       PDFDataModel_NEW.BAdd_2_1,
            //       PDFDataModel_NEW.BAdd_2_2,
            //       PDFDataModel_NEW.BAdd_2_3,
            //       PDFDataModel_NEW.BAdd_2_4,
            //       PDFDataModel_NEW.BAdd_3,
            //       PDFDataModel_NEW.BAdd_3_1,
            //       PDFDataModel_NEW.BAdd_3_2,
            //       PDFDataModel_NEW.BAdd_4,
            //       PDFDataModel_NEW.BAdd_5,
            //       PDFDataModel_NEW.BAdd_6,
            //       PDFDataModel_NEW.BAdd_7,
            //       PDFDataModel_NEW.BAdd_8,
            //       PDFDataModel_NEW.BAdd_9
            //    );

            // 簽約日
            PDFDataModel_NEW.ContractDate = $"{PDFDataModel_NEW.Contract_Y}-{PDFDataModel_NEW.Contract_M}-{PDFDataModel_NEW.Contract_D}";
            if (PDFDataModel_NEW.ContractDate == "--") {
                PDFDataModel_NEW.ContractDate = "";
            }

            // 租賃期間
            PDFDataModel_NEW.BRDStart = $"{PDFDataModel_NEW.BRDStart_Y}-{PDFDataModel_NEW.BRDStart_M}-{PDFDataModel_NEW.BRDStart_D}";
            if (PDFDataModel_NEW.BRDStart == "--") {
                PDFDataModel_NEW.BRDStart = "";
            }
            PDFDataModel_NEW.BRDTEnd = $"{PDFDataModel_NEW.BRDTEnd_Y}-{PDFDataModel_NEW.BRDTEnd_M}-{PDFDataModel_NEW.BRDTEnd_D}";
            if (PDFDataModel_NEW.BRDTEnd == "--") {
                PDFDataModel_NEW.BRDTEnd = "";
            }

            //// 建物地址
            //PDFDataModel_NEW.BAdd =
            //    Utils.memergeAddFix(
            //       PDFDataModel_NEW.BAdd_1,
            //       PDFDataModel_NEW.BAdd_1_1,
            //       PDFDataModel_NEW.BAdd_1_2,
            //       PDFDataModel_NEW.BAdd_2,
            //       PDFDataModel_NEW.BAdd_2_1,
            //       PDFDataModel_NEW.BAdd_2_2,
            //       PDFDataModel_NEW.BAdd_2_3,
            //       PDFDataModel_NEW.BAdd_2_4,
            //       PDFDataModel_NEW.BAdd_3,
            //       PDFDataModel_NEW.BAdd_3_1,
            //       PDFDataModel_NEW.BAdd_3_2,
            //       PDFDataModel_NEW.BAdd_4,
            //       PDFDataModel_NEW.BAdd_5,
            //       PDFDataModel_NEW.BAdd_6,
            //       PDFDataModel_NEW.BAdd_7,
            //       PDFDataModel_NEW.BAdd_8,
            //       PDFDataModel_NEW.BAdd_9
            //    );

            Contract contract = PDFDataModel_NEW.MapTo<Contract>();

            Contract contract_DB = await iService.GetByCIDRaw(contract.CID);

            string id = contract_DB.Id;
            //Contract contract_NEW = contract_DB.MapTo(contract);
            Contract contract_NEW = Merger.CloneAndMerge<Contract>(contract_DB, contract);
            contract_NEW.Id = id;
            OnBeforeUpdate(contract_NEW);

            bool test = await iService.UpdateAsync(contract_NEW, id, conn, tran).ConfigureAwait(false);

            ContractLandlord clInfo = null;
            ContractRenter crInfo = null;
            ContractBuilding cbInfo = new ContractBuilding();
            ContractRelevant creInfo = new ContractRelevant();

            // BZ欄位值
            switch (PDFDataModel_NEW.CType.Trim()) {
                case "1":
                    // 出租人
                    MapPDFLS(ref PDFDataModel_NEW);
                    clInfo = PDFDataModel_NEW.MapTo<ContractLandlord>();
                    if (clInfo != null) {
                        ContractLandlord clInfo_DB = await contractLandlordService.GetAsync(id);
                        if (clInfo_DB != null) {
                            ContractLandlord clInfo_NEW = Merger.CloneAndMerge<ContractLandlord>(clInfo_DB, clInfo);
                            clInfo_NEW.Id = id;
                            clInfo_NEW.LastModifyUserId = LastModifyUserId;
                            clInfo_NEW.LastModifyTime = LastModifyTime;
                            await contractLandlordService.UpdateAsync(clInfo_NEW, id, conn, tran);
                        }
                    }
                    TBNoC1 TBNoC1 = PDFDataModel_NEW.MapTo<TBNoC1>();
                    if (!string.IsNullOrWhiteSpace(TBNoC1.CID)) {
                        TBNoC1 TBNoC1_DB = await tbNoC1Service.GetAsync(id);
                        if (TBNoC1_DB == null) {
                            TBNoC1.Id = id;
                            TBNoC1.CreatorUserId = CreatorUserId;
                            TBNoC1.CreatorTime = CreatorTime;
                            await tbNoC1Service.InsertAsync(TBNoC1, conn, tran);

                        } else {
                            TBNoC1 TBNoC1_NEW = Merger.CloneAndMerge<TBNoC1>(TBNoC1_DB, TBNoC1);
                            TBNoC1_NEW.Id = TBNoC1_DB.Id;
                            TBNoC1_NEW.LastModifyUserId = LastModifyUserId;
                            TBNoC1_NEW.LastModifyTime = LastModifyTime;
                            await tbNoC1Service.UpdateAsync(TBNoC1_NEW, TBNoC1_DB.Id, conn, tran);

                        }
                    }
                    TBNoC1_2 TBNoC1_2 = PDFDataModel_NEW.MapTo<TBNoC1_2>();
                    if (!string.IsNullOrWhiteSpace(TBNoC1_2.CID)) {
                        TBNoC1_2 TBNoC1_2_DB = await tbNoC1_2Service.GetAsync(id);
                        if (TBNoC1_2_DB == null) {
                            TBNoC1_2.Id = id;
                            TBNoC1_2.CreatorUserId = CreatorUserId;
                            TBNoC1_2.CreatorTime = CreatorTime;
                            await tbNoC1_2Service.InsertAsync(TBNoC1_2, conn, tran);

                        } else {
                            TBNoC1_2 TBNoC1_2_NEW = Merger.CloneAndMerge<TBNoC1_2>(TBNoC1_2_DB, TBNoC1_2);
                            TBNoC1_2_NEW.Id = TBNoC1_2_DB.Id;
                            TBNoC1_2_NEW.LastModifyUserId = LastModifyUserId;
                            TBNoC1_2_NEW.LastModifyTime = LastModifyTime;
                            await tbNoC1_2Service.UpdateAsync(TBNoC1_2_NEW, TBNoC1_2_DB.Id, conn, tran);
                        }
                    }
                    TBNoC1_3 TBNoC1_3 = PDFDataModel_NEW.MapTo<TBNoC1_3>();
                    if (!string.IsNullOrWhiteSpace(TBNoC1_3.CID)) {
                        TBNoC1_3 TBNoC1_3_DB = await tbNoC1_3Service.GetAsync(id);
                        if (TBNoC1_3_DB == null) {
                            TBNoC1_3.Id = id;
                            TBNoC1_3.CreatorUserId = CreatorUserId;
                            TBNoC1_3.CreatorTime = CreatorTime;
                            await tbNoC1_3Service.InsertAsync(TBNoC1_3, conn, tran);

                        } else {
                            TBNoC1_3 TBNoC1_3_NEW = Merger.CloneAndMerge<TBNoC1_3>(TBNoC1_3_DB, TBNoC1_3);
                            TBNoC1_3_NEW.Id = TBNoC1_3_DB.Id;
                            TBNoC1_3_NEW.LastModifyUserId = LastModifyUserId;
                            TBNoC1_3_NEW.LastModifyTime = LastModifyTime;
                            await tbNoC1_3Service.UpdateAsync(TBNoC1_3_NEW, TBNoC1_3_DB.Id, conn, tran);
                        }
                    }
                    break;
                case "2":
                    // 承租人
                    MapPDFRN(ref PDFDataModel_NEW);
                    crInfo = PDFDataModel_NEW.MapTo<ContractRenter>();
                    if (crInfo != null) {
                        ContractRenter crInfo_DB = await contractRenterService.GetAsync(id);
                        if (crInfo_DB != null) {
                            ContractRenter  crInfo_NEW = Merger.CloneAndMerge<ContractRenter>(crInfo_DB, crInfo);
                            crInfo_NEW.Id = id;
                            crInfo_NEW.LastModifyUserId = LastModifyUserId;
                            crInfo_NEW.LastModifyTime = LastModifyTime;
                            await contractRenterService.UpdateAsync(crInfo_NEW, id, conn, tran);
                        }
                    }
                    TBNoC2 TBNoC2 = PDFDataModel_NEW.MapTo<TBNoC2>();
                    if (!string.IsNullOrWhiteSpace(TBNoC2.CID)) {
                        TBNoC2 TBNoC2_DB = await tbNoC2Service.GetAsync(id);
                        if (TBNoC2_DB == null) {
                            TBNoC2.Id = id;
                            TBNoC2.CreatorUserId = CreatorUserId;
                            TBNoC2.CreatorTime = CreatorTime;
                            await tbNoC2Service.InsertAsync(TBNoC2, conn, tran); ;
                        } else {
                            TBNoC2 TBNoC2_NEW = Merger.CloneAndMerge<TBNoC2>(TBNoC2_DB, TBNoC2);
                            TBNoC2_NEW.Id = TBNoC2_DB.Id;
                            TBNoC2_NEW.LastModifyUserId = LastModifyUserId;
                            TBNoC2.LastModifyTime = LastModifyTime;
                            await tbNoC2Service.UpdateAsync(TBNoC2_NEW, TBNoC2_DB.Id, conn, tran);

                        }
                    }
                    TBNoC2_2 TBNoC2_2 = PDFDataModel_NEW.MapTo<TBNoC2_2>();
                    if (!string.IsNullOrWhiteSpace(TBNoC2_2.CID)) {
                        TBNoC2_2 TBNoC2_2_DB = await tbNoC2_2Service.GetAsync(id);
                        if (TBNoC2_2_DB == null) {
                            TBNoC2_2.Id = id;
                            TBNoC2_2.CreatorUserId = CreatorUserId;
                            TBNoC2_2.CreatorTime = CreatorTime;
                            await tbNoC2_2Service.InsertAsync(TBNoC2_2, conn, tran);

                        } else {
                            TBNoC2_2 TBNoC2_2_NEW = Merger.CloneAndMerge<TBNoC2_2>(TBNoC2_2_DB, TBNoC2_2);
                            TBNoC2_2_NEW.Id = TBNoC2_2_DB.Id;
                            TBNoC2_2_NEW.LastModifyUserId = LastModifyUserId;
                            TBNoC2_2_NEW.LastModifyTime = LastModifyTime;
                            await tbNoC2_2Service.UpdateAsync(TBNoC2_2_NEW, TBNoC2_2_DB.Id, conn, tran);
                        }
                    }
                    TBNoC2_3 TBNoC2_3 = PDFDataModel_NEW.MapTo<TBNoC2_3>();
                    if (!string.IsNullOrWhiteSpace(TBNoC2_3.CID)) {
                        TBNoC2_3 TBNoC2_3_DB = await tbNoC2_3Service.GetAsync(id);
                        if (TBNoC2_3_DB == null) {
                            TBNoC2_3.Id = id;
                            TBNoC2_3.CreatorUserId = CreatorUserId;
                            TBNoC2_3.CreatorTime = CreatorTime;
                            await tbNoC2_3Service.InsertAsync(TBNoC2_3, conn, tran);

                        } else {
                            TBNoC2_3 TBNoC2_3_NEW = Merger.CloneAndMerge<TBNoC2_3>(TBNoC2_3_DB, TBNoC2_3);
                            TBNoC2_3_NEW.Id = TBNoC2_3_DB.Id;
                            TBNoC2_3_NEW.LastModifyUserId = LastModifyUserId;
                            TBNoC2_3_NEW.LastModifyTime = LastModifyTime;
                            await tbNoC2_3Service.UpdateAsync(TBNoC2_3_NEW, TBNoC2_3_DB.Id, conn, tran);
                        }
                    }
                    break;
                case "3":
                    // 出租人
                    MapPDFLS(ref PDFDataModel_NEW);
                    clInfo = PDFDataModel_NEW.MapTo<ContractLandlord>();
                    if (clInfo != null) {
                        ContractLandlord clInfo_DB = await contractLandlordService.GetAsync(id);
                        if (clInfo_DB != null) {
                            ContractLandlord clInfo_NEW = Merger.CloneAndMerge<ContractLandlord>(clInfo_DB, clInfo);
                            clInfo_NEW.Id = id;
                            clInfo_NEW.LastModifyUserId = LastModifyUserId;
                            clInfo_NEW.LastModifyTime = LastModifyTime;
                            await contractLandlordService.UpdateAsync(clInfo_NEW, id, conn, tran);
                        }
                    }
                    // 承租人
                    MapPDFRN(ref PDFDataModel_NEW);
                    crInfo = PDFDataModel_NEW.MapTo<ContractRenter>();
                    if (crInfo != null) {
                        ContractRenter crInfo_DB = await contractRenterService.GetAsync(id);
                        if (crInfo_DB != null) {
                            ContractRenter crInfo_NEW = Merger.CloneAndMerge<ContractRenter>(crInfo_DB, crInfo);
                            crInfo_NEW.Id = id;
                            crInfo_NEW.LastModifyUserId = LastModifyUserId;
                            crInfo_NEW.LastModifyTime = LastModifyTime;
                            await contractRenterService.UpdateAsync(crInfo_NEW, id, conn, tran);
                        }
                    }
                    TBNoC3 TBNoC3 = PDFDataModel_NEW.MapTo<TBNoC3>();
                    if (!string.IsNullOrWhiteSpace(TBNoC3.CID)) {
                        TBNoC3 TBNoC3_DB = await tbNoC3Service.GetAsync(id);
                        if (TBNoC3_DB == null) {
                            TBNoC3.Id = id;
                            TBNoC3.CreatorUserId = CreatorUserId;
                            TBNoC3.CreatorTime = CreatorTime;
                            await tbNoC3Service.InsertAsync(TBNoC3, conn, tran);
                        } else {
                            TBNoC3 TBNoC3_NEW = Merger.CloneAndMerge<TBNoC3>(TBNoC3_DB, TBNoC3);
                            TBNoC3_NEW.Id = TBNoC3_DB.Id;
                            TBNoC3_NEW.LastModifyUserId = LastModifyUserId;
                            TBNoC3_NEW.LastModifyTime = LastModifyTime;
                            await tbNoC3Service.UpdateAsync(TBNoC3_NEW, TBNoC3_DB.Id, conn, tran);
                        }
                    }
                    TBNoC3_2 TBNoC3_2 = PDFDataModel_NEW.MapTo<TBNoC3_2>();
                    if (!string.IsNullOrWhiteSpace(TBNoC3_2.CID)) {
                        TBNoC3_2 TBNoC3_2_DB = await tbNoC3_2Service.GetAsync(id);
                        if (TBNoC3_2_DB == null) {
                            TBNoC3_2.Id = id;
                            TBNoC3_2.CreatorUserId = CreatorUserId;
                            TBNoC3_2.CreatorTime = CreatorTime;
                            await tbNoC3_2Service.InsertAsync(TBNoC3_2, conn, tran);

                        } else {
                            TBNoC3_2 TBNoC3_2_NEW = Merger.CloneAndMerge<TBNoC3_2>(TBNoC3_2_DB, TBNoC3_2);
                            TBNoC3_2_NEW.Id = TBNoC3_2_DB.Id;
                            TBNoC3_2_NEW.LastModifyUserId = LastModifyUserId;
                            TBNoC3_2_NEW.LastModifyTime = LastModifyTime;
                            await tbNoC3_2Service.UpdateAsync(TBNoC3_2_NEW, TBNoC3_2_DB.Id, conn, tran);
                        }
                    }
                    TBNoC3_3 TBNoC3_3 = PDFDataModel_NEW.MapTo<TBNoC3_3>();
                    if (!string.IsNullOrWhiteSpace(TBNoC3_3.CID)) {
                        TBNoC3_3 TBNoC3_3_DB = await tbNoC3_3Service.GetAsync(id);
                        if (TBNoC3_3_DB == null) {
                            TBNoC3_3.Id = id;
                            TBNoC3_3.CreatorUserId = CreatorUserId;
                            TBNoC3_3.CreatorTime = CreatorTime;
                            await tbNoC3_3Service.InsertAsync(TBNoC3_3, conn, tran);

                        } else {
                            TBNoC3_3 TBNoC3_3_NEW = Merger.CloneAndMerge<TBNoC3_3>(TBNoC3_3_DB, TBNoC3_3);
                            TBNoC3_3_NEW.Id = TBNoC3_3_DB.Id;
                            TBNoC3_3_NEW.LastModifyUserId = LastModifyUserId;
                            TBNoC3_3_NEW.LastModifyTime = LastModifyTime;
                            await tbNoC3_3Service.UpdateAsync(TBNoC3_3_NEW, TBNoC3_3_DB.Id, conn, tran);
                        }
                    }
                    break;
            }

            // 合約相關資料
            creInfo = PDFDataModel_NEW.MapTo<ContractRelevant>();
            //creInfo.B1TaxID = PDFDataModel_NEW.B1TaxID;
            //creInfo.RNRentSUBAFee = PDFDataModel_NEW.RNRentSUBAFee;
            if (creInfo != null) {
                ContractRelevant creInfo_DB = await contractRelevantService.GetAsync(id);
                if (creInfo_DB != null) {
                    ContractRelevant crenfo_NEW = Merger.CloneAndMerge<ContractRelevant>(creInfo_DB, creInfo);
                    crenfo_NEW.Id = id;
                    crenfo_NEW.LastModifyUserId = LastModifyUserId;
                    crenfo_NEW.LastModifyTime = LastModifyTime;
                    await contractRelevantService.UpdateAsync(crenfo_NEW, id, conn, tran);
                }
            }

            // 建物
            cbInfo = PDFDataModel_NEW.MapTo<ContractBuilding>();
            if (cbInfo != null) {
                ContractBuilding cbInfo_DB = await contractBuildingService.GetAsync(id);
                if (cbInfo_DB != null) {
                    ContractBuilding cbInfo_NEW = Merger.CloneAndMerge<ContractBuilding>(cbInfo_DB, cbInfo);
                    cbInfo_NEW.Id = id;
                    cbInfo_NEW.LastModifyUserId = LastModifyUserId;
                    cbInfo_NEW.LastModifyTime = LastModifyTime;
                    await contractBuildingService.UpdateAsync(cbInfo_NEW, id, conn, tran);
                }              
            }
        }

        /// <summary>
        /// 取合約類型名稱
        /// </summary>
        /// <param name="type"></param>
        public static string GetTypeName(string type)
        {
            string typeName = "";
            type = type.StartsWith("C") ? type.Remove(0, 1) : type;
            switch (type) {
                case "1":
                    typeName = "包租";
                    break;
                case "2":
                    typeName = "轉租";
                    break;
                case "3":
                    typeName = "代管";
                    break;
            }

            return typeName;
        }

        /// <summary>
        /// 取歸屬ID
        /// </summary>
        /// <param name="info"></param>
        /// <param name="type"></param>
        private string GetArchiveID(ContractInputDto info, string type)
        {
            string archiveID = string.Empty;
            switch (type) {
                case "建物":
                    string BID = buildingService.GetIdByBAdd(info.BAdd);
                    archiveID = BID;
                    break;
                case "出租人":
                    archiveID = info.LSID;
                    break;
                case "承租人":
                    archiveID = info.RNID;
                    break;
            }

            return archiveID;
        }

        /// <summary>
        /// 取承租人身份資格
        /// </summary>
        /// <param name="info"></param>
        private string GetRNQualify(CustomerRN info)
        {
            string result = string.Empty;
            if ("/Yes".Equals(info.RNQualify1C)) {
                result = "一般戶";
            } else if ("/Yes".Equals(info.RNQualify2C)) {
                result = "第一類弱勢戶";
            } else if ("/Yes".Equals(info.RNQualify3C)) {
                result = "第二類弱勢戶";
            }

            return result;
        }

        private void MapLN(ref ContractLandlord clInfo, CustomerLN customerLN)
        {
            clInfo.LSID = customerLN.LNID;
            clInfo.LSName = customerLN.LNName;
            clInfo.LSTel = customerLN.LNTel;
            clInfo.LSTel_1 = customerLN.LNTel_1;
            clInfo.LSTel_2 = customerLN.LNTel_2;
            clInfo.LSCell = customerLN.LNCell;
            clInfo.LSAdd = customerLN.LNAdd;
            clInfo.LSAdd_1 = customerLN.LNAdd_1;
            clInfo.LSAdd_1_1 = customerLN.LNAdd_1_1;
            clInfo.LSAdd_1_2 = customerLN.LNAdd_1_2;
            clInfo.LSAdd_2 = customerLN.LNAdd_2;
            clInfo.LSAdd_2_1 = customerLN.LNAdd_2_1;
            clInfo.LSAdd_2_2 = customerLN.LNAdd_2_2;
            clInfo.LSAdd_2_3 = customerLN.LNAdd_2_3;
            clInfo.LSAdd_2_4 = customerLN.LNAdd_2_4;
            clInfo.LSAdd_3 = customerLN.LNAdd_3;
            clInfo.LSAdd_3_1 = customerLN.LNAdd_3_1;
            clInfo.LSAdd_3_2 = customerLN.LNAdd_3_2;
            clInfo.LSAdd_4 = customerLN.LNAdd_4;
            clInfo.LSAdd_5 = customerLN.LNAdd_5;
            clInfo.LSAdd_6 = customerLN.LNAdd_6;
            clInfo.LSAdd_7 = customerLN.LNAdd_7;
            clInfo.LSAdd_8 = customerLN.LNAdd_8;
            clInfo.LSAdd_9 = customerLN.LNAdd_9;
            clInfo.LSMail = customerLN.LNMail;
            clInfo.LSAddC = customerLN.LNAddC;
            clInfo.LSAddC_1 = customerLN.LNAddC_1;
            clInfo.LSAddC_1_1 = customerLN.LNAddC_1_1;
            clInfo.LSAddC_1_2 = customerLN.LNAddC_1_2;
            clInfo.LSAddC_2 = customerLN.LNAddC_2;
            clInfo.LSAddC_2_1 = customerLN.LNAddC_2_1;
            clInfo.LSAddC_2_2 = customerLN.LNAddC_2_2;
            clInfo.LSAddC_2_3 = customerLN.LNAddC_2_3;
            clInfo.LSAddC_2_4 = customerLN.LNAddC_2_4;
            clInfo.LSAddC_3 = customerLN.LNAddC_3;
            clInfo.LSAddC_3_1 = customerLN.LNAddC_3_1;
            clInfo.LSAddC_3_2 = customerLN.LNAddC_3_2;
            clInfo.LSAddC_4 = customerLN.LNAddC_4;
            clInfo.LSAddC_5 = customerLN.LNAddC_5;
            clInfo.LSAddC_6 = customerLN.LNAddC_6;
            clInfo.LSAddC_7 = customerLN.LNAddC_7;
            clInfo.LSAddC_8 = customerLN.LNAddC_8;
            clInfo.LSAddC_9 = customerLN.LNAddC_9;
            clInfo.LSAGName_A = customerLN.LNAGName_A;
            clInfo.LSAGID_A = customerLN.LNAGID_A;
            clInfo.LSAGAddC_A = customerLN.LNAGAddC_A;
            clInfo.LSAGAddC_1_A = customerLN.LNAGAddC_1_A;
            clInfo.LSAGAddC_1_1_A = customerLN.LNAGAddC_1_1_A;
            clInfo.LSAGAddC_1_2_A = customerLN.LNAGAddC_1_2_A;
            clInfo.LSAGAddC_2_A = customerLN.LNAGAddC_2_A;
            clInfo.LSAGAddC_2_1_A = customerLN.LNAGAddC_2_1_A;
            clInfo.LSAGAddC_2_2_A = customerLN.LNAGAddC_2_2_A;
            clInfo.LSAGAddC_2_3_A = customerLN.LNAGAddC_2_3_A;
            clInfo.LSAGAddC_2_4_A = customerLN.LNAGAddC_2_4_A;
            clInfo.LSAGAddC_3_A = customerLN.LNAGAddC_3_A;
            clInfo.LSAGAddC_3_1_A = customerLN.LNAGAddC_3_1_A;
            clInfo.LSAGAddC_3_2_A = customerLN.LNAGAddC_3_2_A;
            clInfo.LSAGAddC_4_A = customerLN.LNAGAddC_4_A;
            clInfo.LSAGAddC_5_A = customerLN.LNAGAddC_5_A;
            clInfo.LSAGAddC_6_A = customerLN.LNAGAddC_6_A;
            clInfo.LSAGAddC_7_A = customerLN.LNAGAddC_7_A;
            clInfo.LSAGAddC_8_A = customerLN.LNAGAddC_8_A;
            clInfo.LSAGAddC_9_A = customerLN.LNAGAddC_9_A;
            clInfo.LSAGCell_A = customerLN.LNAGCell_A;
        }

        private void MapLC(ref ContractLandlord clInfo, CustomerLC customerLC)
        {
            clInfo.LSID = customerLC.LCID;
            clInfo.LSName = customerLC.LCName;
            clInfo.LSRep = customerLC.LCRep;
            clInfo.LSAddC = customerLC.LCAdd;
            clInfo.LSTel = customerLC.LCTel;
            clInfo.LSTel_1 = customerLC.LCTel_1;
            clInfo.LSTel_2 = customerLC.LCTel_2;
            clInfo.LSAdd = customerLC.LCAdd;
            clInfo.LSAdd_1 = customerLC.LCAdd_1;
            clInfo.LSAdd_1_1 = customerLC.LCAdd_1_1;
            clInfo.LSAdd_1_2 = customerLC.LCAdd_1_2;
            clInfo.LSAdd_2 = customerLC.LCAdd_2;
            clInfo.LSAdd_2_1 = customerLC.LCAdd_2_1;
            clInfo.LSAdd_2_2 = customerLC.LCAdd_2_2;
            clInfo.LSAdd_2_3 = customerLC.LCAdd_2_3;
            clInfo.LSAdd_2_4 = customerLC.LCAdd_2_4;
            clInfo.LSAdd_3 = customerLC.LCAdd_3;
            clInfo.LSAdd_3_1 = customerLC.LCAdd_3_1;
            clInfo.LSAdd_3_2 = customerLC.LCAdd_3_2;
            clInfo.LSAdd_4 = customerLC.LCAdd_4;
            clInfo.LSAdd_5 = customerLC.LCAdd_5;
            clInfo.LSAdd_6 = customerLC.LCAdd_6;
            clInfo.LSAdd_7 = customerLC.LCAdd_7;
            clInfo.LSAdd_8 = customerLC.LCAdd_8;
            clInfo.LSAdd_9 = customerLC.LCAdd_9;
            clInfo.LSAddC_2 = customerLC.LCAdd_2;
            clInfo.LSAddC_2_1 = customerLC.LCAdd_2_1;
            clInfo.LSAddC_2_2 = customerLC.LCAdd_2_2;
            clInfo.LSAddC_2_3 = customerLC.LCAdd_2_3;
            clInfo.LSAddC_2_4 = customerLC.LCAdd_2_4;
            clInfo.LSAddC_3 = customerLC.LCAdd_3;
            clInfo.LSAddC_3_1 = customerLC.LCAdd_3_1;
            clInfo.LSAddC_3_2 = customerLC.LCAdd_3_2;
            clInfo.LSAddC_4 = customerLC.LCAdd_4;
            clInfo.LSAddC_5 = customerLC.LCAdd_5;
            clInfo.LSAddC_6 = customerLC.LCAdd_6;
            clInfo.LSAddC_7 = customerLC.LCAdd_7;
            clInfo.LSAddC_8 = customerLC.LCAdd_8;
            clInfo.LSAddC_9 = customerLC.LCAdd_9;
            clInfo.LSAGName_A = customerLC.LCAGName_A;
            clInfo.LSAGID_A = customerLC.LCAGID_A;
            clInfo.LSAGAddC_A = customerLC.LCAGAdd_A;
            clInfo.LSAGAddC_1_A = customerLC.LCAGAdd_1_A;
            clInfo.LSAGAddC_1_1_A = customerLC.LCAGAdd_1_1_A;
            clInfo.LSAGAddC_1_2_A = customerLC.LCAGAdd_1_2_A;
            clInfo.LSAGAddC_2_A = customerLC.LCAGAdd_2_A;
            clInfo.LSAGAddC_2_1_A = customerLC.LCAGAdd_2_1_A;
            clInfo.LSAGAddC_2_2_A = customerLC.LCAGAdd_2_2_A;
            clInfo.LSAGAddC_2_3_A = customerLC.LCAGAdd_2_3_A;
            clInfo.LSAGAddC_2_4_A = customerLC.LCAGAdd_2_4_A;
            clInfo.LSAGAddC_3_A = customerLC.LCAGAdd_3_A;
            clInfo.LSAGAddC_3_1_A = customerLC.LCAGAdd_3_1_A;
            clInfo.LSAGAddC_3_2_A = customerLC.LCAGAdd_3_2_A;
            clInfo.LSAGAddC_4_A = customerLC.LCAGAdd_4_A;
            clInfo.LSAGAddC_5_A = customerLC.LCAGAdd_5_A;
            clInfo.LSAGAddC_6_A = customerLC.LCAGAdd_6_A;
            clInfo.LSAGAddC_8_A = customerLC.LCAGAdd_8_A;
            clInfo.LSAGAddC_9_A = customerLC.LCAGAdd_9_A;
            clInfo.LSAGCell_A = customerLC.LCAGCell_A;
        }

        private void mapRN(ref ContractRenter clInfo, CustomerRN customerRN) {
            
        }

        private void mapRC(ref ContractRenter clInfo, CustomerRC customerRC)
        {

        }

        private void MapSL(ref ContractInputDto input, SecondLandlordOutputDto slODto, SLMAOutputDto slmaODto)
        {
            input.SLName = slODto.SLName;
            input.SLRep = slODto.SLRep;
            input.SLLRNo = slODto.SLLRNo;
            input.SLAdd = input.SIAdd;
            input.SLTel = input.SITel;
            if (!string.IsNullOrEmpty(input.SLTel)) {
                string[] telArray = Utils.getTelArray(input.SLTel);
                if (telArray != null && telArray.Length > 0) {
                    input.SLTel_1 = telArray[0];
                    input.SLTel_2 = telArray[1];
                }
            }


            if (!string.IsNullOrEmpty(slmaODto.Fax)) {
                input.SLFax = slmaODto.Fax;
                string[] faxArray = Utils.getTelArray(slmaODto.Fax);
                if (faxArray != null && faxArray.Length > 0) {
                    input.SLFax_1 = faxArray[0];
                    input.SLFax_2 = faxArray[1];
                }
            }
        }

        private void MapPDFLS(ref ContractPDFDataModel PDFDataModel_NEW)
        {
            // 出租人聯絡電話
            PDFDataModel_NEW.LSTel = $"{PDFDataModel_NEW.LSTel_1}-{PDFDataModel_NEW.LSTel_2}";
        }

        private void MapPDFRN(ref ContractPDFDataModel PDFDataModel_NEW)
        {
            // 出租人聯絡電話
            PDFDataModel_NEW.RNTel = $"{PDFDataModel_NEW.RNTel_1}-{PDFDataModel_NEW.RNTel_2}";
        }
    }
}