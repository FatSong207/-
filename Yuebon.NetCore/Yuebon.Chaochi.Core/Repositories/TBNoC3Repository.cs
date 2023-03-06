using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.Models;
using Dapper;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.Repositories
{
    /// <summary>
    /// 倉儲接口的實現
    /// </summary>
    public class TBNoC3Repository : BaseRepository<TBNoC3, string>, ITBNoC3Repository
    {
		public TBNoC3Repository()
        {
        }

        public TBNoC3Repository(IDbContextCore context) : base(context)
        {

        }

        #region Dapper 操作

        //DapperConn 用于讀寫操作
        //DapperConnRead 用于只讀操作

        /// <summary>
        /// 根據合約編號查詢代管合約BZ欄位值
        /// 
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<TBNoC3> GetByCID(string cid)
        {
            string sql = @"SELECT * FROM Chaochi_TBNoC3 t WHERE t.CID = @CID";
            return await DapperConn.QueryFirstOrDefaultAsync<TBNoC3>(sql, new { CID = cid });
        }

        #endregion


        #region EF 操作

        //DbContext 用于讀寫操作
        //DbContextRead 用于只讀操作

        #endregion
    }
}