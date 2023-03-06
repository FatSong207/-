using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義合約-出租人倉儲接口
    /// </summary>
    public interface IContractLandlordRepository:IRepository<ContractLandlord, string>
    {
    }
}