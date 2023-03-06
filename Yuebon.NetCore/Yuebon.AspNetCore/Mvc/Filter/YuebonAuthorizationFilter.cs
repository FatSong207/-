using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Yuebon.AspNetCore.Common;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using Yuebon.Security.Dtos;

namespace Yuebon.AspNetCore.Mvc
{
    /// <summary>
    /// 功能權限授權驗證
    /// </summary>
    public class YuebonAuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 授權驗證
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            //匿名訪問，不需要token認證、簽名和登錄
            var allowanyone = controllerActionDescriptor.MethodInfo.GetCustomAttribute(typeof(AllowAnonymousAttribute), true);
            if (allowanyone!=null)
            {
                return;
            }
            CommonResult result = new CommonResult();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,                                   //格式化json字符串
                AllowTrailingCommas = true,                             //可以結尾有逗號
                //IgnoreNullValues = true,                              //可以有空值,轉換json去除空值屬性
                IgnoreReadOnlyProperties = true,                        //忽略只讀屬性
                PropertyNameCaseInsensitive = true,                     //忽略大小寫
                                                                        //PropertyNamingPolicy = JsonNamingPolicy.CamelCase     //命名方式是默認還是CamelCase
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            options.Converters.Add(new DateTimeJsonConverter());

            //需要token認證
            string authHeader = context.HttpContext.Request.Headers["Authorization"];//Header中的token
            if (string.IsNullOrEmpty(authHeader))
            {
                result.ErrCode = "40004";
                result.ErrMsg = ErrCode.err40004;
                context.Result = new JsonResult(result, options);
                return;
            }
            else
            {
                string token = string.Empty;
                if (authHeader != null )
                {
                    token = authHeader.Substring(7);
                }
                TokenProvider tokenProvider = new TokenProvider();
                result = tokenProvider.ValidateToken(token);
                //token驗證失敗
                if (!result.Success)
                {
                    context.Result = new JsonResult(result, options);
                }
                else
                {
                    #region 簽名驗證
                    bool boolSign = context.HttpContext.Request.Headers["sign"].SingleOrDefault().ToBool(true);
                    var isSign = controllerActionDescriptor.MethodInfo.GetCustomAttribute(typeof(NoSignRequiredAttribute), true);
                    //需要簽名驗證
                    if (isSign == null && boolSign)
                    {
                        CommonResult resultSign = SignHelper.CheckSign(context.HttpContext);
                        if (!resultSign.Success)
                        {
                            context.Result = new JsonResult(resultSign, options);
                            return;
                        }
                    }
                    #endregion
                    #region 是否需要驗證用戶登錄以及相關的功能權限
                    //是否需要用戶登錄
                    var isDefined = controllerActionDescriptor.MethodInfo.GetCustomAttribute(typeof(NoPermissionRequiredAttribute));
                    //不需要登錄
                    if (isDefined!=null)
                    {
                        return;
                    }
                    //需要登錄和驗證功能權限
                    if (result.ResData != null)
                    {
                        List<Claim> claimlist = result.ResData as List<Claim>;
                        string userId = claimlist[3].Value;
                        YuebonCurrentUser user = new YuebonCurrentUser
                        {
                            UserId = userId,
                            Account = claimlist[2].Value,
                            Role = claimlist[4].Value
                        };

                        var claims = new[] {
                           new Claim(YuebonClaimTypes.UserId,userId),
                           new Claim(YuebonClaimTypes.UserName,claimlist[2].Value),
                           new Claim(YuebonClaimTypes.Role,claimlist[4].Value)
                        };
                        var identity = new ClaimsIdentity(claims);
                        var principal = new ClaimsPrincipal(identity);
                        context.HttpContext.User = principal;
                        bool isAdmin = Permission.IsAdmin(user);
                        if (!isAdmin)
                        {
                            var authorizeAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(YuebonAuthorizeAttribute), true).OfType<YuebonAuthorizeAttribute>();
                            if (authorizeAttributes.FirstOrDefault() != null)
                            {
                                string function = authorizeAttributes.First().Function;
                                if (!string.IsNullOrEmpty(function))
                                {
                                    string functionCode = controllerActionDescriptor.ControllerName + "/" + function;

                                    bool bl = Permission.HasFunction(functionCode, userId);
                                    if (!bl)
                                    {
                                        result.ErrCode = "40006";
                                        result.ErrMsg = ErrCode.err40006;
                                        context.Result = new JsonResult(result, options);
                                    }
                                }
                            }
                        }
                        return;
                    }
                    else
                    {
                        result.ErrCode = "40008";
                        result.ErrMsg = ErrCode.err40008;
                        context.Result = new JsonResult(result, options);
                    }
                    #endregion

                }
                return;
            }

        }

    }
}
