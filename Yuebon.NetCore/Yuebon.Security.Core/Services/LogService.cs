using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Net;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;
using Zxw.Framework.NetCore.Extensions;

namespace Yuebon.Security.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class LogService : BaseService<Log, LogOutputDto, string>, ILogService
    {
        private readonly ILogRepository _iLogRepository;
        private readonly IUserRepository _iuserRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userRepository"></param>
        public LogService(ILogRepository repository, IUserRepository userRepository) : base(repository)
        {
            _iLogRepository = repository;
            _iuserRepository = userRepository;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public async Task<PageResult<LogOutputDto>> FindWithPagerSearchAsync(SearchLogModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            if (!string.IsNullOrEmpty(search.CreatorTime1))
            {
                where += " and CreatorTime >='"+ search.CreatorTime1.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            }
            if (!string.IsNullOrEmpty(search.CreatorTime2))
            {
                where += " and CreatorTime <='" + search.CreatorTime2.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            }
            if (!string.IsNullOrEmpty(search.Filter.Type))
            {
                where += " and Type='"+ search.Filter.Type + "'";
            }
            if (!string.IsNullOrEmpty(search.Filter.IPAddress))
            {
                where += string.Format(" and IPAddress = '{0}'", search.Filter.IPAddress);
            };
            if (!string.IsNullOrEmpty(search.Filter.Account))
            {
                where += string.Format(" and Account = '{0}'", search.Filter.Account);
            };
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };

            //Expression<Func<Log, bool>> filter = log => true;
            //if (!string.IsNullOrEmpty(search.Keywords))
            //{
            //    filter = filter.And(log => log.Account.StartsWith(search.Keywords) || log.ModuleName.StartsWith(search.Keywords) || log.IPAddress.StartsWith(search.Keywords)
            // || log.IPAddressName.StartsWith(search.Keywords) || log.Description.StartsWith(search.Keywords));
            //}
            //if (!string.IsNullOrEmpty(search.EnCode))
            //{
            //    filter = filter.And(log=>search.EnCode.Contains(log.Type));
            //}
            List<Log> list = await _iLogRepository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            PageResult<LogOutputDto> pageResult = new PageResult<LogOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<LogOutputDto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        /// <summary>
        /// 根據相關信息，寫入用戶的操作日志記錄
        /// </summary>
        /// <param name="tableName">操作表名稱</param>
        /// <param name="operationType">操作類型</param>
        /// <param name="note">操作詳細表述</param>
        /// <returns></returns>
        public bool OnOperationLog(string tableName, string operationType, string note)
        {
            try
            {
                //雖然實現了這個事件，但是我們還需要判斷該表是否在配置表里面，如果不在，則不記錄操作日志。
                //var identities = _httpContextAccessor.HttpContext.User.Identities;
                if (HttpContextHelper.HttpContext == null)
                {
                    return false;
                }
                var identities =HttpContextHelper.HttpContext.User.Identities;
                var claimsIdentity = identities.First<ClaimsIdentity>();
                List<Claim> claimlist = claimsIdentity.Claims as List<Claim>;
                string userId = claimlist[0].Value;
                YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                YuebonCurrentUser CurrentUser = new YuebonCurrentUser();
                var user = yuebonCacheHelper.Get("login_user_" + userId).ToJson().ToObject<YuebonCurrentUser>();
                if (user != null)
                {
                    CurrentUser = user;
                    bool insert = operationType == DbLogType.Create.ToString(); ;//&& settingInfo.InsertLog;
                    bool update = operationType == DbLogType.Update.ToString();// && settingInfo.UpdateLog;
                    bool delete = operationType == DbLogType.Delete.ToString();// && settingInfo.DeleteLog;
                    bool deletesoft = operationType == DbLogType.DeleteSoft.ToString();// && settingInfo.DeleteLog;
                    bool exception = operationType == DbLogType.Exception.ToString();// && settingInfo.DeleteLog;
                    bool sql = operationType == DbLogType.SQL.ToString();// && settingInfo.DeleteLog;

                    if (insert || update || delete || deletesoft || exception || sql)
                    {
                        Log info = new Log();
                        info.ModuleName = tableName;
                        info.Type = operationType;
                        info.Description = note;
                        info.Date = info.CreatorTime = DateTime.Now;
                        info.CreatorUserId = CurrentUser.UserId;
                        info.Account = CurrentUser.Account;
                        info.NickName = CurrentUser.NickName;
                        info.OrganizeId = CurrentUser.OrganizeId;
                        info.IPAddress = CurrentUser.CurrentLoginIP;
                        info.IPAddressName = CurrentUser.IPAddressName;
                        info.Result = true;
                        long lg = _iLogRepository.Insert(info);
                        if (lg > 0)
                        {
                            return true;
                        }
                    }
                }
            }catch(Exception ex)
            {
                Log4NetHelper.Error("",ex);
                return false;
            }
            return false;
        }


        /// <summary>
        /// 根據相關信息，寫入用戶的操作日志記錄
        /// 主要用于寫操作模塊日志
        /// </summary>
        /// <param name="module">操作模塊名稱</param>
        /// <param name="operationType">操作類型</param>
        /// <param name="note">操作詳細表述</param>
        /// <param name="currentUser">操作用戶</param>
        /// <returns></returns>
        public bool OnOperationLog(string module, string operationType, string note, YuebonCurrentUser currentUser)
        {
            //雖然實現了這個事件，但是我們還需要判斷該表是否在配置表里面，如果不在，則不記錄操作日志。
            //OperationLogSettingInfo settingInfo = BLLFactory<OperationLogSetting>.Instance.FindByTableName(tableName, trans);
            
            if (currentUser != null)
            {
                bool login = operationType == DbLogType.Login.ToString();
                bool visit = operationType == DbLogType.Visit.ToString();
                bool exit = operationType == DbLogType.Exit.ToString();
                bool other = operationType == DbLogType.Other.ToString();
                bool insert = operationType == DbLogType.Create.ToString();
                bool update = operationType == DbLogType.Update.ToString();
                bool delete = operationType == DbLogType.Delete.ToString();
                bool deletesoft = operationType == DbLogType.DeleteSoft.ToString();
                bool exception = operationType == DbLogType.Exception.ToString();
                if (login|| visit|| exit|| other||insert || update || delete || deletesoft || exception)
                {
                    Log info = new Log();
                    info.ModuleName = module;
                    info.Type = operationType;
                    info.Description = note;
                    info.Date = info.CreatorTime = DateTime.Now;
                    info.CreatorUserId = currentUser.UserId;
                    info.Account = currentUser.Account;
                    info.NickName = currentUser.NickName;
                    info.OrganizeId = currentUser.OrganizeId;
                    info.IPAddress = currentUser.CurrentLoginIP;
                    info.IPAddressName = IpAddressUtil.GetCityByIp(currentUser.CurrentLoginIP);
                    info.Result = true;
                    long lg = _iLogRepository.Insert(info);
                    if (lg > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}