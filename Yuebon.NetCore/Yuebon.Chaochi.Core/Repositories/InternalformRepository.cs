
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Security.Models;

namespace Yuebon.Chaochi.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class InternalformRepository : BaseRepository<Internalform, string>, IInternalformRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public InternalformRepository()
        {
        }

        public InternalformRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns></returns>
        public async Task<Internalform> GetByFormId(string FormId)
        {
            string sql = @"SELECT * FROM Chaochi_Internalform t WHERE t.FormId = @FormId";
            return await DapperConn.QueryFirstOrDefaultAsync<Internalform>(sql, new { FormId = FormId });
        }


    }
}