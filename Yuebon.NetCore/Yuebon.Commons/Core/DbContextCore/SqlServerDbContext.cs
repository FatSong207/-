using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Pages;

namespace Yuebon.Commons.DbContextCore
{
    /// <summary>
    /// Sql Server數據庫上下文
    /// </summary>
    public class SqlServerDbContext : BaseDbContext, ISqlServerDbContext
    {

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">數據實體集合</param>
        /// <param name="destinationTableName">如果為 null，則使用 TModel 名稱作為 destinationTableName</param>
        /// <returns></returns>
        public override void BulkInsert<T>(IList<T> entities, string destinationTableName = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (entities == null || !entities.Any()) return;
            if (string.IsNullOrEmpty(destinationTableName)) {
                var mappingTableName = typeof(T).GetCustomAttribute<TableAttribute>()?.Name;
                destinationTableName = string.IsNullOrEmpty(mappingTableName) ? typeof(T).Name : mappingTableName;
            }
            SqlBulkInsert<T>(entities, destinationTableName, conn, trans);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">數據實體集合</param>
        /// <param name="destinationTableName">如果為 null，則使用 TModel 名稱作為 destinationTableName</param>
        /// <returns></returns>
        private void SqlBulkInsert<T>(IList<T> entities, string destinationTableName = null, IDbConnection conn_out = null, IDbTransaction trans_out = null) where T : class
        {
            using (var dt = entities.ToDataTable()) {
                dt.TableName = destinationTableName;
                var conn = conn_out == null ? (SqlConnection)Database.GetDbConnection() : (SqlConnection)conn_out;
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (var tran = conn.BeginTransaction()) {
                    try {
                        var bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran) {
                            BatchSize = entities.Count,
                            DestinationTableName = dt.TableName,
                        };
                        GenerateColumnMappings<T>(bulk.ColumnMappings);
                        bulk.WriteToServerAsync(dt);
                        tran.Commit();
                    } catch (Exception) {
                        tran.Rollback();
                        throw;
                    }
                }
                conn.Close();
            }
        }
        /// <summary>
        /// 字段與實體關系映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mappings"></param>
        private void GenerateColumnMappings<T>(SqlBulkCopyColumnMappingCollection mappings)
            where T : class
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties) {
                if (property.GetCustomAttributes<KeyAttribute>().Any()) {
                    mappings.Add(new SqlBulkCopyColumnMapping(property.Name, typeof(T).Name + property.Name));
                } else {
                    mappings.Add(new SqlBulkCopyColumnMapping(property.Name, property.Name));
                }
            }
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
        public override PageResult<T> SqlQueryByPagination<T, TView>(string sql, string[] orderBys, int pageIndex, int pageSize,
            Action<TView> eachAction = null)
        {
            var total = SqlQuery<T, int>($"select count(1) from ({sql}) as s").FirstOrDefault();
            var jsonResults = SqlQuery<T, TView>(
                    $"select * from (select *,row_number() over (order by {string.Join(",", orderBys)}) as RowId from ({sql}) as s) as t where RowId between {pageSize * (pageIndex - 1) + 1} and {pageSize * pageIndex} order by {string.Join(",", orderBys)}")
                .ToList();
            if (eachAction != null) {
                jsonResults = jsonResults.Each(eachAction).ToList();
            }

            return new PageResult<T>() {
                CurrentPage = pageIndex,
                ItemsPerPage = pageSize,
                TotalItems = total
            };
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
        public override PageResult<T> SqlQueryByPagination<T>(string sql, string[] orderBys, int pageIndex, int pageSize,
            params DbParameter[] parameters)
        {
            var total = (int)this.ExecuteScalar($"select count(1) from ({sql}) as s");
            var jsonResults = GetDataTable(
                    $"select * from (select *,row_number() over (order by {string.Join(",", orderBys)}) as RowId from ({sql}) as s) as t where RowId between {pageSize * (pageIndex - 1) + 1} and {pageSize * pageIndex} order by {string.Join(",", orderBys)}")
                .ToList<T>();
            return new PageResult<T>() {
                CurrentPage = pageIndex,
                ItemsPerPage = pageSize,
                TotalItems = total,
                Items = jsonResults
            };
        }
        /// <summary>
        /// 根據sql語句返回DataTable數據
        /// </summary>
        /// <param name="sql">Sql語句</param>
        /// <param name="cmdTimeout">執行超時時間，默認30毫秒</param>
        /// <param name="parameters">DbParameter[]參數</param>
        /// <returns></returns>
        public override DataTable GetDataTable(string sql, int cmdTimeout = 30, params DbParameter[] parameters)
        {
            return GetDataTables(sql, cmdTimeout, parameters).FirstOrDefault();
        }
        /// <summary>
        /// 根據sql語句返回List數據
        /// </summary>
        /// <param name="sql">Sql語句</param>
        /// <param name="cmdTimeout">執行超時時間，默認30毫秒</param>
        /// <param name="parameters">DbParameter[] 參數</param>
        /// <returns></returns>
        public override List<DataTable> GetDataTables(string sql, int cmdTimeout = 30, params DbParameter[] parameters)
        {
            var dts = new List<DataTable>();
            //TODO： connection 不能dispose 或者 用using，否則下次獲取connection會報錯提示“the connectionstring property has not been initialized。”
            var connection = Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using (var cmd = new SqlCommand(sql, (SqlConnection)connection)) {
                cmd.CommandTimeout = cmdTimeout;
                if (parameters != null && parameters.Length > 0) {
                    cmd.Parameters.AddRange(parameters);
                }

                using (var da = new SqlDataAdapter(cmd)) {
                    using (var ds = new DataSet()) {
                        da.Fill(ds);
                        foreach (DataTable table in ds.Tables) {
                            dts.Add(table);
                        }
                    }
                }
            }
            connection.Close();

            return dts;
        }
    }
}
