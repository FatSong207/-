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
    public class BuildingListOutputDto
    {

        #region Property Members

        /// <summary>
        /// 用戶主鍵
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 建物狀態
        /// </summary>
        public virtual string BState { get; set; }

        /// <summary>
        /// 建物屬性(住宅、店面、辦公室、停車位、商業登記、廣告牆面)
        /// </summary>
        public virtual string BPropoty { get; set; }

        /// <summary>
        /// 實際使用坪數(權狀坪數)
        /// </summary>
        public virtual string BRealPING { get; set; }

        /// <summary>
        /// 格局_1房
        /// </summary>
        public virtual string BPatten1R { get; set; }

        /// <summary>
        /// 格局_套房
        /// </summary>
        public virtual string BPatten1S { get; set; }

        /// <summary>
        /// 格局_2房
        /// </summary>
        public virtual string BPatten2R { get; set; }

        /// <summary>
        /// 格局_3房以上
        /// </summary>
        public virtual string BPatten3Rup { get; set; }

        /// <summary>
        /// 格局_4房以上
        /// </summary>
        public virtual string BPatten4Rup { get; set; }

        /// <summary>
        /// 格局_雅房
        /// </summary>
        public virtual string BBedsit { get; set; }

        /// <summary>
        /// 格局_開放空間
        /// </summary>
        public virtual string BOpenspace { get; set; }

        /// <summary>
        /// 管理費
        /// </summary>
        public virtual string BMFee { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址
        /// </summary>
        public virtual string BAdd { get; set; }

        ///// <summary>
        ///// 建築完成日期
        ///// </summary>
        //public virtual string BCDate { get; set; }

        ///// <summary>
        ///// 謄本面積(平方公尺)1
        ///// </summary>
        //public virtual string BArea { get; set; }

        ///// <summary>
        ///// 謄本面積（坪）2
        ///// </summary>
        //public virtual string BPing { get; set; }

        /// <summary>
        /// 代租租金1年/月
        /// </summary>
        public virtual string BTCRent { get; set; }

        ///// <summary>
        ///// 押金2
        ///// </summary>
        //public virtual string Bdeposit { get; set; }

        /// <summary>
        /// 屬於哪一個房東
        /// </summary>
        public virtual string BelongLID { get; set; }


        /// <summary>
        /// 創建日期
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public virtual string CreatorUserId { get; set; }

        public string CreatorUserName { get; set; }

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
