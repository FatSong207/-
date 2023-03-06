using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義二房東倉儲接口
    /// </summary>
    public interface ISecondLandlordRepository:IRepository<SecondLandlord, string>
    {
        ///// <summary>
        ///// 根據統一編號查詢二房東
        ///// </summary>
        ///// <param name="slid">統一編號</param>
        ///// <returns></returns>
        //Task<SecondLandlord> GetById(string slid);
    }
}