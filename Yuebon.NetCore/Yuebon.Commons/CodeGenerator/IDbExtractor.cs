using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.Pages;

namespace Yuebon.Commons.CodeGenerator
{
    /// <summary>
    /// 數據庫操作接口
    /// </summary>
    public interface IDbExtractor : IDisposable
    {
        /// <summary>
        /// 獲取數據庫信息
        /// </summary>
        /// <returns></returns>
        List<DataBaseInfo> GetAllDataBases();
        /// <summary>
        /// 獲取數據庫表的信息
        /// </summary>
        /// <param name="tablelist">數據庫表名稱</param>
        /// <returns></returns>
        List<DbTableInfo> GetWhereTables(string tablelist=null);

        /// <summary>
        /// 根據條件獲取數據庫的所有表的信息
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="fieldNameToSort"></param>
        /// <param name="isDescending"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        List<DbTableInfo> GetTablesWithPage(string strwhere, string fieldNameToSort, bool isDescending, PagerInfo info);

        /// <summary>
        /// 獲取表的所有字段名及字段類型
        /// </summary>
        /// <param name="tableName">數據表的名稱</param>
        /// <returns></returns>
        List<DbFieldInfo> GetAllColumns(string tableName);
    }
}
