using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義合約-承租人倉儲接口
    /// </summary>
    public interface IContractRenterRepository:IRepository<ContractRenter, string>
    {
    }
}