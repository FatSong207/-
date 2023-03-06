using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyModel;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Yuebon.Commons.Attributes;
using Yuebon.Commons.Core.DataManager;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Enums;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Json;
using Yuebon.Commons.Models;
using Yuebon.Commons.Options;
using Yuebon.Commons.Pages;

namespace Yuebon.Commons.DbContextCore
{
    /// <summary>
    /// DbContext上下文的實現
    /// </summary>
    public class BaseDbContext : DbContext, IDbContextCore
    {


        #region 基礎參數

        /// <summary>
        /// 數據庫配置名稱，可在子類指定不同的配置名稱，用于訪問不同的數據庫
        /// </summary>
        protected string dbConfigName = "";

        /// <summary>
        /// 是否開啟多租戶
        /// </summary>
        protected bool isMultiTenant = false;

        /// <summary>
        /// 
        /// </summary>
        protected DbConnectionOptions dbConnectionOptions;


        /// <summary>
        /// 數據庫訪問對象的外鍵約束
        /// </summary>
        public bool IsMultiTenant
        {
            get
            {
                return isMultiTenant;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DatabaseFacade GetDatabase() => Database;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        protected BaseDbContext()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnectionOptions"></param>
        public BaseDbContext(DbConnectionOptions dbConnectionOptions)
        {
            this.dbConnectionOptions = dbConnectionOptions;
        }

        /// <summary>
        /// 配置，初始化數據庫引擎
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (dbConnectionOptions == null) {
                dbConnectionOptions = DBServerProvider.GeDbConnectionOptions();
            }
            string defaultSqlConnectionString = dbConnectionOptions.ConnectionString;

            DatabaseType dbType = dbConnectionOptions.DatabaseType;
            if (dbType == DatabaseType.SqlServer) {
                optionsBuilder.UseSqlServer(defaultSqlConnectionString);
            } else if (dbType == DatabaseType.MySql) {
                optionsBuilder.UseMySql(defaultSqlConnectionString, new MySqlServerVersion(new Version(8, 0, 21)));
            } else if (dbType == DatabaseType.Oracle) {
                optionsBuilder.UseOracle(defaultSqlConnectionString);
            } else if (dbType == DatabaseType.SQLite) {
                optionsBuilder.UseSqlite(defaultSqlConnectionString);
            } else if (dbType == DatabaseType.Npgsql) {
                optionsBuilder.UseNpgsql(defaultSqlConnectionString);
            } else {
                throw new NotSupportedException("The database is not supported");
            }
            //使用查詢跟蹤行為
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }
        /// <summary>
        /// 模型創建
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MappingEntityTypes(modelBuilder);

            //https://github.com/dotnet/efcore/issues/5159
            //https://stackoverflow.com/questions/42190909/how-to-specify-entity-framework-core-table-mapping
            //https://stackoverflow.com/questions/53275567/how-to-apply-common-configuration-to-all-entities-in-ef-core
            //https://stackoverflow.com/questions/26170868/how-to-change-table-name-using-database-first-entity-framework
            //https://docs.microsoft.com/en-us/answers/questions/658905/apply-common-configuration-to-all-entity-types-ef.html

            //https://www.meziantou.net/entity-framework-core-naming-convention.htm
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
                // Add NuGet package "Humanizer" to use Singularize()
                entityType.SetTableName(entityType.GetTableName().ToLower());

                //https://www.meziantou.net/entity-framework-core-naming-convention.htm
                //https://stackoverflow.com/questions/60697523/missing-relational-method-when-migration-from-net-core-2-2-to-3-1
                //foreach (var property in entityType.GetProperties())
                //{
                //    property.Relational().ColumnName = entityType.Relational().TableName + "_" + property.Relational().ColumnName;
                //}
                foreach (var property in entityType.GetProperties()) {
                    //property.SetColumnName(property.GetColumnBaseName().ToLower());
                    property.SetColumnName(property.GetColumnName().ToLower());
                    // https://docs.microsoft.com/zh-tw/ef/core/what-is-new/ef-core-5.0/breaking-changes#getcolumnname-obsolete
                    //property.SetColumnName(property.GetColumnName(StoreObjectIdentifier.Table(entityType.GetTableName().ToLower(), null)).ToLower());
                }
            }

