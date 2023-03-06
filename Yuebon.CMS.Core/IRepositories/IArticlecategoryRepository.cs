using System;
using Yuebon.Commons.IRepositories;
using Yuebon.CMS.Models;

namespace Yuebon.CMS.IRepositories
{
    /// <summary>
    /// 定義文章分類倉儲接口
    /// </summary>
    public interface IArticlecategoryRepository:IRepository<Articlecategory, string>
    {
    }
}