using Dapper;
using System.Text;
using Yuebon.Chaochi.Core.IRepositories;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Commons.Enums;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;

namespace Yuebon.Chaochi.Core.Repositories
{
    public class CategoryFormRepository : BaseRepository<CategoryForm, string>, ICategoryFormRepository
    {
        public CategoryFormRepository()
        {
        }

        public CategoryFormRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 獲取根節點分類
        /// </summary>
        /// <param name="id">分類Id</param>
        /// <returns></returns>
        public CategoryForm GetRootCategory(string id)
        {
            var sb = new StringBuilder(";WITH ");
            if (dbConnectionOptions.DatabaseType == DatabaseType.MySql) {
                sb.Append(" Recursive ");
            }
            sb.Append(" T AS (");
            sb.Append(" SELECT Id, ParentId, Name, Layers FROM Chaochi_CategoryForm");
            sb.AppendFormat(" WHERE Id = '{0}'", id);
            sb.Append(" UNION ALL ");
            sb.Append(" SELECT A.Id, A.ParentId, A.Name, A.Layers FROM Chaochi_CategoryForm AS A JOIN T AS B ON A.Id = B.ParentId ) SELECT* FROM T ORDER BY Layers");
            return DapperConn.QueryFirstOrDefault<CategoryForm>(sb.ToString());
        }
    }
}
