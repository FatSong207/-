using System.Threading.Tasks;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Data;

namespace Yuebon.Chaochi.Core.IRepositories
{
    public interface ICustomerRNRepository : IRepository<CustomerRN, string>
    {
        /// <summary>
        /// 根據身分證號/居留證號查詢房客資料
        /// </summary>
        /// <param name="RNID">身分證號/居留證號</param>
        /// <returns></returns>
        Task<CustomerRN> GetCustomerByRNID(string RNID);

        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        //bool Insert(CustomerRN entity, Security.Models.UserLogOn userLogOnEntity, IDbTransaction trans = null);

        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        //Task<bool> InsertAsync(CustomerRN entity, Security.Models.UserLogOn userLogOnEntity, IDbTransaction trans = null);

        /// <summary>
        /// 註冊用戶,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        //bool Insert(CustomerRN entity, Security.Models.UserLogOn userLogOnEntity, Security.Models.UserOpenIds userOpenIds, IDbTransaction trans = null);

    }
}
