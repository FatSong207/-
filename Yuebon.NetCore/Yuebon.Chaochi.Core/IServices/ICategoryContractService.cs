using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Core.IServices
{
    /// <summary>
    /// 契約分類
    /// </summary>
    public interface ICategoryContractService : IService<CategoryContract, CategoryContractOutputDto, string>
    {
        /// <summary>
        /// 獲取契約分類適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        Task<List<CategoryContractOutputDto>> GetAllCategoryTreeTable();

        /// <summary>
        /// 獲取父節點分類
        /// </summary>
        /// <param name="parentId">父節點分類Id</param>
        /// <returns></returns>
        Task<List<CategoryContractOutputDto>> GetByParent(string parentId);

        /// <summary>
        /// 獲取根節點表單分類
        /// </summary>
        /// <param name="id">分類Id</param>
        /// <returns></returns>
        CategoryContract GetRootCategory(string id);

        /// <summary>
        /// 按條件批次刪除
        /// </summary>
        /// <param name="ids">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        CommonResult DeleteBatchWhere(DeletesInputDto ids, IDbTransaction trans = null);
        /// <summary>
        /// 非同步按條件批次刪除
        /// </summary>
        /// <param name="ids">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<CommonResult> DeleteBatchWhereAsync(DeletesInputDto ids, IDbTransaction trans = null);
    }
}
