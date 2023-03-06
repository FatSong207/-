using System;
using System.Threading.Tasks;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Models;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 定義單據編碼服務接口
    /// </summary>
    public interface ISequenceService : IService<Sequence, SequenceOutputDto, string>
    {
        /// <summary>
        /// 獲取最新業務單據編碼
        /// </summary>
        /// <param name="sequenceName">業務單據編碼名稱</param>
        /// <returns></returns>
        Task<CommonResult> GetSequenceNextTask(string sequenceName);
        /// <summary>
        /// 獲取最新業務單據編碼
        /// </summary>
        /// <param name="sequenceName">業務單據編碼名稱</param>
        /// <returns></returns>
        CommonResult GetSequenceNext(string sequenceName);
    }
}
