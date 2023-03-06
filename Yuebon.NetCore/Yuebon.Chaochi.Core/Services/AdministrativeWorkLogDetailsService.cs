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
    /// 行政工作日誌明細服務接口實現
    /// </summary>
    public class AdministrativeWorkLogDetailsService: BaseService<AdministrativeWorkLogDetails,AdministrativeWorkLogDetailsOutputDto, string>, IAdministrativeWorkLogDetailsService
    {
		private readonly IAdministrativeWorkLogDetailsRepository _repository;
        public AdministrativeWorkLogDetailsService(IAdministrativeWorkLogDetailsRepository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}