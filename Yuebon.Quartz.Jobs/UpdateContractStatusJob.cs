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
using System.Globalization;
using System.Data;
using Yuebon.Commons.IDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Org.BouncyCastle.Cms;
using Yuebon.Chaochi.Core.Helpers;

namespace Yuebon.Quartz.Jobs
{
    public class UpdateContractStatusJob : IJob
    {
        ITaskManagerService iService = App.GetService<ITaskManagerService>();
        IContractService contractService = App.GetService<IContractService>();
        IBuildingService buildingService = App.GetService<IBuildingService>();
        IDbContextCore ybContext = App.GetService<IDbContextCore>();
        ISendMailInfoService sendMailInfoService = App.GetService<ISendMailInfoService>();

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
            if (taskManager == null) {
                FileQuartz.WriteErrorLog($"任務不存在");
                return Task.Delay(1);
            }

            bool updateContractStatusResult = false;

            using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
            conn.Open();
            using var eftran = ybContext.GetDatabase().BeginTransaction();
            var tran = eftran.GetDbTransaction();


            try {
                string msg = $"開始時間:{dateTime.ToString("yyyy-MM-dd HH:mm:ss ffff")}";
                //記錄任務執行記錄
                iService.RecordRun(taskManager.Id, JobAction.開始, true, msg);
                //初始化任務日志
                FileQuartz.InitTaskJobLogPath(taskManager.Id);
                var jobId = context.MergedJobDataMap.GetString("OpenJob");

                List<Contract> contractList = contractService.GetAll().ToList();

                foreach (Contract contract in contractList) {
                    // 合約起始日期大於系統日期，合約狀態為「有效合約」;小於等於系統日期，合約狀態為「尚未起租」
                    if (!string.IsNullOrWhiteSpace(contract.ContractDate) && !string.IsNullOrWhiteSpace(contract.BRDStart) && !string.IsNullOrWhiteSpace(contract.BRDTEnd)) {
                        DateTime today = DateTime.Now;
                        CultureInfo culture = new CultureInfo("zh-TW");
                        culture.DateTimeFormat.Calendar = new TaiwanCalendar();

                        DateTime contractEffiectiveDate = DateTime.Parse(contract.BRDStart, culture);
                        int timeCompareResult = DateTime.Compare(contractEffiectiveDate, today);

                        DateTime contractEndDate = DateTime.Parse(contract.BRDTEnd, culture);
                        int timeCompareResult2 = DateTime.Compare(contractEndDate, today);

                        // 更新合約狀態
                        if (timeCompareResult <= 0 && timeCompareResult2 > 0) {
                            if (contract.CStatus != "效期中") {
                                updateContractStatusResult = contractService.UpdateContractStatusTask(sysSetting.ChaochiContractStatusUpdateUserId, contract.CID, "效期中", "", conn, tran);
                                //var cr = await SignUpHelper.EncryptURLAsync("001", contract.Sales);
                                //if (cr.ResData != null) {
                                //   await sendMailInfoService.InsertAsync(new SendMailInfo {
                                //        RefKey = contract.Id,
                                //        Subject = $"[調查]*房東滿意度調查",
                                //        Recipient = "opqr24680@gmail.com",
                                //        Enable = true,
                                //        SendTime = DateTime.Now,
                                //        Body = cr.ResData.ToString(),
                                //    });
                                //}

                            }
                        } else if (timeCompareResult <= 0 && timeCompareResult2 <= 0) {
                            if (contract.CStatus != "待招租") {
                                updateContractStatusResult = contractService.UpdateContractStatusTask(sysSetting.ChaochiContractStatusUpdateUserId, contract.CID, "已到期", "", conn, tran);

                                // 改變建物狀態
                                string badd = contract.BAdd;
                                if (badd.Contains(";") && !badd.Contains("\n")) {
                                    string bId = buildingService.GetIdByBAdd(badd);
                                    // 建物狀態需改為[待招租]
                                    buildingService.UpdateBStateTask(sysSetting.ChaochiContractStatusUpdateUserId, bId, "待招租", conn, tran);
                                }
                            }
                        } else {
                            if (contract.CStatus != "待生效") {
                                updateContractStatusResult = contractService.UpdateContractStatusTask(sysSetting.ChaochiContractStatusUpdateUserId, contract.CID, "待生效", "", conn, tran);
                            }
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
                        Body = msg + content + ",請勿回復本郵件",
                        Recipients = recipients,
                        Subject = taskManager.TaskName
                    };
                    SendMailHelper.SendMail(mailBodyEntity);

                }

                if (updateContractStatusResult) {
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
                    var mailBodyEntity = new MailBodyEntity() {
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
