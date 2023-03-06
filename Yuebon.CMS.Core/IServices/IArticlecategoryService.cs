using System;
using Yuebon.Commons.IServices;
using Yuebon.CMS.Dtos;
using Yuebon.CMS.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Yuebon.Commons.Models;
using Yuebon.Commons.Core.Dtos;
using System.Data;

namespace Yuebon.CMS.IServices
{
    /// <summary>
    /// 定義文章分類服務接口
    /// </summary>
    public interface IArticlecategoryService:IService<Articlecategory,ArticlecategoryOutputDto, string>
    {

        /// <summary>
        /// 獲取章分類適用于Vue 樹形列表，關鍵詞為空時獲取所有
        /// <param name="keyword">名稱關鍵詞</param>
        /// </summary>
        /// <returns></returns>
        Task<List<ArticlecategoryOutputDto>> GetAllArticlecategoryTreeTable(string keyword);


        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="ids">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        CommonResult DeleteBatchWhere(DeletesInputDto ids, IDbTransaction trans = null);
        /// <summary>
        /// 異步按條件批量刪除
        /// </summary>
        /// <param name="ids">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<CommonResult> DeleteBatchWhereAsync(DeletesInputDto ids, IDbTransaction trans = null);
    }
}
