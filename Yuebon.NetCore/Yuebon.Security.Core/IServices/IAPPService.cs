using System;
using System.Collections.Generic;
using System.Data;
using Yuebon.Commons.IServices;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAPPService : IService<APP, AppOutputDto, string>
    {
        /// <summary>
        /// 獲取app對象
        /// </summary>
        /// <param name="appid">應用ID</param>
        /// <param name="secret">應用密鑰AppSecret</param>
        /// <returns></returns>
        APP GetAPP(string appid, string secret);

        /// <summary>
        /// 獲取app對象
        /// </summary>
        /// <param name="appid">應用ID</param>
        /// <returns></returns>
        APP GetAPP(string appid);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<AppOutputDto> SelectApp();
        /// <summary>
        /// 更新可用的應用到緩存
        /// </summary>
        void UpdateCacheAllowApp(IDbConnection conn = null, IDbTransaction trans = null);
    }
}
