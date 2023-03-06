using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義合約-建物倉儲接口
    /// </summary>
    public interface IContractBuildingRepository:IRepository<ContractBuilding, string>
    {
    }
}