
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
    public class EqFileInfo
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public EqFileInfo()
        {
            
        }

        #region Property Members
        /// <summary>
        /// 檔名
        /// </summary>
        public virtual string BeforeRepairClose { get; set; }

        /// <summary>
        /// ImgUrl
        /// </summary>
        public virtual string BeforeRepairFar { get; set; }

        public virtual string RepairingClose { get; set; }

        public virtual string RepairingFar { get; set; }
        public virtual string AfterRepairClose { get; set; }

        public virtual string AfterRepairFar { get; set; }
        #endregion

    }
}