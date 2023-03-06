using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Data;
using Yuebon.Commons.Pages;
using System;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義建物廣告管理服務接口
    /// </summary>
    public interface IBuildingAdvertisementService:IService<BuildingAdvertisement,BuildingAdvertisementOutputDto, string>
    {
        /// <summary>
        /// 根據建物地址查詢建物廣告資訊
        /// </summary>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        Task<BuildingAdvertisement> GetByBAdd(string BAdd);

        /// <summary>
        /// 分頁查詢包含建物資訊
        /// </summary>
        /// <param name="condition">查詢條件字段需要加表別名</param>
        /// <param name="info">分頁信息</param>
        /// <param name="fieldToSort">排序字段，也需要加表別名</param>
        /// <param name="desc">排序方式</param>
        /// <param name="trans">事務</param>
        /// <returns></returns>
        Task<PageResult<BuildingAdvertisementOutputDto>> FindWithPagerAsync(SearchBuildingAdvertisementModel search, IDbConnection conn = null, IDbTransaction trans = null);        

        /// <summary>
        /// 新增建物廣告
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        Task<bool> InsertAsync(BuildingAdvertisement entity, IDbTransaction trans = null);

        /// <summary>
        /// 更新建物廣告狀態
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="Id">主鍵值</param>
        /// <param name="status">建物廣告狀態</param>
        /// <returns></returns> 
        Task<bool> UpdateBAStatus(string userId, string Id, string status, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 更新建物廣告網址
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="Id">主鍵值</param>
        /// <param name="URL">建物廣告址</param>
        /// <returns></returns> 
        Task<bool> UpdateBAURL(string userId, string Id, string URL, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 更新建物廣告出租天數
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="Id">主鍵值</param>
        /// <param name="brDays">廣告出租天數</param>
        /// <returns></returns> 
        bool UpdateBRDays(string userId, string Id, string brDays, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 更新建物廣告天數
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="Id">主鍵值</param>
        /// <param name="baDays">廣告天數</param>
        /// <returns></returns> 
        bool UpdateBADays(string userId, string Id, string baDays, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 更新建物廣告出租天數和廣告天數
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="Id">主鍵值</param>
        /// <param name="brDays">廣告出租天數</param>
        /// <param name="baDays">廣告天數</param>
        /// <returns></returns> 
        bool UpdateBDays(string userId, string Id, string brDays, string baDays, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 更新建物廣告招租日期
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="Id">主鍵值</param>
        /// <param name="brStartDate">待招租起始日</param>
        /// <returns></returns> 
        Task<bool> UpdateBRStartDate(string userId, string Id, DateTime brStartDate, IDbConnection conn = null, IDbTransaction trans = null);
    }
}