            //foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    // check if current entity type is child of BaseModel
            //    if (mutableEntityType.ClrType.IsAssignableTo(typeof(Entity)))
            //    {
            //        var schema = mutableEntityType.GetSchema();
            //        var tableName = mutableEntityType.GetTableName();
            //        Console.WriteLine(tableName);
            //    }
            //}

            base.OnModelCreating(modelBuilder);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void MappingEntityTypes(ModelBuilder modelBuilder)
        {
            var assemblies = RuntimeHelper.GetAllYuebonAssemblies();
            foreach (var assembly in assemblies) {
                var entityTypes = assembly.GetTypes()
                    .Where(type => !string.IsNullOrWhiteSpace(type.Namespace))
                    .Where(type => type.IsClass)
                    .Where(type => type.BaseType != null)
                    .Where(type => typeof(Entity).IsAssignableFrom(type) || type.IsSubclassOf(typeof(BaseViewModel)));

                foreach (var entityType in entityTypes) {
                    if (modelBuilder.Model.FindEntityType(entityType) != null || entityType.Name == "Entity" || entityType.Name == "BaseEntity`1")
                        continue;
                    var table = entityType.GetCustomAttributes<TableAttribute>().FirstOrDefault();
                    modelBuilder.Model.AddEntityType(entityType).SetTableName(table.Name);

                    var ientityTypes = modelBuilder.Model.FindEntityType(entityType);
                    var attr = entityType.GetCustomAttributes<ShardingTableAttribute>().FirstOrDefault();
                    if (attr != null && entityType != null) {
                        modelBuilder.Model.FindEntityType(entityType).SetTableName($"{entityType.Name}{attr.Splitter}{DateTime.Now.ToString(attr.Suffix)}");
                    }

                    if (typeof(IDeleteAudited).IsAssignableFrom(typeof(Entity))) {
                        modelBuilder.Entity<Entity>().HasQueryFilter(m => ((IDeleteAudited)m).DeleteMark == false);
                    }
                    if (IsMultiTenant) {
                        if (typeof(IMustHaveTenant).IsAssignableFrom(typeof(Entity))) {
                            modelBuilder.Entity<Entity>().HasQueryFilter(m => ((IMustHaveTenant)m).TenantId == "");
                        }
                    }
                }
            }
        }


        #region 接口實現


        /// <summary>
        /// 新增實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public new virtual int Add<T>(T entity) where T : class
        {
            base.Add(entity);
            return SaveChanges();
        }

