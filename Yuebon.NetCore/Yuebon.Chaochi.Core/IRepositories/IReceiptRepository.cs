using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Data;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義倉儲接口
    /// </summary>
    public interface IReceiptRepository:IRepository<Receipt, string>
    {
        /// <summary>
        /// 根據領收據編號查詢收據編資料
        /// </summary>
        /// <param name="ReceiptId"></param>
        /// <returns></returns>    
        Task<Receipt> GetReceiptById(string ReceiptId);

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