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
    /// 合約-建物服務接口實現
    /// </summary>
    public class ContractBuildingService: BaseService<ContractBuilding,ContractBuildingOutputDto, string>, IContractBuildingService
    {
		private readonly IContractBuildingRepository _repository;
        public ContractBuildingService(IContractBuildingRepository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據合約編號查詢建物資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<ContractBuildingOutputDto> GetByCID(string cid)
        {
            ContractBuilding info = await _repository.GetWhereAsync(string.Format(" CID = '{0}'", cid));
            ContractBuildingOutputDto result = info.MapTo<ContractBuildingOutputDto>();

            return result;
        }
    }
}