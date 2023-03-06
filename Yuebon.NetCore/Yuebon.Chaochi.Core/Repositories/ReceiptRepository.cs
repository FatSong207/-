using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System;
using System.Collections.Generic;

namespace Yuebon.Chaochi.Repositories
{
    /// <summary>
    /// 倉儲接口的實現
    /// </summary>
    public class ReceiptRepository : BaseRepository<Receipt, string>, IReceiptRepository
    {
		public ReceiptRepository()
        {
        }

        public ReceiptRepository(IDbContextCore context) : base(context)
        {

        }

        #region Dapper 操作

        //DapperConn 用于讀寫操作
        //DapperConnRead 用于只讀操作

        /// <summary>
        /// 根據領收據編號查詢收據編資料
        /// </summary>
        /// <param name="ReceiptId"></param>
        /// <returns></returns>
        public async Task<Receipt> GetReceiptById(string ReceiptId)
        {
            string sql = @"SELECT * FROM Chaochi_Receipt t WHERE t.ReceiptId = @ReceiptId";
            return await DapperConn.QueryFirstOrDefaultAsync<Receipt>(sql, new { ReceiptId = ReceiptId });
        }

        /// <summary>
        /// 異步設定領收據狀態
        /// </summary>
        /// <param name="status">狀態</param>
        /// <param name="receiptId">領收據編號</param>
        /// <param name="userId">使用者ID</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public async Task<bool> SetReceiptStatusAsync(string status, string id, string userId = null, IDbTransaction trans = null)
        {
            string sql = $"UPDATE Chaochi_Receipt SET RStatus = @RStatus";
                
            if (!string.IsNullOrEmpty(userId)) {
                sql += ", LastModifyUserId = '" + userId + "'";
            }

            DateTime lastModifyTime = DateTime.Now;
            sql += ", LastModifyTime = @LastModifyTime WHERE id = @id";

            var param = new List<Tuple<string, object>>();
            Tuple<string, object> tupel = new Tuple<string, object>(sql, new { @RStatus = status, id = id, @LastModifyTime = lastModifyTime });
            param.Add(tupel);
            Tuple<bool, string> result = await ExecuteTransactionAsync(param);
            return result.Item1;
        }

        #endregion


        #region EF 操作

        //DbContext 用于讀寫操作
        //DbContextRead 用于只讀操作

        #endregion
    }
}