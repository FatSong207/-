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
using Yuebon.Commons.Json;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Chaochi.Core.IServices;
using System.Text.Json;
using Yuebon.Chaochi.Core.Helpers;

namespace Yuebon.WebApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 用戶接口
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class CustomerLNController : AreaApiController<CustomerLN, CustomerLNOutputDto, CustomerLNInputDto, ICustomerLNService, string>
    {
        private readonly Yuebon.Security.IServices.IUserService _userService;
        private Yuebon.Security.IServices.IOrganizeService organizeService;
        private Yuebon.Security.IServices.IRoleService roleService;
        private Yuebon.Security.IServices.IUserLogOnService userLogOnService;
        private readonly IHistoryFormLNService _historyFormService;
        private readonly ILandLordBelongingService _landLordBelongingService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="userService"></param>
        /// <param name="landLordBelongingService"></param>
        /// <param name="historyFormService"></param>
        /// <param name="_organizeService"></param>
        /// <param name="_roleService"></param>
        /// <param name="_userLogOnService"></param>
        public CustomerLNController(ICustomerLNService _iService, Yuebon.Security.IServices.IUserService userService, ILandLordBelongingService landLordBelongingService, IHistoryFormLNService historyFormService, Yuebon.Security.IServices.IOrganizeService _organizeService, Yuebon.Security.IServices.IRoleService _roleService, Yuebon.Security.IServices.IUserLogOnService _userLogOnService) : base(_iService)
        {
            iService = _iService;
            _userService = userService;
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
        protected override void OnBeforeInsert(CustomerLN info)
        {
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
            info.LastModifyUserId = CurrentUser.UserId;
            info.CreatorUserOrgId = CurrentUser.OrganizeId;
            info.CreatorUserDeptId = CurrentUser.DeptId;
            _landLordBelongingService.Insert(new LandLordBelonging {
                LId = info.LNID,
                SalesId = CurrentUser.UserId,
            });
        }
        /// <summary>
        /// 更新前鉤子
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeUpdate(CustomerLN info)
        {
            info.LastModifyTime = DateTime.Now;
            info.LastModifyUserId = CurrentUser.UserId;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(CustomerLNInputDto tinfo)
        {
            var result = new CommonResult();
            //if (string.IsNullOrEmpty(tinfo.LNAdd_1) || string.IsNullOrEmpty(tinfo.LNAdd_2) || string.IsNullOrEmpty(tinfo.LNAdd_3)) {
            //    result.Success = false;
            //    result.ErrCode = ErrCode.err43002;
            //    result.ErrMsg = "請輸入完整戶籍地址";
            //    return ToJsonContent(result);
            //}
            if (tinfo.LNID.Length != 10) {
                result.Success = false;
                result.ErrMsg = "身分證字號長度錯誤";
                return ToJsonContent(result);
            }
            var customerLN = tinfo.MapTo<CustomerLN>();
            var isExists = await iService.GetByCustomerLNID(customerLN.LNID);
            if (isExists != null) {
                result.Success = false;
                result.ErrMsg = "此身份證字號／居留證號／統一編號已存在";
                return ToJsonContent(result);
            } else {
                try {
                    HandleSpecificData.HandleSpecificDataLN(customerLN, tinfo);
                    //HandleSpecialData(customerLN, tinfo);
                    OnBeforeInsert(customerLN);
                    long ln = await iService.InsertAsync(customerLN).ConfigureAwait(false);
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
        public async Task<IActionResult> FindWithPagerSearchAsync(SearchCustomerLNModel search)
        {
            CommonResult<PageResult<CustomerLNOutputDto>> result = new CommonResult<PageResult<CustomerLNOutputDto>>();
            result.ResData = await iService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override Task<IActionResult> UpdateAsync(CustomerLNInputDto inInfo)
        {
            var result = new CommonResult();
            if (string.IsNullOrEmpty(inInfo.LNAdd_1) || string.IsNullOrEmpty(inInfo.LNAdd_2) || string.IsNullOrEmpty(inInfo.LNAdd_3)) {
                result.Success = false;
                result.ErrCode = ErrCode.err43002;
                result.ErrMsg = "請輸入完整戶籍地址";
                return Task.FromResult(ToJsonContent(result));
            }
            var cLN = iService.Get(inInfo.Id);
            var info = inInfo.MapTo(cLN);
            //info = Merger.CloneAndMerge<CustomerLN>(cLN, info);

            HandleSpecificData.HandleSpecificDataLN(info, inInfo);
            //HandleSpecialData(info, inInfo);

            OnBeforeUpdate(info);
            info.LNID = cLN.LNID;
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
            var lnList =await iService.GetListWhereAsync($"CreatorUserId like '%{creatorUserId}%'");

            result.ResData = lnList;
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 取的當前使用者可觸摸到的userlist(利用於基本資料介面的查詢條件(歸屬業務))
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("GetPermissionUserList")]
        [YuebonAuthorize("CreatorId")]
        public async Task<IActionResult> GetPermissionUserList(string name)
        {
            var result = new CommonResult();
            var yuebonCacheHelper = new YuebonCacheHelper();
            var list = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + CurrentUser.UserId).ToJson()).ToArray();
            string test = $"DepartmentId in (' {string.Join("','", list)} ') and RealName like '%{name}%'";
            var user = await _userService.GetListWhereAsync($"DepartmentId in ('{string.Join("','", list)}') and RealName like '%{name}%'");
            var nameList = new List<String>();
            if (user.Count() > 0) {
                foreach (var item in user) {
                    nameList.Add(item.RealName);
                }
            }
            result.ResData = nameList;
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
                var LNID = iService.Get(formCollection["sid"]).LNID;
                var Addres = Add(filelist[0], belongApp, belongAppId, LNID);
                result.ResData = Addres;
                var historyform = new HistoryFormLN() {
                    IDNo = LNID,
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
        /// 取出租人身份證字號或居留證號
        /// </summary>
        /// <param name="LNID">出租人身份證字號或居留證號</param>
        /// <returns></returns>
        [HttpPost("GetByLNID")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetByLNID(string LNID)
        {
            var result = new CommonResult();
            if (string.IsNullOrEmpty(LNID) || LNID.Length != 10) {
                result.Success = false;
                result.ErrMsg = "身分證字號或居留證號長度錯誤";
                return ToJsonContent(result);
            }

            var ln = await iService.GetByCustomerLNID(LNID);

            result.ResData = (ln != null) ? ln.LNName : string.Empty;
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
        /// <param name="LNID"></param>
        private string UploadFile(string fileName, byte[] fileBuffers, string LNID)
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
            string uploadPath = @$"{path}historyPDF\CustomerL\LN\";
            if (!(System.IO.File.Exists(uploadPath + $"{LNID}"))) {
                Directory.CreateDirectory(@$"{uploadPath}{LNID}\");
            }
            uploadPath = uploadPath + LNID + "\\";
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
        /// <param name="SourceCustomerLN"></param>
        /// <param name="customerLNInputDto"></param>
        /// <returns></returns>
        private CustomerLN HandleSpecialData(CustomerLN SourceCustomerLN, CustomerLNInputDto customerLNInputDto)
        {
            //生日
            if (!string.IsNullOrEmpty(customerLNInputDto.LNBirthday)) {
                SourceCustomerLN.LNBirthday_Y = customerLNInputDto.LNBirthday.Split("-")[0];
                SourceCustomerLN.LNBirthday_M = customerLNInputDto.LNBirthday.Split("-")[1];
                SourceCustomerLN.LNBirthday_D = customerLNInputDto.LNBirthday.Split("-")[2];
            }
            //電話
            SourceCustomerLN.LNTel = customerLNInputDto.LNTel_1 + "-" + customerLNInputDto.LNTel_2;
            SourceCustomerLN.LNTelN = customerLNInputDto.LNTelN_1 + "-" + customerLNInputDto.LNTelN_2;
            SourceCustomerLN.LNAGTel_A = customerLNInputDto.LNAGTel_1_A + "-" + customerLNInputDto.LNAGTel_2_A;
            SourceCustomerLN.LNAGTel_B = customerLNInputDto.LNAGTel_1_B + "-" + customerLNInputDto.LNAGTel_2_B;
            //手機
            //if (!string.IsNullOrEmpty(customerLNInputDto.LNCell)) {
            //    if (customerLNInputDto.LNCell.Length >= 4 && !customerLNInputDto.LNCell.Contains("-")) {
            //        SourceCustomerLN.LNCell = customerLNInputDto.LNCell.Insert(4, "-");
            //    }
            //}
            //身分證字號
            var idSplit = customerLNInputDto.LNID.ToCharArray();
            SourceCustomerLN.LNID_1_1 = idSplit[0].ToString();
            SourceCustomerLN.LNID_1_2 = idSplit[1].ToString();
            SourceCustomerLN.LNID_1_3 = idSplit[2].ToString();
            SourceCustomerLN.LNID_1_4 = idSplit[3].ToString();
            SourceCustomerLN.LNID_1_5 = idSplit[4].ToString();
            SourceCustomerLN.LNID_1_6 = idSplit[5].ToString();
            SourceCustomerLN.LNID_1_7 = idSplit[6].ToString();
            SourceCustomerLN.LNID_1_8 = idSplit[7].ToString();
            SourceCustomerLN.LNID_1_9 = idSplit[8].ToString();
            SourceCustomerLN.LNID_1_10 = idSplit[9].ToString();

            //地址
            SourceCustomerLN.LNAdd = Utils.memergeAddFix(
                customerLNInputDto.LNAdd_1,
                customerLNInputDto.LNAdd_1_1,
                customerLNInputDto.LNAdd_1_2,
                customerLNInputDto.LNAdd_2,
                customerLNInputDto.LNAdd_2_1,
                customerLNInputDto.LNAdd_2_2,
                customerLNInputDto.LNAdd_2_3,
                customerLNInputDto.LNAdd_2_4,
                customerLNInputDto.LNAdd_3,
                customerLNInputDto.LNAdd_3_1,
                customerLNInputDto.LNAdd_3_2,
                customerLNInputDto.LNAdd_4,
                customerLNInputDto.LNAdd_5,
                customerLNInputDto.LNAdd_6,
                customerLNInputDto.LNAdd_7,
                customerLNInputDto.LNAdd_8,
                customerLNInputDto.LNAdd_9);
            SourceCustomerLN.LNAGAdd_A = Utils.memergeAddFix(
                            customerLNInputDto.LNAGAdd_1_A,
                            customerLNInputDto.LNAGAdd_1_1_A,
                            customerLNInputDto.LNAGAdd_1_2_A,
                            customerLNInputDto.LNAGAdd_2_A,
                            customerLNInputDto.LNAGAdd_2_1_A,
                            customerLNInputDto.LNAGAdd_2_2_A,
                            customerLNInputDto.LNAGAdd_2_3_A,
                            customerLNInputDto.LNAGAdd_2_4_A,
                            customerLNInputDto.LNAGAdd_3_A,
                            customerLNInputDto.LNAGAdd_3_1_A,
                            customerLNInputDto.LNAGAdd_3_2_A,
                            customerLNInputDto.LNAGAdd_4_A,
                            customerLNInputDto.LNAGAdd_5_A,
                            customerLNInputDto.LNAGAdd_6_A,
                            customerLNInputDto.LNAGAdd_7_A,
                            customerLNInputDto.LNAGAdd_8_A,
                            customerLNInputDto.LNAGAdd_9_A);
            SourceCustomerLN.LNAGAdd_B = Utils.memergeAddFix(
                            customerLNInputDto.LNAGAdd_1_B,
                            customerLNInputDto.LNAGAdd_1_1_B,
                            customerLNInputDto.LNAGAdd_1_2_B,
                            customerLNInputDto.LNAGAdd_2_B,
                            customerLNInputDto.LNAGAdd_2_1_B,
                            customerLNInputDto.LNAGAdd_2_2_B,
                            customerLNInputDto.LNAGAdd_2_3_B,
                            customerLNInputDto.LNAGAdd_2_4_B,
                            customerLNInputDto.LNAGAdd_3_B,
                            customerLNInputDto.LNAGAdd_3_1_B,
                            customerLNInputDto.LNAGAdd_3_2_B,
                            customerLNInputDto.LNAGAdd_4_B,
                            customerLNInputDto.LNAGAdd_5_B,
                            customerLNInputDto.LNAGAdd_6_B,
                            customerLNInputDto.LNAGAdd_7_B,
                            customerLNInputDto.LNAGAdd_8_B,
                            customerLNInputDto.LNAGAdd_9_B);
            if (customerLNInputDto.LNAddSame == "/Yes") {
                SourceCustomerLN.LNAddC_1 = "";
                SourceCustomerLN.LNAddC_1_1 = "";
                SourceCustomerLN.LNAddC_1_2 = "";
                SourceCustomerLN.LNAddC_2 = "";
                SourceCustomerLN.LNAddC_2_1 = "";
                SourceCustomerLN.LNAddC_2_2 = "";
                SourceCustomerLN.LNAddC_2_3 = "";
                SourceCustomerLN.LNAddC_2_4 = "";
                SourceCustomerLN.LNAddC_3 = "";
                SourceCustomerLN.LNAddC_3_1 = "";
                SourceCustomerLN.LNAddC_3_2 = "";
                SourceCustomerLN.LNAddC_4 = "";
                SourceCustomerLN.LNAddC_5 = "";
                SourceCustomerLN.LNAddC_6 = "";
                SourceCustomerLN.LNAddC_7 = "";
                SourceCustomerLN.LNAddC_8 = "";
                SourceCustomerLN.LNAddC_9 = "";
                SourceCustomerLN.LNAddC = SourceCustomerLN.LNAdd;
            } else {
                SourceCustomerLN.LNAddC = Utils.memergeAddFix(
                        customerLNInputDto.LNAddC_1,
                        customerLNInputDto.LNAddC_1_1,
                        customerLNInputDto.LNAddC_1_2,
                        customerLNInputDto.LNAddC_2,
                        customerLNInputDto.LNAddC_2_1,
                        customerLNInputDto.LNAddC_2_2,
                        customerLNInputDto.LNAddC_2_3,
                        customerLNInputDto.LNAddC_2_4,
                        customerLNInputDto.LNAddC_3,
                        customerLNInputDto.LNAddC_3_1,
                        customerLNInputDto.LNAddC_3_2,
                        customerLNInputDto.LNAddC_4,
                        customerLNInputDto.LNAddC_5,
                        customerLNInputDto.LNAddC_6,
                        customerLNInputDto.LNAddC_7,
                        customerLNInputDto.LNAddC_8,
                        customerLNInputDto.LNAddC_9);
            }
            return SourceCustomerLN;
        }
    }
}