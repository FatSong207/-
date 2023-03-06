using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.IServices;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 數據字典明細
    /// </summary>
    public interface IItemsDetailService:IService<ItemsDetail, ItemsDetailOutputDto, string>
    {
        /// <summary>
        /// 根據數據字典分類編碼獲取該分類可用內容
        /// </summary>
        /// <param name="itemCode">分類編碼</param>
        /// <returns></returns>
        Task<List<ItemsDetailOutputDto>> GetItemDetailsByItemCode(string itemCode);

        /// <summary>
        /// 獲取適用于Vue 樹形列表
        /// </summary>
        /// <param name="itemId">類別Id</param>
        /// <returns></returns>
       Task<List<ItemsDetailOutputDto>> GetAllItemsDetailTreeTable(string itemId);
    }
}
