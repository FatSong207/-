using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Mapping;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class SLMAService: BaseService<SLMA,SLMAOutputDto, string>, ISLMAService
    {
		private readonly ISLMARepository _repository;
        public SLMAService(ISLMARepository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 明確查詢
        /// </summary>
        /// <param name="fieldName">欄位</param>
        /// <param name="where">條件</param>
        /// <returns></returns>
        public new async Task<IEnumerable<SLMA>> GetDistinctByFieldAsync(string fieldName, string where)
        {
            return await _repository.GetDistinctByFieldAsync(fieldName, where);
        }

        /// <summary>
        /// 根據統一編號和營業地址取得二房東/管理方資訊
        /// </summary>
        /// <param name="slmaid">統一編號</param>
        /// <param name="address">營業地址</param>
        /// <returns></returns>
        public async Task<SLMAOutputDto> GetBySLMAInfo(string slmaid, string address)
        {
            SLMA slma = await _repository.GetWhereAsync($"SLMAID = '{slmaid}' AND Address = '{address}'");
            SLMAOutputDto outputDto = slma.MapTo<SLMAOutputDto>();

            return outputDto;
        }

        /// <summary>
        /// 根據證書字號回傳管理人資訊
        /// </summary>
        /// <param name="silrno">證書字號</param>
        /// <returns></returns>
        public async Task<SLMAOutputDto> GetBySILRNo(string silrno)
        {
            SLMA slma = await _repository.GetWhereAsync($"SILRNo='{silrno}'");
            SLMAOutputDto outputDto = slma.MapTo<SLMAOutputDto>();

            return outputDto;
        }

        /// <summary>
        /// 根據證書字號回傳經紀人資訊
        /// </summary>
        /// <param name="aglrno">證書字號</param>
        /// <returns></returns>
        public async Task<SLMAOutputDto> GetByAGLRNo(string aglrno)
        {
            SLMA slma = await _repository.GetWhereAsync($"AGLRNo='{aglrno}'");
            SLMAOutputDto outputDto = slma.MapTo<SLMAOutputDto>();

            return outputDto;
        }
    }
}