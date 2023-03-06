using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Dtos
{
    /// <summary>
    /// 定義輸入DTO
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IInputDto<TKey>
    {
        /// <summary>
        /// 獲取或設置 主鍵，唯一標識
        /// </summary>
        TKey Id { get; set; }
    }
}
