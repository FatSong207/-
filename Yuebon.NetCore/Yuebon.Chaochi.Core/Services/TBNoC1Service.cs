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
    /// 包租合約BZ服務接口實現
    /// </summary>
    public class TBNoC1Service: BaseService<TBNoC1,TBNoC1OutputDto, string>, ITBNoC1Service
    {
		private readonly ITBNoC1Repository _repository;
        public TBNoC1Service(ITBNoC1Repository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據合約編號查詢相關資料
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<TBNoC1> GetByCID(string cid)
        {
            TBNoC1 info = await _repository.GetWhereAsync(string.Format(" CID = '{0}'", cid));

            return info;
        }
    }
}