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
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.IServices;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.AspNetCore.Mvc;
using System.Data;
using Yuebon.Commons.IDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.StaticFiles;
using Senparc.CO2NET.Extensions;
using Yuebon.Commons.Json;
using Microsoft.AspNetCore.Http;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using Yuebon.Commons.Core.Dtos;
using System.Linq;
using Yuebon.Commons.Extensions;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 合約表單接口
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class ContractformController : AreaApiController<Contractform, ContractformOutputDto,ContractformInputDto,IContractformService,string>
    {
        private readonly IDbContextCore ybContext;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_ybContext"></param>
        public ContractformController(IContractformService _iService, IDbContextCore _ybContext) : base(_iService)
        {
            iService = _iService;
            ybContext = _ybContext;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Contractform info)
        {
            info.Id = GuidUtils.CreateNo();
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
        protected override void OnBeforeUpdate(Contractform info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 異步新增數據
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(ContractformInputDto input)
        {
            CommonResult result = new CommonResult();

            if (!string.IsNullOrWhiteSpace(input.FormId)) {
                string where = string.Format("FormId = '{0}'", input.FormId);
                Contractform contractForm = await iService.GetWhereAsync(where);
                if (contractForm != null) {
                    result.ErrMsg = "表單編號不能重復";
                    return ToJsonContent(result);
                }
            } else {
                result.ErrMsg = "表單編號不能為空";
                return ToJsonContent(result);
            }
            Contractform info = input.MapTo<Contractform>();
            OnBeforeInsert(info);

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                await iService.InsertAsync(info, conn, tran).ConfigureAwait(false);

                eftran.Commit();
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
                result.Success = true;
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("新增合約表單失敗", ex);
                result.ErrMsg = "新增合約表單失敗";
                result.ErrCode = ErrCode.err43001;
                result.Success = false;
            }
            
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步更新數據
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(ContractformInputDto input)
        {
            CommonResult result = new CommonResult();

            if (!string.IsNullOrWhiteSpace(input.FormId)) {
                string where = string.Format(" FormId = '{0}' and id != '{1}' ", input.FormId, input.Id);
                Contractform contractForm = await iService.GetWhereAsync(where);
                if (contractForm != null) {
                    result.ErrMsg = "表單編號不能重復";
                    return ToJsonContent(result);
                }
            } else {
                result.ErrMsg = "表單編號不能為空";
                return ToJsonContent(result);
            }

            Contractform info_DB = await iService.GetAsync(input.Id);
            Contractform info = input.MapTo(info_DB);

            OnBeforeUpdate(info);

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                await iService.UpdateAsync(info, input.Id, conn, tran);

                eftran.Commit();
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
                result.Success = true;
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("更新合約表單失敗", ex);
                result.ErrMsg = "更新合約表單失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
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
                    bool bl = await iService.DeleteBatchWhereAsync(where, conn, tran).ConfigureAwait(false);
                    
                    if (bl) {
                        eftran.Commit();
                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;
                    } else {
                        result.ErrCode = "43003";
                        result.ErrMsg = "刪除合約表單失敗";
                        result.Success = false;
                    }
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("刪除合約表單失敗", ex);
                    result.ErrMsg = "刪除合約表單失敗";
                    result.ErrCode = ErrCode.err43003;
                    result.Success = false;
                }
            }
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

                if (!dictV.ContainsKey("CCate")) {
                    result.ErrCode = "40001";
                    result.ErrMsg = "上傳PDF檔案不含標籤[CCate]";
                    return ToJsonContent(result, true);
                } else {
                    if (dictV["CCate"] != FormId) {
                        result.ErrCode = "40001";
                        result.ErrMsg = $"上傳PDF檔案標籤[CCate]不為{FormId}";
                        return ToJsonContent(result, true);
                    }
                }

                if (!dictV.ContainsKey("Vno")) {
                    result.ErrCode = "40001";
                    result.ErrMsg = "上傳PDF檔案不含標籤[Vno]";
                    return ToJsonContent(result, true);
                } else {
                    string where = string.Format("FormId = '{0}'", FormId);
                    Contractform Contractform_DB = await iService.GetWhereAsync(where);
                    if (Contractform_DB == null) {
                        result.ErrCode = "40001";
                        result.ErrMsg = $"FormId:{FormId}對應資料尚未建立!";
                        return ToJsonContent(result, true);
                    }

                    if (dictV["Vno"] != Contractform_DB.Vno) {
                        result.ErrCode = "40001";
                        result.ErrMsg = $"上傳PDF檔案[版本號]標籤[Vno]不為{Contractform_DB.Vno}";
                        return ToJsonContent(result, true);
                    }
                }

                Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
                Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
                //https://stackoverflow.com/questions/13865718/directory-getfiles-get-todays-files-only
                //DirectoryInfo infoD = new DirectoryInfo(Path.Combine("D:/zzz/", "image/"));
                //DirectoryInfo infoD = new DirectoryInfo(sysSetting.ChaochiFilepath);
                string uploadPath = sysSetting.ChaochiFilepath + "TemplatePDF/";//這邊之後加工需要不同的目錄

                result.ResData = Utils.AddFile(filelist[0], uploadPath, FormId);

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
        /// 下載合約表單PDF範本
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        [HttpGet("downloadPDFTemplateByFormId")]
        [YuebonAuthorize("List")]
        [LogNotMethod]
        public IActionResult DownloadPDFTemplateByFormId(string formId)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            
            var pdf = System.IO.File.OpenRead($"{sysSetting.ChaochiFilepath}TemplatePDF/{formId}.pdf");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }
    }
}