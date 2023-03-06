
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;

namespace Yuebon.Chaochi.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class TBNoA1Repository : BaseRepository<TBNoA1, string>, ITBNoA1Repository
    {
        /// <summary>
        /// 
        /// </summary>
        public TBNoA1Repository()
        {
        }

        public TBNoA1Repository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="BID"></param>
        /// <returns></returns>
        public async Task<TBNoA1> GetByBID(string BID)
        {
            string sql = @"SELECT * FROM Chaochi_TBNoA1  WHERE BID = @BID";
            return await DapperConn.QueryFirstOrDefaultAsync<TBNoA1>(sql, new { BID = BID });
        }

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        public async Task<TBNoA1> GetByBAdd(string formId, string BAdd)
        {
            string sql = @"SELECT * FROM Chaochi_TBNoA1 WHERE BAdd = @BAdd AND FName = @FormId ";
            return await DapperConn.QueryFirstOrDefaultAsync<TBNoA1>(sql, new { BAdd = BAdd, FormId = formId });
        }

        public async Task<List<TBNoA1>> GetListByBAdd(string BAdd)
        {
            string sql = @"SELECT * FROM Chaochi_TBNoA1 WHERE BAdd = @BAdd";
            return (await DapperConn.QueryAsync<TBNoA1>(sql, new { BAdd = BAdd })).ToList();
        }
    }
}