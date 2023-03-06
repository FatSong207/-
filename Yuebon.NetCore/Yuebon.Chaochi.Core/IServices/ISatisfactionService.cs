using System;
using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using Yuebon.Commons.Pages;
using System.Data;
using Yuebon.Commons.Dtos;
using System.Collections.Generic;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface ISatisfactionService:IService<Satisfaction,SatisfactionOutputDto, string>
    {
        Task<List<string>> GetTableTitleArr(string eventId);
        Task<dynamic> GetTableDataForXLSX(string eventId);

        Task<int> GetFinishCount(string questTopicId);
    }
}
