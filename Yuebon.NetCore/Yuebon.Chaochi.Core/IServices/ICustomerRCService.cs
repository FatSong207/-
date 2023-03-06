using System;
using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Pages;
using System.Threading.Tasks;
using Yuebon.Chaochi.Core.Dtos;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface ICustomerRCService:IService<CustomerRC,CustomerRCOutputDto, string>
    {
        /// <summary>
        /// 新增存取權
        /// </summary>
        /// <param name="customerRN"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> InsertAccess(CustomerRC customerRC, string userId, string userDeptId);
        Task<PageResult<CustomerRCOutputDto>> FindWithPagerSearchAsync(SearchCustomerRCModel search);
    }
}
