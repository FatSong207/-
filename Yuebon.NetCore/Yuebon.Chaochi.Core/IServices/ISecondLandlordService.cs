using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義二房東服務接口
    /// </summary>
    public interface ISecondLandlordService:IService<SecondLandlord,SecondLandlordOutputDto, string>
    {
        /// <summary>
        /// 根據統一編號查詢二房東
        /// </summary>
        /// <param name="slid">統一編號</param>
        /// <returns></returns>
        Task<SecondLandlordOutputDto> GetById(string slid);

        /// <summary>
        /// 根據統一編號回傳整個實體
        /// </summary>
        /// <param name="slid"></param>
        /// <returns></returns>
        Task<SecondLandlord> GetBySLID(string slid);

        /// <summary>
        /// 根據二房東名稱回傳整個實體
        /// </summary>
        /// <param name="slid"></param>
        /// <returns></returns>
         Task<SecondLandlord> GetBySLName(string slName);
    }
}
