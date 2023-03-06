
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
    /// 客戶表_房東_自然人，數據實體對象
    /// </summary>
    [Table("Chaochi_CustomerLSBuilding")]
    [Serializable]
    public class CustomerLSBuilding : BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
        public CustomerLSBuilding()
        {

        }

        #region Property Members

        /// <summary>
        /// (自然人)身分證字號/(法人)統一編號
        /// </summary>
        public virtual string LSID { get; set; }
        /// <summary>
        /// (自然人)姓名/(法人)法人名稱
        /// </summary>
        public virtual string LSName { get; set; }

        /// <summary>
        /// 物件編號
        /// </summary>
        public virtual string BID { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址
        /// </summary>
        public virtual string BAdd { get; set; }

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

        #endregion

    }
}