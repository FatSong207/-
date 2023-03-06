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
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.IServices;
using Yuebon.AspNetCore.Mvc;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Commons.IDbContext;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Yuebon.AspNetCore.Mvc.Filter;
using Microsoft.AspNetCore.Http;
using Yuebon.Chaochi.Core.Models;
using System.IO;
using Yuebon.Commons.Json;
using Yuebon.Chaochi.Core.Helpers;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class CustomerRCController : AreaApiController<CustomerRC, CustomerRCOutputDto,CustomerRCInputDto,ICustomerRCService,string>
    {
        private readonly IDbContextCore _ybContext;
        private readonly IHistoryFormRCService _historyFormRCService;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="ybContext"></param>
        /// <param name="historyFormRCService"></param>
        public CustomerRCController(ICustomerRCService _iService, IDbContextCore ybContext,IHistoryFormRCService historyFormRCService) : base(_iService)
        {
            iService = _iService;
            _ybContext = ybContext;
            _historyFormRCService = historyFormRCService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(CustomerRC info)
        {
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
            info.LastModifyUserId = CurrentUser.UserId;
            info.CreatorUserOrgId = CurrentUser.OrganizeId;
            info.CreatorUserDeptId = CurrentUser.DeptId;
            info.RCFRelation = "本人";
        }
        
        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(CustomerRC info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(CustomerRC info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }

        /// <summary>
        /// 異步分頁查詢
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerSearchAsync")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> FindWithPagerSearchAsync(SearchCustomerRCModel search)
        {
            CommonResult<PageResult<CustomerRCOutputDto>> result = new CommonResult<PageResult<CustomerRCOutputDto>>();
            result.ResData = await iService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 新增承租人
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(CustomerRCInputDto tinfo)
        {
            var result = new CommonResult();
            var customerRC = tinfo.MapTo<CustomerRC>();
            var isExists = await iService.GetWhereAsync($" RCID = '{customerRC.RCID}'");
            if (isExists != null) {
                result.Success = false;
                result.ErrMsg = "此身份證字號／居留證號／統一編號已存在";
                return ToJsonContent(result);
            } else {
                using IDbConnection conn = _ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = _ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();

                try {
                    HandleSpecificData.HandleSpecificDataRC(customerRC, tinfo);
                    //HandleSpecialData(customerRN, tinfo);
                    OnBeforeInsert(customerRC);
                    long ln = await iService.InsertAsync(customerRC, conn, tran).ConfigureAwait(false);

                    if (ln > 0) {
                        eftran.Commit();
                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;
                    } else {
                        result.Success = false;
                        result.ErrMsg = ErrCode.err43001;
                        result.ErrCode = "43001";
                    }
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("新增承租人失敗", ex);
                    result.ErrMsg = "新增承租人失敗";
                    result.ErrCode = ErrCode.err43001;
                    result.Success = false;
                    // 觸發ExceptionHandlingAttribute.OnException
                    throw;
                }

                return ToJsonContent(result);
            }

        }

        /// <summary>
        /// 異步更新資料
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(CustomerRCInputDto inInfo)
        {
            var result = new CommonResult();

            var cRC = iService.Get(inInfo.Id);
            var info = inInfo.MapTo(cRC);

            using IDbConnection conn = _ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = _ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                HandleSpecificData.HandleSpecificDataRC(info, inInfo);
                OnBeforeUpdate(info);
                info.RCID = cRC.RCID;
                var isSuccess = await iService.UpdateAsync(info, inInfo.Id, conn, tran);
                if (isSuccess) {
                    tran.Commit();
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.Success = false;
                    result.ErrMsg = ErrCode.err43002;
                    result.ErrCode = "43002";
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("更新承租人失敗", ex);
                result.ErrMsg = "更新承租人失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步更新同住人頁籤
        /// </summary>
        /// <param name="rRFInputDto"></param>
        /// <returns></returns>
        [HttpPost("UpdateRCFAsync")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> UpdateRCFAsync(RCFInputDto rRFInputDto)
        {
            var result = new CommonResult();

            var cRC = iService.Get(rRFInputDto.Id);
            var info = rRFInputDto.MapTo(cRC);

            using IDbConnection conn = _ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = _ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                HandleSpecificData.HandleSpecificDataRC(info, rRFInputDto);
                OnBeforeUpdate(info);
                var isSuccess = await iService.UpdateAsync(info, rRFInputDto.Id, conn, tran);
                if (isSuccess) {
                    tran.Commit();
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.Success = false;
                    result.ErrMsg = ErrCode.err43002;
                    result.ErrCode = "43002";
                }
            } catch (Exception ex) {
                eftran.Rollback();
                Log4NetHelper.Error("更新同住人失敗", ex);
                result.ErrMsg = "更新同住人失敗";
                result.ErrCode = ErrCode.err43002;
                result.Success = false;
                // 觸發ExceptionHandlingAttribute.OnException
                throw;
            }

            return ToJsonContent(result);
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
            try {
                var RNID = iService.Get(formCollection["sid"]).RCID;
                var Addres = Add(filelist[0], belongApp, belongAppId, RNID);
                result.ResData = Addres;
                var historyform = new HistoryFormRC() {
                    IDNo = RNID,
                    FormName = string.IsNullOrEmpty(formCollection["formname"]) ? Addres.FileName.Remove(0, 14) : formCollection["formname"],
                    Note = formCollection["note"],
                    UploadTime = DateTime.Now,
                    FileName = Addres.FileName,
                    CreatorUserId = CurrentUser.UserId
                };
                _historyFormRCService.InsertAsync(historyform);
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
        /// <param name="LNID"></param>
        /// <returns></returns>
        private Yuebon.Security.Dtos.UploadFileResultOuputDto Add(IFormFile file, string belongApp, string belongAppId, string LNID)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = file.FileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    var filenameExt = UploadFile(fileName, data, LNID);

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
        /// <param name="RNID"></param>
        private string UploadFile(string fileName, byte[] fileBuffers, string RNID)
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
            if (System.IO.File.Exists("")) {
            }

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            string uploadPath = @$"{path}historyPDF\CustomerR\RC\";
            if (!(System.IO.File.Exists(uploadPath + $"{RNID}"))) {
                Directory.CreateDirectory(@$"{uploadPath}{RNID}\");
            }
            uploadPath = uploadPath + RNID + "\\";
            //string ext = Path.GetExtension(fileName).ToLower();
            //string newName = GuidUtils.CreateNo();
            //string newfileName = newName + ext;

            using (var fs = new FileStream(uploadPath + fileName, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }
            return fileName;
        }
    }
}