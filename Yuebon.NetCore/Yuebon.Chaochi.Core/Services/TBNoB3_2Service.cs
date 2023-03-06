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
    public class TBNoB3_2Service: BaseService<TBNoB3_2, TBNoB3_2, string>, ITBNoB3_2Service
    {
		private readonly ITBNoB3_2Repository _repository;
        public TBNoB3_2Service(ITBNoB3_2Repository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}