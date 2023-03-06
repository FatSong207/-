using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Chaochi.IServices;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Json;
using System.IO;
using Yuebon.Commons.Log;
using Microsoft.AspNetCore.StaticFiles;
using System.Reflection;
using System.Linq;
using Yuebon.WebApi.Areas.Chaochi.Controllers;
using System.Collections.Generic;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.AcroForms;
using PdfSharp.Pdf.Annotations;
using PdfSharp.Pdf.IO;
using Yuebon.Chaochi.IRepositories;
using System.Text;
using Yuebon.Commons.IDbContext;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extensions;

namespace Yuebon.WebApi.Areas.Chaochi
{
    /// <summary>
    /// 領收據分類接口
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/Chaochi/[controller]")]
    public class ReceiptController : AreaApiController<Receipt, ReceiptOutputDto, ReceiptInputDto, IReceiptService, string>
    {
        private IBuildingRepository IBuildingRepository;
        private readonly IDbContextCore ybContext;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_IBuildingRepository"></param>
        /// <param name="_ybContext"></param>
        public ReceiptController(IReceiptService _iService, IBuildingRepository _IBuildingRepository, IDbContextCore _ybContext) : base(_iService)
        {
            iService = _iService;
            ybContext = _ybContext;
            IBuildingRepository = _IBuildingRepository;
        }

        /// <summary>
        /// 新增前處理資料
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Receipt info)
        {
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.CreatorUserOrgId = CurrentUser.OrganizeId;
            info.CreatorUserDeptId = CurrentUser.DeptId;
            info.DeleteMark = false;
        }

        /// <summary>
        /// 在更新資料前對資料的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(Receipt info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在軟刪除資料前對資料的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Receipt info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }

        /// <summary>
        /// 異步新增數據
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(ReceiptInputDto input)
        {
            CommonResult result = new CommonResult();

            // 地址
            input.RAdd =
                Utils.memergeAddFix(
                   input.RAdd_1,
                   input.RAdd_1_1,
                   input.RAdd_1_2,
                   input.RAdd_2,
                   input.RAdd_2_1,
                   input.RAdd_2_2,
                   input.RAdd_2_3,
                   input.RAdd_2_4,
                   input.RAdd_3,
                   input.RAdd_3_1,
                   input.RAdd_3_2,
                   input.RAdd_4,
                   input.RAdd_5,
                   input.RAdd_6,
                   input.RAdd_7,
                   input.RAdd_8,
                   input.RAdd_9
                );

            // 儲存簽名檔
            if (!input.RSignatureIsEmpty) {
                input.RSignatureImgPath = SaveReceiptSignature(input.RSignatureBase64, $"{input.ReceiptId}.jpg");
            }                        

            // 處理領收據日
            if (!string.IsNullOrEmpty(input.RDate)) {

                string[] dates = input.RDate.Split("-");
                if (dates.Length > 0 && dates.Length == 3) {
                    input.RDate_Y = dates[0];
                    input.RDate_M = dates[1];
                    input.RDate_D = dates[2];
                }
            }

            // 處理租金起日
            if (!string.IsNullOrEmpty(input.PIRentStartDate)) {
                string[] starts = input.PIRentStartDate.Split("-");
                if (starts.Length > 0 && starts.Length == 3) {
                    input.PIRentStartDate_Y = starts[0];
                    input.PIRentStartDate_M = starts[1];
                    input.PIRentStartDate_D = starts[2];
                }
            }

            // 處理租金迄日
            if (!string.IsNullOrEmpty(input.PIRentEndDate)) {
                string[] ends = input.PIRentEndDate.Split("-");
                if (ends.Length > 0 && ends.Length == 3) {
                    input.PIRentEndDate_Y = ends[0];
                    input.PIRentEndDate_M = ends[1];
                    input.PIRentEndDate_D = ends[2];
                }
            }

            // 處理抽金日
            if (!string.IsNullOrEmpty(input.PIDepositDate)) {
                string[] dds = input.PIDepositDate.Split("-");
                if (dds.Length > 0 && dds.Length == 3) {
                    input.PIDepositDate_Y = dds[0];
                    input.PIDepositDate_M = dds[1];
                    input.PIDepositDate_D = dds[2];
                }
            }


            // todo 重複收訂
            

            // 產生PDF(用印和未用印)
            string formId = (!string.IsNullOrEmpty(input.RCate) && "內部收據".Equals(input.RCate)) ? "A000601" : "A000501";
            SavePDFWithDataByFormId(formId, input);

            Receipt info = input.MapTo<Receipt>();

            OnBeforeInsert(info);

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                long ln = await iService.InsertAsync(info, conn, tran).ConfigureAwait(false);
                if (ln > 0) {
                    tran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    result.ErrMsg = ErrCode.err43001;
                    result.ErrCode = "43001";
                    result.Success = false;
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("新增領收據失敗", ex);
                result.ErrMsg = "新增領收據失敗";
                result.ErrCode = ErrCode.err43001;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }


            return ToJsonContent(result);
        }

