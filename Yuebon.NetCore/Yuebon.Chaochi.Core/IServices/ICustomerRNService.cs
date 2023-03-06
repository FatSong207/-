using System.Data;
using System.Threading.Tasks;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;

namespace Yuebon.Chaochi.Core.IServices
{
    public interface ICustomerRNService : IService<CustomerRN, CustomerRNOutputDto, string>
    {
        /// <summary>
        /// 新增存取權
        /// </summary>
        /// <param name="customerRN"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> InsertAccess(CustomerRN customerRN, string userId, string userDeptId);

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="RNID"></param>
        /// <returns></returns>
        Task<CustomerRN> GetCustomerByRNID(string RNID);

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        Task<PageResult<CustomerRNOutputDto>> FindWithPagerSearchAsync(SearchCustomerRNModel search);

        /// <summary>
        /// 非同步按條件批次刪除
        /// </summary>
        /// <param name="ids">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<CommonResult> DeleteBatchWhereAsync(DeletesInputDto ids, IDbTransaction trans = null);

    }
}
