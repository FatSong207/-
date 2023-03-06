using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{

    /// <summary>
    /// 用戶搜索條件
    /// </summary>
    public class SearchBuildingModel : SearchInputDto<Building>
    {
        /// <summary>
        /// 產權建物門牌地址：戶籍地址
        /// </summary>
        public virtual string BAdd { get; set; }

    }
}
