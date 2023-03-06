using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.DataManager;
using Yuebon.Commons.DbContextCore;

namespace Yuebon.Commons.IDbContext
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbContextFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writeAndRead">指定讀、寫操作</param>
        /// <returns></returns>
        BaseDbContext CreateContext(WriteAndReadEnum writeAndRead);
        /// <summary>
        /// 創建數據庫讀寫上下文
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="writeAndRead">指定讀、寫操作</param>
        /// <returns></returns>
        BaseDbContext CreateContext<TEntity>(WriteAndReadEnum writeAndRead);
    }
}
