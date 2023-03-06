using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.Repositories
{
    /// <summary>
    /// 系統類型，也是子系統
    /// </summary>
    public class SystemTypeRepository : BaseRepository<SystemType, string>, ISystemTypeRepository
    {
        public SystemTypeRepository()
        {
        }

        public SystemTypeRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 根據系統編碼查詢系統對象
        /// </summary>
        /// <param name="appkey">系統編碼</param>
        /// <returns></returns>
        public SystemType GetByCode(string appkey)
        {
            string sql = @"SELECT * FROM " + this.tableName + " t WHERE t.EnCode = @EnCode";
            return DapperConn.QueryFirstOrDefault<SystemType>(sql, new { EnCode = appkey });
        }
    }
}
