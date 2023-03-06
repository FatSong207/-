using System.Threading.Tasks;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Pages;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Data;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 用戶服務接口
    /// </summary>
    public interface ICustomerLNService : IService<CustomerLN, CustomerLNOutputDto, string>
    {
        /// <summary>
        /// 新增存取權
        /// </summary>
        /// <param name="customerLN"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> InsertAccess(CustomerLN customerLN, string userId,string userDeptId);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="customerLN"></param>
        void Insert(CustomerLN customerLN,string currentUserId);

        /// <summary>
        /// 根據出租人地址查詢姓名
        /// </summary>
        /// <param name="addr">建物地址</param>
        /// <returns></returns>
        Task<CustomerLN> GetNameByAddr(string addr);

        /// <summary>
        /// 根據房東自然人身份證字號查詢
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<CustomerLN> GetByCustomerLNID(string LNID, IDbConnection conn = null, IDbTransaction tran = null);

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        Task<PageResult<CustomerLNOutputDto>> FindWithPagerSearchAsync(SearchCustomerLNModel search);
    }
}
