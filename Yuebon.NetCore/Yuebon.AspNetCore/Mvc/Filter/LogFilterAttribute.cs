using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Json;
using Yuebon.Commons.Models;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Models;
using Yuebon.Security.Repositories;

namespace Yuebon.AspNetCore.Mvc
{
    /// <summary>
    /// 日志過濾器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private ILogRepository service = new LogRepository();

        public LogFilterAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            //是否需要用戶登錄
            var needLog = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute(typeof(LogControllerAttribute));
            //不需要登錄
            if (needLog == null)
            {
                return;
            }

            needLog = controllerActionDescriptor.MethodInfo.GetCustomAttribute(typeof(LogNotMethodAttribute), true);
            if (needLog != null)
            {
                return;
            }

            //設置日志開關
            //if (!GlobalContext.LogConfig.IsEnabled)
            //{
            //    return;
            //}
            YuebonCurrentUser currentUser = new YuebonCurrentUser();
            var identities = context.HttpContext.User.Identities;
            var claimsIdentity = identities.First<ClaimsIdentity>();
            if (claimsIdentity != null)
            {
                List<Claim> claimlist = claimsIdentity.Claims as List<Claim>;
                if (claimlist.Count > 0)
                {
                    string userId = claimlist[0].Value;
                    YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                    var user = yuebonCacheHelper.Get("login_user_" + userId).ToJson().ToObject<YuebonCurrentUser>();
                    if (user != null)
                    {
                        currentUser = user;
                    }
                }
            }

            //var token = context.HttpContext.GetToken();
            ////獲取用戶
            //var user = RedisUtil.Get<SysUserDTO>(token);
            //if (user == null)
            //{
            //    throw new Exception("token is not valid");
            //}

            #region 監控信息入庫

            Log log = new Log();

            log.Account = currentUser.Account;
            log.NickName = currentUser.NickName;
            log.Date = log.CreatorTime = DateTime.Now;
            log.IPAddress = currentUser.CurrentLoginIP;
            log.IPAddressName = currentUser.IPAddressName;
            log.Result = false;
            log.Description = "";// $"請求：{exDesc}\r\n異常類型：{exception.GetType().Name} \r\n異常信息：{exception.Message} \r\n【堆棧調用】：\r\n{exception.StackTrace}";
            log.Type = "Visit";
            //log.Id = Guid.NewGuid();
            /*
            log.UserId = user.Id;
            log.UserName = user.DisplayName;

            log.OrgId = user.OrgId;
            if (log.OrgId.Equals(Guid.Empty))
            {
                log.OrgName = "none";
            }
            else
            {
                log.OrgName = user.OrgName;
            }

            log.System = context.HttpContext.Request.Headers["User-Agent"].FirstOrDefault().ToString().Split('(')[1].Split(')')[0];
            log.Browser = context.HttpContext.Request.Headers["sec-ch-ua"];

            var ip = context.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            log.IP = ip;

            log.OperationType = this.operationType;
            log.RequestUrl = context.HttpContext.Request.Path.Value;
            */
            #region 設置參數
            //log.Description = context.HttpContext.Request.Path;
            log.ModuleId = context.HttpContext.Request.Path;
            //switch (context.HttpContext.Request.Method)
            //{
            //    case "GET":
            //    case "DELETE":
            //        log.Description = context.HttpContext.Request.Path;
            //        break;
            //    case "PUT":
            //    case "POST":
            //        context.HttpContext.Request.EnableBuffering();
            //        context.HttpContext.Request.Body.Position = 0;
            //        StreamReader reader = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8);
            //        log.Description = reader.ReadToEndAsync().GetAwaiter().GetResult();
            //        context.HttpContext.Request.Body.Position = 0;
            //        break;
            //    default: break;
            //}
            //if (log.Description != null && log.Description.Length > 1024)
            //{
            //    log.Description = log.Description.Substring(0, 1021) + "...";
            //}
            #endregion

            #region 設置返回值

            if (context.ActionArguments == null || context.ActionArguments.Count != 1)
            {
                return;
            }

            //if (context.ActionArguments.ContainsKey("info"))
            //{
            //    return;
            //}
            //context.ActionArguments.Keys.ElementAt(0)

            var isFileStream = false;//context.Result.GetType().Equals(typeof(Microsoft.AspNetCore.Mvc.FileStreamResult));
            var isFile = false;//context.Result.GetType().Equals(typeof(Microsoft.AspNetCore.Mvc.FileResult));
            if (!isFileStream && !isFile)
            {
                //var result = JsonConvert.SerializeObject(((Microsoft.AspNetCore.Mvc.ObjectResult)context.ActionArguments["info"]).Value);
                var result = context.ActionArguments[context.ActionArguments.Keys.ElementAt(0)].ToJson();
                if (result != null)
                {
                    if (result.Length > 2000)
                    {
                        log.Description = result.Substring(0, 2000) + "...";

                        //返回結果落盤
                        //var monitoringLogsPath = String.Join(String.Empty, GlobalContext.DirectoryConfig.GetMonitoringLogsPath(), "/" + log.Id + ".txt");
                        //var file = new FileStream(monitoringLogsPath, FileMode.Create);
                        //byte[] byteArray = System.Text.Encoding.Default.GetBytes(result);
                        //file.Write(byteArray, 0, byteArray.Length);
                        //file.Flush();
                        //file.Close();
                    }
                    else
                    {
                        log.Description = result;
                    }
                }
            }

            #endregion

            //log.OldVaue = String.Empty;
            //log.NewValue = String.Empty;
            //log.Remark = $"{user.DisplayName}于{DateTime.Now}訪問了{log.RequestUrl}接口";

            //log.IsDel = 0;
            //log.Creator = user.Id;
            //log.CreateTime = DateTime.Now;
            //log.Modifier = user.Id;
            //log.ModifyTime = DateTime.Now;

            //SqlSugarDbContext.Repository.Insertable<SysLog>(log).ExecuteCommand();
            service.Insert(log);

            #endregion
        }
    }
}
