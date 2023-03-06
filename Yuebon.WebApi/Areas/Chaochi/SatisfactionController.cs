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
using Yuebon.Commons.Dtos;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Json;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using static Yuebon.ChaochiApi.Areas.Chaochi.Controllers.EventController;
using System.IO;
using System.Net.Http;
using System.Text;
using Yuebon.Commons;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Data;
using Yuebon.Chaochi.Services;
using Yuebon.Commons.IServices;
using Yuebon.WeChat.CommonService;
using Yuebon.Chaochi.Core.Helpers;

namespace Yuebon.ChaochiApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/Chaochi/[controller]")]
    [LogController]
    public class SatisfactionController : AreaApiController<Satisfaction, SatisfactionOutputDto, SatisfactionInputDto, ISatisfactionService, string>
    {
        private readonly IEventService _eventService;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="eventService"></param>
        public SatisfactionController(ISatisfactionService _iService, IEventService eventService) : base(_iService)
        {
            iService = _iService;
            _eventService = eventService;
        }
        /// <summary>
        /// 新增前處理數據
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Satisfaction info)
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
        protected override void OnBeforeUpdate(Satisfaction info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Satisfaction info)
        {
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(SatisfactionInputDto inInfo)
        {
            var result = new CommonResult();
            var satisfaction_DB = await iService.GetAsync(inInfo.Id);
            var satisfaction = inInfo.MapTo(satisfaction_DB);

            try {
                OnBeforeUpdate(satisfaction);
                var isSuccess = await iService.UpdateAsync(satisfaction, inInfo.Id);
                if (isSuccess) {
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                } else {
                    result.Success = false;
                    result.ErrMsg = ErrCode.err43002;
                    result.ErrCode = "43002";
                }
            } catch (Exception ex) {
                Log4NetHelper.Error("更新失敗", ex);
                result.Success = false;
                result.ErrMsg = "更新失敗";
                result.ErrCode = ErrCode.err43002;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        /// 
        [HttpPost("FindWithPagerAsync")]
        [YuebonAuthorize("List")]
        public override async Task<CommonResult<PageResult<SatisfactionOutputDto>>> FindWithPagerAsync(SearchInputDto<Satisfaction> search)
        {
            CommonResult<PageResult<SatisfactionOutputDto>> result = new CommonResult<PageResult<SatisfactionOutputDto>>();
            result.ResData = await iService.FindWithPagerAsync(search);
            result.ErrCode = ErrCode.successCode;
            return result;
        }

        /// <summary>
        /// 下載滿意度問卷題目定義檔CSV
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("DownloadSatisfactionTopicById")]
        [LogNotMethod]
        public async Task<IActionResult> DownloadSatisfactionTopicById(string id)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            var satisfaction = await iService.GetAsync(id);
            var fileName = satisfaction.QFileName;
            string streamPath;
            streamPath = @$"{path}SatisfactionTopic\{fileName}";

            var pdf = System.IO.File.OpenRead(streamPath);
            new FileExtensionContentTypeProvider().TryGetContentType("xx.csv", out string contentType);
            return File(pdf, contentType ?? "image/jpeg");
        }

        [HttpPost("GenSignUpForSatisfaction")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> GenSignUpForSatisfaction(SatisfactionInputDto inputDto)
        {
            var result = new CommonResult();
            var sa = await iService.GetAsync(inputDto.Id);
            try {
                var dSuccess = await _eventService.DeleteSignUP(inputDto.Id);
                if (!dSuccess) {
                    result.ErrCode = "43001";
                    result.ErrMsg = "刪除原題目資料時發生錯誤";
                    return ToJsonContent(result);
                }

                //生成問卷
                var cr = await SignUpHelper.GenSignUp(sa.Id, sa.Type, inputDto.QBeginWords, inputDto.QEndWords, @$"D:\\zzz\\image\\SatisfactionTopic\\{sa.QFileName}");
                if (cr.ResData == null) {
                    result.ErrCode = "43001";
                    result.ErrMsg = "生成問卷時發生錯誤";
                    return ToJsonContent(result);
                }

                //更新開頭語、結尾語、填寫網誌
                sa.QUrl = cr.ResData.ToString();
                sa.QEndWords = inputDto.QEndWords;
                sa.QBeginWords = inputDto.QBeginWords;
                var success = await iService.UpdateAsync(sa, sa.Id);
                if (!success) {
                    result.ErrCode = "43002";
                    result.ErrMsg = ErrCode.err43002;
                    return ToJsonContent(result);
                }

            } catch (HttpRequestException ex) {
                result.ErrCode = "43001";
                result.ErrMsg = "生成問卷時發生錯誤";
                return ToJsonContent(result);
            }
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 匯出XLSX
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet("ExportToXLSX")]
        [YuebonAuthorize("")]
        public async Task<IActionResult> ExportToXLSX(string eventId)
        {
            var result = new CommonResult();
            try {
                var list = await iService.GetTableTitleArr(eventId);
                var test = await iService.GetTableDataForXLSX(eventId);
                if (test is null) {
                    result.Success = false;
                    result.ErrCode = "43001";
                    result.ErrMsg = "獲取題目時發生錯誤";
                    return ToJsonContent(result);
                }
                var js = JsonConvert.SerializeObject(test);
                var dt = (DataTable)JsonConvert.DeserializeObject<DataTable>(js);
                var e = await iService.GetAsync(eventId);

                //var ms = new MemoryStream();
                using var fs = new MemoryStream();
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet1 = workbook.CreateSheet("Sheet1");

                var columns = new List<string>();

                IRow rowf = sheet1.CreateRow(0);
                rowf.Height = 30 * 60;
                var c = rowf.CreateCell(0);
                ICellStyle notesStyle = workbook.CreateCellStyle();
                notesStyle.WrapText = true;
                var fCount = await iService.GetFinishCount(eventId);
                string title = $"{e.Type}統計" + "\n";
                title += $"滿意度問卷代號：{e.Id}" + "\n";
                title += $"問卷當前總份數：{fCount}" + "\n";
                c.SetCellValue(title);
                c.CellStyle = notesStyle;
                sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 4));



                IRow row = sheet1.CreateRow(1);

                //sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 10));
                var columnIndex = 0;
                //row.Height = 30 * 80;
                ICellStyle style = workbook.CreateCellStyle();
                style.BorderBottom = BorderStyle.Double;
                style.BorderLeft = BorderStyle.Double;
                style.BorderRight = BorderStyle.Double;
                style.BorderTop = BorderStyle.Double;
                foreach (DataColumn item in dt.Columns) {
                    if (item.ColumnName == "編號") {
                        continue;
                    }
                    columns.Add(item.ColumnName);
                    var tc = row.CreateCell(columnIndex);
                    tc.SetCellValue(item.ColumnName);
                    tc.CellStyle = style;
                    sheet1.AutoSizeColumn(columnIndex);
                    columnIndex++;
                }
                //row.CreateCell(0).SetCellValue("this is content");

                int dataIndex = 2;
                foreach (DataRow item in dt.Rows) {
                    row = row.Sheet.CreateRow(dataIndex);
                    int cellIndex = 0;
                    foreach (var col in columns) {
                        row.CreateCell(cellIndex).SetCellValue(item[col].ToString());
                        sheet1.AutoSizeColumn(cellIndex);
                        cellIndex++;
                    }
                    dataIndex++;
                }
                workbook.Write(fs);
                string contentType;
                new FileExtensionContentTypeProvider().TryGetContentType("xx.xlsx", out contentType);
                return File(fs.ToArray(), contentType ?? "image/jpeg");
            } catch (Exception ex) {
                Log4NetHelper.Error(ex.ToString());
                result.Success = false;
                result.ErrCode = "43001";
                result.ErrMsg = "獲取題目時發生錯誤";
                return ToJsonContent(result);
                throw;
            }
        }
    }
}