using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.IRepositories;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;

namespace Yuebon.Commons.Application
{

    /// <summary>
    /// 業務層基類，Service用于普通的數據庫操作
    /// </summary>
    /// <typeparam name="T">實體類型</typeparam>
    /// <typeparam name="TDto">實體類型</typeparam>
    /// <typeparam name="TService">Service類型</typeparam>
    /// <typeparam name="Tkey">主鍵類型</typeparam>
    public class BaseApp<T, TDto, TService, Tkey>
       where T : Entity
       where TDto : class
        where TService : IService<T, TDto, Tkey>
    {
        /// <summary>
        /// 用于普通的數據庫操作
        /// </summary>
        /// <value>The service.</value>
        protected IService<T, TDto, Tkey> service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_service"></param>
        public BaseApp(IService<T, TDto, Tkey> _service)
        {
            service = _service;
        }

        /// <summary>
        /// 同步物理刪除實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <returns></returns>
        public virtual bool Delete(T entity)
        {
            return service.Delete(entity, null);
        }
        /// <summary>
        /// 同步物理刪除實體。
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <returns></returns>
        public virtual bool Delete(Tkey id)
        {
            return service.Delete(id, null);
        }

        /// <summary>
        /// 異步物理刪除實體。
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <returns></returns>
        public virtual Task<bool> DeleteAsync(Tkey id)
        {
            return service.DeleteAsync(id, null);
        }


        /// <summary>
        /// 異步物理刪除實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <returns></returns>
        public virtual Task<bool> DeleteAsync(T entity)
        {
            return service.DeleteAsync(entity, null);
        }

        /// <summary>
        /// 按主鍵批量刪除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual bool DeleteBatch(IList<dynamic> ids)
        {
            return service.DeleteBatch(ids, null);
        }


        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="where">條件</param>
        /// <returns></returns>
        public virtual bool DeleteBatchWhere(string where)
        {
            return service.DeleteBatchWhere(where, null);
        }
        /// <summary>
        /// 軟刪除信息，將DeleteMark設置為1-刪除，0-恢復刪除
        /// </summary>
        /// <param name="bl">true為不刪除，false刪除</param>
        /// <param name="id">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <returns></returns>
        public virtual bool DeleteSoft(bool bl, Tkey id, string userId)
        {
            return service.DeleteSoft(bl, id, userId, null);
        }

        /// <summary>
        /// 異步軟刪除信息，將DeleteMark設置為1-刪除，0-恢復刪除
        /// </summary>
        /// <param name="bl">true為不刪除，false刪除</param>
        /// <param name="id">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <returns></returns>
        public virtual Task<bool> DeleteSoftAsync(bool bl, Tkey id, string userId)
        {
            return service.DeleteSoftAsync(bl, id, userId, null);
        }
        /// <summary>
        /// 同步查詢單個實體。
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <returns></returns>
        public virtual T Get(Tkey id)
        {
            return service.Get(id);
        }
        /// <summary>
        /// 同步查詢單個實體。
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual T GetWhere(string where)
        {
            return service.GetWhere(where, null);
        }

        /// <summary>
        /// 異步查詢單個實體。
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual async Task<T> GetWhereAsync(string where)
        {
            return await service.GetWhereAsync(where, null);
        }

        /// <summary>
        /// 根據查詢條件查詢前多少條數據
        /// </summary>
        /// <param name="top">多少條數據</param>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetListTopWhere(int top, string where = null)
        {
            return service.GetListTopWhere(top, where);
        }
        /// <summary>
        /// 同步查詢所有實體。
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return service.GetAll(null);
        }

