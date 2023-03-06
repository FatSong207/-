using Dapper;
using System;
using System.Data;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.Repositories
{
    public class UploadFileRepository : BaseRepository<UploadFile, string>, IUploadFileRepository
    {
        public UploadFileRepository()
        {
        }

        public UploadFileRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 根據應用Id和應用標識批量更新數據
        /// </summary>
        /// <param name="beLongAppId">更新后的應用Id</param>
        /// <param name="oldBeLongAppId">更新前舊的應用Id</param>
        /// <param name="belongApp">應用標識</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool UpdateByBeLongAppId(string beLongAppId, string oldBeLongAppId, string belongApp = null, IDbTransaction trans = null)
        {
            try
            {
                trans = DapperConn.BeginTransaction();
                string sqlStr = string.Format("update {0} set beLongAppId='{1}' where beLongAppId='{2}'", this.tableName, beLongAppId, oldBeLongAppId);
                if (!string.IsNullOrEmpty(belongApp))
                {
                    sqlStr = string.Format(" and BelongApp='{0}'", belongApp);
                }
                int num = DapperConn.Execute(sqlStr, null, trans);
                trans.Commit();
                return num >= 0;
            }
            catch (Exception)
            {
                trans.Rollback();
                throw;
            }
        }
    }
}
