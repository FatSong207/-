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
    public interface IBuilding1Repository : IRepository<Building1, string>
    {
        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        Task<Building1> GetByBAdd(string BAdd);

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="BID"></param>
        /// <returns></returns>
        Task<Building1> GetByBID(string BID);

        bool Insert(Building1 entity, Security.Models.UserLogOn userLogOnEntity, IDbTransaction trans = null);
        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        Task<bool> InsertAsync(Building1 entity, IDbTransaction trans = null);
        /// <summary>
        /// 註冊用戶,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        bool Insert(Building1 entity, Security.Models.UserLogOn userLogOnEntity, Security.Models.UserOpenIds userOpenIds, IDbTransaction trans = null);

    }
}