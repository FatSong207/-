using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Log;
using Yuebon.Commons.Models;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.IServices;
using Microsoft.AspNetCore.Http;
using System.IO;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Json;
using Yuebon.Security.Dtos;
using System.Linq;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    public class ContractAttachmentController : AreaApiController<ContractAttachment, ContractAttachmentOutputDto,ContractAttachmentInputDto,IContractAttachmentService,string>
    {
        private readonly IUserService userService;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_userService"></param>
        public ContractAttachmentController(IContractAttachmentService _iService, IUserService _userService) : base(_iService)
        {
            iService = _iService;
            userService = _userService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(ContractAttachment info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
        }
        
        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(ContractAttachment info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        ///  上傳其他附件
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("UploadMinorAttachment")]
        [NoSignRequired]
        public async Task<IActionResult> UploadMinorAttachment([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            var actualFilelist = filelist.Distinct();

            try {
                string CID = "";
                if (formCollection.ContainsKey("CID")) {
                    CID = formCollection["CID"];
                }

                string cCate = string.Empty;
                if (formCollection.ContainsKey("CCate")) {
                    cCate = formCollection["CCate"];
                }

                Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
                Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Security.Models.SysSetting>();
                string typeName = ContractController.GetTypeName(formCollection["CType"]);
                string uploadPath = sysSetting.ChaochiFilepath + $"Contract/{typeName}/{cCate}/{CID}/次要附件/";

                var minorAttachment = new UploadFileResultOuputDto();
                string CreatorUserId = CurrentUser?.UserId;
                string creatorUserDeptId = CurrentUser.DeptId;
                User user = await userService.GetUserByLogin(CurrentUser.Account);
                string userName = (user != null) ? user.RealName : "";
                DateTime CreatorTime = DateTime.Now;
                foreach (var file in actualFilelist) {
                    minorAttachment = AddFile(file, uploadPath, "");
                    ContractAttachment attachment = new ContractAttachment();
                    attachment.Id = GuidUtils.CreateNo();
                    attachment.CID = CID;
                    attachment.FormName = minorAttachment.FileName;
                    attachment.UploadTime = CreatorTime;
                    attachment.UploadUserId = userName;
                    attachment.CreatorTime = CreatorTime;
                    attachment.CreatorUserId = CreatorUserId;

                    long aln = await iService.InsertAsync(attachment).ConfigureAwait(false);
                }              

                result.ResData = minorAttachment;

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
        /// 單個上傳文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="uploadPath">上傳路徑</param>
        /// <param name="fileName">檔名</param>
        /// <returns></returns>
        private Security.Dtos.UploadFileResultOuputDto AddFile(IFormFile file, string uploadPath, string fileName)
        {
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    fileName = string.IsNullOrWhiteSpace(fileName) ? file.FileName : fileName + ".pdf";

                    var data = binaryReader.ReadBytes((int)file.Length);
                    UploadFile(fileName, uploadPath, data);

                    Security.Dtos.UploadFileResultOuputDto uploadFileResultOuputDto = new Security.Dtos.UploadFileResultOuputDto
                    {
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
        /// 上傳文件到伺服器保存
        /// </summary>
        /// <param name="fileName">文件名稱</param>
        /// <param name="uploadPath">上傳路徑</param>
        /// <param name="fileBuffers">文件位元組</param>
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
    }
}