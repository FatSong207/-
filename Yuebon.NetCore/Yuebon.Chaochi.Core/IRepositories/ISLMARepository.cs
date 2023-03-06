using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義倉儲接口
    /// </summary>
    public interface ISLMARepository:IRepository<SLMA, string>
    {
        new Task<IEnumerable<SLMA>> GetDistinctByFieldAsync(string fieldName, string where);
    }
}