        /// <summary>
        /// 非同步更新數據
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(ReceiptInputDto input)
        {
            CommonResult result = new CommonResult();

            Receipt info = iService.Get(input.Id);

            // 地址
            input.RAdd =
                Utils.memergeAddFix(
                   input.RAdd_1,
                   input.RAdd_1_1,
                   input.RAdd_1_2,
                   input.RAdd_2,
                   input.RAdd_2_1,
                   input.RAdd_2_2,
                   input.RAdd_2_3,
                   input.RAdd_2_4,
                   input.RAdd_3,
                   input.RAdd_3_1,
                   input.RAdd_3_2,
                   input.RAdd_4,
                   input.RAdd_5,
                   input.RAdd_6,
                   input.RAdd_7,
                   input.RAdd_8,
                   input.RAdd_9
                );

            // 儲存簽名檔
            input.RSignatureImgPath = (string.IsNullOrEmpty(info.RSignatureImgPath) && !input.RSignatureIsEmpty) ? SaveReceiptSignature(input.RSignatureBase64, $"{input.ReceiptId}.jpg") : info.RSignatureImgPath;

            // 處理領收據日
            if (!string.IsNullOrEmpty(input.RDate)) {

                string[] dates = input.RDate.Split("-");
                if (dates.Length > 0 && dates.Length == 3) {
                    input.RDate_Y = dates[0];
                    input.RDate_M = dates[1];
                    input.RDate_D = dates[2];
                }
            }

            // 處理租金起日
            if (!string.IsNullOrEmpty(input.PIRentStartDate)) {
                string[] starts = input.PIRentStartDate.Split("-");
                if (starts.Length > 0 && starts.Length == 3) {
                    input.PIRentStartDate_Y = starts[0];
                    input.PIRentStartDate_M = starts[1];
                    input.PIRentStartDate_D = starts[2];
                }
            }

            // 處理租金迄日
            if (!string.IsNullOrEmpty(input.PIRentEndDate)) {
                string[] ends = input.PIRentEndDate.Split("-");
                if (ends.Length > 0 && ends.Length == 3) {
                    input.PIRentEndDate_Y = ends[0];
                    input.PIRentEndDate_M = ends[1];
                    input.PIRentEndDate_D = ends[2];
                }
            }

            // 處理抽金日
            if (!string.IsNullOrEmpty(input.PIDepositDate)) {
                string[] dds = input.PIDepositDate.Split("-");
                if (dds.Length > 0 && dds.Length == 3) {
                    input.PIDepositDate_Y = dds[0];
                    input.PIDepositDate_M = dds[1];
                    input.PIDepositDate_D = dds[2];
                }
            }

            // todo 重複收訂

            // 產生PDF(用印和未用印)
            string formId = (!string.IsNullOrEmpty(input.RCate) && "內部收據".Equals(input.RCate)) ? "A000601" : "A000501";
            SavePDFWithDataByFormId(formId, input);

            Receipt info2 = input.MapTo<Receipt>();            
            if (!string.IsNullOrEmpty(info.RSignatureImgPath)) {
                info2.RSignatureImgPath = info.RSignatureImgPath;
            }
            if (!string.IsNullOrEmpty(info.RUnsignPDFPath)) {
                info2.RUnsignPDFPath = info.RUnsignPDFPath;
            }

            OnBeforeUpdate(info2);

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                bool bl = await iService.UpdateAsync(info2, input.Id, conn, tran).ConfigureAwait(false);
                if (bl) {
                    tran.Commit();
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } else {
                    result.ErrMsg = ErrCode.err43002;
                    result.ErrCode = "43002";
                    result.Success = false;
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("更新領收據失敗", ex);
                result.ErrMsg = "更新領收據失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
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
                    bool deleteResult = await iService.DeleteBatchWhereAsync(where, conn, tran).ConfigureAwait(false);

                    if (deleteResult) {
                        eftran.Commit();
                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;

                        YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                        Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
                        foreach (string id in input.Ids) {                            
                            ReceiptOutputDto oDto = await iService.GetOutDtoAsync(id);
                            
                            if (oDto != null ) {
                                string basePath = @$"{sysSetting.ChaochiFilepath}\";
                                string signStatus = (string.IsNullOrEmpty(oDto.RSignedPDFPath)) ? "Unsigned" : "Signed";
                                string path = @$"{basePath}\Receipt\";

                                string formId = (!string.IsNullOrEmpty(oDto.RCate) && "內部收據".Equals(oDto.RCate)) ? "A000301" : "A000401";
                                string pdfPath = @$"{path}\{signStatus}\{oDto.ReceiptId}_{formId}.pdf";
                                var pdfFile = new FileInfo(pdfPath);
                                if (pdfFile.Exists) {
                                    pdfFile.Delete();
                                }

                                string signaturePath = @$"{path}\Signature\{oDto.ReceiptId}.jpg";
                                var signatureFile = new FileInfo(signaturePath);
                                if (signatureFile.Exists) {
                                    signatureFile.Delete();
                                }
                            }
                        }
                    } else {
                        result.ErrCode = "43003";
                        result.ErrMsg = "刪除承租人";
                        result.Success = false;
                    }
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("刪除領收據失敗", ex);
                    result.ErrMsg = "刪除領收據";
                    result.ErrCode = ErrCode.err43003;
                    result.Success = false;
                    // 觸發ExceptionHandlingAttribute.OnException
                    throw;
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 產生公會代管收據(B031301)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DownloadGuildPDF1")]
        [YuebonAuthorize("List")]
        [LogNotMethod]
        public async Task<IActionResult> DownloadGuildPDF1(ReceiptInputDto input)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

            MemoryStream PDFstream = new MemoryStream();
            using (var doc = PdfSharp.Pdf.IO.PdfReader.Open($"{sysSetting.ChaochiFilepath}TemplatePDF/B031301.pdf", PdfDocumentOpenMode.Modify)) {
                if (doc.AcroForm == null) {
                    doc.Close();
                    CommonResult result = new CommonResult();
                    result.ErrCode = "40001";
                    result.ErrMsg = "上傳PDF檔案不含任何標籤";
                    return ToJsonContent(result, true);
                } else {
                    // 處理租金起迄日
                    if (!string.IsNullOrEmpty(input.RDate) && !string.IsNullOrEmpty(input.PIRentStartDate) && !string.IsNullOrEmpty(input.PIRentEndDate)) {
                        string[] dates = input.RDate.Split("-");
                        if (dates.Length > 0 && dates.Length == 3) {
                            input.RDate_Y = dates[0];
                            input.RDate_M = dates[1];
                            input.RDate_D = dates[2];
                        }

                        string[] starts = input.PIRentStartDate.Split("-");
                        if (starts.Length > 0 && starts.Length == 3) {
                            input.PIRentStartDate_Y = starts[0];
                            input.PIRentStartDate_M = starts[1];
                            input.PIRentStartDate_D = starts[2];
                        }

                        string[] ends = input.PIRentEndDate.Split("-");
                        if (ends.Length > 0 && ends.Length == 3) {
                            input.PIRentEndDate_Y = ends[0];
                            input.PIRentEndDate_M = ends[1];
                            input.PIRentEndDate_D = ends[2];
                        }

                        input.PIRentMonth = "1";
                    }

                    PDFDataModel PDFDataModel = new PDFDataModel();
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (input.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(input, null))));
                    if (doc.AcroForm.Elements.ContainsKey("/NeedAppearances")) {
                        doc.AcroForm.Elements["/NeedAppearances"] = new PdfSharp.Pdf.PdfBoolean(true);
                    } else {
                        doc.AcroForm.Elements.Add("/NeedAppearances", new PdfSharp.Pdf.PdfBoolean(true));
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
                                doc.AcroForm.Fields[name].ReadOnly = false;
                                doc.AcroForm.Fields[name].Value = new PdfSharp.Pdf.PdfString(dictFinal.ContainsKey(name) ? (string)dictFinal[name] : "");
                            }
                        } else if (doc.AcroForm.Fields[name].Value is PdfItem) {
                            doc.AcroForm.Fields[name].ReadOnly = false;
                            if (dictFinal.ContainsKey(name) && (string)dictFinal[name] == "/Yes") {
                                ((PdfCheckBoxField)doc.AcroForm.Fields[name]).Checked = true;
                            } else {
                                ((PdfCheckBoxField)doc.AcroForm.Fields[name]).Checked = false;
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
        /// 產生公會包租收據(B031201)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DownloadGuildPDF2")]
        [YuebonAuthorize("List")]
        [LogNotMethod]
        public async Task<IActionResult> DownloadGuildPDF2(ReceiptInputDto input)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

            MemoryStream PDFstream = new MemoryStream();
            using (var doc = PdfReader.Open($"{sysSetting.ChaochiFilepath}TemplatePDF/B031201.pdf", PdfDocumentOpenMode.Modify)) {
                if (doc.AcroForm == null) {
                    doc.Close();
                    CommonResult result = new CommonResult();
                    result.ErrCode = "40001";
                    result.ErrMsg = "上傳PDF檔案不含任何標籤";
                    return ToJsonContent(result, true);
                } else {
                    // 處理租金起迄日
                    if (!string.IsNullOrEmpty(input.RDate) && !string.IsNullOrEmpty(input.PIRentStartDate) && !string.IsNullOrEmpty(input.PIRentEndDate)) {
                        string[] dates = input.RDate.Split("-");
                        if (dates.Length > 0 && dates.Length == 3) {
                            input.RDate_Y = dates[0];
                            input.RDate_M = dates[1];
                            input.RDate_D = dates[2];
                        }

                        string[] starts = input.PIRentStartDate.Split("-");
                        if (starts.Length > 0 && starts.Length == 3) {
                            input.PIRentStartDate_Y = starts[0];
                            input.PIRentStartDate_M = starts[1];
                            input.PIRentStartDate_D = starts[2];
                        }

                        string[] ends = input.PIRentEndDate.Split("-");
                        if (ends.Length > 0 && ends.Length == 3) {
                            input.PIRentEndDate_Y = ends[0];
                            input.PIRentEndDate_M = ends[1];
                            input.PIRentEndDate_D = ends[2];
                        }

                        input.PIRentMonth = "1";
                    }

                    PDFDataModel PDFDataModel = new PDFDataModel();
                    PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (input.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(input, null))));
                    if (doc.AcroForm.Elements.ContainsKey("/NeedAppearances")) {
                        doc.AcroForm.Elements["/NeedAppearances"] = new PdfSharp.Pdf.PdfBoolean(true);
                    } else {
                        doc.AcroForm.Elements.Add("/NeedAppearances", new PdfSharp.Pdf.PdfBoolean(true));
                    }

                    Dictionary<string, string> dictV = new Dictionary<string, string>();

                    Dictionary<string, object> dictFinal =
                            PDFDataModel.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .ToDictionary(prop => prop.Name, prop => prop.GetValue(PDFDataModel, null));

                    //設值
                    foreach (var name in doc.AcroForm.Fields.Names) {
                        if (doc.AcroForm.Fields[name] is PdfTextField) {
                            if (name == "FName" || name == "Vno" || name == "TBNO") {

                            } else {
                                doc.AcroForm.Fields[name].ReadOnly = false;
                                doc.AcroForm.Fields[name].Value = new PdfSharp.Pdf.PdfString(dictFinal.ContainsKey(name) ? (string)dictFinal[name] : "");
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

                    doc.Save(PDFstream);
                }
            }

            //https://stackoverflow.com/questions/34131326/using-mimemapping-in-asp-net-core
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
            return File(PDFstream, contentType ?? "image/jpeg");
        }

        /// <summary>
        /// 異步分頁查詢
        /// </summary>
        /// <param name="search">搜尋條件</param>
        /// <returns></returns>
        [HttpPost("FindWithPagerSearchAsync")]
        [YuebonAuthorize("List")]
        [LogNotMethod]
        public async Task<IActionResult> FindWithPagerSearchAsync(SearchReceiptModel search)
        {
            CommonResult<PageResult<ReceiptOutputDto>> result = new CommonResult<PageResult<ReceiptOutputDto>>();
            result.ResData = await iService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步設定領收據狀態
        /// </summary>
        /// <param name="id">主鍵Id</param>
        /// <param name="status">領收據狀態</param>
        [HttpPost("SetReceiptStatusAsync")]
        [YuebonAuthorize("Enable")]
        public async Task<IActionResult> SetReceiptStatusAsync(string id, string status)
        {
            CommonResult result = new CommonResult();

            bool setResult = await iService.SetReceiptStatusAsync(status, id, CurrentUser.UserId);
            if (setResult) {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } else {
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 取領收據簽名
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

        ///// <summary>
        ///// 下載PDF
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <returns></returns>
        //[HttpGet("DownloadPDF")]
        //[YuebonAuthorize("List")]
        //[LogNotMethod]
        //public IActionResult DownloadPDF(string filePath)
        //{
        //    string path = @$"{sysSetting.ChaochiFilepath}\ReceiptSingature\{DateTime.Now.ToString("yyyyMMdd")}";
        //    string filePath = $"{path}\{receiptId}_{formId}_{signStatus}.pdf"
        //    if (string.IsNullOrEmpty(filePath)) return new EmptyResult();
        //    YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
        //    Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

        //    var image = System.IO.File.OpenRead($"{sysSetting.ChaochiFilepath}{filePath}");
        //    string contentType;
        //    new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
        //    return File(image, contentType ?? "image/jpeg");
        //}

        /// <summary>
        /// 領收據簽名存檔
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string SaveReceiptSignature(string signature, string fileName)
        {
            string filePath = "";
            try {
                YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

                if (!string.IsNullOrEmpty(signature) && signature.IndexOf(',') > -1) {
                    string basePath = @$"{sysSetting.ChaochiFilepath}\";
                    //string dirPath = @$"ReceiptSingature\{DateTime.Now.ToString("yyyyMMdd")}\";
                    string dirPath = @"Receipt\Signature\";
                    filePath = dirPath + fileName;
                    var result = Directory.CreateDirectory(basePath + dirPath);
                    System.IO.File.WriteAllBytes(@$"{basePath}\{filePath}", Convert.FromBase64String(signature.Split(",")[1]));
                }
            } catch (Exception ex) {
                Log4NetHelper.Error("領收據簽名存檔寫入失敗", ex);
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }
            return filePath;
        }

        /// <summary>
        /// 異步分頁查詢
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private void SavePDFWithDataByFormId(string formId, ReceiptInputDto input)
        {
            //CommonResult result = new CommonResult();
            string receiptId = input.ReceiptId;
            string signStatus = !input.RSignatureIsEmpty ? "Signed" : "Unsigned";

            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

            //MemoryStream PDFstream = new MemoryStream();
            using (var doc = PdfReader.Open($"{sysSetting.ChaochiFilepath}TemplatePDF/{formId}.pdf", PdfDocumentOpenMode.Modify)) {
                if (doc.AcroForm == null) {
                    doc.Close();                    
                    //result.ErrCode = "40001";
                    //result.ErrMsg = "上傳PDF檔案不含任何標籤";
                    //return result;
                } else {
                    PDFDataModel PDFDataModel = GetData(formId, input);
                    if (doc.AcroForm.Elements.ContainsKey("/NeedAppearances")) {
                        doc.AcroForm.Elements["/NeedAppearances"] = new PdfSharp.Pdf.PdfBoolean(true);
                    } else {
                        doc.AcroForm.Elements.Add("/NeedAppearances", new PdfSharp.Pdf.PdfBoolean(true));
                    }

                    Dictionary<string, object> dictFinal =
                            PDFDataModel.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .ToDictionary(prop => prop.Name, prop => prop.GetValue(PDFDataModel, null));

                    string basePath = sysSetting.ChaochiFilepath;
                    //string dirPath = @$"ReceiptSingature\{DateTime.Now.ToString("yyyyMMdd")}";
                    string dirPath = @$"Receipt\{signStatus}";
                    string path = @$"{basePath}\{dirPath}";
                    if (!Directory.Exists(path)) {
                        Directory.CreateDirectory(path);
                    }

                    //設值
                    foreach (var name in doc.AcroForm.Fields.Names) {
                        if (!string.IsNullOrEmpty(input.RSignatureImgPath) && !input.RSignatureIsEmpty && name.StartsWith("Image")) {
                            try {
                                // 簽名圖檔                                
                                /* draw image on pdf */
                                PdfSharp.Pdf.PdfRectangle rect = doc.AcroForm.Fields["Image"].Elements.GetRectangle(PdfAnnotation.Keys.Rect);
                                XGraphics gfx = XGraphics.FromPdfPage(doc.Pages[0]);
                                XImage image = XImage.FromFile(@$"{basePath}\{input.RSignatureImgPath}");
                                double w = rect.Width, h = rect.Height;
                                ImageScaling(Math.Max(rect.Height, rect.Width) - 10, ref w, ref h);
                                gfx.DrawImage(image, rect.X1, doc.Pages[0].Height - rect.Y2, w, h);
                            } catch (Exception e) {
                                //Console.WriteLine(e.Message + " " + e.StackTrace);
                                Log4NetHelper.Error("領收據寫入PDF範本失敗", e);
                                // 觸發ExceptionHandlingAttribute.OnException
                                throw;
                            }

                        } else if (doc.AcroForm.Fields[name] is PdfTextField) {
                            if (name == "FName" || name == "Vno" || name == "TBNO") {

                            } else {
                                doc.AcroForm.Fields[name].ReadOnly = false;
                                doc.AcroForm.Fields[name].Value = new PdfSharp.Pdf.PdfString(dictFinal.ContainsKey(name) ? (string)dictFinal[name] : "");
                            }
                        } else if (doc.AcroForm.Fields[name].Value is PdfItem) {
                            doc.AcroForm.Fields[name].ReadOnly = false;
                            if (dictFinal.ContainsKey(name) && (string)dictFinal[name] == "/Yes") {
                                ((PdfCheckBoxField)doc.AcroForm.Fields[name]).Checked = true;
                            } else {
                                ((PdfCheckBoxField)doc.AcroForm.Fields[name]).Checked = false;
                            }
                        } else {
                            Console.WriteLine($"{name}: is not supported");
                        }
                    }

                    string PDFPath = @$"{dirPath}\{receiptId}_{formId}.pdf";
                    if (input.RSignatureIsEmpty) {
                        input.RUnsignPDFPath = PDFPath;
                    } else {
                        input.RSignedPDFPath = PDFPath;
                    }
                    string actualPath = @$"{path}\{receiptId}_{formId}.pdf";
                    FileStream fs = new FileStream(actualPath, FileMode.Create, FileAccess.Write);
                    doc.Save(fs);
                    doc.Close();
                    fs.Close();
                }
            }

            //return result;
        }

        private PDFDataModel GetData(string formId, ReceiptInputDto input)
        {
            PDFDataModel PDFDataModel = new PDFDataModel();

            if (input != null) {
                // 取BID
                if (!string.IsNullOrWhiteSpace(input.RAdd_1)) {
                    input.RAdd =
                        Utils.memergeAddFix(
                           input.RAdd_1,
                           input.RAdd_1_1,
                           input.RAdd_1_2,
                           input.RAdd_2,
                           input.RAdd_2_1,
                           input.RAdd_2_2,
                           input.RAdd_2_3,
                           input.RAdd_2_4,
                           input.RAdd_3,
                           input.RAdd_3_1,
                           input.RAdd_3_2,
                           input.RAdd_4,
                           input.RAdd_5,
                           input.RAdd_6,
                           input.RAdd_7,
                           input.RAdd_8,
                           input.RAdd_9
                        );

                    string sqlWhere = "BAdd='" + input.RAdd + "'";
                    Building Building_DB = IBuildingRepository.GetWhere(sqlWhere);

                    if (Building_DB != null) {
                        input.BID = Building_DB.BID;
                    }
                }

                // 收費項目
                StringBuilder payItemDetail = new StringBuilder();
                StringBuilder payItemCost = new StringBuilder();
                int itemCount = 1;

                // 租金
                if (!string.IsNullOrEmpty(input.PIRent)) {
                    payItemDetail.Append($"{itemCount}.租金-租期：{input.PIRentStartDate_Y}年{input.PIRentStartDate_M}月{input.PIRentStartDate_D}日至{input.PIRentEndDate_Y}年{input.PIRentEndDate_M}月{input.PIRentEndDate_D}日").Append("\n");
                    payItemCost.Append($"{input.PIRent}元").Append("\n");
                    itemCount++;
                }

                // 押金
                if (!string.IsNullOrEmpty(input.PIDeposit)) {
                    payItemDetail.Append($"{itemCount}.押金-{input.PIDepositDate_Y}年{input.PIDepositDate_M}月{input.PIRentStartDate_D}日已收{input.PIDepositActual}元，尚欠{input.PIDepositOwe}元未繳齊").Append("\n");
                    payItemCost.Append($"{input.PIDeposit}元").Append("\n");
                    itemCount++;
                }

                // 大樓管理費
                if (!string.IsNullOrEmpty(input.PIManagementFee)) {
                    payItemDetail.Append($"{itemCount}.大樓管理費-{input.PIManagementFeeMonth}月份").Append("\n");
                    payItemCost.Append($"{input.PIManagementFee}元").Append("\n");
                    itemCount++;
                }

                // 仲介服務費
                if (!string.IsNullOrEmpty(input.PIServiceFee)) {
                    payItemDetail.Append($"{itemCount}.仲介服務費").Append("\n");
                    payItemCost.Append($"{input.PIServiceFee}元").Append("\n");
                    itemCount++;
                }

                // 顧問費
                if (!string.IsNullOrEmpty(input.PIConsultantFee)) {
                    payItemDetail.Append($"{itemCount}.顧問費-收取{input.PIConsultantFeeMonth}月").Append("\n");
                    payItemCost.Append($"{input.PIConsultantFee}元").Append("\n");
                    itemCount++;
                }

                // 設備費用
                if (!string.IsNullOrEmpty(input.PIEquipmentCost)) {
                    payItemDetail.Append($"{itemCount}.設備費用-購買：{input.PIEquipmentNote}").Append("\n");
                    payItemCost.Append($"{input.PIEquipmentCost}元").Append("\n");
                    itemCount++;
                }

                // 電費
                if (!string.IsNullOrEmpty(input.PIElectricityBill)) {
                    payItemDetail.Append($"{itemCount}.電費").Append("\n");
                    payItemCost.Append($"{input.PIElectricityBill}元").Append("\n");
                    itemCount++;
                }

                // 水雜費
                if (!string.IsNullOrEmpty(input.PIWaterBill)) {
                    payItemDetail.Append($"{itemCount}.水雜費").Append("\n");
                    payItemCost.Append($"{input.PIWaterBill}元").Append("\n");
                    itemCount++;
                }

                // 瓦斯費
                if (!string.IsNullOrEmpty(input.PIGasFee)) {
                    payItemDetail.Append($"{itemCount}.瓦斯費").Append("\n");
                    payItemCost.Append($"{input.PIGasFee}元").Append("\n");
                    itemCount++;
                }

                // 垃圾費
                if (!string.IsNullOrEmpty(input.PITrashFee)) {
                    payItemDetail.Append($"{itemCount}.垃圾費").Append("\n");
                    payItemCost.Append($"{input.PITrashFee}元").Append("\n");
                    itemCount++;
                }

                // 清潔費
                if (!string.IsNullOrEmpty(input.PICleaningFee)) {
                    payItemDetail.Append($"{itemCount}.清潔費").Append("\n");
                    payItemCost.Append($"{input.PICleaningFee}元").Append("\n");
                    itemCount++;
                }

                // 行政處理費
                if (!string.IsNullOrEmpty(input.PIAdministrativeFee)) {
                    payItemDetail.Append($"{itemCount}.行政處理費").Append("\n");
                    payItemCost.Append($"{input.PIAdministrativeFee}元").Append("\n");
                    itemCount++;
                }

                // 管理服務費
                if (!string.IsNullOrEmpty(input.PIManagementServiceFee)) {
                    payItemDetail.Append($"{itemCount}.管理服務費").Append("\n");
                    payItemCost.Append($"{input.PIManagementServiceFee}元").Append("\n");
                    itemCount++;
                }

                // 其他費用
                if (!string.IsNullOrEmpty(input.PIMiscellaneousFee)) {
                    payItemDetail.Append($"{itemCount}.{input.PIMiscellaneousFeeNote}").Append("\n");
                    payItemCost.Append($"{input.PIMiscellaneousFee}元").Append("\n");                    
                }

                input.PIDetail = payItemDetail.ToString();
                input.PIDetailCost = payItemCost.ToString();

                PDFDataModel = Merger.DictCloneAndMerge<PDFDataModel>(PDFDataModel, (input.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(input, null))));
            }

            return PDFDataModel;
        }

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
    }
}
