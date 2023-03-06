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
    public class TBNoB4_2Service: BaseService<TBNoB4_2, TBNoB4_2, string>, ITBNoB4_2Service
    {
		private readonly ITBNoB4_2Repository _repository;
        public TBNoB4_2Service(ITBNoB4_2Repository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}