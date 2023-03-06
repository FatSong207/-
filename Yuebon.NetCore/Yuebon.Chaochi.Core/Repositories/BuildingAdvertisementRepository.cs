using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.Models;
using Dapper;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System;
using Yuebon.Commons.Log;
using Yuebon.Commons.Pages;

namespace Yuebon.Chaochi.Repositories
{
    /// <summary>
    /// 建物廣告管理倉儲接口的實現
    /// </summary>
    public class BuildingAdvertisementRepository : BaseRepository<BuildingAdvertisement, string>, IBuildingAdvertisementRepository
    {
		public BuildingAdvertisementRepository()
        {
        }

        public BuildingAdvertisementRepository(IDbContextCore context) : base(context)
        {

        }

        #region Dapper 操作

        //DapperConn 用于讀寫操作
        //DapperConnRead 用于只讀操作

        /// <summary>
        /// 根據建物地址查詢建物廣告資訊
        /// </summary>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        public async Task<BuildingAdvertisement> GetByBAdd(string BAdd)
        {
            string sql = @"SELECT * FROM Chaochi_BuildingAdvertisement t WHERE t.BAdd = @BAdd";
            return await DapperConn.QueryFirstOrDefaultAsync<BuildingAdvertisement>(sql, new { BAdd = BAdd });
        }

        /// <summary>
        /// 分頁查詢包含建物資訊
        /// </summary>
        /// <param name="condition">查詢條件字段需要加表別名</param>
        /// <param name="info">分頁信息</param>
        /// <param name="fieldToSort">排序字段，也需要加表別名</param>
        /// <param name="desc">排序方式</param>
        /// <param name="trans">事務</param>
        /// <returns></returns>
        public async Task<List<object>> FindWithPagerRelationBuildingAsync(string condition, PagerInfo info, string fieldToSort, bool desc, IDbConnection conn = null, IDbTransaction trans = null)
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
            sb.AppendFormat("SELECT count(*) as RecordCount FROM (select t1.{0} FROM {1} t1 inner join Chaochi_Building t2 on t1.BAdd = t2.BAdd where {2})  AS main_temp;", primaryKey, tableName, condition);
            //sb.AppendFormat("SELECT * FROM (SELECT ROW_NUMBER() OVER (order by  {0}) AS rows ,t1.{1},t2.Account as Account,t2.NickName as NickName,t2.RealName as RealName,t2.HeadIcon as HeadIcon ,t2.MobilePhone as MobilePhone  FROM {2} t1 inner join Sys_User t2 on t1.CreatorUserId = t2.Id " +
            //    "where {3}) AS main_temp where rows BETWEEN {4} and {5}", strOrder, selectedFields, tableName, condition, startRows, endNum);
            sb.AppendFormat("SELECT * FROM (SELECT ROW_NUMBER() OVER (order by {0}) AS rows ,t1.Id AS Id,t2.BState AS BState,t2.BPropoty AS BPropoty,t1.BAStatus AS BAStatus,t1.BAdd AS BAdd,t1.BADays AS BADays,t1.BRDays AS BRDays,t2.BRoom AS BRoom,t2.BLD AS BLD,t2.BBath AS BBath,t2.BRealPING AS BRealPING,t2.BTRent AS BTRent,t2.CreatorUserId AS sales,t2.CreatorUserDeptId AS dept,t1.BRStartDate AS BRStartDate,t1.BAStartDate AS BAStartDate,t1.BAURL AS BAURL FROM {1} t1 inner join Chaochi_Building t2 on t1.BAdd = t2.BAdd " +
                "where {2}) AS main_temp where rows BETWEEN {3} and {4}", strOrder, tableName, condition, startRows, endNum);
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

        /// <summary>
        /// 新增建物廣告
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        public async Task<bool> InsertAsync(BuildingAdvertisement entity, IDbTransaction trans = null)
        {
            DbContext.GetDbSet<BuildingAdvertisement>().Add(entity);

            try {
                return await DbContext.SaveChangesAsync() > 0;
            } catch (System.Exception) {

                throw;
            }
        }

        #endregion
    }
}