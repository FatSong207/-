using System;
using Yuebon.Commons.Enums;

namespace Yuebon.Commons.Pages
{

    /// <summary> 
    /// 根據各種不同數據庫生成不同分頁語句的輔助類 PagerHelper
    /// </summary> 
    public class PagerHelper
    {
        #region 成員變量

        private string tableName;//待查詢表或自定義查詢語句
        private string fieldsToReturn = "*";//需要返回的列
        private string fieldNameToSort = string.Empty;//排序字段名稱
        private int pageSize = 10;//頁尺寸,就是一頁顯示多少條記錄
        private int pageIndex = 1;//當前的頁碼
        private bool isDescending = false;//是否以降序排列
        private string strwhere = string.Empty;//檢索條件(注意: 不要加 where)

        #endregion

        #region 屬性對象

        /// <summary>
        /// 待查詢表或自定義查詢語句
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        /// <summary>
        /// 需要返回的列
        /// </summary>
        public string FieldsToReturn
        {
            get { return fieldsToReturn; }
            set { fieldsToReturn = value; }
        }

        /// <summary>
        /// 排序字段名稱
        /// </summary>
        public string FieldNameToSort
        {
            get { return fieldNameToSort; }
            set { fieldNameToSort = value; }
        }

        /// <summary>
        /// 頁尺寸,就是一頁顯示多少條記錄
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        /// <summary>
        /// 當前的頁碼
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        /// <summary>
        /// 是否以降序排列結果
        /// </summary>
        public bool IsDescending
        {
            get { return isDescending; }
            set { isDescending = value; }
        }

        /// <summary>
        /// 檢索條件(注意: 不要加 where)
        /// </summary>
        public string StrWhere
        {
            get { return strwhere; }
            set { strwhere = value; }
        }

        /// <summary>
        /// 表或Sql語句包裝屬性
        /// </summary>
        internal string TableOrSqlWrapper
        {
            get
            {
                bool isSql = tableName.ToLower().Contains("from");
                if (isSql)
                {
                    return string.Format("({0}) AA ", tableName);//如果是Sql語句，則加括號后再使用
                }
                else
                {
                    return tableName;//如果是表名，則直接使用
                }
            }
        }

        #endregion

        #region 構造函數

        /// <summary>
        /// 默認構造函數，其他通過屬性設置
        /// </summary>
        public PagerHelper()
        {
        }

        /// <summary>
        /// 完整的構造函數,可以包含條件,返回記錄字段等條件
        /// </summary>
        /// <param name="tableName">表名稱，可以自定義查詢語句</param>
        /// <param name="fieldsToReturn">需要返回的列</param>
        /// <param name="fieldNameToSort">排序字段名稱</param>
        /// <param name="pageSize">每頁顯示數量</param>
        /// <param name="pageIndex">當前的頁碼</param>
        /// <param name="isDescending">是否以降序排列</param>
        /// <param name="strwhere">檢索條件</param>
        public PagerHelper(string tableName, string fieldsToReturn, string fieldNameToSort,
            int pageSize, int pageIndex, bool isDescending, string strwhere)
        {
            this.tableName = tableName;
            this.fieldsToReturn = fieldsToReturn;
            this.fieldNameToSort = fieldNameToSort;
            this.pageSize = pageSize;
            this.pageIndex = pageIndex;
            this.isDescending = isDescending;
            this.strwhere = strwhere;
        }

        #endregion

