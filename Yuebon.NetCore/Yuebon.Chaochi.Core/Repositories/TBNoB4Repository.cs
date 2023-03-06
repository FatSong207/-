
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class TBNoB4Repository : BaseRepository<TBNoB4, string>, ITBNoB4Repository
    {
        /// <summary>
        /// 
        /// </summary>
        public TBNoB4Repository()
        {
        }

        public TBNoB4Repository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="BID"></param>
        /// <returns></returns>
        public async Task<TBNoB4> GetByBID(string BID)
        {
            string sql = @"SELECT * FROM Chaochi_TBNoB4 t WHERE t.BID = @BID";
            return await DapperConn.QueryFirstOrDefaultAsync<TBNoB4>(sql, new { BID = BID });
        }

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        public async Task<TBNoB4> GetByBAdd(string BAdd)
        {
            string sql = @"SELECT * FROM Chaochi_TBNoB4 t WHERE t.BAdd = @BAdd";
            return await DapperConn.QueryFirstOrDefaultAsync<TBNoB4>(sql, new { BAdd = BAdd });
        }

    }
}