using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yuebon.AspNetCore.ViewModel
{
    /// <summary>
    /// 批量更新操作傳入參數，如設為禁用、有效、軟刪除；
    /// 物理刪除操作是Flag無效不用傳參
    /// </summary>
    [Serializable]
    public class UpdateEnableViewModel
    {
        /// <summary>
        /// 主鍵Id集合
        /// </summary>
        public dynamic[] Ids { get; set; }
        /// <summary>
        /// 有效標識，默認為1：即設為無效,0：有效
        /// </summary>
        public string Flag { get; set; }
    }
}
