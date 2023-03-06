using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義合約歷史版本倉儲接口
    /// </summary>
    public interface IHistoryFormContractRepository:IRepository<HistoryFormContract, string>
    {
        ///// <summary>
        ///// 根據合約編號+合約版號查詢合約歷史
        ///// </summary>
        ///// <param name="cid">合約編號</param>
        ///// <param name="version">合約版號</param>
        ///// <returns></returns>
        //Task<HistoryFormContract> GetByVersion(string cid, int version);
    }
}