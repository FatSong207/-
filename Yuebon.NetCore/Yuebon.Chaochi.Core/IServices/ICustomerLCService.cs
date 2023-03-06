using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Yuebon.Commons.Enums;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Pages;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.Core.Dtos;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 用戶服務接口
    /// </summary>
    public interface ICustomerLCService : IService<CustomerLC, CustomerLCOutputDto, string>
    {
        /// <summary>
        /// 新增存取權
        /// </summary>
        /// <param name="customerLC"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> InsertAccess(CustomerLC customerLC, string userId, string userDeptId);
        void Insert(CustomerLC customerLC, string currentUserId);

        /// <summary>
        /// 根據出租人地址查詢姓名
        /// </summary>
        /// <param name="addr">建物地址</param>
        /// <returns></returns>
        Task<CustomerLC> GetNameByAddr(string addr);

        /// <summary>
        /// 根據房東自然人身份證字號查詢
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<CustomerLC> GetByLCID(string LCID,IDbConnection conn=null,IDbTransaction tran=null);

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        Task<PageResult<CustomerLCOutputDto>> FindWithPagerSearchAsync(SearchCustomerLCModel search);
    }
}
