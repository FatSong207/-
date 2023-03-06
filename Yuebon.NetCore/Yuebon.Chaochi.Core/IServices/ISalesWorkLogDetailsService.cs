using System;
using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義業務工作日誌明細服務接口
    /// </summary>
    public interface ISalesWorkLogDetailsService:IService<SalesWorkLogDetails,SalesWorkLogDetailsOutputDto, string>
    {
    }
}
