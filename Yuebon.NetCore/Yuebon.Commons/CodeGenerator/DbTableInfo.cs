using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yuebon.Commons.CodeGenerator
{
    /// <summary>
    /// 數據表的信息
    /// </summary>
    public class DbTableInfo
    {
        /// <summary>
        /// 表格ID，表的名稱。
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表的別稱，或者描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 字段列表
        /// </summary>
        public List<DbFieldInfo> Fileds { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        public DbTableInfo()
        {
            Fileds = new List<DbFieldInfo>();
        }

        /// <summary>
        /// 獲取主鍵的名稱列表。
        /// </summary>
        /// <returns></returns>
        public List<string> GetIdentityList()
        {
            var list = Fileds.Where(x => x.IsIdentity);
            if (list == null) return null;
            return list.Select(x => x.FieldName).ToList();
        }
        /// <summary>
        /// 獲取主鍵字段列表
        /// </summary>
        /// <returns></returns>
        public List<DbFieldInfo> GetIdentityFields()
        {
            var list = Fileds.Where(x => x.IsIdentity);
            if (list == null) return null;
            return list.ToList();
        }
        /// <summary>
        /// 獲取可空字段。
        /// </summary>
        /// <returns></returns>
        public List<DbFieldInfo> GetIsNullableFields()
        {
            var list = Fileds.Where(x => x.IsNullable);
            if (list == null) return null;
            return list.ToList();
        }
        /// <summary>
        /// 獲取不可空字段。
        /// </summary>
        /// <returns></returns>
        public List<DbFieldInfo> GetNotNullableFields()
        {
            var list = Fileds.Where(x => !x.IsNullable);
            if (list == null) return null;
            return list.ToList();
        }
    }
}
