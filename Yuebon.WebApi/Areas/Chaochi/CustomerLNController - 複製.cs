using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using Yuebon.WebApi.Areas.Security.Models;
using Yuebon.Commons.Dtos;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Globalization;
using System.Collections.Generic;
using Yuebon.Chaochi.Core.Models;

namespace Yuebon.WebApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 用戶接口
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/Chaochi/[controller]")]
    public class CustomerLNController : AreaApiController<CustomerLN, CustomerLNOutputDto, CustomerLNInputDto, ICustomerLNService, string>
    {
        private Yuebon.Security.IServices.IOrganizeService organizeService;
        private Yuebon.Security.IServices.IRoleService roleService;
        private Yuebon.Security.IServices.IUserLogOnService userLogOnService;
        private readonly IHistoryFormService _historyFormService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_organizeService"></param>
        /// <param name="_roleService"></param>
        /// <param name="_userLogOnService"></param>
        public CustomerLNController(ICustomerLNService _iService,IHistoryFormService historyFormService, Yuebon.Security.IServices.IOrganizeService _organizeService, Yuebon.Security.IServices.IRoleService _roleService, Yuebon.Security.IServices.IUserLogOnService _userLogOnService) : base(_iService)
        {
            iService = _iService;
            organizeService = _organizeService;
            roleService = _roleService;
            userLogOnService = _userLogOnService;
            _historyFormService = historyFormService;
        }

        /// <summary>
        /// 異步分頁查詢
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerSearchAsync")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> FindWithPagerSearchAsync(SearchCustomerLNModel search)
        {
            CommonResult<PageResult<CustomerLNOutputDto>> result = new CommonResult<PageResult<CustomerLNOutputDto>>();
            result.ResData = await iService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override Task<IActionResult> UpdateAsync(CustomerLNInputDto inInfo)
        {
            var result = new CommonResult();
            var cLN = iService.Get(inInfo.Id);
            cLN.LNName = inInfo.LNName;
            cLN.LNID = inInfo.LNID;
            cLN.LNBirthday = inInfo.LNbirthday;
            cLN.LNGender1 = inInfo.LNGender1;
            cLN.LNGender2 = inInfo.LNGender2;
            cLN.LNTel_1 = inInfo.LNTel_1;
            cLN.LNTel_2 = inInfo.LNTel_2;
            cLN.LNTel = inInfo.LNTel_1 + inInfo.LNTel_2;
            cLN.LNCell = inInfo.LNCell;
            cLN.LNTelN_1 = inInfo.LNTelN_1;
            cLN.LNTelN_2 = inInfo.LNTelN_2;
            cLN.LNTelN = inInfo.LNTelN_1 + inInfo.LNTelN_2;
            cLN.LNMail = inInfo.LNMail;
            cLN.LNAdd_1 = inInfo.LNAdd_1;
            cLN.LNAdd_1_1 = inInfo.LNAdd_1_1;
            cLN.LNAdd_1_2 = inInfo.LNAdd_1_2;
            cLN.LNAdd_2 = inInfo.LNAdd_2;
            cLN.LNAdd_2_1 = inInfo.LNAdd_2_1;
            cLN.LNAdd_2_2 = inInfo.LNAdd_2_2;
            cLN.LNAdd_2_3 = inInfo.LNAdd_2_3;
            cLN.LNAdd_2_4 = inInfo.LNAdd_2_4;
            cLN.LNAdd_3 = inInfo.LNAdd_3;
            cLN.LNAdd_3_1 = inInfo.LNAdd_3_1;
            cLN.LNAdd_3_2 = inInfo.LNAdd_3_2;
            cLN.LNAdd_4 = inInfo.LNAdd_4;
            cLN.LNAdd_5 = inInfo.LNAdd_5;
            cLN.LNAdd_6 = inInfo.LNAdd_6;
            cLN.LNAdd_7 = inInfo.LNAdd_7;
            cLN.LNAdd_8 = inInfo.LNAdd_8;
            cLN.LNAdd_9 = inInfo.LNAdd_9;
            cLN.LNAdd = Utils.memergeAddFix(
                            inInfo.LNAdd_1, 
                            inInfo.LNAdd_1_1, 
                            inInfo.LNAdd_1_2,
                            inInfo.LNAdd_2, 
                            inInfo.LNAdd_2_1,
                            inInfo.LNAdd_2_2, 
                            inInfo.LNAdd_2_3,
                            inInfo.LNAdd_2_4,
                            inInfo.LNAdd_3, 
                            inInfo.LNAdd_3_1,
                            inInfo.LNAdd_3_2,
                            inInfo.LNAdd_4, 
                            inInfo.LNAdd_5, 
                            inInfo.LNAdd_6,
                            inInfo.LNAdd_7,
                            inInfo.LNAdd_8, 
                            inInfo.LNAdd_9);
            cLN.LNAddSame = inInfo.LNAddSame;
            cLN.CreatorTime = inInfo.CreatorTime;
            cLN.CreatorUserId = inInfo.CreatorUserId;
            cLN.LastModifyTime = inInfo.LastModifyTime;
            cLN.LastModifyUserId = inInfo.LastModifyUserId;
            if (inInfo.LNAddSame == "/Off")
            {
                cLN.LNAddC_1 = inInfo.LNAddC_1;
                cLN.LNAddC_1_1 = inInfo.LNAddC_1_1;
                cLN.LNAddC_1_2 = inInfo.LNAddC_1_2;
                cLN.LNAddC_2 = inInfo.LNAddC_2;
                cLN.LNAddC_2_1 = inInfo.LNAddC_2_1;
                cLN.LNAddC_2_2 = inInfo.LNAddC_2_2;
                cLN.LNAddC_2_3 = inInfo.LNAddC_2_3;
                cLN.LNAddC_2_4 = inInfo.LNAddC_2_4;
                cLN.LNAddC_3 = inInfo.LNAddC_3;
                cLN.LNAddC_3_1 = inInfo.LNAddC_3_1;
                cLN.LNAddC_3_2 = inInfo.LNAddC_3_2;
                cLN.LNAddC_4 = inInfo.LNAddC_4;
                cLN.LNAddC_5 = inInfo.LNAddC_5;
                cLN.LNAddC_6 = inInfo.LNAddC_6;
                cLN.LNAddC_7 = inInfo.LNAddC_7;
                cLN.LNAddC_8 = inInfo.LNAddC_8;
                cLN.LNAddC_9 = inInfo.LNAddC_9;
                cLN.LNAddC = Utils.memergeAddFix(
                                inInfo.LNAddC_1,
                                inInfo.LNAddC_1_1,
                                inInfo.LNAddC_1_2,
                                inInfo.LNAddC_2,
                                inInfo.LNAddC_2_1,
                                inInfo.LNAddC_2_2,
                                inInfo.LNAddC_2_3,
                                inInfo.LNAddC_2_4,
                                inInfo.LNAddC_3,
                                inInfo.LNAddC_3_1,
                                inInfo.LNAddC_3_2,
                                inInfo.LNAddC_4,
                                inInfo.LNAddC_5,
                                inInfo.LNAddC_6,
                                inInfo.LNAddC_7,
                                inInfo.LNAddC_8,
                                inInfo.LNAddC_9);
            }
            else
            {
                cLN.LNAddC_1 = inInfo.LNAdd_1;
                cLN.LNAddC_1_1 = inInfo.LNAdd_1_1;
                cLN.LNAddC_1_2 = inInfo.LNAdd_1_2;
                cLN.LNAddC_2 = inInfo.LNAdd_2;
                cLN.LNAddC_2_1 = inInfo.LNAdd_2_1;
                cLN.LNAddC_2_2 = inInfo.LNAdd_2_2;
                cLN.LNAddC_2_3 = inInfo.LNAdd_2_3;
                cLN.LNAddC_2_4 = inInfo.LNAdd_2_4;
                cLN.LNAddC_3 = inInfo.LNAdd_3;
                cLN.LNAddC_3_1 = inInfo.LNAdd_3_1;
                cLN.LNAddC_3_2 = inInfo.LNAdd_3_2;
                cLN.LNAddC_4 = inInfo.LNAdd_4;
                cLN.LNAddC_5 = inInfo.LNAdd_5;
                cLN.LNAddC_6 = inInfo.LNAdd_6;
                cLN.LNAddC_7 = inInfo.LNAdd_7;
                cLN.LNAddC_8 = inInfo.LNAdd_8;
                cLN.LNAddC_9 = inInfo.LNAdd_9;
                cLN.LNAddC = Utils.memergeAddFix(
                                inInfo.LNAdd_1, 
                                inInfo.LNAdd_1_1, 
                                inInfo.LNAdd_1_2, 
                                inInfo.LNAdd_2, 
                                inInfo.LNAdd_2_1, 
                                inInfo.LNAdd_2_2, 
                                inInfo.LNAdd_2_3, 
                                inInfo.LNAdd_2_4,
                                inInfo.LNAdd_3, 
                                inInfo.LNAdd_3_1, 
                                inInfo.LNAdd_3_2, 
                                inInfo.LNAdd_4, 
                                inInfo.LNAdd_5, 
                                inInfo.LNAdd_6, 
                                inInfo.LNAdd_7, 
                                inInfo.LNAdd_8, 
                                inInfo.LNAdd_9);
            }
            var isSuccess = iService.Update(cLN, inInfo.Id);
            if (isSuccess)
            {
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.Success = false;
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return Task.FromResult(ToJsonContent(result));
        }

        /// <summary>
        ///  單文件上傳接口
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("ImgUpload")]
        [NoSignRequired]
        public IActionResult ImgUpload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            //string _fileName = filelist[0].FileName;
            try
            {
                var Addres = Add(filelist[0], belongApp, belongAppId);
                result.ResData = Addres;
                var historyform = new HistoryForm()
                {
                    IDNo = iService.Get(formCollection["id"]).LNID,
                    FormName = formCollection["formname"],
                    Note = formCollection["note"],
                    UploadTime = DateTime.Now,
                    FileName = Addres.FileName
                };
                _historyFormService.InsertAsync(historyform);
                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                throw ex;
            }
            return ToJsonContent(result);
        }

        public class send
        {
            public string formname { get; set; }
            public string note { get; set; } 
        }

        /// <summary>
        /// 單個上傳文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="belongApp"></param>
        /// <param name="belongAppId"></param>
        /// <returns></returns>
        private Yuebon.Security.Dtos.UploadFileResultOuputDto Add(IFormFile file, string belongApp, string belongAppId)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760)
            {
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    var fileName = string.Empty;
                    fileName = file.FileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    var filenameExt= UploadFile(fileName, data);

                    Yuebon.Security.Dtos.UploadFileResultOuputDto uploadFileResultOuputDto = new Yuebon.Security.Dtos.UploadFileResultOuputDto
                    {
                        FileName = filenameExt,
                    };
                    return uploadFileResultOuputDto;
                }
            }
            else
            {
                Log4NetHelper.Info("文件過大");
                throw new Exception("文件過大");
            }
        }

        /// <summary>
        /// 實現文件上傳到服務器保存，并生成縮略圖
        /// </summary>
        /// <param name="fileName">文件名稱</param>
        /// <param name="fileBuffers">文件字節流</param>
        private string UploadFile(string fileName, byte[] fileBuffers)
        {

            //判斷文件是否為空
            if (string.IsNullOrEmpty(fileName))
            {
                Log4NetHelper.Info("文件名不能為空");
                throw new Exception("文件名不能為空");
            }

            //判斷文件是否為空
            if (fileBuffers.Length < 1)
            {
                Log4NetHelper.Info("文件不能為空");
                throw new Exception("文件不能為空");
            }

            fileName = DateTime.Now.ToString("yyyyMMddHHmm")+fileName;

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            //Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

            //string uploadPath = sysSetting.ChaochiFilepath;//這邊之後加工需要不同的目錄
            string uploadPath = @"D:\zzz\historyPDF\CustomerL\LN\";
            //string ext = Path.GetExtension(fileName).ToLower();
            //string newName = GuidUtils.CreateNo();
            //string newfileName = newName + ext;

            using (var fs = new FileStream(uploadPath + fileName, FileMode.Create))
            {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }
            return fileName;
        }

    }
}