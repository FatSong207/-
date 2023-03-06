using System.Threading.Tasks;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Pages;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 用戶服務接口
    /// </summary>
    public interface IInternalformService : IService<Internalform, InternalformOutputDto, string>
    { 

        Task<Internalform> GetByFormId(string FormId);

        Task<PageResult<InternalformOutputDto>> FindFormListWithPage(SearchInternalformModel search);

    }
}
