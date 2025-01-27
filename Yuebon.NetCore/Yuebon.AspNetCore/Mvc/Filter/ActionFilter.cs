﻿using Microsoft.AspNetCore.Mvc.Filters;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.Core.App;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.AspNetCore.Mvc.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionFilter : IAsyncActionFilter
    {

         ILogService _logService = App.GetService<ILogService>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var profiler = MiniProfiler.StartNew("StartNew");
            using (profiler.Step("Level1"))
            {
                //執行Action
                await next();
            }
           WriteLog(profiler);
        }

        /// <summary>
        /// sql跟蹤
        /// 下載：MiniProfiler.AspNetCore
        /// </summary>
        /// <param name="profiler"></param>
        private void WriteLog(MiniProfiler profiler)
        {
            if (profiler?.Root != null)
            {
                var root = profiler.Root;
                if (root.HasChildren)
                {
                    GetSqlLog(root.Children);
                }
            }
        }
        /// <summary>
        /// 遞歸獲取MiniProfiler內容
        /// </summary>
        /// <param name="chil"></param>
        private void GetSqlLog(List<Timing> chil)
        {
            chil.ForEach(chill =>
            {
                if (chill.CustomTimings?.Count > 0)
                {
                    StringBuilder logSql = new StringBuilder();
                    foreach (var customTiming in chill.CustomTimings)
                    {
                        int i = 1;
                        customTiming.Value?.ForEach(value =>
                        {
                            if (value.ExecuteType != "OpenAsync"&& !value.CommandString.Contains("Connection"))
                            {
                                logSql.Append($"【{customTiming.Key}{i++}】{value.CommandString} 耗時 :{value.DurationMilliseconds} ms,狀態 :{(value.Errored?"失敗":"成功")}");
                            }
                        });
                    }
                    Log logEntity = new Log();
                    logEntity.Date = logEntity.CreatorTime = DateTime.Now;
                    logEntity.Type = "SQL";
                    logEntity.Result = true;
                    logEntity.Description = logSql.ToString();
                    _logService.Insert(logEntity);
                }
                else
                {
                    if (chill.Children!=null)
                    {
                        GetSqlLog(chill.Children);
                    }
                }
            });
        }
    }
}
