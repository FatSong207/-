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
    /// 合約相關資料服務接口實現
    /// </summary>
    public class ContractRelevantService: BaseService<ContractRelevant,ContractRelevantOutputDto, string>, IContractRelevantService
    {
		private readonly IContractRelevantRepository _repository;
        public ContractRelevantService(IContractRelevantRepository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據合約編號查詢相關資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<ContractRelevantOutputDto> GetByCID(string cid)
        {
            ContractRelevantOutputDto outputDto = new ContractRelevantOutputDto();
            ContractRelevant info = await _repository.GetWhereAsync(string.Format(" CID = '{0}'", cid));
            if (info != null) {
                outputDto = info.MapTo<ContractRelevantOutputDto>();
            }

            return outputDto;
        }
    }
}