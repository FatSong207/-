using System;
using Yuebon.Commons.IServices;
using Yuebon.CMS.Dtos;
using Yuebon.CMS.Models;
using System.Threading.Tasks;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;
using System.Collections.Generic;

namespace Yuebon.CMS.IServices
{
    /// <summary>
    /// 定義文章服務接口
    /// </summary>
    public interface IArticlenewsService:IService<Articlenews,ArticlenewsOutputDto, string>
    {
        /// <summary>
        /// 根據用戶角色獲取分類及該分類的文章
        /// </summary>
        /// <returns></returns>
        Task<List<CategoryArticleOutputDto>> GetCategoryArticleList();
    }
}
