using System;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.Models;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.Repositories
{
    /// <summary>
    /// 倉儲接口的實現
    /// </summary>
    public class SLMARepository : BaseRepository<SLMA, string>, ISLMARepository
    {
		public SLMARepository()
        {
        }

        public SLMARepository(IDbContextCore context) : base(context)
        {

        }

        #region Dapper 操作

        //DapperConn 用于讀寫操作
        //DapperConnRead 用于只讀操作

        public new async Task<IEnumerable<SLMA>> GetDistinctByFieldAsync(string fieldName, string where)
        {
            string sql = $"SELECT DISTINCT {fieldName} FROM Chaochi_SLMA WHERE {where}";
            return await DapperConnRead.QueryAsync<SLMA>(sql);
        }

        #endregion


        #region EF 操作

        //DbContext 用于讀寫操作
        //DbContextRead 用于只讀操作

        #endregion
    }
}