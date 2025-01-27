﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;

namespace Yuebon.Commons.IDbContext
{
    /// <summary>
    /// 上下文基礎接口
    /// </summary>
    public interface IDbContextCore: IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DatabaseFacade GetDatabase();

        #region 新增
        /// <summary>
        /// 新增實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Add<T>(T entity) where T : class;
        /// <summary>
        /// 異步新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> AddAsync<T>(T entity) where T : class;
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        int AddRange<T>(ICollection<T> entities) where T : class;
        /// <summary>
        /// 異步批量新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> AddRangeAsync<T>(ICollection<T> entities) where T : class;
        #endregion

        #region 刪除
        /// <summary>
        /// 物理刪除數據
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">主鍵類型</typeparam>
        /// <param name="key">主鍵</param>
        /// <returns></returns>
        int Delete<T,TKey>(TKey key) where T : Entity;

        /// <summary>
        /// 根據條件刪除一個實體，返回影響記錄行數
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">條件</param>
        /// <returns></returns>
        ////int Delete<T>(Expression<Func<T, bool>> @where) where T : class;
        /// <summary>
        /// 根據條件刪除一個實體，返回影響記錄行數
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">條件</param>
        /// <returns></returns>
        //Task<int> DeleteAsync<T>(Expression<Func<T, bool>> @where) where T : class;
        #endregion

        /// <summary>
        /// 創建數據表
        /// </summary>
        /// <returns></returns>
        bool EnsureCreated();
        /// <summary>
        /// 異步創建數據表
        /// </summary>
        /// <returns></returns>
        Task<bool> EnsureCreatedAsync();
        /// <summary>
        /// 執行Sql語句，返回影響記錄行數
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlWithNonQuery(string sql, params object[] parameters);
        /// <summary>
        /// 執行Sql，返回影響記錄行數
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> ExecuteSqlWithNonQueryAsync(string sql, params object[] parameters);

        #region 更新
        /// <summary>
        /// 更新保存實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Edit<T>(T entity) where T : class;
        /// <summary>
        /// 批量更新保存實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        int EditRange<T>(ICollection<T> entities) where T : class;

        /// <summary>
        /// 更新指定字段的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">數據實體</param>
        /// <param name="updateColumns">指定字段</param>
        /// <returns></returns>
        int Update<T>(T model, params string[] updateColumns) where T : class;

        /// <summary>
        /// 按條件更新，
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">條件</param>
        /// <param name="updateFactory"></param>
        /// <returns></returns>
        //int Update<T>(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory) where T : class;

        /// <summary>
        /// 按條件更新，
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">條件</param>
        /// <param name="updateFactory"></param>
        /// <returns></returns>
       // Task<int> UpdateAsync<T>(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory)
        //    where T : class;
        #endregion

        #region 查詢
        /// <summary>
        /// 根據條件統計數量Count()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        int Count<T>(Expression<Func<T, bool>> @where = null) where T : class;
        /// <summary>
        /// 根據條件異步統計數量Count()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        Task<int> CountAsync<T>(Expression<Func<T, bool>> @where = null) where T : class;
        /// <summary>
        /// 是否存在,存在返回true，不存在返回false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        bool Exist<T>(Expression<Func<T, bool>> @where = null) where T : class;
        /// <summary>
        /// 是否存在,存在返回true，不存在返回false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        Task<bool> ExistAsync<T>(Expression<Func<T, bool>> @where = null) where T : class;
        /// <summary>
        /// 根據條件進行查詢數據
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="include"></param>
        /// <param name="where">查詢數據</param>
        /// <returns></returns>
        IQueryable<T> FilterWithInclude<T>(Func<IQueryable<T>, IQueryable<T>> include, Expression<Func<T, bool>> where) where T : class;
       
        /// <summary>
        /// 根據主鍵查詢實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">主鍵值</param>
        /// <returns></returns>
        T Find<T>(object key) where T : class;

