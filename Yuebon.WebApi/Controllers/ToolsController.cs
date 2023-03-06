using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.AspNetCore.ViewModel;
using Yuebon.Chaochi.Dtos;
using Yuebon.Commons.Cache;
using Yuebon.Commons.CodeGenerator;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Security.Models;

namespace Yuebon.WebApi.Controllers
{
    /// <summary>
    /// 代碼生成器
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ToolsController : ApiController
    {
        /// <summary>
        ///  上傳PDF
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("UploadPDFTemplate")]
        [NoSignRequired]
        public async Task<IActionResult> UploadPDFTemplateAsync([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();
            var keyWords = new List<string>() { "Image", "btnCheck" };

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            try {
                string FormId = "";
                if (formCollection.ContainsKey("FormId")) {
                    FormId = formCollection["FormId"];
                }

                //Dictionary<string, string> dictV = new Dictionary<string, string>();
                List<string> fieldsName = new List<string>();
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
                                if (!keyWords.Any(x => x == name)) {
                                    fieldsName.Add(name);
                                }
                            }
                        }
                    }
                }

                var pdfDataModel = typeof(PDFDataModel);

                var publicProperties = pdfDataModel.GetProperties();

                var output = new PDFDataModel();

                string resultString = "";

                foreach (var item in fieldsName) {
                    if (!publicProperties.Any(x => x.Name == item)) {
                        resultString += $"{item}，";
                    }
                }
                if (resultString.Contains("，")) {

                    resultString = resultString.Substring(0, resultString.Length - 1);
                }
                result.ResData = resultString;
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
        ///  上傳合約PDF
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("UploadContractPDFTemplate")]
        [NoSignRequired]
        public async Task<IActionResult> UploadContractPDFTemplateAsync([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();
            var keyWords = new List<string>() { "Image", "btnCheck" };

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            try {
                string FormId = "";
                if (formCollection.ContainsKey("FormId")) {
                    FormId = formCollection["FormId"];
                }

                //Dictionary<string, string> dictV = new Dictionary<string, string>();
                List<string> fieldsName = new List<string>();
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
                                if (!keyWords.Any(x => x == name)) {
                                    fieldsName.Add(name);
                                }
                            }
                        }
                    }
                }

                var pdfDataModel = typeof(ContractPDFDataModel);

                var publicProperties = pdfDataModel.GetProperties();

                var output = new ContractPDFDataModel();

                string resultString = "";

                foreach (var item in fieldsName) {
                    if (!publicProperties.Any(x => x.Name == item)) {
                        resultString += $"{item}，";
                    }
                }
                if (resultString.Contains("，")) {

                    resultString = resultString.Substring(0, resultString.Length - 1);
                }
                result.ResData = resultString;
                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
            }
            return ToJsonContent(result);
        }
    }
}