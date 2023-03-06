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
using Yuebon.Email;
using Yuebon.Quartz.IServices;
using Yuebon.Quartz.Models;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Extend;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using SysSetting = Yuebon.Commons.Options.SysSetting;
using Yuebon.Commons.Encrypt;
using Yuebon.Chaochi.Core.Helpers;
using System.Net.Http;
using System.Text.Json;
using User = Yuebon.Security.Models.User;
using YuebonUser = Yuebon.Quartz.Models.User;
using Yuebon.Commons.Extensions;

namespace Yuebon.Quartz.Jobs
{
    public class OrgSyncJob : IJob
    {
        ITaskManagerService iService = App.GetService<ITaskManagerService>();
        IOrganizeService orgService = App.GetService<IOrganizeService>();
        IUserService userService = App.GetService<IUserService>();
        IUserLogOnService userLogOnService = App.GetService<IUserLogOnService>();
        IRoleService roleService = App.GetService<IRoleService>();
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
            if (taskManager == null) {
                FileQuartz.WriteErrorLog($"任務不存在");
                return Task.Delay(1);
            }

            //var chaochiDir = $"{sysSetting.ChaochiFilepath}";
            //var orgImportIdList = new List<OrganizeD>();
            var orgInsertList = new List<Organize>();
            var orgUpdateList = new List<Organize>();
            var orgDeleteIdList = new List<string>();
            //var userImportIdList = new List<UserD>();
            var userInsertList = new List<User>();
            var userUpdateList = new List<User>();
            var userDeleteIdList = new List<string>();
            var userLogOnList = new List<UserLogOn>();
            var APIAuthID = string.Empty;
            var APIAuthCode = string.Empty;
            var deptURI = string.Empty;
            var userURI = string.Empty;
            if (sysSetting != null) {
                APIAuthID = sysSetting.ChaochiAPIAuthID;
                APIAuthCode = sysSetting.ChaochiAPIAuthCode;
                deptURI = sysSetting.ChaochiAPIURLDept;
                userURI = sysSetting.ChaochiAPIURLUser;
            }

