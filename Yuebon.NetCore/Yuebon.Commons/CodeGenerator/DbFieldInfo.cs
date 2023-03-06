using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Yuebon.Commons.CodeGenerator
{
    /// <summary>
    /// 表的字段
    /// </summary>
    public class DbFieldInfo
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public DbFieldInfo()
        {
            FieldName = string.Empty;
            Description = string.Empty;
        }
        /// <summary>
        /// 字段名稱
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 系統數據類型，如 int
        /// </summary>
        public string DataType
        {
            get;
            set;
        }

        /// <summary>
        /// 數據庫里面存放的類型。
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// 代表小數位精度。
        /// </summary>
        public long? FieldScale { get; set; }
        /// <summary>
        /// 數據精度，僅數字類型有效，總共多少位數字（10進制）。
        /// 在MySql里面代表了字段長度
        /// </summary>
        public long? FieldPrecision { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? FieldMaxLength { get; set; }

        /// <summary>
        /// 可空
        /// </summary>
        public bool IsNullable { get; set; }
        /// <summary>
        /// 是否為主鍵字段
        /// </summary>
        public bool IsIdentity { get; set; }
        /// <summary>
        /// 【未用上】該字段是否自增
        /// </summary>
        public bool Increment { get; set; }


        /// <summary>
        /// 默認值
        /// </summary>
        public string FieldDefaultValue { get; set; }
       
    }
}
