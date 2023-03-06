using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 日志搜索條件
    /// </summary>
    public class SearchLogModel : SearchInputDto<Log>
    {
        /// <summary>
        /// 添加開始時間 
        /// </summary>
        public string CreatorTime1
        {
            get; set;
        }
        /// <summary>
        /// 添加結束時間 
        /// </summary>
        public string CreatorTime2
        {
            get; set;
        }
    }
}
