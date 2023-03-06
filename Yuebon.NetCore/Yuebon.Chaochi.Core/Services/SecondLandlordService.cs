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
    /// 二房東服務接口實現
    /// </summary>
    public class SecondLandlordService: BaseService<SecondLandlord,SecondLandlordOutputDto, string>, ISecondLandlordService
    {
		private readonly ISecondLandlordRepository _repository;
        public SecondLandlordService(ISecondLandlordRepository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據統一編號查詢二房東
        /// </summary>
        /// <param name="slid">統一編號</param>
        /// <returns></returns>
        public async Task<SecondLandlordOutputDto> GetById(string slid)
        {
            SecondLandlord secondLandlord = await _repository.GetWhereAsync(string.Format(" SLID = '{0}'", slid));
            SecondLandlordOutputDto outputDto = secondLandlord.MapTo<SecondLandlordOutputDto>();

            return outputDto;
        }
        
        /// <summary>
        /// 根據統一編號回傳整個實體
        /// </summary>
        /// <param name="slid"></param>
        /// <returns></returns>
        public async Task<SecondLandlord> GetBySLID(string slid)
        {
            return await _repository.GetWhereAsync($"SLID='{slid}'");
        }

        /// <summary>
        /// 根據二房東名稱回傳整個實體
        /// </summary>
        /// <param name="slid"></param>
        /// <returns></returns>
        public async Task<SecondLandlord> GetBySLName(string slName)
        {
            return await _repository.GetWhereAsync($"SLName='{slName}'");
        }
    }
}