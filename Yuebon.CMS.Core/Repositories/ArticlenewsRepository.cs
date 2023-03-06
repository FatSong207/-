using System;

using Yuebon.Commons.Repositories;
using Yuebon.CMS.IRepositories;
using Yuebon.CMS.Models;
using Yuebon.Commons.IDbContext;

namespace Yuebon.CMS.Repositories
{
    /// <summary>
    /// 文章倉儲接口的實現
    /// </summary>
    public class ArticlenewsRepository : BaseRepository<Articlenews, string>, IArticlenewsRepository
    {
		public ArticlenewsRepository()
        {
        }

        public ArticlenewsRepository(IDbContextCore context) : base(context)
        {
        }
    }
}