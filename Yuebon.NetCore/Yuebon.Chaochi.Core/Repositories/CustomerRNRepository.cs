using Dapper;
using System.Threading.Tasks;
using Yuebon.Chaochi.Core.IRepositories;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;

namespace Yuebon.Chaochi.Core.Repositories
{
    public class CustomerRNRepository : BaseRepository<CustomerRN, string>, ICustomerRNRepository
    {
        public CustomerRNRepository()
        {
        }

        public CustomerRNRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 根據身分證號/居留證號查詢房客資料
        /// </summary>
        /// <param name="RNID">身分證號/居留證號</param>
        /// <returns></returns>
        public async Task<CustomerRN> GetCustomerByRNID(string RNID)
        {
            string sql = @"SELECT * FROM Chaochi_CustomerRN t WHERE t.RNID = @RNID";
            return await DapperConn.QueryFirstOrDefaultAsync<CustomerRN>(sql, new { @RNID = RNID });
        }
    }
}
