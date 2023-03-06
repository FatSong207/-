using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// select2 地區選擇
    /// </summary>
    [Serializable]
    public class AreaSelect2OutDto
    {
        /// <summary>
        /// 值
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 顯示內容
        /// </summary>		
        public  string text { get; set; }
    }
}
