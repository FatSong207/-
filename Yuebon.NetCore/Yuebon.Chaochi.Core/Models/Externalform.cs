
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
    /// 外部表單，數據實體對象
    /// </summary>
    [Table("Chaochi_Externalform")]
    [Serializable]
    public class Externalform : BaseEntity<string>, ICreationAudited, IModificationAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public Externalform()
        {
            
        }

        #region Property Members
        /// <summary>
        /// 表單代號
        /// </summary>
        public virtual string FormId { get; set; }

        /// <summary>
        /// 版本號
        /// </summary>
        public virtual string Vno { get; set; }

        /// <summary>
        /// TBNO 存放BZ欄位判斷
        /// </summary>
        public virtual string TBNO { get; set; }

        /// <summary>
        /// 表單名稱
        /// </summary>
        public virtual string FormName { get; set; }

        /// <summary>
        /// 自訂標籤
        /// </summary>
        public virtual string CustTag { get; set; }
        
        /// <summary>
        /// level
        /// </summary>
        public string level
        {
            get; set;
        }

        /// <summary>
        /// 必填檢查欄位 房東
        /// </summary>
        public virtual string RequiredLandlord { get; set; }

        /// <summary>
        /// 必填檢查欄位 建物
        /// </summary>
        public virtual string RequiredBuilding { get; set; }

        /// <summary>
        /// 必填檢查欄位 客戶
        /// </summary>
        public virtual string RequiredRenter { get; set; }

        /// <summary>
        /// 必需為已存在值 房東
        /// </summary>
        public virtual string MustExistsLandLord { get; set; }

        /// <summary>
        /// 必需為已存在值 建物
        /// </summary>
        public virtual string MustExistsBuilding { get; set; }

        /// <summary>
        /// 必需為已存在值 房客
        /// </summary>
        public virtual string MustExistsRenter { get; set; }

        /// <summary>
        /// 歷史存檔 房東 建物 客戶  打勾 (單選)
        /// </summary>
        public virtual string ArchiveLTo { get; set; }

        /// <summary>
        /// 是否為無主表單
        /// </summary>
        public string IsNoKey { get; set; }

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