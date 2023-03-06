using System.Threading.Tasks;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.Repositories
{
    public class ItemsRepository : BaseRepository<Items, string>, IItemsRepository
    {
        public ItemsRepository()
        {
        }

        public ItemsRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 根據編碼查詢字典分類
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public async Task<Items> GetByEnCodAsynce(string enCode)
        {
            return await DbContext.GetSingleOrDefaultAsync<Items>(u => u.EnCode == enCode);
        }


        /// <summary>
        /// 更新時判斷分類編碼是否存在（排除自己）
        /// </summary>
        /// <param name="enCode">分類編碼</param
        /// <param name="id">主鍵Id</param>
        /// <returns></returns>
        public async Task<Items> GetByEnCodAsynce(string enCode,string id)
        {
            return await DbContext.GetSingleOrDefaultAsync<Items>(u => u.EnCode == enCode&&u.Id!=id);
        }
    }
}