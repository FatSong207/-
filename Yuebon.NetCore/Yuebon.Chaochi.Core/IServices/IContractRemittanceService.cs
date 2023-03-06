using System;
using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義合約匯款帳號維護服務接口
    /// </summary>
    public interface IContractRemittanceService:IService<ContractRemittance,ContractRemittanceOutputDto, string>
    {
    }
}
