using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Models
{
    /// <summary>
    /// 定義實體Hash功能，對實體的屬性值進行Hash，確定實體是否存在變化，
    /// 這些變化可用于系統初始化時確定是否需要進行數據同步
    /// </summary>
    public interface IEntityHash
    { }
}
