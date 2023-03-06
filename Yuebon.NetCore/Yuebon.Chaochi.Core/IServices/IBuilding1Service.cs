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

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 用戶服務接口
    /// </summary>
    public interface IBuilding1Service : IService<Building1, Building1OutputDto, string>
    {
        /// <summary>
        /// 新增實體
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Add(Building1 entity);

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        Task<Building1> GetByBAdd(string BAdd);

        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        bool Insert(Building1 entity, Yuebon.Security.Models.UserLogOn userLogOnEntity, IDbTransaction trans = null);
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
        /// <param name="userOpenIds"></param>
        /// <param name="trans"></param>
        bool Insert(Building1 entity, Yuebon.Security.Models.UserLogOn userLogOnEntity, Yuebon.Security.Models.UserOpenIds userOpenIds,IDbTransaction trans = null);

        string GetIdByBAdd(string BAdd);
    }
}
