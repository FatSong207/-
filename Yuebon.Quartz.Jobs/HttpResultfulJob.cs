using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Core.App;
using Yuebon.Commons.Enums;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Json;
using Yuebon.Email;
using Yuebon.Quartz.IServices;
using Yuebon.Quartz.Models;
using Yuebon.Security.Models;

namespace Yuebon.Quartz.Jobs
{
    /// <summary>
    /// 處理遠程接口url的定時任務
    /// </summary>
    public class HttpResultfulJob : IJob
    {
        ITaskManagerService iService = App.GetService<ITaskManagerService>();

        /// <summary>
        /// 執行遠程接口url的定時任務
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<SysSetting>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            AbstractTrigger trigger = (context as JobExecutionContextImpl).Trigger as AbstractTrigger;
            string sqlWhere = string.Format("Id='{0}' and GroupName='{1}'", trigger.Name, trigger.Group);
            TaskManager taskManager = iService.GetWhere(sqlWhere);
            string httpMessage = "";
            if (taskManager == null)
            {
                FileQuartz.WriteErrorLog($"任務不存在");
                return Task.Delay(1);
            }
            FileQuartz.InitTaskJobLogPath(taskManager.Id);
            string msg = $"開始時間:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}";
            //記錄任務執行記錄
            iService.RecordRun(taskManager.Id, JobAction.開始, true, msg);
            if (string.IsNullOrEmpty(taskManager.JobCallAddress) || taskManager.JobCallAddress == "/")
            {
                FileQuartz.WriteErrorLog($"{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}未配置任務地址,");
                iService.RecordRun(taskManager.Id, JobAction.結束, false, "未配置任務地址");
                return Task.Delay(1);
            }
            try
            {
                Dictionary<string, string> header = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(taskManager.JobCallParams))
                {
                    httpMessage = HttpRequestHelper.HttpPost(taskManager.JobCallAddress, taskManager.JobCallParams, null, header);
                }
                else
                {
                    httpMessage = HttpRequestHelper.HttpGet(taskManager.JobCallAddress);
                }
                stopwatch.Stop();
                string content = $"結束時間:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")} 共耗時{stopwatch.ElapsedMilliseconds} 毫秒,消息:{httpMessage??"OK"}\r\n";
                iService.RecordRun(taskManager.Id, JobAction.結束,true, content);
                if ((MsgType)taskManager.SendMail == MsgType.All)
                {
                    string emailAddress = sysSetting.Email;
                    if (!string.IsNullOrEmpty(taskManager.EmailAddress))
                    {
                        emailAddress = taskManager.EmailAddress;
                    }

                    List<string> recipients = new List<string>();
                    recipients = taskManager.EmailAddress.Split(",").ToList();
                    //recipients.Add(taskManager.EmailAddress);
                    var mailBodyEntity = new MailBodyEntity()
                    {
                        Body = content + "\n\r請勿直接回復本郵件！",
                        Recipients = recipients,
                        Subject = taskManager.TaskName,
                    };
                    SendMailHelper.SendMail(mailBodyEntity);
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                string content = $"結束時間:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")} 共耗時{stopwatch.ElapsedMilliseconds} 毫秒\r\n";
                iService.RecordRun(taskManager.Id, JobAction.結束, false, content+ex.Message);
                FileQuartz.WriteErrorLog(ex.Message); 
                if ((MsgType)taskManager.SendMail== MsgType.Error|| (MsgType)taskManager.SendMail == MsgType.All)
                {
                    string emailAddress = sysSetting.Email;
                    if (!string.IsNullOrEmpty(taskManager.EmailAddress))
                    {
                        emailAddress = taskManager.EmailAddress;
                    }
                    List<string> recipients = new List<string>();
                    recipients = emailAddress.Split(",").ToList();
                    var mailBodyEntity = new MailBodyEntity()
                    {
                        Body = ex.Message + "\n\r請勿直接回復本郵件!",
                        Recipients = recipients,
                        Subject = taskManager.TaskName,
                    };
                    SendMailHelper.SendMail(mailBodyEntity);
                }
            }

            return Task.Delay(1);
        }
    }
}
