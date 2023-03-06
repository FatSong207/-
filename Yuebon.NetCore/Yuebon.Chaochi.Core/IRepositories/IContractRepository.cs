using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.Pages;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義合約版本倉儲接口
    /// </summary>
    public interface IContractRepository:IRepository<Contract, string>
    {
        /// <summary>
        /// 分頁查詢包含出租中,承租人,建物資訊
        /// </summary>
        /// <param name="condition">查詢條件字段需要加表別名</param>
        /// <param name="info">分頁信息</param>
        /// <param name="fieldToSort">排序字段，也需要加表別名</param>
        /// <param name="desc">排序方式</param>
        /// <param name="trans">事務</param>
        /// <returns></returns>
        Task<List<object>> FindWithPagerRelationAsync(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null);
    }
}