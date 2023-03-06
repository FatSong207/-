using System;
using System.Collections.Generic;
using Yuebon.Commons.IRepositories;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IRepositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAPPRepository:IRepository<APP,string>
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
    }
}