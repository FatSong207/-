using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.CodeGenerator
{

    [Serializable]
    public class DbTableColumn
    {
        /// <summary>
        /// 表名稱
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string ColName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsIdentity { get; set; }
        /// <summary>
        /// 是否主鍵
        /// </summary>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// 字段數據類型
        /// </summary>
        public string ColumnType { get; set; }
        /// <summary>
        /// 字段數據長度
        /// </summary>
        public long? ColumnLength { get; set; }
        /// <summary>
        /// 是否允許為空
        /// </summary>
        public bool IsNullable { get; set; }
        /// <summary>
        /// 默認值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 字段說明
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// C#數據類型
        /// </summary>
        public string CSharpType { get; set; }
        /// <summary>
        /// 數據精度
        /// </summary>
        public int? DataPrecision { get; set; }
        /// <summary>
        /// 數據刻度
        /// </summary>
        public int? DataScale { get; set; }
    }
}
