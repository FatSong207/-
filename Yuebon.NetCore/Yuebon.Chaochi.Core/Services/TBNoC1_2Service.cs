using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.Dtos;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class TBNoC1_2Service: BaseService<TBNoC1_2,TBNoC1_2OutputDto, string>, ITBNoC1_2Service
    {
		private readonly ITBNoC1_2Repository _repository;
        public TBNoC1_2Service(ITBNoC1_2Repository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據合約編號查詢相關資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<TBNoC1_2> GetByCID(string cid)
        {
            TBNoC1_2 info = await _repository.GetWhereAsync(string.Format(" CID = '{0}'", cid));

            return info;
        }
    }
}