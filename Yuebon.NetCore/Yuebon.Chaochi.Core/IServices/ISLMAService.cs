using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface ISLMAService:IService<SLMA,SLMAOutputDto, string>
    {
        new
        /// <summary>
        /// 明確查詢
        /// </summary>
        /// <param name="fieldName">欄位</param>
        /// <param name="where">條件</param>
        /// <returns></returns>
        Task<IEnumerable<SLMA>> GetDistinctByFieldAsync(string fieldName, string where);

        /// <summary>
        /// 根據統一編號和營業地址取得二房東/管理方資訊
        /// </summary>
        /// <param name="slmaid">統一編號</param>
        /// <param name="address">營業地址</param>
        /// <returns></returns>
        Task<SLMAOutputDto> GetBySLMAInfo(string slmaid, string address);

        /// <summary>
        /// 根據證書字號回傳管理人資訊
        /// </summary>
        /// <param name="silrno">證書字號</param>
        /// <returns></returns>
        Task<SLMAOutputDto> GetBySILRNo(string silrno);

        /// <summary>
        /// 根據證書字號回傳經紀人資訊
        /// </summary>
        /// <param name="aglrno">證書字號</param>
        /// <returns></returns>
        Task<SLMAOutputDto> GetByAGLRNo(string aglrno);
    }
}
