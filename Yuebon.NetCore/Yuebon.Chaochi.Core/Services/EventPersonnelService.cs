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
    public class EventPersonnelService: BaseService<EventPersonnel,EventPersonnelOutputDto, string>, IEventPersonnelService
    {
		private readonly IEventPersonnelRepository _repository;
        public EventPersonnelService(IEventPersonnelRepository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}