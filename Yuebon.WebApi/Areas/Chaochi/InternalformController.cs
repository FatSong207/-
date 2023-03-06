using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Services;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.AspNetCore.Mvc;
using Yuebon.Commons.Cache;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authorization;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Commons.Json;
using System.IO;
using System.Linq;
using System.Dynamic;
using Newtonsoft.Json;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Core.IServices;
using System.Reflection;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Chaochi.Core.Helpers;
using Microsoft.AspNetCore.Http;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Annotations;
using PdfSharp.Drawing;
using PdfSharp.Pdf.AcroForms;
using PdfSharp.Pdf.Advanced;
using Yuebon.Security.IServices;
using System.Net.Http;
using System.Data;
using Yuebon.Commons.IDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Yuebon.WebApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 內部表單接口
    /// </summary>
    [ApiController]
    [Route("api/chaochi/[controller]")]
    [LogController]
    public class InternalformController : AreaApiController<Internalform, InternalformOutputDto, InternalformInPutDto, IInternalformService, string>
    {
        private ISignatureImgRepository _signatureRepos;
        private readonly IDbContextCore _ybdbcontext;
        private readonly ICustomerRCService _customerRCService;
        private readonly IRemittanceLService _remittanceLService;
        private readonly IRemittanceRService _remittanceRService;
        private readonly ITBNoB6Service _tBNoB6Service;
        private readonly IBuildingService _buildingService;
        private readonly IBuilding1Service _building1Service;
        private readonly ICustomerRNService _customerRNService;
        private readonly ICustomerLCService _customerLCService;
        private readonly ICustomerLNService _customerLNService;
        private readonly IHistoryFormBuildingService _historyFormBuildingService;
        private readonly IHistoryFormRNService _historyFormRNService;
        private readonly IHistoryFormLCService _historyFormLCService;
        private readonly IHistoryFormLNService _historyFormLNService;
        private readonly IBlackListService _blackListService;
        private readonly IOrganizeService _organizeService;
        private readonly IHistoryFormRCService _historyFormRCService;
        private ITBNoA1Repository _tBNoA1Repos;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="ybdbcontext"></param>
        /// <param name="customerRCService"></param>
        /// <param name="blackListService"></param>
        /// <param name="organizeService"></param>
        /// <param name="historyFormRCService"></param>
        /// <param name="historyFormBuildingService"></param>
        /// <param name="historyFormRNService"></param>
        /// <param name="historyFormLCService"></param>
        /// <param name="historyFormLNService"></param>
        /// <param name="remittanceLService"></param>
        /// <param name="remittanceRService"></param>
        /// <param name="tBNoB6Service"></param>
        /// <param name="buildingService"></param>
        /// <param name="building1Service"></param>
        /// <param name="customerRNService"></param>
        /// <param name="customerLCService"></param>
        /// <param name="customerLNService"></param>
        /// <param name="tBNoA1Repos"></param>
        /// <param name="signatureRepos"></param>
        public InternalformController(IInternalformService _iService, IDbContextCore ybdbcontext, ICustomerRCService customerRCService, IBlackListService blackListService, Yuebon.Security.IServices.IOrganizeService organizeService, IHistoryFormRCService historyFormRCService, IHistoryFormBuildingService historyFormBuildingService, IHistoryFormRNService historyFormRNService, IHistoryFormLCService historyFormLCService, IHistoryFormLNService historyFormLNService, IRemittanceLService remittanceLService, IRemittanceRService remittanceRService, ITBNoB6Service tBNoB6Service, IBuildingService buildingService, IBuilding1Service building1Service, ICustomerRNService customerRNService, ICustomerLCService customerLCService, ICustomerLNService customerLNService, ITBNoA1Repository tBNoA1Repos, ISignatureImgRepository signatureRepos) : base(_iService)
        {
            iService = _iService;
            _ybdbcontext = ybdbcontext;
            _customerRCService = customerRCService;
            _blackListService = blackListService;
            _organizeService = organizeService;
            _historyFormRCService = historyFormRCService;
            _historyFormBuildingService = historyFormBuildingService;
            _historyFormRNService = historyFormRNService;
            _historyFormLCService = historyFormLCService;
            _historyFormLNService = historyFormLNService;
            _remittanceLService = remittanceLService;
            _remittanceRService = remittanceRService;
            _tBNoB6Service = tBNoB6Service;
            _buildingService = buildingService;
            _building1Service = building1Service;
            _customerRNService = customerRNService;
            _customerLCService = customerLCService;
            _customerLNService = customerLNService;
            _tBNoA1Repos = tBNoA1Repos;
            _signatureRepos = signatureRepos;
        }

        protected override void OnBeforeInsert(Internalform info)
        {
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeUpdate(Internalform info)
        {
            info.LastModifyTime = DateTime.Now;
            info.LastModifyUserId = CurrentUser.UserId;
        }

        /// <summary>
        /// 下載
        /// </summary>
        /// <param name="searchPDF"></param>
        /// <returns></returns>
        [HttpPost("DownloadWithData")]
        [YuebonAuthorize("")]
        //public IActionResult Get(string fileName)
        public async Task<IActionResult> DownloadWithData(SearchExternalformPDFModel searchPDF)
        {
            CommonResult result = new CommonResult();
            //檢查下載存取權限
            var checkAuth = await CheckAuth(searchPDF);
            if (checkAuth.Success == false) {
                return ToJsonContent(checkAuth);
            }
            string FormId = searchPDF.FormId;
            string LNId = searchPDF.LNID;
            string LCId = searchPDF.LCID;
            string BAdd = Utils.memergeAddFix(
                   searchPDF.BAdd_1,
                   searchPDF.BAdd_1_1,
                   searchPDF.BAdd_1_2,
                   searchPDF.BAdd_2,
                   searchPDF.BAdd_2_1,
                   searchPDF.BAdd_2_2,
                   searchPDF.BAdd_2_3,
                   searchPDF.BAdd_2_4,
                   searchPDF.BAdd_3,
                   searchPDF.BAdd_3_1,
                   searchPDF.BAdd_3_2,
                   searchPDF.BAdd_4,
                   searchPDF.BAdd_5,
                   searchPDF.BAdd_6,
                   searchPDF.BAdd_7,
                   searchPDF.BAdd_8,
                   searchPDF.BAdd_9);
            string RNId = searchPDF.RNID;
            string RCId = searchPDF.RCID;
            string sqlWhere = $" 1=1 and FName = '{FormId}'  ";
            string refKeys = LNId + LCId + RNId + BAdd;
            if (!string.IsNullOrEmpty(refKeys)) {
                sqlWhere += $" and RefKeys = '{refKeys}' ";
            }

            var internalForm = await iService.GetByFormId(FormId);
            var customerln = await _customerLNService.GetByCustomerLNID(LNId);
            var customerlc = await _customerLCService.GetByLCID(LCId);
            var customerrn = await _customerRNService.GetCustomerByRNID(RNId);
            var customerrc = await _customerRCService.GetWhereAsync($" RCID = '{RCId}' ");
            var building = await _buildingService.GetBuildingByBAddAsync(BAdd);
            var building1 = building is null ? null : await _building1Service.GetAsync(building.Id);
            var tbnob6 = await _tBNoB6Service.GetWhereAsync($"{sqlWhere}");

            //必填檢查
            if (internalForm.RequiredLandlordN == "Y") {
                //判斷是否為第一張WEB表單(A000001 or A000002)，因為弟張WEB表單的必填項分得較細，所以當表單屬性中必填項同時勾選出租自然人+出租法人時代表只需要填出租自然人or出租法人 其一條件即可
                if (internalForm.RequiredLandlordC == "N") {
                    if (string.IsNullOrEmpty(LNId)) {
                        result.Success = false;
                        result.ErrMsg = "請輸入出租人身分證字號/居留證號";
                        return ToJsonContent(result);
                    }
                }
            }
            if (internalForm.RequiredLandlordC == "Y") {
                //同上
                if (internalForm.RequiredLandlordN == "N") {
                    if (string.IsNullOrEmpty(LCId)) {
                        result.Success = false;
                        result.ErrMsg = "請輸入出租人統一編號";
                        return ToJsonContent(result);
                    }
                }
            }
            if (internalForm.RequiredRenterN == "Y") {
                //同出租人邏輯
                if (internalForm.RequiredRenterC == "N") {
                    if (string.IsNullOrEmpty(RNId)) {
                        result.Success = false;
                        result.ErrMsg = "請輸入承租人身分證字號/居留證號";
                        return ToJsonContent(result);
                    }
                }

            }
            if (internalForm.RequiredRenterC == "Y") {
                //同出租人邏輯
                if (internalForm.RequiredRenterN == "N") {
                    if (string.IsNullOrEmpty(RCId)) {
                        result.Success = false;
                        result.ErrMsg = "請輸入承租人統一編號";
                        return ToJsonContent(result);
                    }
                }

            }
            if (internalForm.RequiredBuilding == "Y") {
                if (string.IsNullOrEmpty(BAdd)) {
                    result.Success = false;
                    result.ErrMsg = "請輸入建物地址";
                    return ToJsonContent(result);
                }
            }
            //必須為已存在值檢查
            if (internalForm.MustExistsLandLord == "Y") {
                if (customerln is null && customerlc is null) {
                    result.Success = false;
                    result.ErrMsg = "查無此出租人";
                    return ToJsonContent(result);
                }
            }
            if (internalForm.MustExistsRenter == "Y") {
                if (customerrn is null && customerrc is null) {
                    result.Success = false;
                    result.ErrMsg = "查無此承租人";
                    return ToJsonContent(result);
                }
            }
            if (internalForm.MustExistsBuilding == "Y") {
                if (building is null) {
                    result.Success = false;
                    result.ErrMsg = "查無此建物";
                    return ToJsonContent(result);
                }
            }

            var webFormDataModel = new WebFormDataModel();

            if (searchPDF.ReplaceROFieldCheck == true) {
                if (tbnob6 == null) {
                    tbnob6 = new TBNoB6();
                    tbnob6.FName = FormId;
                    tbnob6.RefKeys = refKeys;
                    tbnob6.IsFiled = "N";
                    tbnob6.CreatorUserId = CurrentUser.UserId;
                    tbnob6.CreatorTime = DateTime.Now;
                    tbnob6.ROName = searchPDF.ROName;
                    tbnob6.RORep = searchPDF.RORep;
                    tbnob6.ROAdd = searchPDF.ROAdd;
                    tbnob6.ROUserName = searchPDF.ROUserName;
                    tbnob6.ROTel = searchPDF.ROTel;
                    tbnob6.ROFax = searchPDF.ROFax;
                    tbnob6.ROLRNo = searchPDF.ROLRNo;
                    tbnob6.ROID = searchPDF.ROID;
                    tbnob6.RHMName = searchPDF.RHMName;
                    tbnob6.RHMRNo = searchPDF.RHMRNo;
                    tbnob6.AGName = searchPDF.AGName;
                    tbnob6.AGLRNo = searchPDF.AGLRNo;
                    await _tBNoB6Service.InsertAsync(tbnob6);
                } else {
                    tbnob6.LastModifyTime = DateTime.Now;
                    tbnob6.LastModifyUserId = CurrentUser.UserId;
                    tbnob6.ROName = searchPDF.ROName;
                    tbnob6.RORep = searchPDF.RORep;
                    tbnob6.ROAdd = searchPDF.ROAdd;
                    tbnob6.ROUserName = searchPDF.ROUserName;
                    tbnob6.ROTel = searchPDF.ROTel;
                    tbnob6.ROFax = searchPDF.ROFax;
                    tbnob6.ROLRNo = searchPDF.ROLRNo;
                    tbnob6.ROID = searchPDF.ROID;
                    tbnob6.RHMName = searchPDF.RHMName;
                    tbnob6.RHMRNo = searchPDF.RHMRNo;
                    tbnob6.AGName = searchPDF.AGName;
                    tbnob6.AGLRNo = searchPDF.AGLRNo;
                    await _tBNoB6Service.UpdateAsync(tbnob6, tbnob6.Id);
                }
            }

            //MergeLN
            if (!string.IsNullOrEmpty(LNId)) {
                if (customerln != null) {
                    webFormDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webFormDataModel, (customerln.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(customerln, null))));
                }
                {
                    #region LS欄位處理

                    webFormDataModel.LSName = webFormDataModel.LNName;
                    webFormDataModel.LSID = webFormDataModel.LNID;
                    webFormDataModel.LSTel_1 = webFormDataModel.LNTel_1;
                    webFormDataModel.LSTel_2 = webFormDataModel.LNTel_2;
                    webFormDataModel.LSAdd = webFormDataModel.LNAdd;
                    webFormDataModel.LSAdd_1 = webFormDataModel.LNAdd_1;
                    webFormDataModel.LSAdd_1_1 = webFormDataModel.LNAdd_1_1;
                    webFormDataModel.LSAdd_1_2 = webFormDataModel.LNAdd_1_2;
                    webFormDataModel.LSAdd_2 = webFormDataModel.LNAdd_2;
                    webFormDataModel.LSAdd_2_1 = webFormDataModel.LNAdd_2_1;
                    webFormDataModel.LSAdd_2_2 = webFormDataModel.LNAdd_2_2;
                    webFormDataModel.LSAdd_2_3 = webFormDataModel.LNAdd_2_3;
                    webFormDataModel.LSAdd_2_4 = webFormDataModel.LNAdd_2_4;
                    webFormDataModel.LSAdd_3 = webFormDataModel.LNAdd_3;
                    webFormDataModel.LSAdd_3_1 = webFormDataModel.LNAdd_3_1;
                    webFormDataModel.LSAdd_3_2 = webFormDataModel.LNAdd_3_2;
                    webFormDataModel.LSAdd_4 = webFormDataModel.LNAdd_4;
                    webFormDataModel.LSAdd_5 = webFormDataModel.LNAdd_5;
                    webFormDataModel.LSAdd_6 = webFormDataModel.LNAdd_6;
                    webFormDataModel.LSAdd_7 = webFormDataModel.LNAdd_7;
                    webFormDataModel.LSAdd_8 = webFormDataModel.LNAdd_8;
                    webFormDataModel.LSAdd_9 = webFormDataModel.LNAdd_9;

                    #endregion
                }
            }

            //MergeLC
            if (!string.IsNullOrEmpty(LCId)) {
                if (customerlc != null) {
                    webFormDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webFormDataModel, (customerlc.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(customerlc, null))));
                }
                {
                    #region LS欄位處理

                    webFormDataModel.LSName = webFormDataModel.LCName;
                    webFormDataModel.LSID = webFormDataModel.LCID;
                    webFormDataModel.LSTel_1 = webFormDataModel.LCTel_1;
                    webFormDataModel.LSTel_2 = webFormDataModel.LCTel_2;
                    webFormDataModel.LSAdd = webFormDataModel.LCAdd;
                    webFormDataModel.LSAdd_1 = webFormDataModel.LCAdd_1;
                    webFormDataModel.LSAdd_1_1 = webFormDataModel.LCAdd_1_1;
                    webFormDataModel.LSAdd_1_2 = webFormDataModel.LCAdd_1_2;
                    webFormDataModel.LSAdd_2 = webFormDataModel.LCAdd_2;
                    webFormDataModel.LSAdd_2_1 = webFormDataModel.LCAdd_2_1;
                    webFormDataModel.LSAdd_2_2 = webFormDataModel.LCAdd_2_2;
                    webFormDataModel.LSAdd_2_3 = webFormDataModel.LCAdd_2_3;
                    webFormDataModel.LSAdd_2_4 = webFormDataModel.LCAdd_2_4;
                    webFormDataModel.LSAdd_3 = webFormDataModel.LCAdd_3;
                    webFormDataModel.LSAdd_3_1 = webFormDataModel.LCAdd_3_1;
                    webFormDataModel.LSAdd_3_2 = webFormDataModel.LCAdd_3_2;
                    webFormDataModel.LSAdd_4 = webFormDataModel.LCAdd_4;
                    webFormDataModel.LSAdd_5 = webFormDataModel.LCAdd_5;
                    webFormDataModel.LSAdd_6 = webFormDataModel.LCAdd_6;
                    webFormDataModel.LSAdd_7 = webFormDataModel.LCAdd_7;
                    webFormDataModel.LSAdd_8 = webFormDataModel.LCAdd_8;
                    webFormDataModel.LSAdd_9 = webFormDataModel.LCAdd_9;

                    #endregion
                }
            }

            //MergeRN
            if (!string.IsNullOrEmpty(RNId)) {
                if (customerrn != null) {
                    webFormDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webFormDataModel, (customerrn.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(customerrn, null))));
                }
            }

            //MergeRC
            if (!string.IsNullOrEmpty(RCId)) {
                if (customerrc != null) {
                    webFormDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webFormDataModel, (customerrc.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(customerrc, null))));
                }
            }

            //MergeBuilding
            if (!string.IsNullOrEmpty(BAdd)) {
                if (building != null) {
                    webFormDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webFormDataModel, (building.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(building, null))));
                }
                if (building1 != null) {
                    webFormDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webFormDataModel, (building1.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(building1, null))));
                }
            }
            //Merge TBNoB6
            if (tbnob6 is not null) {
                webFormDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webFormDataModel, (tbnob6.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(tbnob6, null))));
            }
            //Merge RemittanceL
            if (!string.IsNullOrEmpty(searchPDF.LRemittanceId)) {
                var id = searchPDF.LRemittanceId;
                if (id != "new" && !string.IsNullOrEmpty(id)) {
                    var remittanceL = await _remittanceLService.GetAsync(id);
                    webFormDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webFormDataModel, (remittanceL.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(remittanceL, null))));
                }
            }

            //Merge RemittanceR
            if (!string.IsNullOrEmpty(searchPDF.RRemittanceId)) {
                var id = searchPDF.RRemittanceId;
                if (id != "new" && !string.IsNullOrEmpty(id)) {
                    var remittanceR = await _remittanceRService.GetAsync(id);
                    webFormDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webFormDataModel, (remittanceR.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(remittanceR, null))));
                }
            }


            switch (FormId) {
                case "A000002":
                    result.ResData = webFormDataModel.MapTo<A000002OutputDto>();
                    break;
                case "A000001":
                    result.ResData = webFormDataModel.MapTo<A000001OutputDto>();
                    break;
                case "A000003":
                    result.ResData = webFormDataModel.MapTo<A000003OutputDto>();
                    break;
                case "A000004":
                    result.ResData = webFormDataModel.MapTo<A000004OutputDto>();
                    break;
                case "A000201":
                    result.ResData = webFormDataModel.MapTo<A000201OutputDto>();
                    break;
                case "A000202":
                    result.ResData = webFormDataModel.MapTo<A000202OutputDto>();
                    break;
                case "A000203":
                    result.ResData = webFormDataModel.MapTo<A000203OutputDto>();
                    break;
                default:
                    break;
            }

            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        ///  上傳
        /// </summary>
        [HttpPost("UploadWithData")]
        [NoSignRequired]
        public async Task<IActionResult> UploadWithData(string formId, WebFormDataModel webFormDataModel)
        {
            var result = new CommonResult();
            switch (formId) {
                case "A000002":
                    var inputDtoA2 = webFormDataModel.MapTo<A000002InputDto>();
                    try {
                        var saveResult = await SaveDataFromInPutDto("A000002", inputDtoA2);
                        if (saveResult == "送審失敗!") {
                            result.Success = false;
                            result.ErrCode = ErrCode.err1;
                            result.ErrMsg = "請求入居者審查API失敗 請連絡相關人員";
                            return ToJsonContent(result);
                        }
                        result.ResData = saveResult;
                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } catch (Exception ex) {
                        Log4NetHelper.Error("上傳失敗!", ex);
                        result.Success = false;
                        result.ErrCode = ErrCode.err40110;
                        result.ErrMsg = "上傳失敗";
                        throw;
                    }
                    break;
                case "A000001":
                    var inputDtoA1 = webFormDataModel.MapTo<A000001InputDto>();
                    try {
                        result.ResData = await SaveDataFromInPutDto("A000001", inputDtoA1);

                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } catch (Exception ex) {
                        Log4NetHelper.Error("上傳失敗!", ex);
                        result.Success = false;
                        result.ErrCode = ErrCode.err40110;
                        result.ErrMsg = "上傳失敗";
                        throw;
                    }
                    break;
                case "A000003":
                    var inputDtoA3 = webFormDataModel.MapTo<A000003InputDto>();
                    try {
                        result.ResData = await SaveDataFromInPutDto("A000003", inputDtoA3);

                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } catch (Exception ex) {
                        Log4NetHelper.Error("上傳失敗!", ex);
                        result.Success = false;
                        result.ErrCode = ErrCode.err40110;
                        result.ErrMsg = "上傳失敗";
                        throw;
                    }
                    break;
                case "A000004":
                    var inputDtoA4 = webFormDataModel.MapTo<A000004InputDto>();
                    try {
                        result.ResData = await SaveDataFromInPutDto("A000004", inputDtoA4);

                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } catch (Exception ex) {
                        Log4NetHelper.Error("上傳失敗!", ex);
                        result.Success = false;
                        result.ErrCode = ErrCode.err40110;
                        result.ErrMsg = "上傳失敗";
                        throw;
                    }
                    break;
                case "A000201":
                    var inputDtoA201 = webFormDataModel.MapTo<A000201InputDto>();
                    try {
                        //時間額外處理(因為在WEB上是一個BZ035欄位，但是匯出程PDF時要拆分成3個欄位BZ035 BZ036 BZ037)
                        if (!string.IsNullOrEmpty(inputDtoA201.BZ035)) {
                            inputDtoA201.BZ036 = inputDtoA201.BZ035.Split('-')[1];
                            inputDtoA201.BZ037 = inputDtoA201.BZ035.Split('-')[2];
                            //inputDtoA201.BZ035 = inputDtoA201.BZ035.Split('-')[0];
                        }
                        if (!string.IsNullOrEmpty(inputDtoA201.BZ038)) {
                            inputDtoA201.BZ039 = inputDtoA201.BZ038.Split('-')[1];
                            inputDtoA201.BZ040 = inputDtoA201.BZ038.Split('-')[2];
                            //inputDtoA201.BZ038 = inputDtoA201.BZ038.Split('-')[0];
                        }
                        result.ResData = await SaveDataFromInPutDto("A000201", inputDtoA201);

                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } catch (Exception ex) {
                        Log4NetHelper.Error("上傳失敗!", ex);
                        result.Success = false;
                        result.ErrCode = ErrCode.err40110;
                        result.ErrMsg = "上傳失敗";
                        throw;
                    }
                    break;
                case "A000202":
                    var inputDtoA202 = webFormDataModel.MapTo<A000202InputDto>();
                    try {
                        result.ResData = await SaveDataFromInPutDto("A000202", inputDtoA202);

                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } catch (Exception ex) {
                        Log4NetHelper.Error("上傳失敗!", ex);
                        result.Success = false;
                        result.ErrCode = ErrCode.err40110;
                        result.ErrMsg = "上傳失敗";
                        throw;
                    }
                    break;
                case "A000203":
                    var inputDtoA203 = webFormDataModel.MapTo<A000203InputDto>();
                    try {
                        result.ResData = await SaveDataFromInPutDto("A000203", inputDtoA203);

                        result.Success = true;
                        result.ErrCode = ErrCode.successCode;
                    } catch (Exception ex) {
                        Log4NetHelper.Error("上傳失敗!", ex);
                        result.Success = false;
                        result.ErrCode = ErrCode.err40110;
                        result.ErrMsg = "上傳失敗";
                        throw;
                    }
                    break;
                default:
                    break;
            }
            //var crn = pdfDataModel.MapTo<CustomerRN>();
            return ToJsonContent(result);
        }

        /// <summary>
        /// 新增or更新資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="formId"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public async Task<string> SaveDataFromInPutDto<T>(string formId, T inputDto) where T : class
        {
            var t = typeof(T);
            var result = "";
            string RNID = "";
            string RCID = "";
            string LNID = "";
            string LCID = "";
            string LSID = "";
            string BAdd = "";
            string LAName = "";
            string RAName = "";
            string IsGenPDf = "";
            string sqlWhere = $"1=1 and FName='{formId}'";
            string refKeys = "";
            var props = t.GetProperties();
            foreach (var item in props) {
                if (item.Name == "LNID") {
                    LNID = item.GetValue(inputDto)?.ToString();
                    //refKeys += LNID;
                }
                if (item.Name == "RCID") {
                    RCID = item.GetValue(inputDto)?.ToString();
                    //refKeys += LNID;
                }
                if (item.Name == "LCID") {
                    LCID = item.GetValue(inputDto)?.ToString();
                    //refKeys += LCID;
                }
                if (item.Name == "LSID") {
                    LSID = item.GetValue(inputDto)?.ToString();
                }
                if (item.Name == "RNID") {
                    RNID = item.GetValue(inputDto)?.ToString();
                    //refKeys += RNID;
                }
                if (item.Name == "LAName") {
                    LAName = item.GetValue(inputDto)?.ToString();
                }
                if (item.Name == "RAName") {
                    RAName = item.GetValue(inputDto)?.ToString();
                }
                if (item.Name == "IsGenPDF") {
                    IsGenPDf = item.GetValue(inputDto)?.ToString();
                }
                if (item.Name == "BAdd") {
                    BAdd = item.GetValue(inputDto)?.ToString();
                    if (string.IsNullOrEmpty(BAdd)) {
                        BAdd = Utils.memergeAddFix(
                            t.GetProperty("BAdd_1").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_1_1").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_1_2").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_2").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_2_1").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_2_2").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_2_3").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_2_4").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_3").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_3_1").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_3_2").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_4").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_5").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_6").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_7").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_8").GetValue(inputDto)?.ToString(),
                            t.GetProperty("BAdd_9").GetValue(inputDto)?.ToString()
                            );
                    }
                    //refKeys += BAdd;
                }

            }
            //判斷LSID長度決定要更新自然人或法人
            if (!string.IsNullOrEmpty(LSID)) {
                switch (LSID.Length) {
                    case 8:
                        LCID = LSID;
                        break;
                    case 10:
                        LNID = LSID;
                        break;
                    default:
                        break;
                }
            }
            refKeys = LNID + LCID + RNID + RCID + BAdd;
            if (!string.IsNullOrEmpty(refKeys)) {
                sqlWhere += $" and RefKeys='{refKeys}'";
            }

            using IDbConnection conn = _ybdbcontext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = _ybdbcontext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                //下方現階段保留新增功能，目前不可能會走到新增的代碼(保留起來以防未來修求變更)
                //更新or新增出租自然人
                if (!string.IsNullOrEmpty(LNID)) {
                    var customerln = await _customerLNService.GetByCustomerLNID(LNID);
                    if (customerln == null) {
                        var cln = inputDto.MapTo<CustomerLN>();
                        HandleSpecificData.HandleSpecificDataLN(cln, inputDto);
                        cln.CreatorTime = DateTime.Now;
                        cln.CreatorUserDeptId = CurrentUser.DeptId;
                        cln.CreatorUserId = CurrentUser.UserId;
                        cln.CreatorUserOrgId = CurrentUser.OrganizeId;
                        cln.LastModifyTime = DateTime.Now;
                        cln.LastModifyUserId = CurrentUser.UserId;
                        await _customerLNService.InsertAsync(cln, conn, tran);
                    } else {
                        var cln = inputDto.MapTo(customerln);
                        HandleSpecificData.HandleSpecificDataLN(cln, inputDto);
                        cln.LastModifyTime = DateTime.Now;
                        cln.LastModifyUserId = CurrentUser.UserId;
                        await _customerLNService.UpdateAsync(cln, customerln.Id, conn, tran);
                    }
                }
                //更新or新增出租自然人
                if (!string.IsNullOrEmpty(LCID)) {
                    var customerlc = await _customerLCService.GetByLCID(LCID);
                    if (customerlc == null) {
                        var clc = inputDto.MapTo<CustomerLC>();
                        HandleSpecificData.HandleSpecificDataLC(clc, inputDto);
                        clc.CreatorTime = DateTime.Now;
                        clc.CreatorUserDeptId = CurrentUser.DeptId;
                        clc.CreatorUserId = CurrentUser.UserId;
                        clc.CreatorUserOrgId = CurrentUser.OrganizeId;
                        clc.LastModifyTime = DateTime.Now;
                        clc.LastModifyUserId = CurrentUser.UserId;
                        await _customerLCService.InsertAsync(clc, conn, tran);
                    } else {
                        var clc = inputDto.MapTo(customerlc);
                        HandleSpecificData.HandleSpecificDataLC(clc, inputDto);
                        clc.LastModifyTime = DateTime.Now;
                        clc.LastModifyUserId = CurrentUser.UserId;
                        await _customerLCService.UpdateAsync(clc, customerlc.Id, conn, tran);
                    }
                }
                //更新or新增承租自然人
                if (!string.IsNullOrEmpty(RNID)) {
                    var customerrn = await _customerRNService.GetCustomerByRNID(RNID);
                    if (customerrn == null) {
                        var crn = inputDto.MapTo<CustomerRN>();
                        HandleSpecificData.HandleSpecificDataRN(crn, inputDto);
                        crn.CreatorTime = DateTime.Now;
                        crn.CreatorUserDeptId = CurrentUser.DeptId;
                        crn.CreatorUserId = CurrentUser.UserId;
                        crn.CreatorUserOrgId = CurrentUser.OrganizeId;
                        crn.LastModifyTime = DateTime.Now;
                        crn.LastModifyUserId = CurrentUser.UserId;
                        await _customerRNService.InsertAsync(crn, conn, tran);
                    } else {
                        var crn = inputDto.MapTo(customerrn);
                        HandleSpecificData.HandleSpecificDataRN(crn, inputDto);
                        crn.LastModifyTime = DateTime.Now;
                        crn.LastModifyUserId = CurrentUser.UserId;
                        await _customerRNService.UpdateAsync(crn, customerrn.Id, conn, tran);
                    }
                }
                //更新or新增承租法人
                if (!string.IsNullOrEmpty(RCID)) {
                    var customerrc = await _customerRCService.GetWhereAsync($" RCID = '{RCID}' ");
                    if (customerrc == null) {
                        var crc = inputDto.MapTo<CustomerRC>();
                        HandleSpecificData.HandleSpecificDataRC(crc, inputDto);
                        crc.CreatorTime = DateTime.Now;
                        crc.CreatorUserDeptId = CurrentUser.DeptId;
                        crc.CreatorUserId = CurrentUser.UserId;
                        crc.CreatorUserOrgId = CurrentUser.OrganizeId;
                        crc.LastModifyTime = DateTime.Now;
                        crc.LastModifyUserId = CurrentUser.UserId;
                        await _customerRCService.InsertAsync(crc, conn, tran);
                    } else {
                        var crc = inputDto.MapTo(customerrc);
                        HandleSpecificData.HandleSpecificDataRC(crc, inputDto);
                        crc.LastModifyTime = DateTime.Now;
                        crc.LastModifyUserId = CurrentUser.UserId;
                        await _customerRCService.UpdateAsync(crc, customerrc.Id, conn, tran);
                    }
                }
                //更新or新增建物
                if (!string.IsNullOrEmpty(BAdd)) {
                    var building = await _buildingService.GetBuildingByBAddAsync(BAdd);
                    var building1 = await _building1Service.GetByBAdd(BAdd);
                    if (building == null) {
                        var b = inputDto.MapTo<Building>();
                        var b1 = inputDto.MapTo<Building1>();
                        b.CreatorTime = DateTime.Now;
                        b.CreatorUserId = CurrentUser.UserId;
                        b.CreatorUserOrgId = CurrentUser.OrganizeId;
                        b.CreatorUserDeptId = CurrentUser.DeptId;
                        b.LastModifyTime = DateTime.Now;
                        b.LastModifyUserId = CurrentUser.UserId;
                        b.BState = "待招租";
                        b.BelongLID = LNID ?? LCID;
                        HandleSpecificData.HandleSpecificDataBuilding(b, inputDto);
                        b1.Id = b.Id;
                        b1.BAdd = b.BAdd;
                        b1.CreatorUserId = b.CreatorUserId;
                        b1.CreatorTime = b.CreatorTime;
                        b1.CreatorUserDeptId = b.CreatorUserDeptId;
                        b1.CreatorUserOrgId = b.CreatorUserOrgId;
                        await _buildingService.InsertAsync(b, conn, tran);
                        await _building1Service.InsertAsync(b1, conn, tran);
                    } else {
                        var b = inputDto.MapTo(building);
                        var b1 = inputDto.MapTo(building1);
                        b.LastModifyTime = DateTime.Now;
                        b.LastModifyUserId = CurrentUser.UserId;
                        b1.LastModifyTime = DateTime.Now;
                        b1.LastModifyUserId = CurrentUser.UserId;
                        HandleSpecificData.HandleSpecificDataBuilding(b, inputDto);
                        await _buildingService.UpdateAsync(b, building.Id, conn, tran);
                        await _building1Service.UpdateAsync(b1, building1.Id, conn, tran);
                    }
                }

                //更新or新增TBNoB6
                var tbnoB6 = await _tBNoB6Service.GetWhereAsync($"{sqlWhere}");
                if (tbnoB6 == null) {
                    var b6 = inputDto.MapTo<TBNoB6>();
                    b6 = HandleSig(b6, inputDto);
                    b6.FName = formId;
                    b6.RefKeys = refKeys;
                    //b6.IsFiled = IsGenPDf == "Y" ? "Y" : "N";
                    b6.SignatureImgPath_1 = IsGenPDf == "Y" ? null : b6.SignatureImgPath_1;
                    //b6.IsFiled = !string.IsNullOrEmpty(b6.SignatureImgPath_1) ? "Y" : "N";
                    b6.CreatorTime = DateTime.Now;
                    b6.CreatorUserId = CurrentUser.UserId;
                    b6.LastModifyTime = DateTime.Now;
                    b6.LastModifyUserId = CurrentUser.UserId;
                    await _tBNoB6Service.InsertAsync(b6, conn, tran);
                    result = b6.Id;
                } else {
                    var b6 = inputDto.MapTo(tbnoB6);
                    b6 = HandleSig(b6, inputDto);
                    //b6.IsFiled = IsGenPDf == "Y" ? "Y" : "N";
                    b6.SignatureImgPath_1 = IsGenPDf == "Y" ? null : b6.SignatureImgPath_1;
                    //b6.IsFiled = !string.IsNullOrEmpty(b6.SignatureImgPath_1) ? "Y" : "N";
                    await _tBNoB6Service.UpdateAsync(b6, tbnoB6.Id, conn, tran);
                    result = b6.Id;
                }

                //新增匯款資料(出租人)，目前不做檢查 直接新增
                var propInfo = props.Where(p => p.Name == "LCanModify").FirstOrDefault();//是否可更新匯款資料
                bool canModi = propInfo == null ? false : (bool)propInfo.GetValue(inputDto);
                if ((bool)canModi) {
                    if (!string.IsNullOrEmpty(LAName)) {
                        var IDNo = string.IsNullOrEmpty(LNID) ? LCID : LNID;
                        var res = string.IsNullOrEmpty(LNID);
                        string cusomerLId = "";
                        switch (IDNo.Length) {
                            case 10:
                                var cln = await _customerLNService.GetByCustomerLNID(IDNo, conn, tran);
                                cusomerLId = cln.Id;
                                break;
                            case 8:
                                var clc = await _customerLCService.GetByLCID(IDNo, conn, tran);
                                cusomerLId = clc.Id;
                                break;
                            default:
                                break;
                        }
                        var remittanceL = inputDto.MapTo<RemittanceL>();
                        remittanceL.IDNo = IDNo;
                        remittanceL.CustomerLId = cusomerLId;
                        remittanceL.CreatorTime = DateTime.Now;
                        remittanceL.CreatorUserId = CurrentUser.UserId;
                        remittanceL.LastModifyTime = DateTime.Now;
                        remittanceL.LastModifyUserId = CurrentUser.UserId;
                        await _remittanceLService.InsertAsync(remittanceL, conn, tran);
                    }
                }

                //若儲存的表單代號=A000002 且 不是暫存(也就是匯出PDF)的話要新增至黑名單
                if (formId == "A000002" && IsGenPDf == "Y") {
                    var isErr = await InsertBlackList(inputDto.MapTo<A000002InputDto>(), conn, tran);
                    if (isErr == "Err") {
                        tran.Rollback();
                        return "送審失敗!";
                    }
                }
                tran.Commit();
            } catch (Exception) {
                tran.Rollback();
                throw;
            }
            return result;
        }

        /// <summary>
        /// 根據傳進來的簽名檔轉成實體檔案並儲存起來
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tBNoB6"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public TBNoB6 HandleSig<T>(TBNoB6 tBNoB6, T inputDto)
        {
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            string basePath = @$"{sysSetting.ChaochiFilepath}\";
            string dirPath = @$"Singature\{tBNoB6.Id}\";
            string sig1 = "";
            string sig2 = "";
            string sig3 = "";
            string sig4 = "";
            string sig5 = "";
            var t = typeof(T);
            var props = t.GetProperties();
            foreach (var prop in props) {
                if (prop.Name == "SignatureBase64_1") {
                    sig1 = prop.GetValue(inputDto)?.ToString();
                }
                if (prop.Name == "SignatureBase64_2") {
                    sig2 = prop.GetValue(inputDto)?.ToString();
                }
                if (prop.Name == "SignatureBase64_3") {
                    sig3 = prop.GetValue(inputDto)?.ToString();
                }
                if (prop.Name == "SignatureBase64_4") {
                    sig4 = prop.GetValue(inputDto)?.ToString();
                }
                if (prop.Name == "SignatureBase64_5") {
                    sig5 = prop.GetValue(inputDto)?.ToString();
                }
            }

            if (!string.IsNullOrEmpty(sig1)) {
                try {
                    if (!string.IsNullOrEmpty(sig1) && sig1.IndexOf(',') > -1) {
                        tBNoB6.SignatureImgPath_1 = dirPath + $"{tBNoB6.FName}_1.jpg";
                        Directory.CreateDirectory(basePath + dirPath);
                        System.IO.File.WriteAllBytes(@$"{basePath}\{tBNoB6.SignatureImgPath_1}", Convert.FromBase64String(sig1.Split(",")[1]));
                    }
                } catch (Exception ex) {
                    Log4NetHelper.Error("", ex);
                }
            }
            return tBNoB6;
        }

        /// <summary>
        /// 根據傳入rId(有可能為10碼 or8碼)是否存在此承租人+是否可以存取此承租人(為了匯款資料)
        /// </summary>
        /// <param name="rId"></param>
        /// <returns></returns>
        /// 
        [HttpPost("CheckCustomerRNExists")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> CheckCustomerRNExists(string rId)
        {
            var result = new CommonResult();
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            if (rId.Length == 10) {
                var customerrn = await _customerRNService.GetCustomerByRNID(rId);
                if (customerrn is null) {
                    result.ResData = false;
                } else {
                    if (!customerrn.CreatorUserId.Contains(CurrentUser.UserId)) {
                        result.ResData = "deny";
                    } else {
                        result.ResData = true;
                    }
                }
            } else if (rId.Length == 8) {
                var customerrc = await _customerRCService.GetWhereAsync($" RCID = '{rId}' ");
                if (customerrc is null) {
                    result.ResData = false;
                } else {
                    if (!customerrc.CreatorUserId.Contains(CurrentUser.UserId)) {
                        result.ResData = "deny";
                    } else {
                        result.ResData = true;
                    }
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 根據傳入lId(有可能為10碼 or8碼)是否存在此出租人+是否可以存取此出租人(為了匯款資料)
        /// </summary>
        /// <param name="lId"></param>
        /// <returns></returns>
        /// 
        [HttpPost("CheckCustomerLNExists")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> CheckCustomerLNExists(string lId)
        {
            var result = new CommonResult();
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            if (lId.Length == 10) {
                var customerln = await _customerLNService.GetByCustomerLNID(lId);
                if (customerln is null) {
                    result.ResData = false;
                } else {
                    if (!customerln.CreatorUserId.Contains(CurrentUser.UserId)) {
                        result.ResData = "deny";
                    } else {
                        result.ResData = true;
                    }
                }
            } else if (lId.Length == 8) {
                var customerlc = await _customerLCService.GetByLCID(lId);
                if (customerlc is null) {
                    result.ResData = false;
                } else {
                    if (!customerlc.CreatorUserId.Contains(CurrentUser.UserId)) {
                        result.ResData = "deny";
                    } else {
                        result.ResData = true;
                    }
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 根據傳入lId(有可能為10碼 or8碼)是否存在此出租人
        /// </summary>
        /// <param name="bAdd"></param>
        /// <returns></returns>
        /// 
        [HttpPost("CheckCustomerBbuildingExists")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> CheckCustomerBbuildingExists(string bAdd)
        {
            var result = new CommonResult();
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            var building = await _buildingService.GetBuildingByBAddAsync(bAdd);
            if (building is null) {
                result.ResData = false;
            } else {
                result.ResData = true;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 檢查登入者下載權限
        /// </summary>
        /// <param name="searchPDF"></param>
        /// <returns></returns>
        private async Task<CommonResult> CheckAuth(SearchExternalformPDFModel searchPDF)
        {
            var result = new CommonResult();
            bool isLN = false, isLC = false;
            if (!string.IsNullOrEmpty(searchPDF.LNID)) {
                isLN = true;
            }
            if (!string.IsNullOrEmpty(searchPDF.LCID)) {
                isLC = true;
            }

            //檢查房東歸屬權
            if (isLN) {
                var checkLN = await _customerLNService.GetByCustomerLNID(searchPDF.LNID);
                if (checkLN != null) {
                    //var s = _landLordBelongingService.GetListWhere($"LId='{dictV["LNID"]}'").Where(x => x.SalesId == CurrentUser.UserId);
                    var s = checkLN.CreatorUserId.Contains(CurrentUser.UserId);
                    if (!s) {
                        result.Success = false;
                        result.ErrMsg = $"你沒有操作此房東的權限";
                        return result;
                    }
                }
            } else if (isLC) {
                var checkLC = await _customerLCService.GetByLCID(searchPDF.LCID);
                if (checkLC != null) {
                    //var s = _landLordBelongingService.GetListWhere($"LId='{dictV["LCID"]}'").Where(x => x.SalesId == CurrentUser.UserId);
                    var s = checkLC.CreatorUserId.Contains(CurrentUser.UserId);
                    if (!s) {
                        result.Success = false;
                        result.ErrMsg = $"你沒有操作此房東的權限";
                        return result;
                        //throw new Exception("你沒有操作此房東的權限");
                    }
                }
            }
            //檢查建物歸屬權
            if (!string.IsNullOrEmpty(searchPDF.BAdd_1)) {
                string BAdd = "";
                if (searchPDF.BAdd_1.Length <= 3) {
                    BAdd = Utils.memergeAddFix(
                       searchPDF.BAdd_1,
                       searchPDF.BAdd_1_1,
                       searchPDF.BAdd_1_2,
                       searchPDF.BAdd_2,
                       searchPDF.BAdd_2_1,
                       searchPDF.BAdd_2_2,
                       searchPDF.BAdd_2_3,
                       searchPDF.BAdd_2_4,
                       searchPDF.BAdd_3,
                       searchPDF.BAdd_3_1,
                       searchPDF.BAdd_3_2,
                       searchPDF.BAdd_4,
                       searchPDF.BAdd_5,
                       searchPDF.BAdd_6,
                       searchPDF.BAdd_7,
                       searchPDF.BAdd_8,
                       searchPDF.BAdd_9);
                }
                var checkB = await _buildingService.GetWhereAsync($"BAdd='{BAdd}'");
                if (checkB != null) {
                    if (checkB.CreatorUserId != CurrentUser.UserId) {
                        result.Success = false;
                        result.ErrMsg = $"你沒有操作此建物的權限";
                        return result;
                        //throw new Exception("你沒有操作此建物的權限");
                    }
                    if (!string.IsNullOrEmpty(searchPDF.LNID)) {
                        if (checkB.BelongLID != searchPDF.LNID) {
                            result.Success = false;
                            result.ErrMsg = $"你沒有操作此房東對應之建物的權限";
                            return result;
                            //throw new Exception("你沒有操作此建物對應之房東的權限");
                        }
                    } else if (!string.IsNullOrEmpty(searchPDF.LCID)) {
                        if (checkB.BelongLID != searchPDF.LCID) {
                            result.Success = false;
                            result.ErrMsg = $"你沒有操作此房東對應之建物的權限";
                            return result;
                            //throw new Exception("你沒有操作此建物對應之房東的權限");
                        }
                    }
                }
            }
            //檢查房客歸屬權
            if (!string.IsNullOrEmpty(searchPDF.RNID)) {
                var checkRN = await _customerRNService.GetCustomerByRNID(searchPDF.RNID);
                if (checkRN != null) {
                    //var s = _landLordBelongingService.GetListWhere($"LId='{dictV["LNID"]}'").Where(x => x.SalesId == CurrentUser.UserId);
                    var s = checkRN.CreatorUserId.Contains(CurrentUser.UserId);
                    if (!s) {
                        result.Success = false;
                        result.ErrMsg = $"你沒有操作此房客的權限";
                        return result;
                        //throw new Exception("你沒有操作此房東的權限");
                    }
                }
            }
            return new CommonResult { Success = true };
        }


        /// <summary>
        /// 下載Web表單歷史存檔
        /// </summary>
        /// <param name="searchPDF"></param>
        /// <returns></returns>
        [HttpPost("downloadWebFormHistoryFormById")]
        [LogNotMethod]
        public async Task<IActionResult> downloadWebFormHistoryFormById(SearchExternalformPDFModel searchPDF)
        {
            string FormId = searchPDF.FormId;
            string LNId = searchPDF.LNID;
            string LCId = searchPDF.LCID;
            string BAdd = Utils.memergeAddFix(
                   searchPDF.BAdd_1,
                   searchPDF.BAdd_1_1,
                   searchPDF.BAdd_1_2,
                   searchPDF.BAdd_2,
                   searchPDF.BAdd_2_1,
                   searchPDF.BAdd_2_2,
                   searchPDF.BAdd_2_3,
                   searchPDF.BAdd_2_4,
                   searchPDF.BAdd_3,
                   searchPDF.BAdd_3_1,
                   searchPDF.BAdd_3_2,
                   searchPDF.BAdd_4,
                   searchPDF.BAdd_5,
                   searchPDF.BAdd_6,
                   searchPDF.BAdd_7,
                   searchPDF.BAdd_8,
                   searchPDF.BAdd_9);
            string RNId = searchPDF.RNID;
            string RCId = searchPDF.RCID;
            string sqlWhere = $" 1=1 and FName = '{FormId}'  ";
            string refKeys = LNId + LCId + RNId + BAdd;
            if (!string.IsNullOrEmpty(refKeys)) {
                sqlWhere += $" and RefKeys = '{refKeys}' ";
            }

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();


            var b6 = await _tBNoB6Service.GetWhereAsync($"{sqlWhere}");
            var path = @$"{sysSetting.ChaochiFilepath}historyPDF\WebForm\{b6.Id}\";
            var fileName = b6.Id + ".pdf";
            var filePath = Path.Combine(path, fileName);
            var pdf = System.IO.File.OpenRead(@$"{filePath}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
            return File(pdf, contentType ?? "image/jpeg", "aaa.pdf");
        }

        /// <summary>
        ///  單文件上傳接口
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("ImgUpload")]
        [NoSignRequired]
        public async Task<IActionResult> ImgUpload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            string id = formCollection["id"].ToString();
            string role = formCollection["role"].ToString();
            string IDno = formCollection["IDNo"].ToString();
            string formName = formCollection["formname"].ToString();
            string BAdd = formCollection["BAdd"].ToString();
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            try {
                string cId = "";
                switch (role) {
                    case "LN":
                        var cln = await _customerLNService.GetByCustomerLNID(IDno);
                        cId = cln.Id;
                        break;
                    case "LC":
                        var clc = await _customerLCService.GetByLCID(IDno);
                        cId = clc.Id;
                        break;
                    case "RN":
                        var crn = await _customerRNService.GetCustomerByRNID(IDno);
                        cId = crn.Id;
                        break;
                    case "RC":
                        var crc = await _customerRCService.GetWhereAsync($" RCID = '{IDno}' ");
                        cId = crc.Id;
                        break;
                    case "Building":
                        var building = await _buildingService.GetBuildingByBAddAsync(BAdd);
                        IDno = building.Id; //歷史表單目錄中 建物較特別 目錄名稱為Id 出租人跟承租人為身份證字號/統一編號
                        break;
                    default:
                        break;
                }
                var Addres = Add(filelist[0], belongApp, belongAppId, role, IDno, id);
                switch (role) {
                    case "LN":
                        var hisFormL = new HistoryFormLN() {
                            IDNo = IDno,
                            FormName = formName,
                            Note = "Web表單歷史存檔上傳",
                            UploadTime = DateTime.Now,
                            FileName = Addres.FileName,
                            CreatorUserId = CurrentUser.UserId
                        };
                        await _historyFormLNService.InsertAsync(hisFormL);
                        break;
                    case "LC":
                        var hisFormLc = new HistoryFormLC() {
                            IDNo = IDno,
                            FormName = formName,
                            Note = "Web表單歷史存檔上傳",
                            UploadTime = DateTime.Now,
                            FileName = Addres.FileName,
                            CreatorUserId = CurrentUser.UserId
                        };
                        await _historyFormLCService.InsertAsync(hisFormLc);
                        break;
                    case "RN":
                        var hisFormR = new HistoryFormRN() {
                            IDNo = IDno,
                            FormName = formName,
                            Note = "Web表單歷史存檔上傳",
                            UploadTime = DateTime.Now,
                            FileName = Addres.FileName,
                            CreatorUserId = CurrentUser.UserId
                        };
                        await _historyFormRNService.InsertAsync(hisFormR);
                        break;
                    case "RC":
                        var hisFormRC = new HistoryFormRC() {
                            IDNo = IDno,
                            FormName = formName,
                            Note = "Web表單歷史存檔上傳",
                            UploadTime = DateTime.Now,
                            FileName = Addres.FileName,
                            CreatorUserId = CurrentUser.UserId
                        };
                        await _historyFormRCService.InsertAsync(hisFormRC);
                        break;
                    case "Building":
                        var b = await _buildingService.GetBuildingByBAddAsync(BAdd);
                        var hisFormB = new HistoryFormBuilding() {
                            IDNo = b.Id,
                            FormName = formName,
                            Note = "Web表單歷史存檔上傳",
                            UploadTime = DateTime.Now,
                            FileName = Addres.FileName,
                            CreatorUserId = CurrentUser.UserId
                        };
                        await _historyFormBuildingService.InsertAsync(hisFormB);
                        break;
                    default:
                        break;
                }

                //result.ResData = Addres;
                //var historyform = new HistoryFormLN() {
                //    IDNo = LNID,
                //    FormName = string.IsNullOrEmpty(formCollection["formname"]) ? Addres.FileName.Remove(0, 14) : formCollection["formname"],
                //    Note = formCollection["note"],
                //    UploadTime = DateTime.Now,
                //    FileName = Addres.FileName
                //};
                //_historyFormService.InsertAsync(historyform);
                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                throw;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 單個上傳文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="belongApp"></param>
        /// <param name="belongAppId"></param>
        /// <param name="role"></param>
        /// <param name="cId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private Yuebon.Security.Dtos.UploadFileResultOuputDto Add(IFormFile file, string belongApp, string belongAppId, string role, string cId, string id)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = file.FileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    var filenameExt = UploadFile(fileName, data, role, cId, id);

                    Yuebon.Security.Dtos.UploadFileResultOuputDto uploadFileResultOuputDto = new Yuebon.Security.Dtos.UploadFileResultOuputDto {
                        FileName = filenameExt,
                    };
                    return uploadFileResultOuputDto;
                }
            } else {
                Log4NetHelper.Info("文件過大");
                throw new Exception("文件過大");
            }
        }

        /// <summary>
        /// 實現文件上傳到服務器保存，并生成縮略圖
        /// </summary>
        /// <param name="fileName">文件名稱</param>
        /// <param name="fileBuffers">文件字節流</param>
        /// <param name="role"></param>
        /// <param name="cId"></param>
        /// <param name="id"></param>
        private string UploadFile(string fileName, byte[] fileBuffers, string role, string cId, string id)
        {

            //判斷文件是否為空
            if (string.IsNullOrEmpty(fileName)) {
                Log4NetHelper.Info("文件名不能為空");
                throw new Exception("文件名不能為空");
            }

            //判斷文件是否為空
            if (fileBuffers.Length < 1) {
                Log4NetHelper.Info("文件不能為空");
                throw new Exception("文件不能為空");
            }

            fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileName;

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            string uploadPath = @$"{path}historyPDF\";
            switch (role) {
                case "LN":
                    uploadPath += @$"CustomerL\LN\{cId}\";
                    break;
                case "LC":
                    uploadPath += @$"CustomerL\LC\{cId}\";
                    break;
                case "RN":
                    uploadPath += @$"CustomerR\RN\{cId}\"; ;
                    break;
                case "RC":
                    uploadPath += @$"CustomerR\RC\{cId}\"; ;
                    break;
                case "Building":
                    uploadPath += @$"Building\{cId}\";
                    break;
                default:
                    break;
            }

            if (!Directory.Exists(uploadPath)) {
                Directory.CreateDirectory($"{uploadPath}");
            }

            var savePath = Path.Combine(uploadPath, fileName);

            using (var fs = new FileStream(savePath, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }
            return fileName;
        }


        /// <summary>
        /// 更新WEB表單
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        /// 
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(InternalformInPutDto inInfo)
        {
            var result = new CommonResult();
            var interFormDB = await iService.GetAsync(inInfo.Id);
            var interForm = inInfo.MapTo(interFormDB);

            OnBeforeUpdate(interForm);
            var isSuccess = iService.Update(interForm, inInfo.Id);
            if (isSuccess) {
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } else {
                result.Success = false;
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 取得圖檔
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [HttpGet("GetImg")]
        [YuebonAuthorize("List")]
        [LogNotMethod]
        public IActionResult GetImage(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return new EmptyResult();
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

            var image = System.IO.File.OpenRead($"{sysSetting.ChaochiFilepath}{filePath}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
            return File(image, contentType ?? "image/jpeg");
        }

        [HttpPost("FindFormList")]
        [YuebonAuthorize("List")]
        [LogNotMethod]
        public async Task<IActionResult> FindFormList(SearchInternalformModel inputData)
        {
            CommonResult<PageResult<InternalformOutputDto>> result = new CommonResult<PageResult<InternalformOutputDto>>();

            var data = await iService.FindFormListWithPage(inputData);
            inputData.BAdd =
               Utils.memergeAddFix(
                  inputData.BAdd_1,
                  inputData.BAdd_1_1,
                  inputData.BAdd_1_2,
                  inputData.BAdd_2,
                  inputData.BAdd_2_1,
                  inputData.BAdd_2_2,
                  inputData.BAdd_2_3,
                  inputData.BAdd_2_4,
                  inputData.BAdd_3,
                  inputData.BAdd_3_1,
                  inputData.BAdd_3_2,
                  inputData.BAdd_4,
                  inputData.BAdd_5,
                  inputData.BAdd_6,
                  inputData.BAdd_7,
                  inputData.BAdd_8,
                  inputData.BAdd_9
               );
            if (!string.IsNullOrEmpty(inputData.BAdd)) {
                var tbAlDb = await _tBNoA1Repos.GetListByBAdd(inputData.BAdd);
                foreach (var r in data.Items) {
                    var sameData = tbAlDb.Where(w => w.FName == r.FormId).FirstOrDefault();
                    if (sameData != null) {
                        r.IsSign = sameData.IsSign;
                        r.IsFileing = sameData.IsFileing;
                        r.DataExist = true;
                    }
                }
            }

            result.ResData = data;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        ///  上傳模板PDF
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("UploadPDFTemplate")]
        [NoSignRequired]
        public async Task<IActionResult> UploadPDFTemplateAsync([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            //string _fileName = filelist[0].FileName;
            try {
                string FormId = "";
                if (formCollection.ContainsKey("FormId")) {
                    FormId = formCollection["FormId"];
                }

                if (filelist == null || filelist.Count <= 0 || filelist.Count >= 2) {
                    result.ErrCode = "40001";
                    result.ErrMsg = "只能有一個上傳PDF檔案";
                    return ToJsonContent(result, true);
                }

                Dictionary<string, string> dictV = new Dictionary<string, string>();
                Dictionary<string, string> dictTU = new Dictionary<string, string>();
                IFormFile file = filelist[0];
                if (file != null && file.Length > 0) {
                    using (var doc = PdfReader.Open(file.OpenReadStream(), PdfDocumentOpenMode.Modify)) {
                        if (doc.AcroForm == null) {
                            doc.Close();
                            result.ErrCode = "40001";
                            result.ErrMsg = "上傳PDF檔案不含任何標籤";
                            return ToJsonContent(result, true);
                        } else {
                            //取值
                            foreach (var name in doc.AcroForm.Fields.Names) {
                                Console.WriteLine($"{name}:{doc.AcroForm.Fields[name].Value}");
                                if (doc.AcroForm.Fields[name].Value is PdfString) {
                                    if (doc.AcroForm.Fields[name].Value.ToString() == "<FEFF>") {
                                        //Console.WriteLine($"PdfString:{name} is <FEFF>:={((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value}");
                                        dictV.Add(name, "");
                                        dictTU.Add(name, ((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value);
                                    } else {
                                        //Console.WriteLine($"PdfString:{name}:{((PdfString)doc.AcroForm.Fields[name].Value).Value}:={((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value}");
                                        dictV.Add(name, ((PdfString)doc.AcroForm.Fields[name].Value).Value);
                                        dictTU.Add(name, ((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value);
                                    }
                                } else if (doc.AcroForm.Fields[name].Value is PdfItem) {
                                    //Console.WriteLine($"PdfString:{name}:{((PdfString)doc.AcroForm.Fields[name].Value).Value}:={((PdfString)doc.AcroForm.Fields[name]?.Elements["/TU"])?.Value}");
                                    dictV.Add(name, ((PdfItem)doc.AcroForm.Fields[name].Value).ToString());
                                    dictTU.Add(name, ((PdfItem)doc.AcroForm.Fields[name]?.Elements["/TU"])?.ToString());
                                }
                            }
                        }
                    }
                }

                //if (!dictV.ContainsKey("FName")) {
                //    result.ErrCode = "40001";
                //    result.ErrMsg = "上傳PDF檔案不含標籤[FName]";
                //    return ToJsonContent(result, true);
                //} else {
                //    if (dictV["FName"] != FormId) {
                //        result.ErrCode = "40001";
                //        result.ErrMsg = $"上傳PDF檔案標籤[FName]不為{FormId}";
                //        return ToJsonContent(result, true);
                //    }
                //}

                //if (!dictV.ContainsKey("Vno")) {
                //    result.ErrCode = "40001";
                //    result.ErrMsg = "上傳PDF檔案不含標籤[Vno]";
                //    return ToJsonContent(result, true);
                //} else {
                //    Externalform Externalform_DB = await iService.GetByFormId(FormId);
                //    if (Externalform_DB == null) {
                //        result.ErrCode = "40001";
                //        result.ErrMsg = $"FormId:{FormId}對應資料尚未建立!";
                //        return ToJsonContent(result, true);
                //    }

                //    if (dictV["Vno"] != Externalform_DB.Vno) {
                //        result.ErrCode = "40001";
                //        result.ErrMsg = $"上傳PDF檔案[版本號]標籤[Vno]不為{Externalform_DB.Vno}";
                //        return ToJsonContent(result, true);
                //    }
                //}

                Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
                Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
                string uploadPath = sysSetting.ChaochiFilepath + "TemplatePDF/";//這邊之後加工需要不同的目錄

                result.ResData = AddFile(filelist[0], uploadPath, FormId);

                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 單個上傳文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="uploadPath"></param>
        /// <param name="FormId"></param>
        /// <returns></returns>
        private Yuebon.Security.Dtos.UploadFileResultOuputDto AddFile(IFormFile file, string uploadPath, string FormId)
        {
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = string.IsNullOrWhiteSpace(FormId) ? file.FileName : FormId + ".pdf";

                    var data = binaryReader.ReadBytes((int)file.Length);
                    UploadFile(fileName, uploadPath, data);

                    Yuebon.Security.Dtos.UploadFileResultOuputDto uploadFileResultOuputDto = new Yuebon.Security.Dtos.UploadFileResultOuputDto {
                        FileName = fileName,
                    };
                    return uploadFileResultOuputDto;
                }
            } else {
                Log4NetHelper.Info("文件過大");
                throw new Exception("文件過大");
            }
        }

        /// <summary>
        /// 實現文件上傳到服務器保存，并生成縮略圖
        /// </summary>
        /// <param name="fileName">文件名稱</param>
        /// <param name="uploadPath"></param>
        /// <param name="fileBuffers">文件字節流</param>
        private void UploadFile(string fileName, string uploadPath, byte[] fileBuffers)
        {

            //判斷文件是否為空
            if (string.IsNullOrEmpty(fileName)) {
                Log4NetHelper.Info("文件名不能為空");
                throw new Exception("文件名不能為空");
            }

            //判斷文件是否為空
            if (fileBuffers.Length < 1) {
                Log4NetHelper.Info("文件不能為空");
                throw new Exception("文件不能為空");
            }

            if (!System.IO.Directory.Exists(uploadPath)) {
                System.IO.Directory.CreateDirectory(uploadPath);
            }

            using (var fs = new FileStream(uploadPath + fileName, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }
        }

        /// <summary>
        /// 下載
        /// </summary>
        /// <param name="searchPDF"></param>
        /// <returns></returns>
        [HttpPost("downloadPDFWithDataByFormId")]
        [YuebonAuthorize("List")]
        [LogNotMethod]
        //public IActionResult Get(string fileName)
        public async Task<IActionResult> DownloadPDFWithDataByFormId(SearchWebFormPDF searchPDF)
        {
            CommonResult result = new CommonResult();
            string FormId = searchPDF.FormId;
            string LNId = searchPDF.LNID;
            string LCId = searchPDF.LCID;
            string BAdd = searchPDF.BAdd;
            string RNId = searchPDF.RNID;
            string RCId = searchPDF.RCID;
            string refKeys = LNId + LCId + RNId + BAdd;

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

            MemoryStream PDFstream = new MemoryStream();
            using (var doc = PdfReader.Open($"{sysSetting.ChaochiFilepath}TemplatePDF/{FormId}.pdf", PdfDocumentOpenMode.Modify)) {
                if (doc.AcroForm == null) {
                    doc.Close();
                    result.ErrCode = "40001";
                    result.ErrMsg = "上傳PDF檔案不含任何標籤";
                    return ToJsonContent(result, true);
                } else {

                    WebFormDataModel webFormDataModel = await GetData(searchPDF);
                    if (doc.AcroForm.Elements.ContainsKey("/NeedAppearances")) {
                        doc.AcroForm.Elements["/NeedAppearances"] = new PdfBoolean(true);
                    } else {
                        doc.AcroForm.Elements.Add("/NeedAppearances", new PdfBoolean(true));
                    }

                    Dictionary<string, string> dictV = new Dictionary<string, string>();
                    Dictionary<string, object> dictFinal =
                            webFormDataModel.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .ToDictionary(prop => prop.Name, prop => prop.GetValue(webFormDataModel, null));
                    //設值
                    foreach (var name in doc.AcroForm.Fields.Names) {

                        //Console.WriteLine($"{name}:{doc.AcroForm.Fields[name].Value}");
                        if (name.StartsWith("Image")) {
                            try {
                                // 簽名圖檔

                                var tbNoB6 = await _tBNoB6Service.GetWhereAsync($" FName = '{FormId}' and Refkeys='{refKeys}'");
                                string path = @$"{sysSetting.ChaochiFilepath}\Singature\{tbNoB6.Id}\";
                                var files = System.IO.Directory.GetFiles(path).Where(s => s.Contains("_1")).FirstOrDefault(); //簽名檔名包含_1代表簽名
                                /* draw image on pdf */
                                PdfRectangle rect = doc.AcroForm.Fields["Image"].Elements.GetRectangle(PdfAnnotation.Keys.Rect);
                                PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(doc.Pages[0]);
                                XImage image = XImage.FromFile(files);
                                double w = rect.Width, h = rect.Height;
                                ImageScaling(Math.Max(rect.Height, rect.Width) - 10, ref w, ref h);
                                gfx.DrawImage(image, rect.X1, doc.Pages[0].Height - rect.Y2, w, h);
                            } catch (Exception e) {
                                Console.WriteLine(e.Message + " " + e.StackTrace);
                            }

                        } else if (doc.AcroForm.Fields[name] is PdfTextField) {
                            if (name == "FName" || name == "Vno" || name == "TBNO") {

                            } else {
                                if (doc.AcroForm.Fields[name].ReadOnly == true) {
                                    doc.AcroForm.Fields[name].ReadOnly = false;
                                    doc.AcroForm.Fields[name].Value = new PdfString(dictFinal.ContainsKey(name) ? (string)dictFinal[name] : "");
                                    doc.AcroForm.Fields[name].ReadOnly = true;
                                } else {
                                    doc.AcroForm.Fields[name].ReadOnly = false;
                                    doc.AcroForm.Fields[name].Value = new PdfString(dictFinal.ContainsKey(name) ? (string)dictFinal[name] : "");
                                }
                            }
                        } else if (doc.AcroForm.Fields[name].Value is PdfItem) {
                            var test = dictFinal[name];
                            if (doc.AcroForm.Fields[name].ReadOnly == true) {
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
                                        ((PdfCheckBoxField)(doc.AcroForm.Fields[name])).Checked = true;
                                    } else {
                                        ((PdfCheckBoxField)doc.AcroForm.Fields[name]).Checked = false;
                                    }
                                }
                                doc.AcroForm.Fields[name].ReadOnly = true;
                            } else {
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
                                        ((PdfCheckBoxField)(doc.AcroForm.Fields[name])).Checked = true;
                                    } else {
                                        ((PdfCheckBoxField)doc.AcroForm.Fields[name]).Checked = false;
                                    }
                                }
                            }
                            //doc.AcroForm.Fields[name].Value = new PdfCheckBoxField(dictV.ContainsKey(name) ? dictV["name"] : "");
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


        private async Task<WebFormDataModel> GetData(SearchWebFormPDF searchPDF)
        {
            WebFormDataModel webformDataModel = new WebFormDataModel();

            #region CustomerLN
            if (!string.IsNullOrWhiteSpace(searchPDF.LNID)) {
                CustomerLN CustomerLN_DB = await _customerLNService.GetByCustomerLNID(searchPDF.LNID);

                if (CustomerLN_DB != null) {
                    webformDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webformDataModel, (CustomerLN_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(CustomerLN_DB, null))));
                    var dict = new Dictionary<string, object>();
                    {
                        #region LS欄位額外賦值
                        dict["LSName"] = CustomerLN_DB.LNName;
                        //身分證字號
                        dict["LSID"] = CustomerLN_DB.LNID;
                        dict["LSID_1_1"] = CustomerLN_DB.LNID_1_1;
                        dict["LSID_1_2"] = CustomerLN_DB.LNID_1_2;
                        dict["LSID_1_3"] = CustomerLN_DB.LNID_1_3;
                        dict["LSID_1_4"] = CustomerLN_DB.LNID_1_4;
                        dict["LSID_1_5"] = CustomerLN_DB.LNID_1_5;
                        dict["LSID_1_6"] = CustomerLN_DB.LNID_1_6;
                        dict["LSID_1_7"] = CustomerLN_DB.LNID_1_7;
                        dict["LSID_1_8"] = CustomerLN_DB.LNID_1_8;
                        dict["LSID_1_9"] = CustomerLN_DB.LNID_1_9;
                        dict["LSID_1_10"] = CustomerLN_DB.LNID_1_10;
                        //信箱
                        dict["LSMail"] = CustomerLN_DB.LNMail;
                        //性別
                        dict["LSGender1"] = CustomerLN_DB.LNGender1;
                        dict["LSGender2"] = CustomerLN_DB.LNGender2;
                        //生日
                        dict["LSBirthday_D"] = CustomerLN_DB.LNBirthday_D;
                        dict["LSBirthday_M"] = CustomerLN_DB.LNBirthday_M;
                        dict["LSBirthday_Y"] = CustomerLN_DB.LNBirthday_Y;
                        //住址
                        dict["LSAdd"] = CustomerLN_DB.LNAdd;
                        dict["LSAdd_1"] = CustomerLN_DB.LNAdd_1;
                        dict["LSAdd_1_1"] = CustomerLN_DB.LNAdd_1_1;
                        dict["LSAdd_1_2"] = CustomerLN_DB.LNAdd_1_2;
                        dict["LSAdd_2"] = CustomerLN_DB.LNAdd_2;
                        dict["LSAdd_2_1"] = CustomerLN_DB.LNAdd_2_1;
                        dict["LSAdd_2_2"] = CustomerLN_DB.LNAdd_2_2;
                        dict["LSAdd_2_3"] = CustomerLN_DB.LNAdd_2_3;
                        dict["LSAdd_2_4"] = CustomerLN_DB.LNAdd_2_4;
                        dict["LSAdd_3"] = CustomerLN_DB.LNAdd_3;
                        dict["LSAdd_3_1"] = CustomerLN_DB.LNAdd_3_1;
                        dict["LSAdd_3_2"] = CustomerLN_DB.LNAdd_3_2;
                        dict["LSAdd_4"] = CustomerLN_DB.LNAdd_4;
                        dict["LSAdd_5"] = CustomerLN_DB.LNAdd_5;
                        dict["LSAdd_6"] = CustomerLN_DB.LNAdd_6;
                        dict["LSAdd_7"] = CustomerLN_DB.LNAdd_7;
                        dict["LSAdd_8"] = CustomerLN_DB.LNAdd_8;
                        dict["LSAdd_9"] = CustomerLN_DB.LNAdd_9;
                        dict["LSAddSame"] = CustomerLN_DB.LNAddSame;
                        //電話
                        dict["LSTel_1"] = CustomerLN_DB.LNTel_1;
                        dict["LSTel_2"] = CustomerLN_DB.LNTel_2;
                        dict["LSTel"] = CustomerLN_DB.LNTel;
                        dict["LSTelN_1"] = CustomerLN_DB.LNTelN_1;
                        dict["LSTelN_2"] = CustomerLN_DB.LNTelN_2;
                        dict["LSTelN"] = CustomerLN_DB.LNTelN;
                        //手機
                        dict["LSCell"] = CustomerLN_DB.LNCell;
                        //代理人1
                        dict["LSAGName_A"] = CustomerLN_DB.LNAGName_A;
                        dict["LSAGAdd_1_A"] = CustomerLN_DB.LNAGAdd_1_A;
                        dict["LSAGAdd_1_1_A"] = CustomerLN_DB.LNAGAdd_1_1_A;
                        dict["LSAGAdd_1_2_A"] = CustomerLN_DB.LNAGAdd_1_2_A;
                        dict["LSAGAdd_2_A"] = CustomerLN_DB.LNAGAdd_2_A;
                        dict["LSAGAdd_2_1_A"] = CustomerLN_DB.LNAGAdd_2_1_A;
                        dict["LSAGAdd_2_2_A"] = CustomerLN_DB.LNAGAdd_2_2_A;
                        dict["LSAGAdd_2_3_A"] = CustomerLN_DB.LNAGAdd_2_3_A;
                        dict["LSAGAdd_2_4_A"] = CustomerLN_DB.LNAGAdd_2_4_A;
                        dict["LSAGAdd_3_A"] = CustomerLN_DB.LNAGAdd_3_A;
                        dict["LSAGAdd_3_1_A"] = CustomerLN_DB.LNAGAdd_3_1_A;
                        dict["LSAGAdd_3_2_A"] = CustomerLN_DB.LNAGAdd_3_2_A;
                        dict["LSAGAdd_4_A"] = CustomerLN_DB.LNAGAdd_4_A;
                        dict["LSAGAdd_5_A"] = CustomerLN_DB.LNAGAdd_5_A;
                        dict["LSAGAdd_6_A"] = CustomerLN_DB.LNAGAdd_6_A;
                        dict["LSAGAdd_7_A"] = CustomerLN_DB.LNAGAdd_7_A;
                        dict["LSAGAdd_8_A"] = CustomerLN_DB.LNAGAdd_8_A;
                        dict["LSAGAdd_9_A"] = CustomerLN_DB.LNAGAdd_9_A;
                        dict["LSAGCell_A"] = CustomerLN_DB.LNAGCell_A;
                        dict["LSAGTel_1_A"] = CustomerLN_DB.LNAGTel_1_A;
                        dict["LSAGTel_2_A"] = CustomerLN_DB.LNAGTel_2_A;
                        dict["LSAGID_A"] = CustomerLN_DB.LNAGID_A;
                        //代理人2
                        dict["LSAGName_B"] = CustomerLN_DB.LNAGName_B;
                        dict["LSAGAdd_1_B"] = CustomerLN_DB.LNAGAdd_1_B;
                        dict["LSAGAdd_1_1_B"] = CustomerLN_DB.LNAGAdd_1_1_B;
                        dict["LSAGAdd_1_2_B"] = CustomerLN_DB.LNAGAdd_1_2_B;
                        dict["LSAGAdd_2_B"] = CustomerLN_DB.LNAGAdd_2_B;
                        dict["LSAGAdd_2_1_B"] = CustomerLN_DB.LNAGAdd_2_1_B;
                        dict["LSAGAdd_2_2_B"] = CustomerLN_DB.LNAGAdd_2_2_B;
                        dict["LSAGAdd_2_3_B"] = CustomerLN_DB.LNAGAdd_2_3_B;
                        dict["LSAGAdd_2_4_B"] = CustomerLN_DB.LNAGAdd_2_4_B;
                        dict["LSAGAdd_3_B"] = CustomerLN_DB.LNAGAdd_3_B;
                        dict["LSAGAdd_3_1_B"] = CustomerLN_DB.LNAGAdd_3_1_B;
                        dict["LSAGAdd_3_2_B"] = CustomerLN_DB.LNAGAdd_3_2_B;
                        dict["LSAGAdd_4_B"] = CustomerLN_DB.LNAGAdd_4_B;
                        dict["LSAGAdd_5_B"] = CustomerLN_DB.LNAGAdd_5_B;
                        dict["LSAGAdd_6_B"] = CustomerLN_DB.LNAGAdd_6_B;
                        dict["LSAGAdd_7_B"] = CustomerLN_DB.LNAGAdd_7_B;
                        dict["LSAGAdd_8_B"] = CustomerLN_DB.LNAGAdd_8_B;
                        dict["LSAGAdd_9_B"] = CustomerLN_DB.LNAGAdd_9_B;
                        dict["LSAGCell_B"] = CustomerLN_DB.LNAGCell_B;
                        dict["LSAGTel_1_B"] = CustomerLN_DB.LNAGTel_1_B;
                        dict["LSAGTel_2_B"] = CustomerLN_DB.LNAGTel_2_B;
                        dict["LSAGID_B"] = CustomerLN_DB.LNAGID_B;
                        #endregion LS欄位額外賦值
                    }
                    webformDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webformDataModel, dict);
                }
            }
            #endregion

            #region CustomerLC
            if (!string.IsNullOrWhiteSpace(searchPDF.LCID)) {
                CustomerLC CustomerLC_DB = await _customerLCService.GetByLCID(searchPDF.LCID);

                if (CustomerLC_DB != null) {
                    webformDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webformDataModel, (CustomerLC_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(CustomerLC_DB, null))));
                    var dict = new Dictionary<string, object>();
                    {
                        #region LS欄位額外賦值
                        dict["LSName"] = CustomerLC_DB.LCName;
                        //統一編號
                        dict["LSID"] = CustomerLC_DB.LCID;
                        dict["LSID_1_1"] = CustomerLC_DB.LCID_1_1;
                        dict["LSID_1_2"] = CustomerLC_DB.LCID_1_2;
                        dict["LSID_1_3"] = CustomerLC_DB.LCID_1_3;
                        dict["LSID_1_4"] = CustomerLC_DB.LCID_1_4;
                        dict["LSID_1_5"] = CustomerLC_DB.LCID_1_5;
                        dict["LSID_1_6"] = CustomerLC_DB.LCID_1_6;
                        dict["LSID_1_7"] = CustomerLC_DB.LCID_1_7;
                        dict["LSID_1_8"] = CustomerLC_DB.LCID_1_8;
                        //EMail (法人無信箱)
                        //dict["LSMail"] = CustomerLC_DB.LCMail;
                        //性別 (法人無性別)
                        //dict["LSGender1"] = CustomerLC_DB.LCGender1;
                        //dict["LSGender2"] = CustomerLC_DB.LCGender2;
                        //生日 (法人無生日)
                        //dict["LSBirthday_D"] = CustomerLC_DB.LNBirthday_D;
                        //dict["LSBirthday_M"] = CustomerLC_DB.LNBirthday_M;
                        //dict["LSBirthday_Y"] = CustomerLC_DB.LNBirthday_Y;
                        //住址
                        dict["LSAdd"] = CustomerLC_DB.LCAdd;
                        dict["LSAdd_1"] = CustomerLC_DB.LCAdd_1;
                        dict["LSAdd_1_1"] = CustomerLC_DB.LCAdd_1_1;
                        dict["LSAdd_1_2"] = CustomerLC_DB.LCAdd_1_2;
                        dict["LSAdd_2"] = CustomerLC_DB.LCAdd_2;
                        dict["LSAdd_2_1"] = CustomerLC_DB.LCAdd_2_1;
                        dict["LSAdd_2_2"] = CustomerLC_DB.LCAdd_2_2;
                        dict["LSAdd_2_3"] = CustomerLC_DB.LCAdd_2_3;
                        dict["LSAdd_2_4"] = CustomerLC_DB.LCAdd_2_4;
                        dict["LSAdd_3"] = CustomerLC_DB.LCAdd_3;
                        dict["LSAdd_3_1"] = CustomerLC_DB.LCAdd_3_1;
                        dict["LSAdd_3_2"] = CustomerLC_DB.LCAdd_3_2;
                        dict["LSAdd_4"] = CustomerLC_DB.LCAdd_4;
                        dict["LSAdd_5"] = CustomerLC_DB.LCAdd_5;
                        dict["LSAdd_6"] = CustomerLC_DB.LCAdd_6;
                        dict["LSAdd_7"] = CustomerLC_DB.LCAdd_7;
                        dict["LSAdd_8"] = CustomerLC_DB.LCAdd_8;
                        dict["LSAdd_9"] = CustomerLC_DB.LCAdd_9;
                        dict["LSAddSame"] = CustomerLC_DB.LCAddSame;
                        //電話
                        dict["LSTel_1"] = CustomerLC_DB.LCTel_1;
                        dict["LSTel_2"] = CustomerLC_DB.LCTel_2;
                        dict["LSTel"] = CustomerLC_DB.LCTel;
                        //法人目前只有一組電話(日)
                        //dict["LSTelN_1"] = CustomerLC_DB.LCTelN_1;
                        //dict["LSTelN_2"] = CustomerLC_DB.LCTelN_2;
                        //dict["LSTelN"] = CustomerLC_DB.LCTelN;

                        //手機 法人目前沒有此欄位
                        //dict["LSCell"] = CustomerLC_DB.LCCell;

                        //代理人1
                        dict["LSAGName_A"] = CustomerLC_DB.LCAGName_A;
                        dict["LSAGAdd_1_A"] = CustomerLC_DB.LCAGAdd_1_A;
                        dict["LSAGAdd_1_1_A"] = CustomerLC_DB.LCAGAdd_1_1_A;
                        dict["LSAGAdd_1_2_A"] = CustomerLC_DB.LCAGAdd_1_2_A;
                        dict["LSAGAdd_2_A"] = CustomerLC_DB.LCAGAdd_2_A;
                        dict["LSAGAdd_2_1_A"] = CustomerLC_DB.LCAGAdd_2_1_A;
                        dict["LSAGAdd_2_2_A"] = CustomerLC_DB.LCAGAdd_2_2_A;
                        dict["LSAGAdd_2_3_A"] = CustomerLC_DB.LCAGAdd_2_3_A;
                        dict["LSAGAdd_2_4_A"] = CustomerLC_DB.LCAGAdd_2_4_A;
                        dict["LSAGAdd_3_A"] = CustomerLC_DB.LCAGAdd_3_A;
                        dict["LSAGAdd_3_1_A"] = CustomerLC_DB.LCAGAdd_3_1_A;
                        dict["LSAGAdd_3_2_A"] = CustomerLC_DB.LCAGAdd_3_2_A;
                        dict["LSAGAdd_4_A"] = CustomerLC_DB.LCAGAdd_4_A;
                        dict["LSAGAdd_5_A"] = CustomerLC_DB.LCAGAdd_5_A;
                        dict["LSAGAdd_6_A"] = CustomerLC_DB.LCAGAdd_6_A;
                        dict["LSAGAdd_7_A"] = CustomerLC_DB.LCAGAdd_7_A;
                        dict["LSAGAdd_8_A"] = CustomerLC_DB.LCAGAdd_8_A;
                        dict["LSAGAdd_9_A"] = CustomerLC_DB.LCAGAdd_9_A;
                        dict["LSAGCell_A"] = CustomerLC_DB.LCAGCell_A;
                        dict["LSAGTel_1_A"] = CustomerLC_DB.LCAGTel_1_A;
                        dict["LSAGTel_2_A"] = CustomerLC_DB.LCAGTel_2_A;
                        dict["LSAGID_A"] = CustomerLC_DB.LCAGID_A;
                        //代理人2
                        dict["LSAGName_B"] = CustomerLC_DB.LCAGName_B;
                        dict["LSAGAdd_1_B"] = CustomerLC_DB.LCAGAdd_1_B;
                        dict["LSAGAdd_1_1_B"] = CustomerLC_DB.LCAGAdd_1_1_B;
                        dict["LSAGAdd_1_2_B"] = CustomerLC_DB.LCAGAdd_1_2_B;
                        dict["LSAGAdd_2_B"] = CustomerLC_DB.LCAGAdd_2_B;
                        dict["LSAGAdd_2_1_B"] = CustomerLC_DB.LCAGAdd_2_1_B;
                        dict["LSAGAdd_2_2_B"] = CustomerLC_DB.LCAGAdd_2_2_B;
                        dict["LSAGAdd_2_3_B"] = CustomerLC_DB.LCAGAdd_2_3_B;
                        dict["LSAGAdd_2_4_B"] = CustomerLC_DB.LCAGAdd_2_4_B;
                        dict["LSAGAdd_3_B"] = CustomerLC_DB.LCAGAdd_3_B;
                        dict["LSAGAdd_3_1_B"] = CustomerLC_DB.LCAGAdd_3_1_B;
                        dict["LSAGAdd_3_2_B"] = CustomerLC_DB.LCAGAdd_3_2_B;
                        dict["LSAGAdd_4_B"] = CustomerLC_DB.LCAGAdd_4_B;
                        dict["LSAGAdd_5_B"] = CustomerLC_DB.LCAGAdd_5_B;
                        dict["LSAGAdd_6_B"] = CustomerLC_DB.LCAGAdd_6_B;
                        dict["LSAGAdd_7_B"] = CustomerLC_DB.LCAGAdd_7_B;
                        dict["LSAGAdd_8_B"] = CustomerLC_DB.LCAGAdd_8_B;
                        dict["LSAGAdd_9_B"] = CustomerLC_DB.LCAGAdd_9_B;
                        dict["LSAGCell_B"] = CustomerLC_DB.LCAGCell_B;
                        dict["LSAGTel_1_B"] = CustomerLC_DB.LCAGTel_1_B;
                        dict["LSAGTel_2_B"] = CustomerLC_DB.LCAGTel_2_B;
                        dict["LSAGID_B"] = CustomerLC_DB.LCAGID_B;
                        #endregion LS欄位額外賦值
                    }
                    webformDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webformDataModel, dict);
                }
            }
            #endregion

            #region CustomerRN
            if (!string.IsNullOrWhiteSpace(searchPDF.RNID)) {
                CustomerRN CustomerRN_DB = await _customerRNService.GetCustomerByRNID(searchPDF.RNID);

                if (CustomerRN_DB != null) {
                    webformDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webformDataModel, (CustomerRN_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(CustomerRN_DB, null))));
                    //之後應改為RS
                    //var dict = new Dictionary<string, object>();
                    //{
                    //    #region LS欄位額外賦值
                    //    dict["LSName"] = CustomerRN_DB.RNName;
                    //    //身分證字號
                    //    dict["LSID"] = CustomerRN_DB.RNID;
                    //    //住址
                    //    dict["LSAdd"] = CustomerRN_DB.RNAdd;
                    //    dict["LSAdd_1"] = CustomerRN_DB.RNAdd_1;
                    //    dict["LSAdd_1_1"] = CustomerRN_DB.RNAdd_1_1;
                    //    dict["LSAdd_1_2"] = CustomerRN_DB.RNAdd_1_2;
                    //    dict["LSAdd_2"] = CustomerRN_DB.RNAdd_2;
                    //    dict["LSAdd_2_1"] = CustomerRN_DB.RNAdd_2_1;
                    //    dict["LSAdd_2_2"] = CustomerRN_DB.RNAdd_2_2;
                    //    dict["LSAdd_2_3"] = CustomerRN_DB.RNAdd_2_3;
                    //    dict["LSAdd_2_4"] = CustomerRN_DB.RNAdd_2_4;
                    //    dict["LSAdd_3"] = CustomerRN_DB.RNAdd_3;
                    //    dict["LSAdd_3_1"] = CustomerRN_DB.RNAdd_3_1;
                    //    dict["LSAdd_3_2"] = CustomerRN_DB.RNAdd_3_2;
                    //    dict["LSAdd_4"] = CustomerRN_DB.RNAdd_4;
                    //    dict["LSAdd_5"] = CustomerRN_DB.RNAdd_5;
                    //    dict["LSAdd_6"] = CustomerRN_DB.RNAdd_6;
                    //    dict["LSAdd_7"] = CustomerRN_DB.RNAdd_7;
                    //    dict["LSAdd_8"] = CustomerRN_DB.RNAdd_8;
                    //    dict["LSAdd_9"] = CustomerRN_DB.RNAdd_9;
                    //    //電話
                    //    dict["LSTel_1"] = CustomerRN_DB.RNTel_1;
                    //    dict["LSTel_2"] = CustomerRN_DB.RNTel_2;
                    //    //手機
                    //    dict["LSCell"] = CustomerRN_DB.RNCell;
                    //    //代理人1
                    //    dict["LSAGName_A"] = CustomerRN_DB.RNAGName_A;
                    //    dict["LSAGAdd_1_A"] = CustomerRN_DB.RNAGAdd_1_A;
                    //    dict["LSAGAdd_1_1_A"] = CustomerRN_DB.RNAGAdd_1_1_A;
                    //    dict["LSAGAdd_1_2_A"] = CustomerRN_DB.RNAGAdd_1_2_A;
                    //    dict["LSAGAdd_2_A"] = CustomerRN_DB.RNAGAdd_2_A;
                    //    dict["LSAGAdd_2_1_A"] = CustomerRN_DB.RNAGAdd_2_1_A;
                    //    dict["LSAGAdd_2_2_A"] = CustomerRN_DB.RNAGAdd_2_2_A;
                    //    dict["LSAGAdd_2_3_A"] = CustomerRN_DB.RNAGAdd_2_3_A;
                    //    dict["LSAGAdd_2_4_A"] = CustomerRN_DB.RNAGAdd_2_4_A;
                    //    dict["LSAGAdd_3_A"] = CustomerRN_DB.RNAGAdd_3_A;
                    //    dict["LSAGAdd_3_1_A"] = CustomerRN_DB.RNAGAdd_3_1_A;
                    //    dict["LSAGAdd_3_2_A"] = CustomerRN_DB.RNAGAdd_3_2_A;
                    //    dict["LSAGAdd_4_A"] = CustomerRN_DB.RNAGAdd_4_A;
                    //    dict["LSAGAdd_5_A"] = CustomerRN_DB.RNAGAdd_5_A;
                    //    dict["LSAGAdd_6_A"] = CustomerRN_DB.RNAGAdd_6_A;
                    //    dict["LSAGAdd_7_A"] = CustomerRN_DB.RNAGAdd_7_A;
                    //    dict["LSAGAdd_8_A"] = CustomerRN_DB.RNAGAdd_8_A;
                    //    dict["LSAGAdd_9_A"] = CustomerRN_DB.RNAGAdd_9_A;
                    //    dict["LSAGCell_A"] = CustomerRN_DB.RNAGCell_A;
                    //    dict["LSAGTel_1_A"] = CustomerRN_DB.RNAGTel_1_A;
                    //    dict["LSAGTel_2_A"] = CustomerRN_DB.RNAGTel_2_A;
                    //    dict["LSAGID_A"] = CustomerRN_DB.RNAGID_A;
                    //    #endregion LS欄位額外賦值
                    //}

                }
            }
            #endregion

            #region 生日格式調整(86-02-07改為86/02/07)

            webformDataModel.RNBirthday = webformDataModel.RNBirthday != null ? webformDataModel.RNBirthday.Replace("-", "/") : "";
            webformDataModel.RNFBirthday_A = webformDataModel.RNFBirthday_A != null ? webformDataModel.RNFBirthday_A.Replace("-", "/") : "";
            webformDataModel.RNFBirthday_B = webformDataModel.RNFBirthday_B != null ? webformDataModel.RNFBirthday_B.Replace("-", "/") : "";
            webformDataModel.RNFBirthday_C = webformDataModel.RNFBirthday_C != null ? webformDataModel.RNFBirthday_C.Replace("-", "/") : "";
            webformDataModel.RNFBirthday_D = webformDataModel.RNFBirthday_D != null ? webformDataModel.RNFBirthday_D.Replace("-", "/") : "";
            webformDataModel.RNFBirthday_E = webformDataModel.RNFBirthday_E != null ? webformDataModel.RNFBirthday_E.Replace("-", "/") : "";
            webformDataModel.RNFBirthday_F = webformDataModel.RNFBirthday_F != null ? webformDataModel.RNFBirthday_F.Replace("-", "/") : "";
            webformDataModel.RNFBirthday_G = webformDataModel.RNFBirthday_G != null ? webformDataModel.RNFBirthday_G.Replace("-", "/") : "";

            #endregion 生日格式調整(86-02-07改為86/02/07)

            #region Building
            if (!string.IsNullOrWhiteSpace(searchPDF.BAdd)) {

                //searchPDF.BAdd =
                //Utils.memergeAddFix(
                //   searchPDF.BAdd_1,
                //   searchPDF.BAdd_1_1,
                //   searchPDF.BAdd_1_2,
                //   searchPDF.BAdd_2,
                //   searchPDF.BAdd_2_1,
                //   searchPDF.BAdd_2_2,
                //   searchPDF.BAdd_2_3,
                //   searchPDF.BAdd_2_4,
                //   searchPDF.BAdd_3,
                //   searchPDF.BAdd_3_1,
                //   searchPDF.BAdd_3_2,
                //   searchPDF.BAdd_4,
                //   searchPDF.BAdd_5,
                //   searchPDF.BAdd_6,
                //   searchPDF.BAdd_7,
                //   searchPDF.BAdd_8,
                //   searchPDF.BAdd_9
                //);

                Building Building_DB = await _buildingService.GetBuildingByBAddAsync(searchPDF.BAdd);
                Building1 Building1_DB = await _building1Service.GetByBAdd(searchPDF.BAdd);

                if (Building_DB != null) {
                    webformDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webformDataModel, (Building_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(Building_DB, null))));
                }
                if (Building1_DB != null) {
                    webformDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webformDataModel, (Building1_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(Building1_DB, null))));
                }
            }
            #endregion

            #region TBNoB6
            string sqlWhere = "1=1";
            string refkeys = "";
            sqlWhere += $" and FName='{searchPDF.FormId}'";
            if (!string.IsNullOrEmpty(searchPDF.LNID))
                refkeys += searchPDF.LNID;
            if (!string.IsNullOrEmpty(searchPDF.LCID))
                refkeys += searchPDF.LCID;
            if (!string.IsNullOrEmpty(searchPDF.RNID))
                refkeys += searchPDF.RNID;
            if (!string.IsNullOrEmpty(searchPDF.BAdd))
                refkeys += searchPDF.BAdd;
            if (!string.IsNullOrEmpty(refkeys)) {
                sqlWhere += $"and Refkeys='{refkeys}'";
            }


            var TBNoB6_DB = _tBNoB6Service.GetWhere($"{sqlWhere}");

            if (TBNoB6_DB != null) {
                //var TBNoB1_2_DB = await _tBNoB1_2Service.GetAsync(TBNoB6_DB.Id);
                webformDataModel = Merger.DictCloneAndMerge<WebFormDataModel>(webformDataModel, (TBNoB6_DB.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(TBNoB6_DB, null))));
                //webformDataModel = Merger.DictCloneAndMerge<PDFDataModel>(webformDataModel, (TBNoB1_2_DB.GetType()
                //    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                //        .ToDictionary(prop => prop.Name, prop => prop.GetValue(TBNoB1_2_DB, null))));
            }
            #endregion

            #region Remittance

            ////有指定出租人匯款資訊
            //if (!string.IsNullOrEmpty(searchPDF.LRemittanceId)) {
            //    var remittanceLN = await _remittanceLService.GetAsync(searchPDF.LRemittanceId);
            //    if (remittanceLN != null) {
            //        webformDataModel = Merger.DictCloneAndMerge<PDFDataModel>(webformDataModel, (remittanceLN.GetType()
            //            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            //                .ToDictionary(prop => prop.Name, prop => prop.GetValue(remittanceLN, null))));
            //    }
            //}
            ////沒指定的話就用LNID 或 LCID 去找，若有找到預設使用第一組匯款資訊
            //else {
            //    if (!string.IsNullOrEmpty(searchPDF.LNID)) {
            //        var reLNList = await _remittanceLService.GetListWhereAsync($" IDNo = '{searchPDF.LNID}' ");
            //        if (reLNList.Count() > 0) {
            //            var reln = reLNList.FirstOrDefault();
            //            webformDataModel = Merger.DictCloneAndMerge<PDFDataModel>(webformDataModel, (reln.GetType()
            //                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            //                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(reln, null))));
            //        }
            //    } else if (!string.IsNullOrEmpty(searchPDF.LCID)) {
            //        var reLNList = await _remittanceLService.GetListWhereAsync($" IDNo = '{searchPDF.LCID}' ");
            //        if (reLNList.Count() > 0) {
            //            var reln = reLNList.FirstOrDefault();
            //            webformDataModel = Merger.DictCloneAndMerge<PDFDataModel>(webformDataModel, (reln.GetType()
            //                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            //                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(reln, null))));
            //        }
            //    }
            //}

            ////有指定承租人匯款資訊
            //if (!string.IsNullOrEmpty(searchPDF.RRemittanceId)) {
            //    var remittanceLN = await _remittanceRService.GetAsync(searchPDF.RRemittanceId);
            //    if (remittanceLN != null) {
            //        webformDataModel = Merger.DictCloneAndMerge<PDFDataModel>(webformDataModel, (remittanceLN.GetType()
            //            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            //                .ToDictionary(prop => prop.Name, prop => prop.GetValue(remittanceLN, null))));
            //    }
            //}
            ////沒指定的話就用LNID 或 LCID 去找，若有找到預設使用第一組匯款資訊
            //else {
            //    if (!string.IsNullOrEmpty(searchPDF.RNID)) {
            //        var reLNList = await _remittanceRService.GetListWhereAsync($" IDNo = '{searchPDF.RNID}' ");
            //        if (reLNList.Count() > 0) {
            //            var reln = reLNList.FirstOrDefault();
            //            webformDataModel = Merger.DictCloneAndMerge<PDFDataModel>(webformDataModel, (reln.GetType()
            //                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            //                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(reln, null))));
            //        }
            //    } else if (!string.IsNullOrEmpty(searchPDF.RCID)) {
            //        var reLNList = await _remittanceRService.GetListWhereAsync($" IDNo = '{searchPDF.RCID}' ");
            //        if (reLNList.Count() > 0) {
            //            var reln = reLNList.FirstOrDefault();
            //            webformDataModel = Merger.DictCloneAndMerge<PDFDataModel>(webformDataModel, (reln.GetType()
            //                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            //                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(reln, null))));
            //        }
            //    }
            //}

            #endregion

            #region internalform
            //var isSign = false;
            //// TBNO A1
            //if ("A1" == internalForm?.TBNO) {
            //    var tbAlDb = await _tBNoA1Repos.GetByBAdd(PDFDataModel.FName, PDFDataModel.BAdd);

            //    if (tbAlDb != null) {
            //        isSign = tbAlDb.IsSign;
            //        PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (tbAlDb.GetType()
            //            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            //                .ToDictionary(prop => prop.Name, prop => prop.GetValue(tbAlDb, null))));
            //    }
            //}

            //// 取得簽名檔 (已經有填表過的資料才會有簽名檔)
            //if (internalForm != null && isSign) {
            //    var signatureDb = await _signatureRepos.FindByFormIdBAdd(PDFDataModel.FName, PDFDataModel.BAdd);

            //    if (signatureDb != null) {
            //        PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (signatureDb.GetType()
            //            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            //                .ToDictionary(prop => prop.Name, prop => prop.GetValue(signatureDb, null))));
            //    }
            //}

            #endregion  internalform

            return webformDataModel;
        }

        /// <summary>
        /// 上傳時若有同住人欄位就要新增此同住人至黑名單並且將此同住人送出審核
        /// </summary>
        /// <param name="dataModel"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        private async Task<string> InsertBlackList(A000002InputDto dataModel, IDbConnection conn = null, IDbTransaction trans = null)
        {
            List<BlackList> blackLists = new List<BlackList>();
            //本人
            if (!string.IsNullOrEmpty(dataModel.RNName) && !string.IsNullOrEmpty(dataModel.RNID) && !string.IsNullOrEmpty(dataModel.RNBirthday)) {
                var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNBirthday.Split("-")[0]) + 1911)).ToString();
                var blacklist = new BlackList() { Name = dataModel.RNName, IDNo = dataModel.RNID, Birthday = dataModel.RNBirthday, Age = age, ReportDept = _organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, CreatorUserDeptId = CurrentUser.DeptId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "承租人" };
                blackLists.Add(blacklist);
            }
            //同住人
            if (!string.IsNullOrEmpty(dataModel.RNFName_A) && !string.IsNullOrEmpty(dataModel.RNFID_1_A) && !string.IsNullOrEmpty(dataModel.RNFBirthday_A)) {
                var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNFBirthday_A.Split("-")[0]) + 1911)).ToString();
                var blacklist = new BlackList() { Name = dataModel.RNFName_A, IDNo = dataModel.RNFID_1_A, Birthday = dataModel.RNFBirthday_A, Age = age, ReportDept = _organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, CreatorUserDeptId = CurrentUser.DeptId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "同住人" };
                blackLists.Add(blacklist);
            }
            //同住人
            if (!string.IsNullOrEmpty(dataModel.RNFName_B) && !string.IsNullOrEmpty(dataModel.RNFID_1_B) && !string.IsNullOrEmpty(dataModel.RNFBirthday_B)) {
                var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNFBirthday_B.Split("-")[0]) + 1911)).ToString();
                var blacklist = new BlackList() { Name = dataModel.RNFName_B, IDNo = dataModel.RNFID_1_B, Birthday = dataModel.RNFBirthday_B, Age = age, ReportDept = _organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, CreatorUserDeptId = CurrentUser.DeptId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "同住人" };
                blackLists.Add(blacklist);
            }
            //同住人
            if (!string.IsNullOrEmpty(dataModel.RNFName_C) && !string.IsNullOrEmpty(dataModel.RNFID_1_C) && !string.IsNullOrEmpty(dataModel.RNFBirthday_C)) {
                var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNFBirthday_C.Split("-")[0]) + 1911)).ToString();
                var blacklist = new BlackList() { Name = dataModel.RNFName_C, IDNo = dataModel.RNFID_1_C, Birthday = dataModel.RNFBirthday_C, Age = age, ReportDept = _organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, CreatorUserDeptId = CurrentUser.DeptId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "同住人" };
                blackLists.Add(blacklist);
            }
            //同住人
            if (!string.IsNullOrEmpty(dataModel.RNFName_D) && !string.IsNullOrEmpty(dataModel.RNFID_1_D) && !string.IsNullOrEmpty(dataModel.RNFBirthday_D)) {
                var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNFBirthday_D.Split("-")[0]) + 1911)).ToString();
                var blacklist = new BlackList() { Name = dataModel.RNFName_D, IDNo = dataModel.RNFID_1_D, Birthday = dataModel.RNFBirthday_D, Age = age, ReportDept = _organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, CreatorUserDeptId = CurrentUser.DeptId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "同住人" };
                blackLists.Add(blacklist);
            }
            //同住人
            if (!string.IsNullOrEmpty(dataModel.RNFName_E) && !string.IsNullOrEmpty(dataModel.RNFID_1_E) && !string.IsNullOrEmpty(dataModel.RNFBirthday_E)) {
                var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNFBirthday_E.Split("-")[0]) + 1911)).ToString();
                var blacklist = new BlackList() { Name = dataModel.RNFName_E, IDNo = dataModel.RNFID_1_E, Birthday = dataModel.RNFBirthday_E, Age = age, ReportDept = _organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, CreatorUserDeptId = CurrentUser.DeptId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "同住人" };
                blackLists.Add(blacklist);
            }
            //同住人
            if (!string.IsNullOrEmpty(dataModel.RNFName_F) && !string.IsNullOrEmpty(dataModel.RNFID_1_F) && !string.IsNullOrEmpty(dataModel.RNFBirthday_F)) {
                var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNFBirthday_F.Split("-")[0]) + 1911)).ToString();
                var blacklist = new BlackList() { Name = dataModel.RNFName_F, IDNo = dataModel.RNFID_1_F, Birthday = dataModel.RNFBirthday_F, Age = age, ReportDept = _organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, CreatorUserDeptId = CurrentUser.DeptId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "同住人" };
                blackLists.Add(blacklist);
            }
            //同住人
            if (!string.IsNullOrEmpty(dataModel.RNFName_G) && !string.IsNullOrEmpty(dataModel.RNFID_1_G) && !string.IsNullOrEmpty(dataModel.RNFBirthday_G)) {
                var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNFBirthday_G.Split("-")[0]) + 1911)).ToString();
                var blacklist = new BlackList() { Name = dataModel.RNFName_G, IDNo = dataModel.RNFID_1_G, Birthday = dataModel.RNFBirthday_G, Age = age, ReportDept = _organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, CreatorUserDeptId = CurrentUser.DeptId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "同住人" };
                blackLists.Add(blacklist);
            }
            //保證人
            if (!string.IsNullOrEmpty(dataModel.RNGName) && !string.IsNullOrEmpty(dataModel.RNGID) && !string.IsNullOrEmpty(dataModel.RNGBirthday)) {
                var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNGBirthday.Split("-")[0]) + 1911)).ToString();
                var blacklist = new BlackList() { Name = dataModel.RNGName, IDNo = dataModel.RNGID, Birthday = dataModel.RNGBirthday, Age = age, ReportDept = _organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, CreatorUserDeptId = CurrentUser.DeptId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "保證人" };
                blackLists.Add(blacklist);
            }
            if (blackLists.Count() > 0) {

                foreach (var item in blackLists) {
                    var CRMresult = await _blackListService.SendApproval(item);
                    if (CRMresult != "Err") {
                        try {
                            var b_Id = CRMresult.ToObject<CRMAPIResult>().index;
                            if (b_Id == -1) {
                                return "Err";
                            } else {
                                item.B_Id = b_Id;
                            }
                        } catch (Exception) {
                            return "Err";
                        }
                    } else {
                        return "Err";
                    }
                }

                _blackListService.InsertRange(blackLists, conn, trans);
            }
            return "";
        }

        #region ////

        /// <summary>
        /// 等比例縮放圖片
        /// </summary>
        /// <param name="maxPx"></param>
        /// <param name="imgW"></param>
        /// <param name="imgH"></param>
        private void ImageScaling(double maxPx, ref double imgW, ref double imgH)
        {
            double fixH = 0;
            double fixW = 0;
            if (imgW > maxPx || imgH > maxPx) {
                if (imgW >= imgH) {
                    fixH = Convert.ToInt32((maxPx / imgW) * imgH);
                    fixW = maxPx;
                } else {
                    fixW = Convert.ToInt32((maxPx / imgH) * imgW);
                    fixH = maxPx;
                }
                imgW = fixW;
                imgH = fixH;
            }

        }

        //[HttpPost("FindSignature")]
        //[NoPermissionRequired]
        //[AllowAnonymous]
        //[NoSignRequired]
        //public async Task<IActionResult> FindSignature(SearchExternalformPDFModel searchPDF)
        //{
        //    CommonResult<SignatureImg> result = new CommonResult<SignatureImg>();
        //    result.ResData = await _signatureRepos.FindByFormIdBAdd(searchPDF.FormId, searchPDF.BAdd); ;
        //    result.ErrCode = ErrCode.successCode;

        //    return ToJsonContent(result);
        //}

        //[HttpPost("UploadSignature")]
        //[NoSignRequired]
        //public async Task<IActionResult> UploadSignature(SignatureInPutDto inputData)
        //{
        //    CommonResult result = new CommonResult();

        //    try {
        //        var loginId = CurrentUser?.UserId;
        //        var internalForm = await iService.GetByFormId(inputData.FName);
        //        if (internalForm == null) {
        //            throw new Exception($"表單名稱: {inputData.FName}已遺失 !");
        //        }

        //        if (string.IsNullOrEmpty(inputData.BAdd)) {
        //            throw new Exception($"表單地址: {inputData.BAdd}已遺失 !");
        //        }
        //        var fileName = "";
        //        var now = DateTime.Now;
        //        var tbAlDb = await _tBNoA1Repos.GetByBAdd(inputData.FName, inputData.BAdd);
        //        var data = await _signatureRepos.FindByFormIdBAdd(inputData.FName, inputData.BAdd);
        //        if (data == null) {
        //            // 第一次簽名
        //            var guid = GuidUtils.CreateNo();
        //            fileName = $"{internalForm.FormId}_{guid}";
        //            var s = new SignatureImg {
        //                Id = guid,
        //                BAdd = inputData.BAdd,
        //                FormId = inputData.FName,
        //                SignatureImgPath_1 = SaveSignature(inputData.SignatureBase64_1, $"{fileName}_1.jpg"),
        //                SignatureImgPath_2 = SaveSignature(inputData.SignatureBase64_2, $"{fileName}_2.jpg"),
        //                SignatureImgPath_3 = SaveSignature(inputData.SignatureBase64_3, $"{fileName}_3.jpg"),
        //                SignatureImgPath_4 = SaveSignature(inputData.SignatureBase64_4, $"{fileName}_4.jpg"),
        //                SignatureImgPath_5 = SaveSignature(inputData.SignatureBase64_5, $"{fileName}_5.jpg"),
        //                CreatorUserId = loginId,
        //                CreatorTime = now,
        //                LastModifyUserId = loginId,
        //                LastModifyTime = now
        //            };

        //            if (s.SignatureImgPath_1 == "") {
        //                throw new Exception($"簽名檔已遺失 !");
        //            }

        //            _signatureRepos.Add(s);

        //            tbAlDb.IsSign = true;
        //            tbAlDb.LastModifyTime = now;
        //            tbAlDb.LastModifyUserId = loginId;
        //            _tBNoA1Repos.Update(tbAlDb);

        //        } else {

        //            //else if (internalForm.IsFileing) {
        //            // 已歸檔 才可以再次簽名新表單
        //            if (!tbAlDb.IsSign) {
        //                fileName = $"{internalForm.FormId}_{data.Id}";
        //                data.SignatureImgPath_1 = SaveSignature(inputData.SignatureBase64_1, $"{fileName}_1.jpg");
        //                data.SignatureImgPath_2 = SaveSignature(inputData.SignatureBase64_2, $"{fileName}_2.jpg");
        //                data.SignatureImgPath_3 = SaveSignature(inputData.SignatureBase64_3, $"{fileName}_3.jpg");
        //                data.SignatureImgPath_4 = SaveSignature(inputData.SignatureBase64_4, $"{fileName}_4.jpg");
        //                data.SignatureImgPath_5 = SaveSignature(inputData.SignatureBase64_5, $"{fileName}_5.jpg");
        //                data.LastModifyTime = now;
        //                data.LastModifyUserId = loginId;

        //                _signatureRepos.Update(data);

        //                tbAlDb.IsSign = true;
        //                tbAlDb.LastModifyTime = now;
        //                tbAlDb.LastModifyUserId = loginId;
        //                _tBNoA1Repos.Update(tbAlDb);
        //            }
        //            //}
        //        }


        //        //if (name.StartsWith("Image")) {
        //        //    string path = @$"{sysSetting.ChaochiFilepath}\Singature\{DateTime.Now.ToString("yyyyMMdd")}";
        //        //    Directory.CreateDirectory(path);
        //        //    /* draw image on pdf */
        //        //    PdfRectangle rect = doc.AcroForm.Fields["Image1"].Elements.GetRectangle(PdfAnnotation.Keys.Rect);
        //        //    XGraphics gfx = XGraphics.FromPdfPage(doc.Pages[0]);
        //        //    XImage image = XImage.FromFile(@$"{path}\{FormId}_{CurrentUser?.UserId}.jpg");
        //        //    gfx.DrawImage(image, rect.X1, doc.Pages[0].Height - rect.Y2, rect.Width, rect.Height);
        //        //}

        //        result.ErrCode = ErrCode.successCode;
        //        result.Success = true;
        //    } catch (Exception ex) {
        //        result.ErrCode = "500";
        //        result.ErrMsg = ex.Message;
        //        Log4NetHelper.Error("", ex);
        //        if (ex.InnerException == null) {
        //            throw ex;
        //        } else {
        //            throw new Exception(ex.InnerException.Message);
        //        }
        //    }
        //    return ToJsonContent(result);
        //}

        //private string SaveSignature(string signature, string fileName)
        //{
        //    string filePath = "";
        //    try {
        //        YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
        //        Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

        //        if (!string.IsNullOrEmpty(signature) && signature.IndexOf(',') > -1) {
        //            string basePath = @$"{sysSetting.ChaochiFilepath}\";
        //            string dirPath = @$"Singature\{DateTime.Now.ToString("yyyyMMdd")}\";
        //            filePath = dirPath + fileName;
        //            Directory.CreateDirectory(basePath + dirPath);
        //            System.IO.File.WriteAllBytes(@$"{basePath}\{filePath}", Convert.FromBase64String(signature.Split(",")[1]));
        //        }
        //    } catch (Exception ex) {
        //        Log4NetHelper.Error("", ex);
        //    }
        //    return filePath;
        //}


        //[HttpPost("FindFormData")]
        //[NoPermissionRequired]
        //[AllowAnonymous]
        //[NoSignRequired]
        //public async Task<IActionResult> FindFormData(SearchExternalformPDFModel inputData)
        //{
        //    CommonResult<PDFDataModel> result = new CommonResult<PDFDataModel>();
        //    result.ResData = await GetData(inputData); ;
        //    result.ErrCode = ErrCode.successCode;

        //    return ToJsonContent(result);
        //}

        //[HttpPost("SaveFomrData")]
        //[NoSignRequired]
        //public async Task<IActionResult> SaveFomrData([FromBody] dynamic jsonData)
        //{
        //    CommonResult result = new CommonResult();

        //    //https://stackoverflow.com/questions/63163768/get-net-core-json-body-as-dynamic-object
        //    ExpandoObject data = JsonConvert.DeserializeObject<ExpandoObject>(jsonData.ToString());
        //    string json = JsonConvert.SerializeObject(data);

        //    try {

        //        Dictionary<string, object> dictV = ((IDictionary<string, object>)data).ToDictionary(nvp => nvp.Key, nvp => nvp.Value);
        //        Dictionary<string, string> dictTU = new Dictionary<string, string>();

        //        if (!dictV.ContainsKey("FName")) {
        //            throw new Exception("上傳Json檔案不含標籤[FName]");
        //        }

        //        if (!dictV.ContainsKey("Vno")) {
        //            throw new Exception("上傳Json檔案不含標籤[Vno]");
        //        } else {
        //            var internalForm = await iService.GetByFormId((string)dictV["FName"]);
        //            if (internalForm == null) {
        //                throw new Exception($"FormId:{dictV["FName"]}對應資料尚未建立!");
        //            }

        //            if ((string)dictV["Vno"] != internalForm.Vno) {
        //                throw new Exception($"上傳Json檔案標籤[Vno]不為{internalForm.Vno}");
        //            }
        //        }


        //        //if (name.StartsWith("Image")) {
        //        //    string path = @$"{sysSetting.ChaochiFilepath}\Singature\{DateTime.Now.ToString("yyyyMMdd")}";
        //        //    Directory.CreateDirectory(path);
        //        //    /* draw image on pdf */
        //        //    PdfRectangle rect = doc.AcroForm.Fields["Image1"].Elements.GetRectangle(PdfAnnotation.Keys.Rect);
        //        //    XGraphics gfx = XGraphics.FromPdfPage(doc.Pages[0]);
        //        //    XImage image = XImage.FromFile(@$"{path}\{FormId}_{CurrentUser?.UserId}.jpg");
        //        //    gfx.DrawImage(image, rect.X1, doc.Pages[0].Height - rect.Y2, rect.Width, rect.Height);
        //        //}

        //        await SaveData(dictV);

        //        result.ErrCode = ErrCode.successCode;
        //        result.Success = true;
        //    } catch (Exception ex) {
        //        result.ErrCode = "500";
        //        result.ErrMsg = ex.Message;
        //        Log4NetHelper.Error("", ex);
        //        if (ex.InnerException == null) {
        //            throw ex;
        //        } else {
        //            throw new Exception(ex.InnerException.Message);
        //        }
        //    }
        //    return ToJsonContent(result);
        //}

        //[HttpPost("FileingForm")]
        //[NoSignRequired]
        //public async Task<IActionResult> FileingForm(SearchInternalformModel inputData)
        //{
        //    CommonResult<PageResult<InternalformOutputDto>> result = new CommonResult<PageResult<InternalformOutputDto>>();

        //    inputData.BAdd =
        //       Utils.memergeAddFix(
        //          inputData.BAdd_1,
        //          inputData.BAdd_1_1,
        //          inputData.BAdd_1_2,
        //          inputData.BAdd_2,
        //          inputData.BAdd_2_1,
        //          inputData.BAdd_2_2,
        //          inputData.BAdd_2_3,
        //          inputData.BAdd_2_4,
        //          inputData.BAdd_3,
        //          inputData.BAdd_3_1,
        //          inputData.BAdd_3_2,
        //          inputData.BAdd_4,
        //          inputData.BAdd_5,
        //          inputData.BAdd_6,
        //          inputData.BAdd_7,
        //          inputData.BAdd_8,
        //          inputData.BAdd_9
        //       );

        //    result.ErrCode = ErrCode.failCode;

        //    if (!string.IsNullOrEmpty(inputData.BAdd)) {
        //        var tbAlDb = await _tBNoA1Repos.GetByBAdd(inputData.FormId, inputData.BAdd);
        //        if (tbAlDb != null) {
        //            tbAlDb.IsFileing = true;
        //            tbAlDb.IsSign = false;
        //            _tBNoA1Repos.Update(tbAlDb);
        //            result.ErrCode = ErrCode.successCode;
        //        }
        //    }


        //    return ToJsonContent(result);
        //}

        //[HttpGet("GetInstance")]
        //[NoSignRequired]
        //public IActionResult GetInstance(string formId)
        //{
        //    var result = new CommonResult();
        //    string msg = "";
        //    var extForm = iService.GetWhere($"FormId='{formId}'");
        //    if (extForm != null) {
        //        result.Success = true;
        //        result.ResData = extForm;
        //        result.ErrCode = ErrCode.successCode;
        //    } else {
        //        result.Success = false;
        //        result.ErrCode = ErrCode.err60001;
        //    }
        //    return ToJsonContent(result);
        //}

        ///// <summary>
        ///// 取得不能顯示"下載空白表單" 的PDF FormID
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("GetCantNullPDF")]
        //[NoSignRequired]
        //public async Task<IActionResult> GetCantNullPDF()
        //{
        //    var result = new CommonResult();
        //    var list = await iService.GetListWhereAsync("not(MustExistsLandLord='N' and MustExistsBuilding='N' and MustExistsRenter='N')");
        //    var resultlist = list.Select(x => x.FormId).ToList();
        //    result.Success = true;
        //    result.ErrCode = ErrCode.successCode;
        //    result.ResData = resultlist;
        //    return ToJsonContent(result);
        //}

        #endregion 

    }
}