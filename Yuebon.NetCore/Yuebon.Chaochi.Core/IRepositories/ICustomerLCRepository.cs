using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomerLCRepository : IRepository<CustomerLC, string>
    {
        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="LCID"></param>
        /// <returns></returns>
        Task<CustomerLC> GetByLCID(string LCID, IDbConnection conn, IDbTransaction tran);

        /// <summary>
        /// 所有用戶信息用于關注
        /// </summary>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        IEnumerable<UserAllListFocusOutPutDto> GetUserAllListFocusByPage(string currentpage,
            string pagesize, string userid);
    }
}