using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yuebon.Commons.Core.Dapper;
using Yuebon.Commons.Core.DataManager;
using Yuebon.Commons.DataManager;
using Yuebon.Commons.DbContextCore;
using Yuebon.Commons.DependencyInjection;
using Yuebon.Commons.Enums;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.IRepositories;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using static Dapper.SqlMapper;

namespace Yuebon.Commons.Repositories
{
    /// <summary>
    /// 泛型倉儲，實現泛型倉儲接口
    /// </summary>
    /// <typeparam name="T">實體類型</typeparam>
    /// <typeparam name="TKey">實體主鍵類型</typeparam>
    public abstract class BaseRepository<T, TKey> : IRepository<T, TKey>, ITransientDependency
        where T : Entity
    {
        #region 構造函數及基本配置
        /// <summary>
        ///  EF DBContext
        /// </summary>
        private IDbContextCore _dbContext;
        private IDbContextFactory _dbContextFactory;
        /// <summary>
        /// 
        /// </summary>
        protected DbSet<T> DbSet => DbContext.GetDbSet<T>();
        /// <summary>
        /// 獲取訪問數據庫配置
        /// </summary>
        protected DbConnectionOptions dbConnectionOptions = DBServerProvider.GeDbConnectionOptions<T>();
        /// <summary>
        /// 需要初始化的對象表名
        /// </summary>
        protected string tableName = typeof(T).GetCustomAttribute<TableAttribute>(false)?.Name;
        /// <summary>
        /// 數據庫參數化訪問的占位符
        /// </summary>
        protected string parameterPrefix = "@";
        /// <summary>
        /// 防止和保留字、關鍵字同名的字段格式，如[value]
        /// </summary>
        protected string safeFieldFormat = "[{0}]";
        /// <summary>
        /// 數據庫的主鍵字段名,若主鍵不是Id請重載BaseRepository設置
        /// </summary>
        protected string primaryKey = "Id";
        /// <summary>
        /// 排序字段
        /// </summary>
        protected string sortField;
        /// <summary>
        /// 是否為降序
        /// </summary>
        protected bool isDescending = true;
        /// <summary>
        /// 選擇的字段，默認為所有(*) 
        /// </summary>
        protected string selectedFields = " * ";
        /// <summary>
        /// 是否開啟多租戶
        /// </summary>
        protected bool isMultiTenant = false;


        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortField
        {
            get
            {
                return sortField;
            }
            set
            {
                sortField = value;
            }
        }

        /// <summary>
        /// 數據庫訪問對象的外鍵約束
        /// </summary>
        public string PrimaryKey
        {
            get
            {
                return primaryKey;
            }
        }


        /// <summary>
        /// 構造方法
        /// </summary>
        public BaseRepository()
        {
        }

        /// <summary>
        /// 構造方法，注入上下文
        /// </summary>
        /// <param name="dbContext">上下文</param>
        public BaseRepository(IDbContextCore dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext;
            _dbContext.EnsureCreated();
        }

        /// <summary>
        /// 構造方法，注入上下文
        /// </summary>
        /// <param name="dbContextFactory">上下文</param>
        public BaseRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        #endregion

        #region Dapper 操作


        /// <summary>
        /// 用Dapper原生方法操作數據，支持讀寫操作
        /// </summary>
        public IDbConnection DapperConn
        {
            get { return new DapperDbContext().GetConnection<T>(); }
        }

        /// <summary>
        /// 用Dapper原生方法，僅用于只讀數據庫
        /// </summary>
        public IDbConnection DapperConnRead
        {
            get { return new DapperDbContext().GetConnection<T>(false); }
        }

        #region 查詢獲得對象和列表
        /// <summary>
        /// 根據id獲取一個對象
        /// </summary>
        /// <param name="primaryKey">主鍵</param>
        /// <returns></returns>
        public virtual T Get(TKey primaryKey, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (conn != null) {
                return conn.Get<T>(primaryKey, trans);
            } else {
                return DapperConnRead.Get<T>(primaryKey, trans);
            }
        }
        /// <summary>
        /// 異步根據id獲取一個對象
        /// </summary>
        /// <param name="primaryKey">主鍵</param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(TKey primaryKey, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (conn != null) {
                return await conn.GetAsync<T>(primaryKey, trans);
            } else {
                return await DapperConnRead.GetAsync<T>(primaryKey, trans);
            }
        }
        /// <summary>
        /// 根據條件獲取一個對象
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual T GetWhere(string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(where)) {
                where = "1=1";
            }
            string sql = $"select * from { tableName} ";
            sql += " where " + where;
            if (conn != null) {
                return conn.QueryFirstOrDefault<T>(sql, transaction:trans);
            } else {
                return DapperConnRead.QueryFirstOrDefault<T>(sql, transaction:trans);
            }
        }

        /// <summary>
        /// 根據條件異步獲取一個對象
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual async Task<T> GetWhereAsync(string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(where)) {
                where = "1=1";
            }
            string sql = $"select * from { tableName} ";
            sql += " where " + where;

            if (conn != null) {
                return await conn.QueryFirstOrDefaultAsync<T>(sql, transaction:trans);
            } else {
                return await DapperConnRead.QueryFirstOrDefaultAsync<T>(sql, transaction:trans);
            }
            //return await DapperConnRead.QueryFirstOrDefaultAsync<T>(sql);
        }

        /// <summary>
        /// 獲取所有數據，謹慎使用
        /// </summary>
        /// <param name="trans">事務</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll(IDbConnection conn = null, IDbTransaction trans = null)
        {
            return GetListWhere(null, conn, trans);
        }
        /// <summary>
        /// 獲取所有數據，謹慎使用
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync(IDbConnection conn = null, IDbTransaction trans = null)
        {
            return await GetListWhereAsync(null, conn, trans);
        }


        /// <summary>
        /// 根據查詢條件獲取數據集合
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetListWhere(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            string sql = $"select {selectedFields} from { tableName} ";
            if (!string.IsNullOrWhiteSpace(where)) {
                sql += " where " + where;
            }

            if (conn != null) {
                return conn.Query<T>(sql, trans);
            } else {
                return DapperConnRead.Query<T>(sql, trans);
            }
            //return DapperConnRead.Query<T>(sql, trans);
        }

        /// <summary>
        /// 根據查詢條件獲取數據集合
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetListWhereAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            string sql = $"select {selectedFields}  from { tableName} ";
            if (!string.IsNullOrWhiteSpace(where)) {
                sql += " where " + where;
            }
            if (conn != null) {
                return await conn.QueryAsync<T>(sql, transaction:trans);
            } else {
                return await DapperConnRead.QueryAsync<T>(sql, trans);
            }
            //return await DapperConnRead.QueryAsync<T>(sql, trans);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<string> GetUserPermissionUser(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            string sql = $"select Id  from Sys_User ";
            if (!string.IsNullOrWhiteSpace(where)) {
                sql += " where " + where;
            }
            if (conn != null) {
                return conn.Query<string>(sql, trans);
            } else {
                return DapperConnRead.Query<string>(sql, trans);
            }
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
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }

            string sql = $"select top {top} {selectedFields} from " + tableName; ;
            if (dbConnectionOptions.DatabaseType == DatabaseType.SqlServer) {
                if (!string.IsNullOrWhiteSpace(where)) {
                    sql += " where " + where;
                }
            } else if (dbConnectionOptions.DatabaseType == DatabaseType.MySql) {
                sql = $"select {selectedFields} from " + tableName;

                if (!string.IsNullOrWhiteSpace(where)) {
                    sql += " where " + where;
                }
                sql += $"  LIMIT 0,{top}; ";
            }

            if (conn != null) {
                return conn.Query<T>(sql, trans);
            } else {
                return DapperConnRead.Query<T>(sql, trans);
            }
            //return DapperConnRead.Query<T>(sql, trans);
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
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            string sql = $"select top {top} {selectedFields} from " + tableName;
            if (dbConnectionOptions.DatabaseType == DatabaseType.SqlServer) {
                if (!string.IsNullOrWhiteSpace(where)) {
                    sql += " where " + where;
                }
            } else if (dbConnectionOptions.DatabaseType == DatabaseType.MySql) {
                sql = $"select {selectedFields} from " + tableName;

                if (!string.IsNullOrWhiteSpace(where)) {
                    sql += " where " + where;
                }
                sql += $"  LIMIT 0,{top}; ";
            } else if (dbConnectionOptions.DatabaseType == DatabaseType.Npgsql) {
                sql = $"select {selectedFields} from " + tableName;

                if (!string.IsNullOrWhiteSpace(where)) {
                    sql += " where " + where;
                }
                sql += $"  LIMIT {top}; ";
            }
            if (conn != null) {
                return await conn.QueryAsync<T>(sql, trans);
            } else {
                return await DapperConnRead.QueryAsync<T>(sql, trans);
            }
            //return await DapperConnRead.QueryAsync<T>(sql, trans);
        }
        /// <summary>
        /// 查詢軟刪除的數據，如果查詢條件為空，即查詢所有軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsDeleteMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            string sqlWhere = " DeleteMark='1' ";
            if (!string.IsNullOrWhiteSpace(where)) {
                sqlWhere += " and " + where;
            }

            return GetListWhere(sqlWhere, conn, trans);
            //return GetListWhere(sqlWhere,trans);
        }

        /// <summary>
        /// 查詢未軟刪除的數據，如果查詢條件為空，即查詢所有未軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsNotDeleteMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            string sqlWhere = " DeleteMark=0 ";
            if (!string.IsNullOrWhiteSpace(where)) {
                sqlWhere += " and " + where;
            }
            return GetListWhere(sqlWhere, conn, trans);
        }

        /// <summary>
        /// 查詢有效的數據，如果查詢條件為空，即查詢所有有效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsEnabledMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {

            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            string sqlWhere = " EnabledMark='1' ";
            if (!string.IsNullOrWhiteSpace(where)) {
                sqlWhere += " and " + where;
            }
            return GetListWhere(sqlWhere, conn, trans);
        }

        /// <summary>
        /// 查詢無效的數據，如果查詢條件為空，即查詢所有無效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsNotEnabledMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {

            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            string sqlWhere = " EnabledMark=0 ";
            if (!string.IsNullOrWhiteSpace(where)) {
                sqlWhere += " and " + where;
            }
            return GetListWhere(sqlWhere, conn, trans);
        }
        /// <summary>
        /// 查詢未軟刪除且有效的數據，如果查詢條件為空，即查詢所有數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllByIsNotDeleteAndEnabledMark(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {

            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            string sqlWhere = " DeleteMark='0' and EnabledMark='1' ";
            if (!string.IsNullOrWhiteSpace(where)) {
                sqlWhere += " and " + where;
            }
            return GetListWhere(sqlWhere, conn, trans);
        }

        /// <summary>
        /// 查詢軟刪除的數據，如果查詢條件為空，即查詢所有軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsDeleteMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {

            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            string sqlWhere = " DeleteMark='1'";
            if (!string.IsNullOrWhiteSpace(where)) {
                sqlWhere += " and " + where;
            }
            return await GetListWhereAsync(sqlWhere, conn, trans);
        }

        /// <summary>
        /// 查詢未軟刪除的數據，如果查詢條件為空，即查詢所有未軟刪除的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsNotDeleteMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {

            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }

            string sqlWhere = " DeleteMark=0 ";
            if (!string.IsNullOrWhiteSpace(where)) {
                sqlWhere += " and " + where;
            }
            return await GetListWhereAsync(sqlWhere, conn, trans);
        }

        /// <summary>
        /// 查詢有效的數據，如果查詢條件為空，即查詢所有有效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsEnabledMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {

            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }

            string sqlWhere = " EnabledMark='1' ";
            if (!string.IsNullOrWhiteSpace(where)) {
                sqlWhere += " and " + where;
            }
            return await GetListWhereAsync(sqlWhere, conn, trans);
        }

        /// <summary>
        /// 查詢無效的數據，如果查詢條件為空，即查詢所有無效的數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsNotEnabledMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {

            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            string sqlWhere = " EnabledMark=0";
            if (!string.IsNullOrWhiteSpace(where)) {
                sqlWhere += " and " + where;
            }
            return await GetListWhereAsync(sqlWhere, conn, trans);
        }
        /// <summary>
        /// 查詢未軟刪除且有效的數據，如果查詢條件為空，即查詢所有數據
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllByIsNotDeleteAndEnabledMarkAsync(string where = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            string sqlWhere = " DeleteMark='0' and EnabledMark='1'";
            if (!string.IsNullOrWhiteSpace(where)) {
                sqlWhere += " and " + where;
            }
            return await GetListWhereAsync(sqlWhere, conn, trans);
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
            return FindWithPager(condition, info, this.SortField, this.isDescending, conn, trans);
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
            return FindWithPager(condition, info, fieldToSort, this.isDescending, conn, trans);
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
            return await FindWithPagerAsync(condition, info, fieldToSort, this.isDescending, conn, trans);
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
            return await FindWithPagerAsync(condition, info, this.SortField, conn, trans);
        }
        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">排序方式 true為desc，false為asc</param>
        /// <param name="trans">事務對象</param>
        /// <returns>指定對象的集合</returns>
        public virtual List<T> FindWithPager(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null)
        {
            List<T> list = new List<T>();

            if (HasInjectionData(condition)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", condition));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(condition)) {
                condition = "1=1";
            }
            PagerHelper pagerHelper = new PagerHelper(this.tableName, this.selectedFields, fieldToSort, info.PageSize, info.CurrenetPageIndex, desc, condition);

            string pageSql = pagerHelper.GetPagingSql(true, dbConnectionOptions.DatabaseType);
            pageSql += ";" + pagerHelper.GetPagingSql(false, dbConnectionOptions.DatabaseType);

            GridReader reader = null;
            if (conn != null) {
                reader = conn.QueryMultiple(pageSql, trans);
            } else {
                reader = DapperConnRead.QueryMultiple(pageSql, trans);
            }
            //var reader = DapperConnRead.QueryMultiple(pageSql);
            info.RecordCount = reader.ReadFirst<int>();
            list = reader.Read<T>().AsList();
            return list;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="condition">查詢的條件</param>
        /// <param name="info">分頁實體</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">排序方式 true為desc，false為asc</param>
        /// <param name="trans">事務對象</param>
        /// <returns>指定對象的集合</returns>
        public virtual async Task<List<T>> FindWithPagerAsync(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null)
        {

            List<T> list = new List<T>();

            if (HasInjectionData(condition)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", condition));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(condition)) {
                condition = "1=1";
            }

            PagerHelper pagerHelper = new PagerHelper(this.tableName, this.selectedFields, fieldToSort, info.PageSize, info.CurrenetPageIndex, desc, condition);

            string pageSql = pagerHelper.GetPagingSql(true, dbConnectionOptions.DatabaseType);
            pageSql += ";" + pagerHelper.GetPagingSql(false, dbConnectionOptions.DatabaseType);

            GridReader reader = null;
            if (conn != null) {
                reader = await conn.QueryMultipleAsync(pageSql, trans);
            } else {
                reader = await DapperConnRead.QueryMultipleAsync(pageSql, trans);
            }
            //var reader = await DapperConnRead.QueryMultipleAsync(pageSql);
            info.RecordCount = reader.ReadFirst<int>();
            list = reader.Read<T>().AsList();
            return list;
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
            List<T> list = new List<T>();
            if (HasInjectionData(condition)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", condition));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(condition)) {
                condition = "1=1";
            }
            StringBuilder sb = new StringBuilder();
            int startRows = (info.CurrenetPageIndex - 1) * info.PageSize + 1;//起始記錄
            int endNum = info.CurrenetPageIndex * info.PageSize;//結束記錄
            string strOrder = string.Format(" {0} {1}", fieldToSort, desc ? "DESC" : "ASC");
            sb.AppendFormat("SELECT count(*) as RecordCount FROM (select {0} FROM {1} where {2})  AS main_temp;", primaryKey, tableName, condition);
            sb.AppendFormat("SELECT * FROM ( SELECT ROW_NUMBER() OVER (order by {0}) AS rows ,{1} FROM {2} where {3}) AS main_temp where rows BETWEEN {4} and {5}", strOrder, selectedFields, tableName, condition, startRows, endNum);
            GridReader reader = null;
            if (conn != null) {
                reader = conn.QueryMultiple(sb.ToString(), trans);
            } else {
                reader = DapperConnRead.QueryMultiple(sb.ToString(), trans);
            }
            //var reader = DapperConnRead.QueryMultiple(sb.ToString());
            info.RecordCount = reader.ReadFirst<int>();
            list = reader.Read<T>().AsList();
            return list;
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
            if (HasInjectionData(condition)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", condition));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(condition)) {
                condition = "1=1";
            }
            StringBuilder sb = new StringBuilder();
            int startRows = (info.CurrenetPageIndex - 1) * info.PageSize + 1;//起始記錄
            int endNum = info.CurrenetPageIndex * info.PageSize;//結束記錄
            string strOrder = string.Format(" {0} {1}", fieldToSort, desc ? "DESC" : "ASC");
            sb.AppendFormat("SELECT count(*) as RecordCount FROM (select {0} FROM {1} where {2})  AS main_temp;", primaryKey, tableName, condition);
            sb.AppendFormat("SELECT * FROM ( SELECT ROW_NUMBER() OVER (order by {0}) AS rows ,{1} FROM {2} where {3}) AS main_temp where rows BETWEEN {4} and {5}", strOrder, selectedFields, tableName, condition, startRows, endNum);
            GridReader reader = null;
            if (conn != null) {
                reader = await conn.QueryMultipleAsync(sb.ToString(), trans);
            } else {
                reader = await DapperConnRead.QueryMultipleAsync(sb.ToString(), trans);
            }
            //var reader = await DapperConnRead.QueryMultipleAsync(sb.ToString());
            info.RecordCount = reader.ReadFirst<int>();
            List<T> list = reader.Read<T>().AsList();
            return list;
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

            if (HasInjectionData(condition)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", condition));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(condition)) {
                condition = "1=1";
            }
            StringBuilder sb = new StringBuilder();
            int startRows = (info.CurrenetPageIndex - 1) * info.PageSize + 1;//起始記錄
            int endNum = info.CurrenetPageIndex * info.PageSize;//結束記錄
            string strOrder = string.Format(" {0} {1}", fieldToSort, desc ? "DESC" : "ASC");
            sb.AppendFormat("SELECT count(*) as RecordCount FROM (select t1.{0} FROM {1} t1 inner join Sys_User t2 on t1.CreatorUserId = t2.Id where {2})  AS main_temp;", primaryKey, tableName, condition);
            sb.AppendFormat("SELECT * FROM (SELECT ROW_NUMBER() OVER (order by  {0}) AS rows ,t1.{1},t2.Account as Account,t2.NickName as NickName,t2.RealName as RealName,t2.HeadIcon as HeadIcon ,t2.MobilePhone as MobilePhone  FROM {2} t1 inner join Sys_User t2 on t1.CreatorUserId = t2.Id " +
                "where {3}) AS main_temp where rows BETWEEN {4} and {5}", strOrder, selectedFields, tableName, condition, startRows, endNum);

            GridReader reader = null;
            if (conn != null) {
                reader = conn.QueryMultiple(sb.ToString(), trans);
            } else {
                reader = DapperConnRead.QueryMultiple(sb.ToString(), trans);
            }
            //var reader = DapperConnRead.QueryMultiple(sb.ToString());
            info.RecordCount = reader.ReadFirst<int>();
            List<object> list = reader.Read<object>().AsList();
            return list;
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
            if (HasInjectionData(condition)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", condition));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(condition)) {
                condition = "1=1";
            }
            StringBuilder sb = new StringBuilder();
            int startRows = (info.CurrenetPageIndex - 1) * info.PageSize + 1;//起始記錄
            int endNum = info.CurrenetPageIndex * info.PageSize;//結束記錄
            string strOrder = string.Format(" {0} {1}", fieldToSort, desc ? "DESC" : "ASC");
            sb.AppendFormat("SELECT count(*) as RecordCount FROM (select t1.{0} FROM {1} t1 inner join Sys_User t2 on t1.CreatorUserId = t2.Id where {2})  AS main_temp;", primaryKey, tableName, condition);
            sb.AppendFormat("SELECT * FROM (SELECT ROW_NUMBER() OVER (order by  {0}) AS rows ,t1.{1},t2.Account as Account,t2.NickName as NickName,t2.RealName as RealName,t2.HeadIcon as HeadIcon ,t2.MobilePhone as MobilePhone  FROM {2} t1 inner join Sys_User t2 on t1.CreatorUserId = t2.Id " +
                "where {3}) AS main_temp where rows BETWEEN {4} and {5}", strOrder, selectedFields, tableName, condition, startRows, endNum);
            GridReader reader = null;
            if (conn != null) {
                reader = await conn.QueryMultipleAsync(sb.ToString(), trans);
            } else {
                reader = await DapperConnRead.QueryMultipleAsync(sb.ToString(), trans);
            }
            //var reader = await DapperConnRead.QueryMultipleAsync(sb.ToString());
            info.RecordCount = reader.ReadFirst<int>();
            List<object> list = reader.Read<object>().AsList();
            return list;
        }
        /// <summary>
        /// 根據條件統計數據
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <param name="fieldName">統計字段名稱</param>
        /// <returns></returns>
        public virtual int GetCountByWhere(string condition, string fieldName = "*", IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(condition)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", condition));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(condition)) {
                condition = "1=1";
            }
            string sql = $"select count({fieldName}) from {tableName}  where ";
            if (!string.IsNullOrWhiteSpace(condition)) {
                sql = sql + condition;
            }
            if (conn != null) {
                return conn.Query<int>(sql, trans).FirstOrDefault();
            } else {
                return DapperConnRead.Query<int>(sql, trans).FirstOrDefault();
            }
            //return DapperConnRead.Query<int>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 根據條件統計數據
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <param name="fieldName">統計字段名稱</param>
        /// <returns></returns>
        public virtual async Task<int> GetCountByWhereAsync(string condition, string fieldName = "*", IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(condition)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", condition));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(condition)) {
                condition = "1=1";
            }
            string sql = $"select count({fieldName}) from {tableName}  where ";
            if (!string.IsNullOrWhiteSpace(condition)) {
                sql = sql + condition;
            }
            if (conn != null) {
                return await conn.QueryFirstAsync<int>(sql, trans);
            } else {
                return await DapperConnRead.QueryFirstAsync<int>(sql, trans);
            }
            //return await DapperConnRead.QueryFirstAsync<int>(sql);
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
            string sql = $"select isnull(MAX({strField}),0) as maxVaule from {tableName} ";
            if (dbConnectionOptions.DatabaseType == DatabaseType.MySql) {
                sql = $"select if(isnull(MAX({strField})),0,MAX({strField})) as maxVaule from {tableName} ";
            }
            if (!string.IsNullOrEmpty(where)) {
                sql += " where " + where;
            }

            if (conn != null) {
                return await conn.QueryFirstAsync<decimal>(sql, trans);
            } else {
                return await DapperConnRead.QueryFirstAsync<decimal>(sql, trans);
            }
            //return await DapperConnRead.QueryFirstAsync<decimal>(sql);
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
            string sql = $"select isnull(sum({strField}),0) as sumVaule from {tableName} ";
            if (dbConnectionOptions.DatabaseType == DatabaseType.MySql) {
                sql = $"select if(isnull(sum({strField})),0,sum({strField})) as sumVaule from {tableName} ";
            }
            if (!string.IsNullOrEmpty(where)) {
                sql += " where " + where;
            }

            if (conn != null) {
                return await conn.QueryFirstAsync<decimal>(sql, trans);
            } else {
                return await DapperConnRead.QueryFirstAsync<decimal>(sql, trans);
            }
            //return await DapperConnRead.QueryFirstAsync<decimal>(sql);
        }

        /// <summary>
        /// 獲取distinct後的集合
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetDistinctByFieldAsync(string fieldName, string where)
        {
            string sql = $"SELECT DISTINCT {fieldName} FROM {tableName} WHERE {where}";
            return await DapperConnRead.QueryAsync<T>(sql);
        }
        #endregion

        #region 新增、修改和刪除

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual long Insert(T entity, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (entity.KeyIsNull()) {
                entity.GenerateDefaultKeyVal();
            }
            if (conn != null) {
                return conn.Insert<T>(entity, trans);
            } else {
                return DapperConn.Insert<T>(entity, trans);
            }
            //return DapperConn.Insert<T>(entity);
        }


        /// <summary>
        /// 異步新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual async Task<long> InsertAsync(T entity, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (entity.KeyIsNull()) {
                entity.GenerateDefaultKeyVal();
            }
            if (conn != null) {
                return await conn.InsertAsync<T>(entity, transaction:trans);
            } else {
                return await DapperConn.InsertAsync<T>(entity, trans);
            }
            //return await DapperConn.InsertAsync<T>(entity);
        }

        /// <summary>
        /// 批量插入數據
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual void Insert(List<T> entities, IDbConnection conn = null, IDbTransaction trans = null)
        {
            DbContext.BulkInsert<T>(entities, "", conn, trans);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="primaryKey">主鍵</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual bool Update(T entity, TKey primaryKey, IDbTransaction trans = null)
        {
            return DbContext.Edit<T>(entity) > 0;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual bool Update(T entity, IDbTransaction trans = null)
        {
            return DbContext.Edit(entity) > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="primaryKey"></param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual async Task<bool> UpdateAsync(T entity, TKey primaryKey, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (conn != null) {
                return await conn.UpdateAsync<T>(entity, transaction:trans);
            } else {
                return await DapperConn.UpdateAsync<T>(entity, trans);
            }
            //return await DapperConn.UpdateAsync<T>(entity, trans);
        }

        /// <summary>
        /// 同步物理刪除實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <returns></returns>
        public virtual bool Delete(T entity)
        {
            DbContext.GetDbSet<T>().Remove(entity);
            return DbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 異步物理刪除實體。
        /// </summary>
        /// <param name="entity">實體</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(T entity, IDbTransaction trans = null)
        {
            DbContext.GetDbSet<T>().Remove(entity);
            return await DbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="where">條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual bool DeleteConnWhereAsync(string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(where)) {
                where = "1=1";
            }
            string sql = $"delete from {tableName} where " + where;
            if (conn != null) {
                return  conn.Execute(sql, transaction:trans) > 0;
            } else {
                return  DapperConn.Execute(sql, transaction: trans) > 0;
            }
        }


        /// <summary>
        /// 物理刪除信息
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual bool Delete(TKey primaryKey, IDbConnection conn = null, IDbTransaction trans = null)
        {
            var param = new List<Tuple<string, object>>();
            string sql = $"delete from {tableName} where " + PrimaryKey + "=@PrimaryKey";
            Tuple<string, object> tupel = new Tuple<string, object>(sql, new { @PrimaryKey = primaryKey });

            param.Add(tupel);
            Tuple<bool, string> result = ExecuteTransaction(param);
            return result.Item1;
        }
        /// <summary>
        /// 異步物理刪除信息
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual async Task<bool> DeleteAsync(TKey primaryKey, IDbConnection conn = null, IDbTransaction trans = null)
        {
            var param = new List<Tuple<string, object>>();
            string sql = $"delete from {tableName} where " + PrimaryKey + "=@PrimaryKey";
            Tuple<string, object> tupel = new Tuple<string, object>(sql, new { @PrimaryKey = primaryKey });
            param.Add(tupel);
            Tuple<bool, string> result = await ExecuteTransactionAsync(param);
            return result.Item1;
        }

        /// <summary>
        /// 按主鍵批量刪除
        /// </summary>
        /// <param name="ids">主鍵Id List集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual bool DeleteBatch(IList<dynamic> ids, IDbConnection conn = null, IDbTransaction trans = null)
        {
            var param = new List<Tuple<string, object>>();
            string sql = $"delete from {tableName} where PrimaryKey in (@PrimaryKey)";
            Tuple<string, object> tupel = new Tuple<string, object>(sql, new { @PrimaryKey = ids });

            param.Add(tupel);
            Tuple<bool, string> result = ExecuteTransaction(param);
            return result.Item1;
        }
        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="where">條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual bool DeleteBatchWhere(string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(where)) {
                where = "1=1";
            }
            var param = new List<Tuple<string, object>>();
            string sql = $"delete from {tableName} where " + where;
            Tuple<string, object> tupel = new Tuple<string, object>(sql, null);

            param.Add(tupel);
            Tuple<bool, string> result = ExecuteTransaction(param);
            return result.Item1;
        }
        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="where">條件</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual async Task<bool> DeleteBatchWhereAsync(string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(where)) {
                where = "1=1";
            }
            var param = new List<Tuple<string, object>>();
            string sql = $"delete from {tableName} where " + where;
            Tuple<string, object> tupel = new Tuple<string, object>(sql, null);
            param.Add(tupel);
            Tuple<bool, string> result = await ExecuteTransactionAsync(param);
            return result.Item1;
        }

        /// <summary>
        /// 根據指定對象的ID和用戶ID,從數據庫中刪除指定對象(用于記錄人員的操作日志）
        /// </summary>
        /// <param name="primaryKey">指定對象的ID</param>
        /// <param name="userId">用戶ID</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual bool DeleteByUser(TKey primaryKey, string userId, IDbConnection conn = null, IDbTransaction trans = null)
        {
            var param = new List<Tuple<string, object>>();
            string sql = $"delete from {tableName} where " + PrimaryKey + " = @PrimaryKey";
            Tuple<string, object> tupel = new Tuple<string, object>(sql, new { @PrimaryKey = primaryKey });
            param.Add(tupel);
            Tuple<bool, string> result = ExecuteTransaction(param);
            return result.Item1;
        }
        /// <summary>
        /// 異步根據指定對象的ID和用戶ID,從數據庫中刪除指定對象(用于記錄人員的操作日志）
        /// </summary>
        /// <param name="primaryKey">指定對象的ID</param>
        /// <param name="userId">用戶ID</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual async Task<bool> DeleteByUserAsync(TKey primaryKey, string userId, IDbConnection conn = null, IDbTransaction trans = null)
        {
            var param = new List<Tuple<string, object>>();
            string sql = $"delete from {tableName} where " + PrimaryKey + " = @PrimaryKey";
            Tuple<string, object> tupel = new Tuple<string, object>(sql, new { @PrimaryKey = primaryKey });
            param.Add(tupel);
            Tuple<bool, string> result = await ExecuteTransactionAsync(param);
            return result.Item1;
        }

        /// <summary>
        /// 邏輯刪除信息，bl為true時將DeleteMark設置為1刪除，bl為flase時將DeleteMark設置為10-恢復刪除
        /// </summary>
        /// <param name="bl">true為不刪除，false刪除</param>
        /// <param name="primaryKey">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual bool DeleteSoft(bool bl, TKey primaryKey, string userId = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            string sql = $"update {tableName} set ";
            if (bl) {
                sql += "DeleteMark='0' ";
            } else {
                sql += "DeleteMark='1' ";
            }
            if (!string.IsNullOrEmpty(userId)) {
                sql += ",DeleteUserId='" + userId + "'";
            }
            DateTime deleteTime = DateTime.Now;
            sql += ",DeleteTime=@DeleteTime where " + PrimaryKey + "=@PrimaryKey";
            var param = new List<Tuple<string, object>>();
            Tuple<string, object> tupel = new Tuple<string, object>(sql, new { @PrimaryKey = primaryKey, @DeleteTime = deleteTime });
            param.Add(tupel);
            Tuple<bool, string> result = ExecuteTransaction(param);
            return result.Item1;
        }

        /// <summary>
        /// 異步邏輯刪除信息，bl為true時將DeleteMark設置為1刪除，bl為flase時將DeleteMark設置為0-恢復刪除
        /// </summary>
        /// <param name="bl">true為不刪除，false刪除</param>
        /// <param name="primaryKey">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual async Task<bool> DeleteSoftAsync(bool bl, TKey primaryKey, string userId = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            string sql = $"update {tableName} set ";
            if (bl) {
                sql += "DeleteMark='0' ";
            } else {
                sql += "DeleteMark='1' ";
            }
            if (!string.IsNullOrEmpty(userId)) {
                sql += ",DeleteUserId='" + userId + "'";
            }
            DateTime deleteTime = DateTime.Now;
            sql += ",DeleteTime=@DeleteTime where " + PrimaryKey + "=@PrimaryKey";
            var param = new List<Tuple<string, object>>();
            Tuple<string, object> tupel = new Tuple<string, object>(sql, new { @PrimaryKey = primaryKey, @DeleteTime = deleteTime });
            param.Add(tupel);
            Tuple<bool, string> result = await ExecuteTransactionAsync(param);
            return result.Item1;
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
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(where)) {
                where = "1=1";
            }
            string sql = $"update {tableName} set ";
            if (bl) {
                sql += "DeleteMark='0' ";
            } else {
                sql += "DeleteMark='1' ";
            }
            if (!string.IsNullOrEmpty(userId)) {
                sql += ",DeleteUserId='" + userId + "'";
            }
            DateTime deleteTime = DateTime.Now;
            sql += ",DeleteTime=@DeleteTime where " + where;

            var param = new List<Tuple<string, object>>();
            Tuple<string, object> tupel = new Tuple<string, object>(sql, new { @DeleteTime = deleteTime });
            param.Add(tupel);
            Tuple<bool, string> result = await ExecuteTransactionAsync(param);
            return result.Item1;
        }
        /// <summary>
        /// 設置數據有效性，將EnabledMark設置為1-有效，0-為無效
        /// </summary>
        /// <param name="bl">true為有效，false無效</param>
        /// <param name="primaryKey">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual bool SetEnabledMark(bool bl, TKey primaryKey, string userId = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            string sql = $"update {tableName} set ";
            if (bl) {
                sql += "EnabledMark='1' ";
            } else {
                sql += "EnabledMark='0' ";
            }
            if (!string.IsNullOrEmpty(userId)) {
                sql += ",LastModifyUserId='" + userId + "'";
            }
            DateTime lastModifyTime = DateTime.Now;
            sql += ",LastModifyTime=@lastModifyTime where " + PrimaryKey + "=@PrimaryKey";

            var param = new List<Tuple<string, object>>();
            Tuple<string, object> tupel = new Tuple<string, object>(sql, new { @PrimaryKey = primaryKey, @lastModifyTime = lastModifyTime });
            param.Add(tupel);
            Tuple<bool, string> result = ExecuteTransaction(param);
            return result.Item1;
        }

        /// <summary>
        /// 異步設置數據有效性，將EnabledMark設置為1:有效，0-為無效
        /// </summary>
        /// <param name="bl">true為有效，false無效</param>
        /// <param name="primaryKey">主鍵ID</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual async Task<bool> SetEnabledMarkAsync(bool bl, TKey primaryKey, string userId = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            string sql = $"update {tableName} set ";
            if (bl) {
                sql += "EnabledMark='1' ";
            } else {
                sql += "EnabledMark='0' ";
            }
            if (!string.IsNullOrEmpty(userId)) {
                sql += ",LastModifyUserId='" + userId + "'";
            }
            DateTime lastModifyTime = DateTime.Now;
            sql += ",LastModifyTime=@LastModifyTime where " + PrimaryKey + "=@PrimaryKey";

            var param = new List<Tuple<string, object>>();
            Tuple<string, object> tupel = new Tuple<string, object>(sql, new { @PrimaryKey = PrimaryKey, @LastModifyTime = lastModifyTime });
            param.Add(tupel);
            Tuple<bool, string> result = await ExecuteTransactionAsync(param);
            return result.Item1;
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
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(where)) {
                where = "1=1";
            }
            string sql = $"update {tableName} set ";
            if (bl) {
                sql += "EnabledMark='1' ";
            } else {
                sql += "EnabledMark='0' ";
            }
            if (!string.IsNullOrEmpty(userId)) {
                sql += ",LastModifyUserId='" + userId + "'";
            }
            DateTime lastModifyTime = DateTime.Now;
            sql += ",LastModifyTime=@LastModifyTime where " + where;

            var param = new List<Tuple<string, object>>();
            Tuple<string, object> tupel = new Tuple<string, object>(sql, new { LastModifyTime = lastModifyTime });
            param.Add(tupel);
            Tuple<bool, string> result = await ExecuteTransactionAsync(param);
            return result.Item1;
        }
        /// <summary>
        /// 異步按條件設置數據有效性，將EnabledMark設置為1:有效，0-為無效
        /// </summary>
        /// <param name="bl">true為有效，false無效</param>
        /// <param name="where">條件</param>
        /// <param name="paramparameters"></param>
        /// <param name="userId"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual async Task<bool> SetEnabledMarkByWhereAsync(bool bl, string where, object paramparameters = null, string userId = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(where)) {
                where = "1=1";
            }
            string sql = $"update {tableName} set ";
            if (bl) {
                sql += "EnabledMark='1' ";
            } else {
                sql += "EnabledMark='0' ";
            }
            if (!string.IsNullOrEmpty(userId)) {
                sql += ",LastModifyUserId='" + userId + "'";
            }
            DateTime lastModifyTime = DateTime.Now;
            sql += ",LastModifyTime=@LastModifyTime  " + where;

            var param = new List<Tuple<string, object>>();
            Tuple<string, object> tupel = new Tuple<string, object>(sql, new { LastModifyTime = lastModifyTime, paramparameters });
            param.Add(tupel);
            Tuple<bool, string> result = await ExecuteTransactionAsync(param);
            return result.Item1;
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
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(where)) {
                where = "1=1";
            }
            string sql = $"update {tableName} set " + strField + "='" + fieldValue + "'";
            if (!string.IsNullOrEmpty(where)) {
                sql += " where " + where;
            }

            var param = new List<Tuple<string, object>>();
            Tuple<string, object> tupel = new Tuple<string, object>(sql, null);
            param.Add(tupel);
            Tuple<bool, string> result = ExecuteTransaction(param);
            return result.Item1;
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
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(where)) {
                where = "1=1";
            }
            string sql = $"update {tableName} set " + strField + "='" + fieldValue + "'";
            if (!string.IsNullOrEmpty(where)) {
                sql += " where " + where;
            }
            var param = new List<Tuple<string, object>>();
            Tuple<string, object> tupel = new Tuple<string, object>(sql, null);
            param.Add(tupel);
            Tuple<bool, string> result = await ExecuteTransactionAsync(param);
            return result.Item1;
        }
        /// <summary>
        /// 更新某一字段值，字段值為數字
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="fieldValue">字段值數字</param>
        /// <param name="where">條件,為空更新所有內容</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual bool UpdateTableField(string strField, int fieldValue, string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(where)) {
                where = "1=1";
            }
            string sql = $"update {tableName} set " + strField + "=" + fieldValue + "";
            if (!string.IsNullOrEmpty(where)) {
                sql += " where " + where;
            }
            var param = new List<Tuple<string, object>>();
            Tuple<string, object> tupel = new Tuple<string, object>(sql, null);
            param.Add(tupel);
            Tuple<bool, string> result = ExecuteTransaction(param);
            return result.Item1;
        }


        /// <summary>
        /// 更新某一字段值，字段值為數字
        /// </summary>
        /// <param name="strField">字段</param>
        /// <param name="fieldValue">字段值數字</param>
        /// <param name="where">條件,為空更新所有內容</param>
        /// <param name="trans">事務對象</param>
        /// <returns>執行成功返回<c>true</c>，否則為<c>false</c>。</returns>
        public virtual async Task<bool> UpdateTableFieldAsync(string strField, int fieldValue, string where, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where)) {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(where)) {
                where = "1=1";
            }
            string sql = $"update {tableName} set " + strField + "=" + fieldValue + "";
            if (!string.IsNullOrEmpty(where)) {
                sql += " where " + where;
            }
            var param = new List<Tuple<string, object>>();
            Tuple<string, object> tupel = new Tuple<string, object>(sql, null);
            param.Add(tupel);
            Tuple<bool, string> result = await ExecuteTransactionAsync(param);
            return result.Item1;
        }

        /// <summary>
        /// 多表多數據操作批量插入、更新、刪除--事務
        /// </summary>
        /// <param name="trans">事務</param>
        /// <param name="commandTimeout">超時</param>
        /// <returns></returns>
        public async Task<Tuple<bool, string>> ExecuteTransactionAsync(List<Tuple<string, object>> trans, int? commandTimeout = null)
        {
            if (!trans.Any()) return new Tuple<bool, string>(false, "執行事務SQL語句不能為空！");
            using (IDbConnection connection = DapperConn) {
                bool isClosed = connection.State == ConnectionState.Closed;
                if (isClosed) connection.Open();
                using (var transaction = connection.BeginTransaction()) {
                    try {
                        foreach (var tran in trans) {
                            await connection.ExecuteAsync(tran.Item1, tran.Item2, transaction, commandTimeout);
                        }
                        //提交事務
                        transaction.Commit();
                        return new Tuple<bool, string>(true, string.Empty);
                    } catch (Exception ex) {
                        //回滾事務
                        Log4NetHelper.Error("", ex);
                        transaction.Rollback();
                        connection.Close();
                        connection.Dispose();
                        DapperConn.Close();
                        DapperConn.Dispose();
                        throw ex;
                    } finally {
                        connection.Close();
                        connection.Dispose();
                        DapperConn.Close();
                        DapperConn.Dispose();
                    }
                }
            }
        }


        /// <summary>
        /// 多表多數據操作批量插入、更新、刪除--事務
        /// </summary>
        /// <param name="trans">事務</param>
        /// <param name="commandTimeout">超時</param>
        /// <returns></returns>
        public Tuple<bool, string> ExecuteTransaction(List<Tuple<string, object>> trans, int? commandTimeout = null)
        {
            if (!trans.Any()) return new Tuple<bool, string>(false, "執行事務SQL語句不能為空！");
            using (IDbConnection connection = DapperConn) {
                bool isClosed = connection.State == ConnectionState.Closed;
                if (isClosed) connection.Open();
                //開啟事務
                using (var transaction = connection.BeginTransaction()) {
                    try {
                        foreach (var tran in trans) {
                            connection.Execute(tran.Item1, tran.Item2, transaction, commandTimeout);
                        }
                        //提交事務
                        transaction.Commit();
                        return new Tuple<bool, string>(true, string.Empty);
                    } catch (Exception ex) {
                        //回滾事務
                        Log4NetHelper.Error("", ex);
                        transaction.Rollback();
                        connection.Close();
                        connection.Dispose();
                        DapperConn.Close();
                        DapperConn.Dispose();
                        return new Tuple<bool, string>(false, ex.ToString());
                    } finally {
                        connection.Close();
                        connection.Dispose();
                        DapperConn.Close();
                        DapperConn.Dispose();
                    }
                }
            }
        }

        #endregion

        #endregion

        #region EF操作

        /// <summary>
        /// EF 上下文接口，可讀可寫
        /// </summary>
        public virtual IDbContextCore DbContext
        {
            get { return _dbContext; }
        }

        /// <summary>
        /// EF 上下文接口，僅可讀
        /// </summary>
        public virtual IDbContextCore DbContextRead
        {
            get { return _dbContextFactory.CreateContext<T>(WriteAndReadEnum.Read); }
        }

        #region 新增
        /// <summary>
        /// 新增實體
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Add(T entity)
        {
            return DbContext.Add<T>(entity);
        }
        /// <summary>
        /// 新增實體
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> AddAsync(T entity)
        {
            return await DbContext.AddAsync(entity);
        }
        /// <summary>
        /// 批量新增實體，數量量較多是推薦使用BulkInsert()
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int AddRange(ICollection<T> entities)
        {
            return DbContext.AddRange(entities);
        }
        /// <summary>
        /// 批量新增實體，數量量較多是推薦使用BulkInsert()
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> AddRangeAsync(ICollection<T> entities)
        {
            return await DbContext.AddRangeAsync(entities);
        }
        /// <summary>
        /// 批量新增SqlBulk方式，效率最高
        /// </summary>
        /// <param name="entities">數據實體集合</param>
        /// <param name="destinationTableName">數據庫表名稱，默認為實體名稱</param>
        public virtual void BulkInsert(IList<T> entities, string destinationTableName = null)
        {
            DbContext.BulkInsert<T>(entities, destinationTableName);
        }
        /// <summary>
        /// 執行新增的sql語句
        /// </summary>
        /// <param name="sql">新增Sql語句</param>
        /// <returns></returns>
        public int AddBySql(string sql)
        {
            return DbContext.ExecuteSqlWithNonQuery(sql);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        public void InsertRange(List<T> entities, IDbConnection conn = null, IDbTransaction trans = null)
        {
            DbContext.GetDbSet<T>().AddRange(entities);
            DbContext.SaveChanges();
        }

        #endregion

        #region Update

        /// <summary>
        /// 更新數據實體
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Edit(T entity)
        {
            return DbContext.Edit<T>(entity);
        }
        /// <summary>
        /// 批量更新數據實體
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int EditRange(ICollection<T> entities)
        {
            return DbContext.EditRange(entities);
        }
        /// <summary>
        /// 更新指定字段的值
        /// </summary>
        /// <param name="model">數據實體</param>
        /// <param name="updateColumns">指定字段</param>
        /// <returns></returns>
        public virtual int Update(T model, params string[] updateColumns)
        {
            DbContext.Update(model, updateColumns);
            return DbContext.SaveChanges();
        }
        /// <summary>
        /// 執行更新數據的Sql語句
        /// </summary>
        /// <param name="sql">更新數據的Sql語句</param>
        /// <returns></returns>
        public int UpdateBySql(string sql)
        {
            return DbContext.ExecuteSqlWithNonQuery(sql);
        }
        #endregion

        #region Delete

        /// <summary>
        /// 根據主鍵刪除數據
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual int Delete(TKey key)
        {
            return DbContext.Delete<T, TKey>(key);
        }
        /// <summary>
        /// 執行刪除數據Sql語句
        /// </summary>
        /// <param name="sql">刪除的Sql語句</param>
        /// <returns></returns>
        public int DeleteBySql(string sql)
        {
            return DbContext.ExecuteSqlWithNonQuery(sql);
        }

        #endregion

        #region Query
        /// <summary>
        /// 根據條件統計數量Count()
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<T, bool>> @where = null)
        {
            return DbContext.Count(where);
        }

        /// <summary>
        /// 根據條件統計數量Count()
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> @where = null)
        {
            return await DbContext.CountAsync(where);
        }
        /// <summary>
        /// 是否存在,存在返回true，不存在返回false
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>

        public virtual bool Exist(Expression<Func<T, bool>> @where = null)
        {
            return DbContext.Exist(where);
        }
        /// <summary>
        /// 是否存在,存在返回true，不存在返回false
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistAsync(Expression<Func<T, bool>> @where = null)
        {
            return await DbContext.ExistAsync(where);
        }

        /// <summary>
        /// 根據主鍵獲取實體。建議：如需使用Include和ThenInclude請重載此方法。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T GetSingle(TKey key)
        {
            return DbContext.Find<T, TKey>(key);
        }


        /// <summary>
        /// 根據主鍵獲取實體。建議：如需使用Include和ThenInclude請重載此方法。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<T> GetSingleAsync(TKey key)
        {
            return await DbContext.FindAsync<T, TKey>(key);
        }

        /// <summary>
        /// 獲取單個實體。建議：如需使用Include和ThenInclude請重載此方法。
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual T GetSingleOrDefault(Expression<Func<T, bool>> @where = null)
        {
            return DbContext.GetSingleOrDefault(@where);
        }

        /// <summary>
        /// 獲取單個實體。建議：如需使用Include和ThenInclude請重載此方法。
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> @where = null)
        {
            return await DbContext.GetSingleOrDefaultAsync(where);
        }

        /// <summary>
        /// 獲取實體列表。建議：如需使用Include和ThenInclude請重載此方法。
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IList<T> Get(Expression<Func<T, bool>> @where = null)
        {
            return DbContext.GetByCompileQuery(where);
        }
        /// <summary>
        /// 獲取實體列表。建議：如需使用Include和ThenInclude請重載此方法。
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> @where = null)
        {
            return await DbContext.GetByCompileQueryAsync(where);
        }

        /// <summary>
        ///  分頁獲取實體列表。建議：如需使用Include和ThenInclude請重載此方法。
        /// </summary>
        /// <param name="where">查詢條件</param>
        /// <param name="pagerInfo">分頁信息</param>
        /// <param name="asc">排序方式</param>
        /// <param name="orderby">排序字段</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetByPagination(Expression<Func<T, bool>> @where, PagerInfo pagerInfo, bool asc = false, params Expression<Func<T, object>>[] @orderby)
        {
            var filter = DbContext.Get(where);
            if (orderby != null) {
                foreach (var func in orderby) {
                    filter = asc ? filter.OrderBy(func).AsQueryable() : filter.OrderByDescending(func).AsQueryable();
                }
            }
            pagerInfo.RecordCount = filter.Count();
            return filter.Skip(pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)).Take(pagerInfo.PageSize);
        }
        /// <summary>
        /// sql語句查詢數據集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> GetBySql(string sql)
        {
            return DbContext.SqlQuery<T, T>(sql);
        }
        /// <summary>
        /// sql語句查詢數據集，返回輸出Dto實體
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<TView> GetViews<TView>(string sql)
        {
            var list = DbContext.SqlQuery<T, TView>(sql);
            return list;
        }
        /// <summary>
        /// 查詢視圖
        /// </summary>
        /// <typeparam name="TView">返回結果對象</typeparam>
        /// <param name="viewName">視圖名稱</param>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public List<TView> GetViews<TView>(string viewName, Func<TView, bool> @where)
        {
            var list = DbContext.SqlQuery<T, TView>($"select * from {viewName}");
            if (where != null) {
                return list.Where(where).ToList();
            }

            return list;
        }

        #endregion
        #endregion

        #region 輔助類方法


        /// <summary>
        /// 驗證是否存在注入代碼(條件語句）
        /// </summary>
        /// <param name="inputData"></param>
        public virtual bool HasInjectionData(string inputData)
        {
            if (string.IsNullOrEmpty(inputData))
                return false;

            //里面定義惡意字符集合
            //驗證inputData是否包含惡意集合
            if (Regex.IsMatch(inputData.ToLower(), GetRegexString())) {
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// 獲取正則表達式
        /// </summary>
        /// <returns></returns>
        private static string GetRegexString()
        {
            //構造SQL的注入關鍵字符
            string[] strBadChar =
            {
                "select\\s",
                "from\\s",
                "insert\\s",
                "delete\\s",
                "update\\s",
                "drop\\s",
                "truncate\\s",
                "exec\\s",
                "count\\(",
                "declare\\s",
                "asc\\(",
                "mid\\(",
                //"char\\(",
                "net user",
                "xp_cmdshell",
                "/add\\s",
                "exec master.dbo.xp_cmdshell",
                "net localgroup administrators"
            };

            //構造正則表達式
            string str_Regex = ".*(";
            for (int i = 0; i < strBadChar.Length - 1; i++) {
                str_Regex += strBadChar[i] + "|";
            }
            str_Regex += strBadChar[^1] + ").*";

            return str_Regex;
        }


        #endregion

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
            if (DbContext != null) {
                DbContext.Dispose();
            }
            if (DapperConn != null) {
                DapperConn?.Dispose();
            }
        }

        // TODO: 僅當以上 Dispose(bool disposing) 擁有用于釋放未托管資源的代碼時才替代終結器。
        // ~BaseRepository() {
        //   // 請勿更改此代碼。將清理代碼放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // 請勿更改此代碼。將清理代碼放入以上 Dispose(bool disposing) 中。
            Dispose(true);

            DbContext?.Dispose();
            DapperConn?.Dispose();
            // TODO: 如果在以上內容中替代了終結器，則取消注釋以下行。
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
