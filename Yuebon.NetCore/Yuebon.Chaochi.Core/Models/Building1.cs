
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
    /// 房屋物件，數據實體對象
    /// </summary>
    [Table("Chaochi_Building1")]
    [Serializable]
    public class Building1 : BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
        public Building1()
        {

        }
        public virtual string BAdd { get; set; }
        public virtual DateTime? CreatorTime { get; set; }
        public virtual string CreatorUserId { get; set; }
        public virtual string CreatorUserOrgId { get; set; }
        public virtual string CreatorUserDeptId { get; set; }
        public virtual DateTime? LastModifyTime { get; set; }
        public virtual string LastModifyUserId { get; set; }

        #region 07/28MM增加權力範圍
        public virtual string B1ExtentOfOwner { get; set; }
        public virtual string B1ExtentOfOwner_1 { get; set; }
        public virtual string B1ExtentOfOwner_2 { get; set; }
        public virtual string B1ExtentOfOwner_A { get; set; }
        public virtual string B1ExtentOfOwner_A_1 { get; set; }
        public virtual string B1ExtentOfOwner_A_2 { get; set; }
        public virtual string B1ExtentOfOwner_B { get; set; }
        public virtual string B1ExtentOfOwner_B_1 { get; set; }
        public virtual string B1ExtentOfOwner_B_2 { get; set; }
        public virtual string B1ExtentOfOwner_C { get; set; }
        public virtual string B1ExtentOfOwner_C_1 { get; set; }
        public virtual string B1ExtentOfOwner_C_2 { get; set; }
        public virtual string B1ExtentOfOwner_D { get; set; }
        public virtual string B1ExtentOfOwner_D_1 { get; set; }
        public virtual string B1ExtentOfOwner_D_2 { get; set; }
        public virtual string B1ExtentOfOwner_E { get; set; }
        public virtual string B1ExtentOfOwner_E_1 { get; set; }
        public virtual string B1ExtentOfOwner_E_2 { get; set; }

        #endregion 07/28MM增加權力範圍

        #region B031001

        /// <summary>
        /// 所有權人1姓名
        /// </summary>
        public virtual string B1OwnerName_A { get; set; }

        /// <summary>
        /// 所有權人1身分證字號
        /// </summary>
        public virtual string B1OwnerID_A { get; set; }

        /// <summary>
        /// 所有權人1地址
        /// </summary>
        public virtual string B1OwnerAdd_A { get; set; }

        /// <summary>
        /// 所有權人1地址1
        /// </summary>
        public virtual string B1OwnerAdd_1_A { get; set; }

        /// <summary>
        /// 所有權人1地址1_1
        /// </summary>
        public virtual string B1OwnerAdd_1_1_A { get; set; }

        /// <summary>
        /// 所有權人1地址1_2
        /// </summary>
        public virtual string B1OwnerAdd_1_2_A { get; set; }

        /// <summary>
        /// 所有權人1地址2
        /// </summary>
        public virtual string B1OwnerAdd_2_A { get; set; }

        /// <summary>
        /// 所有權人1地址2_1
        /// </summary>
        public virtual string B1OwnerAdd_2_1_A { get; set; }

        /// <summary>
        /// 所有權人1地址2_2
        /// </summary>
        public virtual string B1OwnerAdd_2_2_A { get; set; }

        /// <summary>
        /// 所有權人1地址2_3
        /// </summary>
        public virtual string B1OwnerAdd_2_3_A { get; set; }
        public virtual string B1OwnerAdd_2_4_A { get; set; }
        public virtual string B1OwnerAdd_3_A { get; set; }
        public virtual string B1OwnerAdd_3_1_A { get; set; }
        public virtual string B1OwnerAdd_3_2_A { get; set; }
        public virtual string B1OwnerAdd_4_A { get; set; }
        public virtual string B1OwnerAdd_5_A { get; set; }
        public virtual string B1OwnerAdd_6_A { get; set; }
        public virtual string B1OwnerAdd_7_A { get; set; }
        public virtual string B1OwnerAdd_8_A { get; set; }
        public virtual string B1OwnerAdd_9_A { get; set; }
        public virtual string B1OwnerCell_A { get; set; }
        public virtual string B1OwnerTel_1_A { get; set; }
        public virtual string B1OwnerTel_2_A { get; set; }
        public virtual string B1OwnerName_B { get; set; }
        public virtual string B1OwnerID_B { get; set; }

        /// <summary>
        /// 所有權人2地址
        /// </summary>
        public virtual string B1OwnerAdd_B { get; set; }
        public virtual string B1OwnerAdd_1_B { get; set; }
        public virtual string B1OwnerAdd_1_1_B { get; set; }
        public virtual string B1OwnerAdd_1_2_B { get; set; }
        public virtual string B1OwnerAdd_2_B { get; set; }
        public virtual string B1OwnerAdd_2_1_B { get; set; }
        public virtual string B1OwnerAdd_2_2_B { get; set; }
        public virtual string B1OwnerAdd_2_3_B { get; set; }
        public virtual string B1OwnerAdd_2_4_B { get; set; }
        public virtual string B1OwnerAdd_3_B { get; set; }
        public virtual string B1OwnerAdd_3_1_B { get; set; }
        public virtual string B1OwnerAdd_3_2_B { get; set; }
        public virtual string B1OwnerAdd_4_B { get; set; }
        public virtual string B1OwnerAdd_5_B { get; set; }
        public virtual string B1OwnerAdd_6_B { get; set; }
        public virtual string B1OwnerAdd_7_B { get; set; }
        public virtual string B1OwnerAdd_8_B { get; set; }
        public virtual string B1OwnerAdd_9_B { get; set; }
        public virtual string B1OwnerCell_B { get; set; }
        public virtual string B1OwnerTel_1_B { get; set; }
        public virtual string B1OwnerTel_2_B { get; set; }
        public virtual string B1OwnerName_C { get; set; }
        public virtual string B1OwnerID_C { get; set; }

        /// <summary>
        /// 所有權人3地址
        /// </summary>
        public virtual string B1OwnerAdd_C { get; set; }
        public virtual string B1OwnerAdd_1_C { get; set; }
        public virtual string B1OwnerAdd_1_1_C { get; set; }
        public virtual string B1OwnerAdd_1_2_C { get; set; }
        public virtual string B1OwnerAdd_2_C { get; set; }
        public virtual string B1OwnerAdd_2_1_C { get; set; }
        public virtual string B1OwnerAdd_2_2_C { get; set; }
        public virtual string B1OwnerAdd_2_3_C { get; set; }
        public virtual string B1OwnerAdd_2_4_C { get; set; }
        public virtual string B1OwnerAdd_3_C { get; set; }
        public virtual string B1OwnerAdd_3_1_C { get; set; }
        public virtual string B1OwnerAdd_3_2_C { get; set; }
        public virtual string B1OwnerAdd_4_C { get; set; }
        public virtual string B1OwnerAdd_5_C { get; set; }
        public virtual string B1OwnerAdd_6_C { get; set; }
        public virtual string B1OwnerAdd_7_C { get; set; }
        public virtual string B1OwnerAdd_8_C { get; set; }
        public virtual string B1OwnerAdd_9_C { get; set; }
        public virtual string B1OwnerCell_C { get; set; }
        public virtual string B1OwnerTel_1_C { get; set; }
        public virtual string B1OwnerTel_2_C { get; set; }
        public virtual string B1OwnerName_D { get; set; }
        public virtual string B1OwnerID_D { get; set; }

        /// <summary>
        /// 所有權人4地址
        /// </summary>
        public virtual string B1OwnerAdd_D { get; set; }
        public virtual string B1OwnerAdd_1_D { get; set; }
        public virtual string B1OwnerAdd_1_1_D { get; set; }
        public virtual string B1OwnerAdd_1_2_D { get; set; }
        public virtual string B1OwnerAdd_2_D { get; set; }
        public virtual string B1OwnerAdd_2_1_D { get; set; }
        public virtual string B1OwnerAdd_2_2_D { get; set; }
        public virtual string B1OwnerAdd_2_3_D { get; set; }
        public virtual string B1OwnerAdd_2_4_D { get; set; }
        public virtual string B1OwnerAdd_3_D { get; set; }
        public virtual string B1OwnerAdd_3_1_D { get; set; }
        public virtual string B1OwnerAdd_3_2_D { get; set; }
        public virtual string B1OwnerAdd_4_D { get; set; }
        public virtual string B1OwnerAdd_5_D { get; set; }
        public virtual string B1OwnerAdd_6_D { get; set; }
        public virtual string B1OwnerAdd_7_D { get; set; }
        public virtual string B1OwnerAdd_8_D { get; set; }
        public virtual string B1OwnerAdd_9_D { get; set; }
        public virtual string B1OwnerCell_D { get; set; }
        public virtual string B1OwnerTel_1_D { get; set; }
        public virtual string B1OwnerTel_2_D { get; set; }
        public virtual string B1OwnerName_E { get; set; }
        public virtual string B1OwnerID_E { get; set; }

        /// <summary>
        /// 所有權人5地址
        /// </summary>
        public virtual string B1OwnerAdd_E { get; set; }
        public virtual string B1OwnerAdd_1_E { get; set; }
        public virtual string B1OwnerAdd_1_1_E { get; set; }
        public virtual string B1OwnerAdd_1_2_E { get; set; }
        public virtual string B1OwnerAdd_2_E { get; set; }
        public virtual string B1OwnerAdd_2_1_E { get; set; }
        public virtual string B1OwnerAdd_2_2_E { get; set; }
        public virtual string B1OwnerAdd_2_3_E { get; set; }
        public virtual string B1OwnerAdd_2_4_E { get; set; }
        public virtual string B1OwnerAdd_3_E { get; set; }
        public virtual string B1OwnerAdd_3_1_E { get; set; }
        public virtual string B1OwnerAdd_3_2_E { get; set; }
        public virtual string B1OwnerAdd_4_E { get; set; }
        public virtual string B1OwnerAdd_5_E { get; set; }
        public virtual string B1OwnerAdd_6_E { get; set; }
        public virtual string B1OwnerAdd_7_E { get; set; }
        public virtual string B1OwnerAdd_8_E { get; set; }
        public virtual string B1OwnerAdd_9_E { get; set; }
        public virtual string B1OwnerCell_E { get; set; }
        public virtual string B1OwnerTel_1_E { get; set; }
        public virtual string B1OwnerTel_2_E { get; set; }

        #endregion B031001

        #region C01030101

        /// <summary>
        /// 代管費
        /// </summary>
        public virtual string B1EscrowFee { get; set; }

        /// <summary>
        /// 包管費
        /// </summary>
        public virtual string B1GuaranteeFee { get; set; }

        /// <summary>
        /// 媒合費
        /// </summary>
        public virtual string B1MatchFee { get; set; }

        /// <summary>
        /// 開發費
        /// </summary>
        public virtual string B1RDExFee { get; set; }


        #endregion C01030101

        #region 8/17 MM 欄位修改0817.docx

        /// <summary>
        /// 稅籍編號
        /// </summary>
        public virtual string B1TaxID { get; set; }

        #endregion 8/17 MM 欄位修改0817.docx

        #region A000002

        /// <summary>
        /// 無漏水情形
        /// </summary>
        public virtual string B1WaterLeakage_N { get; set; }

        /// <summary>
        /// 有漏水情形
        /// </summary>
        public virtual string B1WaterLeakage_Y { get; set; }

        /// <summary>
        /// 滲漏水處
        /// </summary>
        public virtual string B1WaterEX { get; set; }

        #endregion
    }
}