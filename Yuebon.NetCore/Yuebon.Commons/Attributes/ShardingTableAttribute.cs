using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Yuebon.Commons.Attributes
{
    /// <summary>
    /// 數據庫分表特性
    /// </summary>
    public class ShardingTableAttribute : TableAttribute
    {
        /// <summary>
        /// 分隔符
        /// </summary>
        public string Splitter { get; set; } = "_";
        /// <summary>
        /// 分表后綴格式。默認值：_yyyyMMdd
        /// </summary>
        public string Suffix { get; set; } = "yyyyMMdd";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public ShardingTableAttribute(string name) : base(name)
        {
            this.Suffix = "yyyyMMdd";
            this.Splitter = "_";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="splitter"></param>
        /// <param name="suffix"></param>
        public ShardingTableAttribute(string name, string splitter = "_", string suffix = "yyyyMMdd") : base(name)
        {
            this.Suffix = suffix;
        }
    }
}
