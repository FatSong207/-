using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 轉租合約BZ服務接口實現
    /// </summary>
    public class TBNoC2Service: BaseService<TBNoC2,TBNoC2OutputDto, string>, ITBNoC2Service
    {
		private readonly ITBNoC2Repository _repository;
        public TBNoC2Service(ITBNoC2Repository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據合約編號查詢相關資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<TBNoC2> GetByCID(string cid)
        {
            TBNoC2 info = await _repository.GetWhereAsync(string.Format(" CID = '{0}'", cid));

            return info;
        }
    }
}