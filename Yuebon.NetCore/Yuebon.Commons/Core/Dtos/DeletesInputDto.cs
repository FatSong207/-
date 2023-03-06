using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Commons.Core.Dtos
{
    /// <summary>
    /// 批量物理刪除操作傳參
    /// </summary>
    [Serializable]
    public class DeletesInputDto
    {
        /// <summary>
        /// 主鍵Id集合
        /// </summary>
        public dynamic[] Ids { get; set; }
    }
}
