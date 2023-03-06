using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class InternalformOutputDto
    {

        #region Property Members

        /// <summary>
        /// 用戶主鍵
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 表單代號
        /// </summary>
        public virtual string FormId { get; set; }
        public virtual string TBNO { get; set; }
        /// <summary>
        /// 版本號
        /// </summary>
        public virtual string Vno { get; set; }

        /// <summary>
        /// 表單名稱
        /// </summary>
        public virtual string FormName { get; set; }

        public virtual bool IsSign { get; set; } = false;
        public virtual bool IsFileing { get; set; } = false;
        public virtual bool DataExist { get; set; } = false;
        /// <summary>
        /// 期數
        /// </summary>
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
        /// 是否有PDF模板?(用於一般表單)
        /// </summary>
        public virtual string HasPDFTemplate { get; set; }

        /// <summary>
        /// 歷史表單跟著誰
        /// </summary>
        public virtual string ArchiveLTo { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 最後修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }



        #endregion

    }
}
