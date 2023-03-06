using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Chaochi.Core.Helpers;
using Yuebon.Chaochi.Core.IServices;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;

namespace Yuebon.WebApi.Areas.Chaochi
{
    /// <summary>
    /// 房客-自然人接口
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class CustomerRNController : AreaApiController<CustomerRN, CustomerRNOutputDto, CustomerRNInputDto, ICustomerRNService, string>
    {
        private readonly IHistoryFormRNService _historyFormService;
        private readonly IDbContextCore ybContext;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="historyFormService"></param>
        /// <param name="_ybContext"></param>
        public CustomerRNController(ICustomerRNService _iService,
            IHistoryFormRNService historyFormService,
            IDbContextCore _ybContext) : base(_iService)
        {
            iService = _iService;
            _historyFormService = historyFormService;
            ybContext = _ybContext;
        }
        /// <summary>
        /// 在新增資料前對資料的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeInsert(CustomerRN info)
        {
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
            info.LastModifyUserId = CurrentUser.UserId;
            info.CreatorUserOrgId = CurrentUser.OrganizeId;
            info.CreatorUserDeptId = CurrentUser.DeptId;
            info.RNFRelation = "本人";
            //_landLordBelongingService.Insert(new LandLordBelonging {
            //    LId = info.LNID,
            //    SalesId = CurrentUser.UserId,
            //});
        }

        /// <summary>
        /// 在更新資料前對資料的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(CustomerRN info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
            //info.CreatorUserOrgId = CurrentUser.OrganizeId;
            //info.CreatorUserDeptId = CurrentUser.DeptId;
        }

