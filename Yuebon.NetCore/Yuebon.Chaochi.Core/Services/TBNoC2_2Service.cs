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
    public class TBNoC2_2Service: BaseService<TBNoC2_2,TBNoC2_2OutputDto, string>, ITBNoC2_2Service
    {
		private readonly ITBNoC2_2Repository _repository;
        public TBNoC2_2Service(ITBNoC2_2Repository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據合約編號查詢相關資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<TBNoC2_2> GetByCID(string cid)
        {
            TBNoC2_2 info = await _repository.GetWhereAsync(string.Format(" CID = '{0}'", cid));

            return info;
        }
    }
}