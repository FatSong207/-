﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Models;
using Yuebon.Commons.Options;
using Yuebon.Security.Application;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yuebon.WebApi.Controllers
{
    /// <summary>
    /// Token令牌接口控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IAPPService _iAPPService;
        private readonly IUserService userService;
        private readonly JwtOption _jwtModel;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="iAPPService"></param>
        /// <param name="_userService"></param>
        /// <param name="jwtModel"></param>
        public TokenController(IAPPService iAPPService, IUserService _userService, JwtOption jwtModel)
        {
            if (iAPPService == null)
                throw new ArgumentNullException(nameof(iAPPService));
            _iAPPService = iAPPService;
            userService = _userService;
            _jwtModel = jwtModel;
        }

        /// <summary>
        /// 根據應用信息獲得token令牌
        /// </summary>
        /// <param name="grant_type">獲取access_token填寫client_credential</param>
        /// <param name="appid">應用唯一憑證，應用AppId</param>
        /// <param name="secret">應用密鑰AppSecret</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get(string grant_type, string appid, string secret)
        {
            CommonResult result = new CommonResult();
            if (!grant_type.Equals(GrantType.ClientCredentials))
            {
                result.ErrCode = "40003";
                result.ErrMsg = ErrCode.err40003;
                return ToJsonContent(result);
            }
            else if(string.IsNullOrEmpty(grant_type))
            {
                result.ErrCode = "40003";
                result.ErrMsg = ErrCode.err40003;
                return ToJsonContent(result);
            }
            string strHost = Request.Host.ToString();
            APP app = _iAPPService.GetAPP(appid, secret);
            if (app == null)
            {
                result.ErrCode = "40001";
                result.ErrMsg = ErrCode.err40001;
            }
            else
            {
                if (!app.RequestUrl.Contains(strHost))
                {
                    result.ErrCode = "40002";
                    result.ErrMsg = ErrCode.err40002+"，你當前請求主機："+strHost;
                }
                else
                {
                    TokenProvider tokenProvider = new TokenProvider(_jwtModel);
                    TokenResult tokenResult = tokenProvider.GenerateToken(grant_type, appid, secret);
                    result.ResData = tokenResult;
                    result.ErrCode = "0";
                    return ToJsonContent(result);
                }
            }
            return ToJsonContent(result);
        }


        /// <summary>
        /// 驗證token的合法性。
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("CheckToken")]
        [AllowAnonymous]
        public IActionResult CheckToken(string token)
        {
            CommonResult result = new CommonResult();
            TokenProvider tokenProvider = new TokenProvider(_jwtModel);
            result = tokenProvider.ValidateToken(token);
            return ToJsonContent(result);
        }


        /// <summary>
        /// 刷新token。
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(string token)
        {
            CommonResult result = new CommonResult();
            TokenProvider tokenProvider = new TokenProvider(_jwtModel);
            if (!string.IsNullOrEmpty(token))
            {
                JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                #if DEBUG
                Log4NetHelper.Debug(jwtToken.ToJson());
                #endif
                if (jwtToken != null)
                {
                    //根據應用獲取token
                    if (jwtToken.Subject == GrantType.ClientCredentials)
                    {
                        TokenResult tresult = new TokenResult();
                        var claimlist = jwtToken?.Payload.Claims as List<Claim>;
                        string strHost = Request.Host.ToString();
                        APP app = _iAPPService.GetAPP(claimlist[0].Value);
                        if (app == null)
                        {
                            result.ErrCode = "40001";
                            result.ErrMsg = ErrCode.err40001;
                        }
                        else
                        {
                            if (!app.RequestUrl.Contains(strHost))
                            {
                                result.ErrCode = "40002";
                                result.ErrMsg = ErrCode.err40002 + "，你當前請求主機：" + strHost;
                            }
                            else
                            {
                                TokenResult tokenResult = tokenProvider.GenerateToken(GrantType.ClientCredentials, app.AppId, app.AppSecret);
                                result.ResData = tokenResult;
                                result.ErrCode = "0";
                                result.Success = true;
                            }
                        }
                    }
                    // 用戶帳號密碼登錄獲取token類型
                    if (jwtToken.Subject == GrantType.Password)
                    {
                        var claimlist = jwtToken?.Payload.Claims as List<Claim>;
                        User user = await userService.GetByUserName(claimlist[2].Value);
                        TokenResult tokenResult = tokenProvider.LoginToken(user, claimlist[0].Value);
                        result.ResData = tokenResult;
                        result.ErrCode = "0";
                        result.Success = true;
                    }
                }
                else
                {
                    result.ErrMsg = ErrCode.err40004;
                    result.ErrCode = "40004";
                }
            }
            else
            {
                result.ErrMsg = ErrCode.err40004;
                result.ErrCode = "40004";
            }
            return ToJsonContent(result);
        }
        /// <summary>
        /// 把object對象轉換為ContentResult
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ToJsonContent")]
        protected IActionResult ToJsonContent(object obj)
        {
            string result = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return Content(obj.ToJson());
        }

    }
}
