using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(Externalform))]
    [Serializable]
    public class InternalformInPutDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 表單代號
        /// </summary>
        public virtual string FormId { get; set; }

        /// <summary>
        /// 版本號
        /// </summary>
        public virtual string Vno { get; set; }

        /// <summary>
        /// 表單名稱
        /// </summary>
        public virtual string FormName { get; set; }

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
        /// 歷史表單跟著誰
        /// </summary>
        public virtual string ArchiveLTo { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 最後修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最後修改用戶
        /// </summary>
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

        ///// <summary>
        ///// 產權資料
        ///// </summary>
        //public virtual List<BuildingBelonging> BuildingBelongings { get; set; }


    }
}
