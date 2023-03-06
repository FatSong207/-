using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using Dapper;

namespace Yuebon.Chaochi.Repositories
{
    /// <summary>
    /// 倉儲接口的實現
    /// </summary>
    public class TBNoC1Repository : BaseRepository<TBNoC1, string>, ITBNoC1Repository
    {
		public TBNoC1Repository()
        {
        }

        public TBNoC1Repository(IDbContextCore context) : base(context)
        {

        }

        #region Dapper 操作

        //DapperConn 用于讀寫操作
        //DapperConnRead 用于只讀操作

        /// <summary>
        /// 根據合約編號查詢包租合約BZ欄位值
        /// 
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>    
        public async Task<TBNoC1> GetByCID(string cid)
        {
            string sql = @"SELECT * FROM Chaochi_TBNoC1 t WHERE t.CID = @CID";
            return await DapperConn.QueryFirstOrDefaultAsync<TBNoC1>(sql, new { CID = cid  });
        }

        #endregion


        #region EF 操作

        //DbContext 用于讀寫操作
        //DbContextRead 用于只讀操作

        #endregion
    }
}