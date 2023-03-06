using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class TBNoC2_3Service: BaseService<TBNoC2_3,TBNoC2_3OutputDto, string>, ITBNoC2_3Service
    {
		private readonly ITBNoC2_3Repository _repository;
        public TBNoC2_3Service(ITBNoC2_3Repository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據合約編號查詢相關資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<TBNoC2_3> GetByCID(string cid)
        {
            TBNoC2_3 info = await _repository.GetWhereAsync(string.Format(" CID = '{0}'", cid));

            return info;
        }
    }
}