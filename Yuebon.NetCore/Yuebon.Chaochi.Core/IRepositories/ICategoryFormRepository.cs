
using Yuebon.Chaochi.Core.Models;
using Yuebon.Commons.IRepositories;

namespace Yuebon.Chaochi.Core.IRepositories
{
    /// <summary>
    /// 表單分類倉儲介面
    /// </summary>
    public interface ICategoryFormRepository : IRepository<CategoryForm, string>
    {
        /// <summary>
        /// 獲取根節點分類
        /// </summary>
        /// <param name="id">分類Id</param>
        /// <returns></returns>
        CategoryForm GetRootCategory(string id);
    }
}
