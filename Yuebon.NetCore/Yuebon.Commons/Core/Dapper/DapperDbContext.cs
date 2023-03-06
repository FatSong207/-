using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Text;
using Yuebon.Commons.Core.DataManager;
using Yuebon.Commons.Log;

namespace Yuebon.Commons.Core.Dapper
{
    /// <summary>
    /// 註冊的時候 InstancePerLifetimeScope
    /// 線程內唯一（也就是單個請求內唯一）
    /// </summary>
    public class DapperDbContext
    {

        private IDbConnection dbConnection { get; set; }

        /// <summary>
        /// 獲取的數據庫連接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="masterDb"></param>
        /// <returns></returns>
        public IDbConnection GetConnection<T>(bool masterDb = true) where T : class
        {
            if (dbConnection == null || dbConnection.State == ConnectionState.Closed)
            {
                dbConnection = DBServerProvider.GetDBConnection<T>(masterDb);

                //if (MiniProfiler.Current != null)
                //{
                //    dbConnection = new StackExchange.Profiling.Data.ProfiledDbConnection((DbConnection)dbConnection, MiniProfiler.Current);
                //}
            }
            return dbConnection;
        }

        /// <summary>
        /// 事務
        /// </summary>
        public IDbTransaction DbTransaction { get; set; }

        /// <summary>
        /// 是否已被提交
        /// </summary>
        public bool Committed { get; private set; } = true;

        /// <summary>
        /// 開啟事務
        /// </summary>
        public void BeginTransaction()
        {
            Committed = false;
            bool isClosed = dbConnection.State == ConnectionState.Closed;
            if (isClosed) dbConnection.Open();
            DbTransaction = dbConnection?.BeginTransaction();
        }

        /// <summary>
        /// 事務提交
        /// </summary>
        public void CommitTransaction()
        {
            DbTransaction?.Commit();
            Committed = true;
            Dispose();
        }

        /// <summary>
        /// 事務回滾
        /// </summary>
        public void RollBackTransaction()
        {
            DbTransaction?.Rollback();
            Committed = true;
            Dispose();
        }


        #region Dispose實現
        private bool disposedValue = false; // 要檢測冗余調用



        /// <summary>
        /// 釋放
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 釋放托管狀態(托管對象)。
                }

                // TODO: 釋放未托管的資源(未托管的對象)并在以下內容中替代終結器。
                // TODO: 將大型字段設置為 null。

                disposedValue = true;
            }
            if (dbConnection != null)
            {
                DbTransaction.Dispose();
                dbConnection.Dispose();
            }
        }
        /// <summary>
        /// 僅當以上 Dispose(bool disposing) 擁有用于釋放未托管資源的代碼時才替代終結器。
        /// </summary>
        public void Dispose()
        {
            // 請勿更改此代碼。將清理代碼放入以上 Dispose(bool disposing) 中。
            Dispose(true);

            DbTransaction?.Dispose();
            if (dbConnection.State == ConnectionState.Open)
                dbConnection?.Close();
            // TODO: 如果在以上內容中替代了終結器，則取消注釋以下行。
            // GC.SuppressFinalize(this);
        }

        #endregion

    }
}
