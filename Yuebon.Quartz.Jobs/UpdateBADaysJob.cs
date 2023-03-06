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
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Json;
using Yuebon.Commons.Options;
using Yuebon.Email;
using Yuebon.Quartz.IServices;
using Yuebon.Quartz.Models;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using System.Data;
using Yuebon.Commons.IDbContext;
using Microsoft.EntityFrameworkCore;
using Yuebon.Commons.Extend;
using Microsoft.EntityFrameworkCore.Storage;

namespace Yuebon.Quartz.Jobs
{
    public class UpdateBADaysJob : IJob
    {
        ITaskManagerService iService = App.GetService<ITaskManagerService>();
        IBuildingAdvertisementService baService = App.GetService<IBuildingAdvertisementService>();
        IBuildingService bService = App.GetService<IBuildingService>();
        IDbContextCore ybContext = App.GetService<IDbContextCore>();

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

            bool updateResult = false;
            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();

            try
            {
                string msg = $"開始時間:{dateTime.ToString("yyyy-MM-dd HH:mm:ss ffff")}";
                //記錄任務執行記錄
                iService.RecordRun(taskManager.Id, JobAction.開始,true, msg);
                //初始化任務日志
                FileQuartz.InitTaskJobLogPath(taskManager.Id);
                var jobId = context.MergedJobDataMap.GetString("OpenJob");

                List<BuildingAdvertisement> baList = baService.GetAll().ToList();

                foreach (BuildingAdvertisement ba in baList) {
                    DateTime today = DateTime.Now;
                    int brDays = 0;
                    int baDays = 0;                    

                    if (ba != null) {
                        Building bInfo = bService.Get(ba.Id);
                        if (bInfo != null) {
                            // 招租天數
                            if ("待招租".Equals(bInfo.BState) && ba.BRStartDate != null) {
                                //var brDaysArray = ToDiffResult(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")), Convert.ToDateTime("2023-01-12"), DiffResultFormat.dd);
                                //if (brDaysArray.Length > 0) {
                                //    Console.WriteLine(brDaysArray[0]);
                                //}

                                var brDaysArray = ExtDate.ToDiffResult(DateTime.Today, (DateTime)ba.BRStartDate, DiffResultFormat.dd);
                                if (brDaysArray.Length > 0) {
                                    brDays = brDaysArray[0];
                                }
                            }
                        }

                        // 上架天數
                        if ("廣告已上架".Equals(ba.BAStatus) && ba.BAStartDate != null) {
                            var baDaysArray = ExtDate.ToDiffResult(DateTime.Today, (DateTime)ba.BAStartDate, DiffResultFormat.dd);
                            if (baDaysArray.Length > 0) {
                                baDays = baDaysArray[0];
                            }
                        }

                        //try {
                            // 更新出租天數
                            //if (brDays > 0) {
                            //    updateResult = baService.UpdateBRDays(sysSetting.ChaochiBuildingAdvertisementDaysUpdateUserId, ba.Id, brDays.ToString(), conn, tran);
                            //}

                            //// 更新出租天數
                            //if (baDays > 0) {
                            //    updateResult = baService.UpdateBADays(sysSetting.ChaochiBuildingAdvertisementDaysUpdateUserId, ba.Id, baDays.ToString(), conn, tran);
                            //}

                            updateResult = baService.UpdateBDays(sysSetting.ChaochiBuildingAdvertisementDaysUpdateUserId, ba.Id, brDays.ToString(), baDays.ToString(), conn, tran);

                        //} catch (Exception ex) {
                        //    eftran.Rollback();
                        //    Log4NetHelper.Error("排程:建物廣告天數更新失敗", ex);

                        //}
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
                    var mailBodyEntity = new MailBodyEntity()
                    {
                        Body = msg + content + ",請勿回復本郵件",
                        Recipients = recipients,
                        Subject = taskManager.TaskName
                    };
                    SendResultEntity sendResult = SendMailHelper.SendMail(mailBodyEntity);
                }

                if (updateResult) {
                    eftran.Commit();
                }
            } catch (Exception ex) {
                eftran.Rollback();
                stopwatch.Stop();
                string content = $"結束時間:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")} 共耗時{stopwatch.ElapsedMilliseconds} 毫秒\r\n";
                iService.RecordRun(taskManager.Id, JobAction.結束, false, content + ex.Message);
                FileQuartz.WriteErrorLog(ex.Message);
                if ((MsgType)taskManager.SendMail == MsgType.Error || (MsgType)taskManager.SendMail == MsgType.All) {
                    string emailAddress = sysSetting.Email;
                    if (!string.IsNullOrEmpty(taskManager.EmailAddress)) {
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
