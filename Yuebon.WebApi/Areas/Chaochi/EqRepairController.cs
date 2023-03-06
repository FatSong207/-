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
using Yuebon.Commons.Dtos;
using System.Data;
using Yuebon.Commons.IDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Yuebon.Chaochi.IRepositories;
using System.Linq;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Json;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Youbon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class EqRepairController : AreaApiController<EqRepair, EqRepairOutputDto, EqRepairInputDto, IEqRepairService, string>
    {
        private readonly IEqRepairDetailRepository _eqRepairDetailRepository;
        private readonly IEqRepairDetailService _eqRepairDetailService;
        private readonly IDbContextCore _ybContext;
        private readonly IContractService _contractService;
        private readonly IContractLandlordService _contractLandlordService;
        private readonly IContractRenterService _contractRenterService;
        private readonly IContractBuildingService _contractBuildingService;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="eqRepairDetailRepository"></param>
        /// <param name="eqRepairDetailService"></param>
        /// <param name="ybContext"></param>
        /// <param name="contractService"></param>
        /// <param name="contractRenterService"></param>
        /// <param name="contractBuildingService"></param>
        /// <param name="contractLandlordService"></param>
        public EqRepairController(IEqRepairService _iService, IEqRepairDetailRepository eqRepairDetailRepository, IEqRepairDetailService eqRepairDetailService, IDbContextCore ybContext, IContractService contractService, IContractRenterService contractRenterService, IContractBuildingService contractBuildingService, IContractLandlordService contractLandlordService) : base(_iService)
        {
            iService = _iService;
            _eqRepairDetailRepository = eqRepairDetailRepository;
            _eqRepairDetailService = eqRepairDetailService;
            _ybContext = ybContext;
            _contractService = contractService;
            _contractRenterService = contractRenterService;
            _contractBuildingService = contractBuildingService;
            _contractLandlordService = contractLandlordService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(EqRepair info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
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
        protected override void OnBeforeUpdate(EqRepair info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 更新EqRepair與EqRepairDetails
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(EqRepairInputDto inInfo)
        {
            var result = new CommonResult();
            var cLN = iService.Get(inInfo.Id);
            var info = inInfo.MapTo(cLN);
            var eqRepairDetails = inInfo.eqRepairDetails;
            if (eqRepairDetails.Any(x => x.CurrentState == "已派工")) {
                info.State = "派工中";
                info.EndCaseDate = "";
            } else if (eqRepairDetails.Any(x => x.CurrentState == "未處理")) {
                info.State = "申請中";
                info.EndCaseDate = "";
            } else if (!eqRepairDetails.Any(x => x.CurrentState != "已完成")) {
                info.State = "已結案";
                info.EndCaseDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }

            OnBeforeUpdate(info);
            try {
                using IDbConnection conn = _ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = _ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();
                try {
                    //_buildingEquipmentService.Insert(buildingEquipment);
                    //_buildingEqService.InsertRange(buildingEqs);
                    var sss = await iService.UpdateAsync(info, inInfo.Id, conn, eftran.GetDbTransaction());
                    var eee = await _eqRepairDetailService.DeleteConnWhereAsync($" EqRepairId = '{inInfo.Id}' ", conn, eftran.GetDbTransaction());
                    _eqRepairDetailRepository.InsertRange(eqRepairDetails);

                    eftran.Commit();
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("更新失敗", ex);
                    result.ErrMsg = "更新失敗";
                    result.ErrCode = ErrCode.err43001;
                    throw;
                }
            } catch (Exception ex) {
                Log4NetHelper.Error("TransactionFailed", ex);
                result.ErrMsg = "TransactionFailed";
                result.ErrCode = ErrCode.err43001;
            }
            return await Task.FromResult(ToJsonContent(result));
        }

        /// <summary>
        /// 根據主鍵Id獲取一個對象信息
        /// </summary>
        /// <param name="id">主鍵Id</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        [YuebonAuthorize("List")]
        //[NoPermissionRequired]
        public override async Task<CommonResult<EqRepairOutputDto>> GetById(string id)
        {
            var result = new CommonResult<EqRepairOutputDto>();
            var info = await iService.GetOutDtoAsync(id);

            // 下面whereScript還需要增加簽約日期的判斷
            //var contractRenter = await _contractRenterService.GetWhereAsync($" RNID = '{info.CustomerID}' ");
            //var contractBuilding = await _contractBuildingService.GetWhereAsync($" BAdd = '{info.BAdd}' ");
            //string cid = string.Empty;
            //if (contractRenter != null && contractBuilding != null) {
            //    cid = contractRenter.CID;
            //}

            var c =await _contractService.GetWhereAsync($" CID = '{info.CID}' ");
            if (c is null) {
                result.ErrMsg = "合約不存在";
                result.ErrCode = "合約不存在";
                return result;
            } else {
                var issl = string.IsNullOrEmpty(c.LSID); //判斷是否為二房東
                if (issl) {
                    info.LSName = c.SLName;
                    info.LSTel = c.SLTel;
                } else {
                    var cl = await _contractLandlordService.GetWhereAsync($" CID = '{info.CID}' ");
                    info.LSName = cl.LSName;
                    info.LSTel = cl.LSTel;
                }

                result.ErrCode = ErrCode.successCode;
                result.ResData = info;
                return result;
            }
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerAsync")]
        [YuebonAuthorize("List")]
        public override async Task<CommonResult<PageResult<EqRepairOutputDto>>> FindWithPagerAsync(SearchInputDto<EqRepair> search)
        {
            CommonResult<PageResult<EqRepairOutputDto>> result = new CommonResult<PageResult<EqRepairOutputDto>>();
            result.ResData = await iService.FindWithPagerAsync(search);
            result.ErrCode = ErrCode.successCode;
            return result;
        }

        /// <summary>
        /// 圖片列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Getimgslist")]
        public virtual async Task<CommonResult<FileInfoOutputDto>> Getimgslist(string id)
        {
            CommonResult<FileInfoOutputDto> result = new CommonResult<FileInfoOutputDto>();
            var imgs = await iService.GetImgOutDtoAsync(id);
            if (imgs != null) {
                result.ErrCode = ErrCode.successCode;
                result.ResData = imgs;
            } else {
                result.Success = false;
                result.ErrMsg = "獲取錯誤!";
                result.ErrCode = "50001";
            }
            return result;
        }

        /// <summary>
        /// 獲取圖檔
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="id"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet("ShowImg")]
        [YuebonAuthorize("List")]
        [LogNotMethod]
        //public IActionResult Get(string fileName)
        public async Task<IActionResult> Get(string fileName, string id, string guid)
        {
            Yuebon.Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Yuebon.Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

            var image = System.IO.File.OpenRead($"{sysSetting.ChaochiFilepath}EqRepair\\{guid}\\{id}\\{fileName}");

            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
            return File(image, contentType ?? "image/jpeg");
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

            var EqRepaieDetailId = formCollection["id"];
            var EqRepairId = formCollection["guid"];
            var type = formCollection["type"];
            //string _fileName = filelist[0].FileName;

            Yuebon.Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Yuebon.Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = @$"{sysSetting.ChaochiFilepath}EqRepair\{EqRepairId}\{EqRepaieDetailId}\";

            try {
                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }
                var files = Directory.GetFiles(path);
                List<string> deletePath = new List<string>();
                string fieldName = "";
                switch (type) {
                    case "RC":
                        deletePath = files.Where(x => x.Contains("維修中近照")).ToList();
                        fieldName = "RepairingClose";
                        break;
                    case "RF":
                        deletePath = files.Where(x => x.Contains("維修中遠照")).ToList();
                        fieldName = "RepairingFar";
                        break;
                    case "RAC":
                        deletePath = files.Where(x => x.Contains("維修後近照")).ToList();
                        fieldName = "AfterRepairClose";
                        break;
                    case "RAF":
                        deletePath = files.Where(x => x.Contains("維修後遠照")).ToList();
                        fieldName = "AfterRepairFar";
                        break;
                    default:
                        break;  
                }
                foreach (var item in deletePath) {
                    System.IO.File.Delete(item);
                }
                var address = Add(filelist[0], EqRepairId, EqRepaieDetailId,type);

                await _eqRepairDetailService.UpdateTableFieldAsync($"{fieldName}",address.FileName,$" Id = '{EqRepaieDetailId}' ");
                var eqDetail =await _eqRepairDetailService.GetAsync(EqRepaieDetailId);
                int i = 0;
                if (!string.IsNullOrEmpty(eqDetail.BeforeRepairClose)) {
                    i++;
                }
                if (!string.IsNullOrEmpty(eqDetail.BeforeRepairFar)) {
                    i++;
                }
                if (!string.IsNullOrEmpty(eqDetail.RepairingClose)) {
                    i++;
                }
                if (!string.IsNullOrEmpty(eqDetail.RepairingFar)) {
                    i++;
                }
                if (!string.IsNullOrEmpty(eqDetail.AfterRepairClose)) {
                    i++;
                }
                if (!string.IsNullOrEmpty(eqDetail.AfterRepairFar)) {
                    i++;
                }
                await _eqRepairDetailService.UpdateTableFieldAsync("PhotoCount",i, $" Id = '{EqRepaieDetailId}' ");
                result.ResData = address;
                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                result.Success = false;
                Log4NetHelper.Error("修繕管理圖檔上傳失敗!", ex);
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 單個上傳文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="EqRepairId"></param>
        /// <param name="EqRepaieDetailId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private Yuebon.Security.Dtos.UploadFileResultOuputDto Add(IFormFile file, string EqRepairId, string EqRepaieDetailId,string type)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = file.FileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    var filenameExt = UploadFile(fileName, data, EqRepairId, EqRepaieDetailId,type);

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
        /// <param name="EqRepairId"></param>
        /// <param name="EqRepaieDetailId"></param>
        /// <param name="type"></param>
        private string UploadFile(string fileName, byte[] fileBuffers, string EqRepairId,string EqRepaieDetailId,string type)
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
            string typeFileName = "";
            switch (type) {
                case "RC":
                    typeFileName = "維修中近照";
                    break;
                case "RF":
                    typeFileName = "維修中遠照";
                    break;
                case "RAC":
                    typeFileName = "維修後近照";
                    break;
                case "RAF":
                    typeFileName = "維修後遠照";
                    break;
                default:
                    break;
            }
            fileName = DateTime.Now.ToString("yyyyMMddHHmmss")+ $"{typeFileName}" + fileName;

            Yuebon.Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Yuebon.Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";

                string uploadPath = @$"{path}EqRepair\{EqRepairId}\{EqRepaieDetailId}\";
                if (!System.IO.Directory.Exists(uploadPath)) {
                    Directory.CreateDirectory(@$"{uploadPath}\");
                }
                using (var fs = new FileStream(@$"{uploadPath}\" + fileName, FileMode.Create)) {
                    fs.Write(fileBuffers, 0, fileBuffers.Length);
                    fs.Close();
                }
            
            return fileName;
        }
    }
}