        /// <summary>
        /// 異步步查詢所有實體。
        /// </summary>
        /// <returns></returns>
        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return service.GetAllAsync(null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<T> GetAsync(Tkey id)
        {
            return service.GetAsync(id);
        }
        ///<summary>
        /// 根據查詢條件查詢數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>

        public virtual IEnumerable<T> GetListWhere(string where = null)
        {
            return service.GetListWhere(where, null);
        }
        ///<summary>
        /// 異步根據查詢條件查詢數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetListWhereAsync(string where = null)
        {
            return await service.GetListWhereAsync(where, null);
        }
        /// <summary>
        /// 同步新增實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <returns></returns>
        public virtual long Insert(T entity)
        {
            return service.Insert(entity, null);
        }

        /// <summary>
        /// 異步步新增實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <returns></returns>
        public virtual Task<long> InsertAsync(T entity)
        {
            return service.InsertAsync(entity, null);
        }
        /// <summary>
        /// 同步更新實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="id">主鍵ID</param>
        /// <returns></returns>
        public virtual bool Update(T entity, Tkey id)
        {
            return service.Update(entity, id, null);
        }


        /// <summary>
        /// 異步更新實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="id">主鍵ID</param>
        /// <returns></returns>
        public virtual Task<bool> UpdateAsync(T entity, Tkey id)
        {
            return service.UpdateAsync(entity, id, null);
        }



        /// <summary>
        /// 更新某一字段值
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="where">條件,為空更新所有內容</param>
        /// <returns></returns>
        public virtual bool UpdateTableField(string strField, string fieldValue, string where)
        {

            return service.UpdateTableField(strField, fieldValue, where, null);
        }

        /// <summary>
        /// 查詢軟刪除的數據，如果查詢條件為空，即查詢所有軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsDeleteMark(string where = null)
        {
            return service.GetAllByIsDeleteMark(where, null);
        }

        /// <summary>
        /// 查詢未軟刪除的數據，如果查詢條件為空，即查詢所有未軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsNotDeleteMark(string where = null)
        {
            return service.GetAllByIsNotDeleteMark(where, null);
        }

        /// <summary>
        /// 查詢有效的數據，如果查詢條件為空，即查詢所有有效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsEnabledMark(string where = null)
        {
            return service.GetAllByIsEnabledMark(where, null);
        }

        /// <summary>
        /// 查詢無效的數據，如果查詢條件為空，即查詢所有無效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsNotEnabledMark(string where = null)
        {
            return service.GetAllByIsNotEnabledMark(where, null);
        }
        /// <summary>
        /// 查詢未軟刪除且有效的數據，如果查詢條件為空，即查詢所有數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsNotDeleteAndEnabledMark(string where = null)
        {
            return service.GetAllByIsNotDeleteAndEnabledMark(where, null);
        }

        /// <summary>
        /// 查詢軟刪除的數據，如果查詢條件為空，即查詢所有軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsDeleteMarkAsync(string where = null)
        {
            return await service.GetAllByIsDeleteMarkAsync(where, null);
        }

        /// <summary>
        /// 查詢未軟刪除的數據，如果查詢條件為空，即查詢所有未軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsNotDeleteMarkAsync(string where = null)
        {
            return await service.GetAllByIsNotDeleteMarkAsync(where, null);
        }

        /// <summary>
        /// 查詢有效的數據，如果查詢條件為空，即查詢所有有效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsEnabledMarkAsync(string where = null)
        {
            return await service.GetAllByIsEnabledMarkAsync(where, null);
        }

        /// <summary>
        /// 查詢無效的數據，如果查詢條件為空，即查詢所有無效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsNotEnabledMarkAsync(string where = null)
        {
            return await service.GetAllByIsNotEnabledMarkAsync(where, null);
        }

        /// <summary>
        /// 查詢未軟刪除且有效的數據，如果查詢條件為空，即查詢所有數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsNotDeleteAndEnabledMarkAsync(string where = null)
        {
            return await service.GetAllByIsNotDeleteAndEnabledMarkAsync(where, null);
        }


        /// <summary>
        /// 設置數據有效性，將EnabledMark設置為1:有效，0-為無效
        /// </summary>
        /// <param name="bl">true為有效，false無效</param>
        /// <param name="id">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <returns></returns>
        public virtual bool SetEnabledMark(bool bl, Tkey id, string userId = null)
        {
            return service.SetEnabledMark(bl, id, userId, null);
        }

        /// <summary>
        /// 異步設置數據有效性，將EnabledMark設置為1:有效，0-為無效
        /// </summary>
        /// <param name="bl">true為有效，false無效</param>
        /// <param name="id">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <returns></returns>
        public virtual async Task<bool> SetEnabledMarkAsync(bool bl, Tkey id, string userId = null)
        {
            return await service.SetEnabledMarkAsync(bl, id, userId, null);
        }


        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <returns>指定對象的集合</returns>
        public virtual List<T> FindWithPager(string condition, PagerInfo info, IDbConnection conn = null, IDbTransaction trans = null)
        {
            return service.FindWithPager(condition, info, conn, trans);
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <returns>指定對象的集合</returns>
        public virtual List<T> FindWithPager(string condition, PagerInfo info, string fieldToSort)
        {
            return service.FindWithPager(condition, info, fieldToSort, null);
        }
        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns>指定對象的集合</returns>
        public virtual List<T> FindWithPager(string condition, PagerInfo info, string fieldToSort, bool desc)
        {
            return service.FindWithPager(condition, info, fieldToSort, desc, null);
        }

        /// <summary>
        /// 分頁查詢，自行封裝sql語句
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <param name="info">分頁信息</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">排序方式 true為desc，false為asc</param>
        /// <returns></returns>
        public virtual List<T> FindWithPagerSql(string condition, PagerInfo info, string fieldToSort, bool desc)
        {
            return service.FindWithPagerSql(condition, info, fieldToSort, desc, null);
        }

        /// <summary>
        /// 分頁查詢包含用戶信息
        /// 查詢主表別名為t1,用戶表別名為t2，在查詢字段需要注意使用t1.xxx格式，xx表示主表字段
        /// 用戶信息主要有用戶帳號：Account、昵稱：NickName、真實姓名：RealName、頭像：HeadIcon、手機號：MobilePhone
        /// 輸出對象請在Dtos中進行自行封裝，不能是使用實體Model類
        /// </summary>
        /// <param name="condition">查詢條件字段需要加表別名</param>
        /// <param name="info">分頁信息</param>
        /// <param name="fieldToSort">排序字段，也需要加表別名</param>
        /// <param name="desc">排序方式</param>
        /// <returns></returns>
        public virtual List<object> FindWithPagerRelationUser(string condition, PagerInfo info, string fieldToSort, bool desc)
        {
            return service.FindWithPagerRelationUser(condition, info, fieldToSort, desc, null);
        }
        /// <summary>
        /// 根據條件統計數據
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <returns></returns>
        public virtual int GetCountByWhere(string condition)
        {
            return service.GetCountByWhere(condition);
        }
    }
}
