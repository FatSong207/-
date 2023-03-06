using System;
using Yuebon.Commons.IServices;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 定義序號編碼規則表服務接口
    /// </summary>
    public interface ISequenceRuleService : IService<SequenceRule, SequenceRuleOutputDto, string>
    {
    }
}
