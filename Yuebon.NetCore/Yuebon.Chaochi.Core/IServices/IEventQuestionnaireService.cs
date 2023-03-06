using System;
using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface IEventQuestionnaireService:IService<EventQuestionnaire,EventQuestionnaireOutputDto, string>
    {
        Task<bool> UpdateEventQuestionnaire(EventQuestionnaireInputDto inputDto);
    }
}
