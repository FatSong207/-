using System;
using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Data;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義待辦事項服務接口
    /// </summary>
    public interface IToDoListService:IService<ToDoList,ToDoListOutputDto, string>
    {
        /// <summary>
        /// 更新待辧事項狀態
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="account">審核者Id</param>
        /// <param name="typeId">功能類型ID</param>
        /// <param name="status">當前狀態</param>
        /// <returns></returns> 
        Task<bool> UpdateToDoListStatus(string userId, string account, string typeId, string status, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 根據功能模組ID查詢待辦事項
        /// </summary>
        /// <param name="typeId">功能模組ID</param>
        /// <returns></returns>
        Task<ToDoListOutputDto> GetByTypeID(string typeId);

        /// <summary>
        /// 根據功能模組ID和功能模組資料查詢待辦事項
        /// </summary>
        /// <param name="typeId">功能模組ID</param>
        /// <param name="typeData">功能模組資料</param>
        /// <returns></returns>
        Task<ToDoListOutputDto> GetByTypeData(string typeId, string typeData);
    }
}
