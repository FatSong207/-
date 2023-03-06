
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
    /// 表格B030101，數據實體對象
    /// </summary>
    [Table("Chaochi_B030101")]
    [Serializable]
    public class B030101 : BaseEntity<string>, ICreationAudited, IModificationAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public B030101()
        {
            
        }

        #region Property Members
        /// <summary>
        /// 物件編號
        /// </summary>
        public virtual string BID { get; set; }

        /// <summary>
        /// 本人(姓名)
        /// </summary>
        public virtual string BZName { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        public virtual string BZUnit { get; set; }

        /// <summary>
        /// 同意包租包管(B030101)/
        /// </summary>
        public virtual string BZ001 { get; set; }

        /// <summary>
        /// 同意代租代管(B030101)/
        /// </summary>
        public virtual string BZ002 { get; set; }

        /// <summary>
        /// 同意契約公證(B030101)/
        /// </summary>
        public virtual string BZ003 { get; set; }

        /// <summary>
        /// 不同意契約公證(B030101)/
        /// </summary>
        public virtual string BZ004 { get; set; }

        /// <summary>
        /// 同意稅賦減免(B030101)/
        /// </summary>
        public virtual string BZ005 { get; set; }

        /// <summary>
        /// 不同意稅賦減免
        /// </summary>
        public virtual string BZ006 { get; set; }

        /// <summary>
        /// 申請日期
        /// </summary>
        public virtual string BZApplyDate { get; set; }

        /// <summary>
        /// 申請日期(年)
        /// </summary>
        public virtual string BZApplyDate_Y { get; set; }

        /// <summary>
        /// 申請日期(月)
        /// </summary>
        public virtual string BZApplyDate_M { get; set; }

        /// <summary>
        /// 申請日期(日)
        /// </summary>
        public virtual string BZApplyDate_D { get; set; }

        /// <summary>
        /// 出租人承租人皆轉入本計畫(B030101)/
        /// </summary>
        public virtual string BZ401 { get; set; }

        /// <summary>
        /// 出租人轉入本計畫(B030101)/
        /// </summary>
        public virtual string BZ402 { get; set; }

        /// <summary>
        /// 身份證明文件影本（自然人）已檢附(B030101)/
        /// </summary>
        public virtual string BZ501V { get; set; }

        /// <summary>
        /// 公司登記事項影本（私法人）已檢附(B030101)/
        /// </summary>
        public virtual string BZ502V { get; set; }

        /// <summary>
        /// 代理人ID影本已檢附(B030101)/
        /// </summary>
        public virtual string BZ503V { get; set; }

        /// <summary>
        /// 第一類建物謄本1(B030101)/
        /// </summary>
        public virtual string BZ504_1C { get; set; }

        /// <summary>
        /// 第一類建物謄本（已檢附）2(B030101)/
        /// </summary>
        public virtual string BZ504_1V { get; set; }

        /// <summary>
        /// 建物所有權影本(B030101)/
        /// </summary>
        public virtual string BZ504_2C { get; set; }

        /// <summary>
        /// 建物使用執照影本(B030101)/
        /// </summary>
        public virtual string BZ504_3C { get; set; }

        /// <summary>
        /// 測量成果圖影本(B030101)/
        /// </summary>
        public virtual string BZ504_4C { get; set; }

        /// <summary>
        /// 建物證明文件已檢附(B030101)/
        /// </summary>
        public virtual string BZ504_2V { get; set; }

        /// <summary>
        /// 個資同意書已檢附(B030101)/
        /// </summary>
        public virtual string BZ505V { get; set; }

        /// <summary>
        /// 檢核表已檢附(B030101)/
        /// </summary>
        public virtual string BZ506V { get; set; }

        /// <summary>
        /// 匯款資訊已檢附(B030101)/
        /// </summary>
        public virtual string BZ507V { get; set; }

        /// <summary>
        /// 估價師租金簽註意見(B030101)/
        /// </summary>
        public virtual string BZ508_1C { get; set; }

        /// <summary>
        /// 代理人代表人授權書(B030101)/
        /// </summary>
        public virtual string BZ508_2C { get; set; }

        /// <summary>
        /// 共有住宅代表人授權書(B030101)/
        /// </summary>
        public virtual string BZ508_3C { get; set; }

        /// <summary>
        /// 原租賃契約影本(B030101)/
        /// </summary>
        public virtual string BZ508_4C { get; set; }

        /// <summary>
        /// 其他文件已檢附(B030101)/
        /// </summary>
        public virtual string BZ508V { get; set; }

        /// <summary>
        /// 出租人為住宅所有權人已檢附(B030101)/
        /// </summary>
        public virtual string BZ509V { get; set; }

        /// <summary>
        /// 租賃標的需坐落當地直轄市已檢附(B030101)/
        /// </summary>
        public virtual string BZ510V { get; set; }

        /// <summary>
        /// 建築住宅使用1(B030101)/
        /// </summary>
        public virtual string BZ511_1C { get; set; }

        /// <summary>
        /// 建築住宅使用2(B030101)/
        /// </summary>
        public virtual string BZ511_2C { get; set; }

        /// <summary>
        /// 建築住宅使用3(B030101)/
        /// </summary>
        public virtual string BZ511_3C { get; set; }

        /// <summary>
        /// 建築住宅使用4(B030101)/
        /// </summary>
        public virtual string BZ511_4C { get; set; }

        /// <summary>
        /// 合法建物條件已檢附(B030101)/
        /// </summary>
        public virtual string BZ511V { get; set; }

        /// <summary>
        /// 同意書日期
        /// </summary>
        public virtual string BZAgreeDate { get; set; }

        /// <summary>
        /// 同意書日期（年）
        /// </summary>
        public virtual string BZAgreeDate_Y { get; set; }

        /// <summary>
        /// 同意書日期（月）
        /// </summary>
        public virtual string BZAgreeDate_M { get; set; }

        /// <summary>
        /// 同意書日期（日）
        /// </summary>
        public virtual string BZAgreeDate_D { get; set; }

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