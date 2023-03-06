using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.Models;
using Dapper;
using static Dapper.SqlMapper;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System;
using Yuebon.Commons.Log;
using Yuebon.Commons.Pages;

namespace Yuebon.Chaochi.Repositories
{
    /// <summary>
    /// 倉儲接口的實現
    /// </summary>
    public class ContractRepository : BaseRepository<Contract, string>, IContractRepository
    {
		public ContractRepository()
        {
        }

        public ContractRepository(IDbContextCore context) : base(context)
        {

        }

        #region Dapper 操作

        //DapperConn 用于讀寫操作
        //DapperConnRead 用于只讀操作

        /// <summary>
        /// 分頁查詢包含出租中,承租人,建物資訊
        /// </summary>
        /// <param name="condition">查詢條件字段需要加表別名</param>
        /// <param name="info">分頁信息</param>
        /// <param name="fieldToSort">排序字段，也需要加表別名</param>
        /// <param name="desc">排序方式</param>
        /// <param name="trans">事務</param>
        /// <returns></returns>
        public async Task<List<object>> FindWithPagerRelationAsync(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null)
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
            sb.AppendFormat("SELECT count(*) as RecordCount FROM (select t1.{0} FROM {1} t1 inner join Chaochi_ContractBuilding t2 on t1.CID = t2.CID where {2})  AS main_temp;", primaryKey, tableName, condition);
            sb.AppendFormat("SELECT * FROM (SELECT ROW_NUMBER() OVER (order by t1.{0}) AS rows ,t1.Id,t1.CID,t1.Version,t1.LSName,t1.SLName,t1.RNName,t1.MName,t1.MOID,t1.BAdd,t2.BRDStart,t2.BRDTEnd,t2.BTCRent,t1.SalesName,t1.CStatus FROM {1} t1 inner join Chaochi_ContractBuilding t2 on t1.CID = t2.CID where {2}) AS main_temp where rows BETWEEN {3} and {4}", strOrder, tableName, condition, startRows, endNum);
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

        #endregion


        #region EF 操作

        //DbContext 用于讀寫操作
        //DbContextRead 用于只讀操作

        #endregion
    }
}