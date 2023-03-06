using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface IMOBuildingService:IService<MOBuilding,MOBuildingOutputDto, string>
    {
        /// <summary>
        /// 根據多物件單號查詢多物件資料
        /// </summary>
        /// <param name="moid">多物件單號</param>
        /// <returns></returns>
        Task<List<MOBuildingOutputDto>> GetByMOID(string moid);

        /// <summary>
        /// 根據多物件單號查詢物件資料
        /// </summary>
        /// <param name="moid">多物件單號</param>
        /// <param name="badd">物件地址</param>
        /// <returns></returns>
        Task<MOBuildingOutputDto> GetByMOBuilding(string moid, string badd, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 根據建物地址查詢分租物件資料
        /// </summary>
        /// <param name="BAdd">建物地址</param>
        /// <returns></returns>
        Task<MOBuildingOutputDto> GetByBAdd(string BAdd, IDbConnection conn = null, IDbTransaction trans = null);
    }
}
