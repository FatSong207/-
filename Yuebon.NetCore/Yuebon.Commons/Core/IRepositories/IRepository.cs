using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.IRepositories;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Repositories;
namespace Yuebon.Commons.IRepositories
{
    /// <summary>
    /// 定義泛型接口,實體倉儲模型的數據標準操作
    /// </summary>
    /// <typeparam name="T">實體類型</typeparam>
    /// <typeparam name="TKey">主鍵類型</typeparam>
    public interface IRepository<T, TKey> : IDisposable where T : Entity
    {
        #region dapper 操作

        #region 新增
        /// <summary>
        /// 同步新增實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        long Insert(T entity, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 異步新增實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<long> InsertAsync(T entity, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 同步批量新增實體。
        /// </summary>
        /// <param name="entities">實體集合</param>
        /// <returns></returns>
        void Insert(List<T> entities, IDbConnection conn = null, IDbTransaction trans = null);
        #endregion

        #region 刪除
        /// <summary>
        /// 同步物理刪除實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <returns></returns>
        bool Delete(T entity);

        /// <summary>
        /// 異步物理刪除實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity, IDbTransaction trans = null);

        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="where">條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        bool DeleteConnWhereAsync(string where, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 同步物理刪除實體。
        /// </summary>
        /// <param name="primaryKey">主鍵</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        bool Delete(TKey primaryKey, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 異步物理刪除實體。
        /// </summary>
        /// <param name="primaryKey">主鍵</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TKey primaryKey, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 按主鍵批量刪除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        bool DeleteBatch(IList<dynamic> ids, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="where">條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        bool DeleteBatchWhere(string where, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 異步按條件批量刪除
        /// </summary>
        /// <param name="where">條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<bool> DeleteBatchWhereAsync(string where, IDbConnection conn = null, IDbTransaction trans = null);

        #endregion

        #region 更新操作

        #region 更新實體或批量更新
        /// <summary>
        /// 同步更新實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="primaryKey">主鍵ID</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        bool Update(T entity, TKey primaryKey, IDbTransaction trans = null);

        /// <summary>
        /// 異步更新實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="primaryKey">主鍵ID</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity, TKey primaryKey, IDbConnection conn = null, IDbTransaction trans = null);

        #endregion

        #region 更新某一字段值
        /// <summary>
        /// 更新某一字段值
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="where">條件,為空更新所有內容</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        bool UpdateTableField(string strField, string fieldValue, string where, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 異步更新某一字段值
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="where">條件,為空更新所有內容</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        Task<bool> UpdateTableFieldAsync(string strField, string fieldValue, string where, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 更新某一字段值，字段值為數字
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="fieldValue">字段值數字</param>
        /// <param name="where">條件,為空更新所有內容</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        bool UpdateTableField(string strField, int fieldValue, string where, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 更新某一字段值，字段值為數字
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="fieldValue">字段值數字</param>
        /// <param name="where">條件,為空更新所有內容</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        Task<bool> UpdateTableFieldAsync(string strField, int fieldValue, string where, IDbConnection conn = null, IDbTransaction trans = null);
        #endregion

        #region 邏輯刪除
        /// <summary>
        /// 同步軟刪除信息，將DeleteMark設置為1-刪除，0-恢復刪除
        /// </summary>
        /// <param name="bl">true為不刪除，false刪除</param>
        /// <param name="primaryKey">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        bool DeleteSoft(bool bl, TKey primaryKey, string userId = null, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 異步軟刪除信息，將DeleteMark設置為1-刪除，0-恢復刪除
        /// </summary>
        /// <param name="bl">true為不刪除，false刪除</param> c
        /// <param name="primaryKey">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<bool> DeleteSoftAsync(bool bl, TKey primaryKey, string userId = null, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 異步批量軟刪除信息，將DeleteMark設置為1-刪除，0-恢復刪除
        /// </summary>
        /// <param name="bl">true為不刪除，false刪除</param> c
        /// <param name="where">條件</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<bool> DeleteSoftBatchAsync(bool bl, string where, string userId = null, IDbConnection conn = null, IDbTransaction trans = null);

        #endregion

        #region 數據有效性
        /// <summary>
        /// 設置數據有效性，將EnabledMark設置為1-有效，0-為無效
        /// </summary>
        /// <param name="bl">true為有效，false無效</param>
        /// <param name="primaryKey">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        bool SetEnabledMark(bool bl, TKey primaryKey, string userId = null, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 異步設置數據有效性，將EnabledMark設置為1:有效，0-為無效
        /// </summary>
        /// <param name="bl">true為有效，false無效</param>
        /// <param name="primaryKey">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<bool> SetEnabledMarkAsync(bool bl, TKey primaryKey, string userId = null, IDbConnection conn = null, IDbTransaction trans = null);


        /// <summary>
        /// 異步按條件設置數據有效性，將EnabledMark設置為1:有效，0-為無效
        /// </summary>
        /// <param name="bl">true為有效，false無效</param>
        /// <param name="where">條件</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<bool> SetEnabledMarkByWhereAsync(bool bl, string where, string userId = null, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 異步按條件設置數據有效性，將EnabledMark設置為1:有效，0-為無效
        /// </summary>
        /// <param name="bl">true為有效，false無效</param>
        /// <param name="where">條件</param>
        /// <param name="paramparameters">參數</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<bool> SetEnabledMarkByWhereAsync(bool bl, string where, object paramparameters = null, string userId = null, IDbConnection conn = null, IDbTransaction trans = null);

        #endregion

        #endregion

        #region 查詢


        #region 單個實體
        /// <summary>
        /// 同步查詢單個實體。
        /// </summary>
        /// <param name="primaryKey">主鍵</param>
        /// <returns></returns>
        T Get(TKey primaryKey, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 異步查詢單個實體。
        /// </summary>
        /// <param name="primaryKey">主鍵</param>
        /// <returns></returns>
        Task<T> GetAsync(TKey primaryKey, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 同步查詢單個實體。
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        T GetWhere(string where, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 異步查詢單個實體。
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        Task<T> GetWhereAsync(string where, IDbConnection conn = null, IDbTransaction trans = null);

        #endregion

        /// <summary>
        /// 獲取所有數據，謹慎使用
        /// </summary>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        IEnumerable<T> GetAll(IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 獲取所有數據，謹慎使用
        /// </summary>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(IDbConnection conn = null, IDbTransaction trans = null);


        /// <summary>
        /// 根據查詢條件查詢數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        IEnumerable<T> GetListWhere(string where = null, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 異步根據查詢條件查詢數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListWhereAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 根據查詢條件查詢前多少條數據
        /// </summary>
        /// <param name="top">多少條數據</param>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        IEnumerable<T> GetListTopWhere(int top, string where = null, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 根據查詢條件查詢前多少條數據
        /// </summary>
        /// <param name="top">多少條數據</param>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListTopWhereAsync(int top, string where = null, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 查詢軟刪除的數據，如果查詢條件為空，即查詢所有軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        IEnumerable<T> GetAllByIsDeleteMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 查詢未軟刪除的數據，如果查詢條件為空，即查詢所有未軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="tran">事務對象</param>
        /// <returns></returns>
        IEnumerable<T> GetAllByIsNotDeleteMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 查詢有效的數據，如果查詢條件為空，即查詢所有有效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="tran">事務對象</param>
        /// <returns></returns>
        IEnumerable<T> GetAllByIsEnabledMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 查詢無效的數據，如果查詢條件為空，即查詢所有無效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="tran">事務對象</param>
        /// <returns></returns>
        IEnumerable<T> GetAllByIsNotEnabledMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 查詢未軟刪除且有效的數據，如果查詢條件為空，即查詢所有數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="tran">事務對象</param>
        /// <returns></returns>
        IEnumerable<T> GetAllByIsNotDeleteAndEnabledMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 查詢軟刪除的數據，如果查詢條件為空，即查詢所有軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="tran">事務對象</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllByIsDeleteMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 查詢未軟刪除的數據，如果查詢條件為空，即查詢所有未軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="tran">事務對象</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllByIsNotDeleteMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 查詢有效的數據，如果查詢條件為空，即查詢所有有效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="tran">事務對象</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllByIsEnabledMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 查詢無效的數據，如果查詢條件為空，即查詢所有無效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="tran">事務對象</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllByIsNotEnabledMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 查詢未軟刪除且有效的數據，如果查詢條件為空，即查詢所有數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllByIsNotDeleteAndEnabledMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <param name="trans">事務對象</param>
        /// <returns>指定對象的集合</returns>
        Task<List<T>> FindWithPagerAsync(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <param name="trans">事務對象</param>
        /// <returns>指定對象的集合</returns>
        List<T> FindWithPager(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="trans">事務對象</param>
        /// <returns>指定對象的集合</returns>
        Task<List<T>> FindWithPagerAsync(string condition, PagerInfo info, string fieldToSort, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="trans">事務對象</param>
        /// <returns>指定對象的集合</returns>
        List<T> FindWithPager(string condition, PagerInfo info, string fieldToSort, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns>指定對象的集合</returns>
        Task<List<T>> FindWithPagerAsync(string condition, PagerInfo info, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns>指定對象的集合</returns>
        List<T> FindWithPager(string condition, PagerInfo info, IDbConnection conn = null, IDbTransaction trans = null);


        /// <summary>
        /// 分頁查詢，自行封裝sql語句(僅支持sql server)
        /// 非常復雜的查詢，可在具體業務模塊重寫該方法
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <param name="info">分頁信息</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">排序方式 true為desc，false為asc</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        Task<List<T>> FindWithPagerSqlAsync(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 分頁查詢，自行封裝sql語句(僅支持sql server)
        /// 非常復雜的查詢，可在具體業務模塊重寫該方法
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <param name="info">分頁信息</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">排序方式 true為desc，false為asc</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        List<T> FindWithPagerSql(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 分頁查詢包含用戶信息(僅支持sql server)
        /// 查詢主表別名為t1,用戶表別名為t2，在查詢字段需要注意使用t1.xxx格式，xx表示主表字段
        /// 用戶信息主要有用戶帳號：Account、昵稱：NickName、真實姓名：RealName、頭像：HeadIcon、手機號：MobilePhone
        /// 輸出對象請在Dtos中進行自行封裝，不能是使用實體Model類
        /// </summary>
        /// <param name="condition">查詢條件字段需要加表別名</param>
        /// <param name="info">分頁信息</param>
        /// <param name="fieldToSort">排序字段，也需要加表別名</param>
        /// <param name="desc">排序方式</param>
        /// <param name="trans">事務</param>
        /// <returns></returns>
        Task<List<object>> FindWithPagerRelationUserAsync(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 分頁查詢包含用戶信息(僅支持sql server)
        /// 查詢主表別名為t1,用戶表別名為t2，在查詢字段需要注意使用t1.xxx格式，xx表示主表字段
        /// 用戶信息主要有用戶帳號：Account、昵稱：NickName、真實姓名：RealName、頭像：HeadIcon、手機號：MobilePhone
        /// 輸出對象請在Dtos中進行自行封裝，不能是使用實體Model類
        /// </summary>
        /// <param name="condition">查詢條件字段需要加表別名</param>
        /// <param name="info">分頁信息</param>
        /// <param name="fieldToSort">排序字段，也需要加表別名</param>
        /// <param name="desc">排序方式</param>
        /// <param name="trans">事務</param>
        /// <returns></returns>
        List<object> FindWithPagerRelationUser(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 根據條件統計數據
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <param name="fieldName">統計字段名稱</param>
        /// <returns></returns>
        int GetCountByWhere(string condition, string fieldName = "*", IDbConnection conn = null, IDbTransaction trans = null);
        /// <summary>
        /// 根據條件統計數據
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <param name="fieldName">統計字段名稱</param>
        /// <returns></returns>
        Task<int> GetCountByWhereAsync(string condition, string fieldName = "*", IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 根據條件查詢獲取某個字段的最大值
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="where">條件</param>
        /// <param name="trans">事務</param>
        /// <returns>返回字段的最大值</returns>
        Task<decimal> GetMaxValueByFieldAsync(string strField, string where, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 根據條件統計某個字段之和,sum(字段)
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="where">條件</param>
        /// <param name="trans">事務</param>
        /// <returns>返回字段求和后的值</returns>
        Task<decimal> GetSumValueByFieldAsync(string strField, string where, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 獲取distinct後的集合
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="where"></param>
        /// <returns></returns>

        Task<IEnumerable<T>> GetDistinctByFieldAsync(string fieldName, string where);

        #endregion

        #region 多表批量操作，支持事務

        /// <summary>
        /// 多表操作--事務
        /// </summary>
        /// <param name="trans">事務</param>
        /// <param name="commandTimeout">超時</param>
        /// <returns></returns>
        Task<Tuple<bool, string>> ExecuteTransactionAsync(List<Tuple<string, object>> trans, int? commandTimeout = null);
        /// <summary>
        /// 多表操作--事務
        /// </summary>
        /// <param name="trans">事務</param>
        /// <param name="commandTimeout">超時</param>
        /// <returns></returns>
        Tuple<bool, string> ExecuteTransaction(List<Tuple<string, object>> trans, int? commandTimeout = null);

        #endregion

        #endregion

        #region EF 操作

        #region Insert 新增
        /// <summary>
        /// 新增實體
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Add(T entity);
        /// <summary>
        /// 新增實體
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> AddAsync(T entity);
        /// <summary>
        /// 批量新增，數量量較多是推薦使用BulkInsert()
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int AddRange(ICollection<T> entities);
        /// <summary>
        /// 批量新增，數量量較多是推薦使用BulkInsert()
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> AddRangeAsync(ICollection<T> entities);
        /// <summary>
        /// 批量新增SqlBulk方式，效率最高
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="destinationTableName"></param>
        void BulkInsert(IList<T> entities, string destinationTableName = null);
        /// <summary>
        /// 執行新增的sql語句
        /// </summary>
        /// <param name="sql">新增Sql語句</param>
        /// <returns></returns>
        int AddBySql(string sql);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        public void InsertRange(List<T> entities, IDbConnection conn = null, IDbTransaction trans = null);

        #endregion

        #region Delete 刪除
        /// <summary>
        /// 根據主鍵刪除數據
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int Delete(TKey key);
        /// <summary>
        /// 執行刪除數據Sql語句
        /// </summary>
        /// <param name="sql">刪除的Sql語句</param>
        /// <returns></returns>
        int DeleteBySql(string sql);
        #endregion

        #region Update 更新
        /// <summary>
        /// 更新一個實體數據
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Edit(T entity);
        /// <summary>
        /// 批量更新數據實體
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int EditRange(ICollection<T> entities);
        /// <summary>
        /// 更新指定字段的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">數據實體</param>
        /// <param name="updateColumns">指定字段</param>
        /// <returns></returns>
        int Update(T model, params string[] updateColumns);
        /// <summary>
        /// 執行更新數據的Sql語句
        /// </summary>
        /// <param name="sql">更新數據的Sql語句</param>
        /// <returns></returns>
        int UpdateBySql(string sql);

        #endregion

        #region Query 查詢

        /// <summary>
        /// 根據條件統計數量Count()
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> @where = null);
        /// <summary>
        /// 根據條件統計數量Count()
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> @where = null);
        /// <summary>
        /// 是否存在,存在返回true，不存在返回false
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Exist(Expression<Func<T, bool>> @where = null);
        /// <summary>
        /// 是否存在,存在返回true，不存在返回false
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(Expression<Func<T, bool>> @where = null);
        /// <summary>
        ///  根據主鍵獲取實體。建議：如需使用Include和ThenInclude請重載此方法。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetSingle(TKey key);
        /// <summary>
        ///  根據主鍵獲取實體。建議：如需使用Include和ThenInclude請重載此方法。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetSingleAsync(TKey key);
        /// <summary>
        /// 獲取單個實體。建議：如需使用Include和ThenInclude請重載此方法。
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T GetSingleOrDefault(Expression<Func<T, bool>> @where = null);
        /// <summary>
        /// 獲取單個實體。建議：如需使用Include和ThenInclude請重載此方法。
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> @where = null);
        /// <summary>
        /// 獲取實體列表。建議：如需使用Include和ThenInclude請重載此方法。
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IList<T> Get(Expression<Func<T, bool>> @where = null);
        /// <summary>
        /// 獲取實體列表。建議：如需使用Include和ThenInclude請重載此方法。
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<List<T>> GetAsync(Expression<Func<T, bool>> @where = null);
        /// <summary>
        ///  分頁獲取實體列表。建議：如需使用Include和ThenInclude請重載此方法。
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="pagerInfo">分頁信息</param>
        /// <param name="asc">排序方式</param>
        /// <param name="orderby">排序字段</param>
        /// <returns></returns>
        IEnumerable<T> GetByPagination(Expression<Func<T, bool>> @where, PagerInfo pagerInfo, bool asc = true,
            params Expression<Func<T, object>>[] @orderby);
        /// <summary>
        /// sql語句查詢數據集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<T> GetBySql(string sql);
        /// <summary>
        /// sql語句查詢數據集，返回輸出Dto實體
        /// </summary>
        /// <typeparam name="TView">返回結果對象</typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<TView> GetViews<TView>(string sql);
        /// <summary>
        /// 查詢視圖
        /// </summary>
        /// <typeparam name="TView">返回結果對象</typeparam>
        /// <param name="viewName">視圖名稱</param>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        List<TView> GetViews<TView>(string viewName, Func<TView, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        IEnumerable<string> GetUserPermissionUser(string where = null, IDbConnection conn = null, IDbTransaction trans = null);

        #endregion
        #endregion
    }
}
