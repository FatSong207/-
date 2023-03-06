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
    /// 業務工作日誌明細服務接口實現
    /// </summary>
    public class SalesWorkLogDetailsService: BaseService<SalesWorkLogDetails,SalesWorkLogDetailsOutputDto, string>, ISalesWorkLogDetailsService
    {
		private readonly ISalesWorkLogDetailsRepository _repository;
        public SalesWorkLogDetailsService(ISalesWorkLogDetailsRepository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}