        #region 各種數據庫Sql分頁查詢，不依賴于存儲過程
        /// <summary>
        /// 不依賴于存儲過程的分頁(Oracle)
        /// </summary>
        /// <param name="isDoCount">如果isDoCount為True，返回總數統計Sql；否則返回分頁語句Sql</param>
        /// <returns></returns>
        private string GetOracleSql(bool isDoCount)
        {
            string sql = "";
            if (string.IsNullOrEmpty(this.strwhere))
            {
                this.strwhere = " (1=1) ";
            }

            if (isDoCount)//執行總數統計
            {
                sql = string.Format("select count(*) as Total from {0} Where {1} ", this.TableOrSqlWrapper, this.strwhere);
            }
            else
            {
                string strOrder = string.Format(" order by {0} {1}", this.fieldNameToSort, this.isDescending ? "DESC" : "ASC");

                int minRow = pageSize * (pageIndex - 1);
                int maxRow = pageSize * pageIndex;
                string selectSql = string.Format("select {0} from {1} Where {2} {3}", fieldsToReturn, this.TableOrSqlWrapper, this.strwhere, strOrder);
                sql = string.Format(@"select b.* from
                           (select a.*, rownum as rowIndex from({2}) a) b
                           where b.rowIndex > {0} and b.rowIndex <= {1}", minRow, maxRow, selectSql);
            }

            return sql;
        }

        /// <summary>
        /// 不依賴于存儲過程的分頁(SqlServer)
        /// </summary>
        /// <param name="isDoCount">如果isDoCount為True，返回總數統計Sql；否則返回分頁語句Sql</param>
        /// <param name="isSql2008">是否是Sql server2008及低版本，默認為false</param>
        /// <returns></returns>
        private string GetSqlServerSql(bool isDoCount,bool isSql2008=false)
        {
            string sql = "";
            if (string.IsNullOrEmpty(this.strwhere))
            {
                this.strwhere = " (1=1) ";
            }

            if (isDoCount)//執行總數統計
            {
                sql = string.Format("select count(*) as Total from {0} Where {1} ", this.TableOrSqlWrapper, this.strwhere);
            }
            else
            {
                string strOrder = string.Format(" order by {0} {1}", this.fieldNameToSort, this.isDescending ? "DESC" : "ASC");
                int minRow = pageSize * (pageIndex - 1) + 1;
                int maxRow = pageSize * pageIndex;
                if (isSql2008)
                {
                    sql = string.Format("SELECT * FROM ( SELECT ROW_NUMBER() OVER (order by {0}) AS rows ,{1} FROM {2} where {3}) AS main_temp where rows BETWEEN {4} and {5}", strOrder, fieldsToReturn, TableOrSqlWrapper, strwhere, minRow, maxRow);
                }
                else
                {
                    sql = string.Format(@"With Paging AS
                ( SELECT ROW_NUMBER() OVER ({0}) as RowNumber, {1} FROM {2} Where {3})
                SELECT * FROM Paging WHERE RowNumber Between {4} and {5}", strOrder, this.fieldsToReturn, this.TableOrSqlWrapper, this.strwhere,
                    minRow, maxRow);
                }
            }

            return sql;
        }

        /// <summary>
        /// 不依賴于存儲過程的分頁(Access)
        /// </summary>
        /// <param name="isDoCount">如果isDoCount為True，返回總數統計Sql；否則返回分頁語句Sql</param>
        /// <returns></returns>
        private string GetAccessSql(bool isDoCount)
        {
            string sql = "";
            if (string.IsNullOrEmpty(this.strwhere))
            {
                this.strwhere = " (1=1) ";
            }

            if (isDoCount)//執行總數統計
            {
                sql = string.Format("select count(*) as Total from {0} Where {1} ", this.TableOrSqlWrapper, this.strwhere);
            }
            else
            {
                string strTemp = string.Empty;
                string strOrder = string.Empty;
                if (this.isDescending)
                {
                    strTemp = "<(select min";
                    strOrder = string.Format(" order by [{0}] desc", this.fieldNameToSort);
                }
                else
                {
                    strTemp = ">(select max";
                    strOrder = string.Format(" order by [{0}] asc", this.fieldNameToSort);
                }

                sql = string.Format("select top {0} {1} from {2} ", this.pageSize, this.fieldsToReturn, this.TableOrSqlWrapper);

                //如果是第一頁就執行以上代碼，這樣會加快執行速度
                if (this.pageIndex == 1)
                {
                    sql += string.Format(" Where {0} ", this.strwhere);
                    sql += strOrder;
                }
                else
                {
                    sql += string.Format(" Where [{0}] {1} ([{0}]) from (select top {2} [{0}] from {3} where {5} {4} ) as tblTmp) and {5} {4}",
                        this.fieldNameToSort, strTemp, (this.pageIndex - 1) * this.pageSize, this.TableOrSqlWrapper, strOrder, this.strwhere);
                }
            }

            return sql;
        }

        /// <summary>
        /// 不依賴于存儲過程的分頁(MySql)
        /// </summary>
        /// <param name="isDoCount">如果isDoCount為True，返回總數統計Sql；否則返回分頁語句Sql</param>
        /// <returns></returns>
        private string GetMySqlSql(bool isDoCount)
        {
            string sql = "";
            if (string.IsNullOrEmpty(this.strwhere))
            {
                this.strwhere = " (1=1) ";
            }

            if (isDoCount)//執行總數統計
            {
                sql = string.Format("select count(Id) as Total from {0} Where {1}", this.TableOrSqlWrapper, this.strwhere);
            }
            else
            {
                //SELECT * FROM 表名稱 LIMIT M,N 
                string strOrder = string.Format(" order by {0} {1}", this.fieldNameToSort, this.isDescending ? "DESC" : "ASC");

                int minRow = pageSize * (pageIndex - 1);
                int maxRow = pageSize * pageIndex;
                sql = string.Format("select {0} from {1} where Id IN(select t.Id from (select Id from {1} Where {2} {3} limit {4},{5})as t);",
                    fieldsToReturn, this.TableOrSqlWrapper, this.strwhere, strOrder, minRow, pageSize);
            }

            return sql;
        }

        /// <summary>
        /// 不依賴于存儲過程的分頁(SQLite)
        /// </summary>
        /// <param name="isDoCount">如果isDoCount為True，返回總數統計Sql；否則返回分頁語句Sql</param>
        /// <returns></returns>
        private string GetSQLiteSql(bool isDoCount)
        {
            string sql = "";
            if (string.IsNullOrEmpty(this.strwhere))
            {
                this.strwhere = " (1=1) ";
            }

            if (isDoCount)//執行總數統計
            {
                sql = string.Format("select count(*) as Total from {0} Where {1} ", this.TableOrSqlWrapper, this.strwhere);
            }
            else
            {
                //SELECT * FROM 表名稱 LIMIT M,N 
                string strOrder = string.Format(" order by {0} {1}", this.fieldNameToSort, this.isDescending ? "DESC" : "ASC");

                int minRow = pageSize * (pageIndex - 1);
                int maxRow = pageSize * pageIndex;
                sql = string.Format("select {0} from {1} Where {2} {3} LIMIT {4},{5}",
                    fieldsToReturn, this.TableOrSqlWrapper, this.strwhere, strOrder, minRow, pageSize);
            }

            return sql;
        }

        /// <summary>
        /// 獲取對應數據庫的分頁語句（指定數據庫類型）
        /// </summary>
        /// <param name="isDoCount">如果isDoCount為True，返回總數統計Sql；否則返回分頁語句Sql</param>
        /// <param name="dbType">數據庫類型枚舉</param>
        public string GetPagingSql( bool isDoCount, DatabaseType dbType)
        {
            string sql = "";
            switch (dbType)
            {
                case DatabaseType.Access:
                    sql = GetAccessSql(isDoCount);
                    break;
                case DatabaseType.SqlServer:
                    sql = GetSqlServerSql(isDoCount);
                    break;
                case DatabaseType.Oracle:
                    sql = GetOracleSql(isDoCount);
                    break;
                case DatabaseType.MySql:
                    sql = GetMySqlSql(isDoCount);
                    break;
                case DatabaseType.SQLite:
                    sql = GetSQLiteSql(isDoCount);
                    break;
            }
            return sql;
        }


        /// <summary>
        /// 數據庫類型
        /// </summary>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        private DatabaseType GetDataBaseType(string databaseType)
        {
            DatabaseType returnValue = DatabaseType.SqlServer;
            foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
            {
                if (dbType.ToString().Equals(databaseType, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = dbType;
                    break;
                }
            }
            return returnValue;
        }
        #endregion
    }


}