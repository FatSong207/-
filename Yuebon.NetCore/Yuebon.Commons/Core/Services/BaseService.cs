using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Yuebon.Commons.Cache;
using Yuebon.Commons.DependencyInjection;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IRepositories;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Json;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;

namespace Yuebon.Commons.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TODto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseService<T, TODto, TKey> : IService<T, TODto, TKey>, ITransientDependency
        where T : Entity
        where TODto : class
        where TKey : IEquatable<TKey>
    {
        private readonly IHttpContextAccessor _accessor;
        /// <summary>
        /// 
        /// </summary>
        protected IRepository<T, TKey> repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRepository"></param>
        protected BaseService(IRepository<T, TKey> iRepository)
        {
            repository = iRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRepository"></param>
        /// <param name="accessor"></param>
        protected BaseService(IRepository<T, TKey> iRepository, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            repository = iRepository;

        }
        /// <summary>
        /// 同步物理刪除實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual bool Delete(T entity, IDbTransaction trans = null)
        {
            return repository.Delete(entity);
        }
        /// <summary>
        /// 同步物理刪除實體。
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual bool Delete(TKey id, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.Delete(id, conn, trans);
        }

        /// <summary>
        /// 異步物理刪除實體。
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual Task<bool> DeleteAsync(TKey id, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.DeleteAsync(id, conn, trans);
        }

        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="where">條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual async Task<bool> DeleteConnWhereAsync(string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.DeleteConnWhereAsync(where, conn , trans);
        }

        /// <summary>
        /// 異步物理刪除實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual Task<bool> DeleteAsync(T entity, IDbTransaction trans = null)
        {
            return repository.DeleteAsync(entity, trans);
        }

        /// <summary>
        /// 按主鍵批量刪除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual bool DeleteBatch(IList<dynamic> ids, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.DeleteBatch(ids, conn, trans);
        }


        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="where">條件</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual bool DeleteBatchWhere(string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.DeleteBatchWhere(where, conn, trans);
        }

        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="where">條件</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteBatchWhereAsync(string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.DeleteBatchWhereAsync(where, conn, trans);
        }
        /// <summary>
        /// 軟刪除信息，將DeleteMark設置為1-刪除，0-恢復刪除
        /// </summary>
        /// <param name="bl">true為不刪除，false刪除</param>
        /// <param name="id">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual bool DeleteSoft(bool bl, TKey id, string userId, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.DeleteSoft(bl, id, userId, conn, trans);
        }

        /// <summary>
        /// 異步軟刪除信息，將DeleteMark設置為1-刪除，0-恢復刪除
        /// </summary>
        /// <param name="bl">true為不刪除，false刪除</param>
        /// <param name="id">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteSoftAsync(bool bl, TKey id, string userId, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.DeleteSoftAsync(bl, id, userId, conn, trans);
        }
        /// <summary>
        /// 異步批量軟刪除信息，將DeleteMark設置為1-刪除，0-恢復刪除
        /// </summary>
        /// <param name="bl">true為不刪除，false刪除</param> c
        /// <param name="where">條件</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteSoftBatchAsync(bool bl, string where, string userId = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.DeleteSoftBatchAsync(bl, where, userId, conn, trans);
        }
        /// <summary>
        /// 同步查詢單個實體。
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <returns></returns>
        public virtual T Get(TKey id)
        {
            return repository.Get(id);
        }

        /// <summary>
        /// 同步查詢單個實體。
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <returns></returns>
        public virtual TODto GetOutDto(TKey id)
        {
            return repository.Get(id).MapTo<TODto>();
        }

        /// <summary>
        /// 同步查詢單個實體。
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual T GetWhere(string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.GetWhere(where, conn, trans);
        }
        /// <summary>
        /// 同步查詢單個實體。
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual TODto GetOutDtoWhere(string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.GetWhere(where, conn, trans).MapTo<TODto>();
        }

        /// <summary>
        /// 異步查詢單個實體。
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual async Task<T> GetWhereAsync(string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.GetWhereAsync(where, conn, trans);
        }

        /// <summary>
        /// 異步查詢單個實體。
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual async Task<TODto> GetOutDtoWhereAsync(string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            T info = await repository.GetWhereAsync(where, conn, trans);
            return info.MapTo<TODto>();
        }
        /// <summary>
        /// 根據查詢條件查詢前多少條數據
        /// </summary>
        /// <param name="top">多少條數據</param>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetListTopWhere(int top, string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.GetListTopWhere(top, where, conn, trans);
        }

        /// <summary>
        /// 根據查詢條件查詢前多少條數據
        /// </summary>
        /// <param name="top">多少條數據</param>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetListTopWhereAsync(int top, string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.GetListTopWhereAsync(top, where, conn, trans);
        }
        /// <summary>
        /// 同步查詢所有實體。
        /// </summary>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll(IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.GetAll(conn, trans);
        }

        /// <summary>
        /// 異步步查詢所有實體。
        /// </summary>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual Task<IEnumerable<T>> GetAllAsync(IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.GetAllAsync(conn, trans);
        }

        /// <summary>
        /// 異步查詢單個實體。
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(TKey id)
        {
            return await repository.GetAsync(id);
        }
        /// <summary>
        /// 異步查詢單個實體。
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <returns></returns>
        public virtual async Task<TODto> GetOutDtoAsync(TKey id)
        {
            T info = await repository.GetAsync(id);
            return info.MapTo<TODto>();
        }
        ///<summary>
        /// 根據查詢條件查詢數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>

        public virtual IEnumerable<T> GetListWhere(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.GetListWhere(where, conn, trans);
        }
        ///<summary>
        /// 異步根據查詢條件查詢數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetListWhereAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.GetListWhereAsync(where, conn, trans);
        }
        /// <summary>
        /// 同步新增實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual long Insert(T entity, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.Insert(entity, conn, trans);
        }

        /// <summary>
        /// 異步步新增實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual Task<long> InsertAsync(T entity, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.InsertAsync(entity, conn, trans);
        }
        /// <summary>
        /// 同步批量新增實體。
        /// </summary>
        /// <param name="entities">實體集合</param>
        /// <returns></returns>
        public virtual void Insert(List<T> entities, IDbConnection conn = null, IDbTransaction trans = null)
        {
            repository.Insert(entities, conn, trans);
        }

        /// <summary>
        /// 批輛新增
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        public virtual void InsertRange(List<T> entities, IDbConnection conn = null, IDbTransaction trans = null)
        {
            repository.InsertRange(entities, conn, trans);
        }
        /// <summary>
        /// 同步更新實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="id">主鍵ID</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual bool Update(T entity, TKey id, IDbTransaction trans = null)
        {
            return repository.Update(entity, id, trans);
        }


        /// <summary>
        /// 異步更新實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="id">主鍵ID</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual Task<bool> UpdateAsync(T entity, TKey id, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.UpdateAsync(entity, id, conn, trans);
        }


        /// <summary>
        /// 更新某一字段值,字段值字符類型
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="fieldValue">字段值字符類型</param>
        /// <param name="where">條件,為空更新所有內容</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual bool UpdateTableField(string strField, string fieldValue, string where, IDbConnection conn = null, IDbTransaction trans = null)
        {

            return repository.UpdateTableField(strField, fieldValue, where, conn, trans);
        }
        /// <summary>
        /// 更新某一字段值,字段值字符類型
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="fieldValue">字段值字符類型</param>
        /// <param name="where">條件,為空更新所有內容</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual async Task<bool> UpdateTableFieldAsync(string strField, string fieldValue, string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.UpdateTableFieldAsync(strField, fieldValue, where, conn, trans);
        }
        /// <summary>
        /// 更新某一字段值,字段值數字類型
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="fieldValue">字段值數字</param>
        /// <param name="where">條件,為空更新所有內容</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual bool UpdateTableField(string strField, int fieldValue, string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.UpdateTableField(strField, fieldValue, where, conn, trans);
        }

        /// <summary>
        /// 更新某一字段值,字段值數字類型
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="fieldValue">字段值數字</param>
        /// <param name="where">條件,為空更新所有內容</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual async Task<bool> UpdateTableFieldAsync(string strField, int fieldValue, string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.UpdateTableFieldAsync(strField, fieldValue, where, conn, trans);
        }
        /// <summary>
        /// 查詢軟刪除的數據，如果查詢條件為空，即查詢所有軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsDeleteMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.GetAllByIsDeleteMark(where, conn, trans);
        }

        /// <summary>
        /// 查詢未軟刪除的數據，如果查詢條件為空，即查詢所有未軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsNotDeleteMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.GetAllByIsNotDeleteMark(where, conn, trans);
        }

        /// <summary>
        /// 查詢有效的數據，如果查詢條件為空，即查詢所有有效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsEnabledMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.GetAllByIsEnabledMark(where, conn, trans);
        }

        /// <summary>
        /// 查詢無效的數據，如果查詢條件為空，即查詢所有無效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsNotEnabledMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.GetAllByIsNotEnabledMark(where, conn, trans);
        }
        /// <summary>
        /// 查詢未軟刪除且有效的數據，如果查詢條件為空，即查詢所有數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsNotDeleteAndEnabledMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.GetAllByIsNotDeleteAndEnabledMark(where, conn, trans);
        }

        /// <summary>
        /// 查詢軟刪除的數據，如果查詢條件為空，即查詢所有軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsDeleteMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.GetAllByIsDeleteMarkAsync(where, conn, trans);
        }

        /// <summary>
        /// 查詢未軟刪除的數據，如果查詢條件為空，即查詢所有未軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsNotDeleteMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.GetAllByIsNotDeleteMarkAsync(where, conn, trans);
        }

        /// <summary>
        /// 查詢有效的數據，如果查詢條件為空，即查詢所有有效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsEnabledMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.GetAllByIsEnabledMarkAsync(where, conn, trans);
        }

        /// <summary>
        /// 查詢無效的數據，如果查詢條件為空，即查詢所有無效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsNotEnabledMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.GetAllByIsNotEnabledMarkAsync(where, conn, trans);
        }

        /// <summary>
        /// 查詢未軟刪除且有效的數據，如果查詢條件為空，即查詢所有數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsNotDeleteAndEnabledMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.GetAllByIsNotDeleteAndEnabledMarkAsync(where, conn, trans);
        }


        /// <summary>
        /// 設置數據有效性，將EnabledMark設置為1:有效，0-為無效
        /// </summary>
        /// <param name="bl">true為有效，false無效</param>
        /// <param name="id">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual bool SetEnabledMark(bool bl, TKey id, string userId = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.SetEnabledMark(bl, id, userId, conn, trans);
        }

        /// <summary>
        /// 異步設置數據有效性，將EnabledMark設置為1:有效，0-為無效
        /// </summary>
        /// <param name="bl">true為有效，false無效</param>
        /// <param name="id">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual async Task<bool> SetEnabledMarkAsync(bool bl, TKey id, string userId = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.SetEnabledMarkAsync(bl, id, userId, conn, trans);
        }


        /// <summary>
        /// 異步按條件設置數據有效性，將EnabledMark設置為1:有效，0-為無效
        /// </summary>
        /// <param name="bl">true為有效，false無效</param>
        /// <param name="where">條件</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual async Task<bool> SetEnabledMarkByWhereAsync(bool bl, string where, string userId = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await this.repository.SetEnabledMarkByWhereAsync(bl, where, userId, conn, trans);
        }
        /// <summary>
        /// 異步按條件設置數據有效性，將EnabledMark設置為1:有效，0-為無效
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="where"></param>
        /// <param name="paramparameters"></param>
        /// <param name="userId"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual async Task<bool> SetEnabledMarkByWhereAsync(bool bl, string where, object paramparameters = null, string userId = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await this.repository.SetEnabledMarkByWhereAsync(bl, where, paramparameters, userId, conn, trans);
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns>指定對象的集合</returns>
        public virtual List<T> FindWithPager(string condition, PagerInfo info, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.FindWithPager(condition, info, conn, trans);
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="trans">事務對象</param>
        /// <returns>指定對象的集合</returns>
        public virtual List<T> FindWithPager(string condition, PagerInfo info, string fieldToSort, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.FindWithPager(condition, info, fieldToSort, conn, trans);
        }
        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <param name="trans">事務對象</param>
        /// <returns>指定對象的集合</returns>
        public virtual List<T> FindWithPager(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.FindWithPager(condition, info, fieldToSort, desc, conn, trans);
        }

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
        public virtual List<T> FindWithPagerSql(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.FindWithPagerSql(condition, info, fieldToSort, desc, conn, trans);
        }


        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">排序方式</param>
        /// <param name="trans">事務對象</param>
        /// <returns>指定對象的集合</returns>
        public virtual async Task<List<T>> FindWithPagerAsync(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.FindWithPagerAsync(condition, info, fieldToSort, desc, conn, trans);
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// 查詢條件變換時請重寫該方法。
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public virtual PageResult<TODto> FindWithPager(SearchInputDto<T> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege();
            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<T> list = repository.FindWithPager(where, pagerInfo, search.Sort, order, conn, trans);
            PageResult<TODto> pageResult = new PageResult<TODto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<TODto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }


        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// 查詢條件變換時請重寫該方法。
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public virtual async Task<PageResult<TODto>> FindWithPagerAsync(SearchInputDto<T> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege();
            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<T> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order, conn, trans);
            PageResult<TODto> pageResult = new PageResult<TODto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<TODto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="trans">事務對象</param>
        /// <returns>指定對象的集合</returns>
        public virtual async Task<List<T>> FindWithPagerAsync(string condition, PagerInfo info, string fieldToSort, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.FindWithPagerAsync(condition, info, fieldToSort, conn, trans);
        }


        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns>指定對象的集合</returns>
        public virtual async Task<List<T>> FindWithPagerAsync(string condition, PagerInfo info, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.FindWithPagerAsync(condition, info, conn, trans);
        }

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
        public virtual async Task<List<T>> FindWithPagerSqlAsync(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.FindWithPagerAsync(condition, info, fieldToSort, desc, conn, trans);
        }

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
        public virtual async Task<List<object>> FindWithPagerRelationUserAsync(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.FindWithPagerRelationUserAsync(condition, info, fieldToSort, desc, conn, trans);
        }

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
        public virtual List<object> FindWithPagerRelationUser(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.FindWithPagerRelationUser(condition, info, fieldToSort, desc, conn, trans);
        }
        /// <summary>
        /// 根據條件統計數據
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <param name="fieldName">統計字段名稱</param>
        /// <returns></returns>
        public virtual int GetCountByWhere(string condition, string fieldName = "*", IDbConnection conn = null, IDbTransaction trans = null)
        {
            return repository.GetCountByWhere(condition, fieldName, conn, trans);
        }

        /// <summary>
        /// 根據條件統計數據
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <param name="fieldName">統計字段名稱</param>
        /// <returns></returns>
        public virtual async Task<int> GetCountByWhereAsync(string condition, string fieldName = "*", IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.GetCountByWhereAsync(condition, fieldName, conn, trans);
        }

        /// <summary>
        /// 根據條件查詢獲取某個字段的最大值
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="where">條件</param>
        /// <param name="trans">事務</param>
        /// <returns>返回字段的最大值</returns>
        public virtual async Task<decimal> GetMaxValueByFieldAsync(string strField, string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.GetMaxValueByFieldAsync(strField, where, conn, trans);
        }

        /// <summary>
        /// 根據條件統計某個字段之和,sum(字段)
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="where">條件</param>
        /// <param name="trans">事務</param>
        /// <returns>返回字段求和后的值</returns>
        public virtual async Task<decimal> GetSumValueByFieldAsync(string strField, string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await repository.GetSumValueByFieldAsync(strField, where, conn, trans);
        }

        /// <summary>
        /// 多表操作--事務
        /// </summary>
        /// <param name="trans">事務</param>
        /// <param name="commandTimeout">超時</param>
        /// <returns></returns>
        public virtual async Task<Tuple<bool, string>> ExecuteTransactionAsync(List<Tuple<string, object>> trans, int? commandTimeout = null)
        {
            return await repository.ExecuteTransactionAsync(trans, commandTimeout);
        }
        /// <summary>
        /// 多表操作--事務
        /// </summary>
        /// <param name="trans">事務</param>
        /// <param name="commandTimeout">超時</param>
        /// <returns></returns>
        public virtual Tuple<bool, string> ExecuteTransaction(List<Tuple<string, object>> trans, int? commandTimeout = null)
        {
            return repository.ExecuteTransaction(trans, commandTimeout);
        }

        /// <summary>
        /// 獲取distinct後的集合
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetDistinctByFieldAsync(string fieldName, string where)
        {
            return repository.GetDistinctByFieldAsync(fieldName,where);
        }

        /// <summary>
        /// 獲取當前登錄用戶的數據訪問權限
        /// </summary>
        /// <param name="blDeptCondition">是否開啟，默認開啟</param>
        /// <returns></returns>
        protected virtual string GetDataPrivilege(bool blDeptCondition = true)
        {
            string where = "1=1";
            //開權限數據過濾
            if (blDeptCondition) {
                var identities = HttpContextHelper.HttpContext.User.Identities;
                var claimsIdentity = identities.First<ClaimsIdentity>();
                List<Claim> claimlist = claimsIdentity.Claims as List<Claim>;
                YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                var roleType = JsonSerializer.Deserialize<String[]>(yuebonCacheHelper.Get("User_RoleType_" + claimlist[0].Value).ToJson());
                if (roleType != null && roleType.Length > 0) {
                    var roleTypeString = roleType.Join(",");

                    if(!roleTypeString.Contains("3")) {
                        // 角色類型：個人角色
                        if (roleTypeString.Contains("1")) {
                            where += $" and CreatorUserId like '%{claimlist[0].Value}%'";
                        }
                        // 角色類型：部門角色
                        if (roleTypeString.Contains("2")) {
                            var list = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + claimlist[0].Value).ToJson());

                            //var listArr = string.Join(",", list.ToArray());
                            //var userIdList = repository.GetUserPermissionUser(string.Format(" 1=1 and DepartmentId in ('{0}')", listArr.Replace(",","','")));
                            if (list.Count() > 0) {
                                string likeScript = "";
                                for (int i = 0; i < list.Count(); i++) {
                                    likeScript += $" CreatorUserDeptId like '%{list[i]}%' or";
                                }
                                likeScript = likeScript.Substring(0, likeScript.Length - 2);
                                //string DataFilterCondition = String.Join(",", list.ToArray());
                                if (!string.IsNullOrEmpty(likeScript)) {
                                    //where += string.Format(" and ({0} or CreatorUserId like '%{1}%')", likeScript, claimlist[0].Value);
                                    where += string.Format(" and ( {0} )", likeScript, claimlist[0].Value);
                                }
                            } else {
                                where += string.Format(" and CreatorUserId='{0}'", claimlist[0].Value);
                            }
                        }
                    } 
                }

                //if (!claimlist[2].Value.Contains("administrators")) {
                //    if (claimlist[2].Value == "sales,") {
                //        where += $" and CreatorUserId like '%{claimlist[0].Value}%'";
                //    } else {
                //        var list = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get("User_PermissionDepts" + claimlist[0].Value).ToJson());

                //        //var listArr = string.Join(",", list.ToArray());
                //        //var userIdList = repository.GetUserPermissionUser(string.Format(" 1=1 and DepartmentId in ('{0}')", listArr.Replace(",","','")));
                //        if (list.Count() > 0) {
                //            string likeScript = "";
                //            for (int i = 0; i < list.Count(); i++) {
                //                likeScript += $" CreatorUserDeptId like '%{list[i]}%' or";
                //            }
                //            likeScript = likeScript.Substring(0, likeScript.Length - 2);
                //            //string DataFilterCondition = String.Join(",", list.ToArray());
                //            if (!string.IsNullOrEmpty(likeScript)) {
                //                where += string.Format(" and ({0} or CreatorUserId like '%{1}%')", likeScript, claimlist[0].Value);
                //            }
                //        } else {
                //            where += string.Format(" and CreatorUserId='{0}'", claimlist[0].Value);
                //        }
                //    }
                //}
            }
            return where;
        }

        /// <summary>
        /// 將西元年日期轉成民國年日期(字串)
        /// </summary>
        public string TranDateToROCDate(string ROCDate)
        {
            if (string.IsNullOrEmpty(ROCDate)) {
                return "";
            } else {
               return Convert.ToDateTime(ROCDate).AddYears(-1911).ToString("yyy-MM-dd");
            }
        }

        /// <summary>
        /// 將民國年日期轉成西元年日期(字串)
        /// </summary>
        public string TranROCDateToDate(string ROCDate)
        {
            if (string.IsNullOrEmpty(ROCDate)) {
                return "";
            } else {
                return Convert.ToDateTime(ROCDate).AddYears(+1911).ToString("yyy-MM-dd");
            }
        }
        #region IDisposable Support
        private bool disposedValue = false; // 要檢測冗余調用
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: 釋放托管狀態(托管對象)。
                }

                // TODO: 釋放未托管的資源(未托管的對象)并在以下內容中替代終結器。
                // TODO: 將大型字段設置為 null。

                disposedValue = true;
            }
        }

        // TODO: 僅當以上 Dispose(bool disposing) 擁有用于釋放未托管資源的代碼時才替代終結器。
        // ~BaseService() {
        //   // 請勿更改此代碼。將清理代碼放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        /// <summary>
        /// 添加此代碼以正確實現可處置模式
        /// </summary>
        void IDisposable.Dispose()
        {
            // 請勿更改此代碼。將清理代碼放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上內容中替代了終結器，則取消注釋以下行。
            // GC.SuppressFinalize(this);
        }


        #endregion
    }
}
