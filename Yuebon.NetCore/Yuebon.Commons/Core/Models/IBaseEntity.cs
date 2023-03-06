using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Models
{
    /// <summary>
    /// 數據模型接口
    /// </summary>
    /// <typeparam name="TKey">實體主鍵類型</typeparam>
    public interface IBaseEntity<out TKey>: IEntity
    {
        /// <summary>
        /// 獲取 實體唯一標識，主鍵
        /// </summary>
        TKey Id { get; }
    }
}
