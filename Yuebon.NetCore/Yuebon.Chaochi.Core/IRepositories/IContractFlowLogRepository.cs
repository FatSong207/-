using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義合約審核記錄倉儲接口
    /// </summary>
    public interface IContractFlowLogRepository:IRepository<ContractFlowLog, string>
    {
    }
}