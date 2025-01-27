﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.ViewModel;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Models;

namespace Yuebon.WebApi.Controllers
{
    /// <summary>
    /// 數據庫連接加解密
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DbToolsController : ApiController
    {
        /// <summary>
        /// 連接字符串加密
        /// </summary>
        /// <param name="dbConnInfo"></param>
        /// <returns></returns>
        [HttpPost("ConnStrEncrypt")]
        [YuebonAuthorize("ConnStrEncrypt")]
        public async Task<IActionResult> ConnStrEncrypt([FromQuery]DbConnInfo dbConnInfo)
        {
            CommonResult result = new CommonResult();
            DBConnResult dBConnResult = new DBConnResult();
            if (dbConnInfo != null)
            {
                if (string.IsNullOrEmpty(dbConnInfo.DbName))
                {
                    result.ErrMsg = "數據庫名稱不能為空";

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
                if (dbConnInfo.DbType == "SqlServer")
                {
                    dBConnResult.ConnStr = string.Format("Server={0};Database={1};User id={2}; password={3};MultipleActiveResultSets=True;", dbConnInfo.DbAddress, dbConnInfo.DbName, dbConnInfo.DbUserName, dbConnInfo.DbPassword);
                    dBConnResult.EncryptConnStr = DEncrypt.Encrypt(dBConnResult.ConnStr);
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                }
                else if (dbConnInfo.DbType == "MySql")
                {
                    dBConnResult.ConnStr = string.Format("server={0};database={1};uid={2}; pwd={3};", dbConnInfo.DbAddress, dbConnInfo.DbName, dbConnInfo.DbUserName, dbConnInfo.DbPassword);
                    dBConnResult.EncryptConnStr = DEncrypt.Encrypt(dBConnResult.ConnStr);
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                }
                result.ResData = dBConnResult;

            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 連接字符串解密
        /// </summary>
        /// <returns></returns>

        [HttpPost("ConnStrDecrypt")]
        [YuebonAuthorize("ConnStrDecrypt")]
        public IActionResult ConnStrDecrypt(string strConn)
        {
            CommonResult result = new CommonResult();
            DBConnResult dBConnResult = new DBConnResult();
            if (string.IsNullOrEmpty(strConn))
            {
                result.ErrMsg = "數據庫名稱不能為空";
            }
            else
            {
                dBConnResult.ConnStr = DEncrypt.Decrypt(strConn);
                result.Success = true;
                result.ErrCode = ErrCode.successCode;
            }
            result.ResData = dBConnResult;
            return ToJsonContent(result);
        }
    }
}