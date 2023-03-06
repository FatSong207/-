using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Common;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Models;
using Yuebon.Commons.Net;
using Yuebon.Commons.Pages;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Models;
using Yuebon.Security.Repositories;

namespace Yuebon.AspNetCore.Controllers
{
    /// <summary>
    /// WebApi控制器基類
    /// </summary>
    [ApiController]
    [EnableCors("yuebonCors")]
    public class ApiController : Controller
    {
        /// <summary>
        /// 當前登錄的用戶屬性
        /// </summary>
        public YuebonCurrentUser CurrentUser;
        private ILogRepository service = new LogRepository();

        #region 
        /// <summary>
        /// 重寫基類在Action執行之前的事情
        /// 根據token獲得當前用戶，允許匿名的不需要獲取用戶
        /// </summary>
        /// <param name="context">重寫方法的參數</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try {
                var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                //匿名訪問，不需要token認證、簽名和登錄
                var allowanyone = controllerActionDescriptor.MethodInfo.GetCustomAttribute(typeof(AllowAnonymousAttribute), true);
                if (allowanyone != null) return;

                CommonResult result = new CommonResult();
                //需要token認證
                string authHeader = context.HttpContext.Request.Headers["Authorization"];//Header中的token
                if (string.IsNullOrEmpty(authHeader)) {
                    result.ErrCode = "40004";
                    result.ErrMsg = ErrCode.err40004;
                    context.Result = ToJsonContent(result);
                    return;
                } else {
                    string token = string.Empty;
                    if (authHeader != null && authHeader.StartsWith("Bearer ", StringComparison.Ordinal)) token = authHeader.Substring(7);
                    TokenProvider tokenProvider = new TokenProvider();
                    result = tokenProvider.ValidateToken(token);
                    //token驗證失敗
                    if (!result.Success) {
                        context.Result = ToJsonContent(result);
                    } else {

                        #region 簽名驗證
                        bool boolSign = context.HttpContext.Request.Headers["sign"].SingleOrDefault().ToBool(true); //2022/10/13 從VUE前端可以發現這裡一定回true
                        var isSign = controllerActionDescriptor.MethodInfo.GetCustomAttribute(typeof(NoSignRequiredAttribute), true);
                        //需要簽名驗證
                        // if (isSign == null && boolSign) //整點會容易導致CheckSign回傳接口請求超時，故一律不開啟
                        if (false) {
                            CommonResult resultSign = SignHelper.CheckSign(context.HttpContext);
                            if (!resultSign.Success) {
                                context.Result = ToJsonContent(resultSign);
                                return;
                            }
                        }
                        #endregion

                        #region 是否需要驗證用戶登錄以及相關的功能權限
                        //是否需要用戶登錄
                        var isDefined = controllerActionDescriptor.MethodInfo.GetCustomAttribute(typeof(NoPermissionRequiredAttribute));
                        //不需要登錄
                        if (isDefined != null) return;
                        //需要登錄和驗證功能權限
                        if (result.ResData != null) {

                            List<Claim> claimlist = result.ResData as List<Claim>;
                            string userId = claimlist[3].Value;



                            var claims = new[] {
                               new Claim(YuebonClaimTypes.UserId,userId),
                               new Claim(YuebonClaimTypes.UserName,claimlist[2].Value),
                               new Claim(YuebonClaimTypes.Role,claimlist[4].Value)
                            };
                            var identity = new ClaimsIdentity(claims);
                            var principal = new ClaimsPrincipal(identity);
                            context.HttpContext.User = principal;
                            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                            var user = yuebonCacheHelper.Get<YuebonCurrentUser>("login_user_" + userId);
                            if (user != null) {
                                CurrentUser = user;
                            } else {
                                result.ErrCode = "44002";
                                result.ErrMsg = "登入超時";
                                context.Result = ToJsonContent(result);
                            }

                            #region 搶登驗證
                            var userCacheIP = JsonSerializer.Deserialize<String>(yuebonCacheHelper.Get("User_IP" + userId).ToJson());
                            RemoteIpParser remoteIpParser = new RemoteIpParser();
                            string strIp = remoteIpParser.GetClientIp(HttpContext).MapToIPv4().ToString();
                            var curIp = strIp;
                            Console.WriteLine("CacheIP:" + userCacheIP);
                            Console.WriteLine("curIp:" + curIp);
                            if (userCacheIP != null && userCacheIP != curIp) {
                                result.ErrCode = "44001";
                                result.ErrMsg = "您已從另一端點登入";
                                context.Result = ToJsonContent(result);
                            }
                            #endregion 搶登驗證

                            if (user is not null) {
                                bool isAdmin = Permission.IsAdmin(user);
                                if (!isAdmin) {
                                    var authorizeAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(YuebonAuthorizeAttribute), true).OfType<YuebonAuthorizeAttribute>();
                                    if (authorizeAttributes.FirstOrDefault() != null) {
                                        string function = authorizeAttributes.First().Function;
                                        if (!string.IsNullOrEmpty(function)) {
                                            string functionCode = controllerActionDescriptor.ControllerName + "/" + function;

                                            bool bl = Permission.HasFunction(functionCode, userId);
                                            if (!bl) {
                                                result.ErrCode = "40006";
                                                result.ErrMsg = ErrCode.err40006;
                                                context.Result = ToJsonContent(result);
                                            }
                                        }
                                    }
                                }
                            } else {
                                result.ErrCode = "44002";
                                result.ErrMsg = "登入超時";
                                context.Result = ToJsonContent(result);
                            }

                            return;
                        } else {
                            result.ErrCode = "40008";
                            result.ErrMsg = ErrCode.err40008;
                            context.Result = ToJsonContent(result);
                        }
                        #endregion

                    }
                    return;
                }
            } catch (Exception ex) {
                Log4NetHelper.Error("", ex);
            }
        }
        #endregion

        /// <summary>
        /// 把object對象轉換為ContentResult
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ToJsonContent")]
        protected IActionResult ToJsonContent(object obj)
        {
            return Content(obj.ToJson());
        }


        /// <summary>
        /// 把object對象轉換為ContentResult
        /// </summary>
        /// <param name="obj">轉換對象</param>
        /// <param name="isNull">是否忽略空值</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ToJsonContent")]
        protected IActionResult ToJsonContent(object obj, bool isNull = false)
        {
            JsonSerializerOptions options = new JsonSerializerOptions() {
                WriteIndented = true,                                   //格式化json字符串
                AllowTrailingCommas = true,                             //可以結尾有逗號
                IgnoreNullValues = true,                              //可以有空值,轉換json去除空值屬性
                IgnoreReadOnlyProperties = true,                        //忽略只讀屬性
                PropertyNameCaseInsensitive = true,                     //忽略大小寫
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            options.Converters.Add(new DateTimeJsonConverter());
            return Content(JsonSerializer.Serialize(obj, options));
        }

        /// <summary>
        /// 根據Request參數獲取分頁對象數據
        /// </summary>
        /// <returns></returns>
        protected virtual PagerInfo GetPagerInfo()
        {
            int pageSize = Request.Query["length"].ToString() == null ? 1 : Request.Query["length"].ToString().ToInt();
            int pageIndex = 1;
            string currentPage = Request.Query["CurrentPage"].ToString();
            if (string.IsNullOrWhiteSpace(currentPage)) {
                string start = Request.Query["start"].ToString();
                if (!string.IsNullOrWhiteSpace(start)) {
                    pageIndex = (start.ToInt() / pageSize) + 1;
                }
            } else {
                pageIndex = currentPage.ToInt();
            }
            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = pageIndex;
            pagerInfo.PageSize = pageSize;
            return pagerInfo;
        }

        /// <summary>
        /// 獲取token
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetToken")]
        [HiddenApi]
        public string GetToken()
        {
            string token = HttpContext.Request.Query["Token"];
            if (!String.IsNullOrEmpty(token)) return token;
            string authHeader = HttpContext.Request.Headers["Authorization"];//Header中的token
            if (authHeader != null && authHeader.StartsWith("Bearer")) {
                token = authHeader.Substring("Bearer ".Length).Trim();
                return token;
            }
            var cookie = HttpContext.Request.Cookies["Token"];
            return cookie == null ? String.Empty : cookie;
        }

    }
}
