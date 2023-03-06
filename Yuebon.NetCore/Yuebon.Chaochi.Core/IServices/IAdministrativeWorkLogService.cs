using System;
using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Dtos;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.Pages;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義行政工作日誌服務接口
    /// </summary>
    public interface IAdministrativeWorkLogService:IService<AdministrativeWorkLog,AdministrativeWorkLogOutputDto, string>
    {
        //Task<PageResult<AdministrativeWorkLogOutputDto>> FindWithPagerAsync(SearchInputDto<AdministrativeWorkLog> search, IDbConnection conn = null, IDbTransaction trans = null);
    }
}
