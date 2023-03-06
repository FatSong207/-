using System;
using Yuebon.Commons.Repositories;
using Yuebon.CMS.IRepositories;
using Yuebon.CMS.Models;
using Yuebon.Commons.IDbContext;

namespace Yuebon.CMS.Repositories
{
    /// <summary>
    /// 文章分類倉儲接口的實現
    /// </summary>
    public class ArticlecategoryRepository : BaseRepository<Articlecategory, string>, IArticlecategoryRepository
    {
		public ArticlecategoryRepository()
        {
        }

        public ArticlecategoryRepository(IDbContextCore context) : base(context)
        {
        }
    }
}