using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Models
{
    /// <summary>
    /// 定義多租戶實體信息
    /// </summary>
    public interface IMustHaveTenant
    {
        /// <summary>
        /// 租戶Id
        /// </summary>
        string TenantId {
            get;
            set;
        }
    }
}
