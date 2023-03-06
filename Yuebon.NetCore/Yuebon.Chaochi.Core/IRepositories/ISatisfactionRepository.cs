using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義倉儲接口
    /// </summary>
    public interface ISatisfactionRepository:IRepository<Satisfaction, string>
    {
        Task<List<string>> GetTableTitleArr(string eventId);
        Task<dynamic> GetTableDataForXLSX(string eventId, string type);
        Task<int> GetFinishCount(string questTopicId);
    }
}