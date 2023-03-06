using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using Yuebon.Commons.Pages;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義建物廣告管理倉儲接口
    /// </summary>
    public interface IBuildingAdvertisementRepository:IRepository<BuildingAdvertisement, string>
    {
        /// <summary>
        /// 根據建物地址查詢建物廣告資訊
        /// </summary>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        Task<BuildingAdvertisement> GetByBAdd(string BAdd);

        /// <summary>
        /// 分頁查詢包含建物資訊
        /// </summary>
        /// <param name="condition">查詢條件字段需要加表別名</param>
        /// <param name="info">分頁信息</param>
        /// <param name="fieldToSort">排序字段，也需要加表別名</param>
        /// <param name="desc">排序方式</param>
        /// <param name="trans">事務</param>
        /// <returns></returns>
        Task<List<object>> FindWithPagerRelationBuildingAsync(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 新增建物廣告
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        Task<bool> InsertAsync(BuildingAdvertisement entity, IDbTransaction trans = null);
    }
}