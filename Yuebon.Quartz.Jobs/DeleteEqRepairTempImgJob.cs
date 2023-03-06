using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Core.App;
using Yuebon.Commons.Enums;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IoC;
using Yuebon.Commons.Json;
using Yuebon.Email;
using Yuebon.Quartz.IServices;
using Yuebon.Quartz.Models;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.Quartz.Jobs
{
    /// <summary>
    /// 刪除修繕管理的暫存
    /// 
    /// </summary>
    public class DeleteEqRepairTempImgJob : IJob
    {
        ITaskManagerService iService = App.GetService<ITaskManagerService>();

        ITaskJobsLogService iJobLogService = App.GetService<ITaskJobsLogService>();
        ILogService iLogService = App.GetService<ILogService>();

        public Task Execute(IJobExecutionContext context)
        {
            DateTime dateTime = DateTime.Now; 
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<SysSetting>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            AbstractTrigger trigger = (context as JobExecutionContextImpl).Trigger as AbstractTrigger;
            string sqlWhere = string.Format("Id='{0}' and GroupName='{1}'", trigger.Name, trigger.Group);
            TaskManager taskManager = iService.GetWhere(sqlWhere);
            if (taskManager == null)
            {
                FileQuartz.WriteErrorLog($"任務不存在");
                return Task.Delay(1);
            }
            try
            {
                string msg = $"開始時間:{dateTime.ToString("yyyy-MM-dd HH:mm:ss ffff")}";
                //記錄任務執行記錄
                iService.RecordRun(taskManager.Id, JobAction.開始, true, msg);
                //初始化任務日志
                FileQuartz.InitTaskJobLogPath(taskManager.Id);

                var jobId = context.MergedJobDataMap.GetString("OpenJob");

                //下方方代碼片段需客製化
                {
                    var chaochiDir = $"{sysSetting.ChaochiFilepath}";
                    var eqRepairTempImgDir = Path.Combine(chaochiDir,@"EqRepair\temp\");
                    if (Directory.Exists(eqRepairTempImgDir)) {
                        DirectoryInfo di = new DirectoryInfo(eqRepairTempImgDir);
                        foreach (FileInfo file in di.EnumerateFiles()) {
                            file.Delete();
                        }
                        foreach (DirectoryInfo subDirectory in di.EnumerateDirectories()) {
                            subDirectory.Delete(true);
                        }
                    }
                }

                stopwatch.Stop();
                string content = $"結束時間:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")} 共耗時{stopwatch.ElapsedMilliseconds} 毫秒\r\n";
                iService.RecordRun(taskManager.Id, JobAction.結束, true, content);
                if ((MsgType)taskManager.SendMail == MsgType.All) {
                    string emailAddress = sysSetting.Email;
                    if (!string.IsNullOrEmpty(taskManager.EmailAddress)) {
                        emailAddress = taskManager.EmailAddress;
                    }

                    List<string> recipients = new List<string>();
                    recipients = emailAddress.Split(",").ToList();
                    var mailBodyEntity = new MailBodyEntity() {
                        Body = content + ",請勿回復本郵件",
                        Recipients = recipients,
                        Subject = taskManager.TaskName
                    };
                    SendMailHelper.SendMail(mailBodyEntity);
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                string content = $"結束時間:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")} 共耗時{stopwatch.ElapsedMilliseconds} 毫秒\r\n";
                iService.RecordRun(taskManager.Id, JobAction.結束, false, content+ ex.Message);
                FileQuartz.WriteErrorLog(ex.Message);
                if ((MsgType)taskManager.SendMail == MsgType.Error || (MsgType)taskManager.SendMail == MsgType.All)
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
                        Body = "處理失敗," + ex.Message + ",請勿回復本郵件",
                        Recipients = recipients,
                        Subject = taskManager.TaskName
                    };
                    SendMailHelper.SendMail(mailBodyEntity);

                }
            }

            return Task.Delay(1);
        }
    }
}
