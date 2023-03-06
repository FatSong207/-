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
using Yuebon.Security.Services;
using Yuebon.Security.IServices;
using IUserService = Yuebon.Security.IServices.IUserService;
using static Yuebon.Chaochi.Repositories.EventRepository;
using System.Linq;
using NPOI.OpenXmlFormats.Dml.Diagram;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class EventService : BaseService<Event, EventOutputDto, string>, IEventService
    {
        private readonly IEventRepository _repository;
        private readonly IUserService _userService;
        private readonly IOrganizeService _organizeService;

        public EventService(IEventRepository repository, IUserService userService, IOrganizeService organizeService) : base(repository)
        {
            _repository = repository;
            _userService = userService;
            _organizeService = organizeService;
        }

        public async Task<bool> DeleteSignUP(string eId)
        {
            var qt = await _repository.GetQuestTopicByEventId(eId);
            if (qt == null) return true;

            var param = new List<Tuple<string, object>>() { };
            param.Add(new Tuple<string, object>("delete from QuestTopic where QuestTopicSN = @QuestTopicSN", new { qt.QuestTopicSN })); //deleteQuestTopic
            param.Add(new Tuple<string, object>("delete from QuestTopicItem where QuestTopicSN = @QuestTopicSN", new { qt.QuestTopicSN }));
            param.Add(new Tuple<string, object>("delete from Topic where QuestTopicSN = @QuestTopicSN", new { qt.QuestTopicSN }));

            var suq = await _repository.GetSignUpQuestByQuestTopicSN(qt.QuestTopicSN);
            if (suq != null) {
                param.Add(new Tuple<string, object>("delete from SignUpQuest where QuestTopicSN = @QuestTopicSN", new { qt.QuestTopicSN }));
                param.Add(new Tuple<string, object>("delete from QuestAns where SignUpQuestSN = @SignUpQuestSN", new { suq.SignUpQuestSN }));
            }
            var result = await ExecuteTransactionAsync(param);
            return result.Item1;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public async Task<PageResult<EventOutputDto>> FindWithPagerSearchAsync(SearchInputDto<EventOutputDto> search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            var filter = search.Filter;

            if (filter is not null) {
                if (!string.IsNullOrEmpty(filter.EventType))
                    where += $" and EventType = '{filter.EventType}' ";
                if (!string.IsNullOrEmpty(filter.EventId))
                    where += $" and EventId like '%{filter.EventId}%' ";
                if (!string.IsNullOrEmpty(filter.EventName))
                    where += $" and EventName like '%{filter.EventName}%' ";
                if (!string.IsNullOrEmpty(filter.StartDate)) {
                    var startdate = Convert.ToDateTime(filter.StartDate).AddYears(1911).ToString("yyyy-MM-dd");
                    where += $" and StartDate >= '{startdate}' ";
                }
                if (!string.IsNullOrEmpty(filter.EndDate)) {
                    var enddate = Convert.ToDateTime(filter.EndDate).AddYears(1911).ToString("yyyy-MM-dd");
                    where += $" and EndDate <= '{enddate}' ";
                }

            }

            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Event> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<EventOutputDto> resultList = list.MapTo<EventOutputDto>();
            List<EventOutputDto> listResult = new List<EventOutputDto>();
            foreach (EventOutputDto item in resultList) {
                item.EndDate = Convert.ToDateTime(item.EndDate).AddYears(-1911).ToString("yyy-MM-dd");
                item.StartDate = Convert.ToDateTime(item.StartDate).AddYears(-1911).ToString("yyy-MM-dd");
                listResult.Add(item);
            }
            PageResult<EventOutputDto> pageResult = new PageResult<EventOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;

        }

        public async Task<PageResult<EventDashboardOutputDto>> FindWithPagerSearchAsyncED(SearchInputDto<EventDashboardOutputDto> search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            var filter = search.Filter;

            if (filter is not null) {
                if (!string.IsNullOrEmpty(filter.BelongCompany))
                    where += $" and BelongCompany = '{filter.BelongCompany}' ";
                if (!string.IsNullOrEmpty(filter.EventId))
                    where += $" and EventId like '%{filter.EventId}%' ";
                if (!string.IsNullOrEmpty(filter.EventName))
                    where += $" and EventName like '%{filter.EventName}%' ";
            }

            var d = DateTime.Now.ToShortDateString();
            where += $" and StartDate <= '{d}' and EndDate >= '{d}' ";
            where += " and EventType = '開放參加' ";

            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Event> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<EventDashboardOutputDto> resultList = list.MapTo<EventDashboardOutputDto>();
            List<EventDashboardOutputDto> listResult = new List<EventDashboardOutputDto>();
            foreach (EventDashboardOutputDto item in resultList) {
                item.BelongCompanyName = _organizeService.Get(item.BelongCompany).FullName;
                //item.ManagerName = _userService.Get(item.Manager).RealName;
                item.EndDate = Convert.ToDateTime(item.EndDate).AddYears(-1911).ToString("yyy-MM-dd");
                item.StartDate = Convert.ToDateTime(item.StartDate).AddYears(-1911).ToString("yyy-MM-dd");
                listResult.Add(item);
            }
            PageResult<EventDashboardOutputDto> pageResult = new PageResult<EventDashboardOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;

        }

        public async Task<dynamic> GetTableData(string eventId)
        {
            var e = await _repository.GetAsync(eventId);
            var data = await _repository.GetTableData(eventId, e.EventType);
            return data;
        }

        public async Task<dynamic> GetTableDataForXLSX(string eventId)
        {
            var e = await _repository.GetAsync(eventId);
            var data = await _repository.GetTableDataForXLSX(eventId, e.EventType);
            return data;
        }

        public async Task<List<string>> GetTableTitleArr(string eventId)
        {
            return await _repository.GetTableTitleArr(eventId);
        }
    }
}