        /// <summary>
        /// 在軟刪除資料前對資料的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(CustomerRN info)
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
        public async Task<IActionResult> FindWithPagerSearchAsync(SearchCustomerRNModel search)
        {
            CommonResult<PageResult<CustomerRNOutputDto>> result = new CommonResult<PageResult<CustomerRNOutputDto>>();
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
        public override async Task<IActionResult> InsertAsync(CustomerRNInputDto tinfo)
        {
            var result = new CommonResult();
            //if (string.IsNullOrEmpty(tinfo.RNAdd_1) || string.IsNullOrEmpty(tinfo.RNAdd_2) || string.IsNullOrEmpty(tinfo.RNAdd_3)) {
            //    result.Success = false;
            //    result.ErrCode = ErrCode.err43002;
            //    result.ErrMsg = "請輸入完整戶籍地址";
            //    return ToJsonContent(result);
            //}
            var customerRN = tinfo.MapTo<CustomerRN>();
            var isExists = await iService.GetCustomerByRNID(customerRN.RNID);
            if (isExists != null) {
                result.Success = false;
                result.ErrMsg = "此身份證字號／居留證號／統一編號已存在";
                return ToJsonContent(result);
            } else {
                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();

                try {
                    HandleSpecificData.HandleSpecificDataRN(customerRN, tinfo);
                    //HandleSpecialData(customerRN, tinfo);
                    OnBeforeInsert(customerRN);
                    long ln = await iService.InsertAsync(customerRN, conn, tran).ConfigureAwait(false);

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
        public override async Task<IActionResult> UpdateAsync(CustomerRNInputDto inInfo)
        {
            var result = new CommonResult();
            if (string.IsNullOrEmpty(inInfo.RNAdd_1) || string.IsNullOrEmpty(inInfo.RNAdd_2) || string.IsNullOrEmpty(inInfo.RNAdd_3)) {
                result.Success = false;
                result.ErrCode = ErrCode.err43002;
                result.ErrMsg = "請輸入完整戶籍地址";
                return ToJsonContent(result);
            }
            var cRN = iService.Get(inInfo.Id);
            var info = inInfo.MapTo(cRN);

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                HandleSpecificData.HandleSpecificDataRN(info, inInfo);
                //HandleSpecialData(info, inInfo);
                OnBeforeUpdate(info);
                info.RNID = cRN.RNID;
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
        /// <param name="rFNInputDto"></param>
        /// <returns></returns>
        [HttpPost("UpdateRNFAsync")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> UpdateRNFAsync(RNFInputDto rFNInputDto)
        {
            var result = new CommonResult();

            var cRN = iService.Get(rFNInputDto.Id);
            var info = rFNInputDto.MapTo(cRN);

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try {
                HandleSpecificData.HandleSpecificDataRN(info, rFNInputDto);
                OnBeforeUpdate(info);
                var isSuccess = await iService.UpdateAsync(info, rFNInputDto.Id, conn, tran);
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
        /// 根據主鍵Id獲取一個對象信息
        /// </summary>
        /// <param name="id">主鍵Id</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        [YuebonAuthorize("List")]
        public override async Task<CommonResult<CustomerRNOutputDto>> GetById(string id)
        {
            CommonResult<CustomerRNOutputDto> result = new CommonResult<CustomerRNOutputDto>();
            CustomerRNOutputDto info = await iService.GetOutDtoAsync(id);
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
                    } else {
                        result.ErrCode = "43003";
                        result.ErrMsg = "刪除承租人";
                        result.Success = false;
                    }
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("刪除承租人失敗", ex);
                    result.ErrMsg = "刪除承租人失敗";
                    result.ErrCode = ErrCode.err43003;
                    result.Success = false;
                    // 觸發ExceptionHandlingAttribute.OnException
                    throw;
                }
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
                var RNID = iService.Get(formCollection["sid"]).RNID;
                var Addres = Add(filelist[0], belongApp, belongAppId, RNID);
                result.ResData = Addres;
                var historyform = new HistoryFormRN() {
                    IDNo = RNID,
                    FormName = string.IsNullOrEmpty(formCollection["formname"]) ? Addres.FileName.Remove(0, 14) : formCollection["formname"],
                    Note = formCollection["note"],
                    UploadTime = DateTime.Now,
                    FileName = Addres.FileName,
                    CreatorUserId = CurrentUser.UserId
                };
                _historyFormService.InsertAsync(historyform);
                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                throw ex;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 取承租人身份證字號或居留證號
        /// </summary>
        /// <param name="RNID">承租人身份證字號或居留證號</param>
        /// <returns></returns>
        [HttpPost("GetByRNID")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetByRNID(string RNID)
        {
            var result = new CommonResult();
            if (string.IsNullOrEmpty(RNID) || RNID.Length != 10) {
                result.Success = false;
                result.ErrMsg = "身分證字號或居留證號長度錯誤";
                return ToJsonContent(result);
            }

            var rn = await iService.GetCustomerByRNID(RNID);

            result.ResData = (rn != null) ? rn.RNName : string.Empty;
            result.ErrCode = ErrCode.successCode;
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
            string uploadPath = @$"{path}historyPDF\CustomerR\RN\";
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

        /// <summary>
        /// 新增或修改時合併或分割特殊欄位(地址、生日、電話)
        /// </summary>
        /// <param name="SourceCustomerRN"></param>
        /// <param name="customerRNInputDto"></param>
        /// <returns></returns>
        private CustomerRN HandleSpecialData(CustomerRN SourceCustomerRN, CustomerRNInputDto customerRNInputDto = null)
        {
            //customerRNInputDto == null 代表是更新同住人資料
            if (customerRNInputDto == null) {
                if (!string.IsNullOrEmpty(SourceCustomerRN.RNFID_1_A)) {
                    var splitChar = SourceCustomerRN.RNFID_1_A.ToCharArray();
                    SourceCustomerRN.RNFID_1_A_1 = splitChar[0].ToString();
                    SourceCustomerRN.RNFID_1_A_2 = splitChar[1].ToString();
                    SourceCustomerRN.RNFID_1_A_3 = splitChar[2].ToString();
                    SourceCustomerRN.RNFID_1_A_4 = splitChar[3].ToString();
                    SourceCustomerRN.RNFID_1_A_5 = splitChar[4].ToString();
                    SourceCustomerRN.RNFID_1_A_6 = splitChar[5].ToString();
                    SourceCustomerRN.RNFID_1_A_7 = splitChar[6].ToString();
                    SourceCustomerRN.RNFID_1_A_8 = splitChar[7].ToString();
                    SourceCustomerRN.RNFID_1_A_9 = splitChar[8].ToString();
                    SourceCustomerRN.RNFID_1_A_10 = splitChar[9].ToString();
                }
                if (!string.IsNullOrEmpty(SourceCustomerRN.RNFID_1_B)) {
                    var splitChar = SourceCustomerRN.RNFID_1_B.ToCharArray();
                    SourceCustomerRN.RNFID_1_B_1 = splitChar[0].ToString();
                    SourceCustomerRN.RNFID_1_B_2 = splitChar[1].ToString();
                    SourceCustomerRN.RNFID_1_B_3 = splitChar[2].ToString();
                    SourceCustomerRN.RNFID_1_B_4 = splitChar[3].ToString();
                    SourceCustomerRN.RNFID_1_B_5 = splitChar[4].ToString();
                    SourceCustomerRN.RNFID_1_B_6 = splitChar[5].ToString();
                    SourceCustomerRN.RNFID_1_B_7 = splitChar[6].ToString();
                    SourceCustomerRN.RNFID_1_B_8 = splitChar[7].ToString();
                    SourceCustomerRN.RNFID_1_B_9 = splitChar[8].ToString();
                    SourceCustomerRN.RNFID_1_B_10 = splitChar[9].ToString();
                }
                if (!string.IsNullOrEmpty(SourceCustomerRN.RNFID_1_C)) {
                    var splitChar = SourceCustomerRN.RNFID_1_C.ToCharArray();
                    SourceCustomerRN.RNFID_1_C_1 = splitChar[0].ToString();
                    SourceCustomerRN.RNFID_1_C_2 = splitChar[1].ToString();
                    SourceCustomerRN.RNFID_1_C_3 = splitChar[2].ToString();
                    SourceCustomerRN.RNFID_1_C_4 = splitChar[3].ToString();
                    SourceCustomerRN.RNFID_1_C_5 = splitChar[4].ToString();
                    SourceCustomerRN.RNFID_1_C_6 = splitChar[5].ToString();
                    SourceCustomerRN.RNFID_1_C_7 = splitChar[6].ToString();
                    SourceCustomerRN.RNFID_1_C_8 = splitChar[7].ToString();
                    SourceCustomerRN.RNFID_1_C_9 = splitChar[8].ToString();
                    SourceCustomerRN.RNFID_1_C_10 = splitChar[9].ToString();
                }
                if (!string.IsNullOrEmpty(SourceCustomerRN.RNFID_1_D)) {
                    var splitChar = SourceCustomerRN.RNFID_1_D.ToCharArray();
                    SourceCustomerRN.RNFID_1_D_1 = splitChar[0].ToString();
                    SourceCustomerRN.RNFID_1_D_2 = splitChar[1].ToString();
                    SourceCustomerRN.RNFID_1_D_3 = splitChar[2].ToString();
                    SourceCustomerRN.RNFID_1_D_4 = splitChar[3].ToString();
                    SourceCustomerRN.RNFID_1_D_5 = splitChar[4].ToString();
                    SourceCustomerRN.RNFID_1_D_6 = splitChar[5].ToString();
                    SourceCustomerRN.RNFID_1_D_7 = splitChar[6].ToString();
                    SourceCustomerRN.RNFID_1_D_8 = splitChar[7].ToString();
                    SourceCustomerRN.RNFID_1_D_9 = splitChar[8].ToString();
                    SourceCustomerRN.RNFID_1_D_10 = splitChar[9].ToString();
                }
                if (!string.IsNullOrEmpty(SourceCustomerRN.RNFID_1_E)) {
                    var splitChar = SourceCustomerRN.RNFID_1_E.ToCharArray();
                    SourceCustomerRN.RNFID_1_E_1 = splitChar[0].ToString();
                    SourceCustomerRN.RNFID_1_E_2 = splitChar[1].ToString();
                    SourceCustomerRN.RNFID_1_E_3 = splitChar[2].ToString();
                    SourceCustomerRN.RNFID_1_E_4 = splitChar[3].ToString();
                    SourceCustomerRN.RNFID_1_E_5 = splitChar[4].ToString();
                    SourceCustomerRN.RNFID_1_E_6 = splitChar[5].ToString();
                    SourceCustomerRN.RNFID_1_E_7 = splitChar[6].ToString();
                    SourceCustomerRN.RNFID_1_E_8 = splitChar[7].ToString();
                    SourceCustomerRN.RNFID_1_E_9 = splitChar[8].ToString();
                    SourceCustomerRN.RNFID_1_E_10 = splitChar[9].ToString();
                }
                if (!string.IsNullOrEmpty(SourceCustomerRN.RNFID_1_F)) {
                    var splitChar = SourceCustomerRN.RNFID_1_F.ToCharArray();
                    SourceCustomerRN.RNFID_1_F_1 = splitChar[0].ToString();
                    SourceCustomerRN.RNFID_1_F_2 = splitChar[1].ToString();
                    SourceCustomerRN.RNFID_1_F_3 = splitChar[2].ToString();
                    SourceCustomerRN.RNFID_1_F_4 = splitChar[3].ToString();
                    SourceCustomerRN.RNFID_1_F_5 = splitChar[4].ToString();
                    SourceCustomerRN.RNFID_1_F_6 = splitChar[5].ToString();
                    SourceCustomerRN.RNFID_1_F_7 = splitChar[6].ToString();
                    SourceCustomerRN.RNFID_1_F_8 = splitChar[7].ToString();
                    SourceCustomerRN.RNFID_1_F_9 = splitChar[8].ToString();
                    SourceCustomerRN.RNFID_1_F_10 = splitChar[9].ToString();
                }
                if (!string.IsNullOrEmpty(SourceCustomerRN.RNFID_1_G)) {
                    var splitChar = SourceCustomerRN.RNFID_1_G.ToCharArray();
                    SourceCustomerRN.RNFID_1_G_1 = splitChar[0].ToString();
                    SourceCustomerRN.RNFID_1_G_2 = splitChar[1].ToString();
                    SourceCustomerRN.RNFID_1_G_3 = splitChar[2].ToString();
                    SourceCustomerRN.RNFID_1_G_4 = splitChar[3].ToString();
                    SourceCustomerRN.RNFID_1_G_5 = splitChar[4].ToString();
                    SourceCustomerRN.RNFID_1_G_6 = splitChar[5].ToString();
                    SourceCustomerRN.RNFID_1_G_7 = splitChar[6].ToString();
                    SourceCustomerRN.RNFID_1_G_8 = splitChar[7].ToString();
                    SourceCustomerRN.RNFID_1_G_9 = splitChar[8].ToString();
                    SourceCustomerRN.RNFID_1_G_10 = splitChar[9].ToString();
                }

            } else {
                //生日
                if (!string.IsNullOrEmpty(customerRNInputDto.RNBirthday)) {
                    SourceCustomerRN.RNBirthday_Y = customerRNInputDto.RNBirthday.Split("-")[0];
                    SourceCustomerRN.RNBirthday_M = customerRNInputDto.RNBirthday.Split("-")[1];
                    SourceCustomerRN.RNBirthday_D = customerRNInputDto.RNBirthday.Split("-")[2];
                }
                //電話
                SourceCustomerRN.RNTel = customerRNInputDto.RNTel_1 + "-" + customerRNInputDto.RNTel_2;
                SourceCustomerRN.RNTelN = customerRNInputDto.RNTelN_1 + "-" + customerRNInputDto.RNTelN_2;
                //SourceCustomerRN.rnagte = customerRNInputDto.RNAGTel_1_A + "-" + customerRNInputDto.RNAGTel_2_A;
                //手機
                //if (!string.IsNullOrEmpty(customerRNInputDto.RNCell)) {
                //    if (customerRNInputDto.RNCell.Length >= 4 && !customerRNInputDto.RNCell.Contains("-")) {
                //        SourceCustomerRN.RNCell = customerRNInputDto.RNCell.Insert(4, "-");
                //    }
                //}
                //身分證字號
                var idSplit = customerRNInputDto.RNID.ToCharArray();
                SourceCustomerRN.RNID_1_1 = idSplit[0].ToString();
                SourceCustomerRN.RNID_1_2 = idSplit[1].ToString();
                SourceCustomerRN.RNID_1_3 = idSplit[2].ToString();
                SourceCustomerRN.RNID_1_4 = idSplit[3].ToString();
                SourceCustomerRN.RNID_1_5 = idSplit[4].ToString();
                SourceCustomerRN.RNID_1_6 = idSplit[5].ToString();
                SourceCustomerRN.RNID_1_7 = idSplit[6].ToString();
                SourceCustomerRN.RNID_1_8 = idSplit[7].ToString();
                SourceCustomerRN.RNID_1_9 = idSplit[8].ToString();
                SourceCustomerRN.RNID_1_10 = idSplit[9].ToString();

                SourceCustomerRN.RNAdd = Utils.memergeAddFix(
                    customerRNInputDto.RNAdd_1,
                    customerRNInputDto.RNAdd_1_1,
                    customerRNInputDto.RNAdd_1_2,
                    customerRNInputDto.RNAdd_2,
                    customerRNInputDto.RNAdd_2_1,
                    customerRNInputDto.RNAdd_2_2,
                    customerRNInputDto.RNAdd_2_3,
                    customerRNInputDto.RNAdd_2_4,
                    customerRNInputDto.RNAdd_3,
                    customerRNInputDto.RNAdd_3_1,
                    customerRNInputDto.RNAdd_3_2,
                    customerRNInputDto.RNAdd_4,
                    customerRNInputDto.RNAdd_5,
                    customerRNInputDto.RNAdd_6,
                    customerRNInputDto.RNAdd_7,
                    customerRNInputDto.RNAdd_8,
                    customerRNInputDto.RNAdd_9);
                SourceCustomerRN.RNAGAdd = Utils.memergeAddFix(
                    customerRNInputDto.RNAGAdd_1_A,
                    customerRNInputDto.RNAGAdd_1_1_A,
                    customerRNInputDto.RNAGAdd_1_2_A,
                    customerRNInputDto.RNAGAdd_2_A,
                    customerRNInputDto.RNAGAdd_2_1_A,
                    customerRNInputDto.RNAGAdd_2_2_A,
                    customerRNInputDto.RNAGAdd_2_3_A,
                    customerRNInputDto.RNAGAdd_2_4_A,
                    customerRNInputDto.RNAGAdd_3_A,
                    customerRNInputDto.RNAGAdd_3_1_A,
                    customerRNInputDto.RNAGAdd_3_2_A,
                    customerRNInputDto.RNAGAdd_4_A,
                    customerRNInputDto.RNAGAdd_5_A,
                    customerRNInputDto.RNAGAdd_6_A,
                    customerRNInputDto.RNAGAdd_7_A,
                    customerRNInputDto.RNAGAdd_8_A,
                    customerRNInputDto.RNAGAdd_9_A);
                if (customerRNInputDto.RNAddSame == "/Yes") {
                    SourceCustomerRN.RNAddC_1 = "";
                    SourceCustomerRN.RNAddC_1_1 = "";
                    SourceCustomerRN.RNAddC_1_2 = "";
                    SourceCustomerRN.RNAddC_2 = "";
                    SourceCustomerRN.RNAddC_2_1 = "";
                    SourceCustomerRN.RNAddC_2_2 = "";
                    SourceCustomerRN.RNAddC_2_3 = "";
                    SourceCustomerRN.RNAddC_2_4 = "";
                    SourceCustomerRN.RNAddC_3 = "";
                    SourceCustomerRN.RNAddC_3_1 = "";
                    SourceCustomerRN.RNAddC_3_2 = "";
                    SourceCustomerRN.RNAddC_4 = "";
                    SourceCustomerRN.RNAddC_5 = "";
                    SourceCustomerRN.RNAddC_6 = "";
                    SourceCustomerRN.RNAddC_7 = "";
                    SourceCustomerRN.RNAddC_8 = "";
                    SourceCustomerRN.RNAddC_9 = "";
                    SourceCustomerRN.RNAddC = SourceCustomerRN.RNAdd;
                } else {
                    SourceCustomerRN.RNAddC = Utils.memergeAddFix(
                            customerRNInputDto.RNAddC_1,
                            customerRNInputDto.RNAddC_1_1,
                            customerRNInputDto.RNAddC_1_2,
                            customerRNInputDto.RNAddC_2,
                            customerRNInputDto.RNAddC_2_1,
                            customerRNInputDto.RNAddC_2_2,
                            customerRNInputDto.RNAddC_2_3,
                            customerRNInputDto.RNAddC_2_4,
                            customerRNInputDto.RNAddC_3,
                            customerRNInputDto.RNAddC_3_1,
                            customerRNInputDto.RNAddC_3_2,
                            customerRNInputDto.RNAddC_4,
                            customerRNInputDto.RNAddC_5,
                            customerRNInputDto.RNAddC_6,
                            customerRNInputDto.RNAddC_7,
                            customerRNInputDto.RNAddC_8,
                            customerRNInputDto.RNAddC_9);
                }
                SourceCustomerRN.RNGAdd = Utils.memergeAddFix(
                    customerRNInputDto.RNGAdd_1,
                    customerRNInputDto.RNGAdd_1_1,
                    customerRNInputDto.RNGAdd_1_2,
                    customerRNInputDto.RNGAdd_2,
                    customerRNInputDto.RNGAdd_2_1,
                    customerRNInputDto.RNGAdd_2_2,
                    customerRNInputDto.RNGAdd_2_3,
                    customerRNInputDto.RNGAdd_2_4,
                    customerRNInputDto.RNGAdd_3,
                    customerRNInputDto.RNGAdd_3_1,
                    customerRNInputDto.RNGAdd_3_2,
                    customerRNInputDto.RNGAdd_4,
                    customerRNInputDto.RNGAdd_5,
                    customerRNInputDto.RNGAdd_6,
                    customerRNInputDto.RNGAdd_7,
                    customerRNInputDto.RNGAdd_8,
                    customerRNInputDto.RNGAdd_9);
                if (SourceCustomerRN.RNGAddSame == "/Yes") {
                    SourceCustomerRN.RNGCAdd_1 = "";
                    SourceCustomerRN.RNGCAdd_1_1 = "";
                    SourceCustomerRN.RNGCAdd_1_2 = "";
                    SourceCustomerRN.RNGCAdd_2 = "";
                    SourceCustomerRN.RNGCAdd_2_1 = "";
                    SourceCustomerRN.RNGCAdd_2_2 = "";
                    SourceCustomerRN.RNGCAdd_2_3 = "";
                    SourceCustomerRN.RNGCAdd_2_4 = "";
                    SourceCustomerRN.RNGCAdd_3 = "";
                    SourceCustomerRN.RNGCAdd_3_1 = "";
                    SourceCustomerRN.RNGCAdd_3_2 = "";
                    SourceCustomerRN.RNGCAdd_4 = "";
                    SourceCustomerRN.RNGCAdd_5 = "";
                    SourceCustomerRN.RNGCAdd_6 = "";
                    SourceCustomerRN.RNGCAdd_7 = "";
                    SourceCustomerRN.RNGCAdd_8 = "";
                    SourceCustomerRN.RNGCAdd_9 = "";
                    SourceCustomerRN.RNGCAdd = SourceCustomerRN.RNGAdd;
                } else {
                    SourceCustomerRN.RNGCAdd = Utils.memergeAddFix(
                        customerRNInputDto.RNGCAdd_1,
                        customerRNInputDto.RNGCAdd_1_1,
                        customerRNInputDto.RNGCAdd_1_2,
                        customerRNInputDto.RNGCAdd_2,
                        customerRNInputDto.RNGCAdd_2_1,
                        customerRNInputDto.RNGCAdd_2_2,
                        customerRNInputDto.RNGCAdd_2_3,
                        customerRNInputDto.RNGCAdd_2_4,
                        customerRNInputDto.RNGCAdd_3,
                        customerRNInputDto.RNGCAdd_3_1,
                        customerRNInputDto.RNGCAdd_3_2,
                        customerRNInputDto.RNGCAdd_4,
                        customerRNInputDto.RNGCAdd_5,
                        customerRNInputDto.RNGCAdd_6,
                        customerRNInputDto.RNGCAdd_7,
                        customerRNInputDto.RNGCAdd_8,
                        customerRNInputDto.RNGCAdd_9);
                }


            }
            return SourceCustomerRN;
        }
    }
}
