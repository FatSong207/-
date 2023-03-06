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
    /// 合約匯款帳號維護服務接口實現
    /// </summary>
    public class ContractRemittanceService: BaseService<ContractRemittance,ContractRemittanceOutputDto, string>, IContractRemittanceService
    {
		private readonly IContractRemittanceRepository _repository;
        public ContractRemittanceService(IContractRemittanceRepository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}