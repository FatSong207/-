using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義管理方倉儲接口
    /// </summary>
    public interface IManagementRepository : IRepository<Management, string>
    {
        ///// <summary>
        ///// 根據統一編號查詢管理方
        ///// </summary>
        ///// <param name="maid">統一編號</param>
        ///// <returns></returns>
        //Task<Management> GetById(string maid);
    }
}