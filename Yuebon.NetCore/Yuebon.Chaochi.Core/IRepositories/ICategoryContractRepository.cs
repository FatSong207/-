
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Commons.IRepositories;

namespace Yuebon.Chaochi.Core.IRepositories
{
    /// <summary>
    /// 契約分類倉儲介面
    /// </summary>
    public interface ICategoryContractRepository : IRepository<CategoryContract, string>
    {
        /// <summary>
        /// 獲取根節點分類
        /// </summary>
        /// <param name="id">分類Id</param>
        /// <returns></returns>
        CategoryContract GetRootCategory(string id);

        /// <summary>
        /// 獲取父節點分類
        /// </summary>
        /// <param name="parentId">父節點分類Id</param>
        /// <returns></returns>
        Task<List<CategoryContract>> GetCategoryByParent(string parentId);
    }
}