        /// <summary>
        /// 根據主鍵查詢實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">主鍵值</param>
        /// <returns></returns>
        T Find<T>(string key) where T : class;
        /// <summary>
        /// 根據主鍵查詢實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">主鍵類型</typeparam>
        /// <param name="key">主鍵值</param>
        /// <returns></returns>
        T Find<T, TKey>(TKey key) where T : Entity;
        /// <summary>
        /// 根據主鍵查詢實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">主鍵值</param>
        /// <returns></returns>
        Task<T> FindAsync<T>(object key) where T : class;
        /// <summary>
        /// 根據主鍵查詢實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">主鍵類型</typeparam>
        /// <param name="key">主鍵值</param>
        /// <returns></returns>
        Task<T> FindAsync<T,TKey>(TKey key) where T : Entity;
        /// <summary>
        /// 根據條件查詢實體，返回實體集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查詢條件</param>
        /// <param name="asNoTracking">是否啟用模型跟蹤，默認為false不跟蹤</param>
        /// <returns></returns>
        IQueryable<T> Get<T>(Expression<Func<T, bool>> @where = null, bool asNoTracking = false) where T : class;
        /// <summary>
        /// 獲取所有實體類型
        /// </summary>
        /// <returns></returns>
        List<IEntityType> GetAllEntityTypes();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        DbSet<T> GetDbSet<T>() where T : class;

        /// <summary>
        /// 根據條件查詢一個實體，
        /// 檢驗序列中是否包含有元素。引用類型的默認值default(T)為null，表示在序列中沒有找到元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        T GetSingleOrDefault<T>(Expression<Func<T, bool>> @where = null) where T : class;

        /// <summary>
        /// 根據條件查詢一個實體，
        /// 檢驗序列中是否包含有元素。引用類型的默認值default(T)為null，表示在序列中沒有找到元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        Task<T> GetSingleOrDefaultAsync<T>(Expression<Func<T, bool>> @where = null) where T : class;
        #endregion

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">數據實體集合</param>
        /// <param name="destinationTableName">數據庫表名稱，默認為實體名稱</param>
        /// <returns></returns>
        void BulkInsert<T>(IList<T> entities, string destinationTableName = null, IDbConnection conn = null, IDbTransaction trans = null)
             where T : class ;
        /// <summary>
        /// Sql查詢，并返回實體集合
        /// </summary>
        /// <typeparam name="T">查詢對象實體</typeparam>
        /// <typeparam name="TView">返回/輸出實體</typeparam>
        /// <param name="sql">sql語句</param>
        /// <param name="parameters">SQL參數</param>
        /// <returns></returns>
        List<TView> SqlQuery<T, TView>(string sql, params object[] parameters) 
            where T : class;
        /// <summary>
        /// Sql查詢，并返回實體集合
        /// </summary>
        /// <typeparam name="T">查詢對象實體</typeparam>
        /// <typeparam name="TView">返回/輸出實體</typeparam>
        /// <param name="sql">sql語句</param>
        /// <param name="parameters">SQL參數</param>
        /// <returns></returns>
        Task<List<TView>> SqlQueryAsync<T, TView>(string sql, params object[] parameters)
            where T : class
            where TView : class;
        /// <summary>
        /// 分頁查詢，SQL語句查詢，返回指定輸出對象集合
        /// </summary>
        /// <typeparam name="T">查詢對象實體</typeparam>
        /// <typeparam name="TView">返回/輸出實體</typeparam>
        /// <param name="sql">sql語句</param>
        /// <param name="orderBys">排序條件</param>
        /// <param name="pageIndex">當前頁</param>
        /// <param name="pageSize">每頁顯示數量</param>
        /// <param name="eachAction"></param>
        /// <returns></returns>
        PageResult<T> SqlQueryByPagination<T, TView>(string sql, string[] orderBys, int pageIndex, int pageSize, Action<TView> eachAction = null)
            where T : class
            where TView : class;
        /// <summary>
        /// 分頁查詢，SQL語句查詢，返回數據實體集合
        /// </summary>
        /// <typeparam name="T">查詢對象實體</typeparam>
        /// <param name="sql">sql語句</param>
        /// <param name="orderBys">排序條件</param>
        /// <param name="pageIndex">當前頁</param>
        /// <param name="pageSize">每頁顯示數量</param>
        /// <param name="parameters">查詢SQL參數</param>
        /// <returns></returns>
        PageResult<T> SqlQueryByPagination<T>(string sql, string[] orderBys, int pageIndex, int pageSize,
            params DbParameter[] parameters) where T : class, new();
        /// <summary>
        /// 保存到數據庫
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// 保存到數據庫
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">更改成功發送到數據庫后是否調用AcceptAllChanges()</param>
        /// <returns></returns>
        int SaveChanges(bool acceptAllChangesOnSuccess);
        /// <summary>
        /// 保存到數據庫
        /// </summary>
        /// <param name="cancellationToken">是否等待任務完成時要觀察</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 保存到數據庫
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">是否更改成功發送到數據庫后是否調用AcceptAllChanges()</param>
        /// <param name="cancellationToken">是否等待任務完成時要觀察</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 根據sql語句返回DataTable數據
        /// </summary>
        /// <param name="sql">Sql語句</param>
        /// <param name="cmdTimeout">執行超時時間，默認30毫秒</param>
        /// <param name="parameters">DbParameter[]參數</param>
        /// <returns></returns>
        DataTable GetDataTable(string sql, int cmdTimeout = 30, params DbParameter[] parameters);

