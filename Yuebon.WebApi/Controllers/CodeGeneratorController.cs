using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.AspNetCore.ViewModel;
using Yuebon.Commons.Cache;
using Yuebon.Commons.CodeGenerator;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Security.Models;

namespace Yuebon.WebApi.Controllers
{
    /// <summary>
    /// 代碼生成器
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CodeGeneratorController : ApiController
    {

        private readonly IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public CodeGeneratorController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 創建數據庫連接
        /// </summary>
        /// <param name="dbConnInfo"></param>
        /// <returns></returns>
        [HttpPost("CreateDBConn")]
        [YuebonAuthorize("CreateDBConn")]
        [NoPermissionRequired]
        public async Task<IActionResult> CreateDBConn(DbConnInfo dbConnInfo)
        {
            CommonResult result = new CommonResult();
            DBConnResult dBConnResult = new DBConnResult();
            if (dbConnInfo != null)
            {
                if (string.IsNullOrEmpty(dbConnInfo.DbName))
                {
                    dbConnInfo.DbName = "master";
                }
                else if (string.IsNullOrEmpty(dbConnInfo.DbAddress))
                {
                    result.ErrMsg = "訪問地址不能為空";
                }
                else if (string.IsNullOrEmpty(dbConnInfo.DbUserName))
                {
                    result.ErrMsg = "訪問用戶不能為空";
                }
                else if (string.IsNullOrEmpty(dbConnInfo.DbPassword))
                {
                    result.ErrMsg = "訪問密碼不能為空";
                }
                if (string.IsNullOrEmpty(dbConnInfo.DbPort.ToString()))
                {
                    if (dbConnInfo.DbType == "SqlServer")
                    {
                        dbConnInfo.DbPort = 1433;
                    }else if (dbConnInfo.DbType == "MySql")
                    {
                        dbConnInfo.DbPort = 3306;
                    }
                }
                if (dbConnInfo.DbType == "SqlServer")
                {
                    dBConnResult.ConnStr = string.Format("Server={0},{1};Database={2};User id={3}; password={4};MultipleActiveResultSets=True;", dbConnInfo.DbAddress,dbConnInfo.DbPort, dbConnInfo.DbName, dbConnInfo.DbUserName, dbConnInfo.DbPassword);
                }
                else if (dbConnInfo.DbType == "MySql")
                {
                    dBConnResult.ConnStr = string.Format("server={0};database={1};uid={2}; pwd={3};port={4};Allow User Variables=True;", dbConnInfo.DbAddress, dbConnInfo.DbName, dbConnInfo.DbUserName, dbConnInfo.DbPassword,dbConnInfo.DbPort);
                }
                YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                TimeSpan expiresSliding = DateTime.Now.AddMinutes(30) - DateTime.Now;
                yuebonCacheHelper.Add("CodeGeneratorDbConn", dBConnResult.ConnStr, expiresSliding, false);
                yuebonCacheHelper.Add("CodeGeneratorDbType", dbConnInfo.DbType, expiresSliding, false); 
                yuebonCacheHelper.Add("CodeGeneratorDbName", dbConnInfo.DbName, expiresSliding, false);
                DbExtractor dbExtractor = new DbExtractor();
                List<DataBaseInfo> listTable = dbExtractor.GetAllDataBases();
                result.ResData = listTable;
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
               
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取所有數據庫的信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetListDataBase")]
        [YuebonAuthorize("GetListDataBase")]
        [NoPermissionRequired]
        public async Task<IActionResult> GetListDataBase()
        {
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            yuebonCacheHelper.Remove("CodeGeneratorDbConn");
            yuebonCacheHelper.Remove("CodeGeneratorDbName");
            CommonResult result = new CommonResult();
            DbExtractor dbExtractor = new DbExtractor();
            List<DataBaseInfo> listTable = dbExtractor.GetAllDataBases();
            result.ResData = listTable;
            result.ErrCode = ErrCode.successCode;
            return ToJsonContent(result);
        }

        /// <summary>
        /// 獲取數據庫的所有表信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindListTable")]
        [YuebonAuthorize("FindListTable")]
        [NoPermissionRequired]
        public CommonResult<PageResult<DbTableInfo>> FindListTable(SearchModel search)
        {
            CommonResult<PageResult<DbTableInfo>> result = new CommonResult<PageResult<DbTableInfo>>();
            if (!string.IsNullOrEmpty(search.EnCode))
            {
                YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                object connCode = yuebonCacheHelper.Get("CodeGeneratorDbConn");
                if (connCode != null)
                {
                    string SqlConnectionString = connCode.ToString();
                    string[] sqlconn = SqlConnectionString.Split(";");
                    string[] dataName = sqlconn[1].Split("=");
                    dataName[1] = search.EnCode;
                    sqlconn[1] = dataName.Join("=");
                    string newConnStr = sqlconn.Join(";");
                    TimeSpan expiresSliding = DateTime.Now.AddMinutes(30) - DateTime.Now;
                    yuebonCacheHelper.Add("CodeGeneratorDbConn", newConnStr, expiresSliding,false);
                    yuebonCacheHelper.Add("CodeGeneratorDbName", search.EnCode, expiresSliding, false);
                }
            }
            string orderByDir =search.Order;
            string orderFlied =string.IsNullOrEmpty(search.Sort)? "TableName": search.Sort;
            bool order = orderByDir == "asc" ? false : true;
            string where = "1=1";
            if (!string.IsNullOrEmpty(search.Keywords))
            {
                where += " and TableName like '%"+ search.Keywords + "%'";
            }
            PagerInfo pagerInfo = new PagerInfo { 
                PageSize=search.PageSize,
                CurrenetPageIndex=search.CurrenetPageIndex
            };
            DbExtractor dbExtractor = new DbExtractor();
            List<DbTableInfo> listTable = dbExtractor.GetTablesWithPage(search.Keywords, orderFlied, order,pagerInfo);
           
            PageResult<DbTableInfo> pageResult = new PageResult<DbTableInfo>();
            pageResult.CurrentPage = pagerInfo.CurrenetPageIndex;
            pageResult.Items = listTable;
            pageResult.ItemsPerPage = pagerInfo.PageSize;
            pageResult.TotalItems = pagerInfo.RecordCount;
            result.ResData = pageResult;
            result.ErrCode = ErrCode.successCode;
            return result;
        }
        /// <summary>
        /// 代碼生成器
        /// </summary>
        /// <param name="tables">要生成代碼的表</param>
        /// <param name="baseSpace">項目命名空間</param>
        /// <param name="replaceTableNameStr">要刪除表名的字符串用英文逗號","隔開</param>
        /// <returns></returns>
        [HttpGet("Generate")]
        [YuebonAuthorize("GetGenerate")]
        [NoPermissionRequired]
        public async Task<IActionResult> GetGenerate(string tables,string baseSpace,string replaceTableNameStr)
        {
            CommonResult result = new CommonResult();
            try
            {
                if (string.IsNullOrEmpty(baseSpace))
                {
                    result.ErrMsg = "項目命名空間不能為空";
                    result.ErrCode = ErrCode.failCode;
                }
                else
                {
                    CodeGenerator.Generate(baseSpace, tables, replaceTableNameStr);
                    var path = AppDomain.CurrentDomain.BaseDirectory;
                    var parentPath = path.Substring(0, path.LastIndexOf("\\"));
                    var servicesPath = parentPath + "\\" + baseSpace + "\\";
                    //生成壓縮包
                    string zipReturnFileName = baseSpace+DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip";
                    string zipFileBasePath = "Generatecode";
                    string zipFilesPath = _hostingEnvironment.WebRootPath + "\\"+ zipFileBasePath;
                    if (!System.IO.Directory.Exists(zipFilesPath))
                    {
                        System.IO.Directory.CreateDirectory(zipFilesPath);
                    }
                    string zipFileName = zipFilesPath + "\\" + zipReturnFileName;
                    if (System.IO.File.Exists(zipFileName))
                    {
                        System.IO.File.Delete(zipFileName);
                    }
                    FileHelper.ZipFileDirectory(servicesPath, zipFileName, 7, "", "", "*.*");
                    FileHelper.DeleteDirectory(servicesPath);
                    result.ErrCode = ErrCode.successCode;
                    result.Success = true;
                    result.ResData = new string[2] { zipFileBasePath+"/"+ zipReturnFileName, zipReturnFileName };
                }
            }catch(Exception ex)
            {
                Log4NetHelper.Error("代碼生成異常",ex);
                result.ErrMsg = "代碼生成異常:"+ex.Message;
                result.ErrCode = ErrCode.failCode;
            }
            return ToJsonContent(result);
        }
    }
}