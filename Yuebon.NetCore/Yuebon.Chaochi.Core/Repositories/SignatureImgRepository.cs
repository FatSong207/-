using Dapper;
using System.Threading.Tasks;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;

namespace Yuebon.Chaochi.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class SignatureImgRepository : BaseRepository<SignatureImg, string>, ISignatureImgRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public SignatureImgRepository()
        {
        }

        public SignatureImgRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        public async Task<SignatureImg> FindByFormIdBAdd(string formId, string bAdd)
        {
            string sql = @"SELECT * FROM SignatureImg  WHERE BAdd = @BAdd AND FormId = @FormId";
            return await DapperConn.QueryFirstOrDefaultAsync<SignatureImg>(sql, new
            {
                BAdd = bAdd,
                FormId= formId
            });

        }
    }
}
