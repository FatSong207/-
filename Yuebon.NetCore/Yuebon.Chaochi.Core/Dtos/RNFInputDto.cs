using AutoMapper;
using System;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Core.Dtos
{
    /// <summary>
    /// 戶客_自然人輸入對象模型
    /// </summary>
    [AutoMap(typeof(CustomerRN))]
    [Serializable]
    public class RNFInputDto
    {
        public virtual string Id { get; set; }
        public virtual string RNID { get; set; }
        public virtual string RNName { get; set; }
        //public virtual string RNCell { get; set; }
        public virtual string RNBirthday { get; set; }
        public virtual string RNFName_A { get; set; }
        public virtual string RNFName_B { get; set; }
        public virtual string RNFName_C { get; set; }
        public virtual string RNFID_1_A { get; set; }
        //public virtual string RNFID_1_A_1 { get; set; }
        //public virtual string RNFID_1_A_2 { get; set; }
        //public virtual string RNFID_1_A_3 { get; set; }
        //public virtual string RNFID_1_A_4 { get; set; }
        //public virtual string RNFID_1_A_5 { get; set; }
        //public virtual string RNFID_1_A_6 { get; set; }
        //public virtual string RNFID_1_A_7 { get; set; }
        //public virtual string RNFID_1_A_8 { get; set; }
        //public virtual string RNFID_1_A_9 { get; set; }
        //public virtual string RNFID_1_A_10 { get; set; }
        public virtual string RNFBirthday_A { get; set; }
        //public virtual string RNFBirthday_Y_A { get; set; }
        //public virtual string RNFBirthday_M_A { get; set; }
        //public virtual string RNFBirthday_D_A { get; set; }
        //public virtual string RNFTitle_A { get; set; }
        //public virtual string RNFIncome_A { get; set; }
        //public virtual string DeleteUserId { get; set; }
        //public virtual string RNFID_1_B { get; set; }
        //public virtual string RNFID_1_B_1 { get; set; }
        //public virtual string RNFID_1_B_2 { get; set; }
        //public virtual string RNFID_1_B_3 { get; set; }
        //public virtual string RNFID_1_B_4 { get; set; }
        //public virtual string RNFID_1_B_5 { get; set; }
        //public virtual string RNFID_1_B_6 { get; set; }
        //public virtual string RNFID_1_B_7 { get; set; }
        //public virtual string RNFID_1_B_8 { get; set; }
        //public virtual string RNFID_1_B_9 { get; set; }
        //public virtual string RNFID_1_B_10 { get; set; }
        public virtual string RNFID_1_B { get; set; }
        public virtual string RNFBirthday_B { get; set; }
        //public virtual string RNFBirthday_Y_B { get; set; }
        //public virtual string RNFBirthday_M_B { get; set; }
        //public virtual string RNFBirthday_D_B { get; set; }
        //public virtual string RNFTitle_B { get; set; }
        //public virtual string RNFIncome_B { get; set; }
        public virtual string RNFID_1_C { get; set; }
        //public virtual string RNFID_1_C_1 { get; set; }
        //public virtual string RNFID_1_C_2 { get; set; }
        //public virtual string RNFID_1_C_3 { get; set; }
        //public virtual string RNFID_1_C_4 { get; set; }
        //public virtual string RNFID_1_C_5 { get; set; }
        //public virtual string RNFID_1_C_6 { get; set; }
        //public virtual string RNFID_1_C_7 { get; set; }
        //public virtual string RNFID_1_C_8 { get; set; }
        //public virtual string RNFID_1_C_9 { get; set; }
        //public virtual string RNFID_1_C_10 { get; set; }
        public virtual string RNFBirthday_C { get; set; }
        //public virtual string RNFBirthday_Y_C { get; set; }
        //public virtual string RNFBirthday_M_C { get; set; }
        //public virtual string RNFBirthday_D_C { get; set; }
        //public virtual string RNFTitle_C { get; set; }
        //public virtual string RNFIncome_C { get; set; }
        public virtual string RNFName_D { get; set; }
        public virtual string RNFID_1_D { get; set; }
        public virtual string RNFBirthday_D { get; set; }
        //public virtual string RNFID_1_D_1 { get; set; }
        //public virtual string RNFID_1_D_2 { get; set; }
        //public virtual string RNFID_1_D_3 { get; set; }
        //public virtual string RNFID_1_D_4 { get; set; }
        //public virtual string RNFID_1_D_5 { get; set; }
        //public virtual string RNFID_1_D_6 { get; set; }
        //public virtual string RNFID_1_D_7 { get; set; }
        //public virtual string RNFID_1_D_8 { get; set; }
        //public virtual string RNFID_1_D_9 { get; set; }
        //public virtual string RNFID_1_D_10 { get; set; }
        //public virtual string RNFBirthday_Y_D { get; set; }
        //public virtual string RNFBirthday_M_D { get; set; }
        //public virtual string RNFBirthday_D_D { get; set; }
        public virtual string RNFName_E { get; set; }
        public virtual string RNFID_1_E { get; set; }
        public virtual string RNFBirthday_E { get; set; }
        //public virtual string RNFID_1_E_1 { get; set; }
        //public virtual string RNFID_1_E_2 { get; set; }
        //public virtual string RNFID_1_E_3 { get; set; }
        //public virtual string RNFID_1_E_4 { get; set; }
        //public virtual string RNFID_1_E_5 { get; set; }
        //public virtual string RNFID_1_E_6 { get; set; }
        //public virtual string RNFID_1_E_7 { get; set; }
        //public virtual string RNFID_1_E_8 { get; set; }
        //public virtual string RNFID_1_E_9 { get; set; }
        //public virtual string RNFID_1_E_10 { get; set; }
        //public virtual string RNFBirthday_Y_E { get; set; }
        //public virtual string RNFBirthday_M_E { get; set; }
        //public virtual string RNFBirthday_D_E { get; set; }
        public virtual string RNFName_F { get; set; }
        public virtual string RNFID_1_F { get; set; }
        public virtual string RNFBirthday_F { get; set; }
        //public virtual string RNFID_1_F_1 { get; set; }
        //public virtual string RNFID_1_F_2 { get; set; }
        //public virtual string RNFID_1_F_3 { get; set; }
        //public virtual string RNFID_1_F_4 { get; set; }
        //public virtual string RNFID_1_F_5 { get; set; }
        //public virtual string RNFID_1_F_6 { get; set; }
        //public virtual string RNFID_1_F_7 { get; set; }
        //public virtual string RNFID_1_F_8 { get; set; }
        //public virtual string RNFID_1_F_9 { get; set; }
        //public virtual string RNFID_1_F_10 { get; set; }
        //public virtual string RNFBirthday_Y_F { get; set; }
        //public virtual string RNFBirthday_M_F { get; set; }
        //public virtual string RNFBirthday_D_F { get; set; }
        public virtual string RNFName_G { get; set; }
        public virtual string RNFID_1_G { get; set; }
        public virtual string RNFBirthday_G { get; set; }
        //public virtual string RNFID_1_G_1 { get; set; }
        //public virtual string RNFID_1_G_2 { get; set; }
        //public virtual string RNFID_1_G_3 { get; set; }
        //public virtual string RNFID_1_G_4 { get; set; }
        //public virtual string RNFID_1_G_5 { get; set; }
        //public virtual string RNFID_1_G_6 { get; set; }
        //public virtual string RNFID_1_G_7 { get; set; }
        //public virtual string RNFID_1_G_8 { get; set; }
        //public virtual string RNFID_1_G_9 { get; set; }
        //public virtual string RNFID_1_G_10 { get; set; }
        //public virtual string RNFBirthday_Y_G { get; set; }
        //public virtual string RNFBirthday_M_G { get; set; }
        //public virtual string RNFBirthday_D_G { get; set; }
        /// <summary>
        /// 申請人稱謂(值一定為"本人")
        /// </summary>
        public virtual string RNFRelation { get; set; }

        /// <summary>
        /// 同住人A稱謂
        /// </summary>
        public virtual string RNFRelation_A { get; set; }

        /// <summary>
        /// 同住人A手機
        /// </summary>
        public virtual string RNFCell_A { get; set; }

        /// <summary>
        /// 同住人B稱謂
        /// </summary>
        public virtual string RNFRelation_B { get; set; }

        /// <summary>
        /// 同住人B手機
        /// </summary>
        public virtual string RNFCell_B { get; set; }

        /// <summary>
        /// 同住人C稱謂
        /// </summary>
        public virtual string RNFRelation_C { get; set; }

        /// <summary>
        /// 同住人C手機
        /// </summary>
        public virtual string RNFCell_C { get; set; }

        /// <summary>
        /// 同住人D稱謂
        /// </summary>
        public virtual string RNFRelation_D { get; set; }

        /// <summary>
        /// 同住人D手機
        /// </summary>
        public virtual string RNFCell_D { get; set; }

        /// <summary>
        /// 同住人E稱謂
        /// </summary>
        public virtual string RNFRelation_E { get; set; }

        /// <summary>
        /// 同住人E手機
        /// </summary>
        public virtual string RNFCell_E { get; set; }

        /// <summary>
        /// 同住人F稱謂
        /// </summary>
        public virtual string RNFRelation_F { get; set; }

        /// <summary>
        /// 同住人F手機
        /// </summary>
        public virtual string RNFCell_F { get; set; }

        /// <summary>
        /// 同住人G稱謂
        /// </summary>
        public virtual string RNFRelation_G { get; set; }

        /// <summary>
        /// 同住人G手機
        /// </summary>
        public virtual string RNFCell_G { get; set; }

    }
}
