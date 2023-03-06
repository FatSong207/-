
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 產權資料，數據實體對象
    /// </summary>
    [Table("Chaochi_BuildingBelonging")]
    [Serializable]
    public class BuildingBelonging : BaseEntity<string>, ICreationAudited, IModificationAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public BuildingBelonging()
        {
            
        }

        #region Property Members
        /// <summary>
        /// 前端刪除註記  //https://docs.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwithout-nrt
        /// https://stackoverflow.com/questions/19716568/ignore-property-on-model-property
        /// </summary>
        [NotMapped]
        [Computed]
        public virtual string NeedDel { get; set; }

        /// <summary>
        /// 建物Id
        /// </summary>
        public virtual string BuildingId { get; set; }

        /// <summary>
        /// 建物坐落建號
        /// </summary>
        public virtual string BuildingAccount { get; set; }

        /// <summary>
        /// 房東Id
        /// </summary>
        public virtual string LandlordId { get; set; }

        /// <summary>
        /// 房東身分證號
        /// </summary>
        public virtual string LandlordAccount { get; set; }

        /// <summary>
        /// 房東姓名
        /// </summary>
        public virtual string LandlordRealName { get; set; }

        /// <summary>
        /// 房東持份分子
        /// </summary>
        public virtual string HoldingsNumerator { get; set; }

        /// <summary>
        /// 房東持份分母
        /// </summary>
        public virtual string HoldingsDenominator { get; set; }

        /// <summary>
        /// 匯款銀行
        /// </summary>
        public virtual string RemittanceBank { get; set; }

        /// <summary>
        /// 匯款帳號
        /// </summary>
        public virtual string RemittanceAccount { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 最後修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最後修改用戶
        /// </summary>
        [MaxLength(50)]
        public virtual string LastModifyUserId { get; set; }

        ///// <summary>
        ///// 刪除時間
        ///// </summary>
        //public virtual DateTime? DeleteTime { get; set; }

        ///// <summary>
        ///// 刪除用戶
        ///// </summary>
        //[MaxLength(50)]
        //public virtual string DeleteUserId { get; set; }
        #endregion

    }
}