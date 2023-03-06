
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
    public class TBNoB5Repository : BaseRepository<TBNoB5, string>, ITBNoB5Repository
    {
        /// <summary>
        /// 
        /// </summary>
        public TBNoB5Repository()
        {
        }

        public TBNoB5Repository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="BID"></param>
        /// <returns></returns>
        public async Task<TBNoB5> GetByBID(string BID)
        {
            string sql = @"SELECT * FROM Chaochi_TBNoB5 t WHERE t.BID = @BID";
            return await DapperConn.QueryFirstOrDefaultAsync<TBNoB5>(sql, new { BID = BID });
        }

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        public async Task<TBNoB5> GetByBAdd(string BAdd)
        {
            string sql = @"SELECT * FROM Chaochi_TBNoB5 t WHERE t.BAdd = @BAdd";
            return await DapperConn.QueryFirstOrDefaultAsync<TBNoB5>(sql, new { BAdd = BAdd });
        }

    }
}