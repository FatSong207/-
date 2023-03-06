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

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class EventQuestionnaireService : BaseService<EventQuestionnaire, EventQuestionnaireOutputDto, string>, IEventQuestionnaireService
    {
        private readonly IEventQuestionnaireRepository _repository;
        public EventQuestionnaireService(IEventQuestionnaireRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<bool> UpdateEventQuestionnaire(EventQuestionnaireInputDto inputDto)
        {
            var eq = await _repository.GetAsync(inputDto.Id);
            if (eq == null) return false;

            var param = new List<Tuple<string, object>>() { };
            param.Add(new Tuple<string, object>("update Chaochi_EventQuestionnaire set QName=@QName, QBegingWords=@QBegingWords, QEndWords=@QEndWords where Id=@Id", new { inputDto.QName, inputDto.QBegingWords, inputDto.QEndWords, inputDto.Id }));
            param.Add(new Tuple<string, object>("update QuestTopic set Subject=@QName, RemarkTop=@QBegingWords, RemarkEnd=@QEndWords where QuestTopicId=@EId", new { inputDto.QName, inputDto.QBegingWords, inputDto.QEndWords, inputDto.EId }));

            var result = await ExecuteTransactionAsync(param);
            return result.Item1;
        }
    }
}