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
    /// 代管合約BZ服務接口實現
    /// </summary>
    public class TBNoC3Service: BaseService<TBNoC3,TBNoC3OutputDto, string>, ITBNoC3Service
    {
		private readonly ITBNoC3Repository _repository;
        public TBNoC3Service(ITBNoC3Repository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據合約編號查詢相關資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<TBNoC3> GetByCID(string cid)
        {
            TBNoC3 info = await _repository.GetWhereAsync(string.Format(" CID = '{0}'", cid));

            return info;
        }
    }
}