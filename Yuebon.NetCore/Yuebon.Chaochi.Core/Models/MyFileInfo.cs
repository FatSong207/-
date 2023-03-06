
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 產權資料，數據實體對象
    /// </summary>
    [Serializable]
    public class MyFileInfo
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public MyFileInfo()
        {
            
        }

        #region Property Members
        /// <summary>
        /// 檔名
        /// </summary>
        public virtual string FileName { get; set; }

        /// <summary>
        /// ImgUrl
        /// </summary>
        public virtual string ImgUrl { get; set; }

        public virtual string seq { get; set; }

        public virtual string title { get; set; }
        public virtual string Id { get; set; }

        public virtual string size { get; set; }
        public virtual string isAppear { get; set; }
        #endregion

    }
}