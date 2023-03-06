using System;

namespace Yuebon.Chaochi.Core.Dtos
{
    [Serializable]
    public class A000003OutputDto
    {
        #region 出租人

        public virtual string LCID { get; set; }

        public virtual string LCName { get; set; }

        public virtual string LCTel_1 { get; set; }

        public virtual string LCTel_2 { get; set; }

        public virtual string LCRep { get; set; }

        public virtual string LCCP { get; set; }

        public virtual string LCCPCell { get; set; }

        public virtual string LCCPEMail { get; set; }

        public virtual string LCAdd { get; set; }

        public virtual string LCAdd_1 { get; set; }

        public virtual string LCAdd_1_1 { get; set; }

        public virtual string LCAdd_1_2 { get; set; }

        public virtual string LCAdd_2 { get; set; }

        public virtual string LCAdd_2_1 { get; set; }

        public virtual string LCAdd_2_2 { get; set; }

        public virtual string LCAdd_2_3 { get; set; }

        public virtual string LCAdd_2_4 { get; set; }

        public virtual string LCAdd_3 { get; set; }

        public virtual string LCAdd_3_1 { get; set; }

        public virtual string LCAdd_3_2 { get; set; }

        public virtual string LCAdd_4 { get; set; }

        public virtual string LCAdd_5 { get; set; }

        public virtual string LCAdd_6 { get; set; }

        public virtual string LCAdd_7 { get; set; }

        public virtual string LCAdd_8 { get; set; }

        public virtual string LCAdd_9 { get; set; }

        public virtual string LCAddSame { get; set; }

        public virtual string LCAddC_1 { get; set; }

        public virtual string LCAddC_1_1 { get; set; }

        public virtual string LCAddC_1_2 { get; set; }

        public virtual string LCAddC_2 { get; set; }

        public virtual string LCAddC_2_1 { get; set; }

        public virtual string LCAddC_2_2 { get; set; }

        public virtual string LCAddC_2_3 { get; set; }

        public virtual string LCAddC_2_4 { get; set; }

        public virtual string LCAddC_3 { get; set; }

        public virtual string LCAddC_3_1 { get; set; }

        public virtual string LCAddC_3_2 { get; set; }

        public virtual string LCAddC_4 { get; set; }

        public virtual string LCAddC_5 { get; set; }

        public virtual string LCAddC_6 { get; set; }

        public virtual string LCAddC_7 { get; set; }

        public virtual string LCAddC_8 { get; set; }

        public virtual string LCAddC_9 { get; set; }

        #endregion

        #region 匯款資料

        public virtual string LAName { get; set; }
        public virtual string LBankName { get; set; }
        public virtual string LBankNo { get; set; }
        public virtual string LBranchName { get; set; }
        public virtual string LBranchNo { get; set; }
        public virtual string LANo { get; set; }

        #endregion

        #region BZ欄位

        /// <summary>
        /// 是否已歸檔
        /// </summary>
        public virtual string IsFiled { get; set; }

        /// <summary>
        /// BZ001
        /// </summary>
        public virtual string BZ001 { get; set; }

        /// <summary>
        /// BZ002
        /// </summary>
        public virtual string BZ002 { get; set; }

        /// <summary>
        /// BZ003
        /// </summary>
        public virtual string BZ003 { get; set; }

        /// <summary>
        /// BZ004
        /// </summary>
        public virtual string BZ004 { get; set; }

        /// <summary>
        /// BZ005
        /// </summary>
        public virtual string BZ005 { get; set; }

        /// <summary>
        /// BZ006
        /// </summary>
        public virtual string BZ006 { get; set; }

        /// <summary>
        /// BZ007
        /// </summary>
        public virtual string BZ007 { get; set; }

        /// <summary>
        /// BZ008
        /// </summary>
        public virtual string BZ008 { get; set; }

        /// <summary>
        /// BZ009
        /// </summary>
        public virtual string BZ009 { get; set; }

        /// <summary>
        /// BZ010
        /// </summary>
        public virtual string BZ010 { get; set; }

        /// <summary>
        /// BZ011
        /// </summary>
        public virtual string BZ011 { get; set; }

        /// <summary>
        /// BZ012
        /// </summary>
        public virtual string BZ012 { get; set; }

        /// <summary>
        /// BZ013
        /// </summary>
        public virtual string BZ013 { get; set; }

        /// <summary>
        /// BZ014
        /// </summary>
        public virtual string BZ014 { get; set; }

        /// <summary>
        /// BZ015
        /// </summary>
        public virtual string BZ015 { get; set; }

        /// <summary>
        /// BZ016
        /// </summary>
        public virtual string BZ016 { get; set; }

        /// <summary>
        /// BZ017
        /// </summary>
        public virtual string BZ017 { get; set; }

        /// <summary>
        /// BZ018
        /// </summary>
        public virtual string BZ018 { get; set; }

        /// <summary>
        /// BZ019
        /// </summary>
        public virtual string BZ019 { get; set; }

        /// <summary>
        /// BZ020
        /// </summary>
        public virtual string BZ020 { get; set; }

        /// <summary>
        /// BZ021
        /// </summary>
        public virtual string BZ021 { get; set; }

        /// <summary>
        /// BZ022
        /// </summary>
        public virtual string BZ022 { get; set; }

        /// <summary>
        /// BZ023
        /// </summary>
        public virtual string BZ023 { get; set; }

        /// <summary>
        /// BZ024
        /// </summary>
        public virtual string BZ024 { get; set; }

        /// <summary>
        /// BZ025
        /// </summary>
        public virtual string BZ025 { get; set; }

        /// <summary>
        /// BZ026
        /// </summary>
        public virtual string BZ026 { get; set; }

        /// <summary>
        /// BZ027
        /// </summary>
        public virtual string BZ027 { get; set; }

        /// <summary>
        /// BZ028
        /// </summary>
        public virtual string BZ028 { get; set; }

        /// <summary>
        /// BZ029
        /// </summary>
        public virtual string BZ029 { get; set; }

        #endregion BZ欄位

        #region 簽名

        public string SignatureImgPath_1 { get; set; }


        #endregion 簽名
    }
}
