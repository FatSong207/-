using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PdfSharp.Pdf;
using PdfSharp.Pdf.AcroForms;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.Annotations;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Chaochi.Core.IRepositories;
using Yuebon.Chaochi.Core.IServices;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Security.Dtos;
using static Yuebon.WebApi.Areas.Chaochi.Controllers.PDFDataController;

namespace Yuebon.WebApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 用戶接口
    /// </summary>
    [ApiController]
    //[ApiVersionNeutral]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class ExternalformController : AreaApiController<Externalform, ExternalformOutputDto, ExternalformInputDto, IExternalformService, string>
    {
        private readonly IDbContextCore _ybdbcontext;
        private readonly ICustomerRCService _customerRCService;
        private readonly IRemittanceRService _remittanceRService;
        private readonly IRemittanceLService _remittanceLService;
        private readonly IBlackListService _blackListService;
        private readonly IBuildingEquipmentService _buildingEquipmentService;
        private readonly IBuilding1Service _building1Service;
        private readonly ISecondLandlordService _secondLandlordService;
        private ICustomerLCService CustomerLCService;
        private ICustomerLNService CustomerLNService;
        private ICustomerRNService CustomerRNService;
        private ICustomerRNRepository ICustomerRNRepository;
        private IBuildingRepository IBuildingRepository;
        private readonly IBuildingService buildingService;
        private ITBNoB5Service ITBNoB5Service;
        private ITBNoB4Service ITBNoB4Service;
        private ITBNoB3Service ITBNoB3Service;
        private ITBNoB2Service ITBNoB2Service;
        private ITBNoB1Service ITBNoB1Service;
        private readonly ITBNoB1_2Service _tBNoB1_2Service;
        private readonly ITBNoB2_2Service _tBNoB2_2Service;
        private readonly ITBNoB3_2Service _tBNoB3_2Service;
        private readonly ITBNoB4_2Service _tBNoB4_2Service;
        private readonly ITBNoB5_2Service _tBNoB5_2Service;
        private IHistoryFormLNService IHistoryFormLNService;
        private IHistoryFormLCService IHistoryFormLCService;
        private IHistoryFormBuildingService IHistoryFormBuildingService;
        private IHistoryFormRNService IHistoryFormRNService;
        private IHistoryFormRCService IHistoryFormRCService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="ybdbcontext"></param>
        /// <param name="customerRCService"></param>
        /// <param name="remittanceLService"></param>
        /// <param name="remittanceRService"></param>
        /// <param name="blackListService"></param>
        /// <param name="buildingEquipmentService"></param>
        /// <param name="building1Service"></param>
        /// <param name="secondLandlordService"></param>
        /// <param name="landLordBelongingService"></param>
        /// <param name="signatureRepos"></param>
        /// <param name="internalService"></param>
        /// <param name="_ICustomerRNService"></param>
        /// <param name="_ICustomerLCService"></param>
        /// <param name="_ICustomerLNService"></param>
        /// <param name="_ICustomerRNRepository"></param>
        /// <param name="_ICustomerLSBuildingRepository"></param>
        /// <param name="_ITBNoB1Repository"></param>
        /// <param name="_ITBNoB5Service"></param>
        /// <param name="_ITBNoB4Service"></param>
        /// <param name="_ITBNoB3Service"></param>
        /// <param name="_ITBNoB2Service"></param>
        /// <param name="_ITBNoB1Service"></param>
        /// <param name="tBNoB1_2Service"></param>
        /// <param name="tBNoB2_2Service"></param>
        /// <param name="tBNoB3_2Service"></param>
        /// <param name="tBNoB4_2Service"></param>
        /// <param name="tBNoB5_2Service"></param>
        /// <param name="tBNoA1Repos"></param>
        /// <param name="_IBuildingRepository"></param>
        /// <param name="_buildingService"></param>
        /// <param name="_IBuildingBelongingService"></param>
        /// <param name="_IHistoryFormLNService"></param>
        /// <param name="_IHistoryFormLCService"></param>
        /// <param name="_IHistoryFormBuildingService"></param>
        /// <param name="_IHistoryFormRNService"></param>
        /// <param name="_IHistoryFormRCService"></param>
        /// <param name="_organizeService"></param>
        /// <param name="_roleService"></param>
        /// <param name="_userLogOnService"></param>
        public ExternalformController(IExternalformService _iService,
            IDbContextCore ybdbcontext,
            ICustomerRCService customerRCService,
            IRemittanceLService remittanceLService,
            IRemittanceRService remittanceRService,
            IBlackListService blackListService,
            IBuildingEquipmentService buildingEquipmentService,
            IBuilding1Service building1Service,
            ISecondLandlordService secondLandlordService,
            ILandLordBelongingService landLordBelongingService,
            ISignatureImgRepository signatureRepos,
            IInternalformService internalService,
            ICustomerRNService _ICustomerRNService,
            ICustomerLCService _ICustomerLCService,
            ICustomerLNService _ICustomerLNService,
            ICustomerRNRepository _ICustomerRNRepository,
            ICustomerLSBuildingRepository _ICustomerLSBuildingRepository,
            ITBNoB1Repository _ITBNoB1Repository,
            ITBNoB5Service _ITBNoB5Service,
            ITBNoB4Service _ITBNoB4Service,
            ITBNoB3Service _ITBNoB3Service,
            ITBNoB2Service _ITBNoB2Service,
            ITBNoB1Service _ITBNoB1Service,
            ITBNoB1_2Service tBNoB1_2Service,
            ITBNoB2_2Service tBNoB2_2Service,
            ITBNoB3_2Service tBNoB3_2Service,
            ITBNoB4_2Service tBNoB4_2Service,
            ITBNoB5_2Service tBNoB5_2Service,
            ITBNoA1Repository tBNoA1Repos,
            IBuildingRepository _IBuildingRepository,
            IBuildingService _buildingService,
            IBuildingBelongingService _IBuildingBelongingService,
            IHistoryFormLNService _IHistoryFormLNService,
            IHistoryFormLCService _IHistoryFormLCService,
            IHistoryFormBuildingService _IHistoryFormBuildingService,
            IHistoryFormRNService _IHistoryFormRNService,
            IHistoryFormRCService _IHistoryFormRCService,
            Yuebon.Security.IServices.IOrganizeService _organizeService,
            Yuebon.Security.IServices.IRoleService _roleService,
            Yuebon.Security.IServices.IUserLogOnService _userLogOnService) : base(_iService)
        {
            iService = _iService;
            _ybdbcontext = ybdbcontext;
            _customerRCService = customerRCService;
            _remittanceRService = remittanceRService;
            _remittanceLService = remittanceLService;
            _blackListService = blackListService;
            _buildingEquipmentService = buildingEquipmentService;
            _building1Service = building1Service;
            _secondLandlordService = secondLandlordService;
            CustomerRNService = _ICustomerRNService;
            CustomerLCService = _ICustomerLCService;
            CustomerLNService = _ICustomerLNService;
            ICustomerRNRepository = _ICustomerRNRepository;
            ITBNoB5Service = _ITBNoB5Service;
            ITBNoB4Service = _ITBNoB4Service;
            ITBNoB3Service = _ITBNoB3Service;
            ITBNoB2Service = _ITBNoB2Service;
            ITBNoB1Service = _ITBNoB1Service;
            _tBNoB1_2Service = tBNoB1_2Service;
            _tBNoB2_2Service = tBNoB2_2Service;
            _tBNoB3_2Service = tBNoB3_2Service;
            _tBNoB4_2Service = tBNoB4_2Service;
            _tBNoB5_2Service = tBNoB5_2Service;
            IBuildingRepository = _IBuildingRepository;
            buildingService = _buildingService;
            IHistoryFormLNService = _IHistoryFormLNService;
            IHistoryFormLCService = _IHistoryFormLCService;
            IHistoryFormBuildingService = _IHistoryFormBuildingService;
            IHistoryFormRNService = _IHistoryFormRNService;
            IHistoryFormRCService = _IHistoryFormRCService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Externalform info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            //info.OrganizeId = organizeService.GetRootOrganize(info.DepartmentId).Id;
            //info.DeleteMark = false;
            //if (info.SortCode == null)
            //{
            //    info.SortCode = 99;
            //}
        }

        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(Externalform info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
            //info.OrganizeId = organizeService.GetRootOrganize(info.DepartmentId).Id;
        }

        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <param name="BuildingBelongings"></param>
        /// <returns></returns>
        protected void OnBeforeUpdate(Externalform info, List<BuildingBelonging> BuildingBelongings)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
            //info.OrganizeId = organizeService.GetRootOrganize(info.DepartmentId).Id;
            foreach (var Belonging in BuildingBelongings) {
                Belonging.LastModifyUserId = info.LastModifyUserId;
                Belonging.LastModifyTime = info.LastModifyTime;
            }
        }

        /// <summary>
        /// 異步新增數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(ExternalformInputDto tinfo)
        {
            CommonResult result = new CommonResult();

            if (!string.IsNullOrEmpty(tinfo.FormId)) {
                string where = string.Format("FormId='{0}'", tinfo.FormId);
                Externalform user = iService.GetWhere(where);
                if (user != null) {
                    result.ErrMsg = "表單代號不能重復";
                    return ToJsonContent(result);
                }
            } else {
                result.ErrMsg = "表單代號不能為空";
                return ToJsonContent(result);
            }
            Externalform info = tinfo.MapTo<Externalform>();
            OnBeforeInsert(info);

            List<BuildingBelonging> buildingBelongingEntitys = new List<BuildingBelonging>();

            result.Success = await iService.InsertAsync(info, buildingBelongingEntitys);
            if (result.Success) {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } else {
                result.ErrMsg = ErrCode.err43001;
                result.ErrCode = "43001";
            }
            return ToJsonContent(result);
        }
        /// <summary>
        /// 異步更新數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(ExternalformInputDto tinfo)
        {
            CommonResult result = new CommonResult();
            if (!string.IsNullOrEmpty(tinfo.FormId)) {
                string where = string.Format(" FormId='{0}'  and id!='{1}' ", tinfo.FormId, tinfo.Id);
                Externalform user = iService.GetWhere(where);
                if (user != null) {
                    result.ErrMsg = "表單代號不能重復";
                    return ToJsonContent(result);
                }
            } else {
                result.ErrMsg = "表單代號不能為空";
                return ToJsonContent(result);
            }
            Externalform info = iService.Get(tinfo.Id);
            info.FormId = tinfo.FormId;
            info.FormName = tinfo.FormName;
            info.Vno = tinfo.Vno;
            info.TBNO = tinfo.TBNO;
            info.CustTag = tinfo.CustTag;
            info.RequiredLandlord = tinfo.RequiredLandlord;
            info.RequiredBuilding = tinfo.RequiredBuilding;
            info.RequiredRenter = tinfo.RequiredRenter;
            info.MustExistsLandLord = tinfo.MustExistsLandLord;
            info.MustExistsBuilding = tinfo.MustExistsBuilding;
            info.MustExistsRenter = tinfo.MustExistsRenter;
            info.ArchiveLTo = tinfo.ArchiveLTo;
            info.level = tinfo.level;

            OnBeforeUpdate(info);
            //bool bl = await iService.UpdateAsync(info, tinfo.Id).ConfigureAwait(false);
            bool bl = iService.Update(info, tinfo.Id);
            if (bl) {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } else {
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步分頁查詢
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerSearchAsync")]
        [YuebonAuthorize("List")]
        [LogNotMethod]
        public async Task<IActionResult> FindWithPagerSearchAsync(SearchExternalformModel search)
        {
            CommonResult<PageResult<ExternalformOutputDto>> result = new CommonResult<PageResult<ExternalformOutputDto>>();
            result.ResData = await iService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        ///// <summary>
        ///// 根據主鍵Id獲取一個對象信息
        ///// </summary>
        ///// <param name="FormId"></param>
        ///// <param name="id">主鍵Id</param>
        ///// <returns></returns>
        //[HttpGet("GetById")]
        //[YuebonAuthorize("")]
        //[NoPermissionRequired]
        //public override async Task<CommonResult<BuildingOutputDto>> GetById(string id)
        //{
        //    CommonResult<BuildingOutputDto> result = new CommonResult<BuildingOutputDto>();
        //    BuildingOutputDto info = await iService.GetOutDtoAsync(id);
        //    if (info != null)
        //    {
        //        result.ErrCode = ErrCode.successCode;
        //        result.ResData = info;
        //    }
        //    else
        //    {
        //        result.ErrMsg = ErrCode.err50001;
        //        result.ErrCode = "50001";
        //    }
        //    return result;
        //}

        /// <summary>
        /// 異步分頁查詢
        /// </summary>
        /// <returns></returns>
        [HttpGet("downloadPDFTemplateByFormId")]
        [YuebonAuthorize("List")]
        [LogNotMethod]
        //public IActionResult Get(string fileName)
        public IActionResult DownloadPDFTemplateByFormId(string FormId)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            //https://stackoverflow.com/questions/13865718/directory-getfiles-get-todays-files-only
            //DirectoryInfo infoD = new DirectoryInfo(Path.Combine("D:/zzz/", "image/"));
            //DirectoryInfo infoD = new DirectoryInfo(sysSetting.ChaochiFilepath);

            //var image = System.IO.File.OpenRead($"D:/zzz/image/{fileName}");
            var pdf = System.IO.File.OpenRead($"{sysSetting.ChaochiFilepath}TemplatePDF/{FormId}.pdf");
            //https://stackoverflow.com/questions/34131326/using-mimemapping-in-asp-net-core
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
            return File(pdf, contentType ?? "image/jpeg");
            //return File(image, "application/octet-stream");
        }

        /// <summary>
        /// 設備相關匯出PDF
        /// </summary>
        /// <param name="bAdd"></param>
        /// <returns></returns>
        /// 
        [HttpGet("BeqExportToPDF")]
        public async Task<IActionResult> BeqExportToPDF(string bAdd)
        {
            CommonResult result = new CommonResult();

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

            //MemoryStream PDFstream = new MemoryStream();
            var id = buildingService.GetIdByBAdd(bAdd);
            var desDir = @$"D:\zzz\image\historyPDF\Building\{id}";
            if (!Directory.Exists(desDir)) {
                Directory.CreateDirectory(desDir);
            }
            var now = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fs = new FileStream(Path.Combine(desDir, $"{now}附屬設備清單.pdf"), FileMode.Create);
            using (var doc = PdfSharp.Pdf.IO.PdfReader.Open($"{sysSetting.ChaochiFilepath}TemplatePDF/A000801.pdf", PdfDocumentOpenMode.Modify)) {

                if (doc.AcroForm == null) {
                    doc.Close();
                    result.ErrCode = "40001";
                    result.ErrMsg = "上傳PDF檔案不含任何標籤";
                    return ToJsonContent(result, true);
                } else {
                    //檢查當前用戶可下載的範圍
                    //var ccuser = CurrentUser;
                    //if (!CurrentUser.Role.Contains("administrators")) {
                    //    var dict = new Dictionary<string, object>();
                    //    dict.Add("BAdd_1", bAdd == "" ? null : bAdd);
                    //    var checkAuth = await CheckAuth(dict);
                    //    if (checkAuth.Success == false) {
                    //        return ToJsonContent(checkAuth);
                    //    }
                    //}
                    PDFDataModel PDFDataModel = new PDFDataModel();
                    var buildingequipment = _buildingEquipmentService.Get(_buildingEquipmentService.GetIdByBAdd(bAdd));

                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (buildingequipment.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(buildingequipment, null))));


                    if (doc.AcroForm.Elements.ContainsKey("/NeedAppearances")) {
                        doc.AcroForm.Elements["/NeedAppearances"] = new PdfBoolean(true);
                    } else {
                        doc.AcroForm.Elements.Add("/NeedAppearances", new PdfBoolean(true));
                    }

                    //string vv = DateTime.Now.ToString("yyyyMMddHHmmss");
                    //Console.WriteLine("\n");
                    Dictionary<string, string> dictV = new Dictionary<string, string>();
                    //Dictionary<string, string> dictTU = new Dictionary<string, string>();

                    Dictionary<string, object> dictFinal =
                            PDFDataModel.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .ToDictionary(prop => prop.Name, prop => prop.GetValue(PDFDataModel, null));

                    //格局
                    var building = await buildingService.GetBuildingByBAddAsync(bAdd);
                    dictFinal["BRoom"] = building.BRoom;
                    dictFinal["BLD"] = building.BLD;
                    dictFinal["BBath"] = building.BBath;
                    dictFinal["BBalcony"] = building.BBalcony;


                    //設值
                    foreach (var name in doc.AcroForm.Fields.Names) {

                        //Console.WriteLine($"{name}:{doc.AcroForm.Fields[name].Value}");
                        if (name.StartsWith("Image")) {

                        } else if (doc.AcroForm.Fields[name] is PdfTextField) {
                            if (name == "FName" || name == "Vno" || name == "TBNO") {

                            } else {

                                doc.AcroForm.Fields[name].ReadOnly = false;
                                doc.AcroForm.Fields[name].Value = new PdfString(dictFinal.ContainsKey(name) ? (string)dictFinal[name] : "");
                            }
                        } else if (doc.AcroForm.Fields[name].Value is PdfItem) {
                            doc.AcroForm.Fields[name].ReadOnly = false;
                            if (dictFinal.ContainsKey(name) && (string)dictFinal[name] == "/Yes") {
                                ((PdfCheckBoxField)doc.AcroForm.Fields[name]).Checked = true;
                            } else {
                                ((PdfCheckBoxField)doc.AcroForm.Fields[name]).Checked = false;
                            }
                            //doc.AcroForm.Fields[name].Value = new PdfCheckBoxField(dictV.ContainsKey(name) ? dictV["name"] : "");
                        } else {
                            Console.WriteLine($"{name}: is not supported");
                        }
                    }
                    var historyB = new HistoryFormBuilding() {
                        IDNo = id,
                        FormName = "附屬設備清單",
                        FileName = $"{now}附屬設備清單.pdf",
                        UploadTime = DateTime.Now,
                        Note = "系統上傳",
                        CreatorUserId=CurrentUser.UserId
                    };
                    await IHistoryFormBuildingService.InsertAsync(historyB);
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    doc.Save(fs);
                    fs.Close();
                }
            }
            return ToJsonContent(result);
            //https://stackoverflow.com/questions/34131326/using-mimemapping-in-asp-net-core
            //string contentType;
            //new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
            //return File(fs, contentType ?? "image/jpeg");
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
        public async Task<IActionResult> DownloadPDFWithDataByFormId(SearchExternalformPDFModel searchPDF)
        {
            CommonResult result = new CommonResult();
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
                    var externalForm = await iService.GetByFormId(FormId);
                    // 必填檢查 房東
                    if (externalForm.RequiredLandlord == "Y") {
                        if (string.IsNullOrEmpty(LNId) &&
                            string.IsNullOrEmpty(LCId)) {
                            result.Success = false;
                            result.ErrMsg = $"請輸入出租人身分證字號/居留證號/統一編號";
                            return ToJsonContent(result);
                            //throw new Exception($"外部表單[{dictV["FName"]}] [{Externalform.FormName}] 房東不可空白");
                        }
                    }

                    // 必填檢查 建物
                    if (externalForm.RequiredBuilding == "Y") {
                        if (string.IsNullOrEmpty(BAdd)) {
                            result.Success = false;
                            result.ErrMsg = $"請輸入建物地址";
                            return ToJsonContent(result);
                            //throw new Exception($"外部表單[{dictV["FName"]}] [{Externalform.FormName}] 建物地址不可空白");
                        }
                    }

                    // 必填檢查 房客
                    if (externalForm.RequiredRenter == "Y") {
                        if (string.IsNullOrEmpty(RNId) &&
                            string.IsNullOrEmpty(RCId)) {
                            result.Success = false;
                            result.ErrMsg = $"請輸入承租人身分證字號/居留證號/統一編號";
                            return ToJsonContent(result);
                            //throw new Exception($"外部表單[{dictV["FName"]}] [{Externalform.FormName}] 房客不可空白");
                        }
                    }

                    //必須為已存在檢查 房東
                    if (externalForm.MustExistsLandLord == "Y") {
                        var customerLN = await CustomerLNService.GetByCustomerLNID(LNId);
                        var customerLC = await CustomerLCService.GetByLCID(LCId);
                        if (customerLN == null && customerLC == null) {
                            result.Success = false;
                            result.ErrMsg = "查無此出租人";
                            return ToJsonContent(result);
                        }
                    }

                    //必須為已存在檢查 建物
                    if (externalForm.MustExistsBuilding == "Y") {
                        var building = buildingService.GetIdByBAdd(BAdd);
                        if (string.IsNullOrEmpty(building)) {
                            result.Success = false;
                            result.ErrMsg = "查無此建物";
                            return ToJsonContent(result);
                        }
                    }

                    //必須為已存在檢查 房客
                    if (externalForm.MustExistsRenter == "Y") {
                        var customerRN = await CustomerRNService.GetCustomerByRNID(RNId);
                        var customerRC = await _customerRCService.GetWhereAsync($" RCID = '{RCId}' ");
                        if (customerRN == null && customerRC == null) {
                            result.Success = false;
                            result.ErrMsg = "查無此承租人身份證字號／居留證號";
                            return ToJsonContent(result);
                        }
                    }
                    //if (!string.IsNullOrEmpty(LNId)) {
                    //    var customerLN = await CustomerLNService.GetByCustomerLNID(LNId);
                    //    if (customerLN == null) {
                    //        result.Success = false;
                    //        result.ErrMsg = "查無此出租人身份證字號／居留證號";
                    //        return ToJsonContent(result);
                    //    }
                    //}
                    //if (!string.IsNullOrEmpty(LCId)) {
                    //    var customerLC = await CustomerLCService.GetByLCID(LCId);
                    //    if (customerLC == null) {
                    //        result.Success = false;
                    //        result.ErrMsg = "查無此出租人統一編號";
                    //        return ToJsonContent(result);
                    //    }
                    //}
                    //if (!string.IsNullOrEmpty(BAdd)) {
                    //    var building = buildingService.GetIdByBAdd(BAdd);
                    //    if (string.IsNullOrEmpty(building)) {
                    //        result.Success = false;
                    //        result.ErrMsg = "查無此建物";
                    //        return ToJsonContent(result);
                    //    }
                    //}
                    //if (!string.IsNullOrEmpty(RNId)) {
                    //    var customerRN = await CustomerRNService.GetCustomerByRNID(RNId);
                    //    if (customerRN == null) {
                    //        result.Success = false;
                    //        result.ErrMsg = "查無此承租人身份證字號／居留證號";
                    //        return ToJsonContent(result);
                    //    }
                    //}
                    //檢查當前用戶可下載的範圍
                    var ccuser = CurrentUser;
                    if (!CurrentUser.Role.Contains("administrators")) {
                        var pdfDataModel = new PDFDataModel();
                        pdfDataModel.LNID = LNId;
                        pdfDataModel.LCID = LCId;
                        pdfDataModel.BAdd = BAdd;
                        pdfDataModel.RNID = RNId;
                        pdfDataModel.RCID = RCId;
                        //var dict = new Dictionary<string, object>();
                        //dict.Add("LNID", LNId == "" ? null : LNId);
                        //dict.Add("LCID", LCId == "" ? null : LCId);
                        //dict.Add("BAdd_1", BAdd == "" ? null : BAdd);
                        //dict.Add("BAdd", BAdd == "" ? null : BAdd);
                        //dict.Add("RNID", RNId == "" ? null : RNId);
                        //dict.Add("RCID", RCId == "" ? null : RCId);
                        //dict.Add("LSID", "");
                        var checkAuth = await CheckAuth(pdfDataModel);
                        if (checkAuth.Success == false) {
                            return ToJsonContent(checkAuth);
                        }
                    }

                    //更新BZ欄位的table(只針對RO欄位)

                    if (searchPDF.ReplaceROFieldCheck == true) {
                        string sqlWhere = $"1=1 and FName='{FormId}'";
                        string refKeys = "";
                        if (!string.IsNullOrEmpty(LNId))
                            refKeys += LNId;
                        if (!string.IsNullOrEmpty(LCId))
                            refKeys += LCId;
                        if (!string.IsNullOrEmpty(RNId))
                            refKeys += RNId;
                        if (!string.IsNullOrEmpty(RCId))
                            refKeys += RCId;
                        if (!string.IsNullOrEmpty(BAdd))
                            refKeys += BAdd;
                        if (!string.IsNullOrEmpty(refKeys)) {
                            //TBNoB1.RefKeys = refKeys;
                            sqlWhere += $" and RefKeys='{refKeys}'";
                        }
                        #region TBNoB1  
                        if ("B1" == externalForm.TBNO) {

                            if (refKeys != null) {
                                //TBNoB1 TBNoB1_DB = await ITBNoB1Repository.GetByBAdd(TBNoB1.BAdd);
                                var TBNoB1 = new TBNoB1();
                                TBNoB1 TBNoB1_DB = ITBNoB1Service.GetWhere(@$"{sqlWhere}");
                                if (TBNoB1_DB == null) {
                                    // TBNoB1.Id = newId;
                                    TBNoB1.FName = FormId;
                                    TBNoB1.RefKeys = refKeys;
                                    TBNoB1.CreatorUserId = CurrentUser.UserId;
                                    TBNoB1.CreatorTime = DateTime.Now;
                                    TBNoB1.ROName = searchPDF.ROName;
                                    TBNoB1.RORep = searchPDF.RORep;
                                    TBNoB1.ROAdd = searchPDF.ROAdd;
                                    TBNoB1.ROUserName = searchPDF.ROUserName;
                                    TBNoB1.ROTel = searchPDF.ROTel;
                                    TBNoB1.ROFax = searchPDF.ROFax;
                                    TBNoB1.ROLRNo = searchPDF.ROLRNo;
                                    TBNoB1.ROID = searchPDF.ROID;
                                    TBNoB1.RHMName = searchPDF.RHMName;
                                    TBNoB1.RHMRNo = searchPDF.RHMRNo;
                                    TBNoB1.AGName = searchPDF.AGName;
                                    TBNoB1.AGLRNo = searchPDF.AGLRNo;
                                    TBNoB1.AGTel = searchPDF.AGTel;

                                    using IDbConnection conn = _ybdbcontext.GetDatabase().GetDbConnection();
                                    conn.Open();
                                    using var eftran = _ybdbcontext.GetDatabase().BeginTransaction();
                                    var tran = eftran.GetDbTransaction();
                                    try {
                                        await ITBNoB1Service.InsertAsync(TBNoB1, conn, tran);
                                        await _tBNoB1_2Service.InsertAsync(new TBNoB1_2() { Id = TBNoB1.Id }, conn, tran);
                                        eftran.Commit();
                                        result.ErrCode = ErrCode.successCode;
                                        result.ErrMsg = ErrCode.err0;
                                        result.Success = true;
                                    } catch (Exception ex) {
                                        eftran.Rollback();
                                        Log4NetHelper.Error("新增TBNoB1失敗", ex);
                                        result.ErrMsg = "新增TBNoB1失敗";
                                        result.ErrCode = ErrCode.err43003;
                                        result.Success = false;
                                        // 觸發ExceptionHandlingAttribute.OnException
                                        throw;
                                    }
                                } else {
                                    TBNoB1_DB.CreatorUserId = CurrentUser.UserId;
                                    TBNoB1_DB.CreatorTime = DateTime.Now;
                                    TBNoB1_DB.ROName = searchPDF.ROName;
                                    TBNoB1_DB.RORep = searchPDF.RORep;
                                    TBNoB1_DB.ROAdd = searchPDF.ROAdd;
                                    TBNoB1_DB.ROUserName = searchPDF.ROUserName;
                                    TBNoB1_DB.ROTel = searchPDF.ROTel;
                                    TBNoB1_DB.ROFax = searchPDF.ROFax;
                                    TBNoB1_DB.ROLRNo = searchPDF.ROLRNo;
                                    TBNoB1_DB.ROID = searchPDF.ROID;
                                    TBNoB1_DB.RHMName = searchPDF.RHMName;
                                    TBNoB1_DB.RHMRNo = searchPDF.RHMRNo;
                                    TBNoB1_DB.AGName = searchPDF.AGName;
                                    TBNoB1_DB.AGLRNo = searchPDF.AGLRNo;
                                    TBNoB1_DB.AGTel = searchPDF.AGTel;
                                    bool bl = ITBNoB1Service.Update(TBNoB1_DB, TBNoB1_DB.Id);
                                }
                            }
                        }
                        #endregion TBNoB1

                        #region TBNoB2  
                        else if ("B2" == externalForm.TBNO) {

                            if (refKeys != null) {
                                //TBNoB2 TBNoB2_DB = await ITBNoB2Repository.GetByBAdd(TBNoB2.BAdd);
                                var TBNoB2 = new TBNoB2();
                                TBNoB2 TBNoB2_DB = ITBNoB2Service.GetWhere(@$"{sqlWhere}");
                                if (TBNoB2_DB == null) {
                                    // TBNoB2.Id = newId;
                                    TBNoB2.FName = FormId;
                                    TBNoB2.RefKeys = refKeys;
                                    TBNoB2.CreatorUserId = CurrentUser.UserId;
                                    TBNoB2.CreatorTime = DateTime.Now;
                                    TBNoB2.ROName = searchPDF.ROName;
                                    TBNoB2.RORep = searchPDF.RORep;
                                    TBNoB2.ROAdd = searchPDF.ROAdd;
                                    TBNoB2.ROUserName = searchPDF.ROUserName;
                                    TBNoB2.ROTel = searchPDF.ROTel;
                                    TBNoB2.ROFax = searchPDF.ROFax;
                                    TBNoB2.ROLRNo = searchPDF.ROLRNo;
                                    TBNoB2.ROID = searchPDF.ROID;
                                    TBNoB2.RHMName = searchPDF.RHMName;
                                    TBNoB2.RHMRNo = searchPDF.RHMRNo;
                                    TBNoB2.AGName = searchPDF.AGName;
                                    TBNoB2.AGLRNo = searchPDF.AGLRNo;
                                    TBNoB2.AGTel = searchPDF.AGTel;

                                    using IDbConnection conn = _ybdbcontext.GetDatabase().GetDbConnection();
                                    conn.Open();
                                    using var eftran = _ybdbcontext.GetDatabase().BeginTransaction();
                                    var tran = eftran.GetDbTransaction();
                                    try {
                                        await ITBNoB2Service.InsertAsync(TBNoB2, conn, tran);
                                        await _tBNoB2_2Service.InsertAsync(new TBNoB2_2() { Id = TBNoB2.Id }, conn, tran);
                                        eftran.Commit();
                                        result.ErrCode = ErrCode.successCode;
                                        result.ErrMsg = ErrCode.err0;
                                        result.Success = true;
                                    } catch (Exception ex) {
                                        eftran.Rollback();
                                        Log4NetHelper.Error("新增TBNoB2失敗", ex);
                                        result.ErrMsg = "新增TBNoB2失敗";
                                        result.ErrCode = ErrCode.err43003;
                                        result.Success = false;
                                        // 觸發ExceptionHandlingAttribute.OnException
                                        throw;
                                    }
                                } else {
                                    TBNoB2_DB.CreatorUserId = CurrentUser.UserId;
                                    TBNoB2_DB.CreatorTime = DateTime.Now;
                                    TBNoB2_DB.ROName = searchPDF.ROName;
                                    TBNoB2_DB.RORep = searchPDF.RORep;
                                    TBNoB2_DB.ROAdd = searchPDF.ROAdd;
                                    TBNoB2_DB.ROUserName = searchPDF.ROUserName;
                                    TBNoB2_DB.ROTel = searchPDF.ROTel;
                                    TBNoB2_DB.ROFax = searchPDF.ROFax;
                                    TBNoB2_DB.ROLRNo = searchPDF.ROLRNo;
                                    TBNoB2_DB.ROID = searchPDF.ROID;
                                    TBNoB2_DB.RHMName = searchPDF.RHMName;
                                    TBNoB2_DB.RHMRNo = searchPDF.RHMRNo;
                                    TBNoB2_DB.AGName = searchPDF.AGName;
                                    TBNoB2_DB.AGLRNo = searchPDF.AGLRNo;
                                    TBNoB2_DB.AGTel = searchPDF.AGTel;
                                    bool bl = ITBNoB2Service.Update(TBNoB2_DB, TBNoB2_DB.Id);
                                }
                            }
                        }
                        #endregion TBNoB2

                        #region TBNoB3  
                        else if ("B3" == externalForm.TBNO) {

                            if (refKeys != null) {
                                //TBNoB3 TBNoB3_DB = await ITBNoB3Repository.GetByBAdd(TBNoB3.BAdd);
                                var TBNoB3 = new TBNoB3();
                                TBNoB3 TBNoB3_DB = ITBNoB3Service.GetWhere(@$"{sqlWhere}");
                                if (TBNoB3_DB == null) {
                                    // TBNoB3.Id = newId;
                                    TBNoB3.FName = FormId;
                                    TBNoB3.RefKeys = refKeys;
                                    TBNoB3.CreatorUserId = CurrentUser.UserId;
                                    TBNoB3.CreatorTime = DateTime.Now;
                                    TBNoB3.ROName = searchPDF.ROName;
                                    TBNoB3.RORep = searchPDF.RORep;
                                    TBNoB3.ROAdd = searchPDF.ROAdd;
                                    TBNoB3.ROUserName = searchPDF.ROUserName;
                                    TBNoB3.ROTel = searchPDF.ROTel;
                                    TBNoB3.ROFax = searchPDF.ROFax;
                                    TBNoB3.ROLRNo = searchPDF.ROLRNo;
                                    TBNoB3.ROID = searchPDF.ROID;
                                    TBNoB3.RHMName = searchPDF.RHMName;
                                    TBNoB3.RHMRNo = searchPDF.RHMRNo;
                                    TBNoB3.AGName = searchPDF.AGName;
                                    TBNoB3.AGLRNo = searchPDF.AGLRNo;
                                    TBNoB3.AGTel = searchPDF.AGTel;
                                    using IDbConnection conn = _ybdbcontext.GetDatabase().GetDbConnection();
                                    conn.Open();
                                    using var eftran = _ybdbcontext.GetDatabase().BeginTransaction();
                                    var tran = eftran.GetDbTransaction();
                                    try {
                                        await ITBNoB3Service.InsertAsync(TBNoB3, conn, tran);
                                        await _tBNoB3_2Service.InsertAsync(new TBNoB3_2() { Id = TBNoB3.Id }, conn, tran);
                                        eftran.Commit();
                                        result.ErrCode = ErrCode.successCode;
                                        result.ErrMsg = ErrCode.err0;
                                        result.Success = true;
                                    } catch (Exception ex) {
                                        eftran.Rollback();
                                        Log4NetHelper.Error("新增TBNoB3失敗", ex);
                                        result.ErrMsg = "新增TBNoB3失敗";
                                        result.ErrCode = ErrCode.err43003;
                                        result.Success = false;
                                        // 觸發ExceptionHandlingAttribute.OnException
                                        throw;
                                    }
                                } else {
                                    TBNoB3_DB.CreatorUserId = CurrentUser.UserId;
                                    TBNoB3_DB.CreatorTime = DateTime.Now;
                                    TBNoB3_DB.ROName = searchPDF.ROName;
                                    TBNoB3_DB.RORep = searchPDF.RORep;
                                    TBNoB3_DB.ROAdd = searchPDF.ROAdd;
                                    TBNoB3_DB.ROUserName = searchPDF.ROUserName;
                                    TBNoB3_DB.ROTel = searchPDF.ROTel;
                                    TBNoB3_DB.ROFax = searchPDF.ROFax;
                                    TBNoB3_DB.ROLRNo = searchPDF.ROLRNo;
                                    TBNoB3_DB.ROID = searchPDF.ROID;
                                    TBNoB3_DB.RHMName = searchPDF.RHMName;
                                    TBNoB3_DB.RHMRNo = searchPDF.RHMRNo;
                                    TBNoB3_DB.AGName = searchPDF.AGName;
                                    TBNoB3_DB.AGLRNo = searchPDF.AGLRNo;
                                    TBNoB3_DB.AGTel = searchPDF.AGTel;
                                    bool bl = ITBNoB3Service.Update(TBNoB3_DB, TBNoB3_DB.Id);
                                }
                            }
                        }
                        #endregion TBNoB3

                        #region TBNoB4  
                        else if ("B4" == externalForm.TBNO) {

                            if (refKeys != null) {
                                //TBNoB4 TBNoB4_DB = await ITBNoB4Repository.GetByBAdd(TBNoB4.BAdd);
                                var TBNoB4 = new TBNoB4();
                                TBNoB4 TBNoB4_DB = ITBNoB4Service.GetWhere(@$"{sqlWhere}");
                                if (TBNoB4_DB == null) {
                                    // TBNoB4.Id = newId;
                                    TBNoB4.FName = FormId;
                                    TBNoB4.RefKeys = refKeys;
                                    TBNoB4.CreatorUserId = CurrentUser.UserId;
                                    TBNoB4.CreatorTime = DateTime.Now;
                                    TBNoB4.ROName = searchPDF.ROName;
                                    TBNoB4.RORep = searchPDF.RORep;
                                    TBNoB4.ROAdd = searchPDF.ROAdd;
                                    TBNoB4.ROUserName = searchPDF.ROUserName;
                                    TBNoB4.ROTel = searchPDF.ROTel;
                                    TBNoB4.ROFax = searchPDF.ROFax;
                                    TBNoB4.ROLRNo = searchPDF.ROLRNo;
                                    TBNoB4.ROID = searchPDF.ROID;
                                    TBNoB4.RHMName = searchPDF.RHMName;
                                    TBNoB4.RHMRNo = searchPDF.RHMRNo;
                                    TBNoB4.AGName = searchPDF.AGName;
                                    TBNoB4.AGLRNo = searchPDF.AGLRNo;
                                    TBNoB4.AGTel = searchPDF.AGTel;
                                    using IDbConnection conn = _ybdbcontext.GetDatabase().GetDbConnection();
                                    conn.Open();
                                    using var eftran = _ybdbcontext.GetDatabase().BeginTransaction();
                                    var tran = eftran.GetDbTransaction();
                                    try {
                                        await ITBNoB4Service.InsertAsync(TBNoB4, conn, tran);
                                        await _tBNoB4_2Service.InsertAsync(new TBNoB4_2() { Id = TBNoB4.Id }, conn, tran);
                                        eftran.Commit();
                                        result.ErrCode = ErrCode.successCode;
                                        result.ErrMsg = ErrCode.err0;
                                        result.Success = true;
                                    } catch (Exception ex) {
                                        eftran.Rollback();
                                        Log4NetHelper.Error("新增TBNoB4失敗", ex);
                                        result.ErrMsg = "新增TBNoB4失敗";
                                        result.ErrCode = ErrCode.err43003;
                                        result.Success = false;
                                        // 觸發ExceptionHandlingAttribute.OnException
                                        throw;
                                    }
                                } else {
                                    TBNoB4_DB.CreatorUserId = CurrentUser.UserId;
                                    TBNoB4_DB.CreatorTime = DateTime.Now;
                                    TBNoB4_DB.ROName = searchPDF.ROName;
                                    TBNoB4_DB.RORep = searchPDF.RORep;
                                    TBNoB4_DB.ROAdd = searchPDF.ROAdd;
                                    TBNoB4_DB.ROUserName = searchPDF.ROUserName;
                                    TBNoB4_DB.ROTel = searchPDF.ROTel;
                                    TBNoB4_DB.ROFax = searchPDF.ROFax;
                                    TBNoB4_DB.ROLRNo = searchPDF.ROLRNo;
                                    TBNoB4_DB.ROID = searchPDF.ROID;
                                    TBNoB4_DB.RHMName = searchPDF.RHMName;
                                    TBNoB4_DB.RHMRNo = searchPDF.RHMRNo;
                                    TBNoB4_DB.AGName = searchPDF.AGName;
                                    TBNoB4_DB.AGLRNo = searchPDF.AGLRNo;
                                    TBNoB4_DB.AGTel = searchPDF.AGTel;
                                    bool bl = ITBNoB4Service.Update(TBNoB4_DB, TBNoB4_DB.Id);
                                }
                            }
                        }
                        #endregion TBNoB4

                        #region TBNoB5  
                        else if ("B5" == externalForm.TBNO) {

                            if (refKeys != null) {
                                //TBNoB5 TBNoB5_DB = await ITBNoB5Repository.GetByBAdd(TBNoB5.BAdd);
                                var TBNoB5 = new TBNoB5();
                                TBNoB5 TBNoB5_DB = ITBNoB5Service.GetWhere(@$"{sqlWhere}");
                                if (TBNoB5_DB == null) {
                                    // TBNoB5.Id = newId;
                                    TBNoB5.FName = FormId;
                                    TBNoB5.RefKeys = refKeys;
                                    TBNoB5.CreatorUserId = CurrentUser.UserId;
                                    TBNoB5.CreatorTime = DateTime.Now;
                                    TBNoB5.ROName = searchPDF.ROName;
                                    TBNoB5.RORep = searchPDF.RORep;
                                    TBNoB5.ROAdd = searchPDF.ROAdd;
                                    TBNoB5.ROUserName = searchPDF.ROUserName;
                                    TBNoB5.ROTel = searchPDF.ROTel;
                                    TBNoB5.ROFax = searchPDF.ROFax;
                                    TBNoB5.ROLRNo = searchPDF.ROLRNo;
                                    TBNoB5.ROID = searchPDF.ROID;
                                    TBNoB5.RHMName = searchPDF.RHMName;
                                    TBNoB5.RHMRNo = searchPDF.RHMRNo;
                                    TBNoB5.AGName = searchPDF.AGName;
                                    TBNoB5.AGLRNo = searchPDF.AGLRNo;
                                    TBNoB5.AGTel = searchPDF.AGTel;
                                    using IDbConnection conn = _ybdbcontext.GetDatabase().GetDbConnection();
                                    conn.Open();
                                    using var eftran = _ybdbcontext.GetDatabase().BeginTransaction();
                                    var tran = eftran.GetDbTransaction();
                                    try {
                                        await ITBNoB5Service.InsertAsync(TBNoB5, conn, tran);
                                        await _tBNoB5_2Service.InsertAsync(new TBNoB5_2() { Id = TBNoB5.Id }, conn, tran);
                                        eftran.Commit();
                                        result.ErrCode = ErrCode.successCode;
                                        result.ErrMsg = ErrCode.err0;
                                        result.Success = true;
                                    } catch (Exception ex) {
                                        eftran.Rollback();
                                        Log4NetHelper.Error("新增TBNoB5失敗", ex);
                                        result.ErrMsg = "新增TBNoB5失敗";
                                        result.ErrCode = ErrCode.err43003;
                                        result.Success = false;
                                        // 觸發ExceptionHandlingAttribute.OnException
                                        throw;
                                    }
                                } else {
                                    TBNoB5_DB.CreatorUserId = CurrentUser.UserId;
                                    TBNoB5_DB.CreatorTime = DateTime.Now;
                                    TBNoB5_DB.ROName = searchPDF.ROName;
                                    TBNoB5_DB.RORep = searchPDF.RORep;
                                    TBNoB5_DB.ROAdd = searchPDF.ROAdd;
                                    TBNoB5_DB.ROUserName = searchPDF.ROUserName;
                                    TBNoB5_DB.ROTel = searchPDF.ROTel;
                                    TBNoB5_DB.ROFax = searchPDF.ROFax;
                                    TBNoB5_DB.ROLRNo = searchPDF.ROLRNo;
                                    TBNoB5_DB.ROID = searchPDF.ROID;
                                    TBNoB5_DB.RHMName = searchPDF.RHMName;
                                    TBNoB5_DB.RHMRNo = searchPDF.RHMRNo;
                                    TBNoB5_DB.AGName = searchPDF.AGName;
                                    TBNoB5_DB.AGLRNo = searchPDF.AGLRNo;
                                    TBNoB5_DB.AGTel = searchPDF.AGTel;
                                    bool bl = ITBNoB5Service.Update(TBNoB5_DB, TBNoB5_DB.Id);
                                }
                            }
                        }
                        #endregion TBNoB5
                    }



                    PDFDataModel PDFDataModel = await GetData(searchPDF);
                    if (doc.AcroForm.Elements.ContainsKey("/NeedAppearances")) {
                        doc.AcroForm.Elements["/NeedAppearances"] = new PdfBoolean(true);
                    } else {
                        doc.AcroForm.Elements.Add("/NeedAppearances", new PdfBoolean(true));
                    }

                    Dictionary<string, string> dictV = new Dictionary<string, string>();

                    Dictionary<string, object> dictFinal =
                            PDFDataModel.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .ToDictionary(prop => prop.Name, prop => prop.GetValue(PDFDataModel, null));
                    //設值
                    foreach (var name in doc.AcroForm.Fields.Names) {
                        var hasbarea = dictFinal.ContainsKey("BArea");
                        var barea = (string)dictFinal["BArea"];
                        if (name.StartsWith("Image")) {
                            //try {
                            //    // 簽名圖檔
                            //    string path = @$"{sysSetting.ChaochiFilepath}\Singature\{DateTime.Now.ToString("yyyyMMdd")}";
                            //    /* draw image on pdf */
                            //    PdfRectangle rect = doc.AcroForm.Fields["Image1"].Elements.GetRectangle(PdfAnnotation.Keys.Rect);
                            //    XGraphics gfx = XGraphics.FromPdfPage(doc.Pages[0]);
                            //    XImage image = XImage.FromFile(@$"{path}\{FormId}_{CurrentUser?.UserId}.png");
                            //    double w = rect.Width, h = rect.Height;
                            //    ImageScaling(Math.Max(rect.Height, rect.Width) - 10, ref w, ref h);
                            //    gfx.DrawImage(image, rect.X1, doc.Pages[0].Height - rect.Y2, w, h);
                            //} catch (Exception e) {
                            //    Console.WriteLine(e.Message + " " + e.StackTrace);
                            //}

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


        private async Task<PDFDataModel> GetData(SearchExternalformPDFModel searchPDF)
        {
            PDFDataModel PDFDataModel = new PDFDataModel();

            #region CustomerLN
            if (!string.IsNullOrWhiteSpace(searchPDF.LNID)) {
                CustomerLN CustomerLN_DB = await CustomerLNService.GetByCustomerLNID(searchPDF.LNID);

                if (CustomerLN_DB != null) {
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (CustomerLN_DB.GetType()
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
                        dict["LSAddC"] = CustomerLN_DB.LNAddC;
                        dict["LSAddC_1"] = CustomerLN_DB.LNAddC_1;
                        dict["LSAddC_1_1"] = CustomerLN_DB.LNAddC_1_1;
                        dict["LSAddC_1_2"] = CustomerLN_DB.LNAddC_1_2;
                        dict["LSAddC_2"] = CustomerLN_DB.LNAddC_2;
                        dict["LSAddC_2_1"] = CustomerLN_DB.LNAddC_2_1;
                        dict["LSAddC_2_2"] = CustomerLN_DB.LNAddC_2_2;
                        dict["LSAddC_2_3"] = CustomerLN_DB.LNAddC_2_3;
                        dict["LSAddC_2_4"] = CustomerLN_DB.LNAddC_2_4;
                        dict["LSAddC_3"] = CustomerLN_DB.LNAddC_3;
                        dict["LSAddC_3_1"] = CustomerLN_DB.LNAddC_3_1;
                        dict["LSAddC_3_2"] = CustomerLN_DB.LNAddC_3_2;
                        dict["LSAddC_4"] = CustomerLN_DB.LNAddC_4;
                        dict["LSAddC_5"] = CustomerLN_DB.LNAddC_5;
                        dict["LSAddC_6"] = CustomerLN_DB.LNAddC_6;
                        dict["LSAddC_7"] = CustomerLN_DB.LNAddC_7;
                        dict["LSAddC_8"] = CustomerLN_DB.LNAddC_8;
                        dict["LSAddC_9"] = CustomerLN_DB.LNAddC_9;
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
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, dict);
                }
            }
            #endregion

            #region CustomerLC
            if (!string.IsNullOrWhiteSpace(searchPDF.LCID)) {
                CustomerLC CustomerLC_DB = await CustomerLCService.GetByLCID(searchPDF.LCID);

                if (CustomerLC_DB != null) {
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (CustomerLC_DB.GetType()
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
                        dict["LSAddC"] = CustomerLC_DB.LCAddC;
                        dict["LSAddC_1"] = CustomerLC_DB.LCAddC_1;
                        dict["LSAddC_1_1"] = CustomerLC_DB.LCAddC_1_1;
                        dict["LSAddC_1_2"] = CustomerLC_DB.LCAddC_1_2;
                        dict["LSAddC_2"] = CustomerLC_DB.LCAddC_2;
                        dict["LSAddC_2_1"] = CustomerLC_DB.LCAddC_2_1;
                        dict["LSAddC_2_2"] = CustomerLC_DB.LCAddC_2_2;
                        dict["LSAddC_2_3"] = CustomerLC_DB.LCAddC_2_3;
                        dict["LSAddC_2_4"] = CustomerLC_DB.LCAddC_2_4;
                        dict["LSAddC_3"] = CustomerLC_DB.LCAddC_3;
                        dict["LSAddC_3_1"] = CustomerLC_DB.LCAddC_3_1;
                        dict["LSAddC_3_2"] = CustomerLC_DB.LCAddC_3_2;
                        dict["LSAddC_4"] = CustomerLC_DB.LCAddC_4;
                        dict["LSAddC_5"] = CustomerLC_DB.LCAddC_5;
                        dict["LSAddC_6"] = CustomerLC_DB.LCAddC_6;
                        dict["LSAddC_7"] = CustomerLC_DB.LCAddC_7;
                        dict["LSAddC_8"] = CustomerLC_DB.LCAddC_8;
                        dict["LSAddC_9"] = CustomerLC_DB.LCAddC_9;
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
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, dict);
                }
            }
            #endregion

            #region CustomerRN
            if (!string.IsNullOrWhiteSpace(searchPDF.RNID)) {
                CustomerRN CustomerRN_DB = await ICustomerRNRepository.GetCustomerByRNID(searchPDF.RNID);

                if (CustomerRN_DB != null) {
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (CustomerRN_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(CustomerRN_DB, null))));
                    var dict = new Dictionary<string, object>();
                    {
                        #region RS欄位額外賦值
                        dict["RSName"] = CustomerRN_DB.RNName;
                        //身分證字號
                        dict["RSID"] = CustomerRN_DB.RNID;
                        dict["RSID_1_1"] = CustomerRN_DB.RNID_1_1;
                        dict["RSID_1_2"] = CustomerRN_DB.RNID_1_2;
                        dict["RSID_1_3"] = CustomerRN_DB.RNID_1_3;
                        dict["RSID_1_4"] = CustomerRN_DB.RNID_1_4;
                        dict["RSID_1_5"] = CustomerRN_DB.RNID_1_5;
                        dict["RSID_1_6"] = CustomerRN_DB.RNID_1_6;
                        dict["RSID_1_7"] = CustomerRN_DB.RNID_1_7;
                        dict["RSID_1_8"] = CustomerRN_DB.RNID_1_8;
                        dict["RSID_1_9"] = CustomerRN_DB.RNID_1_9;
                        dict["RSID_1_10"] = CustomerRN_DB.RNID_1_10;
                        //住址
                        dict["RSAdd"] = CustomerRN_DB.RNAdd;
                        dict["RSAdd_1"] = CustomerRN_DB.RNAdd_1;
                        dict["RSAdd_1_1"] = CustomerRN_DB.RNAdd_1_1;
                        dict["RSAdd_1_2"] = CustomerRN_DB.RNAdd_1_2;
                        dict["RSAdd_2"] = CustomerRN_DB.RNAdd_2;
                        dict["RSAdd_2_1"] = CustomerRN_DB.RNAdd_2_1;
                        dict["RSAdd_2_2"] = CustomerRN_DB.RNAdd_2_2;
                        dict["RSAdd_2_3"] = CustomerRN_DB.RNAdd_2_3;
                        dict["RSAdd_2_4"] = CustomerRN_DB.RNAdd_2_4;
                        dict["RSAdd_3"] = CustomerRN_DB.RNAdd_3;
                        dict["RSAdd_3_1"] = CustomerRN_DB.RNAdd_3_1;
                        dict["RSAdd_3_2"] = CustomerRN_DB.RNAdd_3_2;
                        dict["RSAdd_4"] = CustomerRN_DB.RNAdd_4;
                        dict["RSAdd_5"] = CustomerRN_DB.RNAdd_5;
                        dict["RSAdd_6"] = CustomerRN_DB.RNAdd_6;
                        dict["RSAdd_7"] = CustomerRN_DB.RNAdd_7;
                        dict["RSAdd_8"] = CustomerRN_DB.RNAdd_8;
                        dict["RSAdd_9"] = CustomerRN_DB.RNAdd_9;
                        dict["RSAddSame"] = CustomerRN_DB.RNAddSame;
                        dict["RSAddC"] = CustomerRN_DB.RNAddC;
                        dict["RSAddC_1"] = CustomerRN_DB.RNAddC_1;
                        dict["RSAddC_1_1"] = CustomerRN_DB.RNAddC_1_1;
                        dict["RSAddC_1_2"] = CustomerRN_DB.RNAddC_1_2;
                        dict["RSAddC_2"] = CustomerRN_DB.RNAddC_2;
                        dict["RSAddC_2_1"] = CustomerRN_DB.RNAddC_2_1;
                        dict["RSAddC_2_2"] = CustomerRN_DB.RNAddC_2_2;
                        dict["RSAddC_2_3"] = CustomerRN_DB.RNAddC_2_3;
                        dict["RSAddC_2_4"] = CustomerRN_DB.RNAddC_2_4;
                        dict["RSAddC_3"] = CustomerRN_DB.RNAddC_3;
                        dict["RSAddC_3_1"] = CustomerRN_DB.RNAddC_3_1;
                        dict["RSAddC_3_2"] = CustomerRN_DB.RNAddC_3_2;
                        dict["RSAddC_4"] = CustomerRN_DB.RNAddC_4;
                        dict["RSAddC_5"] = CustomerRN_DB.RNAddC_5;
                        dict["RSAddC_6"] = CustomerRN_DB.RNAddC_6;
                        dict["RSAddC_7"] = CustomerRN_DB.RNAddC_7;
                        dict["RSAddC_8"] = CustomerRN_DB.RNAddC_8;
                        dict["RSAddC_9"] = CustomerRN_DB.RNAddC_9;
                        //電話 11/28尚未出現
                        //dict["LSTel_1"] = CustomerRN_DB.RNTel_1;
                        //dict["LSTel_2"] = CustomerRN_DB.RNTel_2;
                        //手機
                        dict["RSCell"] = CustomerRN_DB.RNCell;
                        //代理人1  11/28尚未出現
                        //dict["LSAGName_A"] = CustomerRN_DB.RNAGName_A;
                        //dict["LSAGAdd_1_A"] = CustomerRN_DB.RNAGAdd_1_A;
                        //dict["LSAGAdd_1_1_A"] = CustomerRN_DB.RNAGAdd_1_1_A;
                        //dict["LSAGAdd_1_2_A"] = CustomerRN_DB.RNAGAdd_1_2_A;
                        //dict["LSAGAdd_2_A"] = CustomerRN_DB.RNAGAdd_2_A;
                        //dict["LSAGAdd_2_1_A"] = CustomerRN_DB.RNAGAdd_2_1_A;
                        //dict["LSAGAdd_2_2_A"] = CustomerRN_DB.RNAGAdd_2_2_A;
                        //dict["LSAGAdd_2_3_A"] = CustomerRN_DB.RNAGAdd_2_3_A;
                        //dict["LSAGAdd_2_4_A"] = CustomerRN_DB.RNAGAdd_2_4_A;
                        //dict["LSAGAdd_3_A"] = CustomerRN_DB.RNAGAdd_3_A;
                        //dict["LSAGAdd_3_1_A"] = CustomerRN_DB.RNAGAdd_3_1_A;
                        //dict["LSAGAdd_3_2_A"] = CustomerRN_DB.RNAGAdd_3_2_A;
                        //dict["LSAGAdd_4_A"] = CustomerRN_DB.RNAGAdd_4_A;
                        //dict["LSAGAdd_5_A"] = CustomerRN_DB.RNAGAdd_5_A;
                        //dict["LSAGAdd_6_A"] = CustomerRN_DB.RNAGAdd_6_A;
                        //dict["LSAGAdd_7_A"] = CustomerRN_DB.RNAGAdd_7_A;
                        //dict["LSAGAdd_8_A"] = CustomerRN_DB.RNAGAdd_8_A;
                        //dict["LSAGAdd_9_A"] = CustomerRN_DB.RNAGAdd_9_A;
                        //dict["LSAGCell_A"] = CustomerRN_DB.RNAGCell_A;
                        //dict["LSAGTel_1_A"] = CustomerRN_DB.RNAGTel_1_A;
                        //dict["LSAGTel_2_A"] = CustomerRN_DB.RNAGTel_2_A;
                        //dict["LSAGID_A"] = CustomerRN_DB.RNAGID_A;
                        #endregion LS欄位額外賦值
                    }
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, dict);
                }
            }
            #endregion

            #region CustomerRC
            if (!string.IsNullOrWhiteSpace(searchPDF.RCID)) {
                var CustomerRC_DB = await _customerRCService.GetWhereAsync($" RCID = '{searchPDF.RCID}' ");

                if (CustomerRC_DB != null) {
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (CustomerRC_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(CustomerRC_DB, null))));
                    var dict = new Dictionary<string, object>();
                    {
                        #region RS欄位額外賦值
                        dict["RSName"] = CustomerRC_DB.RCName;
                        //統一編號
                        dict["RSID"] = CustomerRC_DB.RCID;
                        dict["RSID_1_1"] = CustomerRC_DB.RCID_1_1;
                        dict["RSID_1_2"] = CustomerRC_DB.RCID_1_2;
                        dict["RSID_1_3"] = CustomerRC_DB.RCID_1_3;
                        dict["RSID_1_4"] = CustomerRC_DB.RCID_1_4;
                        dict["RSID_1_5"] = CustomerRC_DB.RCID_1_5;
                        dict["RSID_1_6"] = CustomerRC_DB.RCID_1_6;
                        dict["RSID_1_7"] = CustomerRC_DB.RCID_1_7;
                        dict["RSID_1_8"] = CustomerRC_DB.RCID_1_8;
                        //住址
                        dict["RSAdd"] = CustomerRC_DB.RCAdd;
                        dict["RSAdd_1"] = CustomerRC_DB.RCAdd_1;
                        dict["RSAdd_1_1"] = CustomerRC_DB.RCAdd_1_1;
                        dict["RSAdd_1_2"] = CustomerRC_DB.RCAdd_1_2;
                        dict["RSAdd_2"] = CustomerRC_DB.RCAdd_2;
                        dict["RSAdd_2_1"] = CustomerRC_DB.RCAdd_2_1;
                        dict["RSAdd_2_2"] = CustomerRC_DB.RCAdd_2_2;
                        dict["RSAdd_2_3"] = CustomerRC_DB.RCAdd_2_3;
                        dict["RSAdd_2_4"] = CustomerRC_DB.RCAdd_2_4;
                        dict["RSAdd_3"] = CustomerRC_DB.RCAdd_3;
                        dict["RSAdd_3_1"] = CustomerRC_DB.RCAdd_3_1;
                        dict["RSAdd_3_2"] = CustomerRC_DB.RCAdd_3_2;
                        dict["RSAdd_4"] = CustomerRC_DB.RCAdd_4;
                        dict["RSAdd_5"] = CustomerRC_DB.RCAdd_5;
                        dict["RSAdd_6"] = CustomerRC_DB.RCAdd_6;
                        dict["RSAdd_7"] = CustomerRC_DB.RCAdd_7;
                        dict["RSAdd_8"] = CustomerRC_DB.RCAdd_8;
                        dict["RSAdd_9"] = CustomerRC_DB.RCAdd_9;
                        dict["RSAddSame"] = CustomerRC_DB.RCAddSame;
                        dict["RSAddC"] = CustomerRC_DB.RCAddC;
                        dict["RSAddC_1"] = CustomerRC_DB.RCAddC_1;
                        dict["RSAddC_1_1"] = CustomerRC_DB.RCAddC_1_1;
                        dict["RSAddC_1_2"] = CustomerRC_DB.RCAddC_1_2;
                        dict["RSAddC_2"] = CustomerRC_DB.RCAddC_2;
                        dict["RSAddC_2_1"] = CustomerRC_DB.RCAddC_2_1;
                        dict["RSAddC_2_2"] = CustomerRC_DB.RCAddC_2_2;
                        dict["RSAddC_2_3"] = CustomerRC_DB.RCAddC_2_3;
                        dict["RSAddC_2_4"] = CustomerRC_DB.RCAddC_2_4;
                        dict["RSAddC_3"] = CustomerRC_DB.RCAddC_3;
                        dict["RSAddC_3_1"] = CustomerRC_DB.RCAddC_3_1;
                        dict["RSAddC_3_2"] = CustomerRC_DB.RCAddC_3_2;
                        dict["RSAddC_4"] = CustomerRC_DB.RCAddC_4;
                        dict["RSAddC_5"] = CustomerRC_DB.RCAddC_5;
                        dict["RSAddC_6"] = CustomerRC_DB.RCAddC_6;
                        dict["RSAddC_7"] = CustomerRC_DB.RCAddC_7;
                        dict["RSAddC_8"] = CustomerRC_DB.RCAddC_8;
                        dict["RSAddC_9"] = CustomerRC_DB.RCAddC_9;
                        //電話 11/28尚未出現
                        //dict["LSTel_1"] = CustomerRN_DB.RNTel_1;
                        //dict["LSTel_2"] = CustomerRN_DB.RNTel_2;
                        //手機
                        dict["RSCell"] = CustomerRC_DB.RCCell;
                        //代理人1  11/28尚未出現
                        //dict["LSAGName_A"] = CustomerRN_DB.RNAGName_A;
                        //dict["LSAGAdd_1_A"] = CustomerRN_DB.RNAGAdd_1_A;
                        //dict["LSAGAdd_1_1_A"] = CustomerRN_DB.RNAGAdd_1_1_A;
                        //dict["LSAGAdd_1_2_A"] = CustomerRN_DB.RNAGAdd_1_2_A;
                        //dict["LSAGAdd_2_A"] = CustomerRN_DB.RNAGAdd_2_A;
                        //dict["LSAGAdd_2_1_A"] = CustomerRN_DB.RNAGAdd_2_1_A;
                        //dict["LSAGAdd_2_2_A"] = CustomerRN_DB.RNAGAdd_2_2_A;
                        //dict["LSAGAdd_2_3_A"] = CustomerRN_DB.RNAGAdd_2_3_A;
                        //dict["LSAGAdd_2_4_A"] = CustomerRN_DB.RNAGAdd_2_4_A;
                        //dict["LSAGAdd_3_A"] = CustomerRN_DB.RNAGAdd_3_A;
                        //dict["LSAGAdd_3_1_A"] = CustomerRN_DB.RNAGAdd_3_1_A;
                        //dict["LSAGAdd_3_2_A"] = CustomerRN_DB.RNAGAdd_3_2_A;
                        //dict["LSAGAdd_4_A"] = CustomerRN_DB.RNAGAdd_4_A;
                        //dict["LSAGAdd_5_A"] = CustomerRN_DB.RNAGAdd_5_A;
                        //dict["LSAGAdd_6_A"] = CustomerRN_DB.RNAGAdd_6_A;
                        //dict["LSAGAdd_7_A"] = CustomerRN_DB.RNAGAdd_7_A;
                        //dict["LSAGAdd_8_A"] = CustomerRN_DB.RNAGAdd_8_A;
                        //dict["LSAGAdd_9_A"] = CustomerRN_DB.RNAGAdd_9_A;
                        //dict["LSAGCell_A"] = CustomerRN_DB.RNAGCell_A;
                        //dict["LSAGTel_1_A"] = CustomerRN_DB.RNAGTel_1_A;
                        //dict["LSAGTel_2_A"] = CustomerRN_DB.RNAGTel_2_A;
                        //dict["LSAGID_A"] = CustomerRN_DB.RNAGID_A;
                        #endregion LS欄位額外賦值
                    }
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, dict);
                }
            }
            #endregion

            #region 生日格式調整(86-02-07改為86/02/07)

            PDFDataModel.RNBirthday = PDFDataModel.RNBirthday != null ? PDFDataModel.RNBirthday.Replace("-", "/") : "";
            PDFDataModel.RNFBirthday_A = PDFDataModel.RNFBirthday_A != null ? PDFDataModel.RNFBirthday_A.Replace("-", "/") : "";
            PDFDataModel.RNFBirthday_B = PDFDataModel.RNFBirthday_B != null ? PDFDataModel.RNFBirthday_B.Replace("-", "/") : "";
            PDFDataModel.RNFBirthday_C = PDFDataModel.RNFBirthday_C != null ? PDFDataModel.RNFBirthday_C.Replace("-", "/") : "";
            PDFDataModel.RNFBirthday_D = PDFDataModel.RNFBirthday_D != null ? PDFDataModel.RNFBirthday_D.Replace("-", "/") : "";
            PDFDataModel.RNFBirthday_E = PDFDataModel.RNFBirthday_E != null ? PDFDataModel.RNFBirthday_E.Replace("-", "/") : "";
            PDFDataModel.RNFBirthday_F = PDFDataModel.RNFBirthday_F != null ? PDFDataModel.RNFBirthday_F.Replace("-", "/") : "";
            PDFDataModel.RNFBirthday_G = PDFDataModel.RNFBirthday_G != null ? PDFDataModel.RNFBirthday_G.Replace("-", "/") : "";

            #endregion 生日格式調整(86-02-07改為86/02/07)

            #region Building
            if (!string.IsNullOrWhiteSpace(searchPDF.BAdd_1)) {

                searchPDF.BAdd =
                Utils.memergeAddFix(
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
                   searchPDF.BAdd_9
                );

                Building Building_DB = await IBuildingRepository.GetByBAdd(searchPDF.BAdd);
                Building1 Building1_DB = await _building1Service.GetByBAdd(searchPDF.BAdd);

                if (Building_DB != null) {
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (Building_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(Building_DB, null))));
                    #region 二房東欄位處理
                    {
                        var data = await _secondLandlordService.GetBySLID(Building_DB.BelongSLID);
                        if (data != null) {
                            PDFDataModel.SLID = data.SLID;
                            PDFDataModel.SLName = data.SLName;
                        }

                    }
                    #endregion 二房東欄位處理
                }
                if (Building1_DB != null) {
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (Building1_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(Building1_DB, null))));
                }
            }

            //不知下面判斷BID的用意
            //else if (!string.IsNullOrWhiteSpace(searchPDF.BID)) {
            //    Building Building_DB = await IBuildingRepository.GetByBID(searchPDF.BID);

            //    if (Building_DB != null) {
            //        PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (Building_DB.GetType()
            //            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            //                .ToDictionary(prop => prop.Name, prop => prop.GetValue(Building_DB, null))));
            //    }
            //}
            #endregion

            #region Externalform
            Externalform Externalform_DB = await iService.GetByFormId(searchPDF.FormId);

            #endregion

            #region TBNoB1
            if ("B1" == Externalform_DB?.TBNO) {
                string sqlWhere = "1=1";
                string refkeys = "";
                sqlWhere += $" and FName='{Externalform_DB.FormId}'";
                if (!string.IsNullOrEmpty(searchPDF.LNID))
                    refkeys += searchPDF.LNID;
                if (!string.IsNullOrEmpty(searchPDF.LCID))
                    refkeys += searchPDF.LCID;
                if (!string.IsNullOrEmpty(searchPDF.RNID))
                    refkeys += searchPDF.RNID;
                if (!string.IsNullOrEmpty(searchPDF.RCID))
                    refkeys += searchPDF.RCID;
                if (!string.IsNullOrEmpty(searchPDF.BAdd))
                    refkeys += searchPDF.BAdd;
                if (!string.IsNullOrEmpty(refkeys)) {
                    sqlWhere += $"and Refkeys='{refkeys}'";
                }


                var TBNoB1_DB = ITBNoB1Service.GetWhere($"{sqlWhere}");

                //TBNoB1 TBNoB1_DB = await ITBNoB1Repository.GetByBAdd(PDFDataModel.BAdd);

                if (TBNoB1_DB != null) {
                    var TBNoB1_2_DB = await _tBNoB1_2Service.GetAsync(TBNoB1_DB.Id);
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (TBNoB1_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(TBNoB1_DB, null))));
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (TBNoB1_2_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(TBNoB1_2_DB, null))));
                }
            }
            #endregion

            #region TBNoB2
            if ("B2" == Externalform_DB?.TBNO) {
                string sqlWhere = "1=1";
                string refkeys = "";
                sqlWhere += $" and FName='{Externalform_DB.FormId}'";
                if (!string.IsNullOrEmpty(searchPDF.LNID))
                    refkeys += searchPDF.LNID;
                if (!string.IsNullOrEmpty(searchPDF.LCID))
                    refkeys += searchPDF.LCID;
                if (!string.IsNullOrEmpty(searchPDF.RNID))
                    refkeys += searchPDF.RNID;
                if (!string.IsNullOrEmpty(searchPDF.RCID))
                    refkeys += searchPDF.RCID;
                if (!string.IsNullOrEmpty(searchPDF.BAdd))
                    refkeys += searchPDF.BAdd;
                if (!string.IsNullOrEmpty(refkeys)) {
                    sqlWhere += $"and Refkeys='{refkeys}'";
                }


                var TBNoB2_DB = ITBNoB2Service.GetWhere($"{sqlWhere}");

                //TBNoB2 TBNoB2_DB = await ITBNoB2Repository.GetByBAdd(PDFDataModel.BAdd);

                if (TBNoB2_DB != null) {
                    var TBNoB2_2_DB = await _tBNoB2_2Service.GetAsync(TBNoB2_DB.Id);
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (TBNoB2_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(TBNoB2_DB, null))));
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (TBNoB2_2_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(TBNoB2_2_DB, null))));
                }
            }
            #endregion

            #region TBNoB3
            if ("B3" == Externalform_DB?.TBNO) {
                string sqlWhere = "1=1";
                string refkeys = "";
                sqlWhere += $" and FName='{Externalform_DB.FormId}'";
                if (!string.IsNullOrEmpty(searchPDF.LNID))
                    refkeys += searchPDF.LNID;
                if (!string.IsNullOrEmpty(searchPDF.LCID))
                    refkeys += searchPDF.LCID;
                if (!string.IsNullOrEmpty(searchPDF.RNID))
                    refkeys += searchPDF.RNID;
                if (!string.IsNullOrEmpty(searchPDF.RCID))
                    refkeys += searchPDF.RCID;
                if (!string.IsNullOrEmpty(searchPDF.BAdd))
                    refkeys += searchPDF.BAdd;
                if (!string.IsNullOrEmpty(refkeys)) {
                    sqlWhere += $"and Refkeys='{refkeys}'";
                }


                var TBNoB3_DB = ITBNoB3Service.GetWhere($"{sqlWhere}");

                //TBNoB3 TBNoB3_DB = await ITBNoB3Repository.GetByBAdd(PDFDataModel.BAdd);

                if (TBNoB3_DB != null) {
                    var TBNoB3_2_DB = await _tBNoB3_2Service.GetAsync(TBNoB3_DB.Id);
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (TBNoB3_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(TBNoB3_DB, null))));
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (TBNoB3_2_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(TBNoB3_2_DB, null))));
                }
            }
            #endregion

            #region TBNoB4
            if ("B4" == Externalform_DB?.TBNO) {
                string sqlWhere = "1=1";
                string refkeys = "";
                sqlWhere += $" and FName='{Externalform_DB.FormId}'";
                if (!string.IsNullOrEmpty(searchPDF.LNID))
                    refkeys += searchPDF.LNID;
                if (!string.IsNullOrEmpty(searchPDF.LCID))
                    refkeys += searchPDF.LCID;
                if (!string.IsNullOrEmpty(searchPDF.RNID))
                    refkeys += searchPDF.RNID;
                if (!string.IsNullOrEmpty(searchPDF.RCID))
                    refkeys += searchPDF.RCID;
                if (!string.IsNullOrEmpty(searchPDF.BAdd))
                    refkeys += searchPDF.BAdd;
                if (!string.IsNullOrEmpty(refkeys)) {
                    sqlWhere += $"and Refkeys='{refkeys}'";
                }


                var TBNoB4_DB = ITBNoB4Service.GetWhere($"{sqlWhere}");

                //TBNoB4 TBNoB4_DB = await ITBNoB4Repository.GetByBAdd(PDFDataModel.BAdd);

                if (TBNoB4_DB != null) {
                    var TBNoB4_2_DB = await _tBNoB4_2Service.GetAsync(TBNoB4_DB.Id);
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (TBNoB4_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(TBNoB4_DB, null))));
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (TBNoB4_2_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(TBNoB4_2_DB, null))));
                }
            }
            #endregion  

            #region TBNoB5
            if ("B5" == Externalform_DB?.TBNO) {
                string sqlWhere = "1=1";
                string refkeys = "";
                sqlWhere += $" and FName='{Externalform_DB.FormId}'";
                if (!string.IsNullOrEmpty(searchPDF.LNID))
                    refkeys += searchPDF.LNID;
                if (!string.IsNullOrEmpty(searchPDF.LCID))
                    refkeys += searchPDF.LCID;
                if (!string.IsNullOrEmpty(searchPDF.RNID))
                    refkeys += searchPDF.RNID;
                if (!string.IsNullOrEmpty(searchPDF.RCID))
                    refkeys += searchPDF.RCID;
                if (!string.IsNullOrEmpty(searchPDF.BAdd))
                    refkeys += searchPDF.BAdd;
                if (!string.IsNullOrEmpty(refkeys)) {
                    sqlWhere += $"and Refkeys='{refkeys}'";
                }


                var TBNoB5_DB = ITBNoB5Service.GetWhere($"{sqlWhere}");

                //TBNoB5 TBNoB5_DB = await ITBNoB5Repository.GetByBAdd(PDFDataModel.BAdd);

                if (TBNoB5_DB != null) {
                    var TBNoB5_2_DB = await _tBNoB5_2Service.GetAsync(TBNoB5_DB.Id);
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (TBNoB5_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(TBNoB5_DB, null))));
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (TBNoB5_2_DB.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(TBNoB5_2_DB, null))));
                }
            }
            #endregion   

            #region Remittance

            //有指定出租人匯款資訊
            if (!string.IsNullOrEmpty(searchPDF.LRemittanceId)) {
                var remittanceLN = await _remittanceLService.GetAsync(searchPDF.LRemittanceId);
                if (remittanceLN != null) {
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (remittanceLN.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(remittanceLN, null))));
                }
            }
            //沒指定的話就用LNID 或 LCID 去找，若有找到預設使用第一組匯款資訊
            else {
                if (!string.IsNullOrEmpty(searchPDF.LNID)) {
                    var reLNList = await _remittanceLService.GetListWhereAsync($" IDNo = '{searchPDF.LNID}' ");
                    if (reLNList.Count() > 0) {
                        var reln = reLNList.FirstOrDefault();
                        PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (reln.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .ToDictionary(prop => prop.Name, prop => prop.GetValue(reln, null))));
                    }
                } else if (!string.IsNullOrEmpty(searchPDF.LCID)) {
                    var reLNList = await _remittanceLService.GetListWhereAsync($" IDNo = '{searchPDF.LCID}' ");
                    if (reLNList.Count() > 0) {
                        var reln = reLNList.FirstOrDefault();
                        PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (reln.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .ToDictionary(prop => prop.Name, prop => prop.GetValue(reln, null))));
                    }
                }
            }

            //有指定承租人匯款資訊
            if (!string.IsNullOrEmpty(searchPDF.RRemittanceId)) {
                var remittanceLN = await _remittanceRService.GetAsync(searchPDF.RRemittanceId);
                if (remittanceLN != null) {
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (remittanceLN.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(remittanceLN, null))));
                }
                var test = remittanceLN.RAName;
            }
            //沒指定的話就用LNID 或 LCID 去找，若有找到預設使用第一組匯款資訊
            else {
                if (!string.IsNullOrEmpty(searchPDF.RNID)) {
                    var reLNList = await _remittanceRService.GetListWhereAsync($" IDNo = '{searchPDF.RNID}' ");
                    if (reLNList.Count() > 0) {
                        var reln = reLNList.FirstOrDefault();
                        PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (reln.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .ToDictionary(prop => prop.Name, prop => prop.GetValue(reln, null))));
                    }
                } else if (!string.IsNullOrEmpty(searchPDF.RCID)) {
                    var reLNList = await _remittanceRService.GetListWhereAsync($" IDNo = '{searchPDF.RCID}' ");
                    if (reLNList.Count() > 0) {
                        var reln = reLNList.FirstOrDefault();
                        PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (reln.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .ToDictionary(prop => prop.Name, prop => prop.GetValue(reln, null))));
                    }
                }
            }

            #endregion

            return PDFDataModel;
        }

        /// <summary>
        /// 異步分頁查詢
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet("ShowImg")]
        [YuebonAuthorize("List")]
        [LogNotMethod]
        public IActionResult Get(string fileName)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

            var image = System.IO.File.OpenRead($"{sysSetting.ChaochiFilepath}{fileName}");
            //https://stackoverflow.com/questions/34131326/using-mimemapping-in-asp-net-core
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
            return File(image, contentType ?? "image/jpeg");
        }

        /// <summary>
        ///  上傳
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("UploadPDFWithData")]
        [NoSignRequired]
        public async Task<IActionResult> UploadPDFWithDataAsync([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            //string _fileName = filelist[0].FileName;
            try {
                //string FormId = "";
                //if (formCollection.ContainsKey("FormId"))
                //{
                //    FormId = formCollection["FormId"];
                //}

                if (filelist == null || filelist.Count <= 0 || filelist.Count >= 2) {
                    result.ErrCode = "40001";
                    result.ErrMsg = "只能有一個上傳PDF檔案";
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
                            result.ErrMsg = "上傳PDF檔案不含任何標籤";
                            return ToJsonContent(result, true);
                        } else {
                            //取值
                            foreach (var name in doc.AcroForm.Fields.Names) {
                                if (doc.AcroForm.Fields[name].Value is PdfString) {
                                    if (doc.AcroForm.Fields[name].Value.ToString() == "<FEFF>") {
                                        dictV.Add(name, "");
                                    } else {
                                        dictV.Add(name, ((PdfString)doc.AcroForm.Fields[name].Value).Value);
                                    }
                                } else if (doc.AcroForm.Fields[name].Value is PdfItem) {
                                    dictV.Add(name, ((PdfItem)doc.AcroForm.Fields[name].Value).ToString());
                                }
                            }
                        }
                    }
                }

                if (dictV.ContainsKey("Nokey")) {
                    if (dictV["Nokey"].ToString() == "Y") {
                        if (string.IsNullOrEmpty(formCollection["data"].ToString()) && string.IsNullOrEmpty(formCollection["achTo"].ToString())) {
                            result.Success = false;
                            result.ResData = "IsNoKey";
                            result.ErrCode = ErrCode.err40001;
                            return ToJsonContent(result);
                        } else {
                            if (formCollection["isNokey"].ToString() == "Y") {

                                var achTo = formCollection["achTo"].ToString();
                                var data = formCollection["data"].ToString();
                                var exForm = await iService.GetByFormId(dictV["FName"].ToString());
                                var fName = dictV["FName"].ToString()+exForm.FormName;
                                try {
                                    await SaveNoKeyForm(achTo, data, fName, filelist[0]);
                                    result.Success = true;
                                    result.ErrCode = ErrCode.successCode;

                                    return ToJsonContent(result);
                                } catch (Exception ex) {
                                    result.Success = false;
                                    result.ErrMsg = ex.Message;
                                    return ToJsonContent(result);
                                    throw;
                                }

                            }

                        }
                    }
                    result.Success = false;
                    result.ErrMsg = "無主表單有誤";
                    return ToJsonContent(result);
                }


                var PDFDataModel = Merger.DictCloneAndMerge(new PDFDataModel(), dictV);

                var Externalform = await iService.GetByFormId(PDFDataModel.FName);
                if (Externalform == null) {
                    result.Success = false;
                    result.ErrMsg = $"查無外部表單[{PDFDataModel.FName}]設定";
                    return ToJsonContent(result);
                }

                if (string.IsNullOrEmpty(PDFDataModel.Vno)) {
                    result.Success = false;
                    result.ErrMsg = "上傳PDF檔案不含標籤[Vno]";
                    return ToJsonContent(result);
                } else {
                    if (PDFDataModel.Vno != Externalform.Vno) {
                        result.Success = false;
                        result.ErrMsg = $"上傳PDF檔案標籤[Vno]不為{Externalform.Vno}";
                        return ToJsonContent(result);
                    }
                }

                if (string.IsNullOrWhiteSpace(Externalform.ArchiveLTo)) {
                    result.Success = false;
                    result.ErrMsg = $"外部表單[{PDFDataModel.FName}] [{Externalform.FormName}] 歷史存檔未選擇!";
                    return ToJsonContent(result);
                }
                if (PDFDataModel.TBNO != Externalform.TBNO) {
                    result.Success = false;
                    result.ErrMsg = $"TBNo不一致 無法上傳";
                    return ToJsonContent(result);
                }

                // Key 補值
                if (!string.IsNullOrEmpty(PDFDataModel.LNID_1_1)) {
                    PDFDataModel.LNID = PDFDataModel.LNID_1_1 + PDFDataModel.LNID_1_2 + PDFDataModel.LNID_1_3 + PDFDataModel.LNID_1_4 + PDFDataModel.LNID_1_5 + PDFDataModel.LNID_1_6 + PDFDataModel.LNID_1_7 + PDFDataModel.LNID_1_8 + PDFDataModel.LNID_1_9 + PDFDataModel.LNID_1_10;
                }

                if (!string.IsNullOrEmpty(PDFDataModel.LCID_1_1)) {
                    PDFDataModel.LCID = PDFDataModel.LCID_1_1 + PDFDataModel.LCID_1_2 + PDFDataModel.LCID_1_3 + PDFDataModel.LCID_1_4 + PDFDataModel.LCID_1_5 + PDFDataModel.LCID_1_6 + PDFDataModel.LCID_1_7 + PDFDataModel.LCID_1_8;
                }

                //if (!dictV.ContainsKey("LSID")) {
                //    dictV["LSID"] = "";
                //}
                //if (!dictV.ContainsKey("BAdd_1")) {
                //    dictV["BAdd_1"] = "";
                //}
                //if (!dictV.ContainsKey("BAdd")) {
                //    dictV["BAdd"] = "";
                //}
                //if (!dictV.ContainsKey("RNID")) {
                //    dictV["RNID"] = "";
                //}
                //if (!dictV.ContainsKey("RCID")) {
                //    dictV["RCID"] = "";
                //}
                //if (!dictV.ContainsKey("RSID")) {
                //    dictV["RSID"] = "";
                //}

                // 必填檢查 房東
                if (Externalform.RequiredLandlord == "Y") {
                    if (string.IsNullOrEmpty(PDFDataModel.LNID) && string.IsNullOrEmpty(PDFDataModel.LCID) && string.IsNullOrEmpty(PDFDataModel.LSID)) {
                        result.Success = false;
                        result.ErrMsg = $"外部表單[{PDFDataModel.FName}] [{Externalform.FormName}] 出租人不可空白";
                        return ToJsonContent(result);
                    }
                }

                // 必填檢查 建物
                if (Externalform.RequiredBuilding == "Y") {
                    if (string.IsNullOrEmpty(PDFDataModel.BAdd) && string.IsNullOrEmpty(PDFDataModel.BAdd_1)) {
                        result.Success = false;
                        result.ErrMsg = $"外部表單[{PDFDataModel.FName}] [{Externalform.FormName}] 建物地址不可空白";
                        return ToJsonContent(result);
                    }
                }

                // 必填檢查 房客
                if (Externalform.RequiredRenter == "Y") {
                    if (string.IsNullOrEmpty(PDFDataModel.RNID) && string.IsNullOrEmpty(PDFDataModel.RCID) && string.IsNullOrEmpty(PDFDataModel.RSID)) {
                        result.Success = false;
                        result.ErrMsg = $"外部表單[{PDFDataModel.FName}] [{Externalform.FormName}] 承租人不可空白";
                        return ToJsonContent(result);
                    }
                }

                //必需為已存在檢查 房東
                //先檢查LSID
                if (!string.IsNullOrEmpty(PDFDataModel.LSID)) {
                    var lsid = PDFDataModel.LSID;
                    if (lsid.Length == 8) {
                        PDFDataModel.LCID = lsid;
                    } else if (lsid.Length == 10) {
                        PDFDataModel.LNID = lsid;
                    } else {
                        result.Success = false;
                        result.ErrMsg = $"LSID欄位內容必須為10碼或8碼";
                        return ToJsonContent(result);
                    }
                }

                if (Externalform.MustExistsLandLord == "Y") {
                    var checkLN = await CustomerLNService.GetByCustomerLNID(PDFDataModel.LNID);
                    var checkLC = await CustomerLCService.GetByLCID(PDFDataModel.LCID);
                    if (checkLN == null && checkLC == null) {
                        result.Success = false;
                        result.ErrMsg = $"外部表單[{PDFDataModel.FName}] [{Externalform.FormName}] 此出租人不存在 不允許上傳";
                        return ToJsonContent(result);
                    }
                }

                //必需為已存在檢查 建物
                if (Externalform.MustExistsBuilding == "Y") {
                    string bAdd = "";
                    if (!string.IsNullOrEmpty(PDFDataModel.BAdd)) {
                        bAdd = PDFDataModel.BAdd;
                    } else {
                        // var pdfModel = Merger.DictCloneAndMerge<PDFDataModel>(new PDFDataModel(), dictV);
                        bAdd = Utils.memergeAddFix(
                           PDFDataModel.BAdd_1,
                           PDFDataModel.BAdd_1_1,
                           PDFDataModel.BAdd_1_2,
                           PDFDataModel.BAdd_2,
                           PDFDataModel.BAdd_2_1,
                           PDFDataModel.BAdd_2_2,
                           PDFDataModel.BAdd_2_3,
                           PDFDataModel.BAdd_2_4,
                           PDFDataModel.BAdd_3,
                           PDFDataModel.BAdd_3_1,
                           PDFDataModel.BAdd_3_2,
                           PDFDataModel.BAdd_4,
                           PDFDataModel.BAdd_5,
                           PDFDataModel.BAdd_6,
                           PDFDataModel.BAdd_7,
                           PDFDataModel.BAdd_8,
                           PDFDataModel.BAdd_9);
                    }

                    var checkBId = buildingService.GetIdByBAdd(bAdd);
                    if (string.IsNullOrEmpty(checkBId)) {
                        result.Success = false;
                        result.ErrMsg = $"外部表單[{PDFDataModel.FName}] [{Externalform.FormName}] </br> [{bAdd}] 此建物不存在 不允許上傳";
                        return ToJsonContent(result);
                    }
                }

                //必需為已存在檢查 房客
                //先檢查RSID
                if (!string.IsNullOrEmpty(PDFDataModel.RSID)) {
                    var rsid = PDFDataModel.RSID;
                    if (rsid.Length == 8) {
                        PDFDataModel.RCID = rsid;
                    } else if (rsid.Length == 10) {
                        PDFDataModel.RNID = rsid;
                    } else {
                        result.Success = false;
                        result.ErrMsg = $"RSID欄位內容必須為10碼或8碼";
                        return ToJsonContent(result);
                    }
                }

                if (Externalform.MustExistsRenter == "Y") {
                    var checkRN = await CustomerRNService.GetCustomerByRNID(PDFDataModel.RNID);
                    var checkRC = await _customerRCService.GetWhereAsync($" RCID = '{PDFDataModel.RCID}' ");
                    if (checkRN == null && checkRC == null) {
                        result.Success = false;
                        result.ErrMsg = $"外部表單[{PDFDataModel.FName}] [{Externalform.FormName}] 此承租人不存在 不允許上傳";
                        return ToJsonContent(result);
                    }
                }

                //存取權限
                var ccuser = CurrentUser;
                if (!CurrentUser.Role.Contains("administrators")) {
                    var checkAuth = await CheckAuth(PDFDataModel);
                    if (checkAuth.Success == false) {
                        return ToJsonContent(checkAuth);
                    }
                }

                await SaveData(PDFDataModel);


                Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
                Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

                DateTime dt = DateTime.Now;
                string FileName = dt.ToString("yyyyMMddHHmmss") + "_" + PDFDataModel.FName;//filelist[0].FileName;
                string uploadPath = sysSetting.ChaochiFilepath + "historyPDF/CustomerL/LN/";//這邊之後加工需要不同的目錄

                if (Externalform.ArchiveLTo == "出租人") {
                    string LID =
                        (new List<string>() {
                            PDFDataModel.LSID,
                            PDFDataModel.LNID,
                            PDFDataModel.LCID,
                        }.Where(id => !string.IsNullOrWhiteSpace(id)).FirstOrDefault());

                    if (Utils.isCompany(LID)) {
                        uploadPath = sysSetting.ChaochiFilepath + $"historyPDF/CustomerL/LC/{PDFDataModel.LCID}/";
                        long count = await IHistoryFormLCService.InsertAsync(
                            new HistoryFormLC() {
                                IDNo = PDFDataModel.LCID,
                                FormName = $"{dictV["FName"]}_{Externalform.FormName}",
                                Note = "外部表單PDF上傳",
                                UploadTime = dt,
                                FileName = FileName + ".pdf",
                                CreatorUserId = CurrentUser.UserId
                            });
                    } else {
                        uploadPath = sysSetting.ChaochiFilepath + $"historyPDF/CustomerL/LN/{PDFDataModel.LNID}/";
                        long count = await IHistoryFormLNService.InsertAsync(
                            new HistoryFormLN() {
                                IDNo = PDFDataModel.LNID,
                                FormName = $"{dictV["FName"]}_{Externalform.FormName}",
                                Note = "外部表單PDF上傳",
                                UploadTime = dt,
                                FileName = FileName + ".pdf",
                                CreatorUserId = CurrentUser.UserId
                            });
                    }
                } else if (Externalform.ArchiveLTo == "建物") {
                    //PDFDataModel PDFDataModel_NEW = Merger.DictCloneAndMerge<PDFDataModel>(new PDFDataModel(), dictV);
                    if (string.IsNullOrEmpty(PDFDataModel.BAdd)) {
                        PDFDataModel.BAdd = Utils.memergeAddFix(
                            PDFDataModel.BAdd_1,
                            PDFDataModel.BAdd_1_1,
                            PDFDataModel.BAdd_1_2,
                            PDFDataModel.BAdd_2,
                            PDFDataModel.BAdd_2_1,
                            PDFDataModel.BAdd_2_2,
                            PDFDataModel.BAdd_2_3,
                            PDFDataModel.BAdd_2_4,
                            PDFDataModel.BAdd_3,
                            PDFDataModel.BAdd_3_1,
                            PDFDataModel.BAdd_3_2,
                            PDFDataModel.BAdd_4,
                            PDFDataModel.BAdd_5,
                            PDFDataModel.BAdd_6,
                            PDFDataModel.BAdd_7,
                            PDFDataModel.BAdd_8,
                            PDFDataModel.BAdd_9);
                    }

                    var buildingId = buildingService.GetIdByBAdd(PDFDataModel.BAdd);

                    uploadPath = sysSetting.ChaochiFilepath + $"historyPDF/Building/{buildingId}/";
                    long count = await IHistoryFormBuildingService.InsertAsync(
                            new HistoryFormBuilding() {
                                IDNo = buildingId,
                                FormName = $"{dictV["FName"]}_{Externalform.FormName}",
                                Note = "外部表單PDF上傳",
                                UploadTime = dt,
                                FileName = FileName + ".pdf",
                                CreatorUserId = CurrentUser.UserId
                            });

                } else if (Externalform.ArchiveLTo == "承租人") {
                    //string RID = (string)(dictV["RSID"] ?? dictV["RNID"] ?? dictV["RCID"]);
                    string RID =
                        (new List<string>() {
                            PDFDataModel.RSID,
                            PDFDataModel.RNID,
                            PDFDataModel.RCID,
                        }.Where(id => !string.IsNullOrWhiteSpace(id)).FirstOrDefault());
                    if (Utils.isCompany(RID)) {
                        uploadPath = sysSetting.ChaochiFilepath + $"historyPDF/CustomerR/RC/{PDFDataModel.RCID}/";
                        long count = await IHistoryFormRCService.InsertAsync(
                            new HistoryFormRC() {
                                IDNo = PDFDataModel.RCID,
                                FormName = $"{dictV["FName"]}_{Externalform.FormName}",
                                Note = "外部表單PDF上傳",
                                UploadTime = dt,
                                FileName = FileName + ".pdf",
                                CreatorUserId = CurrentUser.UserId
                            });
                    } else {
                        uploadPath = sysSetting.ChaochiFilepath + $"historyPDF/CustomerR/RN/{PDFDataModel.RNID}/";
                        long count = await IHistoryFormRNService.InsertAsync(
                            new HistoryFormRN() {
                                IDNo = PDFDataModel.RNID,
                                FormName = $"{dictV["FName"]}_{Externalform.FormName}",
                                Note = "外部表單PDF上傳",
                                UploadTime = dt,
                                FileName = FileName + ".pdf",
                                CreatorUserId = CurrentUser.UserId
                            });
                    }
                }

                result.ResData = AddFile(filelist[0], uploadPath, FileName);

                result.ErrCode = ErrCode.successCode;
                result.Success = true;
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
        /// 
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost("CheckIsNoKeyForm")]
        [NoSignRequired]
        public IActionResult CheckIsNoKeyForm([FromForm] IFormCollection formCollection)
        {
            var result = new CommonResult();
            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            Dictionary<string, object> dictV = new Dictionary<string, object>();
            IFormFile file = filelist[0];
            if (file != null && file.Length > 0) {
                using (var doc = PdfReader.Open(file.OpenReadStream(), PdfDocumentOpenMode.Modify)) {
                    if (doc.AcroForm == null) {
                        doc.Close();
                        result.ErrCode = "40001";
                        result.ErrMsg = "上傳PDF檔案不含任何標籤";
                        return ToJsonContent(result, true);
                    } else {
                        if (doc.AcroForm.Fields.Names.Contains("Nokey")) {
                            result.ResData = true;
                            result.ErrCode = ErrCode.successCode;
                            return ToJsonContent(result);
                        } else {
                            result.ResData = false;
                            result.ErrCode = ErrCode.successCode;
                            return ToJsonContent(result);
                        }
                        ////取值
                        //foreach (var name in doc.AcroForm.Fields.Names) {
                        //    if (doc.AcroForm.Fields[name].Value is PdfString) {
                        //        if (doc.AcroForm.Fields[name].Value.ToString() == "<FEFF>") {
                        //            dictV.Add(name, "");
                        //        } else {
                        //            dictV.Add(name, ((PdfString)doc.AcroForm.Fields[name].Value).Value);
                        //        }
                        //    } else if (doc.AcroForm.Fields[name].Value is PdfItem) {
                        //        dictV.Add(name, ((PdfItem)doc.AcroForm.Fields[name].Value).ToString());
                        //    }
                        //}
                    }
                }
            }

            result.ErrCode = "40001";
            result.ErrMsg = "上傳PDF檔案不含任何標籤";
            return ToJsonContent(result, true);
        }

        private async Task SaveData(PDFDataModel pDFDataModel)
        {
            // PDFDataModel PDFDataModel_NEW = Merger.DictCloneAndMerge<PDFDataModel>(new PDFDataModel(), dictV);
            PDFDataModel PDFDataModel_NEW = pDFDataModel;

            #region 地址處理 PDF上所有地址都是唯讀且不允許修改 所以不需要額外處理地址了
            //PDFDataModel_NEW.LNAdd =
            //    Utils.memergeAddFix(
            //       PDFDataModel_NEW.LNAdd_1,
            //       PDFDataModel_NEW.LNAdd_1_1,
            //       PDFDataModel_NEW.LNAdd_1_2,
            //       PDFDataModel_NEW.LNAdd_2,
            //       PDFDataModel_NEW.LNAdd_2_1,
            //       PDFDataModel_NEW.LNAdd_2_2,
            //       PDFDataModel_NEW.LNAdd_2_3,
            //       PDFDataModel_NEW.LNAdd_2_4,
            //       PDFDataModel_NEW.LNAdd_3,
            //       PDFDataModel_NEW.LNAdd_3_1,
            //       PDFDataModel_NEW.LNAdd_3_2,
            //       PDFDataModel_NEW.LNAdd_4,
            //       PDFDataModel_NEW.LNAdd_5,
            //       PDFDataModel_NEW.LNAdd_6,
            //       PDFDataModel_NEW.LNAdd_7,
            //       PDFDataModel_NEW.LNAdd_8,
            //       PDFDataModel_NEW.LNAdd_9
            //    );
            //PDFDataModel_NEW.LNAddC =
            //    Utils.memergeAddFix(
            //       PDFDataModel_NEW.LNAddC_1,
            //       PDFDataModel_NEW.LNAddC_1_1,
            //       PDFDataModel_NEW.LNAddC_1_2,
            //       PDFDataModel_NEW.LNAddC_2,
            //       PDFDataModel_NEW.LNAddC_2_1,
            //       PDFDataModel_NEW.LNAddC_2_2,
            //       PDFDataModel_NEW.LNAddC_2_3,
            //       PDFDataModel_NEW.LNAddC_2_4,
            //       PDFDataModel_NEW.LNAddC_3,
            //       PDFDataModel_NEW.LNAddC_3_1,
            //       PDFDataModel_NEW.LNAddC_3_2,
            //       PDFDataModel_NEW.LNAddC_4,
            //       PDFDataModel_NEW.LNAddC_5,
            //       PDFDataModel_NEW.LNAddC_6,
            //       PDFDataModel_NEW.LNAddC_7,
            //       PDFDataModel_NEW.LNAddC_8,
            //       PDFDataModel_NEW.LNAddC_9
            //    );
            //PDFDataModel_NEW.LNAGAdd_A =
            //    Utils.memergeAddFix(
            //       PDFDataModel_NEW.LNAGAdd_1_A,
            //       PDFDataModel_NEW.LNAGAdd_1_1_A,
            //       PDFDataModel_NEW.LNAGAdd_1_2_A,
            //       PDFDataModel_NEW.LNAGAdd_2_A,
            //       PDFDataModel_NEW.LNAGAdd_2_1_A,
            //       PDFDataModel_NEW.LNAGAdd_2_2_A,
            //       PDFDataModel_NEW.LNAGAdd_2_3_A,
            //       PDFDataModel_NEW.LNAGAdd_2_4_A,
            //       PDFDataModel_NEW.LNAGAdd_3_A,
            //       PDFDataModel_NEW.LNAGAdd_3_1_A,
            //       PDFDataModel_NEW.LNAGAdd_3_2_A,
            //       PDFDataModel_NEW.LNAGAdd_4_A,
            //       PDFDataModel_NEW.LNAGAdd_5_A,
            //       PDFDataModel_NEW.LNAGAdd_6_A,
            //       PDFDataModel_NEW.LNAGAdd_7_A,
            //       PDFDataModel_NEW.LNAGAdd_8_A,
            //       PDFDataModel_NEW.LNAGAdd_9_A
            //    );
            //PDFDataModel_NEW.LNAGAdd_B =
            //    Utils.memergeAddFix(
            //       PDFDataModel_NEW.LNAGAdd_1_B,
            //       PDFDataModel_NEW.LNAGAdd_1_1_B,
            //       PDFDataModel_NEW.LNAGAdd_1_2_B,
            //       PDFDataModel_NEW.LNAGAdd_2_B,
            //       PDFDataModel_NEW.LNAGAdd_2_1_B,
            //       PDFDataModel_NEW.LNAGAdd_2_2_B,
            //       PDFDataModel_NEW.LNAGAdd_2_3_B,
            //       PDFDataModel_NEW.LNAGAdd_2_4_B,
            //       PDFDataModel_NEW.LNAGAdd_3_B,
            //       PDFDataModel_NEW.LNAGAdd_3_1_B,
            //       PDFDataModel_NEW.LNAGAdd_3_2_B,
            //       PDFDataModel_NEW.LNAGAdd_4_B,
            //       PDFDataModel_NEW.LNAGAdd_5_B,
            //       PDFDataModel_NEW.LNAGAdd_6_B,
            //       PDFDataModel_NEW.LNAGAdd_7_B,
            //       PDFDataModel_NEW.LNAGAdd_8_B,
            //       PDFDataModel_NEW.LNAGAdd_9_B
            //    );
            //PDFDataModel_NEW.LCAdd =
            //    Utils.memergeAddFix(
            //       PDFDataModel_NEW.LCAdd_1,
            //       PDFDataModel_NEW.LCAdd_1_1,
            //       PDFDataModel_NEW.LCAdd_1_2,
            //       PDFDataModel_NEW.LCAdd_2,
            //       PDFDataModel_NEW.LCAdd_2_1,
            //       PDFDataModel_NEW.LCAdd_2_2,
            //       PDFDataModel_NEW.LCAdd_2_3,
            //       PDFDataModel_NEW.LCAdd_2_4,
            //       PDFDataModel_NEW.LCAdd_3,
            //       PDFDataModel_NEW.LCAdd_3_1,
            //       PDFDataModel_NEW.LCAdd_3_2,
            //       PDFDataModel_NEW.LCAdd_4,
            //       PDFDataModel_NEW.LCAdd_5,
            //       PDFDataModel_NEW.LCAdd_6,
            //       PDFDataModel_NEW.LCAdd_7,
            //       PDFDataModel_NEW.LCAdd_8,
            //       PDFDataModel_NEW.LCAdd_9
            //    );
            //PDFDataModel_NEW.LCAGAdd_A =
            //    Utils.memergeAddFix(
            //       PDFDataModel_NEW.LCAGAdd_1_A,
            //       PDFDataModel_NEW.LCAGAdd_1_1_A,
            //       PDFDataModel_NEW.LCAGAdd_1_2_A,
            //       PDFDataModel_NEW.LCAGAdd_2_A,
            //       PDFDataModel_NEW.LCAGAdd_2_1_A,
            //       PDFDataModel_NEW.LCAGAdd_2_2_A,
            //       PDFDataModel_NEW.LCAGAdd_2_3_A,
            //       PDFDataModel_NEW.LCAGAdd_2_4_A,
            //       PDFDataModel_NEW.LCAGAdd_3_A,
            //       PDFDataModel_NEW.LCAGAdd_3_1_A,
            //       PDFDataModel_NEW.LCAGAdd_3_2_A,
            //       PDFDataModel_NEW.LCAGAdd_4_A,
            //       PDFDataModel_NEW.LCAGAdd_5_A,
            //       PDFDataModel_NEW.LCAGAdd_6_A,
            //       PDFDataModel_NEW.LCAGAdd_7_A,
            //       PDFDataModel_NEW.LCAGAdd_8_A,
            //       PDFDataModel_NEW.LCAGAdd_9_A
            //    );
            //PDFDataModel_NEW.LCAGAdd_B =
            //    Utils.memergeAddFix(
            //       PDFDataModel_NEW.LCAGAdd_1_B,
            //       PDFDataModel_NEW.LCAGAdd_1_1_B,
            //       PDFDataModel_NEW.LCAGAdd_1_2_B,
            //       PDFDataModel_NEW.LCAGAdd_2_B,
            //       PDFDataModel_NEW.LCAGAdd_2_1_B,
            //       PDFDataModel_NEW.LCAGAdd_2_2_B,
            //       PDFDataModel_NEW.LCAGAdd_2_3_B,
            //       PDFDataModel_NEW.LCAGAdd_2_4_B,
            //       PDFDataModel_NEW.LCAGAdd_3_B,
            //       PDFDataModel_NEW.LCAGAdd_3_1_B,
            //       PDFDataModel_NEW.LCAGAdd_3_2_B,
            //       PDFDataModel_NEW.LCAGAdd_4_B,
            //       PDFDataModel_NEW.LCAGAdd_5_B,
            //       PDFDataModel_NEW.LCAGAdd_6_B,
            //       PDFDataModel_NEW.LCAGAdd_7_B,
            //       PDFDataModel_NEW.LCAGAdd_8_B,
            //       PDFDataModel_NEW.LCAGAdd_9_B
            //    );
            //var LSAGAdd_A =
            //     Utils.memergeAddFix(
            //       PDFDataModel_NEW.LSAGAdd_1_A,
            //       PDFDataModel_NEW.LSAGAdd_1_1_A,
            //       PDFDataModel_NEW.LSAGAdd_1_2_A,
            //       PDFDataModel_NEW.LSAGAdd_2_A,
            //       PDFDataModel_NEW.LSAGAdd_2_1_A,
            //       PDFDataModel_NEW.LSAGAdd_2_2_A,
            //       PDFDataModel_NEW.LSAGAdd_2_3_A,
            //       PDFDataModel_NEW.LSAGAdd_2_4_A,
            //       PDFDataModel_NEW.LSAGAdd_3_A,
            //       PDFDataModel_NEW.LSAGAdd_3_1_A,
            //       PDFDataModel_NEW.LSAGAdd_3_2_A,
            //       PDFDataModel_NEW.LSAGAdd_4_A,
            //       PDFDataModel_NEW.LSAGAdd_5_A,
            //       PDFDataModel_NEW.LSAGAdd_6_A,
            //       PDFDataModel_NEW.LSAGAdd_7_A,
            //       PDFDataModel_NEW.LSAGAdd_8_A,
            //       PDFDataModel_NEW.LSAGAdd_9_A
            //    );
            //var LSAGAdd_B =
            //    Utils.memergeAddFix(
            //       PDFDataModel_NEW.LSAGAdd_1_B,
            //       PDFDataModel_NEW.LSAGAdd_1_1_B,
            //       PDFDataModel_NEW.LSAGAdd_1_2_B,
            //       PDFDataModel_NEW.LSAGAdd_2_B,
            //       PDFDataModel_NEW.LSAGAdd_2_1_B,
            //       PDFDataModel_NEW.LSAGAdd_2_2_B,
            //       PDFDataModel_NEW.LSAGAdd_2_3_B,
            //       PDFDataModel_NEW.LSAGAdd_2_4_B,
            //       PDFDataModel_NEW.LSAGAdd_3_B,
            //       PDFDataModel_NEW.LSAGAdd_3_1_B,
            //       PDFDataModel_NEW.LSAGAdd_3_2_B,
            //       PDFDataModel_NEW.LSAGAdd_4_B,
            //       PDFDataModel_NEW.LSAGAdd_5_B,
            //       PDFDataModel_NEW.LSAGAdd_6_B,
            //       PDFDataModel_NEW.LSAGAdd_7_B,
            //       PDFDataModel_NEW.LSAGAdd_8_B,
            //       PDFDataModel_NEW.LSAGAdd_9_B
            //    );
            ////若BAdd為空才合併BAdd_1...BAdd_9，否則會被蓋掉，參考C01030101
            //if (string.IsNullOrEmpty(PDFDataModel_NEW.BAdd)) {
            //    PDFDataModel_NEW.BAdd =
            //        Utils.memergeAddFix(
            //           PDFDataModel_NEW.BAdd_1,
            //           PDFDataModel_NEW.BAdd_1_1,
            //           PDFDataModel_NEW.BAdd_1_2,
            //           PDFDataModel_NEW.BAdd_2,
            //           PDFDataModel_NEW.BAdd_2_1,
            //           PDFDataModel_NEW.BAdd_2_2,
            //           PDFDataModel_NEW.BAdd_2_3,
            //           PDFDataModel_NEW.BAdd_2_4,
            //           PDFDataModel_NEW.BAdd_3,
            //           PDFDataModel_NEW.BAdd_3_1,
            //           PDFDataModel_NEW.BAdd_3_2,
            //           PDFDataModel_NEW.BAdd_4,
            //           PDFDataModel_NEW.BAdd_5,
            //           PDFDataModel_NEW.BAdd_6,
            //           PDFDataModel_NEW.BAdd_7,
            //           PDFDataModel_NEW.BAdd_8,
            //           PDFDataModel_NEW.BAdd_9
            //        );
            //}
            //PDFDataModel_NEW.RNAdd =
            //    Utils.memergeAddFix(
            //       PDFDataModel_NEW.RNAdd_1,
            //       PDFDataModel_NEW.RNAdd_1_1,
            //       PDFDataModel_NEW.RNAdd_1_2,
            //       PDFDataModel_NEW.RNAdd_2,
            //       PDFDataModel_NEW.RNAdd_2_1,
            //       PDFDataModel_NEW.RNAdd_2_2,
            //       PDFDataModel_NEW.RNAdd_2_3,
            //       PDFDataModel_NEW.RNAdd_2_4,
            //       PDFDataModel_NEW.RNAdd_3,
            //       PDFDataModel_NEW.RNAdd_3_1,
            //       PDFDataModel_NEW.RNAdd_3_2,
            //       PDFDataModel_NEW.RNAdd_4,
            //       PDFDataModel_NEW.RNAdd_5,
            //       PDFDataModel_NEW.RNAdd_6,
            //       PDFDataModel_NEW.RNAdd_7,
            //       PDFDataModel_NEW.RNAdd_8,
            //       PDFDataModel_NEW.RNAdd_9
            //    );
            //PDFDataModel_NEW.RNAddC =
            //    Utils.memergeAddFix(
            //       PDFDataModel_NEW.RNAddC_1,
            //       PDFDataModel_NEW.RNAddC_1_1,
            //       PDFDataModel_NEW.RNAddC_1_2,
            //       PDFDataModel_NEW.RNAddC_2,
            //       PDFDataModel_NEW.RNAddC_2_1,
            //       PDFDataModel_NEW.RNAddC_2_2,
            //       PDFDataModel_NEW.RNAddC_2_3,
            //       PDFDataModel_NEW.RNAddC_2_4,
            //       PDFDataModel_NEW.RNAddC_3,
            //       PDFDataModel_NEW.RNAddC_3_1,
            //       PDFDataModel_NEW.RNAddC_3_2,
            //       PDFDataModel_NEW.RNAddC_4,
            //       PDFDataModel_NEW.RNAddC_5,
            //       PDFDataModel_NEW.RNAddC_6,
            //       PDFDataModel_NEW.RNAddC_7,
            //       PDFDataModel_NEW.RNAddC_8,
            //       PDFDataModel_NEW.RNAddC_9
            //    );
            //PDFDataModel_NEW.LSAdd =
            //    Utils.memergeAddFix(
            //        PDFDataModel_NEW.LSAdd_1,
            //        PDFDataModel_NEW.LSAdd_1_1,
            //        PDFDataModel_NEW.LSAdd_1_2,
            //        PDFDataModel_NEW.LSAdd_2,
            //        PDFDataModel_NEW.LSAdd_2_1,
            //        PDFDataModel_NEW.LSAdd_2_2,
            //        PDFDataModel_NEW.LSAdd_2_3,
            //        PDFDataModel_NEW.LSAdd_2_4,
            //        PDFDataModel_NEW.LSAdd_3,
            //        PDFDataModel_NEW.LSAdd_3_1,
            //        PDFDataModel_NEW.LSAdd_3_2,
            //        PDFDataModel_NEW.LSAdd_4,
            //        PDFDataModel_NEW.LSAdd_5,
            //        PDFDataModel_NEW.LSAdd_6,
            //        PDFDataModel_NEW.LSAdd_7,
            //        PDFDataModel_NEW.LSAdd_8,
            //        PDFDataModel_NEW.LSAdd_9
            //    );
            #endregion PDF上所有地址都是唯讀且不允許修改 所以不需要額外處理地址了

            #region 日期 電話處理
            PDFDataModel_NEW.LNBirthday = $"{PDFDataModel_NEW.LNBirthday_Y}-{PDFDataModel_NEW.LNBirthday_M}-{PDFDataModel_NEW.LNBirthday_D}";
            if (PDFDataModel_NEW.LNBirthday == "--") {
                PDFDataModel_NEW.LNBirthday = "";
            }
            PDFDataModel_NEW.LNTel = $"{PDFDataModel_NEW.LNTel_1}-{PDFDataModel_NEW.LNTel_2}";
            PDFDataModel_NEW.LNTelN = $"{PDFDataModel_NEW.LNTelN_1}-{PDFDataModel_NEW.LNTelN_2}";
            var LSAGTel_A = $"{PDFDataModel_NEW.LSAGTel_1_A}-{PDFDataModel_NEW.LSAGTel_2_A}";
            var LSAGTel_B = $"{PDFDataModel_NEW.LSAGTel_1_B}-{PDFDataModel_NEW.LSAGTel_2_B}";
            //PDFDataModel_NEW.LNAGTel_A = $"{PDFDataModel_NEW.LNAGTel_1_A}-{PDFDataModel_NEW.LNAGTel_2_A}";
            //PDFDataModel_NEW.LNAGTel_B = $"{PDFDataModel_NEW.LNAGTel_1_B}-{PDFDataModel_NEW.LNAGTel_2_B}";
            //PDFDataModel_NEW.LCAGTel_A = $"{PDFDataModel_NEW.LCAGTel_1_A}-{PDFDataModel_NEW.LCAGTel_2_A}";
            //PDFDataModel_NEW.LCAGTel_B = $"{PDFDataModel_NEW.LCAGTel_1_B}-{PDFDataModel_NEW.LCAGTel_2_B}";

            PDFDataModel_NEW.BCDate = $"{PDFDataModel_NEW.BCDate_Y}-{PDFDataModel_NEW.BCDate_M}-{PDFDataModel_NEW.BCDate_D}";
            if (PDFDataModel_NEW.BCDate == "--") {
                PDFDataModel_NEW.BCDate = "";
            }


            //處理同住人生日(RNFBirthday，此欄位較特殊都是一格呈現的所以必須由系統上去指定生日(88-02-07)，但是在下載PDF後要呈現86/02/07)
            PDFDataModel_NEW.RNBirthday = PDFDataModel_NEW.RNBirthday != null ? PDFDataModel_NEW.RNBirthday.Replace("/", "-") : "";
            PDFDataModel_NEW.RNFBirthday_A = PDFDataModel_NEW.RNFBirthday_A != null ? PDFDataModel_NEW.RNFBirthday_A.Replace("/", "-") : "";
            PDFDataModel_NEW.RNFBirthday_B = PDFDataModel_NEW.RNFBirthday_B != null ? PDFDataModel_NEW.RNFBirthday_B.Replace("/", "-") : "";
            PDFDataModel_NEW.RNFBirthday_C = PDFDataModel_NEW.RNFBirthday_C != null ? PDFDataModel_NEW.RNFBirthday_C.Replace("/", "-") : "";
            PDFDataModel_NEW.RNFBirthday_D = PDFDataModel_NEW.RNFBirthday_D != null ? PDFDataModel_NEW.RNFBirthday_D.Replace("/", "-") : "";
            PDFDataModel_NEW.RNFBirthday_E = PDFDataModel_NEW.RNFBirthday_E != null ? PDFDataModel_NEW.RNFBirthday_E.Replace("/", "-") : "";
            PDFDataModel_NEW.RNFBirthday_F = PDFDataModel_NEW.RNFBirthday_F != null ? PDFDataModel_NEW.RNFBirthday_F.Replace("/", "-") : "";
            PDFDataModel_NEW.RNFBirthday_G = PDFDataModel_NEW.RNFBirthday_G != null ? PDFDataModel_NEW.RNFBirthday_G.Replace("/", "-") : "";

            //PDFDataModel_NEW.RNBirthday = $"{PDFDataModel_NEW.RNBirthday_Y}-{PDFDataModel_NEW.RNBirthday_M}-{PDFDataModel_NEW.RNBirthday_D}";
            //if (PDFDataModel_NEW.RNBirthday == "--") {
            //    PDFDataModel_NEW.RNBirthday = "";
            //}
            //PDFDataModel_NEW.RNFBirthday_A = $"{PDFDataModel_NEW.RNFBirthday_Y_A}-{PDFDataModel_NEW.RNFBirthday_M_A}-{PDFDataModel_NEW.RNFBirthday_D_A}";
            //if (PDFDataModel_NEW.RNFBirthday_A == "--") {
            //    PDFDataModel_NEW.RNFBirthday_A = "";
            //}
            //PDFDataModel_NEW.RNFBirthday_B = $"{PDFDataModel_NEW.RNFBirthday_Y_B}-{PDFDataModel_NEW.RNFBirthday_M_B}-{PDFDataModel_NEW.RNFBirthday_D_B}";
            //if (PDFDataModel_NEW.RNFBirthday_B == "--") {
            //    PDFDataModel_NEW.RNFBirthday_B = "";
            //}
            //PDFDataModel_NEW.RNFBirthday_C = $"{PDFDataModel_NEW.RNFBirthday_Y_C}-{PDFDataModel_NEW.RNFBirthday_M_C}-{PDFDataModel_NEW.RNFBirthday_D_C}";
            //if (PDFDataModel_NEW.RNFBirthday_C == "--") {
            //    PDFDataModel_NEW.RNFBirthday_C = "";
            //}
            PDFDataModel_NEW.RNTel = $"{PDFDataModel_NEW.RNTel_1}-{PDFDataModel_NEW.RNTel_2}";
            PDFDataModel_NEW.RNTelN = $"{PDFDataModel_NEW.LNTelN_1}-{PDFDataModel_NEW.RNTelN_2}";
            #endregion

            #region 身份證/戶號 處理

            PDFDataModel_NEW.RNMMAccount = $"{PDFDataModel_NEW.RNMMAccount_1}{PDFDataModel_NEW.RNMMAccount_2}{PDFDataModel_NEW.RNMMAccount_3}{PDFDataModel_NEW.RNMMAccount_4}{PDFDataModel_NEW.RNMMAccount_5}{PDFDataModel_NEW.RNMMAccount_6}{PDFDataModel_NEW.RNMMAccount_7}{PDFDataModel_NEW.RNMMAccount_8}";
            PDFDataModel_NEW.RNFID_1_A = string.IsNullOrEmpty(PDFDataModel_NEW.RNFID_1_A) ? $"{PDFDataModel_NEW.RNFID_1_A_1}{PDFDataModel_NEW.RNFID_1_A_2}{PDFDataModel_NEW.RNFID_1_A_3}{PDFDataModel_NEW.RNFID_1_A_4}{PDFDataModel_NEW.RNFID_1_A_5}{PDFDataModel_NEW.RNFID_1_A_6}{PDFDataModel_NEW.RNFID_1_A_7}{PDFDataModel_NEW.RNFID_1_A_8}{PDFDataModel_NEW.RNFID_1_A_9}{PDFDataModel_NEW.RNFID_1_A_10}" : PDFDataModel_NEW.RNFID_1_A;
            PDFDataModel_NEW.RNFID_1_B = string.IsNullOrEmpty(PDFDataModel_NEW.RNFID_1_B) ? $"{PDFDataModel_NEW.RNFID_1_B_1}{PDFDataModel_NEW.RNFID_1_B_2}{PDFDataModel_NEW.RNFID_1_B_3}{PDFDataModel_NEW.RNFID_1_B_4}{PDFDataModel_NEW.RNFID_1_B_5}{PDFDataModel_NEW.RNFID_1_B_6}{PDFDataModel_NEW.RNFID_1_B_7}{PDFDataModel_NEW.RNFID_1_B_8}{PDFDataModel_NEW.RNFID_1_B_9}{PDFDataModel_NEW.RNFID_1_B_10}" : PDFDataModel_NEW.RNFID_1_B;
            PDFDataModel_NEW.RNFID_1_C = string.IsNullOrEmpty(PDFDataModel_NEW.RNFID_1_C) ? $"{PDFDataModel_NEW.RNFID_1_C_1}{PDFDataModel_NEW.RNFID_1_C_2}{PDFDataModel_NEW.RNFID_1_C_3}{PDFDataModel_NEW.RNFID_1_C_4}{PDFDataModel_NEW.RNFID_1_C_5}{PDFDataModel_NEW.RNFID_1_C_6}{PDFDataModel_NEW.RNFID_1_C_7}{PDFDataModel_NEW.RNFID_1_C_8}{PDFDataModel_NEW.RNFID_1_C_9}{PDFDataModel_NEW.RNFID_1_C_10}" : PDFDataModel_NEW.RNFID_1_C;
            PDFDataModel_NEW.RNFID_1_D = string.IsNullOrEmpty(PDFDataModel_NEW.RNFID_1_D) ? $"{PDFDataModel_NEW.RNFID_1_D_1}{PDFDataModel_NEW.RNFID_1_D_2}{PDFDataModel_NEW.RNFID_1_D_3}{PDFDataModel_NEW.RNFID_1_D_4}{PDFDataModel_NEW.RNFID_1_D_5}{PDFDataModel_NEW.RNFID_1_D_6}{PDFDataModel_NEW.RNFID_1_D_7}{PDFDataModel_NEW.RNFID_1_D_8}{PDFDataModel_NEW.RNFID_1_D_9}{PDFDataModel_NEW.RNFID_1_D_10}" : PDFDataModel_NEW.RNFID_1_D;
            PDFDataModel_NEW.RNFID_1_E = string.IsNullOrEmpty(PDFDataModel_NEW.RNFID_1_E) ? $"{PDFDataModel_NEW.RNFID_1_E_1}{PDFDataModel_NEW.RNFID_1_E_2}{PDFDataModel_NEW.RNFID_1_E_3}{PDFDataModel_NEW.RNFID_1_E_4}{PDFDataModel_NEW.RNFID_1_E_5}{PDFDataModel_NEW.RNFID_1_E_6}{PDFDataModel_NEW.RNFID_1_E_7}{PDFDataModel_NEW.RNFID_1_E_8}{PDFDataModel_NEW.RNFID_1_E_9}{PDFDataModel_NEW.RNFID_1_E_10}" : PDFDataModel_NEW.RNFID_1_E;
            PDFDataModel_NEW.RNFID_1_F = string.IsNullOrEmpty(PDFDataModel_NEW.RNFID_1_F) ? $"{PDFDataModel_NEW.RNFID_1_F_1}{PDFDataModel_NEW.RNFID_1_F_2}{PDFDataModel_NEW.RNFID_1_F_3}{PDFDataModel_NEW.RNFID_1_F_4}{PDFDataModel_NEW.RNFID_1_F_5}{PDFDataModel_NEW.RNFID_1_F_6}{PDFDataModel_NEW.RNFID_1_F_7}{PDFDataModel_NEW.RNFID_1_F_8}{PDFDataModel_NEW.RNFID_1_F_9}{PDFDataModel_NEW.RNFID_1_F_10}" : PDFDataModel_NEW.RNFID_1_F;
            PDFDataModel_NEW.RNFID_1_G = string.IsNullOrEmpty(PDFDataModel_NEW.RNFID_1_G) ? $"{PDFDataModel_NEW.RNFID_1_G_1}{PDFDataModel_NEW.RNFID_1_G_2}{PDFDataModel_NEW.RNFID_1_G_3}{PDFDataModel_NEW.RNFID_1_G_4}{PDFDataModel_NEW.RNFID_1_G_5}{PDFDataModel_NEW.RNFID_1_G_6}{PDFDataModel_NEW.RNFID_1_G_7}{PDFDataModel_NEW.RNFID_1_G_8}{PDFDataModel_NEW.RNFID_1_G_9}{PDFDataModel_NEW.RNFID_1_G_10}" : PDFDataModel_NEW.RNFID_1_G;

            PDFDataModel_NEW.RCFID_1_A = string.IsNullOrEmpty(PDFDataModel_NEW.RCFID_1_A) ? $"{PDFDataModel_NEW.RCFID_1_A_1}{PDFDataModel_NEW.RCFID_1_A_2}{PDFDataModel_NEW.RCFID_1_A_3}{PDFDataModel_NEW.RCFID_1_A_4}{PDFDataModel_NEW.RCFID_1_A_5}{PDFDataModel_NEW.RCFID_1_A_6}{PDFDataModel_NEW.RCFID_1_A_7}{PDFDataModel_NEW.RCFID_1_A_8}{PDFDataModel_NEW.RCFID_1_A_9}{PDFDataModel_NEW.RCFID_1_A_10}" : PDFDataModel_NEW.RCFID_1_A;
            PDFDataModel_NEW.RCFID_1_B = string.IsNullOrEmpty(PDFDataModel_NEW.RCFID_1_B) ? $"{PDFDataModel_NEW.RCFID_1_B_1}{PDFDataModel_NEW.RCFID_1_B_2}{PDFDataModel_NEW.RCFID_1_B_3}{PDFDataModel_NEW.RCFID_1_B_4}{PDFDataModel_NEW.RCFID_1_B_5}{PDFDataModel_NEW.RCFID_1_B_6}{PDFDataModel_NEW.RCFID_1_B_7}{PDFDataModel_NEW.RCFID_1_B_8}{PDFDataModel_NEW.RCFID_1_B_9}{PDFDataModel_NEW.RCFID_1_B_10}" : PDFDataModel_NEW.RCFID_1_B;
            PDFDataModel_NEW.RCFID_1_C = string.IsNullOrEmpty(PDFDataModel_NEW.RCFID_1_C) ? $"{PDFDataModel_NEW.RCFID_1_C_1}{PDFDataModel_NEW.RCFID_1_C_2}{PDFDataModel_NEW.RCFID_1_C_3}{PDFDataModel_NEW.RCFID_1_C_4}{PDFDataModel_NEW.RCFID_1_C_5}{PDFDataModel_NEW.RCFID_1_C_6}{PDFDataModel_NEW.RCFID_1_C_7}{PDFDataModel_NEW.RCFID_1_C_8}{PDFDataModel_NEW.RCFID_1_C_9}{PDFDataModel_NEW.RCFID_1_C_10}" : PDFDataModel_NEW.RCFID_1_C;
            PDFDataModel_NEW.RCFID_1_D = string.IsNullOrEmpty(PDFDataModel_NEW.RCFID_1_D) ? $"{PDFDataModel_NEW.RCFID_1_D_1}{PDFDataModel_NEW.RCFID_1_D_2}{PDFDataModel_NEW.RCFID_1_D_3}{PDFDataModel_NEW.RCFID_1_D_4}{PDFDataModel_NEW.RCFID_1_D_5}{PDFDataModel_NEW.RCFID_1_D_6}{PDFDataModel_NEW.RCFID_1_D_7}{PDFDataModel_NEW.RCFID_1_D_8}{PDFDataModel_NEW.RCFID_1_D_9}{PDFDataModel_NEW.RCFID_1_D_10}" : PDFDataModel_NEW.RCFID_1_D;
            PDFDataModel_NEW.RCFID_1_E = string.IsNullOrEmpty(PDFDataModel_NEW.RCFID_1_E) ? $"{PDFDataModel_NEW.RCFID_1_E_1}{PDFDataModel_NEW.RCFID_1_E_2}{PDFDataModel_NEW.RCFID_1_E_3}{PDFDataModel_NEW.RCFID_1_E_4}{PDFDataModel_NEW.RCFID_1_E_5}{PDFDataModel_NEW.RCFID_1_E_6}{PDFDataModel_NEW.RCFID_1_E_7}{PDFDataModel_NEW.RCFID_1_E_8}{PDFDataModel_NEW.RCFID_1_E_9}{PDFDataModel_NEW.RCFID_1_E_10}" : PDFDataModel_NEW.RCFID_1_E;
            PDFDataModel_NEW.RCFID_1_F = string.IsNullOrEmpty(PDFDataModel_NEW.RCFID_1_F) ? $"{PDFDataModel_NEW.RCFID_1_F_1}{PDFDataModel_NEW.RCFID_1_F_2}{PDFDataModel_NEW.RCFID_1_F_3}{PDFDataModel_NEW.RCFID_1_F_4}{PDFDataModel_NEW.RCFID_1_F_5}{PDFDataModel_NEW.RCFID_1_F_6}{PDFDataModel_NEW.RCFID_1_F_7}{PDFDataModel_NEW.RCFID_1_F_8}{PDFDataModel_NEW.RCFID_1_F_9}{PDFDataModel_NEW.RCFID_1_F_10}" : PDFDataModel_NEW.RCFID_1_F;
            PDFDataModel_NEW.RCFID_1_G = string.IsNullOrEmpty(PDFDataModel_NEW.RCFID_1_G) ? $"{PDFDataModel_NEW.RCFID_1_G_1}{PDFDataModel_NEW.RCFID_1_G_2}{PDFDataModel_NEW.RCFID_1_G_3}{PDFDataModel_NEW.RCFID_1_G_4}{PDFDataModel_NEW.RCFID_1_G_5}{PDFDataModel_NEW.RCFID_1_G_6}{PDFDataModel_NEW.RCFID_1_G_7}{PDFDataModel_NEW.RCFID_1_G_8}{PDFDataModel_NEW.RCFID_1_G_9}{PDFDataModel_NEW.RCFID_1_G_10}" : PDFDataModel_NEW.RCFID_1_G;

            #endregion


            string newId = GuidUtils.CreateNo();
            string CreatorUserId = CurrentUser?.UserId;
            string creatorUserOrgId = CurrentUser.OrganizeId;
            string creatorUserDeptId = CurrentUser.DeptId;
            DateTime CreatorTime = DateTime.Now;
            string LastModifyUserId = CurrentUser?.UserId;
            DateTime LastModifyTime = CreatorTime;

            bool isCustomerLN = false;
            bool isCustomerLC = false;
            bool isCustomerRN = false;
            bool isCustomerRC = false;
            //bool isLS = false;

            if (!string.IsNullOrEmpty(PDFDataModel_NEW.LNID)) {
                isCustomerLN = true;
            }

            if (!string.IsNullOrEmpty(PDFDataModel_NEW.LCID)) {
                isCustomerLC = true;
            }

            if (!string.IsNullOrEmpty(PDFDataModel_NEW.RNID)) {
                isCustomerRN = true;
            }

            if (!string.IsNullOrEmpty(PDFDataModel_NEW.RCID)) {
                isCustomerRC = true;
            }

            if (!string.IsNullOrEmpty(PDFDataModel_NEW.LSID)) {
                if (PDFDataModel_NEW.LSID.Length == 8) {
                    isCustomerLC = true;
                }
                if (PDFDataModel_NEW.LSID.Length == 10) {
                    isCustomerLN = true;
                }
            }

            if (!string.IsNullOrEmpty(PDFDataModel_NEW.RSID)) {
                if (PDFDataModel_NEW.RSID.Length == 8) {
                    isCustomerRC = true;
                }
                if (PDFDataModel_NEW.RSID.Length == 10) {
                    isCustomerRN = true;
                }
            }
            //更新基本資料
            #region CustomerLN
            if (isCustomerLN) {
                CustomerLN CustomerLN = PDFDataModel_NEW.MapTo<CustomerLN>();
                {
                    #region LS欄位處理
                    //姓名
                    CustomerLN.LNName = PDFDataModel_NEW.LSName == null ? CustomerLN.LNName : PDFDataModel_NEW.LSName;
                    //身分證字號
                    CustomerLN.LNID = PDFDataModel_NEW.LSID == null || PDFDataModel_NEW.LSID == "" ? CustomerLN.LNID : PDFDataModel_NEW.LSID;
                    //信箱
                    CustomerLN.LNMail = PDFDataModel_NEW.LSMail == null ? CustomerLN.LNMail : PDFDataModel_NEW.LSMail;
                    //性別
                    CustomerLN.LNGender1 = PDFDataModel_NEW.LSGender1 == null ? CustomerLN.LNGender1 : PDFDataModel_NEW.LSGender1;
                    CustomerLN.LNGender2 = PDFDataModel_NEW.LSGender2 == null ? CustomerLN.LNGender2 : PDFDataModel_NEW.LSGender2;
                    //生日
                    CustomerLN.LNBirthday_D = PDFDataModel_NEW.LSBirthday_D == null ? CustomerLN.LNBirthday_D : PDFDataModel_NEW.LSBirthday_D;
                    CustomerLN.LNBirthday_M = PDFDataModel_NEW.LSBirthday_M == null ? CustomerLN.LNBirthday_M : PDFDataModel_NEW.LSBirthday_M;
                    CustomerLN.LNBirthday_Y = PDFDataModel_NEW.LSBirthday_Y == null ? CustomerLN.LNBirthday_Y : PDFDataModel_NEW.LSBirthday_Y;

                    //住址 2022/12/06 PDF上所有地址都是唯讀 不允許修改 所以不需要額外處理地址了
                    //CustomerLN.LNAdd = PDFDataModel_NEW.LSAdd == null ? CustomerLN.LNAdd : PDFDataModel_NEW.LSAdd;
                    //CustomerLN.LNAdd_1 = PDFDataModel_NEW.LSAdd_1 == null ? CustomerLN.LNAdd_1 : PDFDataModel_NEW.LSAdd_1;
                    //CustomerLN.LNAdd_1_1 = PDFDataModel_NEW.LSAdd_1_1 == null ? CustomerLN.LNAdd_1_1 : PDFDataModel_NEW.LSAdd_1_1;
                    //CustomerLN.LNAdd_1_2 = PDFDataModel_NEW.LSAdd_1_2 == null ? CustomerLN.LNAdd_1_2 : PDFDataModel_NEW.LSAdd_1_2;
                    //CustomerLN.LNAdd_2 = PDFDataModel_NEW.LSAdd_2 == null ? CustomerLN.LNAdd_2 : PDFDataModel_NEW.LSAdd_2;
                    //CustomerLN.LNAdd_2_1 = PDFDataModel_NEW.LSAdd_2_1 == null ? CustomerLN.LNAdd_2_1 : PDFDataModel_NEW.LSAdd_2_1;
                    //CustomerLN.LNAdd_2_2 = PDFDataModel_NEW.LSAdd_2_2 == null ? CustomerLN.LNAdd_2_2 : PDFDataModel_NEW.LSAdd_2_2;
                    //CustomerLN.LNAdd_2_3 = PDFDataModel_NEW.LSAdd_2_3 == null ? CustomerLN.LNAdd_2_3 : PDFDataModel_NEW.LSAdd_2_3;
                    //CustomerLN.LNAdd_2_4 = PDFDataModel_NEW.LSAdd_2_4 == null ? CustomerLN.LNAdd_2_4 : PDFDataModel_NEW.LSAdd_2_4;
                    //CustomerLN.LNAdd_3 = PDFDataModel_NEW.LSAdd_3 == null ? CustomerLN.LNAdd_3 : PDFDataModel_NEW.LSAdd_3;
                    //CustomerLN.LNAdd_3_1 = PDFDataModel_NEW.LSAdd_3_1 == null ? CustomerLN.LNAdd_3_1 : PDFDataModel_NEW.LSAdd_3_1;
                    //CustomerLN.LNAdd_3_2 = PDFDataModel_NEW.LSAdd_3_2 == null ? CustomerLN.LNAdd_3_2 : PDFDataModel_NEW.LSAdd_3_2;
                    //CustomerLN.LNAdd_4 = PDFDataModel_NEW.LSAdd_4 == null ? CustomerLN.LNAdd_4 : PDFDataModel_NEW.LSAdd_4;
                    //CustomerLN.LNAdd_5 = PDFDataModel_NEW.LSAdd_5 == null ? CustomerLN.LNAdd_5 : PDFDataModel_NEW.LSAdd_5;
                    //CustomerLN.LNAdd_6 = PDFDataModel_NEW.LSAdd_6 == null ? CustomerLN.LNAdd_6 : PDFDataModel_NEW.LSAdd_6;
                    //CustomerLN.LNAdd_7 = PDFDataModel_NEW.LSAdd_7 == null ? CustomerLN.LNAdd_7 : PDFDataModel_NEW.LSAdd_7;
                    //CustomerLN.LNAdd_8 = PDFDataModel_NEW.LSAdd_8 == null ? CustomerLN.LNAdd_8 : PDFDataModel_NEW.LSAdd_8;
                    //CustomerLN.LNAdd_9 = PDFDataModel_NEW.LSAdd_9 == null ? CustomerLN.LNAdd_9 : PDFDataModel_NEW.LSAdd_9;
                    CustomerLN.LNAddSame = PDFDataModel_NEW.LSAddSame == null ? CustomerLN.LNAddSame : PDFDataModel_NEW.LSAddSame;
                    //電話
                    CustomerLN.LNTel_1 = PDFDataModel_NEW.LSTel_1 == null ? CustomerLN.LNTel_1 : PDFDataModel_NEW.LSTel_1;
                    CustomerLN.LNTel_2 = PDFDataModel_NEW.LSTel_2 == null ? CustomerLN.LNTel_2 : PDFDataModel_NEW.LSTel_2;
                    CustomerLN.LNTel = CustomerLN.LNTel_1 + "-" + CustomerLN.LNTel_2;
                    CustomerLN.LNTelN_1 = PDFDataModel_NEW.LSTelN_1 == null ? CustomerLN.LNTelN_1 : PDFDataModel_NEW.LSTelN_1;
                    CustomerLN.LNTelN_2 = PDFDataModel_NEW.LSTelN_2 == null ? CustomerLN.LNTelN_2 : PDFDataModel_NEW.LSTelN_2;
                    CustomerLN.LNTelN = CustomerLN.LNTelN_1 + "-" + CustomerLN.LNTelN_2;
                    //手機
                    CustomerLN.LNCell = PDFDataModel_NEW.LSCell == null ? CustomerLN.LNCell : PDFDataModel_NEW.LSCell;
                    //代理人1
                    CustomerLN.LNAGID_A = PDFDataModel_NEW.LSAGID_A == null ? CustomerLN.LNAGID_A : PDFDataModel_NEW.LSAGID_A;
                    CustomerLN.LNAGName_A = PDFDataModel_NEW.LSAGName_A == null ? CustomerLN.LNAGName_A : PDFDataModel_NEW.LSAGName_A;
                    //2022/12/06 PDF上所有地址都是唯讀 不允許修改 所以不需要額外處理地址了
                    //CustomerLN.LNAGAdd_A = LSAGAdd_A == null ? CustomerLN.LNAGAdd_A : LSAGAdd_A;
                    //CustomerLN.LNAGAdd_1_A = PDFDataModel_NEW.LSAGAdd_1_A == null ? CustomerLN.LNAGAdd_1_A : PDFDataModel_NEW.LSAGAdd_1_A;
                    //CustomerLN.LNAGAdd_1_1_A = PDFDataModel_NEW.LSAGAdd_1_1_A == null ? CustomerLN.LNAGAdd_1_1_A : PDFDataModel_NEW.LSAGAdd_1_1_A;
                    //CustomerLN.LNAGAdd_1_2_A = PDFDataModel_NEW.LSAGAdd_1_2_A == null ? CustomerLN.LNAGAdd_1_2_A : PDFDataModel_NEW.LSAGAdd_1_2_A;
                    //CustomerLN.LNAGAdd_2_A = PDFDataModel_NEW.LSAGAdd_2_A == null ? CustomerLN.LNAGAdd_2_A : PDFDataModel_NEW.LSAGAdd_2_A;
                    //CustomerLN.LNAGAdd_2_1_A = PDFDataModel_NEW.LSAGAdd_2_1_A == null ? CustomerLN.LNAGAdd_2_1_A : PDFDataModel_NEW.LSAGAdd_2_1_A;
                    //CustomerLN.LNAGAdd_2_2_A = PDFDataModel_NEW.LSAGAdd_2_2_A == null ? CustomerLN.LNAGAdd_2_2_A : PDFDataModel_NEW.LSAGAdd_2_2_A;
                    //CustomerLN.LNAGAdd_2_3_A = PDFDataModel_NEW.LSAGAdd_2_3_A == null ? CustomerLN.LNAGAdd_2_3_A : PDFDataModel_NEW.LSAGAdd_2_3_A;
                    //CustomerLN.LNAGAdd_2_4_A = PDFDataModel_NEW.LSAGAdd_2_4_A == null ? CustomerLN.LNAGAdd_2_4_A : PDFDataModel_NEW.LSAGAdd_2_4_A;
                    //CustomerLN.LNAGAdd_3_A = PDFDataModel_NEW.LSAGAdd_3_A == null ? CustomerLN.LNAGAdd_3_A : PDFDataModel_NEW.LSAGAdd_3_A;
                    //CustomerLN.LNAGAdd_3_1_A = PDFDataModel_NEW.LSAGAdd_3_1_A == null ? CustomerLN.LNAGAdd_3_1_A : PDFDataModel_NEW.LSAGAdd_3_1_A;
                    //CustomerLN.LNAGAdd_3_2_A = PDFDataModel_NEW.LSAGAdd_3_2_A == null ? CustomerLN.LNAGAdd_3_2_A : PDFDataModel_NEW.LSAGAdd_3_2_A;
                    //CustomerLN.LNAGAdd_4_A = PDFDataModel_NEW.LSAGAdd_4_A == null ? CustomerLN.LNAGAdd_4_A : PDFDataModel_NEW.LSAGAdd_4_A;
                    //CustomerLN.LNAGAdd_5_A = PDFDataModel_NEW.LSAGAdd_5_A == null ? CustomerLN.LNAGAdd_5_A : PDFDataModel_NEW.LSAGAdd_5_A;
                    //CustomerLN.LNAGAdd_6_A = PDFDataModel_NEW.LSAGAdd_6_A == null ? CustomerLN.LNAGAdd_6_A : PDFDataModel_NEW.LSAGAdd_6_A;
                    //CustomerLN.LNAGAdd_7_A = PDFDataModel_NEW.LSAGAdd_7_A == null ? CustomerLN.LNAGAdd_7_A : PDFDataModel_NEW.LSAGAdd_7_A;
                    //CustomerLN.LNAGAdd_8_A = PDFDataModel_NEW.LSAGAdd_8_A == null ? CustomerLN.LNAGAdd_8_A : PDFDataModel_NEW.LSAGAdd_8_A;
                    //CustomerLN.LNAGAdd_9_A = PDFDataModel_NEW.LSAGAdd_9_A == null ? CustomerLN.LNAGAdd_9_A : PDFDataModel_NEW.LSAGAdd_9_A;
                    CustomerLN.LNAGCell_A = PDFDataModel_NEW.LSAGCell_A == null ? CustomerLN.LNAGCell_A : PDFDataModel_NEW.LSAGCell_A;
                    CustomerLN.LNAGTel_A = LSAGTel_A == null ? CustomerLN.LNAGTel_A : LSAGTel_A;
                    CustomerLN.LNAGTel_1_A = PDFDataModel_NEW.LSAGTel_1_A == null ? CustomerLN.LNAGTel_1_A : PDFDataModel_NEW.LSAGTel_1_A;
                    CustomerLN.LNAGTel_2_A = PDFDataModel_NEW.LSAGTel_2_A == null ? CustomerLN.LNAGTel_2_A : PDFDataModel_NEW.LSAGTel_2_A;
                    //代理人2
                    CustomerLN.LNAGID_B = PDFDataModel_NEW.LSAGID_B == null ? CustomerLN.LNAGID_B : PDFDataModel_NEW.LSAGID_B;
                    CustomerLN.LNAGName_B = PDFDataModel_NEW.LSAGName_B == null ? CustomerLN.LNAGName_B : PDFDataModel_NEW.LSAGName_B;
                    //2022/12/06 PDF上所有地址都是唯讀 不允許修改 所以不需要額外處理地址了
                    //CustomerLN.LNAGAdd_B = LSAGAdd_B == null ? CustomerLN.LNAGAdd_B : LSAGAdd_B;
                    //CustomerLN.LNAGAdd_1_B = PDFDataModel_NEW.LSAGAdd_1_B == null ? CustomerLN.LNAGAdd_1_B : PDFDataModel_NEW.LSAGAdd_1_B;
                    //CustomerLN.LNAGAdd_1_1_B = PDFDataModel_NEW.LSAGAdd_1_1_B == null ? CustomerLN.LNAGAdd_1_1_B : PDFDataModel_NEW.LSAGAdd_1_1_B;
                    //CustomerLN.LNAGAdd_1_2_B = PDFDataModel_NEW.LSAGAdd_1_2_B == null ? CustomerLN.LNAGAdd_1_2_B : PDFDataModel_NEW.LSAGAdd_1_2_B;
                    //CustomerLN.LNAGAdd_2_B = PDFDataModel_NEW.LSAGAdd_2_B == null ? CustomerLN.LNAGAdd_2_B : PDFDataModel_NEW.LSAGAdd_2_B;
                    //CustomerLN.LNAGAdd_2_1_B = PDFDataModel_NEW.LSAGAdd_2_1_B == null ? CustomerLN.LNAGAdd_2_1_B : PDFDataModel_NEW.LSAGAdd_2_1_B;
                    //CustomerLN.LNAGAdd_2_2_B = PDFDataModel_NEW.LSAGAdd_2_2_B == null ? CustomerLN.LNAGAdd_2_2_B : PDFDataModel_NEW.LSAGAdd_2_2_B;
                    //CustomerLN.LNAGAdd_2_3_B = PDFDataModel_NEW.LSAGAdd_2_3_B == null ? CustomerLN.LNAGAdd_2_3_B : PDFDataModel_NEW.LSAGAdd_2_3_B;
                    //CustomerLN.LNAGAdd_2_4_B = PDFDataModel_NEW.LSAGAdd_2_4_B == null ? CustomerLN.LNAGAdd_2_4_B : PDFDataModel_NEW.LSAGAdd_2_4_B;
                    //CustomerLN.LNAGAdd_3_B = PDFDataModel_NEW.LSAGAdd_3_B == null ? CustomerLN.LNAGAdd_3_B : PDFDataModel_NEW.LSAGAdd_3_B;
                    //CustomerLN.LNAGAdd_3_1_B = PDFDataModel_NEW.LSAGAdd_3_1_B == null ? CustomerLN.LNAGAdd_3_1_B : PDFDataModel_NEW.LSAGAdd_3_1_B;
                    //CustomerLN.LNAGAdd_3_2_B = PDFDataModel_NEW.LSAGAdd_3_2_B == null ? CustomerLN.LNAGAdd_3_2_B : PDFDataModel_NEW.LSAGAdd_3_2_B;
                    //CustomerLN.LNAGAdd_4_B = PDFDataModel_NEW.LSAGAdd_4_B == null ? CustomerLN.LNAGAdd_4_B : PDFDataModel_NEW.LSAGAdd_4_B;
                    //CustomerLN.LNAGAdd_5_B = PDFDataModel_NEW.LSAGAdd_5_B == null ? CustomerLN.LNAGAdd_5_B : PDFDataModel_NEW.LSAGAdd_5_B;
                    //CustomerLN.LNAGAdd_6_B = PDFDataModel_NEW.LSAGAdd_6_B == null ? CustomerLN.LNAGAdd_6_B : PDFDataModel_NEW.LSAGAdd_6_B;
                    //CustomerLN.LNAGAdd_7_B = PDFDataModel_NEW.LSAGAdd_7_B == null ? CustomerLN.LNAGAdd_7_B : PDFDataModel_NEW.LSAGAdd_7_B;
                    //CustomerLN.LNAGAdd_8_B = PDFDataModel_NEW.LSAGAdd_8_B == null ? CustomerLN.LNAGAdd_8_B : PDFDataModel_NEW.LSAGAdd_8_B;
                    //CustomerLN.LNAGAdd_9_B = PDFDataModel_NEW.LSAGAdd_9_B == null ? CustomerLN.LNAGAdd_9_B : PDFDataModel_NEW.LSAGAdd_9_B;
                    CustomerLN.LNAGCell_B = PDFDataModel_NEW.LSAGCell_B == null ? CustomerLN.LNAGCell_B : PDFDataModel_NEW.LSAGCell_B;
                    CustomerLN.LNAGTel_B = LSAGTel_B == null ? CustomerLN.LNAGTel_B : LSAGTel_B;
                    CustomerLN.LNAGTel_1_B = PDFDataModel_NEW.LSAGTel_1_B == null ? CustomerLN.LNAGTel_1_B : PDFDataModel_NEW.LSAGTel_1_B;
                    CustomerLN.LNAGTel_2_B = PDFDataModel_NEW.LSAGTel_2_B == null ? CustomerLN.LNAGTel_2_B : PDFDataModel_NEW.LSAGTel_2_B;
                    #endregion LS欄位處理
                }

                //Console.WriteLine($"To CustomerLN:{CustomerLN.ToJson()}");

                if (CustomerLN.LNID == null) {
                    throw new Exception("(自然人)身分證字號不能為空");
                }
                CustomerLN CustomerLN_DB = await CustomerLNService.GetByCustomerLNID(CustomerLN.LNID);
                CustomerLN CustomerLN_NEW = Merger.CloneAndMerge<CustomerLN>(CustomerLN_DB, CustomerLN);
                CustomerLN_NEW.Id = CustomerLN_DB.Id;
                CustomerLN_NEW.LastModifyUserId = LastModifyUserId;
                CustomerLN_NEW.LastModifyTime = LastModifyTime;
                bool bl = CustomerLNService.Update(CustomerLN_NEW, CustomerLN_NEW.Id);

            }
            #endregion

            #region CustomerLC
            if (isCustomerLC) {
                CustomerLC CustomerLC = PDFDataModel_NEW.MapTo<CustomerLC>();
                {
                    #region LS欄位處理
                    //姓名
                    CustomerLC.LCName = PDFDataModel_NEW.LSName == null ? CustomerLC.LCName : PDFDataModel_NEW.LSName;
                    //身分證字號
                    CustomerLC.LCID = PDFDataModel_NEW.LSID == null || PDFDataModel_NEW.LSID == "" ? CustomerLC.LCID : PDFDataModel_NEW.LSID;
                    //信箱 (法人無信箱)
                    //CustomerLC.LCMail = PDFDataModel_NEW.LSMail == null ? CustomerLC.LCMail : PDFDataModel_NEW.LSMail;
                    //性別 (法人無性別)
                    //CustomerLC.LCGender1 = PDFDataModel_NEW.LSGender1 == null ? CustomerLC.LCGender1 : PDFDataModel_NEW.LSGender1;
                    //CustomerLC.LCGender2 = PDFDataModel_NEW.LSGender2 == null ? CustomerLC.LCGender2 : PDFDataModel_NEW.LSGender2;
                    //生日 (法人無生日)
                    //CustomerLC.LCBirthday_D = PDFDataModel_NEW.LSBirthday_D == null ? CustomerLC.LCBirthday_D : PDFDataModel_NEW.LSBirthday_D;
                    //CustomerLC.LCBirthday_M = PDFDataModel_NEW.LSBirthday_M == null ? CustomerLC.LCBirthday_M : PDFDataModel_NEW.LSBirthday_M;
                    //CustomerLC.LCBirthday_Y = PDFDataModel_NEW.LSBirthday_Y == null ? CustomerLC.LCBirthday_Y : PDFDataModel_NEW.LSBirthday_Y;
                    //住址 2022/12/06 PDF上所有地址都是唯讀 不允許修改 所以不需要額外處理地址了
                    //CustomerLC.LCAdd = PDFDataModel_NEW.LSAdd == null ? CustomerLC.LCAdd : PDFDataModel_NEW.LSAdd;
                    //CustomerLC.LCAdd_1 = PDFDataModel_NEW.LSAdd_1 == null ? CustomerLC.LCAdd_1 : PDFDataModel_NEW.LSAdd_1;
                    //CustomerLC.LCAdd_1_1 = PDFDataModel_NEW.LSAdd_1_1 == null ? CustomerLC.LCAdd_1_1 : PDFDataModel_NEW.LSAdd_1_1;
                    //CustomerLC.LCAdd_1_2 = PDFDataModel_NEW.LSAdd_1_2 == null ? CustomerLC.LCAdd_1_2 : PDFDataModel_NEW.LSAdd_1_2;
                    //CustomerLC.LCAdd_2 = PDFDataModel_NEW.LSAdd_2 == null ? CustomerLC.LCAdd_2 : PDFDataModel_NEW.LSAdd_2;
                    //CustomerLC.LCAdd_2_1 = PDFDataModel_NEW.LSAdd_2_1 == null ? CustomerLC.LCAdd_2_1 : PDFDataModel_NEW.LSAdd_2_1;
                    //CustomerLC.LCAdd_2_2 = PDFDataModel_NEW.LSAdd_2_2 == null ? CustomerLC.LCAdd_2_2 : PDFDataModel_NEW.LSAdd_2_2;
                    //CustomerLC.LCAdd_2_3 = PDFDataModel_NEW.LSAdd_2_3 == null ? CustomerLC.LCAdd_2_3 : PDFDataModel_NEW.LSAdd_2_3;
                    //CustomerLC.LCAdd_2_4 = PDFDataModel_NEW.LSAdd_2_4 == null ? CustomerLC.LCAdd_2_4 : PDFDataModel_NEW.LSAdd_2_4;
                    //CustomerLC.LCAdd_3 = PDFDataModel_NEW.LSAdd_3 == null ? CustomerLC.LCAdd_3 : PDFDataModel_NEW.LSAdd_3;
                    //CustomerLC.LCAdd_3_1 = PDFDataModel_NEW.LSAdd_3_1 == null ? CustomerLC.LCAdd_3_1 : PDFDataModel_NEW.LSAdd_3_1;
                    //CustomerLC.LCAdd_3_2 = PDFDataModel_NEW.LSAdd_3_2 == null ? CustomerLC.LCAdd_3_2 : PDFDataModel_NEW.LSAdd_3_2;
                    //CustomerLC.LCAdd_4 = PDFDataModel_NEW.LSAdd_4 == null ? CustomerLC.LCAdd_4 : PDFDataModel_NEW.LSAdd_4;
                    //CustomerLC.LCAdd_5 = PDFDataModel_NEW.LSAdd_5 == null ? CustomerLC.LCAdd_5 : PDFDataModel_NEW.LSAdd_5;
                    //CustomerLC.LCAdd_6 = PDFDataModel_NEW.LSAdd_6 == null ? CustomerLC.LCAdd_6 : PDFDataModel_NEW.LSAdd_6;
                    //CustomerLC.LCAdd_7 = PDFDataModel_NEW.LSAdd_7 == null ? CustomerLC.LCAdd_7 : PDFDataModel_NEW.LSAdd_7;
                    //CustomerLC.LCAdd_8 = PDFDataModel_NEW.LSAdd_8 == null ? CustomerLC.LCAdd_8 : PDFDataModel_NEW.LSAdd_8;
                    //CustomerLC.LCAdd_9 = PDFDataModel_NEW.LSAdd_9 == null ? CustomerLC.LCAdd_9 : PDFDataModel_NEW.LSAdd_9;
                    //CustomerLC.LCAddSame = PDFDataModel_NEW.LSAddSame == null ? CustomerLC.LCAddSame : PDFDataModel_NEW.LSAddSame;
                    //電話
                    CustomerLC.LCTel_1 = PDFDataModel_NEW.LSTel_1 == null ? CustomerLC.LCTel_1 : PDFDataModel_NEW.LSTel_1;
                    CustomerLC.LCTel_2 = PDFDataModel_NEW.LSTel_2 == null ? CustomerLC.LCTel_2 : PDFDataModel_NEW.LSTel_2;
                    CustomerLC.LCTel = CustomerLC.LCTel_1 + "-" + CustomerLC.LCTel_2;
                    //法人目前只有一組電話(日)
                    //CustomerLC.LCTelN_1 = PDFDataModel_NEW.LSTelN_1 == null ? CustomerLC.LCTelN_1 : PDFDataModel_NEW.LSTelN_1;
                    //CustomerLC.LCTelN_2 = PDFDataModel_NEW.LSTelN_2 == null ? CustomerLC.LCTelN_2 : PDFDataModel_NEW.LSTelN_2;
                    //CustomerLC.LCTelN = CustomerLC.LCTelN_1 + "-" + CustomerLC.LCTelN_2;
                    //手機 法人目前沒有手機欄位
                    //CustomerLC.LCCell = PDFDataModel_NEW.LSCell == null ? CustomerLC.LCCell : PDFDataModel_NEW.LSCell;
                    //代理人1
                    CustomerLC.LCAGID_A = PDFDataModel_NEW.LSAGID_A == null ? CustomerLC.LCAGID_A : PDFDataModel_NEW.LSAGID_A;
                    CustomerLC.LCAGName_A = PDFDataModel_NEW.LSAGName_A == null ? CustomerLC.LCAGName_A : PDFDataModel_NEW.LSAGName_A;
                    //2022/12/06 PDF上所有地址都是唯讀 不允許修改 所以不需要額外處理地址了
                    //CustomerLC.LCAGAdd_A = LSAGAdd_A == null ? CustomerLC.LCAGAdd_A : LSAGAdd_A;
                    //CustomerLC.LCAGAdd_1_A = PDFDataModel_NEW.LSAGAdd_1_A == null ? CustomerLC.LCAGAdd_1_A : PDFDataModel_NEW.LSAGAdd_1_A;
                    //CustomerLC.LCAGAdd_1_1_A = PDFDataModel_NEW.LSAGAdd_1_1_A == null ? CustomerLC.LCAGAdd_1_1_A : PDFDataModel_NEW.LSAGAdd_1_1_A;
                    //CustomerLC.LCAGAdd_1_2_A = PDFDataModel_NEW.LSAGAdd_1_2_A == null ? CustomerLC.LCAGAdd_1_2_A : PDFDataModel_NEW.LSAGAdd_1_2_A;
                    //CustomerLC.LCAGAdd_2_A = PDFDataModel_NEW.LSAGAdd_2_A == null ? CustomerLC.LCAGAdd_2_A : PDFDataModel_NEW.LSAGAdd_2_A;
                    //CustomerLC.LCAGAdd_2_1_A = PDFDataModel_NEW.LSAGAdd_2_1_A == null ? CustomerLC.LCAGAdd_2_1_A : PDFDataModel_NEW.LSAGAdd_2_1_A;
                    //CustomerLC.LCAGAdd_2_2_A = PDFDataModel_NEW.LSAGAdd_2_2_A == null ? CustomerLC.LCAGAdd_2_2_A : PDFDataModel_NEW.LSAGAdd_2_2_A;
                    //CustomerLC.LCAGAdd_2_3_A = PDFDataModel_NEW.LSAGAdd_2_3_A == null ? CustomerLC.LCAGAdd_2_3_A : PDFDataModel_NEW.LSAGAdd_2_3_A;
                    //CustomerLC.LCAGAdd_2_4_A = PDFDataModel_NEW.LSAGAdd_2_4_A == null ? CustomerLC.LCAGAdd_2_4_A : PDFDataModel_NEW.LSAGAdd_2_4_A;
                    //CustomerLC.LCAGAdd_3_A = PDFDataModel_NEW.LSAGAdd_3_A == null ? CustomerLC.LCAGAdd_3_A : PDFDataModel_NEW.LSAGAdd_3_A;
                    //CustomerLC.LCAGAdd_3_1_A = PDFDataModel_NEW.LSAGAdd_3_1_A == null ? CustomerLC.LCAGAdd_3_1_A : PDFDataModel_NEW.LSAGAdd_3_1_A;
                    //CustomerLC.LCAGAdd_3_2_A = PDFDataModel_NEW.LSAGAdd_3_2_A == null ? CustomerLC.LCAGAdd_3_2_A : PDFDataModel_NEW.LSAGAdd_3_2_A;
                    //CustomerLC.LCAGAdd_4_A = PDFDataModel_NEW.LSAGAdd_4_A == null ? CustomerLC.LCAGAdd_4_A : PDFDataModel_NEW.LSAGAdd_4_A;
                    //CustomerLC.LCAGAdd_5_A = PDFDataModel_NEW.LSAGAdd_5_A == null ? CustomerLC.LCAGAdd_5_A : PDFDataModel_NEW.LSAGAdd_5_A;
                    //CustomerLC.LCAGAdd_6_A = PDFDataModel_NEW.LSAGAdd_6_A == null ? CustomerLC.LCAGAdd_6_A : PDFDataModel_NEW.LSAGAdd_6_A;
                    //CustomerLC.LCAGAdd_7_A = PDFDataModel_NEW.LSAGAdd_7_A == null ? CustomerLC.LCAGAdd_7_A : PDFDataModel_NEW.LSAGAdd_7_A;
                    //CustomerLC.LCAGAdd_8_A = PDFDataModel_NEW.LSAGAdd_8_A == null ? CustomerLC.LCAGAdd_8_A : PDFDataModel_NEW.LSAGAdd_8_A;
                    //CustomerLC.LCAGAdd_9_A = PDFDataModel_NEW.LSAGAdd_9_A == null ? CustomerLC.LCAGAdd_9_A : PDFDataModel_NEW.LSAGAdd_9_A;
                    CustomerLC.LCAGCell_A = PDFDataModel_NEW.LSAGCell_A == null ? CustomerLC.LCAGCell_A : PDFDataModel_NEW.LSAGCell_A;
                    CustomerLC.LCAGTel_A = LSAGTel_A == null ? CustomerLC.LCAGTel_A : LSAGTel_A;
                    CustomerLC.LCAGTel_1_A = PDFDataModel_NEW.LSAGTel_1_A == null ? CustomerLC.LCAGTel_1_A : PDFDataModel_NEW.LSAGTel_1_A;
                    CustomerLC.LCAGTel_2_A = PDFDataModel_NEW.LSAGTel_2_A == null ? CustomerLC.LCAGTel_2_A : PDFDataModel_NEW.LSAGTel_2_A;
                    //代理人2
                    CustomerLC.LCAGID_B = PDFDataModel_NEW.LSAGID_B == null ? CustomerLC.LCAGID_B : PDFDataModel_NEW.LSAGID_B;
                    CustomerLC.LCAGName_B = PDFDataModel_NEW.LSAGName_B == null ? CustomerLC.LCAGName_B : PDFDataModel_NEW.LSAGName_B;
                    //2022/12/06 PDF上所有地址都是唯讀 不允許修改 所以不需要額外處理地址了
                    //CustomerLC.LCAGAdd_B = LSAGAdd_B == null ? CustomerLC.LCAGAdd_B : LSAGAdd_B;
                    //CustomerLC.LCAGAdd_1_B = PDFDataModel_NEW.LSAGAdd_1_B == null ? CustomerLC.LCAGAdd_1_B : PDFDataModel_NEW.LSAGAdd_1_B;
                    //CustomerLC.LCAGAdd_1_1_B = PDFDataModel_NEW.LSAGAdd_1_1_B == null ? CustomerLC.LCAGAdd_1_1_B : PDFDataModel_NEW.LSAGAdd_1_1_B;
                    //CustomerLC.LCAGAdd_1_2_B = PDFDataModel_NEW.LSAGAdd_1_2_B == null ? CustomerLC.LCAGAdd_1_2_B : PDFDataModel_NEW.LSAGAdd_1_2_B;
                    //CustomerLC.LCAGAdd_2_B = PDFDataModel_NEW.LSAGAdd_2_B == null ? CustomerLC.LCAGAdd_2_B : PDFDataModel_NEW.LSAGAdd_2_B;
                    //CustomerLC.LCAGAdd_2_1_B = PDFDataModel_NEW.LSAGAdd_2_1_B == null ? CustomerLC.LCAGAdd_2_1_B : PDFDataModel_NEW.LSAGAdd_2_1_B;
                    //CustomerLC.LCAGAdd_2_2_B = PDFDataModel_NEW.LSAGAdd_2_2_B == null ? CustomerLC.LCAGAdd_2_2_B : PDFDataModel_NEW.LSAGAdd_2_2_B;
                    //CustomerLC.LCAGAdd_2_3_B = PDFDataModel_NEW.LSAGAdd_2_3_B == null ? CustomerLC.LCAGAdd_2_3_B : PDFDataModel_NEW.LSAGAdd_2_3_B;
                    //CustomerLC.LCAGAdd_2_4_B = PDFDataModel_NEW.LSAGAdd_2_4_B == null ? CustomerLC.LCAGAdd_2_4_B : PDFDataModel_NEW.LSAGAdd_2_4_B;
                    //CustomerLC.LCAGAdd_3_B = PDFDataModel_NEW.LSAGAdd_3_B == null ? CustomerLC.LCAGAdd_3_B : PDFDataModel_NEW.LSAGAdd_3_B;
                    //CustomerLC.LCAGAdd_3_1_B = PDFDataModel_NEW.LSAGAdd_3_1_B == null ? CustomerLC.LCAGAdd_3_1_B : PDFDataModel_NEW.LSAGAdd_3_1_B;
                    //CustomerLC.LCAGAdd_3_2_B = PDFDataModel_NEW.LSAGAdd_3_2_B == null ? CustomerLC.LCAGAdd_3_2_B : PDFDataModel_NEW.LSAGAdd_3_2_B;
                    //CustomerLC.LCAGAdd_4_B = PDFDataModel_NEW.LSAGAdd_4_B == null ? CustomerLC.LCAGAdd_4_B : PDFDataModel_NEW.LSAGAdd_4_B;
                    //CustomerLC.LCAGAdd_5_B = PDFDataModel_NEW.LSAGAdd_5_B == null ? CustomerLC.LCAGAdd_5_B : PDFDataModel_NEW.LSAGAdd_5_B;
                    //CustomerLC.LCAGAdd_6_B = PDFDataModel_NEW.LSAGAdd_6_B == null ? CustomerLC.LCAGAdd_6_B : PDFDataModel_NEW.LSAGAdd_6_B;
                    //CustomerLC.LCAGAdd_7_B = PDFDataModel_NEW.LSAGAdd_7_B == null ? CustomerLC.LCAGAdd_7_B : PDFDataModel_NEW.LSAGAdd_7_B;
                    //CustomerLC.LCAGAdd_8_B = PDFDataModel_NEW.LSAGAdd_8_B == null ? CustomerLC.LCAGAdd_8_B : PDFDataModel_NEW.LSAGAdd_8_B;
                    //CustomerLC.LCAGAdd_9_B = PDFDataModel_NEW.LSAGAdd_9_B == null ? CustomerLC.LCAGAdd_9_B : PDFDataModel_NEW.LSAGAdd_9_B;
                    CustomerLC.LCAGCell_B = PDFDataModel_NEW.LSAGCell_B == null ? CustomerLC.LCAGCell_B : PDFDataModel_NEW.LSAGCell_B;
                    CustomerLC.LCAGTel_B = LSAGTel_B == null ? CustomerLC.LCAGTel_B : LSAGTel_B;
                    CustomerLC.LCAGTel_1_B = PDFDataModel_NEW.LSAGTel_1_B == null ? CustomerLC.LCAGTel_1_B : PDFDataModel_NEW.LSAGTel_1_B;
                    CustomerLC.LCAGTel_2_B = PDFDataModel_NEW.LSAGTel_2_B == null ? CustomerLC.LCAGTel_2_B : PDFDataModel_NEW.LSAGTel_2_B;
                    #endregion LS欄位處理
                }
                //Console.WriteLine($"To CustomerLC:{CustomerLC.ToJson()}");

                if (CustomerLC.LCID == null) {
                    throw new Exception("(法人)統一編號不能為空");
                }
                CustomerLC CustomerLC_DB = await CustomerLCService.GetByLCID(CustomerLC.LCID);
                CustomerLC CustomerLC_NEW = Merger.CloneAndMerge<CustomerLC>(CustomerLC_DB, CustomerLC);
                CustomerLC_NEW.Id = CustomerLC_DB.Id;
                CustomerLC_NEW.LastModifyUserId = LastModifyUserId;
                CustomerLC_NEW.LastModifyTime = LastModifyTime;
                bool bl = CustomerLCService.Update(CustomerLC_NEW, CustomerLC_NEW.Id);

            }
            #endregion

            #region CustomerRN
            //房客-自然人
            if (isCustomerRN) {
                CustomerRN CustomerRN = PDFDataModel_NEW.MapTo<CustomerRN>();
                {
                    #region RS欄位處理

                    //姓名
                    CustomerRN.RNName = PDFDataModel_NEW.RSName == null ? CustomerRN.RNName : PDFDataModel_NEW.RSName;
                    //身分證字號
                    CustomerRN.RNID = PDFDataModel_NEW.RSID == null || PDFDataModel_NEW.RSID == "" ? CustomerRN.RNID : PDFDataModel_NEW.RSID;
                    //住址 2022/12/06 PDF上所有地址都是唯讀 不允許修改 所以不需要額外處理地址了
                    //CustomerRN.RNAdd = PDFDataModel_NEW.RSAdd == null ? CustomerRN.RNAdd : PDFDataModel_NEW.RSAdd;
                    //CustomerRN.RNAdd_1 = PDFDataModel_NEW.RSAdd_1 == null ? CustomerRN.RNAdd_1 : PDFDataModel_NEW.RSAdd_1;
                    //CustomerRN.RNAdd_1_1 = PDFDataModel_NEW.RSAdd_1_1 == null ? CustomerRN.RNAdd_1_1 : PDFDataModel_NEW.RSAdd_1_1;
                    //CustomerRN.RNAdd_1_2 = PDFDataModel_NEW.RSAdd_1_2 == null ? CustomerRN.RNAdd_1_2 : PDFDataModel_NEW.RSAdd_1_2;
                    //CustomerRN.RNAdd_2 = PDFDataModel_NEW.RSAdd_2 == null ? CustomerRN.RNAdd_2 : PDFDataModel_NEW.RSAdd_2;
                    //CustomerRN.RNAdd_2_1 = PDFDataModel_NEW.RSAdd_2_1 == null ? CustomerRN.RNAdd_2_1 : PDFDataModel_NEW.RSAdd_2_1;
                    //CustomerRN.RNAdd_2_2 = PDFDataModel_NEW.RSAdd_2_2 == null ? CustomerRN.RNAdd_2_2 : PDFDataModel_NEW.RSAdd_2_2;
                    //CustomerRN.RNAdd_2_3 = PDFDataModel_NEW.RSAdd_2_3 == null ? CustomerRN.RNAdd_2_3 : PDFDataModel_NEW.RSAdd_2_3;
                    //CustomerRN.RNAdd_2_4 = PDFDataModel_NEW.RSAdd_2_4 == null ? CustomerRN.RNAdd_2_4 : PDFDataModel_NEW.RSAdd_2_4;
                    //CustomerRN.RNAdd_3 = PDFDataModel_NEW.RSAdd_3 == null ? CustomerRN.RNAdd_3 : PDFDataModel_NEW.RSAdd_3;
                    //CustomerRN.RNAdd_3_1 = PDFDataModel_NEW.RSAdd_3_1 == null ? CustomerRN.RNAdd_3_1 : PDFDataModel_NEW.RSAdd_3_1;
                    //CustomerRN.RNAdd_3_2 = PDFDataModel_NEW.RSAdd_3_2 == null ? CustomerRN.RNAdd_3_2 : PDFDataModel_NEW.RSAdd_3_2;
                    //CustomerRN.RNAdd_4 = PDFDataModel_NEW.RSAdd_4 == null ? CustomerRN.RNAdd_4 : PDFDataModel_NEW.RSAdd_4;
                    //CustomerRN.RNAdd_5 = PDFDataModel_NEW.RSAdd_5 == null ? CustomerRN.RNAdd_5 : PDFDataModel_NEW.RSAdd_5;
                    //CustomerRN.RNAdd_6 = PDFDataModel_NEW.RSAdd_6 == null ? CustomerRN.RNAdd_6 : PDFDataModel_NEW.RSAdd_6;
                    //CustomerRN.RNAdd_7 = PDFDataModel_NEW.RSAdd_7 == null ? CustomerRN.RNAdd_7 : PDFDataModel_NEW.RSAdd_7;
                    //CustomerRN.RNAdd_8 = PDFDataModel_NEW.RSAdd_8 == null ? CustomerRN.RNAdd_8 : PDFDataModel_NEW.RSAdd_8;
                    //CustomerRN.RNAdd_9 = PDFDataModel_NEW.RSAdd_9 == null ? CustomerRN.RNAdd_9 : PDFDataModel_NEW.RSAdd_9;
                    ////電話
                    //CustomerRN.RNTel_1 = PDFDataModel_NEW.RSTel_1 == null ? CustomerRN.RNTel_1 : PDFDataModel_NEW.RSTel_1;
                    //CustomerRN.RNTel_2 = PDFDataModel_NEW.RSTel_2 == null ? CustomerRN.RNTel_2 : PDFDataModel_NEW.RSTel_2;
                    //手機
                    CustomerRN.RNCell = PDFDataModel_NEW.RSCell == null ? CustomerRN.RNCell : PDFDataModel_NEW.RSCell;
                    ////代理人1
                    //CustomerRN.RNAGID_A = PDFDataModel_NEW.RSAGID_A == null ? CustomerRN.RNAGID_A : PDFDataModel_NEW.RSAGID_A;
                    //CustomerRN.RNAGName_A = PDFDataModel_NEW.RSAGName_A == null ? CustomerRN.RNAGName_A : PDFDataModel_NEW.RSAGName_A;
                    //CustomerRN.RNAGAdd = RSAGAdd_A == null ? CustomerRN.RNAGAdd : RSAGAdd_A;
                    //CustomerRN.RNAGAdd_1_A = PDFDataModel_NEW.RSAGAdd_1_A == null ? CustomerRN.RNAGAdd_1_A : PDFDataModel_NEW.RSAGAdd_1_A;
                    //CustomerRN.RNAGAdd_1_1_A = PDFDataModel_NEW.RSAGAdd_1_1_A == null ? CustomerRN.RNAGAdd_1_1_A : PDFDataModel_NEW.RSAGAdd_1_1_A;
                    //CustomerRN.RNAGAdd_1_2_A = PDFDataModel_NEW.RSAGAdd_1_2_A == null ? CustomerRN.RNAGAdd_1_2_A : PDFDataModel_NEW.RSAGAdd_1_2_A;
                    //CustomerRN.RNAGAdd_2_A = PDFDataModel_NEW.RSAGAdd_2_A == null ? CustomerRN.RNAGAdd_2_A : PDFDataModel_NEW.RSAGAdd_2_A;
                    //CustomerRN.RNAGAdd_2_1_A = PDFDataModel_NEW.RSAGAdd_2_1_A == null ? CustomerRN.RNAGAdd_2_1_A : PDFDataModel_NEW.RSAGAdd_2_1_A;
                    //CustomerRN.RNAGAdd_2_2_A = PDFDataModel_NEW.RSAGAdd_2_2_A == null ? CustomerRN.RNAGAdd_2_2_A : PDFDataModel_NEW.RSAGAdd_2_2_A;
                    //CustomerRN.RNAGAdd_2_3_A = PDFDataModel_NEW.RSAGAdd_2_3_A == null ? CustomerRN.RNAGAdd_2_3_A : PDFDataModel_NEW.RSAGAdd_2_3_A;
                    //CustomerRN.RNAGAdd_2_4_A = PDFDataModel_NEW.RSAGAdd_2_4_A == null ? CustomerRN.RNAGAdd_2_4_A : PDFDataModel_NEW.RSAGAdd_2_4_A;
                    //CustomerRN.RNAGAdd_3_A = PDFDataModel_NEW.RSAGAdd_3_A == null ? CustomerRN.RNAGAdd_3_A : PDFDataModel_NEW.RSAGAdd_3_A;
                    //CustomerRN.RNAGAdd_3_1_A = PDFDataModel_NEW.RSAGAdd_3_1_A == null ? CustomerRN.RNAGAdd_3_1_A : PDFDataModel_NEW.RSAGAdd_3_1_A;
                    //CustomerRN.RNAGAdd_3_2_A = PDFDataModel_NEW.RSAGAdd_3_2_A == null ? CustomerRN.RNAGAdd_3_2_A : PDFDataModel_NEW.RSAGAdd_3_2_A;
                    //CustomerRN.RNAGAdd_4_A = PDFDataModel_NEW.RSAGAdd_4_A == null ? CustomerRN.RNAGAdd_4_A : PDFDataModel_NEW.RSAGAdd_4_A;
                    //CustomerRN.RNAGAdd_5_A = PDFDataModel_NEW.RSAGAdd_5_A == null ? CustomerRN.RNAGAdd_5_A : PDFDataModel_NEW.RSAGAdd_5_A;
                    //CustomerRN.RNAGAdd_6_A = PDFDataModel_NEW.RSAGAdd_6_A == null ? CustomerRN.RNAGAdd_6_A : PDFDataModel_NEW.RSAGAdd_6_A;
                    //CustomerRN.RNAGAdd_7_A = PDFDataModel_NEW.RSAGAdd_7_A == null ? CustomerRN.RNAGAdd_7_A : PDFDataModel_NEW.RSAGAdd_7_A;
                    //CustomerRN.RNAGAdd_8_A = PDFDataModel_NEW.RSAGAdd_8_A == null ? CustomerRN.RNAGAdd_8_A : PDFDataModel_NEW.RSAGAdd_8_A;
                    //CustomerRN.RNAGAdd_9_A = PDFDataModel_NEW.RSAGAdd_9_A == null ? CustomerRN.RNAGAdd_9_A : PDFDataModel_NEW.RSAGAdd_9_A;
                    //CustomerRN.RNAGCell_A = PDFDataModel_NEW.RSAGCell_A == null ? CustomerRN.RNAGCell_A : PDFDataModel_NEW.RSAGCell_A;

                    #endregion RS欄位處理
                }

                if (CustomerRN.RNID == null) {
                    throw new Exception("(自然人)身分證字號不能為空");
                }
                CustomerRN CustomerRN_DB = await ICustomerRNRepository.GetCustomerByRNID(CustomerRN.RNID);
                CustomerRN CustomerRN_NEW = Merger.CloneAndMerge<CustomerRN>(CustomerRN_DB, CustomerRN);
                CustomerRN_NEW.Id = CustomerRN_DB.Id;
                CustomerRN_NEW.LastModifyUserId = LastModifyUserId;
                CustomerRN_NEW.LastModifyTime = LastModifyTime;
                bool bl = ICustomerRNRepository.Update(CustomerRN_NEW, CustomerRN_DB.Id);

            }
            #endregion

            #region CustomerRC
            //房客-法人
            if (isCustomerRC) {
                CustomerRC CustomerRC = PDFDataModel_NEW.MapTo<CustomerRC>();
                {
                    #region RS欄位處理

                    //姓名
                    CustomerRC.RCName = PDFDataModel_NEW.RSName == null ? CustomerRC.RCName : PDFDataModel_NEW.RSName;
                    //身分證字號
                    CustomerRC.RCID = PDFDataModel_NEW.RSID == null || PDFDataModel_NEW.RSID == "" ? CustomerRC.RCID : PDFDataModel_NEW.RSID;
                    //住址 2022/12/06 PDF上所有地址都是唯讀 不允許修改 所以不需要額外處理地址了
                    //CustomerRC.RCAdd = PDFDataModel_NEW.RSAdd == null ? CustomerRC.RCAdd : PDFDataModel_NEW.RSAdd;
                    //CustomerRC.RCAdd_1 = PDFDataModel_NEW.RSAdd_1 == null ? CustomerRC.RCAdd_1 : PDFDataModel_NEW.RSAdd_1;
                    //CustomerRC.RCAdd_1_1 = PDFDataModel_NEW.RSAdd_1_1 == null ? CustomerRC.RCAdd_1_1 : PDFDataModel_NEW.RSAdd_1_1;
                    //CustomerRC.RCAdd_1_2 = PDFDataModel_NEW.RSAdd_1_2 == null ? CustomerRC.RCAdd_1_2 : PDFDataModel_NEW.RSAdd_1_2;
                    //CustomerRC.RCAdd_2 = PDFDataModel_NEW.RSAdd_2 == null ? CustomerRC.RCAdd_2 : PDFDataModel_NEW.RSAdd_2;
                    //CustomerRC.RCAdd_2_1 = PDFDataModel_NEW.RSAdd_2_1 == null ? CustomerRC.RCAdd_2_1 : PDFDataModel_NEW.RSAdd_2_1;
                    //CustomerRC.RCAdd_2_2 = PDFDataModel_NEW.RSAdd_2_2 == null ? CustomerRC.RCAdd_2_2 : PDFDataModel_NEW.RSAdd_2_2;
                    //CustomerRC.RCAdd_2_3 = PDFDataModel_NEW.RSAdd_2_3 == null ? CustomerRC.RCAdd_2_3 : PDFDataModel_NEW.RSAdd_2_3;
                    //CustomerRC.RCAdd_2_4 = PDFDataModel_NEW.RSAdd_2_4 == null ? CustomerRC.RCAdd_2_4 : PDFDataModel_NEW.RSAdd_2_4;
                    //CustomerRC.RCAdd_3 = PDFDataModel_NEW.RSAdd_3 == null ? CustomerRC.RCAdd_3 : PDFDataModel_NEW.RSAdd_3;
                    //CustomerRC.RCAdd_3_1 = PDFDataModel_NEW.RSAdd_3_1 == null ? CustomerRC.RCAdd_3_1 : PDFDataModel_NEW.RSAdd_3_1;
                    //CustomerRC.RCAdd_3_2 = PDFDataModel_NEW.RSAdd_3_2 == null ? CustomerRC.RCAdd_3_2 : PDFDataModel_NEW.RSAdd_3_2;
                    //CustomerRC.RCAdd_4 = PDFDataModel_NEW.RSAdd_4 == null ? CustomerRC.RCAdd_4 : PDFDataModel_NEW.RSAdd_4;
                    //CustomerRC.RCAdd_5 = PDFDataModel_NEW.RSAdd_5 == null ? CustomerRC.RCAdd_5 : PDFDataModel_NEW.RSAdd_5;
                    //CustomerRC.RCAdd_6 = PDFDataModel_NEW.RSAdd_6 == null ? CustomerRC.RCAdd_6 : PDFDataModel_NEW.RSAdd_6;
                    //CustomerRC.RCAdd_7 = PDFDataModel_NEW.RSAdd_7 == null ? CustomerRC.RCAdd_7 : PDFDataModel_NEW.RSAdd_7;
                    //CustomerRC.RCAdd_8 = PDFDataModel_NEW.RSAdd_8 == null ? CustomerRC.RCAdd_8 : PDFDataModel_NEW.RSAdd_8;
                    //CustomerRC.RCAdd_9 = PDFDataModel_NEW.RSAdd_9 == null ? CustomerRC.RCAdd_9 : PDFDataModel_NEW.RSAdd_9;
                    //電話
                    //CustomerRC.RCTel_1 = PDFDataModel_NEW.RSTel_1 == null ? CustomerRC.RCTel_1 : PDFDataModel_NEW.RSTel_1;
                    //CustomerRC.RCTel_2 = PDFDataModel_NEW.RSTel_2 == null ? CustomerRC.RCTel_2 : PDFDataModel_NEW.RSTel_2;
                    //手機
                    CustomerRC.RCCell = PDFDataModel_NEW.RSCell == null ? CustomerRC.RCCell : PDFDataModel_NEW.RSCell;


                    #endregion RS欄位處理
                }

                if (CustomerRC.RCID == null) {
                    throw new Exception("(自然人)身分證字號不能為空");
                }
                CustomerRC CustomerRC_DB = await _customerRCService.GetWhereAsync($" RCID = '{CustomerRC.RCID}' ");
                CustomerRC CustomerRC_NEW = Merger.CloneAndMerge<CustomerRC>(CustomerRC_DB, CustomerRC);
                CustomerRC_NEW.Id = CustomerRC_DB.Id;
                CustomerRC_NEW.LastModifyUserId = LastModifyUserId;
                CustomerRC_NEW.LastModifyTime = LastModifyTime;
                bool bl = _customerRCService.Update(CustomerRC_NEW, CustomerRC_DB.Id);

            }
            #endregion

            #region Building
            Building Building = PDFDataModel_NEW.MapTo<Building>();
            Building1 Building1 = PDFDataModel_NEW.MapTo<Building1>();

            #region 二房東欄位 (基本上不會用到)

            if (!string.IsNullOrEmpty(PDFDataModel_NEW.SLID)) {
                Building.BelongSLID = PDFDataModel_NEW.SLID;
            }
            if (!string.IsNullOrEmpty(PDFDataModel_NEW.SLName)) {
                var sldata = await _secondLandlordService.GetBySLName(PDFDataModel_NEW.SLName);
                if (sldata == null) {
                    throw new Exception("請確認二房東名稱");
                } else {
                    Building.BelongSLID = sldata.SLID;
                }
            }
            #endregion 二房東欄位

            if (!string.IsNullOrWhiteSpace(Building.BAdd)) {
                Building Building_DB = await IBuildingRepository.GetByBAdd(Building.BAdd);
                Building1 Building1_DB = await _building1Service.GetByBAdd(Building.BAdd);
                //if (Building_DB == null && Building1_DB == null) {
                //    Building.Id = newId;
                //    Building.CreatorUserId = CreatorUserId;
                //    Building.CreatorTime = CreatorTime;
                //    Building.CreatorUserOrgId = creatorUserOrgId;
                //    Building.CreatorUserDeptId = creatorUserDeptId;
                //    IBuildingRepository.Add(Building);
                //    Building1.Id = newId;
                //    Building1.CreatorUserId = CreatorUserId;
                //    Building1.CreatorTime = CreatorTime;
                //    _building1Service.Add(Building1);
                //}
                if (Building_DB != null && Building1_DB != null) {
                    Building Building_NEW = Merger.CloneAndMerge<Building>(Building_DB, Building);
                    Building1 Building1_NEW = Merger.CloneAndMerge<Building1>(Building1_DB, Building1);
                    Building_NEW.Id = Building_DB.Id;
                    Building_NEW.LastModifyUserId = LastModifyUserId;
                    Building_NEW.LastModifyTime = LastModifyTime;
                    Building1_NEW.Id = Building1_DB.Id;
                    Building1_NEW.LastModifyUserId = LastModifyUserId;
                    Building1_NEW.LastModifyTime = LastModifyTime;
                    bool bl = IBuildingRepository.Update(Building_NEW, Building_NEW.Id);
                    _building1Service.Update(Building1_NEW, Building1_NEW.Id);
                }
            }
            #endregion

            #region TBNoB1  
            if ("B1" == PDFDataModel_NEW.TBNO) {
                //這邊要改成 TBNO 決定不同Table存檔
                TBNoB1 TBNoB1 = PDFDataModel_NEW.MapTo<TBNoB1>();
                TBNoB1_2 TBNoB1_2 = PDFDataModel_NEW.MapTo<TBNoB1_2>();
                string sqlWhere = $"1=1 and FName='{TBNoB1.FName}'";
                string refKeys = "";
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.LNID))
                    refKeys += PDFDataModel_NEW.LNID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.LCID))
                    refKeys += PDFDataModel_NEW.LCID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.RNID))
                    refKeys += PDFDataModel_NEW.RNID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.RCID))
                    refKeys += PDFDataModel_NEW.RCID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.BAdd))
                    refKeys += PDFDataModel_NEW.BAdd;
                if (!string.IsNullOrEmpty(refKeys)) {
                    TBNoB1.RefKeys = refKeys;
                    sqlWhere += $" and RefKeys='{refKeys}'";
                }

                if (TBNoB1.RefKeys != null) {
                    //TBNoB1 TBNoB1_DB = await ITBNoB1Repository.GetByBAdd(TBNoB1.BAdd);
                    TBNoB1 TBNoB1_DB = ITBNoB1Service.GetWhere(@$"{sqlWhere}");
                    if (TBNoB1_DB == null) {
                        TBNoB1.Id = newId;
                        TBNoB1.CreatorUserId = CreatorUserId;
                        TBNoB1.CreatorTime = CreatorTime;
                        ITBNoB1Service.Insert(TBNoB1);
                        TBNoB1_2.Id = newId;
                        _tBNoB1_2Service.Insert(TBNoB1_2);
                    } else {
                        TBNoB1 TBNoB1_NEW = TBNoB1;// Merger.OverrideAndMerge<TBNoB1>(TBNoB1_DB, TBNoB1);
                        TBNoB1_2 tBNoB1_2_NEW = TBNoB1_2;
                        TBNoB1_NEW.Id = TBNoB1_DB.Id;
                        tBNoB1_2_NEW.Id = TBNoB1_DB.Id;
                        TBNoB1_NEW.CreatorUserId = TBNoB1_DB.CreatorUserId;
                        TBNoB1_NEW.CreatorTime = TBNoB1_DB.CreatorTime;
                        bool bl = ITBNoB1Service.Update(TBNoB1_NEW, TBNoB1_NEW.Id);
                        bool b2 = _tBNoB1_2Service.Update(tBNoB1_2_NEW, tBNoB1_2_NEW.Id);
                    }
                }
            }
            #endregion TBNoB1

            #region TBNoB2  
            if ("B2" == PDFDataModel_NEW.TBNO) {
                //這邊要改成 TBNO 決定不同Table存檔
                TBNoB2 TBNoB2 = PDFDataModel_NEW.MapTo<TBNoB2>();
                TBNoB2_2 TBNoB2_2 = PDFDataModel_NEW.MapTo<TBNoB2_2>();
                string sqlWhere = $"1=1 and FName='{TBNoB2.FName}'";
                string refKeys = "";
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.LNID))
                    refKeys += PDFDataModel_NEW.LNID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.LCID))
                    refKeys += PDFDataModel_NEW.LCID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.RNID))
                    refKeys += PDFDataModel_NEW.RNID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.RCID))
                    refKeys += PDFDataModel_NEW.RCID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.BAdd))
                    refKeys += PDFDataModel_NEW.BAdd;
                if (!string.IsNullOrEmpty(refKeys)) {
                    TBNoB2.RefKeys = refKeys;
                    sqlWhere += $" and RefKeys='{refKeys}'";
                }

                if (TBNoB2.RefKeys != null) {
                    //TBNoB2 TBNoB2_DB = await ITBNoB2Repository.GetByBAdd(TBNoB2.BAdd);
                    TBNoB2 TBNoB2_DB = ITBNoB2Service.GetWhere(@$"{sqlWhere}");
                    if (TBNoB2_DB == null) {
                        TBNoB2.Id = newId;
                        TBNoB2.CreatorUserId = CreatorUserId;
                        TBNoB2.CreatorTime = CreatorTime;
                        ITBNoB2Service.Insert(TBNoB2);
                        TBNoB2_2.Id = newId;
                        _tBNoB2_2Service.Insert(TBNoB2_2);
                    } else {
                        TBNoB2 TBNoB2_NEW = TBNoB2;// Merger.OverrideAndMerge<TBNoB2>(TBNoB2_DB, TBNoB2);
                        TBNoB2_2 tBNoB2_2_NEW = TBNoB2_2;
                        TBNoB2_NEW.Id = TBNoB2_DB.Id;
                        tBNoB2_2_NEW.Id = TBNoB2_DB.Id;
                        TBNoB2_NEW.CreatorUserId = TBNoB2_DB.CreatorUserId;
                        TBNoB2_NEW.CreatorTime = TBNoB2_DB.CreatorTime;
                        bool bl = ITBNoB2Service.Update(TBNoB2_NEW, TBNoB2_NEW.Id);
                        bool b2 = _tBNoB2_2Service.Update(tBNoB2_2_NEW, tBNoB2_2_NEW.Id);
                    }
                }
            }
            #endregion TBNoB2

            #region TBNoB3  
            if ("B3" == PDFDataModel_NEW.TBNO) {
                //這邊要改成 TBNO 決定不同Table存檔
                TBNoB3 TBNoB3 = PDFDataModel_NEW.MapTo<TBNoB3>();
                TBNoB3_2 TBNoB3_2 = PDFDataModel_NEW.MapTo<TBNoB3_2>();
                string sqlWhere = $"1=1 and FName='{TBNoB3.FName}'";
                string refKeys = "";
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.LNID))
                    refKeys += PDFDataModel_NEW.LNID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.LCID))
                    refKeys += PDFDataModel_NEW.LCID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.RNID))
                    refKeys += PDFDataModel_NEW.RNID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.RCID))
                    refKeys += PDFDataModel_NEW.RCID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.BAdd))
                    refKeys += PDFDataModel_NEW.BAdd;
                if (!string.IsNullOrEmpty(refKeys)) {
                    TBNoB3.RefKeys = refKeys;
                    sqlWhere += $" and RefKeys='{refKeys}'";
                }

                if (TBNoB3.RefKeys != null) {
                    //TBNoB3 TBNoB3_DB = await ITBNoB3Repository.GetByBAdd(TBNoB3.BAdd);
                    TBNoB3 TBNoB3_DB = ITBNoB3Service.GetWhere(@$"{sqlWhere}");
                    if (TBNoB3_DB == null) {
                        TBNoB3.Id = newId;
                        TBNoB3.CreatorUserId = CreatorUserId;
                        TBNoB3.CreatorTime = CreatorTime;
                        ITBNoB3Service.Insert(TBNoB3);
                        TBNoB3_2.Id = newId;
                        _tBNoB3_2Service.Insert(TBNoB3_2);
                    } else {
                        TBNoB3 TBNoB3_NEW = TBNoB3;// Merger.OverrideAndMerge<TBNoB3>(TBNoB3_DB, TBNoB3);
                        TBNoB3_2 tBNoB3_2_NEW = TBNoB3_2;
                        TBNoB3_NEW.Id = TBNoB3_DB.Id;
                        tBNoB3_2_NEW.Id = TBNoB3_DB.Id;
                        TBNoB3_NEW.CreatorUserId = TBNoB3_DB.CreatorUserId;
                        TBNoB3_NEW.CreatorTime = TBNoB3_DB.CreatorTime;
                        bool bl = ITBNoB3Service.Update(TBNoB3_NEW, TBNoB3_NEW.Id);
                        bool B3 = _tBNoB3_2Service.Update(tBNoB3_2_NEW, tBNoB3_2_NEW.Id);
                    }
                }
            }
            #endregion TBNoB3

            #region TBNoB4  
            if ("B4" == PDFDataModel_NEW.TBNO) {
                //這邊要改成 TBNO 決定不同Table存檔
                TBNoB4 TBNoB4 = PDFDataModel_NEW.MapTo<TBNoB4>();
                TBNoB4_2 TBNoB4_2 = PDFDataModel_NEW.MapTo<TBNoB4_2>();
                string sqlWhere = $"1=1 and FName='{TBNoB4.FName}'";
                string refKeys = "";
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.LNID))
                    refKeys += PDFDataModel_NEW.LNID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.LCID))
                    refKeys += PDFDataModel_NEW.LCID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.RNID))
                    refKeys += PDFDataModel_NEW.RNID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.RCID))
                    refKeys += PDFDataModel_NEW.RCID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.BAdd))
                    refKeys += PDFDataModel_NEW.BAdd;
                if (!string.IsNullOrEmpty(refKeys)) {
                    TBNoB4.RefKeys = refKeys;
                    sqlWhere += $" and RefKeys='{refKeys}'";
                }

                if (TBNoB4.RefKeys != null) {
                    //TBNoB4 TBNoB4_DB = await ITBNoB4Repository.GetByBAdd(TBNoB4.BAdd);
                    TBNoB4 TBNoB4_DB = ITBNoB4Service.GetWhere(@$"{sqlWhere}");
                    if (TBNoB4_DB == null) {
                        TBNoB4.Id = newId;
                        TBNoB4.CreatorUserId = CreatorUserId;
                        TBNoB4.CreatorTime = CreatorTime;
                        ITBNoB4Service.Insert(TBNoB4);
                        TBNoB4_2.Id = newId;
                        _tBNoB4_2Service.Insert(TBNoB4_2);
                    } else {
                        TBNoB4 TBNoB4_NEW = TBNoB4;// Merger.OverrideAndMerge<TBNoB4>(TBNoB4_DB, TBNoB4);
                        TBNoB4_2 tBNoB4_2_NEW = TBNoB4_2;
                        TBNoB4_NEW.Id = TBNoB4_DB.Id;
                        tBNoB4_2_NEW.Id = TBNoB4_DB.Id;
                        TBNoB4_NEW.CreatorUserId = TBNoB4_DB.CreatorUserId;
                        TBNoB4_NEW.CreatorTime = TBNoB4_DB.CreatorTime;
                        bool bl = ITBNoB4Service.Update(TBNoB4_NEW, TBNoB4_NEW.Id);
                        bool B4 = _tBNoB4_2Service.Update(tBNoB4_2_NEW, tBNoB4_2_NEW.Id);
                    }
                }
            }
            #endregion TBNoB4

            #region TBNoB5  
            if ("B5" == PDFDataModel_NEW.TBNO) {
                //這邊要改成 TBNO 決定不同Table存檔
                TBNoB5 TBNoB5 = PDFDataModel_NEW.MapTo<TBNoB5>();
                TBNoB5_2 TBNoB5_2 = PDFDataModel_NEW.MapTo<TBNoB5_2>();
                string sqlWhere = $"1=1 and FName='{TBNoB5.FName}'";
                string refKeys = "";
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.LNID))
                    refKeys += PDFDataModel_NEW.LNID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.LCID))
                    refKeys += PDFDataModel_NEW.LCID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.RNID))
                    refKeys += PDFDataModel_NEW.RNID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.RCID))
                    refKeys += PDFDataModel_NEW.RCID;
                if (!string.IsNullOrEmpty(PDFDataModel_NEW.BAdd))
                    refKeys += PDFDataModel_NEW.BAdd;
                if (!string.IsNullOrEmpty(refKeys)) {
                    TBNoB5.RefKeys = refKeys;
                    sqlWhere += $" and RefKeys='{refKeys}'";
                }

                if (TBNoB5.RefKeys != null) {
                    //TBNoB5 TBNoB5_DB = await ITBNoB5Repository.GetByBAdd(TBNoB5.BAdd);
                    TBNoB5 TBNoB5_DB = ITBNoB5Service.GetWhere(@$"{sqlWhere}");
                    if (TBNoB5_DB == null) {
                        TBNoB5.Id = newId;
                        TBNoB5.CreatorUserId = CreatorUserId;
                        TBNoB5.CreatorTime = CreatorTime;
                        ITBNoB5Service.Insert(TBNoB5);
                        TBNoB5_2.Id = newId;
                        _tBNoB5_2Service.Insert(TBNoB5_2);
                    } else {
                        TBNoB5 TBNoB5_NEW = TBNoB5;// Merger.OverrideAndMerge<TBNoB5>(TBNoB5_DB, TBNoB5);
                        TBNoB5_2 tBNoB5_2_NEW = TBNoB5_2;
                        TBNoB5_NEW.Id = TBNoB5_DB.Id;
                        tBNoB5_2_NEW.Id = TBNoB5_DB.Id;
                        TBNoB5_NEW.CreatorUserId = TBNoB5_DB.CreatorUserId;
                        TBNoB5_NEW.CreatorTime = TBNoB5_DB.CreatorTime;
                        bool bl = ITBNoB5Service.Update(TBNoB5_NEW, TBNoB5_NEW.Id);
                        bool B5 = _tBNoB5_2Service.Update(tBNoB5_2_NEW, tBNoB5_2_NEW.Id);
                    }
                }
            }
            #endregion TBNoB5

            //新增至黑名單列表
            //InsertBlackList(PDFDataModel_NEW); //20220914 黑名單的匯入只經由web表單的A000002

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

                if (!dictV.ContainsKey("FName")) {
                    result.ErrCode = "40001";
                    result.ErrMsg = "上傳PDF檔案不含標籤[FName]";
                    return ToJsonContent(result, true);
                } else {
                    if (dictV["FName"] != FormId) {
                        result.ErrCode = "40001";
                        result.ErrMsg = $"上傳PDF檔案標籤[FName]不為{FormId}";
                        return ToJsonContent(result, true);
                    }
                }

                if (!dictV.ContainsKey("Vno")) {
                    result.ErrCode = "40001";
                    result.ErrMsg = "上傳PDF檔案不含標籤[Vno]";
                    return ToJsonContent(result, true);
                } else {
                    Externalform Externalform_DB = await iService.GetByFormId(FormId);
                    if (Externalform_DB == null) {
                        result.ErrCode = "40001";
                        result.ErrMsg = $"FormId:{FormId}對應資料尚未建立!";
                        return ToJsonContent(result, true);
                    }

                    if (dictV["Vno"] != Externalform_DB.Vno) {
                        result.ErrCode = "40001";
                        result.ErrMsg = $"上傳PDF檔案[版本號]標籤[Vno]不為{Externalform_DB.Vno}";
                        return ToJsonContent(result, true);
                    }
                }

                Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
                Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
                //https://stackoverflow.com/questions/13865718/directory-getfiles-get-todays-files-only
                //DirectoryInfo infoD = new DirectoryInfo(Path.Combine("D:/zzz/", "image/"));
                //DirectoryInfo infoD = new DirectoryInfo(sysSetting.ChaochiFilepath);
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
        private UploadFileResultOuputDto AddFile(IFormFile file, string uploadPath, string FormId)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
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

            //string ext = Path.GetExtension(fileName).ToLower();
            //string newName = GuidUtils.CreateNo();
            //string newfileName = newName + ext;

            if (!System.IO.Directory.Exists(uploadPath)) {
                System.IO.Directory.CreateDirectory(uploadPath);
            }

            using (var fs = new FileStream(uploadPath + fileName, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }
        }

        /// <summary>
        /// 根據主鍵Id獲取一個對象信息
        /// </summary>
        /// <param name="id">主鍵Id</param>
        /// <returns></returns>
        [HttpGet("GetImgById")]
        [YuebonAuthorize("List")]
        public virtual async Task<CommonResult<FileInfoOutputDto>> GetImgById(string id)
        {
            CommonResult<FileInfoOutputDto> result = new CommonResult<FileInfoOutputDto>();
            FileInfoOutputDto info = await iService.GetImgOutDtoAsync(id);
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
        /// 刪除文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet("DeleteImgFile")]
        public IActionResult DeleteImgFile(string fileName)
        {
            CommonResult result = new CommonResult();
            try {
                YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

                //UploadFile uploadFile = new UploadFileApp().Get(id);
                DirectoryInfo infoD = new DirectoryInfo(sysSetting.ChaochiFilepath);

                //https://stackoverflow.com/questions/13865718/directory-getfiles-get-todays-files-only
                //DirectoryInfo infoD = new DirectoryInfo(Path.Combine("D:/zzz/", "image/"));
                List<FileInfo> files = infoD.GetFiles().Where(x => x.Name == fileName).OrderBy(p => p.CreationTime).ToList<FileInfo>();


                string localpath = sysSetting.ChaochiFilepath;
                if (files != null && files.Count == 1) {
                    string filepath = localpath + "/" + fileName;
                    if (System.IO.File.Exists(filepath))
                        System.IO.File.Delete(filepath);
                    //string filepathThu = (localpath + "/" + uploadFile.Thumbnail).ToFilePath();
                    //if (System.IO.File.Exists(filepathThu))
                    //    System.IO.File.Delete(filepathThu);

                    result.ErrCode = ErrCode.successCode;
                    result.Success = true;
                } else {
                    result.ErrCode = ErrCode.failCode;
                    result.Success = false;
                }

            } catch (Exception ex) {
                Log4NetHelper.Error("", ex);
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
            }
            return ToJsonContent(result);
        }


        /// <summary>
        /// 檢查登入者上傳、下載權限
        /// </summary>
        /// <param name="pDFDataModel"></param>
        /// <returns></returns>
        private async Task<CommonResult> CheckAuth(PDFDataModel pDFDataModel)
        {
            var result = new CommonResult();
            bool isLN = false, isLC = false;
            if (!string.IsNullOrEmpty(pDFDataModel.LNID)) {
                isLN = true;
            }
            if (!string.IsNullOrEmpty(pDFDataModel.LCID)) {
                isLC = true;
            }
            if (!string.IsNullOrEmpty(pDFDataModel.LSID)) {
                if (pDFDataModel.LSID.Length == 10) {
                    isLN = true;
                    pDFDataModel.LNID = pDFDataModel.LSID;
                } else if (pDFDataModel.LSID.Length == 8) {
                    isLC = true;
                    pDFDataModel.LCID = pDFDataModel.LSID;
                }
            }

            //檢查房東歸屬權
            if (isLN) {
                var checkLN = await CustomerLNService.GetByCustomerLNID(pDFDataModel.LNID);
                if (checkLN != null) {
                    var s = checkLN.CreatorUserId.Contains(CurrentUser.UserId);
                    if (!s) {
                        result.Success = false;
                        result.ErrMsg = $"你沒有操作此房東的權限";
                        return result;
                    }
                }
            } else if (isLC) {
                var checkLC = await CustomerLCService.GetByLCID(pDFDataModel.LCID);
                if (checkLC != null) {
                    var s = checkLC.CreatorUserId.Contains(CurrentUser.UserId);
                    if (!s) {
                        result.Success = false;
                        result.ErrMsg = $"你沒有操作此房東的權限";
                        return result;
                    }
                }
            }
            //檢查建物歸屬權
            if (!string.IsNullOrEmpty(pDFDataModel.BAdd) || !string.IsNullOrEmpty(pDFDataModel.BAdd_1)) {
                string BAdd = "";
                if (!string.IsNullOrEmpty(pDFDataModel.BAdd_1)) {
                    if (pDFDataModel.BAdd_1.Length <= 3) {
                        //var pdfModel = Merger.DictCloneAndMerge<PDFDataModel>(new PDFDataModel(), dictV);
                        BAdd = Utils.memergeAddFix(
                           pDFDataModel.BAdd_1,
                           pDFDataModel.BAdd_1_1,
                           pDFDataModel.BAdd_1_2,
                           pDFDataModel.BAdd_2,
                           pDFDataModel.BAdd_2_1,
                           pDFDataModel.BAdd_2_2,
                           pDFDataModel.BAdd_2_3,
                           pDFDataModel.BAdd_2_4,
                           pDFDataModel.BAdd_3,
                           pDFDataModel.BAdd_3_1,
                           pDFDataModel.BAdd_3_2,
                           pDFDataModel.BAdd_4,
                           pDFDataModel.BAdd_5,
                           pDFDataModel.BAdd_6,
                           pDFDataModel.BAdd_7,
                           pDFDataModel.BAdd_8,
                           pDFDataModel.BAdd_9);
                    }
                } else {
                    BAdd = pDFDataModel.BAdd;
                }
                var checkB = buildingService.GetWhere($"BAdd='{BAdd}'");
                if (checkB != null) {
                    if (checkB.CreatorUserId != CurrentUser.UserId) {
                        result.Success = false;
                        result.ErrMsg = $"你沒有操作此建物的權限";
                        return result;
                        //throw new Exception("你沒有操作此建物的權限");
                    }
                    if (!string.IsNullOrEmpty(pDFDataModel.LNID)) {
                        if (checkB.BelongLID != pDFDataModel.LNID) {
                            result.Success = false;
                            result.ErrMsg = $"你沒有操作此房東對應之建物的權限";
                            return result;
                            //throw new Exception("你沒有操作此建物對應之房東的權限");
                        }
                    } else if (!string.IsNullOrEmpty(pDFDataModel.LCID)) {
                        if (checkB.BelongLID != pDFDataModel.LCID) {
                            result.Success = false;
                            result.ErrMsg = $"你沒有操作此房東對應之建物的權限";
                            return result;
                            //throw new Exception("你沒有操作此建物對應之房東的權限");
                        }
                    }
                }
            }
            //檢查房客歸屬權
            if (!string.IsNullOrEmpty(pDFDataModel.RNID)) {
                var checkRN = await CustomerRNService.GetCustomerByRNID(pDFDataModel.RNID);
                if (checkRN != null) {
                    var s = checkRN.CreatorUserId.Contains(CurrentUser.UserId);
                    if (!s) {
                        result.Success = false;
                        result.ErrMsg = $"你沒有操作此房客的權限";
                        return result;
                    }
                }
            } else if (!string.IsNullOrEmpty(pDFDataModel.RCID)) {
                var checkRC = await _customerRCService.GetWhereAsync($" RCID = '{pDFDataModel.RCID}' ");
                if (checkRC != null) {
                    var s = checkRC.CreatorUserId.Contains(CurrentUser.UserId);
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



        [NonAction]
        private async Task<CommonResult> SaveNoKeyForm(string arhTo, string data, string fname, IFormFile file)
        {
            var result = new CommonResult();
            var dt = DateTime.Now;
            var fileName = dt.ToString("yyyyMMddHHmmss") + "_" + fname;
            string uploadpath = "";
            var yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            var sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            switch (arhTo) {
                case "LN":
                    var ln = await CustomerLNService.GetByCustomerLNID(data);
                    if (ln is null) {
                        throw new Exception("查無此出租自然人");
                    } else {
                        uploadpath = sysSetting.ChaochiFilepath + $"historyPDF/CustomerL/LN/{data}/";
                        await IHistoryFormLNService.InsertAsync(new HistoryFormLN {
                            IDNo = data,
                            FormName = fname,
                            Note = "外部表單PDF上傳",
                            UploadTime = dt,
                            FileName = fileName + ".pdf",
                            CreatorUserId = CurrentUser.UserId
                        });
                    }
                    break;
                case "LC":
                    var lc = await CustomerLCService.GetByLCID(data);
                    if (lc is null) {
                        throw new Exception("查無此出租法人");
                    } else {
                        uploadpath = sysSetting.ChaochiFilepath + $"historyPDF/CustomerL/LC/{data}/";
                        await IHistoryFormLCService.InsertAsync(new HistoryFormLC {
                            IDNo = data,
                            FormName = fname,
                            Note = "外部表單PDF上傳",
                            UploadTime = dt,
                            FileName = fileName + ".pdf",
                            CreatorUserId = CurrentUser.UserId
                        });
                    }
                    break;
                case "RN":
                    var rn = await CustomerRNService.GetCustomerByRNID(data);
                    if (rn is null) {
                        throw new Exception("查無此承租自然人");
                    } else {
                        uploadpath = sysSetting.ChaochiFilepath + $"historyPDF/CustomerR/RN/{data}/";
                        await IHistoryFormRNService.InsertAsync(new HistoryFormRN {
                            IDNo = data,
                            FormName = fname,
                            Note = "外部表單PDF上傳",
                            UploadTime = dt,
                            FileName = fileName + ".pdf",
                            CreatorUserId = CurrentUser.UserId
                        });
                    }
                    break;
                case "RC":
                    var rc = await _customerRCService.GetWhereAsync($" RCID ='{data}' ");
                    if (rc is null) {
                        throw new Exception("查無此承租法人");
                    } else {
                        uploadpath = sysSetting.ChaochiFilepath + $"historyPDF/CustomerR/RC/{data}/";
                        await IHistoryFormRCService.InsertAsync(new HistoryFormRC {
                            IDNo = data,
                            FormName = fname,
                            Note = "外部表單PDF上傳",
                            UploadTime = dt,
                            FileName = fileName + ".pdf",
                            CreatorUserId = CurrentUser.UserId
                        });
                    }
                    break;
                case "B":
                    var buildingId = buildingService.GetIdByBAdd(data);
                    if (string.IsNullOrEmpty(buildingId)) {
                        throw new Exception("查無此建物");
                    } else {
                        uploadpath = sysSetting.ChaochiFilepath + $"historyPDF/Building/{buildingId}/";
                        await IHistoryFormBuildingService.InsertAsync(new HistoryFormBuilding {
                            IDNo = buildingId,
                            FormName = fname,
                            Note = "外部表單PDF上傳",
                            UploadTime = dt,
                            FileName = fileName + ".pdf",
                            CreatorUserId = CurrentUser.UserId
                        });
                    }
                    break;
                default:
                    break;
            }

            AddFile(file, uploadpath, fileName);

            result.Success = true;
            result.ResData = "";
            return result;
        }

        ///// <summary>
        ///// 上傳時若有同住人欄位就要新增此同住人至黑名單並且將此同住人送出審核
        ///// </summary>
        ///// <param name="dataModel"></param>
        ///// <returns></returns>
        //private void InsertBlackList(PDFDataModel dataModel)
        //{
        //    List<BlackList> blackLists = new List<BlackList>();
        //    if (!string.IsNullOrEmpty(dataModel.RNName) && !string.IsNullOrEmpty(dataModel.RNID) && !string.IsNullOrEmpty(dataModel.RNBirthday)) {
        //        var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNBirthday.Split("-")[0]) + 1911)).ToString();
        //        var blacklist = new BlackList() { Name = dataModel.RNName, IDNo = dataModel.RNID, Birthday = dataModel.RNBirthday, Age = age, ReportDept = organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "承租人" };
        //        blackLists.Add(blacklist);
        //    }
        //    if (!string.IsNullOrEmpty(dataModel.RNFName_A) && !string.IsNullOrEmpty(dataModel.RNFID_1_A) && !string.IsNullOrEmpty(dataModel.RNFBirthday_A)) {
        //        var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNFBirthday_A.Split("-")[0]) + 1911)).ToString();
        //        var blacklist = new BlackList() { Name = dataModel.RNFName_A, IDNo = dataModel.RNFID_1_A, Birthday = dataModel.RNFBirthday_A, Age = age, ReportDept = organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "同住人" };
        //        blackLists.Add(blacklist);
        //    }
        //    if (!string.IsNullOrEmpty(dataModel.RNFName_B) && !string.IsNullOrEmpty(dataModel.RNFID_1_B) && !string.IsNullOrEmpty(dataModel.RNFBirthday_B)) {
        //        var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNFBirthday_B.Split("-")[0]) + 1911)).ToString();
        //        var blacklist = new BlackList() { Name = dataModel.RNFName_B, IDNo = dataModel.RNFID_1_B, Birthday = dataModel.RNFBirthday_B, Age = age, ReportDept = organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "同住人" };
        //        blackLists.Add(blacklist);
        //    }
        //    if (!string.IsNullOrEmpty(dataModel.RNFName_C) && !string.IsNullOrEmpty(dataModel.RNFID_1_C) && !string.IsNullOrEmpty(dataModel.RNFBirthday_C)) {
        //        var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNFBirthday_C.Split("-")[0]) + 1911)).ToString();
        //        var blacklist = new BlackList() { Name = dataModel.RNFName_C, IDNo = dataModel.RNFID_1_C, Birthday = dataModel.RNFBirthday_C, Age = age, ReportDept = organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "同住人" };
        //        blackLists.Add(blacklist);
        //    }
        //    if (!string.IsNullOrEmpty(dataModel.RNFName_D) && !string.IsNullOrEmpty(dataModel.RNFID_1_D) && !string.IsNullOrEmpty(dataModel.RNFBirthday_D)) {
        //        var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNFBirthday_D.Split("-")[0]) + 1911)).ToString();
        //        var blacklist = new BlackList() { Name = dataModel.RNFName_D, IDNo = dataModel.RNFID_1_D, Birthday = dataModel.RNFBirthday_D, Age = age, ReportDept = organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "同住人" };
        //        blackLists.Add(blacklist);
        //    }
        //    if (!string.IsNullOrEmpty(dataModel.RNFName_E) && !string.IsNullOrEmpty(dataModel.RNFID_1_E) && !string.IsNullOrEmpty(dataModel.RNFBirthday_E)) {
        //        var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNFBirthday_E.Split("-")[0]) + 1911)).ToString();
        //        var blacklist = new BlackList() { Name = dataModel.RNFName_E, IDNo = dataModel.RNFID_1_E, Birthday = dataModel.RNFBirthday_E, Age = age, ReportDept = organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "同住人" };
        //        blackLists.Add(blacklist);
        //    }
        //    if (!string.IsNullOrEmpty(dataModel.RNFName_F) && !string.IsNullOrEmpty(dataModel.RNFID_1_F) && !string.IsNullOrEmpty(dataModel.RNFBirthday_F)) {
        //        var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNFBirthday_F.Split("-")[0]) + 1911)).ToString();
        //        var blacklist = new BlackList() { Name = dataModel.RNFName_F, IDNo = dataModel.RNFID_1_F, Birthday = dataModel.RNFBirthday_F, Age = age, ReportDept = organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "同住人" };
        //        blackLists.Add(blacklist);
        //    }
        //    if (!string.IsNullOrEmpty(dataModel.RNFName_G) && !string.IsNullOrEmpty(dataModel.RNFID_1_G) && !string.IsNullOrEmpty(dataModel.RNFBirthday_G)) {
        //        var age = (DateTime.Now.Year - (Convert.ToInt32(dataModel.RNFBirthday_G.Split("-")[0]) + 1911)).ToString();
        //        var blacklist = new BlackList() { Name = dataModel.RNFName_G, IDNo = dataModel.RNFID_1_G, Birthday = dataModel.RNFBirthday_G, Age = age, ReportDept = organizeService.GetAsync(CurrentUser.DeptId).Result.FullName, CreatorUserId = CurrentUser.UserId, LastModifyUserId = "系統自動", CreatorTime = DateTime.Now, LastModifyTime = DateTime.Now, ResultState = "資料蒐集中", IDentity = "同住人" };
        //        blackLists.Add(blacklist);
        //    }
        //    if (blackLists.Count() > 0) {
        //        _blackListService.InsertRange(blackLists);
        //    }
        //}
    }
}