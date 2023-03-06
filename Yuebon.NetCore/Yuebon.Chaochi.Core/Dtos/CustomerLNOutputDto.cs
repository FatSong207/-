using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class CustomerLNOutputDto
    {

        #region Property Members
        public virtual string Id { get; set; }
        public virtual string LNName { get; set; }
        public virtual string LNBirthday { get; set; }
        public virtual string LNID { get; set; }
        public virtual string LNGender1 { get; set; }
        public virtual string LNGender2 { get; set; }
        public virtual string LNTel { get; set; }
        public virtual string LNTel_1 { get; set; }
        public virtual string LNTel_2 { get; set; }
        public virtual string LNTelN { get; set; }
        public virtual string LNTelN_1 { get; set; }
        public virtual string LNTelN_2 { get; set; }
        public virtual string LNCell { get; set; }
        public virtual string LNMail { get; set; }
        public virtual string LNLine { get; set; }
        public virtual string LNAdd_1 { get; set; }
        public virtual string LNAdd_1_1 { get; set; }
        public virtual string LNAdd_1_2 { get; set; }
        public virtual string LNAdd_2 { get; set; }
        public virtual string LNAdd_2_1 { get; set; }
        public virtual string LNAdd_2_2 { get; set; }
        public virtual string LNAdd_2_3 { get; set; }
        public virtual string LNAdd_2_4 { get; set; }
        public virtual string LNAdd_3 { get; set; }
        public virtual string LNAdd_3_1 { get; set; }
        public virtual string LNAdd_3_2 { get; set; }
        public virtual string LNAdd_4 { get; set; }
        public virtual string LNAdd_5 { get; set; }
        public virtual string LNAdd_6 { get; set; }
        public virtual string LNAdd_7 { get; set; }
        public virtual string LNAdd_8 { get; set; }
        public virtual string LNAdd_9 { get; set; }
        public virtual string LNAddC_1 { get; set; }
        public virtual string LNAddC_1_1 { get; set; }
        public virtual string LNAddC_1_2 { get; set; }
        public virtual string LNAddC_2 { get; set; }
        public virtual string LNAddC_2_1 { get; set; }
        public virtual string LNAddC_2_2 { get; set; }
        public virtual string LNAddC_2_3 { get; set; }
        public virtual string LNAddC_2_4 { get; set; }
        public virtual string LNAddC_3 { get; set; }
        public virtual string LNAddC_3_1 { get; set; }
        public virtual string LNAddC_3_2 { get; set; }
        public virtual string LNAddC_4 { get; set; }
        public virtual string LNAddC_5 { get; set; }
        public virtual string LNAddC_6 { get; set; }
        public virtual string LNAddC_7 { get; set; }
        public virtual string LNAddC_8 { get; set; }
        public virtual string LNAddC_9 { get; set; }

        public virtual string LNAGName_A { get; set; }
        public virtual string LNAGID_A { get; set; }
        public virtual string LNAGAdd_A { get; set; }
        public virtual string LNAGAdd_1_A { get; set; }
        public virtual string LNAGAdd_1_1_A { get; set; }
        public virtual string LNAGAdd_1_2_A { get; set; }
        public virtual string LNAGAdd_2_A { get; set; }
        public virtual string LNAGAdd_2_1_A { get; set; }
        public virtual string LNAGAdd_2_2_A { get; set; }
        public virtual string LNAGAdd_2_3_A { get; set; }
        public virtual string LNAGAdd_2_4_A { get; set; }
        public virtual string LNAGAdd_3_A { get; set; }
        public virtual string LNAGAdd_3_1_A { get; set; }
        public virtual string LNAGAdd_3_2_A { get; set; }
        public virtual string LNAGAdd_4_A { get; set; }
        public virtual string LNAGAdd_5_A { get; set; }
        public virtual string LNAGAdd_6_A { get; set; }
        public virtual string LNAGAdd_7_A { get; set; }
        public virtual string LNAGAdd_8_A { get; set; }
        public virtual string LNAGAdd_9_A { get; set; }
        public virtual string LNAGAddSame { get; set; }
        public virtual string LNAGAddC_A { get; set; }
        public virtual string LNAGAddC_1_A { get; set; }
        public virtual string LNAGAddC_1_1_A { get; set; }
        public virtual string LNAGAddC_1_2_A { get; set; }
        public virtual string LNAGAddC_2_A { get; set; }
        public virtual string LNAGAddC_2_1_A { get; set; }
        public virtual string LNAGAddC_2_2_A { get; set; }
        public virtual string LNAGAddC_2_3_A { get; set; }
        public virtual string LNAGAddC_2_4_A { get; set; }
        public virtual string LNAGAddC_3_A { get; set; }
        public virtual string LNAGAddC_3_1_A { get; set; }
        public virtual string LNAGAddC_3_2_A { get; set; }
        public virtual string LNAGAddC_4_A { get; set; }
        public virtual string LNAGAddC_5_A { get; set; }
        public virtual string LNAGAddC_6_A { get; set; }
        public virtual string LNAGAddC_7_A { get; set; }
        public virtual string LNAGAddC_8_A { get; set; }
        public virtual string LNAGAddC_9_A { get; set; }

        public virtual string LNAGCell_A { get; set; }
        public virtual string LNAGTel_A { get; set; }
        public virtual string LNAGTel_1_A { get; set; }
        public virtual string LNAGTel_2_A { get; set; }
        public virtual string LNAGName_B { get; set; }
        public virtual string LNAGID_B { get; set; }
        public virtual string LNAGAdd_B { get; set; }
        public virtual string LNAGAdd_1_B { get; set; }
        public virtual string LNAGAdd_1_1_B { get; set; }
        public virtual string LNAGAdd_1_2_B { get; set; }
        public virtual string LNAGAdd_2_B { get; set; }
        public virtual string LNAGAdd_2_1_B { get; set; }
        public virtual string LNAGAdd_2_2_B { get; set; }
        public virtual string LNAGAdd_2_3_B { get; set; }
        public virtual string LNAGAdd_2_4_B { get; set; }
        public virtual string LNAGAdd_3_B { get; set; }
        public virtual string LNAGAdd_3_1_B { get; set; }
        public virtual string LNAGAdd_3_2_B { get; set; }
        public virtual string LNAGAdd_4_B { get; set; }
        public virtual string LNAGAdd_5_B { get; set; }
        public virtual string LNAGAdd_6_B { get; set; }
        public virtual string LNAGAdd_7_B { get; set; }
        public virtual string LNAGAdd_8_B { get; set; }
        public virtual string LNAGAdd_9_B { get; set; }
        public virtual string LNAGCell_B { get; set; }
        public virtual string LNAGTel_B { get; set; }
        public virtual string LNAGTel_1_B { get; set; }
        public virtual string LNAGTel_2_B { get; set; }


        public virtual string LNAddC { get; set; }
        public virtual string LNAdd { get; set; }

        public virtual string LNAddSame { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string LNCompany { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        public string LNJobtitle { get; set; }

        public DateTime? CreatorTime { get; set; }
        public string CreatorUserId { get; set; }

        public string CreatorUserName { get; set; }
        public string LastModifyUserId { get; set; }
        public DateTime? LastModifyTime { get; set; }
        #endregion

    }
}
