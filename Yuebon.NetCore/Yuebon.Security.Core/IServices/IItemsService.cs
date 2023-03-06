using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.IServices;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    public interface IItemsService:IService<Items, ItemsOutputDto, string>
    {

        /// <summary>
        /// 獲取功能菜單適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        Task<List<ItemsOutputDto>> GetAllItemsTreeTable();

        /// <summary>
        /// 根據編碼查詢字典分類
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        Task<Items> GetByEnCodAsynce(string enCode);


        /// <summary>
        /// 更新時判斷分類編碼是否存在（排除自己）
        /// </summary>
        /// <param name="enCode">分類編碼</param
        /// <param name="id">主鍵Id</param>
        /// <returns></returns>
        Task<Items> GetByEnCodAsynce(string enCode, string id);
    }
}
