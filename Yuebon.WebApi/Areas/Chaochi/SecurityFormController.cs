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
using Microsoft.AspNetCore.StaticFiles;
using Yuebon.Commons.Json;
using Yuebon.Chaochi.Core.IServices;
using Yuebon.Security.Dtos;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Yuebon.WebApi.Areas.Chaochi
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class SecurityFormController : AreaApiController<SecurityFormC, SecurityFormCOutputDto, SecurityFormCInputDto, ISecurityFormCService, string>
    {
        private readonly ISecurityFormService _securityFormService;
        private readonly IInternalformService _internalformService;
        private readonly IBuildingService _buildingService;
        private readonly IHistoryFormBuildingService _historyFormBuildingService;
        private readonly ISecurityFormGService _securityFormGService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="securityFormService"></param>
        /// <param name="internalformService"></param>
        /// <param name="securityFormGService"></param>
        /// <param name="historyFormBuildingService"></param>
        /// <param name="buildingService"></param>
        public SecurityFormController(ISecurityFormCService _iService, ISecurityFormService securityFormService, IInternalformService internalformService, ISecurityFormGService securityFormGService, IHistoryFormBuildingService historyFormBuildingService, IBuildingService buildingService) : base(_iService)
        {
            iService = _iService;
            _securityFormService = securityFormService;
            _internalformService = internalformService;
            _buildingService = buildingService;
            _historyFormBuildingService = historyFormBuildingService;
            _securityFormGService = securityFormGService;
        }

        #region 市府版-租安照片表單

        [HttpGet("GetimgslistC")]
        public virtual async Task<CommonResult<FileInfoOutputDto>> GetimgslistC(string BAdd)
        {
            CommonResult<FileInfoOutputDto> result = new CommonResult<FileInfoOutputDto>();
            var BId = _buildingService.GetIdByBAdd(BAdd);
            var imgs = await iService.GetImgOutDtoAsync(BId);
            if (imgs != null) {
                result.ErrCode = ErrCode.successCode;
                result.ResData = imgs;
            } else {
                result.Success = false;
                result.ErrMsg = "該建物不存在";
                result.ErrCode = "50001";
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        [HttpGet("ShowImgC")]
        [LogNotMethod]
        //public IActionResult Get(string fileName)
        public async Task<IActionResult> ShowImgC(string fileName, string BAdd)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

            var BId = _buildingService.GetIdByBAdd(BAdd);
            var title = iService.GetWhere($"BuildingId='{BId}' and fileName='{fileName}'").title;
            var image = System.IO.File.OpenRead($"{sysSetting.ChaochiFilepath}BuildingImage\\{BId}\\SecurityFormC\\{title}\\{fileName}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
            return File(image, contentType ?? "image/jpeg");
            //return File(image, "application/octet-stream");
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
            var filelist2 = filelist.Distinct();
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            //string _fileName = filelist[0].FileName;
            try {
                var buildingId = _buildingService.GetIdByBAdd(formCollection["BAdd"]);
                var dataList = iService.GetListWhere($"BuildingId='{buildingId}'");
                var hasData = dataList.Count() > 0;
                var startseq = 0;
                if (hasData) {
                    startseq = dataList.Select(x => Convert.ToInt32(x.seq)).Max();
                }
                var title = formCollection["title"];
                var address = new UploadFileResultOuputDto();
                for (int i = 0; i < filelist.Count; i++) {
                    address = Add(filelist[i], i.ToString(), buildingId, title, belongApp, belongAppId);
                    var securityFormC = new SecurityFormC() {
                        BuildingId = buildingId,
                        seq = (startseq + 1 + i).ToString(),
                        title = title,
                        fileName = address.FileName,
                        uploadTime = DateTime.Now,
                    };
                    await iService.InsertAsync(securityFormC);
                }

                result.ResData = address;
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
        /// 產生PDF
        /// </summary>
        /// <param name="BAdd"></param>
        /// <param name="Vno"></param>
        /// <param name="securityFormCInputDto"></param>
        /// <returns></returns>
        [HttpPost("GenPDFC")]
        public IActionResult GenPDFC(string BAdd, string Vno, List<SecurityFormCInputDto> securityFormCInputDto)
        {
            Document myDoc = new Document(PageSize.A4);
            var buildingId = _buildingService.GetIdByBAdd(BAdd);

            var all = "";
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            string chinese = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "KAIU.TTF");
            BaseFont baseFont = BaseFont.CreateFont(chinese, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont);
            iTextSharp.text.Font font2 = new iTextSharp.text.Font(baseFont, size: 12f, style: 1);
            var newLine = new Paragraph(Environment.NewLine);
            var enter = new Paragraph(" \n ") {
                Alignment = Element.ALIGN_CENTER
            };
            //更新table seq
            var result = new CommonResult();
            for (int i = 0; i < securityFormCInputDto.Count; i++) {
                //var imgsId = iService.GetWhere($"BuildingId='{buildingId}' and seq='{securityFormCInputDto[i].seq}'").Id;
                var r = iService.UpdateTableField("seq", i + 1, $"Id='{securityFormCInputDto[i].Id}'");
            }

            var filenameTo = DateTime.Now.ToString("yyyyMMddHHmmss") + buildingId + "市府版-租安照片.pdf";
            if (!Directory.Exists(@$"{path}historyPDF\Building\{buildingId}\")) {
                Directory.CreateDirectory(@$"{path}historyPDF\Building\{buildingId}\");
            }
            var fs = new FileStream(@$"{path}historyPDF\Building\{buildingId}\{filenameTo}", FileMode.Create);
            var writer = PdfWriter.GetInstance(myDoc, fs);

            //此類為達成AOP要覆寫OnStartPage()，為了增加版本號
            var pageEvent = new HeaderPageEvent(Vno, font2);

            foreach (var item in securityFormCInputDto) {
                all += @$"{path}BuildingImage\{buildingId}\SecurityFormC\{item.title}\{item.fileName};;";
            }
            try {
                var files = all.Split(";;").Where(x => x != "").ToArray();
                myDoc.Open();
                var title = new Paragraph($"六、房屋現況及居住安全檢查項目之照片\n", font2);
                title.Alignment = Element.ALIGN_LEFT;

                //建立表頭的樣板(表頭+版本)
                PdfPTable vnoTable = new PdfPTable(new float[] { 1f });
                vnoTable.WidthPercentage = 10;
                vnoTable.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell vnoCell = new PdfPCell(new Phrase(Vno + " 版", font2));
                vnoTable.AddCell(vnoCell);

                PdfPTable titleTable = new PdfPTable(new float[] { 1f, 0.14f });
                titleTable.WidthPercentage = 100f;
                PdfPCell titleCell1 = new PdfPCell(title); //表頭
                titleCell1.Border = Rectangle.NO_BORDER;
                titleTable.AddCell(titleCell1);
                PdfPCell titleCell2 = new PdfPCell(vnoTable); //版本
                titleTable.AddCell(titleCell2);
                myDoc.Add(titleTable);

                myDoc.Add(newLine);

                //此時才將自己實作的pageevent附加上去(附加上去之後就會觸發自己複寫的OnPageStart())
                writer.PageEvent = pageEvent;

                for (int i = 0; i < files.Length; i += 2) {
                    PdfPTable basicTable = new PdfPTable(new float[] { 1f, 1f });
                    basicTable.WidthPercentage = 100f;
                    var filename1 = files[i].Split(@"\");
                    PdfPCell cellfilename1 = new PdfPCell(new Phrase(filename1[filename1.Length - 2], font));
                    basicTable.AddCell(cellfilename1);
                    if (i + 1 != files.Length) {
                        var filename2 = files[i + 1].Split(@"\");
                        PdfPCell cellfilename2 = new PdfPCell(new Phrase(filename2[filename2.Length - 2], font));
                        basicTable.AddCell(cellfilename2);
                    } else {
                        basicTable.AddCell(new PdfPCell(new Phrase("", font)));
                    }
                    iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(files[i]);
                    image1.ScaleToFit(260f, 150f);
                    PdfPCell cell = new PdfPCell(image1);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.UseBorderPadding = true;
                    cell.BackgroundColor = new BaseColor(235, 235, 235);
                    cell.FixedHeight = 151f;
                    basicTable.AddCell(cell);
                    if (i + 1 != files.Length) {
                        iTextSharp.text.Image image2 = iTextSharp.text.Image.GetInstance(files[i + 1]);
                        image2.ScaleToFit(260f, 150f);
                        PdfPCell cell2 = new PdfPCell(image2);
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell2.UseBorderPadding = true;
                        cell2.BackgroundColor = new BaseColor(235, 235, 235);
                        cell2.FixedHeight = 151f;
                        basicTable.AddCell(cell2);
                    } else {
                        basicTable.AddCell(new PdfPCell(new Phrase("", font)));
                    }
                    basicTable.KeepTogether = true;
                    myDoc.Add(basicTable);//加入table


                    myDoc.Add(enter);

                };
                var historyB = new HistoryFormBuilding() {
                    IDNo = buildingId,
                    FormName = "市府版-租安照片",
                    FileName = filenameTo,
                    UploadTime = DateTime.Now,
                    Note = "default note",
                    CreatorUserId = CurrentUser.UserId
                };
                var insertreuslt = _historyFormBuildingService.Insert(historyB);
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } catch (Exception ex) {
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                Log4NetHelper.Error("製作PDF失敗", ex);
            } finally {
                if (myDoc.IsOpen()) {
                    myDoc.Close();
                    fs.Close();
                    writer.Close();
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 預覽PDF
        /// </summary>
        /// <param name="BAdd"></param>
        /// <param name="Vno"></param>
        /// <param name="securityFormCInputDto"></param>
        /// <returns></returns>
        [HttpPost("PreviewPDFC")]
        public IActionResult PreviewPDFC(string BAdd, string Vno, List<SecurityFormCInputDto> securityFormCInputDto)
        {
            Document myDoc = new Document(PageSize.A4);
            var buildingId = _buildingService.GetIdByBAdd(BAdd);
            var all = "";
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            string chinese = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "KAIU.TTF");
            BaseFont baseFont = BaseFont.CreateFont(chinese, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont);
            iTextSharp.text.Font font2 = new iTextSharp.text.Font(baseFont, size: 12f, style: 1);
            var newLine = new Paragraph(Environment.NewLine);
            var enter = new Paragraph(" \n ") {
                Alignment = Element.ALIGN_CENTER
            };
            using (var stream = new MemoryStream()) {
                var writer = PdfWriter.GetInstance(myDoc, stream);

                //此類為達成AOP要覆寫OnStartPage()，為了增加版本號
                var pageEvent = new HeaderPageEvent(Vno,font2);
                foreach (var item in securityFormCInputDto) {
                    all += @$"{path}BuildingImage\{buildingId}\SecurityFormC\{item.title}\{item.fileName};;";
                }
                var files = all.Split(";;").Where(x => x != "").ToArray();
                myDoc.Open();

                var title = new Paragraph($"六、房屋現況及居住安全檢查項目之照片\n", font2);
                title.Alignment = Element.ALIGN_LEFT;

                //建立表頭的樣板(表頭+版本)
                PdfPTable vnoTable = new PdfPTable(new float[] { 1f });
                vnoTable.WidthPercentage = 10;
                vnoTable.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell vnoCell = new PdfPCell(new Phrase(Vno+" 版", font2));
                vnoTable.AddCell(vnoCell);

                PdfPTable titleTable = new PdfPTable(new float[] { 1f, 0.14f });
                titleTable.WidthPercentage = 100f;
                PdfPCell titleCell1 = new PdfPCell(title); //表頭
                titleCell1.Border = Rectangle.NO_BORDER;
                titleTable.AddCell(titleCell1);
                PdfPCell titleCell2 = new PdfPCell(vnoTable); //版本
                titleTable.AddCell(titleCell2);
                myDoc.Add(titleTable);
                myDoc.Add(newLine);

                //此時才將自己實作的pageevent附加上去(附加上去之後就會觸發自己複寫的OnPageStart())
                writer.PageEvent = pageEvent;

                for (int i = 0; i < files.Length; i += 2) {
                    PdfPTable basicTable = new PdfPTable(new float[] { 1f, 1f });
                    basicTable.WidthPercentage = 100f;
                    var filename1 = files[i].Split(@"\");
                    PdfPCell cellfilename1 = new PdfPCell(new Phrase(filename1[filename1.Length - 2], font));
                    basicTable.AddCell(cellfilename1);
                    if (i + 1 != files.Length) {
                        var filename2 = files[i + 1].Split(@"\");
                        PdfPCell cellfilename2 = new PdfPCell(new Phrase(filename2[filename2.Length - 2], font));
                        basicTable.AddCell(cellfilename2);
                    } else {
                        basicTable.AddCell(new PdfPCell(new Phrase("", font)));
                    }
                    iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(files[i]);
                    image1.ScaleToFit(260f, 150f);
                    PdfPCell cell = new PdfPCell(image1);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.UseBorderPadding = true;
                    cell.BackgroundColor = new BaseColor(235, 235, 235);
                    cell.FixedHeight = 151f;
                    basicTable.AddCell(cell);
                    if (i + 1 != files.Length) {
                        iTextSharp.text.Image image2 = iTextSharp.text.Image.GetInstance(files[i + 1]);
                        image2.ScaleToFit(260f, 150f);
                        PdfPCell cell2 = new PdfPCell(image2);
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell2.UseBorderPadding = true;
                        cell2.BackgroundColor = new BaseColor(235, 235, 235);
                        cell2.FixedHeight = 151f;
                        basicTable.AddCell(cell2);
                    } else {
                        basicTable.AddCell(new PdfPCell(new Phrase("", font)));
                    }
                    basicTable.KeepTogether = true;
                    myDoc.Add(basicTable);//加入table


                    myDoc.Add(enter);
                };
                myDoc.Close();
                writer.Close();

                stream.Seek(0, SeekOrigin.Begin);
                string contentType;
                new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
                return File(stream.ToArray(), contentType ?? "image/jpeg");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="i"></param>
        /// <param name="buildingId"></param>
        /// <param name="title"></param>
        /// <param name="belongApp"></param>
        /// <param name="belongAppId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private Yuebon.Security.Dtos.UploadFileResultOuputDto Add(IFormFile file, string i, string buildingId, string title, string belongApp, string belongAppId)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = file.FileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    var filenameExt = UploadFile(i + fileName, data, buildingId, title);

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
        /// <param name="buildingId"></param>
        /// <param name="title"></param>
        private string UploadFile(string fileName, byte[] fileBuffers, string buildingId, string title)
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

            string uploadPath = @$"{path}BuildingImage\{buildingId}\SecurityFormC\";
            if (!System.IO.File.Exists(uploadPath + title + "\\")) {
                Directory.CreateDirectory(uploadPath + title + "\\");
            }
            using (var fs = new FileStream(uploadPath + title + "\\" + fileName, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }

            return fileName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="BAdd"></param>
        /// <param name="myFileInfo"></param>
        /// <returns></returns>
        [HttpPost("DeleteImgFileC")]
        public IActionResult DeleteImgFileC(string BAdd, MyFileInfo myFileInfo)
        {
            CommonResult result = new CommonResult();
            try {
                var buildingId = _buildingService.GetIdByBAdd(BAdd);
                YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
                DirectoryInfo infoD = new DirectoryInfo(sysSetting.ChaochiFilepath + @$"\BuildingImage\{buildingId}\SecurityFormC\{myFileInfo.title}");

                var files = infoD.GetFiles().Where(x => x.Name == myFileInfo.FileName).ToList();

                string localpath = sysSetting.ChaochiFilepath + $"\\BuildingImage\\{buildingId}\\SecurityFormC\\{myFileInfo.title}\\";
                if (files != null && files.Count == 1) {
                    string filepath = localpath + myFileInfo.FileName;
                    if (System.IO.File.Exists(filepath)) {
                        System.IO.File.Delete(filepath);
                        iService.DeleteBatchWhere($"Id='{myFileInfo.Id}'");
                    }
                    result.ErrCode = ErrCode.successCode;
                    result.Success = true;
                } else {
                    result.ErrCode = ErrCode.failCode;
                    result.Success = false;
                }

            } catch (Exception ex) {
                Log4NetHelper.Error("", ex);
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
            }
            return ToJsonContent(result);
        }
        #endregion 市府版-租安照片表單

        #region 公會版-租安照片表單
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        [HttpGet("GetimgslistG")]
        public virtual async Task<CommonResult<FileInfoOutputDto>> GetimgslistG(string BAdd)
        {
            CommonResult<FileInfoOutputDto> result = new CommonResult<FileInfoOutputDto>();
            var BId = _buildingService.GetIdByBAdd(BAdd);
            var imgs = await _securityFormGService.GetImgOutDtoAsync(BId);
            if (imgs != null) {
                result.ErrCode = ErrCode.successCode;
                result.ResData = imgs;
            } else {
                result.Success = false;
                result.ErrMsg = "該建物不存在";
                result.ErrCode = "50001";
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        [HttpGet("ShowImgG")]
        [LogNotMethod]
        //public IActionResult Get(string fileName)
        public async Task<IActionResult> ShowImgG(string fileName, string BAdd)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

            var BId = _buildingService.GetIdByBAdd(BAdd);
            var title = _securityFormGService.GetWhere($"BuildingId='{BId}' and fileName='{fileName}'").title;
            var image = System.IO.File.OpenRead($"{sysSetting.ChaochiFilepath}BuildingImage\\{BId}\\SecurityFormG\\{title}\\{fileName}");
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
            return File(image, contentType ?? "image/jpeg");
            //return File(image, "application/octet-stream");
        }

        /// <summary>
        ///  單文件上傳接口
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("ImgUploadG")]
        [NoSignRequired]
        public async Task<IActionResult> ImgUploadG([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            //string _fileName = filelist[0].FileName;
            try {
                var buildingId = _buildingService.GetIdByBAdd(formCollection["BAdd"]);
                var dataList = _securityFormGService.GetListWhere($"BuildingId='{buildingId}'");
                var hasData = dataList.Count() > 0;
                var startseq = 0;
                if (hasData) {
                    startseq = dataList.Select(x => Convert.ToInt32(x.seq)).Max();
                }
                var title = formCollection["title"];
                var address = new UploadFileResultOuputDto();
                if (filelist.Count != 0) {
                    for (int i = 0; i < filelist.Count; i++) {
                        address = AddG(filelist[i], i.ToString(), buildingId, title, belongApp, belongAppId);
                        var securityFormG = new SecurityFormG() {
                            BuildingId = buildingId,
                            seq = (startseq + 1 + i).ToString(),
                            title = title,
                            size = formCollection["size"],
                            isAppear = "1",
                            fileName = address.FileName,
                            uploadTime = DateTime.Now,
                        };
                        await _securityFormGService.InsertAsync(securityFormG);
                    }
                } else {
                    CreateDirInfo(buildingId, title);
                    var securityFormG = new SecurityFormG() {
                        BuildingId = buildingId,
                        seq = (startseq + 1).ToString(),
                        title = title,
                        size = formCollection["size"],
                        isAppear = formCollection["isAppear"],
                        uploadTime = DateTime.Now,
                    };
                    await _securityFormGService.InsertAsync(securityFormG);

                }


                result.ResData = address;
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
        /// 產生PDF
        /// </summary>
        /// <param name="BAdd"></param>
        /// <param name="Vno"></param>
        /// <param name="securityFormGInputDto"></param>
        /// <returns></returns>
        [HttpPost("GenPDFG")]
        public IActionResult GenPDFG(string BAdd, string Vno, List<SecurityFormGInputDto> securityFormGInputDto)
        {
            Document myDoc = new Document(PageSize.A4);
            var buildingId = _buildingService.GetIdByBAdd(BAdd);
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            string chinese = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "KAIU.TTF");
            BaseFont baseFont = BaseFont.CreateFont(chinese, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font2 = new iTextSharp.text.Font(baseFont, size: 12f, style: 1);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont);
            var newLine = new Paragraph(Environment.NewLine);
            var enter = new Paragraph(" \n ") {
                Alignment = Element.ALIGN_CENTER
            };
            //更新table seq
            var result = new CommonResult();
            for (int i = 0; i < securityFormGInputDto.Count; i++) {
                //var imgsId = _securityFormGService.GetWhere($"BuildingId='{buildingId}' and seq='{securityFormGInputDto[i].seq}'").Id;
                var r = _securityFormGService.UpdateTableField("seq", i + 1, $"Id='{securityFormGInputDto[i].Id}'");
            }

            var filenameTo = DateTime.Now.ToString("yyyyMMddHHmmss") + buildingId + "公會版-租安照片.pdf";
            if (!Directory.Exists(@$"{path}historyPDF\Building\{buildingId}\")) {
                Directory.CreateDirectory(@$"{path}historyPDF\Building\{buildingId}\");
            }
            var fs = new FileStream(@$"{path}historyPDF\Building\{buildingId}\{filenameTo}", FileMode.Create);
            var writer = PdfWriter.GetInstance(myDoc, fs);
            //此類為達成AOP要覆寫OnStartPage()，為了增加版本號
            var pageEvent = new HeaderPageEvent(Vno, font2);
            try {
                foreach (var item in securityFormGInputDto) {
                    if (!string.IsNullOrEmpty(item.fileName)) {
                        item.fileName = @$"{path}BuildingImage\{buildingId}\SecurityFormG\{item.title}\{item.fileName}";
                    }
                }
                myDoc.Open();

                //文件標題
                var title = new Paragraph($"四、附件", font2);
                title.Alignment = Element.ALIGN_LEFT;
                var title2 = new Paragraph("(一) 必要項目", font2);
                title2.Alignment = Element.ALIGN_LEFT;


                //建立表頭的樣板(表頭+版本)
                PdfPTable vnoTable = new PdfPTable(new float[] { 1f });
                vnoTable.WidthPercentage = 10;
                vnoTable.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell vnoCell = new PdfPCell(new Phrase(Vno + " 版", font2));
                vnoTable.AddCell(vnoCell);

                PdfPTable titleTable = new PdfPTable(new float[] { 1f, 0.14f });
                titleTable.WidthPercentage = 100f;
                PdfPCell titleCell1 = new PdfPCell(title); //表頭
                titleCell1.Border = Rectangle.NO_BORDER;
                titleTable.AddCell(titleCell1);
                PdfPCell titleCell2 = new PdfPCell(vnoTable); //版本
                titleTable.AddCell(titleCell2);
                myDoc.Add(titleTable);
                myDoc.Add(title2);
                myDoc.Add(enter);

                //此時才將自己實作的pageevent附加上去(附加上去之後就會觸發自己複寫的OnPageStart())
                writer.PageEvent = pageEvent;
                //groupby成一對多(主題對照片)
                var list = securityFormGInputDto.GroupBy(x => x.title).ToList();
                for (int i = 0; i < list.Count(); i++) {
                    //當前主題所有物件
                    var keyName = list[i].Key;
                    var curTile = list[i].Where(x => x.isAppear != "0").ToList();
                    //主題
                    PdfPTable basicTable = new PdfPTable(new float[] { 1f });
                    basicTable.WidthPercentage = 50;
                    basicTable.HorizontalAlignment = Element.ALIGN_LEFT;
                    var fff = basicTable.SpacingAfter;
                    PdfPCell titleCell = new PdfPCell(new Phrase(keyName, font2));
                    basicTable.AddCell(titleCell);
                    myDoc.Add(basicTable);
                    //var titleAppear = curTile.Where(x => x.fileName != null || (x.fileName == null && x.isAppear == "1")).Count();
                    //if (titleAppear != 0) {

                    //}

                    //空行
                    myDoc.Add(newLine);

                    #region PDF製作
                    for (int i2 = 0; i2 < curTile.Count(); i2++) {
                        //當前主題第一張圖片為NULL(表示無上傳)
                        if (curTile[i2].fileName == null) {
                            //判斷是否要顯示
                            if (curTile[i2].isAppear == "1") {
                                //顯示保留圖框大小判斷(小)
                                if (curTile[i2].size == "M") {
                                    PdfPTable basicTable2 = new PdfPTable(new float[] { 1f, 1f });
                                    basicTable2.WidthPercentage = 100;
                                    PdfPCell nullCell = new PdfPCell();
                                    nullCell.FixedHeight = 151f;
                                    nullCell.BackgroundColor = new BaseColor(235, 235, 235);
                                    basicTable2.AddCell(nullCell);
                                    myDoc.Add(basicTable2);

                                    //若為最後一筆代表右側圖框持空
                                    if (i2 == curTile.Count() - 1) {
                                        PdfPCell nullCell3 = new PdfPCell();
                                        nullCell3.FixedHeight = 151f;
                                        nullCell3.BackgroundColor = new BaseColor(235, 235, 235);
                                        basicTable2.AddCell(nullCell3);
                                        myDoc.Add(basicTable2);

                                        //若下一張圖檔為NULL(表示無上傳)代表右側圖框持空
                                    } else if (curTile[i2 + 1].fileName == null) {
                                        if (curTile[i2 + 1].size == "M") {
                                            PdfPCell nullCell3 = new PdfPCell();
                                            nullCell3.FixedHeight = 151f;
                                            nullCell3.BackgroundColor = new BaseColor(235, 235, 235);
                                            basicTable2.AddCell(nullCell3);
                                            myDoc.Add(basicTable2);
                                        } else {
                                            PdfPCell nullCell3 = new PdfPCell();
                                            nullCell3.FixedHeight = 151f;
                                            nullCell3.BackgroundColor = new BaseColor(235, 235, 235);
                                            basicTable2.AddCell(nullCell3);
                                            myDoc.Add(basicTable2);
                                            PdfPTable basicTable3 = new PdfPTable(new float[] { 1f });
                                            basicTable3.WidthPercentage = 100;
                                            PdfPCell nullCell2 = new PdfPCell();
                                            nullCell2.FixedHeight = 301f;
                                            nullCell2.BackgroundColor = new BaseColor(235, 235, 235);
                                            basicTable3.AddCell(nullCell2);
                                            myDoc.Add(basicTable3);
                                        }

                                        //表示右側圖框不會為空
                                    } else {
                                        iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(curTile[i2 + 1].fileName);
                                        if (curTile[i2 + 1].size == "M") {
                                            image1.ScaleToFit(260f, 150f);
                                            PdfPCell cell = new PdfPCell(image1);
                                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                            cell.UseBorderPadding = true;
                                            cell.BackgroundColor = new BaseColor(235, 235, 235);
                                            cell.FixedHeight = 151f;
                                            basicTable2.AddCell(cell);
                                            myDoc.Add(basicTable2);
                                        } else {
                                            PdfPCell nullcell = new PdfPCell();
                                            nullcell.FixedHeight = 151f;
                                            nullcell.BackgroundColor = new BaseColor(235, 235, 235);
                                            basicTable2.AddCell(nullcell);
                                            myDoc.Add(basicTable2);
                                            PdfPTable basicTable3 = new PdfPTable(new float[] { 1f });
                                            basicTable3.WidthPercentage = 100;
                                            image1.ScaleToFit(520f, 300f);
                                            PdfPCell cell = new PdfPCell(image1);
                                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                            cell.UseBorderPadding = true;
                                            cell.BackgroundColor = new BaseColor(235, 235, 235);
                                            cell.FixedHeight = 301f;
                                            basicTable3.AddCell(cell);
                                            myDoc.Add(basicTable3);
                                        }

                                    }
                                    i2++;

                                    //顯示保留圖框大小判斷(大)
                                } else {
                                    PdfPTable basicTable2 = new PdfPTable(new float[] { 1f });
                                    basicTable2.WidthPercentage = 100;
                                    PdfPCell nullCell = new PdfPCell();
                                    nullCell.FixedHeight = 301f;
                                    nullCell.BackgroundColor = new BaseColor(235, 235, 235);
                                    basicTable2.AddCell(nullCell);
                                    myDoc.Add(basicTable2);
                                }
                            }
                            continue;
                        }
                        //正常顯示圖框
                        if (curTile[i2].size == "M" && curTile[i2].fileName != null) {
                            PdfPTable basicTable2 = new PdfPTable(new float[] { 1f, 1f });
                            basicTable2.WidthPercentage = 100;
                            iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(curTile[i2].fileName);
                            image1.ScaleToFit(260f, 150f);
                            PdfPCell cell = new PdfPCell(image1);
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.UseBorderPadding = true;
                            cell.BackgroundColor = new BaseColor(235, 235, 235);
                            cell.FixedHeight = 151f;
                            basicTable2.AddCell(cell);
                            if (i2 == curTile.Count() - 1) {
                                PdfPCell nullCell = new PdfPCell();
                                nullCell.BackgroundColor = new BaseColor(235, 235, 235);
                                basicTable2.AddCell(nullCell);
                            } else if (curTile[i2 + 1].size == "L") {
                                PdfPCell nullCell = new PdfPCell();
                                nullCell.BackgroundColor = new BaseColor(235, 235, 235);
                                basicTable2.AddCell(nullCell);
                            } else {
                                if (curTile[i2 + 1].fileName == null) {
                                    if (curTile[i2 + 1].isAppear == "1") {
                                        if (curTile[i2 + 1].size == "M") {
                                            PdfPCell nullCell = new PdfPCell();
                                            nullCell.FixedHeight = 151f;
                                            nullCell.BackgroundColor = new BaseColor(235, 235, 235);
                                            basicTable2.AddCell(nullCell);
                                        } else {
                                            PdfPCell nullCell = new PdfPCell();
                                            nullCell.FixedHeight = 151f;
                                            nullCell.BackgroundColor = new BaseColor(235, 235, 235);
                                            basicTable2.AddCell(nullCell);
                                            PdfPTable basicTable3 = new PdfPTable(new float[] { 1f });
                                            basicTable3.WidthPercentage = 100;
                                            PdfPCell nullCell2 = new PdfPCell();
                                            nullCell2.FixedHeight = 301f;
                                            nullCell2.BackgroundColor = new BaseColor(235, 235, 235);
                                            basicTable3.AddCell(nullCell2);
                                            myDoc.Add(basicTable3);
                                        }
                                    }
                                    i2++;
                                } else {
                                    iTextSharp.text.Image image2 = iTextSharp.text.Image.GetInstance(curTile[i2 + 1].fileName);
                                    image2.ScaleToFit(260f, 150f);
                                    PdfPCell cell2 = new PdfPCell(image2);
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                                    cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    cell2.UseBorderPadding = true;
                                    cell2.BackgroundColor = new BaseColor(235, 235, 235);
                                    cell2.FixedHeight = 151f;
                                    basicTable2.AddCell(cell2);
                                    i2++;
                                }
                            }
                            myDoc.Add(basicTable2);
                        } else if (curTile[i2].size == "L") {
                            PdfPTable basicTable2 = new PdfPTable(new float[] { 1f });
                            basicTable2.WidthPercentage = 100;
                            iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(curTile[i2].fileName);
                            image1.ScaleToFit(520f, 300f);
                            PdfPCell cell = new PdfPCell(image1);
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.UseBorderPadding = true;
                            cell.BackgroundColor = new BaseColor(235, 235, 235);
                            cell.FixedHeight = 301f;
                            basicTable2.AddCell(cell);
                            myDoc.Add(basicTable2);
                        }
                    }
                    #endregion
                    //空行
                    var enter1 = new Paragraph($"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}");
                    enter1.Alignment = Element.ALIGN_CENTER;
                    myDoc.Add(enter1);
                }


                var historyB = new HistoryFormBuilding() {
                    IDNo = buildingId,
                    FormName = "公會版-租安照片",
                    FileName = filenameTo,
                    UploadTime = DateTime.Now,
                    Note = "Web表單系統上傳",
                    CreatorUserId = CurrentUser.UserId
                };
                var insertreuslt = _historyFormBuildingService.Insert(historyB);
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            } catch (Exception ex) {
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
                Log4NetHelper.Error("製作PDF失敗", ex);
            } finally {
                if (myDoc.IsOpen()) {
                    myDoc.Close();
                    fs.Close();
                    writer.Close();
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 預覽PDF
        /// </summary>
        /// <param name="BAdd"></param>
        /// <param name="Vno"></param>
        /// <param name="securityFormGInputDto"></param>
        /// <returns></returns>
        [HttpPost("PreviewPDFG")]
        public IActionResult PreviewPDFG(string BAdd, string Vno, List<SecurityFormGInputDto> securityFormGInputDto)
        {
            Document myDoc = new Document(PageSize.A4);
            var buildingId = _buildingService.GetIdByBAdd(BAdd);
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            string chinese = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "KAIU.TTF");
            BaseFont baseFont = BaseFont.CreateFont(chinese, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font2 = new iTextSharp.text.Font(baseFont, size: 12f, style: 1);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont);
            var newLine = new Paragraph(Environment.NewLine);
            var enter = new Paragraph(" \n ") {
                Alignment = Element.ALIGN_CENTER
            };
            using (var stream = new MemoryStream()) {
                var writer = PdfWriter.GetInstance(myDoc, stream);
                //此類為達成AOP要覆寫OnStartPage()，為了增加版本號
                var pageEvent = new HeaderPageEvent(Vno, font2);
                foreach (var item in securityFormGInputDto) {
                    if (!string.IsNullOrEmpty(item.fileName)) {
                        item.fileName = @$"{path}BuildingImage\{buildingId}\SecurityFormG\{item.title}\{item.fileName}";
                    }
                }
                myDoc.Open();

                //文件標題
                var title = new Paragraph($"四、附件", font2);
                title.Alignment = Element.ALIGN_LEFT;
                var title2 = new Paragraph("(一) 必要項目",font2);
                title2.Alignment = Element.ALIGN_LEFT;
                

                //建立表頭的樣板(表頭+版本)
                PdfPTable vnoTable = new PdfPTable(new float[] { 1f });
                vnoTable.WidthPercentage = 10;
                vnoTable.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell vnoCell = new PdfPCell(new Phrase(Vno + " 版", font2));
                vnoTable.AddCell(vnoCell);

                PdfPTable titleTable = new PdfPTable(new float[] { 1f, 0.14f });
                titleTable.WidthPercentage = 100f;
                PdfPCell titleCell1 = new PdfPCell(title); //表頭
                titleCell1.Border = Rectangle.NO_BORDER;
                titleTable.AddCell(titleCell1);
                PdfPCell titleCell2 = new PdfPCell(vnoTable); //版本
                titleTable.AddCell(titleCell2);
                myDoc.Add(titleTable);
                myDoc.Add(title2);
                myDoc.Add(enter);

                //此時才將自己實作的pageevent附加上去(附加上去之後就會觸發自己複寫的OnPageStart())
                writer.PageEvent = pageEvent;

                //groupby成一對多(主題對照片)
                var list = securityFormGInputDto.GroupBy(x => x.title).ToList();
                for (int i = 0; i < list.Count(); i++) {
                    //當前主題所有物件
                    var keyName = list[i].Key;
                    var curTile = list[i].Where(x => x.isAppear != "0").ToList();
                    //主題
                    PdfPTable basicTable = new PdfPTable(new float[] { 1f });
                    basicTable.WidthPercentage = 50;
                    basicTable.HorizontalAlignment = Element.ALIGN_LEFT;
                    var fff = basicTable.SpacingAfter;
                    PdfPCell titleCell = new PdfPCell(new Phrase(keyName, font));
                    basicTable.AddCell(titleCell);
                    myDoc.Add(basicTable);
                    //空行
                    myDoc.Add(newLine);

                    #region PDF製作
                    for (int i2 = 0; i2 < curTile.Count(); i2++) {
                        //當前主題第一張圖片為NULL(表示無上傳)
                        if (curTile[i2].fileName == null) {
                            //判斷是否要顯示
                            if (curTile[i2].isAppear == "1") {
                                //顯示保留圖框大小判斷(小)
                                if (curTile[i2].size == "M") {
                                    PdfPTable basicTable2 = new PdfPTable(new float[] { 1f, 1f });
                                    basicTable2.WidthPercentage = 100;
                                    PdfPCell nullCell = new PdfPCell();
                                    nullCell.FixedHeight = 151f;
                                    nullCell.BackgroundColor = new BaseColor(235, 235, 235);
                                    basicTable2.AddCell(nullCell);
                                    myDoc.Add(basicTable2);

                                    //若為最後一筆代表右側圖框持空
                                    if (i2 == curTile.Count() - 1) {
                                        PdfPCell nullCell3 = new PdfPCell();
                                        nullCell3.FixedHeight = 151f;
                                        nullCell3.BackgroundColor = new BaseColor(235, 235, 235);
                                        basicTable2.AddCell(nullCell3);
                                        myDoc.Add(basicTable2);

                                        //若下一張圖檔為NULL(表示無上傳)代表右側圖框持空
                                    } else if (curTile[i2 + 1].fileName == null) {
                                        if (curTile[i2 + 1].size == "M") {
                                            PdfPCell nullCell3 = new PdfPCell();
                                            nullCell3.FixedHeight = 151f;
                                            nullCell3.BackgroundColor = new BaseColor(235, 235, 235);
                                            basicTable2.AddCell(nullCell3);
                                            myDoc.Add(basicTable2);
                                        } else {
                                            PdfPCell nullCell3 = new PdfPCell();
                                            nullCell3.FixedHeight = 151f;
                                            nullCell3.BackgroundColor = new BaseColor(235, 235, 235);
                                            basicTable2.AddCell(nullCell3);
                                            myDoc.Add(basicTable2);
                                            PdfPTable basicTable3 = new PdfPTable(new float[] { 1f });
                                            basicTable3.WidthPercentage = 100;
                                            PdfPCell nullCell2 = new PdfPCell();
                                            nullCell2.FixedHeight = 301f;
                                            nullCell2.BackgroundColor = new BaseColor(235, 235, 235);
                                            basicTable3.AddCell(nullCell2);
                                            myDoc.Add(basicTable3);
                                        }

                                        //表示右側圖框不會為空
                                    } else {
                                        iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(curTile[i2 + 1].fileName);
                                        if (curTile[i2 + 1].size == "M") {
                                            image1.ScaleToFit(260f, 150f);
                                            PdfPCell cell = new PdfPCell(image1);
                                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                            cell.UseBorderPadding = true;
                                            cell.BackgroundColor = new BaseColor(235, 235, 235);
                                            cell.FixedHeight = 151f;
                                            basicTable2.AddCell(cell);
                                            myDoc.Add(basicTable2);
                                        } else {
                                            PdfPCell nullcell = new PdfPCell();
                                            nullcell.FixedHeight = 151f;
                                            nullcell.BackgroundColor = new BaseColor(235, 235, 235);
                                            basicTable2.AddCell(nullcell);
                                            myDoc.Add(basicTable2);
                                            PdfPTable basicTable3 = new PdfPTable(new float[] { 1f });
                                            basicTable3.WidthPercentage = 100;
                                            image1.ScaleToFit(520f, 300f);
                                            PdfPCell cell = new PdfPCell(image1);
                                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                            cell.UseBorderPadding = true;
                                            cell.BackgroundColor = new BaseColor(235, 235, 235);
                                            cell.FixedHeight = 301f;
                                            basicTable3.AddCell(cell);
                                            myDoc.Add(basicTable3);
                                        }

                                    }
                                    i2++;

                                    //顯示保留圖框大小判斷(大)
                                } else {
                                    PdfPTable basicTable2 = new PdfPTable(new float[] { 1f });
                                    basicTable2.WidthPercentage = 100;
                                    PdfPCell nullCell = new PdfPCell();
                                    nullCell.FixedHeight = 301f;
                                    nullCell.BackgroundColor = new BaseColor(235, 235, 235);
                                    basicTable2.AddCell(nullCell);
                                    myDoc.Add(basicTable2);
                                }
                            }
                            continue;
                        }
                        //正常顯示圖框
                        if (curTile[i2].size == "M" && curTile[i2].fileName != null) {
                            PdfPTable basicTable2 = new PdfPTable(new float[] { 1f, 1f });
                            basicTable2.WidthPercentage = 100;
                            iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(curTile[i2].fileName);
                            image1.ScaleToFit(260f, 150f);
                            PdfPCell cell = new PdfPCell(image1);
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.UseBorderPadding = true;
                            cell.BackgroundColor = new BaseColor(235, 235, 235);
                            cell.FixedHeight = 151f;
                            basicTable2.AddCell(cell);
                            if (i2 == curTile.Count() - 1) {
                                PdfPCell nullCell = new PdfPCell();
                                nullCell.BackgroundColor = new BaseColor(235, 235, 235);
                                basicTable2.AddCell(nullCell);
                            } else if (curTile[i2 + 1].size == "L") {
                                PdfPCell nullCell = new PdfPCell();
                                nullCell.BackgroundColor = new BaseColor(235, 235, 235);
                                basicTable2.AddCell(nullCell);
                            } else {
                                if (curTile[i2 + 1].fileName == null) {
                                    if (curTile[i2 + 1].isAppear == "1") {
                                        if (curTile[i2 + 1].size == "M") {
                                            PdfPCell nullCell = new PdfPCell();
                                            nullCell.FixedHeight = 151f;
                                            nullCell.BackgroundColor = new BaseColor(235, 235, 235);
                                            basicTable2.AddCell(nullCell);
                                        } else {
                                            PdfPCell nullCell = new PdfPCell();
                                            nullCell.FixedHeight = 151f;
                                            nullCell.BackgroundColor = new BaseColor(235, 235, 235);
                                            basicTable2.AddCell(nullCell);
                                            PdfPTable basicTable3 = new PdfPTable(new float[] { 1f });
                                            basicTable3.WidthPercentage = 100;
                                            PdfPCell nullCell2 = new PdfPCell();
                                            nullCell2.FixedHeight = 301f;
                                            nullCell2.BackgroundColor = new BaseColor(235, 235, 235);
                                            basicTable3.AddCell(nullCell2);
                                            myDoc.Add(basicTable3);
                                        }
                                    }
                                    i2++;
                                } else {
                                    iTextSharp.text.Image image2 = iTextSharp.text.Image.GetInstance(curTile[i2 + 1].fileName);
                                    image2.ScaleToFit(260f, 150f);
                                    PdfPCell cell2 = new PdfPCell(image2);
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                                    cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    cell2.UseBorderPadding = true;
                                    cell2.BackgroundColor = new BaseColor(235, 235, 235);
                                    cell2.FixedHeight = 151f;
                                    basicTable2.AddCell(cell2);
                                    i2++;
                                }
                            }
                            myDoc.Add(basicTable2);
                        } else if (curTile[i2].size == "L") {
                            PdfPTable basicTable2 = new PdfPTable(new float[] { 1f });
                            basicTable2.WidthPercentage = 100;
                            iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(curTile[i2].fileName);
                            image1.ScaleToFit(520f, 300f);
                            PdfPCell cell = new PdfPCell(image1);
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.UseBorderPadding = true;
                            cell.BackgroundColor = new BaseColor(235, 235, 235);
                            cell.FixedHeight = 301f;
                            basicTable2.AddCell(cell);
                            myDoc.Add(basicTable2);
                        }
                    }
                    #endregion

                    //空行
                    var enter1 = new Paragraph($"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}");
                    enter1.Alignment = Element.ALIGN_CENTER;
                    myDoc.Add(enter1);
                };
                myDoc.Close();
                writer.Close();

                stream.Seek(0, SeekOrigin.Begin);
                string contentType;
                new FileExtensionContentTypeProvider().TryGetContentType("xx.pdf", out contentType);
                return File(stream.ToArray(), contentType ?? "image/jpeg");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="i"></param>
        /// <param name="buildingId"></param>
        /// <param name="title"></param>
        /// <param name="belongApp"></param>
        /// <param name="belongAppId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private Yuebon.Security.Dtos.UploadFileResultOuputDto AddG(IFormFile file, string i, string buildingId, string title, string belongApp, string belongAppId)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = file.FileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    var filenameExt = UploadFileG(i + fileName, data, buildingId, title);

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
        /// <param name="buildingId"></param>
        /// <param name="title"></param>
        private string UploadFileG(string fileName, byte[] fileBuffers, string buildingId, string title)
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

            string uploadPath = @$"{path}BuildingImage\{buildingId}\SecurityFormG\";
            if (!System.IO.File.Exists(uploadPath + title + "\\")) {
                Directory.CreateDirectory(uploadPath + title + "\\");
            }
            using (var fs = new FileStream(uploadPath + title + "\\" + fileName, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }

            return fileName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="BAdd"></param>
        /// <param name="myFileInfo"></param>
        /// <returns></returns>
        [HttpPost("DeleteImgFileG")]
        public IActionResult DeleteImgFileG(string BAdd, MyFileInfo myFileInfo)
        {
            CommonResult result = new CommonResult();
            try {
                var buildingId = _buildingService.GetIdByBAdd(BAdd);
                YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
                DirectoryInfo infoD = new DirectoryInfo(sysSetting.ChaochiFilepath + @$"\BuildingImage\{buildingId}\SecurityFormG\{myFileInfo.title}");

                var files = infoD.GetFiles().Where(x => x.Name == myFileInfo.FileName).ToList();

                string localpath = sysSetting.ChaochiFilepath + $"\\BuildingImage\\{buildingId}\\SecurityFormG\\{myFileInfo.title}\\";
                string filepath = localpath + myFileInfo.FileName;
                if (files != null && files.Count == 1) {
                    if (System.IO.File.Exists(filepath)) {
                        System.IO.File.Delete(filepath);
                        _securityFormGService.DeleteBatchWhere($"Id='{myFileInfo.Id}'");
                    }
                    result.ErrCode = ErrCode.successCode;
                    result.Success = true;
                } else if (files.Count == 0) {
                    _securityFormGService.DeleteBatchWhere($"Id='{myFileInfo.Id}'");
                    result.ErrCode = ErrCode.successCode;
                    result.Success = true;
                } else {
                    result.ErrCode = ErrCode.failCode;
                    result.Success = false;
                }

            } catch (Exception ex) {
                Log4NetHelper.Error("", ex);
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
            }
            return ToJsonContent(result);
        }

        #endregion 公會版-租安照片表單

        /// <summary>
        /// 異步分頁查詢
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerSearchAsync")]
        [YuebonAuthorize("List")]
        [LogNotMethod]
        public async Task<IActionResult> FindWithPagerSearchAsync(SearchInputDto<SecurityForm> search)
        {
            CommonResult<PageResult<InternalformOutputDto>> result = new CommonResult<PageResult<InternalformOutputDto>>();
            result.ResData = await _securityFormService.FindWithPagerSearchAsync(search);
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 根據租安表單代號查詢版本號並形成list(用於下拉選單)
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        [HttpPost("GetVNoListByFormId")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GetVNoListByFormId(string formId)
        {
            var result = new CommonResult();
            var internalform = await _internalformService.GetWhereAsync($"FormId = '{formId}'");
            if (internalform is null) {
                result.Success = false;
                result.ErrMsg = $"{formId}表單尚未建立對應資料!";
                return ToJsonContent(result);
            } else {
                if (string.IsNullOrEmpty(internalform.Vno)) {
                    result.ResData = new List<string>();
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    return ToJsonContent(result);
                } else {
                    result.ResData = internalform.Vno.Split(',').ToList();
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    return ToJsonContent(result);
                }
            }
        }

        /// <summary>
        /// 組合查詢條件的地址
        /// </summary>
        /// <returns></returns>
        [HttpPost("handelAdd")]
        [LogNotMethod]
        public string handelAdd(PDFDataModel obj)
        {
            var result = Utils.memergeAddFix(
                obj.BAdd_1,
                obj.BAdd_1_1,
                obj.BAdd_1_2,
                obj.BAdd_2,
                obj.BAdd_2_1,
                obj.BAdd_2_2,
                obj.BAdd_2_3,
                obj.BAdd_2_4,
                obj.BAdd_3,
                obj.BAdd_3_1,
                obj.BAdd_3_2,
                obj.BAdd_4,
                obj.BAdd_5,
                obj.BAdd_6,
                obj.BAdd_7,
                obj.BAdd_8,
                obj.BAdd_9);
            return result;
        }

        /// <summary>
        /// 檢查BAdd地址是否存在
        /// </summary>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        [HttpGet("checkExistsAndPermission")]
        public async Task<IActionResult> checkExistsAndPermission(string BAdd)
        {
            var result = new CommonResult();

            var b =await _buildingService.GetBuildingByBAddAsync(BAdd);

            if (b is null ) {
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
                result.ResData = false;
            } else {
                if (b.CreatorUserId == CurrentUser.UserId) {
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    result.ResData = true;
                } else {
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    result.ResData = false;
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateDirInfo(string buildingId, string title)
        {
            var yuebonCacheHelper = new YuebonCacheHelper();
            var sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var infoD = new DirectoryInfo(sysSetting.ChaochiFilepath + @$"\BuildingImage\{buildingId}\SecurityFormG\{title}");

            if (!infoD.Exists) {
                infoD.Create();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class HeaderPageEvent : PdfPageEventHelper
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="vno"></param>
            /// <param name="font"></param>
            public HeaderPageEvent(string vno,Font font)
            {
                this.Vno = vno;
                this.font = font;
            }

            /// <summary>
            /// 版本號
            /// </summary>
            public string Vno { get; set; }

            /// <summary>
            /// 自型
            /// </summary>
            public Font font { get; set; }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="writer"></param>
            /// <param name="document"></param>
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                var title = new Paragraph($"");
                title.Alignment = Element.ALIGN_LEFT;

                PdfPTable vnoTable = new PdfPTable(new float[] { 1f });
                vnoTable.WidthPercentage = 10;
                vnoTable.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell vnoCell = new PdfPCell(new Phrase($"{this.Vno} 版",this.font));
                vnoTable.AddCell(vnoCell);

                PdfPTable titleTable = new PdfPTable(new float[] { 1f, 0.14f });
                titleTable.WidthPercentage = 100f;
                PdfPCell titleCell1 = new PdfPCell(title);
                titleCell1.Border = Rectangle.NO_BORDER;
                titleTable.AddCell(titleCell1);
                PdfPCell titleCell2 = new PdfPCell(vnoTable);
                titleTable.AddCell(titleCell2);
                document.Add(titleTable);

                var enter = new Paragraph(Environment.NewLine);
                enter.Alignment = Element.ALIGN_CENTER;
                document.Add(enter);
                base.OnStartPage(writer, document);
            }
        }
    }
}
