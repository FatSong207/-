using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// uniapp 地區選擇
    /// </summary>
    [Serializable]
    public class AreaPickerOutputDto
    {
        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 顯示內容
        /// </summary>		
        public  string label { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  List<AreaPickerOutputDto> children { get; set; }
    }
}
