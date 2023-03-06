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
    public class TBNoB2_2Service: BaseService<TBNoB2_2, TBNoB2_2, string>, ITBNoB2_2Service
    {
		private readonly ITBNoB2_2Repository _repository;
        public TBNoB2_2Service(ITBNoB2_2Repository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}