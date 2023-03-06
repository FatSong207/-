using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Commons.Pages;
using System.Threading.Tasks;
using Yuebon.Commons.Models;
using System.Data;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.Dtos;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義領收據服務接口
    /// </summary>
    public interface IReceiptService:IService<Receipt,ReceiptOutputDto, string>
    {
        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        Task<PageResult<ReceiptOutputDto>> FindWithPagerSearchAsync(SearchReceiptModel search);

        /// <summary>
        /// 非同步按條件批次刪除
        /// </summary>
        /// <param name="ids">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<CommonResult> DeleteBatchWhereAsync(DeletesInputDto ids, IDbTransaction trans = null);

        /// <summary>
        /// 異步設定領收據狀態
        /// </summary>
        /// <param name="status">狀態</param>
        /// <param name="receiptId">領收據編號</param>
        /// <param name="userId">使用者ID</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<bool> SetReceiptStatusAsync(string status, string receiptId, string userId = null, IDbTransaction trans = null);
    }
}
