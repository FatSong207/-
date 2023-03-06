using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Security.Application;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.WebApi.Controllers
{
    /// <summary>
    /// 文件上傳
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ApiController
    {

        private string _filePath;
        private string _dbFilePath;   //數據庫中的文件路徑
        private string _dbThumbnail;   //數據庫中的縮略圖路徑
        private string _belongApp;//所屬應用
        private string _belongAppId;//所屬應用ID 
        private string _fileName;//文件名稱
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FilesController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        ///  單文件上傳接口
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("Upload")]
        [NoSignRequired]
        public IActionResult Upload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            _fileName = filelist[0].FileName;
            try
            {
                result.ResData = Add(filelist[0], belongApp, belongAppId);
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
        /// <summary>
        ///  批量上傳文件接口
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("Uploads")]
        public IActionResult  Uploads([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();
            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            try
            {
               result.ResData = Adds(filelist, belongApp, belongAppId);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("", ex);
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
            }

            return ToJsonContent(result);
        }
        /// <summary>
        /// 刪除文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("DeleteFile")]
        public IActionResult DeleteFile(string id)
        {
            CommonResult result = new CommonResult();
            try
            {
                UploadFile uploadFile = new UploadFileApp().Get(id);

                YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<SysSetting>();
                string localpath = _hostingEnvironment.WebRootPath;
                if (uploadFile != null)
                {
                    string filepath = (localpath + "/" + uploadFile.FilePath).ToFilePath();
                    if (System.IO.File.Exists(filepath))
                        System.IO.File.Delete(filepath);
                    string filepathThu = (localpath + "/" + uploadFile.Thumbnail).ToFilePath();
                    if (System.IO.File.Exists(filepathThu))
                        System.IO.File.Delete(filepathThu);

                    result.ErrCode = ErrCode.successCode;
                    result.Success = true;
                }
                else
                {
                    result.ErrCode = ErrCode.failCode;
                    result.Success = false;
                }

            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("", ex);
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 批量上傳文件
        /// </summary>
        /// <param name="files">文件</param>
        /// <param name="belongApp">所屬應用，如文章article</param>
        /// <param name="belongAppId">所屬應用ID，如文章id</param>
        /// <returns></returns>
        private List<UploadFileResultOuputDto> Adds(IFormFileCollection files, string belongApp, string belongAppId)
        {
            List<UploadFileResultOuputDto> result = new List<UploadFileResultOuputDto>();
            foreach (var file in files)
            {
                if (file != null)
                {
                    result.Add(Add(file, belongApp, belongAppId));
                }
            }
            return result;
        }
        /// <summary>
        /// 單個上傳文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="belongApp"></param>
        /// <param name="belongAppId"></param>
        /// <returns></returns>
        private UploadFileResultOuputDto Add(IFormFile file, string belongApp, string belongAppId)
        {
            _belongApp = belongApp;
            _belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760)
            {
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    var fileName = string.Empty;
                        fileName = _fileName;
                    
                    var data = binaryReader.ReadBytes((int)file.Length);
                    UploadFile(fileName, data);
                    ;
                    UploadFile filedb = new UploadFile
                    {
                        Id = GuidUtils.CreateNo(),
                        FilePath = _dbFilePath,
                        Thumbnail = _dbThumbnail,
                        FileName = fileName,
                        FileSize = file.Length.ToInt(),
                        FileType = Path.GetExtension(fileName),
                        Extension = Path.GetExtension(fileName),
                        BelongApp = _belongApp,
                        BelongAppId = _belongAppId
                    };
                    new UploadFileApp().Insert(filedb);
                    UploadFileResultOuputDto uploadFileResultOuputDto = filedb.MapTo<UploadFileResultOuputDto>();
                    uploadFileResultOuputDto.PhysicsFilePath = (_hostingEnvironment.WebRootPath + "/"+ _dbThumbnail).ToFilePath(); ;
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
        private void UploadFile(string fileName, byte[] fileBuffers)
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

            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<SysSetting>();
            string folder = DateTime.Now.ToString("yyyyMMdd");
            _filePath = _hostingEnvironment.WebRootPath;
            var _tempfilepath = sysSetting.Filepath;

            if (!string.IsNullOrEmpty(_belongApp))
            {
                _tempfilepath += "/"+_belongApp;
            }
            if (!string.IsNullOrEmpty(_belongAppId))
            {
                _tempfilepath += "/" + _belongAppId;
            }
            if (sysSetting.Filesave == "1")
            {
                _tempfilepath = _tempfilepath + "/" + folder + "/";
            }
            if (sysSetting.Filesave == "2")
            {
                DateTime date = DateTime.Now;
                _tempfilepath = _tempfilepath + "/" + date.Year + "/" + date.Month + "/" + date.Day + "/";
            }

            var uploadPath = _filePath +"/"+ _tempfilepath;
            if (sysSetting.Fileserver == "localhost")
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
            }
            string ext = Path.GetExtension(fileName).ToLower();
            string newName = GuidUtils.CreateNo();
            string newfileName= newName + ext;

            using (var fs = new FileStream(uploadPath + newfileName, FileMode.Create))
            {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
                //生成縮略圖
                if (ext.Contains(".jpg") || ext.Contains(".jpeg") || ext.Contains(".png") || ext.Contains(".bmp") || ext.Contains(".gif"))
                {
                    string thumbnailName = newName + "_" + sysSetting.Thumbnailwidth + "x" + sysSetting.Thumbnailheight + ext;
                    ImgHelper.MakeThumbnail(uploadPath + newfileName, uploadPath + thumbnailName, sysSetting.Thumbnailwidth.ToInt(), sysSetting.Thumbnailheight.ToInt());
                    _dbThumbnail = _tempfilepath +  thumbnailName;
                }
                _dbFilePath = _tempfilepath + newfileName;
            }
        }
    }
}