            try {
                string msg = $"開始時間:{dateTime.ToString("yyyy-MM-dd HH:mm:ss ffff")}";
                //記錄任務執行記錄
                iService.RecordRun(taskManager.Id, JobAction.開始, true, msg);
                //初始化任務日志
                FileQuartz.InitTaskJobLogPath(taskManager.Id);
                var jobId = context.MergedJobDataMap.GetString("OpenJob");

                // 部門                
                //var orgFilePath = Path.Combine(chaochiDir, @"Orangize\resal.csv");
                //using (var stream = new StreamReader(orgFilePath)) {
                using (HttpClient client = new HttpClient()) {
                    client.DefaultRequestHeaders.Add("AuthID", APIAuthID);
                    client.DefaultRequestHeaders.Add("AuthCode", APIAuthCode);
                    HttpResponseMessage responseDept = client.GetAsync(deptURI).Result;
                    responseDept.EnsureSuccessStatusCode();
                    string responseBodyDept = responseDept.Content.ReadAsStringAsync().Result;
                    var deptData = JsonSerializer.Deserialize<JSONResponse<Dept>>(responseBodyDept);
                    //while (!stream.EndOfStream) {
                    if ("1".Equals(deptData.statusCode) && "Success".Equals(deptData.message)) {
                        //string[] rows = stream.ReadLine().Split(',');
                        //if (rows.Length > 0) {
                        List<Dept> deptList = deptData.data;
                        if (deptList.Count > 0) {
                            foreach (Dept dept in deptList) {
                                string Id = dept.code;

                                // 空值代表正常部門
                                if (string.IsNullOrEmpty(dept.status)) {
                                    Organize org = new Organize
                                    {
                                        // 部門代號(主鍵值)
                                        Id = Id,
                                        // 部門簡稱
                                        ShortName = dept.shortName,
                                        // 部門名稱
                                        FullName = dept.name,
                                        // 上層部門代號
                                        ParentId = dept.upperDeptCode,
                                        // 部門階層
                                        Layers = dept.deptMidLevelCode,
                                        // 部門主管
                                        ManagerId = dept.deptManager,
                                        // 啟用
                                        EnabledMark = true
                                    };
                                    //orgImportIdList.Add(org);
                                    Organize orgDB = orgService.Get(Id);
                                    if (orgDB != null) {
                                        Organize newOrg = Merger.CloneAndMerge<Organize>(orgDB, org);
                                        // 更新
                                        newOrg.LastModifyTime = DateTime.Now;
                                        newOrg.LastModifyUserId = String.Empty;
                                        orgUpdateList.Add(newOrg);
                                    } else {
                                        // 新增
                                        //org.EnabledMark = true;
                                        org.CategoryId = "Department";
                                        org.AllowEdit = true;
                                        org.AllowDelete = true;
                                        org.SortCode = 99;
                                        org.DeleteMark = false;
                                        org.CreatorTime = DateTime.Now;
                                        org.CreatorUserId = String.Empty;
                                        orgInsertList.Add(org);
                                    }
                                } else {
                                    orgDeleteIdList.Add(Id);
                                }
                            }
                        }
                    }
                }
                //orgDeleteIdList = orgService.GetObsoleteOrgIdList(orgImportIdList);


                // 人員
                //var userFilePath = Path.Combine(chaochiDir, @"Orangize\resak.csv");
                //using (var stream = new StreamReader(userFilePath)) {
                //    while (!stream.EndOfStream) {
                //        string[] rows = stream.ReadLine().Split(',');
                //        if (rows.Length > 0) {
                using (HttpClient client = new HttpClient()) {
                    client.DefaultRequestHeaders.Add("AuthID", APIAuthID);
                    client.DefaultRequestHeaders.Add("AuthCode", APIAuthCode);
                    HttpResponseMessage responseUser = client.GetAsync(userURI).Result;
                    responseUser.EnsureSuccessStatusCode();
                    string responseBodyUser = responseUser.Content.ReadAsStringAsync().Result;
                    var userData = JsonSerializer.Deserialize<JSONResponse<YuebonUser>>(responseBodyUser);
                    if ("1".Equals(userData.statusCode) && "Success".Equals(userData.message)) {
                        //string[] rows = stream.ReadLine().Split(',');
                        //if (rows.Length > 0) {
                        List<YuebonUser> userList = userData.data;
                        if (userList.Count > 0) {
                            foreach (YuebonUser user in userList) {
                                string account = user.code;
                                // 帳號
                                if (!account.StartsWith("T") && string.IsNullOrEmpty(user.status) && string.IsNullOrEmpty(user.resignDate)) {
                                    string roleId = roleService.GetRole("usermember").Id;
                                    string roleEncode = user.roleCode;
                                    if (!string.IsNullOrEmpty(roleEncode)) {
                                        string[] roleEncodeArray = roleEncode.Split(',');
                                        var roelIdList = roleEncodeArray.Select(item => roleService.GetRole(item).Id);
                                        roleId = roelIdList.ToArray().Join(",");                                        
                                        //List<string> roleIdList = new List<string>();
                                        //if (roleEncodeArray.Length > 0) {
                                        //    foreach (string roleCode in roleEncodeArray) {
                                        //        string rId = roleService.GetRole(roleCode).Id;
                                        //        roleIdList.Add(rId);
                                        //    }
                                        //}
                                        //if (roleIdList.Count > 0) {
                                        //    roleId = roleIdList.Join(",");
                                        //}
                                    }
                                    User userc = new User
                                    {
                                        // 主鍵值
                                        Id = account,
                                        // 帳號
                                        Account = account,
                                        // 姓名
                                        RealName = user.name,
                                        // 電子信箱
                                        Email = user.eMail,
                                        // 手機(需過濾-符號)
                                        MobilePhone = user.mobilePhone.Replace("-", ""),
                                        // 直屬主管
                                        ManagerId = user.supervisor,
                                        OrganizeId = "Root",
                                        // 直屬部門
                                        DepartmentId = user.departmentCode,
                                        // 職稱
                                        NickName = user.title,
                                        // TODO:角色                                        
                                        RoleId = roleId,
                                        // 啟用
                                        EnabledMark = true
                                    };
                                    //userImportIdList.Add(user);
                                    User usercDB = userService.Get(account);
                                    if (usercDB != null) {
                                        User newUser = Merger.CloneAndMerge<User>(usercDB, userc);
                                        // 更新
                                        newUser.LastModifyTime = DateTime.Now;
                                        newUser.LastModifyUserId = String.Empty;
                                        userUpdateList.Add(newUser);
                                    } else {
                                        // 新增
                                        //user.EnabledMark = true;
                                        userc.IsAdministrator = false;
                                        userc.IsMember = true;
                                        //userc.RoleId = roleService.GetRole("usermember").Id;
                                        userc.SortCode = 99;
                                        userc.DeleteMark = false;
                                        userc.CreatorTime = DateTime.Now;
                                        userc.CreatorUserId = String.Empty;
                                        userInsertList.Add(userc);

                                        // 密碼
                                        string secretKey = MD5Util.GetMD5_16(GuidUtils.NewGuidFormatN()).ToLower();
                                        userLogOnList.Add(new UserLogOn
                                        {
                                            Id = GuidUtils.CreateNo(),
                                            UserId = account,
                                            UserSecretkey = secretKey,
                                            UserPassword = MD5Util.GetMD5_32(DEncrypt.Encrypt(MD5Util.GetMD5_32(account).ToLower(), secretKey).ToLower()).ToLower(),
                                            AllowStartTime = DateTime.Now,
                                            LockEndDate = DateTime.Now,
                                            LockStartDate = DateTime.Now,
                                            ChangePasswordDate = DateTime.Now,
                                            AllowEndTime = DateTime.Now.AddYears(100),
                                            MultiUserLogin = false,
                                            CheckIPAddress = false,
                                            LogOnCount = 0
                                        });
                                    }
                                } else {
                                    userDeleteIdList.Add(account);
                                }
                            }
                        }
                    }
                }
                //userDeleteIdList = userService.GetObsoleteUserIdList(userImportIdList);

                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();

                try {        
                    // 部門
                    if (orgInsertList.Count > 0) { orgService.InsertRange(orgInsertList, conn, tran); }
                    if (orgUpdateList.Count > 0) {                        
                        foreach (Organize od in orgUpdateList) {
                            //if (orgDeleteIdList.Contains(od.Id)) {
                            //    od.EnabledMark = false;
                            //}
                            orgService.Update(od, od.Id, tran);
                        }
                    }
                    if (orgDeleteIdList.Count > 0) {
                        foreach (string id in orgDeleteIdList) {
                            Organize od = orgService.Get(id);
                            if (od != null) {
                                od.EnabledMark = false;
                                od.LastModifyTime = DateTime.Now;
                                orgService.Update(od, od.Id, tran);
                            }
                        }
                    }
                    //if (orgDeletesInutDto.Ids.Length > 0) { orgService.DeleteBatchWhere(orgDeletesInutDto, tran); }

                    // 人員
                    if (userInsertList.Count > 0) {
                        userService.InsertRange(userInsertList, conn, tran);
                        userLogOnService.InsertRange(userLogOnList, conn, tran);
                    }
                    if (userUpdateList.Count > 0) {
                        foreach (User ud in userUpdateList) {
                            //if (userDeleteIdList.Contains(ud.Id)) {
                            //    ud.EnabledMark = false;
                            //}
                            userService.Update(ud, ud.Id, tran);
                        }
                    }
                    if (userDeleteIdList.Count > 0) {
                        foreach (string account in userDeleteIdList) {
                            User ud = userService.Get(account);
                            if (ud != null) {
                                ud.EnabledMark = false;
                                ud.LastModifyTime = DateTime.Now;
                                userService.Update(ud, ud.Id, tran);
                            }
                        }
                    }
                    //if (userDeleteList.Count > 0) {
                    //    foreach (UserD ud in userDeleteList) {
                    //        ud.EnabledMark = false;
                    //        ud.LastModifyTime = DateTime.Now;
                    //        userService.UpdateAsync(ud, ud.Id, conn, tran);
                    //    }
                    //}

                    //eftran.Commit();

                } catch(Exception ex) {
                    //eftran.Rollback();
                    stopwatch.Stop();
                    string contentex = $"結束時間:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")} 共耗時{stopwatch.ElapsedMilliseconds} 毫秒\r\n";
                    iService.RecordRun(taskManager.Id, JobAction.結束, false, contentex + ex.Message);
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

            } catch (Exception ex) {
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
