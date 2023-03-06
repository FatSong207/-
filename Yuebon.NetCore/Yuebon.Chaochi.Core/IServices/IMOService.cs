using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using Yuebon.Commons.Pages;
using System.Data;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface IMOService:IService<MO,MOOutputDto, string>
    {
        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        Task<PageResult<MOOutputDto>> FindWithPagerSearchAsync(SearchMOModel search);

        /// <summary>
        /// 根據多物件單號查詢多物件資料
        /// </summary>
        /// <param name="moid">多物件單號</param>
        /// <returns></returns>
        Task<MOOutputDto> GetByMOID(string moid);

        /// <summary>
        /// 更新多物件單狀態
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="moid">多物件單號</param>
        /// <param name="status">多物件單狀態</param>
        /// <returns></returns> 
        Task<bool> UpdateMOStatus(string userId, string moid, string status, IDbConnection conn = null, IDbTransaction trans = null);
    }
}
