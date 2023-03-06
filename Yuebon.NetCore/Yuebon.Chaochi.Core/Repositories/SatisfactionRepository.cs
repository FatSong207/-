using System;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using Dapper;
using System.Collections.Generic;
using Yuebon.Commons.Extensions;

namespace Yuebon.Chaochi.Repositories
{
    /// <summary>
    /// 倉儲接口的實現
    /// </summary>
    public class SatisfactionRepository : BaseRepository<Satisfaction, string>, ISatisfactionRepository
    {
        public SatisfactionRepository()
        {
        }

        public SatisfactionRepository(IDbContextCore context) : base(context)
        {

        }

        public async Task<int> GetFinishCount(string questTopicId)
        {
            return await DapperConnRead.QueryFirstOrDefaultAsync<int>($"select Count(*) from QuestTopic as a join SignUpQuest as b on a.QuestTopicSN = b.QuestTopicSN where a.QuestTopicId = '{questTopicId}'");
        }

        public async Task<dynamic> GetTableDataForXLSX(string eventId, string type)
        {
            var subjlist = await GetTableTitleArr(eventId);
            string sql = "";
            sql = $"select b.UserName as 填寫人姓名,iif(b.Sex = 'M','男',iif(b.sex = 'F','女','未知')) as 性別,b.Phone as 手機號碼,c.RealName as 執行業務,ppt.* from  " +
                $"(select c.SignUpQuestSN as 編號,b.Subject,c.Ans from QuestTopic as a " +
                $"join Topic as b on a.QuestTopicSN = b.QuestTopicSN join QuestAns as c on b.TopicSN = c.TopicSN join SignUpQuest as d  on c.SignUpQuestSN = d.SignUpQuestSN  where QuestTopicId='{eventId}' )" +
                $" as st " +
                $"PIVOT(max(Ans)for Subject IN ([{subjlist.Join("],[")}])) as ppt " +
                $"LEFT join SignUpQuest as b on ppt.編號 = b.SignUpQuestSN " +
                $"LEFT join Sys_User as c on b.AgentID = c.Id  ";

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