        /// <summary>
        /// 根據sql語句返回List集合數據
        /// </summary>
        /// <param name="sql">Sql語句</param>
        /// <param name="cmdTimeout">執行超時時間，默認30毫秒</param>
        /// <param name="parameters">DbParameter[]參數</param>
        /// <returns></returns>
        List<DataTable> GetDataTables(string sql, int cmdTimeout=30, params DbParameter[] parameters);

        #region 顯式編譯的查詢,提高查詢性能
        /// <summary>
        /// 根據主鍵查詢返回一個實體，該方法是顯式編譯的查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">主鍵類型</typeparam>
        /// <param name="id">主鍵值</param>
        /// <returns></returns>
        T GetByCompileQuery<T,TKey>(TKey id) where T : Entity;
        /// <summary>
        /// 根據主鍵查詢返回一個實體，該方法是顯式編譯的查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">主鍵類型</typeparam>
        /// <param name="id">主鍵值</param>
        /// <returns></returns>
        Task<T> GetByCompileQueryAsync<T, TKey>(TKey id) where T : Entity;
        /// <summary>
        /// 根據條件查詢返回實體集合，該方法是顯式編譯的查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        IList<T> GetByCompileQuery<T>(Expression<Func<T, bool>> filter) where T : class;
        /// <summary>
        /// 根據條件查詢返回實體集合，該方法是顯式編譯的查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        Task<List<T>> GetByCompileQueryAsync<T>(Expression<Func<T, bool>> filter) where T : class;
        /// <summary>
        /// 根據條件查詢一個實體，該方法是顯式編譯的查詢
        /// 檢驗序列中是否包含有元素。引用類型的默認值default(T)為null，表示在序列中沒有找到元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        T FirstOrDefaultByCompileQuery<T>(Expression<Func<T, bool>> filter) where T : class;
        /// <summary>
        /// 根據條件查詢一個實體，該方法是顯式編譯的查詢
        /// 檢驗序列中是否包含有元素。引用類型的默認值default(T)為null，表示在序列中沒有找到元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        Task<T> FirstOrDefaultByCompileQueryAsync<T>(Expression<Func<T, bool>> filter) where T : class;
        /// <summary>
        /// 根據條件查詢一個實體，啟用模型跟蹤，該方法是顯式編譯的查詢
        /// 檢驗序列中是否包含有元素。引用類型的默認值default(T)為null，表示在序列中沒有找到元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        T FirstOrDefaultWithTrackingByCompileQuery<T>(Expression<Func<T, bool>> filter) where T : class;
        /// <summary>
        /// 根據條件查詢一個實體，啟用模型跟蹤，該方法是顯式編譯的查詢
        /// 檢驗序列中是否包含有元素。引用類型的默認值default(T)為null，表示在序列中沒有找到元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        Task<T> FirstOrDefaultWithTrackingByCompileQueryAsync<T>(Expression<Func<T, bool>> filter) where T : class;
        /// <summary>
        /// 統計數量Count()，該方法是顯式編譯的查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        int CountByCompileQuery<T>(Expression<Func<T, bool>> filter) where T : class;
        /// <summary>
        /// 統計數量Count()，該方法是顯式編譯的查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        Task<int> CountByCompileQueryAsync<T>(Expression<Func<T, bool>> filter) where T : class;
        #endregion
    }
}