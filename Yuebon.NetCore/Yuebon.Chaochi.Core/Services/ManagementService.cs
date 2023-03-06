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
    /// 管理方服務接口實現
    /// </summary>
    public class ManagementService: BaseService<Management,ManagementOutputDto, string>, IManagementService
    {
		private readonly IManagementRepository _repository;
        public ManagementService(IManagementRepository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據統一編號查詢管理方
        /// </summary>
        /// <param name="maid">統一編號</param>
        /// <returns></returns>
        public async Task<ManagementOutputDto> GetById(string maid)
        {            
            Management management = await _repository.GetWhereAsync(string.Format(" MAID = '{0}'", maid));
            ManagementOutputDto outputDto = management.MapTo<ManagementOutputDto>();

            return outputDto;
        }
    }
}