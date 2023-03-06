using System;
using Yuebon.Commons.IRepositories;
using Yuebon.CMS.Models;

namespace Yuebon.CMS.IRepositories
{
    /// <summary>
    /// 定義文章倉儲接口
    /// </summary>
    public interface IArticlenewsRepository:IRepository<Articlenews, string>
    {
    }
}