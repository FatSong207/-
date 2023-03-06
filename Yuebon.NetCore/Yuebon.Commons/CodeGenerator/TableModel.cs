using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.CodeGenerator
{
    /// <summary>
    /// 表內容模型
    /// </summary>
    public class TableModel
    {
        /// <summary>
        /// 表名稱
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 字段序號
        /// </summary>
        public string FieldColorder { get; set; }


        /// <summary>
        /// 字段名稱
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 標識
        /// </summary>
        public string IsIdentity { get; set; }
        /// <summary>
        /// 主鍵
        /// </summary>
        public string Pkey { get; set; }
        /// <summary>
        /// 字段數據類型
        /// </summary>
        public string FieldDataType { get; set; }
        /// <summary>
        /// 字段長度
        /// </summary>
        public string FieldLength { get; set; }
        /// <summary>
        /// 小數位數
        /// </summary>
        public string FieldDecimalDigit { get; set; }
        /// <summary>
        /// 是否可以為空
        /// </summary>
        public string FieldIsNull { get; set; }
        /// <summary>
        /// 默認值
        /// </summary>
        public string FieldDefaultValue { get; set; }
        /// <summary>
        /// 字段描述
        /// </summary>
        public string FieldDescription { get; set; }
    }
}
