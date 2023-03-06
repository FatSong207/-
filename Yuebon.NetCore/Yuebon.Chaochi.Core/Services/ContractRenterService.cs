using System.Threading.Tasks;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 合約-承租人服務接口實現
    /// </summary>
    public class ContractRenterService: BaseService<ContractRenter,ContractRenterOutputDto, string>, IContractRenterService
    {
		private readonly IContractRenterRepository _repository;
        public ContractRenterService(IContractRenterRepository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據合約編號查詢承租人資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<ContractRenter> GetByCID(string cid)
        {
            ContractRenter info = await _repository.GetWhereAsync(string.Format(" CID = '{0}'", cid));

            return info;
        }
    }
}