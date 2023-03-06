using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Yuebon.Commons.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.IRepositories
{
    public interface IUploadFileRepository : IRepository<UploadFile, string>
    {


        /// <summary>
        /// 根據應用Id和應用標識批量更新數據
        /// </summary>
        /// <param name="beLongAppId">應用Id</param>
        /// <param name="oldBeLongAppId">更新前舊的應用Id</param>
        /// <param name="belongApp">應用標識</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        bool UpdateByBeLongAppId(string beLongAppId, string oldBeLongAppId, string belongApp = null, IDbTransaction trans = null);
    }
}
