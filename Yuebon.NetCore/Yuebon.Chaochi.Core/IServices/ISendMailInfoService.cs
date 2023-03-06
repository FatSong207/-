using System;
using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Data;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface ISendMailInfoService:IService<SendMailInfo,SendMailInfoOutputDto, string>
    {
        /// <summary>
        /// 異步新增實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<long> ComplaintSendMailInfoInsertAsync(SendMailInfo entity, IDbConnection conn = null, IDbTransaction trans = null);
    }
}
