using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義合約匯款帳號維護倉儲接口
    /// </summary>
    public interface IContractRemittanceRepository:IRepository<ContractRemittance, string>
    {
    }
}