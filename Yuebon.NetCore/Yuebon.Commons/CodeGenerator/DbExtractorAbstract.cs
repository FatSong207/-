using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Core.DataManager;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Enums;
using Yuebon.Commons.Pages;

namespace Yuebon.Commons.CodeGenerator
{
    /// <summary>
    /// 實現了基本方法的數據庫抽取器基類
    /// </summary>
    public abstract class DbExtractorAbstract
    {

        #region 初始化
        /// <summary>
        /// 連接字符串
        /// </summary>
        internal string defaultSqlConnectionString { get; set; }

        private DbConnection dbConnection;
        /// <summary>
        /// 數據庫配置名稱
        /// </summary>
        protected string dbConfigName = "";
        /// <summary>
        /// 實例化
        /// </summary>
        public DbExtractorAbstract()
        {

        }
        #endregion

        /// <summary>
        /// 數據庫連接,根據數據庫類型自動識別，類型區分用配置名稱是否包含主要關鍵字
        /// MSSQL、MYSQL、ORACLE、SQLITE、MEMORY、NPGSQL
        /// </summary>
        /// <returns></returns>
        public DbConnection OpenSharedConnection()
        {
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            object connCode = yuebonCacheHelper.Get("CodeGeneratorDbConn");
            DbConnectionOptions dbConnectionOptions = DBServerProvider.GeDbConnectionOptions();
            DatabaseType dbType = DatabaseType.SqlServer;
            if (connCode!=null)
            {
                defaultSqlConnectionString = connCode.ToString();
                string dbTypeCache=yuebonCacheHelper.Get("CodeGeneratorDbType").ToString();
                dbType = (DatabaseType)Enum.Parse(typeof(DatabaseType), dbTypeCache);
            }
            else
            {
                defaultSqlConnectionString = dbConnectionOptions.ConnectionString;

                dbType = dbConnectionOptions.DatabaseType;
                TimeSpan expiresSliding = DateTime.Now.AddMinutes(30) - DateTime.Now;
                yuebonCacheHelper.Add("CodeGeneratorDbConn", defaultSqlConnectionString, expiresSliding, false);
                yuebonCacheHelper.Add("CodeGeneratorDbType", dbType, expiresSliding, false);
            }
            if (dbType == DatabaseType.SqlServer)
            {
                dbConnection = new SqlConnection(defaultSqlConnectionString);
            }
            else if (dbType == DatabaseType.MySql)
            {
                dbConnection = new MySqlConnection(defaultSqlConnectionString);
            }
            else if (dbType == DatabaseType.Oracle)
            {
                dbConnection = new OracleConnection(defaultSqlConnectionString);
            }
            else if (dbType == DatabaseType.SQLite)
            {
                dbConnection = new SqliteConnection(defaultSqlConnectionString);
            }
            else if (dbType == DatabaseType.Npgsql)
            {
                dbConnection = new NpgsqlConnection(defaultSqlConnectionString);
            }
            else
            {
                throw new NotSupportedException("The database is not supported");
            }
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
            return dbConnection;
        }



        #region 信息抽取

        /// <summary>
        /// 獲取數據庫的所有表的信息，
        /// 請定義TABLE_NAME 和 COMMENTS 字段的腳本
        /// </summary>
        /// <param name="sql">具體的腳本</param>
        /// <returns></returns>
        protected List<DbTableInfo> GetAllTablesInternal(string sql)
        {
            var list = new List<DbTableInfo>();
            using (DbConnection conn = OpenSharedConnection())
            {
                list = conn.Query<DbTableInfo>(sql).ToList();
            }
            return list;
        }
        /// <summary>
        /// 獲取所有數據庫信息，
        /// </summary>
        /// <param name="sql">具體的腳本</param>
        /// <returns></returns>
        protected List<DataBaseInfo> GetAllDataBaseInternal(string sql)
        {
            var list = new List<DataBaseInfo>();
            using (DbConnection conn = OpenSharedConnection())
            {
                list = conn.Query<DataBaseInfo>(sql).ToList();
            }
            return list;
        }


        /// <summary>
        /// 獲取數據庫的所有表的信息，
        /// 請定義TABLE_NAME 和 COMMENTS 字段的腳本
        /// </summary>
        /// <param name="sql">具體的腳本</param>
        /// <param name="info">分頁信息</param>
        /// <returns></returns>
        protected List<DbTableInfo> GetAllTablesInternal(string sql, PagerInfo info)
        {
            var list = new List<DbTableInfo>();
            using (DbConnection conn = OpenSharedConnection())
            {
                var reader = conn.QueryMultiple(sql);
                info.RecordCount = reader.ReadFirst<int>();
                list = reader.Read<DbTableInfo>().AsList();
            }
            return list;
        }
        /// <summary>
        /// 獲取表的所有字段名及字段類型
        /// </summary>
        /// <returns></returns>
        protected List<DbFieldInfo> GetAllColumnsInternal(string sql)
        {
            List<DbFieldInfo> list = new List<DbFieldInfo>();
            using (DbConnection conn = OpenSharedConnection())
            {
               IEnumerable<dynamic> dlist = conn.Query(sql);
                foreach(var item in dlist)
                {
                    DbFieldInfo dbFieldInfo = new DbFieldInfo
                    {
                        FieldName = item.FieldName,
                        //Increment = item.Increment == "1" ? true : false,
                        IsIdentity = item.IsIdentity == "1" ? true : false,
                        FieldType = item.FieldType.ToString(),
                        DataType = item.FieldType.ToString(),
                        FieldMaxLength = item.FieldMaxLength,
                        FieldPrecision = item.FieldPrecision,
                        FieldScale = item.FieldScale,
                        IsNullable = item.IsNullable == "1" ? true : false,
                        FieldDefaultValue = item.FieldDefaultValue,
                        Description = item.Description
                    };
                    list.Add(dbFieldInfo);
                }
            }
            return list;
        }

        #endregion
    }
}
