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
    public class TBNoB1_2Service: BaseService<TBNoB1_2, TBNoB1_2, string>, ITBNoB1_2Service
    {
		private readonly ITBNoB1_2Repository _repository;
        public TBNoB1_2Service(ITBNoB1_2Repository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}