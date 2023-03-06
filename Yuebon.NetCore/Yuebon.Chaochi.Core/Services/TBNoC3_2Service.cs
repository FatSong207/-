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
    public class TBNoC3_2Service: BaseService<TBNoC3_2,TBNoC3_2OutputDto, string>, ITBNoC3_2Service
    {
		private readonly ITBNoC3_2Repository _repository;
        public TBNoC3_2Service(ITBNoC3_2Repository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據合約編號查詢相關資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<TBNoC3_2> GetByCID(string cid)
        {
            TBNoC3_2 info = await _repository.GetWhereAsync(string.Format(" CID = '{0}'", cid));

            return info;
        }
    }
}