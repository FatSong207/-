
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
    /// 房屋物件，數據實體對象
    /// </summary>
    [Table("Chaochi_BuildingEq")]
    [Serializable]
    public class BuildingEq : BaseEntity<string>
    {
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
        public BuildingEq()
        {

        }

        #region 建物設備相關

        public virtual string BAdd { get; set; }
        public virtual string  EqName { get; set; }
        public virtual string Category { get; set; }
        public virtual string EqCount { get; set; }
        public virtual string EqBrand { get; set; }
        public virtual string Sort { get; set; }

        #endregion 建物設備相關
    }
}