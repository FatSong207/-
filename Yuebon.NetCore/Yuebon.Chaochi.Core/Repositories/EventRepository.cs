using System;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using Yuebon.Commons.Extensions;
using Microsoft.Extensions.Logging;

namespace Yuebon.Chaochi.Repositories
{
    /// <summary>
    /// 倉儲接口的實現
    /// </summary>
    public class EventRepository : BaseRepository<Event, string>, IEventRepository
    {
        public EventRepository()
        {
        }

        public EventRepository(IDbContextCore context) : base(context)
        {

        }

        public async Task<QuestTopic> GetQuestTopicByEventId(string eventId)
        {
            return await DapperConnRead.QueryFirstOrDefaultAsync<QuestTopic>("select * from QuestTopic where QuestTopicId=@QuestTopicId", new { QuestTopicId = eventId });
        }

        public async Task<SignUpQuest> GetSignUpQuestByQuestTopicSN(int questTopicSN)
        {
            return await DapperConnRead.QueryFirstOrDefaultAsync<SignUpQuest>("select * from SignUpQuest where QuestTopicSN=@QuestTopicSN", new { QuestTopicSN = questTopicSN });
        }

        public async Task<dynamic> GetTableData(string eventId, string type)
        {
            var subjlist = await GetTableTitleArr(eventId);
            string sql = "";
            if (type == "指定來賓") {
                sql = $"select ppt.*,b.UserName,b.Sex,b.Phone from  " +
                    $"(select c.SignUpQuestSN as 編號,b.Subject,c.Ans from QuestTopic as a " +
                    $"join Topic as b on a.QuestTopicSN = b.QuestTopicSN join QuestAns as c on b.TopicSN = c.TopicSN join SignUpQuest as d  on c.SignUpQuestSN = d.SignUpQuestSN  where QuestTopicId='{eventId}' )" +
                    $" as st " +
                    $"PIVOT(max(Ans)for Subject IN ([{subjlist.Join("],[")}])) as ppt " +
                    $"join SignUpQuest as b on ppt.編號 = b.SignUpQuestSN  ";
            } else {
                sql = $"select ppt.*,b.UserName,b.Sex,b.Phone,b.AgentID,c.RealName,d.FullName from  " +
                    $"(select c.SignUpQuestSN as 編號,b.Subject,c.Ans from QuestTopic as a " +
                    $"join Topic as b on a.QuestTopicSN = b.QuestTopicSN join QuestAns as c on b.TopicSN = c.TopicSN join SignUpQuest as d  on c.SignUpQuestSN = d.SignUpQuestSN  where QuestTopicId='{eventId}' )" +
                    $" as st " +
                    $"PIVOT(max(Ans)for Subject IN ([{subjlist.Join("],[")}])) as ppt " +
                    $"join SignUpQuest as b on ppt.編號 = b.SignUpQuestSN " +
                    $"join Sys_User as c on b.AgentID = c.Id  " +
                    $"join Sys_Organize as d on c.DepartmentId=d.Id ";
            }

            var list = await DapperConnRead.QueryAsync(sql);
            return list;
        }

        public async Task<dynamic> GetTableDataForXLSX(string eventId, string type)
        {
            var subjlist = await GetTableTitleArr(eventId);
            string sql = "";
            if (type == "指定來賓") {
                sql = $"select b.UserName as 填寫人姓名,iif(b.Sex = 'M','男',iif(b.sex = 'F','女','未知')) as 性別,b.Phone as 手機號碼,ppt.* from  " +
                    $"(select c.SignUpQuestSN as 編號,b.Subject,c.Ans from QuestTopic as a " +
                    $"join Topic as b on a.QuestTopicSN = b.QuestTopicSN join QuestAns as c on b.TopicSN = c.TopicSN join SignUpQuest as d  on c.SignUpQuestSN = d.SignUpQuestSN  where QuestTopicId='{eventId}' )" +
                    $" as st " +
                    $"PIVOT(max(Ans)for Subject IN ([{subjlist.Join("],[")}])) as ppt " +
                    $"join SignUpQuest as b on ppt.編號 = b.SignUpQuestSN  ";
            } else {
                sql = $"select b.UserName as 填寫人姓名,iif(b.Sex = 'M','男',iif(b.sex = 'F','女','未知')) as 性別,b.Phone as 手機號碼,c.RealName as 執行業務,d.FullName as 負責人所屬店,ppt.* from  " +
                    $"(select c.SignUpQuestSN as 編號,b.Subject,c.Ans from QuestTopic as a " +
                    $"join Topic as b on a.QuestTopicSN = b.QuestTopicSN join QuestAns as c on b.TopicSN = c.TopicSN join SignUpQuest as d  on c.SignUpQuestSN = d.SignUpQuestSN  where QuestTopicId='{eventId}' )" +
                    $" as st " +
                    $"PIVOT(max(Ans)for Subject IN ([{subjlist.Join("],[")}])) as ppt " +
                    $"join SignUpQuest as b on ppt.編號 = b.SignUpQuestSN " +
                    $"join Sys_User as c on b.AgentID = c.Id " +
                    $"join Sys_Organize as d on c.DepartmentId=d.Id ";
            }

            var list = await DapperConnRead.QueryAsync(sql);
            return list;
        }

        public async Task<List<string>> GetTableTitleArr(string eventId)
        {
            string sql = $" select  b.Subject from QuestTopic as a join Topic as b on a.QuestTopicSN = b.QuestTopicSN  where QuestTopicId='{eventId}' order by TopicSN ";
            var list = await DapperConnRead.QueryAsync<string>(sql);
            var result = list.AsList();
            return result;
        }

        public class GetTableDataReuslt
        {
            public string QuestTopicSN { get; set; }
            public string TopicSN { get; set; }
            public string SignUpQuestSN { get; set; }
            public string Subject { get; set; }
            public string Ans { get; set; }
            public string UserID { get; set; }
            public string UserName { get; set; }
            public string Phone { get; set; }
        }

        #region Dapper 操作

        //DapperConn 用于讀寫操作
        //DapperConnRead 用于只讀操作

        #endregion


        #region EF 操作

        //DbContext 用于讀寫操作
        //DbContextRead 用于只讀操作

        #endregion
    }
}