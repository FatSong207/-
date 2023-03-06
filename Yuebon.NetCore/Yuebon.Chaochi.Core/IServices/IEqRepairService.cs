using System;
using System.Threading.Tasks;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Pages;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface IEqRepairService:IService<EqRepair,EqRepairOutputDto, string>
    {
        Task<PageResult<EqRepairOutputDto>> FindWithPagerAsync(SearchInputDto<EqRepair> search);

        /// <summary>
        /// 獲取建物照片
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <returns></returns>
        Task<FileInfoOutputDto> GetImgOutDtoAsync(string id);
    }
}
