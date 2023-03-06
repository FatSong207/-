using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface IContractformService:IService<Contractform,ContractformOutputDto, string>
    {
        /// <summary>
        /// 根據表單編號查詢表單資料
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns></returns>
        Task<ContractformOutputDto> GetByFormId(string formId);
    }
}
