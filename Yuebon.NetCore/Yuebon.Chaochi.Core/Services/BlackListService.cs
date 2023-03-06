using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Data;
using System.Net.Http;
using Yuebon.Commons.Log;
using System.Globalization;
using Yuebon.Commons.Core.DataManager;
using Yuebon.Commons;
using Microsoft.Extensions.Configuration;
using Yuebon.Security.IServices;
using Yuebon.Commons.Extensions;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class BlackListService : BaseService<BlackList, BlackListOutputDto, string>, IBlackListService
    {
        private readonly IBlackListRepository _repository;
        private readonly IOrganizeService _organizeService;
        private readonly Security.IRepositories.IUserRepository _userRepository;
        private readonly Security.IServices.IUserService _userService;

        public BlackListService(IBlackListRepository repository, IOrganizeService organizeService, Security.IRepositories.IUserRepository userRepository, Security.IServices.IUserService userService) : base(repository)
        {
            _repository = repository;
            _organizeService = organizeService;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<string> SendApproval(BlackList blackList)
        {
            using HttpClient client = new HttpClient();
            try {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("AuthID", "CRMAPI");
                client.DefaultRequestHeaders.Add("AuthCode", "Ye3b5PBlQOEPGxD+OZeLlT0F/yRN9c3ycJTYtGaWd1s=");
                var gender = blackList.IDNo.Substring(1, 1) == "1" ? "1" : blackList.IDNo.Substring(1, 1) == "2" ? "2" : "0";
                var cul = new CultureInfo("zh-TW");
                cul.DateTimeFormat.Calendar = new TaiwanCalendar();
                var birthday = DateTime.Parse(blackList.Birthday, cul).ToString("yyyy/MM/dd");
                //var birthYear = Convert.ToInt64(blackList.Birthday.Split("-")[0]) + 1911;
                //var birthday = birthYear.ToString() + blackList.Birthday.Split("-")[1] + blackList.Birthday.Split("-")[2];
                var form = new Dictionary<string, string>
                {
                    { "Name", blackList.Name},//審查者姓名(必填)
                       { "ID", blackList.IDNo},//審查者身分證字號或居留證號碼(必填)
                       { "Birthday", birthday},//生日(必填)
                       { "Type", "1"},//審查者類型 1.國人 2.外國人(必填)
                       { "Gender",gender },//性別1.男 2.女(非必填)
                       { "BuildID", ""},//物件編號(非必填)
                       { "RoomID", ""},////房間編號(非必填)
                       { "CaseID", ""}////合約編號(非必填)
                };

                var url = Configs.GetSection("biliApiUrl").Value;

                var result = await client.PostAsync(url, new FormUrlEncodedContent(form));
                string resultContent = await result.Content.ReadAsStringAsync();
                Console.WriteLine(resultContent);
                return resultContent;

            } catch (HttpRequestException ex) {
                Log4NetHelper.Error("審查送審失敗!", ex);
                return "Err";
                //Console.WriteLine("\nException Caught!");
                //Console.WriteLine("Message :{0} ", e.Message);
            }
        }


        public override async Task<BlackListOutputDto> GetOutDtoAsync(string id)
        {
            var info = await repository.GetAsync(id);
            var outinfo = info.MapTo<BlackListOutputDto>();
            outinfo.CreatorUserName = _userService.GetAsync(info.CreatorUserId).Result.RealName;
            return outinfo;
        }

        public override async Task<PageResult<BlackListOutputDto>> FindWithPagerAsync(SearchInputDto<BlackList> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            var f = search.Filter;
            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            if (!string.IsNullOrEmpty(f.ReportDept)) {
                var depts = _userRepository.GetPermissionDepts(f.ReportDept);
                where += $" and CreatorUserDeptId in ('{depts.Join("','")}') ";
                //var o = await _organizeService.GetAsync(f.ReportDept);
                //where += $" and ReportDept = N'{o.FullName}' ";
            }
            if (!string.IsNullOrEmpty(f.ResultState)) {
                where += $" and ResultState = '{f.ResultState}' ";
            }
            if (!string.IsNullOrEmpty(f.IDNo)) {
                where += $" and IDNo like '%{f.IDNo}%' ";
            }
            if (!string.IsNullOrEmpty(f.Name)) {
                where += $" and Name like N'%{f.Name}%' ";
            }
            List<BlackList> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order, conn, trans);
            var outPutList = list.MapTo<BlackListOutputDto>();
            var outPutList_2 = new List<BlackListOutputDto>();
            foreach (var item in outPutList) {
                item.CreatorUserName = _userService.GetAsync(item.CreatorUserId).Result.RealName;
                if (item.LastModifyUserId != "系統自動") {
                    item.LastModifyUserId = _userService.GetAsync(item.LastModifyUserId).Result.RealName;
                }
                item.IDNo = IDNoMask(item.IDNo);
                outPutList_2.Add(item);
            }
            PageResult<BlackListOutputDto> pageResult = new PageResult<BlackListOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = outPutList_2,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        #region 輔助方法

        private string IDNoMask(string idNo)
        {
            idNo = idNo.Remove(4, 3);
            idNo = idNo.Insert(4, "***");
            return idNo;
        }

        #endregion 輔助方法

    }
}