using System;
using System.Collections.Generic;
using System.Text;


namespace Yuebon.AspNetCore.ViewModel
{
    /// <summary>
    /// Vue Cascader 級聯選擇模型
    /// </summary>
    [Serializable]
    public class VueCascaderModel
    {
        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 顯示名稱
        /// </summary>
        public string label { get; set; }


        /// <summary>
        /// 子集
        /// </summary>
        public List<VueCascaderModel> children { get; set; }
    }
}
