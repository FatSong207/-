using System;
using System.Data.Common;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Repositories;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using System.Data;
using Yuebon.Chaochi.Dtos;
using System.Collections.Generic;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Mapping;
using System.Threading.Tasks;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Enums;
using Yuebon.Chaochi.Core.IServices;
using Yuebon.Commons.IRepositories;
using Yuebon.Commons.IServices;

namespace Yuebon.Chaochi.Core.Services
{
    public class CustomerLSBuildingService : BaseService<CustomerLSBuilding, CustomerLSBuildingOutputDto, string>, ICustomerLSBuildingService
    {
        private ICustomerLSBuildingRepository _customerLSBuildingRepository;

        public CustomerLSBuildingService(ICustomerLSBuildingRepository customerLSBuildingRepository):base(customerLSBuildingRepository)
        {
            _customerLSBuildingRepository = customerLSBuildingRepository;
        }

        public async Task<IEnumerable<CustomerLSBuilding>> FindByLSID(string LSID)
        {
            return await _customerLSBuildingRepository.FindByLSID(LSID);
        }

        PageResult<CustomerLNOutputDto> IService<CustomerLSBuilding, CustomerLNOutputDto, string>.FindWithPager(SearchInputDto<CustomerLSBuilding> search, IDbConnection conn, IDbTransaction trans)
        {
            throw new NotImplementedException();
        }

        Task<PageResult<CustomerLNOutputDto>> IService<CustomerLSBuilding, CustomerLNOutputDto, string>.FindWithPagerAsync(SearchInputDto<CustomerLSBuilding> search, IDbConnection conn, IDbTransaction trans)
        {
            throw new NotImplementedException();
        }

        CustomerLNOutputDto IService<CustomerLSBuilding, CustomerLNOutputDto, string>.GetOutDto(string id)
        {
            throw new NotImplementedException();
        }

        Task<CustomerLNOutputDto> IService<CustomerLSBuilding, CustomerLNOutputDto, string>.GetOutDtoAsync(string id)
        {
            throw new NotImplementedException();
        }

        CustomerLNOutputDto IService<CustomerLSBuilding, CustomerLNOutputDto, string>.GetOutDtoWhere(string where, IDbConnection conn, IDbTransaction trans)
        {
            throw new NotImplementedException();
        }

        Task<CustomerLNOutputDto> IService<CustomerLSBuilding, CustomerLNOutputDto, string>.GetOutDtoWhereAsync(string where, IDbConnection conn, IDbTransaction trans)
        {
            throw new NotImplementedException();
        }
    }
}
