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
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Commons.Json;
using Yuebon.Chaochi.Core.IServices;
using Yuebon.Chaochi.Core.Helpers;

namespace Yuebon.WebApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 用戶接口
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/Chaochi/[controller]")]
    public class CustomerLCController : AreaApiController<CustomerLC, CustomerLCOutputDto, CustomerLCInputDto, ICustomerLCService, string>
    {
        private Yuebon.Security.IServices.IOrganizeService organizeService;
        private Yuebon.Security.IServices.IRoleService roleService;
        private Yuebon.Security.IServices.IUserLogOnService userLogOnService;
        private readonly IHistoryFormLCService _historyFormService;
        private readonly ILandLordBelongingService _landLordBelongingService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="landLordBelongingService"></param>
        /// <param name="historyFormService"></param>
        /// <param name="_organizeService"></param>
        /// <param name="_roleService"></param>
        /// <param name="_userLogOnService"></param>
        public CustomerLCController(ICustomerLCService _iService, ILandLordBelongingService landLordBelongingService, IHistoryFormLCService historyFormService, Yuebon.Security.IServices.IOrganizeService _organizeService, Yuebon.Security.IServices.IRoleService _roleService, Yuebon.Security.IServices.IUserLogOnService _userLogOnService) : base(_iService)
        {
            iService = _iService;
            organizeService = _organizeService;
            roleService = _roleService;
            userLogOnService = _userLogOnService;
            _historyFormService = historyFormService;
            _landLordBelongingService = landLordBelongingService;
        }

        /// <summary>
        /// 新增前鉤子
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(CustomerLC info)
        {
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
            info.LastModifyUserId = CurrentUser.UserId;
            info.CreatorUserDeptId = CurrentUser.DeptId;
            info.CreatorUserOrgId = CurrentUser.OrganizeId;
            _landLordBelongingService.Insert(new LandLordBelonging {
                LId = info.LCID,
                SalesId = CurrentUser.UserId,
            });
        }
        /// <summary>
        /// 更新前鉤子
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeUpdate(CustomerLC info)
        {
            info.LastModifyTime = DateTime.Now;
            info.LastModifyUserId = CurrentUser.UserId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(CustomerLCInputDto tinfo)
        {
            var result = new CommonResult();
            var customerLC = tinfo.MapTo<CustomerLC>();
            var isExists = await iService.GetByLCID(customerLC.LCID);
            if (isExists != null) {
                result.Success = false;
                result.ErrMsg = "此統一編號已存在";
                return ToJsonContent(result);
            } else {
                try {
                    HandleSpecificData.HandleSpecificDataLC(customerLC, tinfo);
                    //HandleSpecialData(customerLC, tinfo);
                    OnBeforeInsert(customerLC);
                    long LC = await iService.InsertAsync(customerLC).ConfigureAwait(false);
                } catch (Exception ex) {
                    result.Success = false;
                    result.ErrMsg = ex.Message;
                    return ToJsonContent(result);
                }
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
                return ToJsonContent(result);
            }

        }

        /// <summary>
        /// 異步分頁查詢
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerSearchAsync")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> FindWithPagerSearchAsync(SearchCustomerLCModel search)
        {
            CommonResult<PageResult<CustomerLCOutputDto>> result = new CommonResult<PageResult<CustomerLCOutputDto>>();
            result.ResData = await iService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 更新、修改
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override Task<IActionResult> UpdateAsync(CustomerLCInputDto inInfo)
        {
            var result = new CommonResult();
            var cLC = iService.Get(inInfo.Id);
            var info = inInfo.MapTo(cLC);


            HandleSpecificData.HandleSpecificDataLC(info, inInfo);
            //HandleSpecialData(info, inInfo);

            OnBeforeUpdate(info);
            info.LCID = cLC.LCID;
            var isSuccess = iService.Update(info, inInfo.Id);
            if (isSuccess) {
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } else {
                result.Success = false;
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return Task.FromResult(ToJsonContent(result));
        }

        /// <summary>
        /// 獲取某業務的所有房東
        /// </summary>
        /// <param name="creatorUserId"></param>
        /// <returns></returns>
        [HttpGet("GetListByCreatorUserId")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetListByCreatorUserId(string creatorUserId)
        {
            var result = new CommonResult();
            var lcList = await iService.GetListWhereAsync($"CreatorUserId like '%{creatorUserId}%'");

            result.ResData = lcList;
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
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
                var LCID = iService.Get(formCollection["sid"]).LCID;
                var Addres = Add(filelist[0], belongApp, belongAppId, LCID);
                result.ResData = Addres;
                var historyform = new HistoryFormLC() {
                    IDNo = LCID,
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
        /// 取出法人統編
        /// </summary>
        /// <param name="LCID">法人統編</param>
        /// <returns></returns>
        [HttpPost("GetByLCID")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetByLCID(string LCID)
        {
            var result = new CommonResult();
            if (string.IsNullOrEmpty(LCID) || LCID.Length != 8) {
                result.Success = false;
                result.ErrMsg = "統一編號長度錯誤";
                return ToJsonContent(result);
            }

            var lc = await iService.GetByLCID(LCID);

            result.ResData = (lc != null) ? lc.LCName : string.Empty;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 單個上傳文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="belongApp"></param>
        /// <param name="belongAppId"></param>
        /// <param name="LCID"></param>
        /// <returns></returns>
        private Yuebon.Security.Dtos.UploadFileResultOuputDto Add(IFormFile file, string belongApp, string belongAppId, string LCID)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = file.FileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    var filenameExt = UploadFile(fileName, data, LCID);

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
        /// <param name="LCID"></param>
        private string UploadFile(string fileName, byte[] fileBuffers, string LCID)
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
            string uploadPath = @$"{path}historyPDF\CustomerL\LC\";
            if (!(System.IO.File.Exists(uploadPath + $"{LCID}"))) {
                Directory.CreateDirectory(@$"{uploadPath}{LCID}\");
            }
            uploadPath = uploadPath + LCID + "\\";
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
        /// <param name="SourceCustomerLC"></param>
        /// <param name="customerLCInputDto"></param>
        /// <returns></returns>
        private CustomerLC HandleSpecialData(CustomerLC SourceCustomerLC, CustomerLCInputDto customerLCInputDto)
        {
            //電話
            SourceCustomerLC.LCTel = customerLCInputDto.LCTel_1 + "-" + customerLCInputDto.LCTel_2;
            SourceCustomerLC.LCAGTel_A = customerLCInputDto.LCAGTel_1_A + "-" + customerLCInputDto.LCAGTel_2_A;
            SourceCustomerLC.LCAGTel_B = customerLCInputDto.LCAGTel_1_B + "-" + customerLCInputDto.LCAGTel_2_B;
            //統一編號
            var idSplit = customerLCInputDto.LCID.ToCharArray();
            SourceCustomerLC.LCID_1_1 = idSplit[0].ToString();
            SourceCustomerLC.LCID_1_2 = idSplit[1].ToString();
            SourceCustomerLC.LCID_1_3 = idSplit[2].ToString();
            SourceCustomerLC.LCID_1_4 = idSplit[3].ToString();
            SourceCustomerLC.LCID_1_5 = idSplit[4].ToString();
            SourceCustomerLC.LCID_1_6 = idSplit[5].ToString();
            SourceCustomerLC.LCID_1_7 = idSplit[6].ToString();
            SourceCustomerLC.LCID_1_8 = idSplit[7].ToString();

            SourceCustomerLC.LCAdd = Utils.memergeAddFix(
                customerLCInputDto.LCAdd_1,
                customerLCInputDto.LCAdd_1_1,
                customerLCInputDto.LCAdd_1_2,
                customerLCInputDto.LCAdd_2,
                customerLCInputDto.LCAdd_2_1,
                customerLCInputDto.LCAdd_2_2,
                customerLCInputDto.LCAdd_2_3,
                customerLCInputDto.LCAdd_2_4,
                customerLCInputDto.LCAdd_3,
                customerLCInputDto.LCAdd_3_1,
                customerLCInputDto.LCAdd_3_2,
                customerLCInputDto.LCAdd_4,
                customerLCInputDto.LCAdd_5,
                customerLCInputDto.LCAdd_6,
                customerLCInputDto.LCAdd_7,
                customerLCInputDto.LCAdd_8,
                customerLCInputDto.LCAdd_9);
            SourceCustomerLC.LCAGAdd_A = Utils.memergeAddFix(
                            customerLCInputDto.LCAGAdd_1_A,
                            customerLCInputDto.LCAGAdd_1_1_A,
                            customerLCInputDto.LCAGAdd_1_2_A,
                            customerLCInputDto.LCAGAdd_2_A,
                            customerLCInputDto.LCAGAdd_2_1_A,
                            customerLCInputDto.LCAGAdd_2_2_A,
                            customerLCInputDto.LCAGAdd_2_3_A,
                            customerLCInputDto.LCAGAdd_2_4_A,
                            customerLCInputDto.LCAGAdd_3_A,
                            customerLCInputDto.LCAGAdd_3_1_A,
                            customerLCInputDto.LCAGAdd_3_2_A,
                            customerLCInputDto.LCAGAdd_4_A,
                            customerLCInputDto.LCAGAdd_5_A,
                            customerLCInputDto.LCAGAdd_6_A,
                            customerLCInputDto.LCAGAdd_7_A,
                            customerLCInputDto.LCAGAdd_8_A,
                            customerLCInputDto.LCAGAdd_9_A);
            SourceCustomerLC.LCAGAdd_B = Utils.memergeAddFix(
                            customerLCInputDto.LCAGAdd_1_B,
                            customerLCInputDto.LCAGAdd_1_1_B,
                            customerLCInputDto.LCAGAdd_1_2_B,
                            customerLCInputDto.LCAGAdd_2_B,
                            customerLCInputDto.LCAGAdd_2_1_B,
                            customerLCInputDto.LCAGAdd_2_2_B,
                            customerLCInputDto.LCAGAdd_2_3_B,
                            customerLCInputDto.LCAGAdd_2_4_B,
                            customerLCInputDto.LCAGAdd_3_B,
                            customerLCInputDto.LCAGAdd_3_1_B,
                            customerLCInputDto.LCAGAdd_3_2_B,
                            customerLCInputDto.LCAGAdd_4_B,
                            customerLCInputDto.LCAGAdd_5_B,
                            customerLCInputDto.LCAGAdd_6_B,
                            customerLCInputDto.LCAGAdd_7_B,
                            customerLCInputDto.LCAGAdd_8_B,
                            customerLCInputDto.LCAGAdd_9_B);
            if (customerLCInputDto.LCAddSame == "/Yes") {
                SourceCustomerLC.LCAddC_1 = "";
                SourceCustomerLC.LCAddC_1_1 = "";
                SourceCustomerLC.LCAddC_1_2 = "";
                SourceCustomerLC.LCAddC_2 = "";
                SourceCustomerLC.LCAddC_2_1 = "";
                SourceCustomerLC.LCAddC_2_2 = "";
                SourceCustomerLC.LCAddC_2_3 = "";
                SourceCustomerLC.LCAddC_2_4 = "";
                SourceCustomerLC.LCAddC_3 = "";
                SourceCustomerLC.LCAddC_3_1 = "";
                SourceCustomerLC.LCAddC_3_2 = "";
                SourceCustomerLC.LCAddC_4 = "";
                SourceCustomerLC.LCAddC_5 = "";
                SourceCustomerLC.LCAddC_6 = "";
                SourceCustomerLC.LCAddC_7 = "";
                SourceCustomerLC.LCAddC_8 = "";
                SourceCustomerLC.LCAddC_9 = "";
                SourceCustomerLC.LCAddC = SourceCustomerLC.LCAdd;
            } else {
                SourceCustomerLC.LCAddC = Utils.memergeAddFix(
                        customerLCInputDto.LCAddC_1,
                        customerLCInputDto.LCAddC_1_1,
                        customerLCInputDto.LCAddC_1_2,
                        customerLCInputDto.LCAddC_2,
                        customerLCInputDto.LCAddC_2_1,
                        customerLCInputDto.LCAddC_2_2,
                        customerLCInputDto.LCAddC_2_3,
                        customerLCInputDto.LCAddC_2_4,
                        customerLCInputDto.LCAddC_3,
                        customerLCInputDto.LCAddC_3_1,
                        customerLCInputDto.LCAddC_3_2,
                        customerLCInputDto.LCAddC_4,
                        customerLCInputDto.LCAddC_5,
                        customerLCInputDto.LCAddC_6,
                        customerLCInputDto.LCAddC_7,
                        customerLCInputDto.LCAddC_8,
                        customerLCInputDto.LCAddC_9);
            }
            return SourceCustomerLC;
        }
    }
}