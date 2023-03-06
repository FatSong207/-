using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Chaochi.Core.IRepositories;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Commons.Enums;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;

namespace Yuebon.Chaochi.Core.Repositories
{
    public class CategoryContractRepository : BaseRepository<CategoryContract, string>, ICategoryContractRepository
    {
        public CategoryContractRepository()
        {
        }

        public CategoryContractRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 獲取根節點分類
        /// </summary>
        /// <param name="id">分類Id</param>
        /// <returns></returns>
        public CategoryContract GetRootCategory(string id)
        {
            var sb = new StringBuilder(";WITH ");
            if (dbConnectionOptions.DatabaseType == DatabaseType.MySql) {
                sb.Append(" Recursive ");
            }
            sb.Append(" T AS (");
            sb.Append(" SELECT Id, ParentId, Name, Layers FROM Chaochi_CategoryContract");
            sb.AppendFormat(" WHERE Id = '{0}'", id);
            sb.Append(" UNION ALL ");
            sb.Append(" SELECT A.Id, A.ParentId, A.Name, A.Layers FROM Chaochi_CategoryContract AS A JOIN T AS B ON A.Id = B.ParentId ) SELECT * FROM T ORDER BY Layers");
            return DapperConn.QueryFirstOrDefault<CategoryContract>(sb.ToString());
        }

        /// <summary>
        /// 獲取父節點分類
        /// </summary>
        /// <param name="parentId">父節點分類Id</param>
        /// <returns></returns>
        public async Task<List<CategoryContract>> GetCategoryByParent(string parentId)
        {
            string sql = @"SELECT * FROM Chaochi_CategoryContract t WHERE t.ParentId = @ParentId";
            return (await DapperConn.QueryAsync<CategoryContract>(sql, new { @ParentId = parentId })).ToList();
        }
    }
}
