using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.IDbContext;

namespace Yuebon.Commons.DbContextCore
{
    /// <summary>
    /// 
    /// </summary>
    public class MySqlDbContext : BaseDbContext, IMySqlDbContext
    {

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">數據實體集合</param>
        /// <param name="destinationTableName">數據庫表名稱</param>
        public override void BulkInsert<T>(IList<T> entities, string destinationTableName = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (entities == null || !entities.Any()) return;
            if (string.IsNullOrEmpty(destinationTableName)) {
                var mappingTableName = typeof(T).GetCustomAttribute<TableAttribute>()?.Name;
                destinationTableName = string.IsNullOrEmpty(mappingTableName) ? typeof(T).Name : mappingTableName;
            }
            MySqlBulkInsert(entities, destinationTableName, conn, trans);
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">數據實體集合</param>
        /// <param name="destinationTableName">數據庫表名稱</param>
        private void MySqlBulkInsert<T>(IList<T> entities, string destinationTableName, IDbConnection conn_out = null, IDbTransaction trans_out = null) where T : class
        {
            var tmpDir = Path.Combine(AppContext.BaseDirectory, "Temp");
            if (!Directory.Exists(tmpDir))
                Directory.CreateDirectory(tmpDir);
            var csvFileName = Path.Combine(tmpDir, $"{DateTime.Now:yyyyMMddHHmmssfff}.csv");
            if (!File.Exists(csvFileName))
                File.Create(csvFileName).Close();
            var separator = ",";
            entities.SaveToCsv(csvFileName, separator);
            var conn = conn_out == null ? (MySqlConnection)Database.GetDbConnection() : (MySqlConnection)conn_out;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            var bulk = new MySqlBulkLoader(conn) {
                NumberOfLinesToSkip = 0,
                TableName = destinationTableName,
                FileName = csvFileName,
                FieldTerminator = separator,
                FieldQuotationCharacter = '"',
                EscapeCharacter = '"',
                LineTerminator = "\r\n"
            };
            bulk.LoadAsync();
            conn.Close();
            File.Delete(csvFileName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdTimeout"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override DataTable GetDataTable(string sql, int cmdTimeout = 30, params DbParameter[] parameters)
        {
            return GetDataTables(sql, cmdTimeout, parameters).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdTimeout"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override List<DataTable> GetDataTables(string sql, int cmdTimeout = 30, params DbParameter[] parameters)
        {
            var dts = new List<DataTable>();
            //TODO： connection 不能dispose 或者 用using，否則下次獲取connection會報錯提示“the connectionstring property has not been initialized。”
            var connection = Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using (var cmd = new MySqlCommand(sql, (MySqlConnection)connection)) {
                cmd.CommandTimeout = cmdTimeout;
                if (parameters != null && parameters.Length > 0) {
                    cmd.Parameters.AddRange(parameters);
                }

                using (var da = new MySqlDataAdapter(cmd)) {
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
