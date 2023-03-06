
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Yuebon.Commons.Core.App;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IoC;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Options;
using Yuebon.Commons.Pages;
using Yuebon.Security.Dtos;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;
using Yuebon.Security.Services;

namespace Yuebon.Security.Application
{
    /// <summary>
    /// 文件上傳
    /// </summary>
    public class UploadFileApp
    {
        private ILogger<UploadFileApp> _logger;
        private string _filePath;
        private string _dbFilePath;   //數據庫中的文件路徑
        private string _dbThumbnail;   //數據庫中的縮略圖路徑
        private string _belongApp;//所屬應用
        private string _belongAppId;//所屬應用ID
        IUploadFileService service = App.GetService<IUploadFileService>();
        /// <summary>
        /// 
        /// </summary>
        public UploadFileApp()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="setOptions"></param>
        /// <param name="logger"></param>
        public UploadFileApp(IOptions<AppSetting> setOptions, ILogger<UploadFileApp> logger)
        {
            _logger = logger;
            _filePath = setOptions.Value.LocalPath;
            if (string.IsNullOrEmpty(_filePath))
            {
                _filePath = AppContext.BaseDirectory;
            }
        }
        /// <summary>
        /// 根據應用Id和應用標識批量更新數據
        /// </summary>
        /// <param name="belongAppId">應用Id</param>
        /// <param name="oldBeLongAppId">更新前舊的應用Id</param>
        /// <param name="beLongApp">應用標識</param>
        /// <returns></returns>
        public bool UpdateByBeLongAppId(string belongAppId, string oldBeLongAppId, string beLongApp = null)
        {
            return service.UpdateByBeLongAppId(belongAppId, oldBeLongAppId, beLongApp);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long Insert(UploadFile info)
        {
            return service.Insert(info);
        }
        /// <summary>
        /// 同步查詢單個實體。
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <returns></returns>
        public UploadFile Get(string id)
        {
            return service.Get(id);
        }
        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns>指定對象的集合</returns>
        public List<UploadFileOutputDto> FindWithPager(string condition, PagerInfo info, string fieldToSort, bool desc)
        {
            return service.FindWithPager(condition,info,fieldToSort, desc,null).MapTo<UploadFileOutputDto>();
        }
        /// <summary>
        /// 批量上傳文件
        /// </summary>
        /// <param name="files">文件</param>
        /// <param name="belongApp">所屬應用，如文章article</param>
        /// <param name="belongAppId">所屬應用ID，如文章id</param>
        /// <returns></returns>
        public List<UploadFileResultOuputDto> Adds(IFormFileCollection files,string belongApp, string belongAppId)
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
        /// <param name="belongApp">所屬應用，如文章article</param>
        /// <param name="belongAppId">所屬應用ID，如文章id</param>
        /// <returns></returns>
        public UploadFileResultOuputDto Add(IFormFile file, string belongApp, string belongAppId)
        {
            _belongApp = belongApp;
            _belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760)
            {
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var data = binaryReader.ReadBytes((int)file.Length);
                    UploadFile(fileName, data);

                    UploadFile filedb = new UploadFile
                    {
                        FilePath = _dbFilePath,
                        Thumbnail = _dbThumbnail,
                        FileName = fileName,
                        FileSize = file.Length.ToInt(),
                        FileType = Path.GetExtension(fileName),
                        Extension = Path.GetExtension(fileName),
                        BelongApp=_belongApp,
                        BelongAppId=_belongAppId
                    };
                    service.Insert(filedb);
                    return filedb.MapTo<UploadFileResultOuputDto>();
                }
            }
            else
            {
                Log4NetHelper.Error("文件過大");
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
            string folder = DateTime.Now.ToString("yyyyMMdd");

            //判斷文件是否為空
            if (string.IsNullOrEmpty(fileName))
            {
                Log4NetHelper.Error("文件名不能為空");
                throw new Exception("文件名不能為空");
            }

            //判斷文件是否為空
            if (fileBuffers.Length < 1)
            {
                Log4NetHelper.Error("文件不能為空");
                throw new Exception("文件不能為空");
            }
            var _tempfilepath = "/upload/" + _belongApp + "/" + folder + "/";
            var uploadPath = _filePath + _tempfilepath;
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            var ext = Path.GetExtension(fileName).ToLower();
            string newName = GuidUtils.CreateNo() + ext;

            using (var fs = new FileStream(uploadPath + newName, FileMode.Create))
            {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
                //生成縮略圖
                if (ext.Contains(".jpg") || ext.Contains(".jpeg") || ext.Contains(".png") || ext.Contains(".bmp") || ext.Contains(".gif"))
                {
                    string thumbnailName = GuidUtils.CreateNo() + ext;
                    ImgHelper.MakeThumbnail(uploadPath + newName, uploadPath + thumbnailName);
                    _dbThumbnail = folder + "/" + thumbnailName;
                }
                _dbFilePath = _tempfilepath + "/" + newName;
            }
        }

        /// <summary>
        /// 統計上傳內容數
        /// </summary>
        /// <returns></returns>
        public int GetCountTotal()
        {
            return service.GetCountByWhere("1=1");
        }
    }
}
