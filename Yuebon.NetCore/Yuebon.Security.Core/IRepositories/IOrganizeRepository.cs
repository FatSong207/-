using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.IRepositories
{
    /// <summary>
    /// 組織倉儲接口
    /// 這里用到的Organize業務對象，是領域對象
    /// </summary>
    public interface IOrganizeRepository:IRepository<Organize, string>
    {
        /// <summary>
        /// 獲取根節點組織
        /// </summary>
        /// <param name="id">組織Id</param>
        /// <returns></returns>
        Organize GetRootOrganize(string id);
    }
}