using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using Yuebon.Commons.Mapping;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 合約-出租人服務接口實現
    /// </summary>
    public class ContractLandlordService: BaseService<ContractLandlord,ContractLandlordOutputDto, string>, IContractLandlordService
    {
		private readonly IContractLandlordRepository _repository;
        public ContractLandlordService(IContractLandlordRepository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據合約編號查詢出租人資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<ContractLandlord> GetByCID(string cid)
        {            
            ContractLandlord info = await _repository.GetWhereAsync(string.Format(" CID = '{0}'", cid));

            return info;
        }
    }
}