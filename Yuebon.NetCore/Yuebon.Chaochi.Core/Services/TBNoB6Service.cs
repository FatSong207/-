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
    /// 用戶服務接口實現
    /// </summary>
    public class TBNoB6Service: BaseService<TBNoB6,TBNoB6OutputDto, string>, ITBNoB6Service
    {
		private readonly ITBNoB6Repository _repository;
        public TBNoB6Service(ITBNoB6Repository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}