
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
    [Table("Chaochi_Internalform")]
    [Serializable]
    public class Internalform : BaseEntity<string>, ICreationAudited, IModificationAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public Internalform()
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
        public virtual string TBNO { get; set; }
        /// <summary>
        /// 表單名稱
        /// </summary>
        public virtual string FormName { get; set; }
        
        public virtual string Dept { get; set; }

        /// <summary>
        /// 類別
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 必填檢查
        /// 
        /// </summary>
        public virtual string RequiredLandlordN { get; set; }
        public virtual string RequiredLandlordC { get; set; }
        public virtual string RequiredBuilding { get; set; }
        public virtual string RequiredRenterN { get; set; }
        public virtual string RequiredRenterC { get; set; }

        /// <summary>
        /// 必須為已存在直
        /// </summary>
        public virtual string MustExistsLandLord { get; set; }
        public virtual string MustExistsBuilding { get; set; }
        public virtual string MustExistsRenter { get; set; }

        /// <summary>
        /// 歷史表單跟著誰
        /// </summary>
        public virtual string ArchiveLTo { get; set; }

        /// <summary>
        /// 是否有PDF模板?(用於一般表單)
        /// </summary>
        public virtual string HasPDFTemplate { get; set; }

        /// <summary>
        /// 種類(用於列表查詢的條件判斷)
        /// </summary>
        public virtual string ListType { get; set; }

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