using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.DependencyInjection;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Log;
using Yuebon.Commons.Repositories;
using Yuebon.Quartz.IRepositories;
using Yuebon.Quartz.Models;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Quartz.Repositories
{
    /// <summary>
    /// 定時任務倉儲接口的實現
    /// </summary>
    public class TaskManagerRepository : BaseRepository<TaskManager, string>, ITaskManagerRepository, IScopedDependency
    {
		public TaskManagerRepository()
        {
        }
        /// <summary>
        /// EF 數據操作注入
        /// </summary>
        /// <param name="context"></param>
        public TaskManagerRepository(IDbContextCore context) : base(context)
        {
        }



        /// <summary>
        /// 異步按條件設置數據有效性，將EnabledMark設置為1:有效，0-為無效
        /// 禁用時會將狀態更新為暫停
        /// </summary>
        /// <param name="bl">true為有效，false無效</param>
        /// <param name="where">條件</param>
        /// <param name="userId">操作用戶</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public override async Task<bool> SetEnabledMarkByWhereAsync(bool bl, string where, string userId = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            
            if (HasInjectionData(where))
            {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            string sql = $"update {tableName} set ";
            if (bl)
            {
                sql += "EnabledMark=1 ";
            }
            else
            {
                sql += "EnabledMark=0 ";
                sql += ",Status=0 ";
            }
            if (!string.IsNullOrEmpty(userId))
            {
                sql += ",LastModifyUserId='" + userId + "'";
            }
            DateTime lastModifyTime = DateTime.Now;
            sql += ",LastModifyTime=@LastModifyTime where " + where;

            var param = new List<Tuple<string, object>>();
            Tuple<string, object> tupel = new Tuple<string, object>(sql, new { @LastModifyTime = lastModifyTime });
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
        public override async Task<bool> DeleteSoftBatchAsync(bool bl, string where, string userId = null, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (HasInjectionData(where))
            {
                Log4NetHelper.Info(string.Format("檢測出SQL注入的惡意數據, {0}", where));
                throw new Exception("檢測出SQL注入的惡意數據");
            }
            if (string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            string sql = $"update {tableName} set ";
            if (bl)
            {
                sql += "DeleteMark=0 ";
            }
            else
            {
                sql += "DeleteMark=1 ";
                sql += ",Status=0 ";
            }
            if (!string.IsNullOrEmpty(userId))
            {
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

    }
}