        /// <summary>
        /// 新增實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> AddAsync<T>(T entity) where T : class
        {
            await base.AddAsync(entity);
            return await SaveChangesAsync();
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int AddRange<T>(ICollection<T> entities) where T : class
        {
            base.AddRange(entities);
            return SaveChanges();
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> AddRangeAsync<T>(ICollection<T> entities) where T : class
        {
            await base.AddRangeAsync(entities);
            return await SaveChangesAsync();
        }
        /// <summary>
        /// 統計數量Count()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual int Count<T>(Expression<Func<T, bool>> @where = null) where T : class
        {
            return where == null ? GetDbSet<T>().Count() : GetDbSet<T>().Count(@where);
        }
        /// <summary>
        /// 統計數量Count()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync<T>(Expression<Func<T, bool>> @where = null) where T : class
        {
            return await (where == null ? GetDbSet<T>().CountAsync() : GetDbSet<T>().CountAsync(@where));
        }
        /// <summary>
        /// 物理刪除數據
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">主鍵類型</typeparam>
        /// <param name="key">主鍵</param>
        /// <returns></returns>
        public virtual int Delete<T, TKey>(TKey key) where T : Entity
        {
            var entity = Find<T>(key);
            Remove(entity);
            return SaveChanges();
        }
        /// <summary>
        /// 創建數據表
        /// </summary>
        /// <returns></returns>
        public virtual bool EnsureCreated()
        {
            return Database.EnsureCreated();
        }
        /// <summary>
        /// 創建數據表
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> EnsureCreatedAsync()
        {
            return await Database.EnsureCreatedAsync();
        }

        /// <summary>
        ///執行Sql，返回影響記錄行數
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual int ExecuteSqlWithNonQuery(string sql, params object[] parameters)
        {
            return Database.ExecuteSqlRaw(sql, parameters);
        }
        /// <summary>
        /// 執行Sql，返回影響記錄行數
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual async Task<int> ExecuteSqlWithNonQueryAsync(string sql, params object[] parameters)
        {
            return await Database.ExecuteSqlRawAsync(sql, parameters);
        }

        /// <summary>
        /// 編輯更新保存實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Edit<T>(T entity) where T : class
        {
            base.Update(entity);
            base.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        /// <summary>
        /// 批量更新保存實體
        /// 以添加狀態開始跟蹤給定的實體和任何其他尚未被跟蹤的可訪問實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int EditRange<T>(ICollection<T> entities) where T : class
        {
            GetDbSet<T>().AttachRange(entities.ToArray());
            return SaveChanges();
        }
        /// <summary>
        /// 是否存在,存在返回true，不存在返回false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual bool Exist<T>(Expression<Func<T, bool>> @where = null) where T : class
        {
            return @where == null ? GetDbSet<T>().Any() : GetDbSet<T>().Any(@where);
        }
        /// <summary>
        /// 是否存在,存在返回true，不存在返回false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistAsync<T>(Expression<Func<T, bool>> @where = null) where T : class
        {
            return await Task.FromResult(Exist(where));
        }
        /// <summary>
        /// 根據條件進行查詢數據
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="include"></param>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual IQueryable<T> FilterWithInclude<T>(Func<IQueryable<T>, IQueryable<T>> include, Expression<Func<T, bool>> @where) where T : class
        {
            var result = GetDbSet<T>().AsQueryable();
            if (where != null)
                result = GetDbSet<T>().Where(where);
            if (include != null)
                result = include(result);
            return result;
        }

        /// <summary>
        /// 根據主鍵查詢實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T Find<T>(object key) where T : class
        {
            return base.Find<T>(key);
        }
        /// <summary>
        /// 根據主鍵查詢實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T Find<T>(string key) where T : class
        {
            return base.Find<T>(key);
        }
        /// <summary>
        /// 根據主鍵查詢實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T Find<T, TKey>(TKey key) where T : Entity
        {
            return base.Find<T>(key);
        }
        /// <summary>
        /// 根據主鍵查詢實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<T> FindAsync<T>(object key) where T : class
        {
            return await base.FindAsync<T>(key);
        }
        /// <summary>
        /// 根據主鍵查詢實體
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<T> FindAsync<T, TKey>(TKey key) where T : Entity
        {
            return await base.FindAsync<T>(key);
        }
        /// <summary>
        /// 根據條件查詢實體，返回實體集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查詢條件</param>
        /// <param name="asNoTracking">是否啟用模型跟蹤，默認為false不跟蹤</param>
        /// <returns></returns>
        public virtual IQueryable<T> Get<T>(Expression<Func<T, bool>> @where = null, bool asNoTracking = false) where T : class
        {
            var query = GetDbSet<T>().AsQueryable();
            if (where != null)
                query = query.Where(where);
            if (asNoTracking)
                query = query.AsNoTracking();
            return query;
        }
        /// <summary>
        /// 獲取所有實體類型
        /// </summary>
        /// <returns></returns>
        public virtual List<IEntityType> GetAllEntityTypes()
        {
            return Model.GetEntityTypes().ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual DbSet<T> GetDbSet<T>() where T : class
        {
            if (Model.FindEntityType(typeof(T)) != null)
                return Set<T>();
            throw new Exception($"類型{typeof(T).Name}未在數據庫上下文中註冊，請先在DbContextOption設置ModelAssemblyName以將所有實體類型註冊到數據庫上下文中。");
        }
        /// <summary>
        /// 根據條件查詢一個實體，
        /// 檢驗序列中是否包含有元素。引用類型的默認值default(T)為null，表示在序列中沒有找到元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual T GetSingleOrDefault<T>(Expression<Func<T, bool>> @where = null) where T : class
        {
            return where == null ? GetDbSet<T>().SingleOrDefault() : GetDbSet<T>().SingleOrDefault(where);
        }

        /// <summary>
        /// 根據條件查詢一個實體，
        /// 檢驗序列中是否包含有元素。引用類型的默認值default(T)為null，表示在序列中沒有找到元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查詢條件</param>
        /// <returns></returns>
        public virtual async Task<T> GetSingleOrDefaultAsync<T>(Expression<Func<T, bool>> @where = null) where T : class
        {
            return await (where == null ? GetDbSet<T>().SingleOrDefaultAsync() : GetDbSet<T>().SingleOrDefaultAsync(where));
        }

        /// <summary>
        /// 更新指定字段的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">數據實體</param>
        /// <param name="updateColumns">指定字段</param>
        /// <returns></returns>
        public virtual int Update<T>(T model, params string[] updateColumns) where T : class
        {
            if (updateColumns != null && updateColumns.Length > 0) {
                if (Entry(model).State == EntityState.Added ||
                    Entry(model).State == EntityState.Detached) GetDbSet<T>().Attach(model);
                foreach (var propertyName in updateColumns) {
                    Entry(model).Property(propertyName).IsModified = true;
                }
            } else {
                Entry(model).State = EntityState.Modified;
            }
            return SaveChanges();
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">數據實體集合</param>
        /// <param name="destinationTableName">數據庫表名稱</param>
        public virtual void BulkInsert<T>(IList<T> entities, string destinationTableName = null, IDbConnection conn = null, IDbTransaction trans = null) where T : class
        {
            if (!Database.IsSqlServer() && !Database.IsMySql())
                throw new NotSupportedException("This method only supports for SQL Server or MySql.");
        }
        /// <summary>
        /// Sql查詢，并返回實體
        /// </summary>
        /// <typeparam name="T">查詢對象實體</typeparam>
        /// <typeparam name="TView">返回/輸出實體</typeparam>
        /// <param name="sql">sql語句</param>
        /// <param name="parameters">SQL參數</param>
        /// <returns></returns>
        public virtual List<TView> SqlQuery<T, TView>(string sql, params object[] parameters)
            where T : class
        {
            return GetDbSet<T>().FromSqlRaw(sql, parameters).Cast<TView>().ToList();
        }

        /// <summary>
        /// Sql查詢，并返回實體
        /// </summary>
        /// <typeparam name="T">查詢對象實體</typeparam>
        /// <typeparam name="TView">返回/輸出實體</typeparam>
        /// <param name="sql">sql語句</param>
        /// <param name="parameters">SQL參數</param>
        /// <returns></returns>
        public virtual async Task<List<TView>> SqlQueryAsync<T, TView>(string sql, params object[] parameters)
            where T : class
            where TView : class
        {
            return await GetDbSet<T>().FromSqlRaw(sql, parameters).Cast<TView>().ToListAsync();
        }
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
        public virtual PageResult<T> SqlQueryByPagination<T, TView>(string sql, string[] orderBys, int pageIndex, int pageSize,
            Action<TView> eachAction = null) where T : class where TView : class
        {
            throw new NotImplementedException();
        }
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
        public virtual PageResult<T> SqlQueryByPagination<T>(string sql, string[] orderBys, int pageIndex, int pageSize,
            params DbParameter[] parameters) where T : class, new()
        {
            throw new NotImplementedException();
        }
        #region 顯式編譯的查詢,提高查詢性能
        /// <summary>
        /// 根據主鍵查詢返回一個實體，該方法是顯式編譯的查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">主鍵類型</typeparam>
        /// <param name="id">主鍵值</param>
        /// <returns></returns>
        public T GetByCompileQuery<T, TKey>(TKey id) where T : Entity
        {
            return EF.CompileQuery((DbContext context, TKey id) => context.Set<T>().Find(id))(this, id);
        }
        /// <summary>
        /// 根據主鍵查詢返回一個實體，該方法是顯式編譯的查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">主鍵類型</typeparam>
        /// <param name="id">主鍵值</param>
        /// <returns></returns>
        public Task<T> GetByCompileQueryAsync<T, TKey>(TKey id) where T : Entity
        {
            return EF.CompileAsyncQuery((DbContext context, TKey id) => context.Set<T>().Find(id))(this, id);
        }
        /// <summary>
        /// 根據條件查詢返回實體集合，該方法是顯式編譯的查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        public IList<T> GetByCompileQuery<T>(Expression<Func<T, bool>> filter) where T : class
        {
            if (filter == null) filter = m => true;
            return EF.CompileQuery((DbContext context) => context.Set<T>().AsNoTracking().Where(filter).ToList())(this);
        }
        /// <summary>
        /// 根據條件查詢返回實體集合，該方法是顯式編譯的查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        public Task<List<T>> GetByCompileQueryAsync<T>(Expression<Func<T, bool>> filter) where T : class
        {
            if (filter == null) filter = m => true;
            return EF.CompileAsyncQuery((DbContext context) => context.Set<T>().AsNoTracking().Where(filter).ToList())(this);
        }

        /// <summary>
        /// 根據條件查詢一個實體，該方法是顯式編譯的查詢
        /// 檢驗序列中是否包含有元素。引用類型的默認值default(T)為null，表示在序列中沒有找到元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        public T FirstOrDefaultByCompileQuery<T>(Expression<Func<T, bool>> filter) where T : class
        {
            if (filter == null) filter = m => true;
            return EF.CompileQuery((DbContext context) => context.Set<T>().AsNoTracking().FirstOrDefault(filter))(this);
        }
        /// <summary>
        /// 根據條件查詢一個實體，該方法是顯式編譯的查詢
        /// 檢驗序列中是否包含有元素。引用類型的默認值default(T)為null，表示在序列中沒有找到元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        public Task<T> FirstOrDefaultByCompileQueryAsync<T>(Expression<Func<T, bool>> filter) where T : class
        {
            if (filter == null) filter = m => true;
            return EF.CompileAsyncQuery((DbContext context) => context.Set<T>().AsNoTracking().FirstOrDefault(filter))(this);
        }
        /// <summary>
        /// 根據條件查詢一個實體，啟用模型跟蹤，該方法是顯式編譯的查詢
        /// 檢驗序列中是否包含有元素。引用類型的默認值default(T)為null，表示在序列中沒有找到元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        public T FirstOrDefaultWithTrackingByCompileQuery<T>(Expression<Func<T, bool>> filter) where T : class
        {
            if (filter == null) filter = m => true;
            return EF.CompileQuery((DbContext context) => context.Set<T>().FirstOrDefault(filter))(this);
        }
        /// <summary>
        /// 根據條件查詢一個實體，啟用模型跟蹤，該方法是顯式編譯的查詢
        /// 檢驗序列中是否包含有元素。引用類型的默認值default(T)為null，表示在序列中沒有找到元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        public Task<T> FirstOrDefaultWithTrackingByCompileQueryAsync<T>(Expression<Func<T, bool>> filter) where T : class
        {
            if (filter == null) filter = m => true;
            return EF.CompileAsyncQuery((DbContext context) => context.Set<T>().FirstOrDefault(filter))(this);
        }
        /// <summary>
        /// 統計數量Count()，該方法是顯式編譯的查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        public int CountByCompileQuery<T>(Expression<Func<T, bool>> filter) where T : class
        {
            if (filter == null) filter = m => true;
            return EF.CompileQuery((DbContext context) => context.Set<T>().Count(filter))(this);
        }
        /// <summary>
        /// 統計數量Count()，該方法是顯式編譯的查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">查詢條件</param>
        /// <returns></returns>
        public Task<int> CountByCompileQueryAsync<T>(Expression<Func<T, bool>> filter) where T : class
        {
            if (filter == null) filter = m => true;
            return EF.CompileAsyncQuery((DbContext context) => context.Set<T>().Count(filter))(this);
        }

        /// <summary>
        /// 根據sql語句返回DataTable數據
        /// </summary>
        /// <param name="sql">Sql語句</param>
        /// <param name="cmdTimeout">執行超時時間，默認30毫秒</param>
        /// <param name="parameters">DbParameter[]參數</param>
        /// <returns></returns>
        public virtual DataTable GetDataTable(string sql, int cmdTimeout = 30, params DbParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根據sql語句返回List數據
        /// </summary>
        /// <param name="sql">Sql語句</param>
        /// <param name="cmdTimeout">執行超時時間，默認30毫秒</param>
        /// <param name="parameters">DbParameter[] 參數</param>
        /// <returns></returns>
        public virtual List<DataTable> GetDataTables(string sql, int cmdTimeout = 30, params DbParameter[] parameters)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion

    }
}
