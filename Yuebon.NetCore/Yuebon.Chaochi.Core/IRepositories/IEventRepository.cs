using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Yuebon.Chaochi.Repositories.EventRepository;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義倉儲接口
    /// </summary>
    public interface IEventRepository:IRepository<Event, string>
    {
        Task<List<string>> GetTableTitleArr(string eventId);
        Task<dynamic> GetTableData(string eventId, string type);
        Task<dynamic> GetTableDataForXLSX(string eventId,string type);
        Task<QuestTopic> GetQuestTopicByEventId(string eventId);
        Task<SignUpQuest> GetSignUpQuestByQuestTopicSN(int questTopicSN);
    }
}