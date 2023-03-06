using System;

namespace Yuebon.Chaochi.Core.Dtos
{
    [Serializable]
    public class A000002OutputDto
    {
        #region 承租人

        public virtual string RNName { get; set; }
        public virtual string RNID { get; set; }
        public virtual string RNGender1 { get; set; }
        public virtual string RNGender2 { get; set; }
        public virtual string RNBirthday { get; set; }
        public virtual string RNTel_1 { get; set; }
        public virtual string RNTel_2 { get; set; }
        public virtual string RNCell { get; set; }
        public virtual string RNMail { get; set; }
        public virtual string RNLine { get; set; }
        public virtual string RNAdd { get; set; }
        public virtual string RNAddC { get; set; }
        /// <summary>
        /// 戶籍地址_縣/市
        /// </summary>
        public virtual string RNAdd_1 { get; set; }

        /// <summary>
        /// 戶籍地址_縣
        /// </summary>
        public virtual string RNAdd_1_1 { get; set; }

        /// <summary>
        /// 戶籍地址_市
        /// </summary>
        public virtual string RNAdd_1_2 { get; set; }

        /// <summary>
        /// 戶籍地址_鄉/鎮/市/區
        /// </summary>
        public virtual string RNAdd_2 { get; set; }

        /// <summary>
        /// 戶籍地址_鄉
        /// </summary>
        public virtual string RNAdd_2_1 { get; set; }

        /// <summary>
        /// 戶籍地址_市
        /// </summary>
        public virtual string RNAdd_2_2 { get; set; }

        /// <summary>
        /// 戶籍地址_鎮
        /// </summary>
        public virtual string RNAdd_2_3 { get; set; }

        /// <summary>
        /// 戶籍地址_區
        /// </summary>
        public virtual string RNAdd_2_4 { get; set; }

        /// <summary>
        /// 戶籍地址_街/路
        /// </summary>
        public virtual string RNAdd_3 { get; set; }

        /// <summary>
        /// 戶籍地址_街
        /// </summary>
        public virtual string RNAdd_3_1 { get; set; }

        /// <summary>
        /// 戶籍地址_路
        /// </summary>
        public virtual string RNAdd_3_2 { get; set; }

        /// <summary>
        /// 戶籍地址_段
        /// </summary>
        public virtual string RNAdd_4 { get; set; }

        /// <summary>
        /// 戶籍地址_巷
        /// </summary>
        public virtual string RNAdd_5 { get; set; }

        /// <summary>
        /// 戶籍地址_弄
        /// </summary>
        public virtual string RNAdd_6 { get; set; }

        /// <summary>
        /// 戶籍地址_號
        /// </summary>
        public virtual string RNAdd_7 { get; set; }

        /// <summary>
        /// 戶籍地址_樓
        /// </summary>
        public virtual string RNAdd_8 { get; set; }

        /// <summary>
        /// 戶籍地址_樓
        /// </summary>
        public virtual string RNAdd_9 { get; set; }
        /// <summary>
        /// 地址_同戶籍地址
        /// </summary>
        public virtual string RNAddSame { get; set; }

        /// <summary>
        /// 通訊地址_縣/市
        /// </summary>
        public virtual string RNAddC_1 { get; set; }

        /// <summary>
        /// 通訊地址_縣
        /// </summary>
        public virtual string RNAddC_1_1 { get; set; }

        /// <summary>
        /// 通訊地址_市
        /// </summary>
        public virtual string RNAddC_1_2 { get; set; }

        /// <summary>
        /// 通訊地址_鄉/鎮/市/區
        /// </summary>
        public virtual string RNAddC_2 { get; set; }

        /// <summary>
        /// 通訊地址_鄉
        /// </summary>
        public virtual string RNAddC_2_1 { get; set; }

        /// <summary>
        /// 通訊地址_市
        /// </summary>
        public virtual string RNAddC_2_2 { get; set; }

        /// <summary>
        /// 通訊地址_鎮
        /// </summary>
        public virtual string RNAddC_2_3 { get; set; }

        /// <summary>
        /// 通訊地址_區
        /// </summary>
        public virtual string RNAddC_2_4 { get; set; }

        /// <summary>
        /// 通訊地址_街/路
        /// </summary>
        public virtual string RNAddC_3 { get; set; }

        /// <summary>
        /// 通訊地址_街
        /// </summary>
        public virtual string RNAddC_3_1 { get; set; }

        /// <summary>
        /// 通訊地址_路
        /// </summary>
        public virtual string RNAddC_3_2 { get; set; }

        /// <summary>
        /// 通訊地址_段
        /// </summary>
        public virtual string RNAddC_4 { get; set; }

        /// <summary>
        /// 通訊地址_巷
        /// </summary>
        public virtual string RNAddC_5 { get; set; }

        /// <summary>
        /// 通訊地址_弄
        /// </summary>
        public virtual string RNAddC_6 { get; set; }

        /// <summary>
        /// 通訊地址_號
        /// </summary>
        public virtual string RNAddC_7 { get; set; }

        /// <summary>
        /// 通訊地址_樓
        /// </summary>
        public virtual string RNAddC_8 { get; set; }

        /// <summary>
        /// 通訊地址_樓
        /// </summary>
        public virtual string RNAddC_9 { get; set; }

        #endregion 承租人

        #region 緊急聯絡人

        /// <summary>
        /// 緊急連絡人身分證字號
        /// </summary>
        public virtual string RNECID { get; set; }

        /// <summary>
        /// 緊急連絡人生日
        /// </summary>
        public virtual string RNECBirthday { get; set; }

        /// <summary>
        /// 緊急聯絡人姓名
        /// </summary>
        public virtual string RNECName { get; set; }

        /// <summary>
        /// 與緊急聯絡人關係
        /// </summary>
        public virtual string RNECRelation { get; set; }

        /// <summary>
        /// 緊急聯絡人手機
        /// </summary>
        public virtual string RNECCell { get; set; }

        /// <summary>
        /// 緊急聯絡人戶籍地址
        /// </summary>
        public virtual string RNECAdd { get; set; }
        public virtual string RNECAdd_1 { get; set; }
        public virtual string RNECAdd_1_1 { get; set; }
        public virtual string RNECAdd_1_2 { get; set; }
        public virtual string RNECAdd_2 { get; set; }
        public virtual string RNECAdd_2_1 { get; set; }
        public virtual string RNECAdd_2_2 { get; set; }
        public virtual string RNECAdd_2_3 { get; set; }
        public virtual string RNECAdd_2_4 { get; set; }
        public virtual string RNECAdd_3 { get; set; }
        public virtual string RNECAdd_3_1 { get; set; }
        public virtual string RNECAdd_3_2 { get; set; }
        public virtual string RNECAdd_4 { get; set; }
        public virtual string RNECAdd_5 { get; set; }
        public virtual string RNECAdd_6 { get; set; }
        public virtual string RNECAdd_7 { get; set; }
        public virtual string RNECAdd_8 { get; set; }
        public virtual string RNECAdd_9 { get; set; }

        public virtual string RNECAddSame { get; set; }
        /// <summary>
        /// 緊急聯絡人通訊地址
        /// </summary>
        public virtual string RNECAddC { get; set; }
        public virtual string RNECAddC_1 { get; set; }
        public virtual string RNECAddC_1_1 { get; set; }
        public virtual string RNECAddC_1_2 { get; set; }
        public virtual string RNECAddC_2 { get; set; }
        public virtual string RNECAddC_2_1 { get; set; }
        public virtual string RNECAddC_2_2 { get; set; }
        public virtual string RNECAddC_2_3 { get; set; }
        public virtual string RNECAddC_2_4 { get; set; }
        public virtual string RNECAddC_3 { get; set; }
        public virtual string RNECAddC_3_1 { get; set; }
        public virtual string RNECAddC_3_2 { get; set; }
        public virtual string RNECAddC_4 { get; set; }
        public virtual string RNECAddC_5 { get; set; }
        public virtual string RNECAddC_6 { get; set; }
        public virtual string RNECAddC_7 { get; set; }
        public virtual string RNECAddC_8 { get; set; }
        public virtual string RNECAddC_9 { get; set; }

        #endregion 緊急聯絡人

        #region 代理人

        /// <summary>
        /// 代理人姓名
        /// </summary>
        public virtual string RNAGName_A { get; set; }

        /// <summary>
        /// 代理人身份證字號/居留證號
        /// </summary>
        public virtual string RNAGID_A { get; set; }

        /// <summary>
        /// 代理人生日
        /// </summary>
        public virtual string RNAGBirthday { get; set; }

        /// <summary>
        /// 代理人手機
        /// </summary>
        public virtual string RNAGCell_A { get; set; }

        /// <summary>
        /// 代理人地址
        /// </summary>
        public virtual string RNAGAdd { get; set; }
        public virtual string RNAGAdd_1_A { get; set; }
        public virtual string RNAGAdd_1_1_A { get; set; }
        public virtual string RNAGAdd_1_2_A { get; set; }
        public virtual string RNAGAdd_2_A { get; set; }
        public virtual string RNAGAdd_2_1_A { get; set; }
        public virtual string RNAGAdd_2_2_A { get; set; }
        public virtual string RNAGAdd_2_3_A { get; set; }
        public virtual string RNAGAdd_2_4_A { get; set; }
        public virtual string RNAGAdd_3_A { get; set; }
        public virtual string RNAGAdd_3_1_A { get; set; }
        public virtual string RNAGAdd_3_2_A { get; set; }
        public virtual string RNAGAdd_4_A { get; set; }
        public virtual string RNAGAdd_5_A { get; set; }
        public virtual string RNAGAdd_6_A { get; set; }
        public virtual string RNAGAdd_7_A { get; set; }
        public virtual string RNAGAdd_8_A { get; set; }
        public virtual string RNAGAdd_9_A { get; set; }
        public virtual string RNAGAddSame { get; set; }
        public virtual string RNAGAddC { get; set; }
        public virtual string RNAGAddC_1_A { get; set; }
        public virtual string RNAGAddC_1_1_A { get; set; }
        public virtual string RNAGAddC_1_2_A { get; set; }
        public virtual string RNAGAddC_2_A { get; set; }
        public virtual string RNAGAddC_2_1_A { get; set; }
        public virtual string RNAGAddC_2_2_A { get; set; }
        public virtual string RNAGAddC_2_3_A { get; set; }
        public virtual string RNAGAddC_2_4_A { get; set; }
        public virtual string RNAGAddC_3_A { get; set; }
        public virtual string RNAGAddC_3_1_A { get; set; }
        public virtual string RNAGAddC_3_2_A { get; set; }
        public virtual string RNAGAddC_4_A { get; set; }
        public virtual string RNAGAddC_5_A { get; set; }
        public virtual string RNAGAddC_6_A { get; set; }
        public virtual string RNAGAddC_7_A { get; set; }
        public virtual string RNAGAddC_8_A { get; set; }
        public virtual string RNAGAddC_9_A { get; set; }

        #endregion 代理人

        #region 其他身份


        /// <summary>
        /// 未滿18歲之未成年人
        /// </summary>
        public virtual string RNQualify3C_12 { get; set; }
        /// <summary>
        /// 六十五歲以上之老人
        /// </summary>
        public virtual string RNQualify2C_1 { get; set; }

        /// <summary>
        /// 原住民
        /// </summary>
        public virtual string RNQualify2C_2 { get; set; }

        /// <summary>
        /// 育有未成年(20 歲以下)子女三人以上
        /// </summary>
        public virtual string RNQualify2C_3 { get; set; }

        /// <summary>
        /// 特殊境遇家庭
        /// </summary>
        public virtual string RNQualify2C_4 { get; set; }

        /// <summary>
        /// □於安置教養機構或寄養家庭結束安置無法返家，未滿二十五歲。
        /// </summary>
        public virtual string RNQualify2C_5 { get; set; }

        /// <summary>
        /// □受家庭暴力或性侵害之受害者及其子女。
        /// </summary>
        public virtual string RNQualify2C_6 { get; set; }

        /// <summary>
        /// □身心障礙者。
        /// </summary>
        public virtual string RNQualify2C_7 { get; set; }

        /// <summary>
        /// □感染人類免疫缺乏病毒者或罹患後天免疫缺乏症候群者。
        /// </summary>
        public virtual string RNQualify2C_8 { get; set; }

        /// <summary>
        /// □災民。
        /// </summary>
        public virtual string RNQualify2C_9 { get; set; }

        /// <summary>
        /// □遊民。
        /// </summary>
        public virtual string RNQualify2C_10 { get; set; }


        /// <summary>
        /// □低收入戶。
        /// </summary>
        public virtual string RNQualify3C_1 { get; set; }

        /// <summary>
        /// □中低收入戶。
        /// </summary>
        public virtual string RNQualify3C_2 { get; set; }


        #endregion 其他身份

        #region 保證人

        public virtual string RNGName { get; set; }
        public virtual string RNGID { get; set; }
        public virtual string RNGBirthday { get; set; }
        public virtual string RNGAdd { get; set; }
        public virtual string RNGAdd_1 { get; set; }
        public virtual string RNGAdd_1_1 { get; set; }
        public virtual string RNGAdd_1_2 { get; set; }
        public virtual string RNGAdd_2 { get; set; }
        public virtual string RNGAdd_2_1 { get; set; }
        public virtual string RNGAdd_2_2 { get; set; }
        public virtual string RNGAdd_2_3 { get; set; }
        public virtual string RNGAdd_2_4 { get; set; }
        public virtual string RNGAdd_3 { get; set; }
        public virtual string RNGAdd_3_1 { get; set; }
        public virtual string RNGAdd_3_2 { get; set; }
        public virtual string RNGAdd_4 { get; set; }
        public virtual string RNGAdd_5 { get; set; }
        public virtual string RNGAdd_6 { get; set; }
        public virtual string RNGAdd_7 { get; set; }
        public virtual string RNGAdd_8 { get; set; }
        public virtual string RNGAdd_9 { get; set; }
        public virtual string RNGAddSame { get; set; }
        public virtual string RNGCAdd { get; set; }
        public virtual string RNGCAdd_1 { get; set; }
        public virtual string RNGCAdd_1_1 { get; set; }
        public virtual string RNGCAdd_1_2 { get; set; }
        public virtual string RNGCAdd_2 { get; set; }
        public virtual string RNGCAdd_2_1 { get; set; }
        public virtual string RNGCAdd_2_2 { get; set; }
        public virtual string RNGCAdd_2_3 { get; set; }
        public virtual string RNGCAdd_2_4 { get; set; }
        public virtual string RNGCAdd_3 { get; set; }
        public virtual string RNGCAdd_3_1 { get; set; }
        public virtual string RNGCAdd_3_2 { get; set; }
        public virtual string RNGCAdd_4 { get; set; }
        public virtual string RNGCAdd_5 { get; set; }
        public virtual string RNGCAdd_6 { get; set; }
        public virtual string RNGCAdd_7 { get; set; }
        public virtual string RNGCAdd_8 { get; set; }
        public virtual string RNGCAdd_9 { get; set; }
        public virtual string RNGCell { get; set; }


        #endregion 保證人

        #region 家庭成員

        public virtual string RNFName_A { get; set; }

        public virtual string RNFID_1_A { get; set; }

        public virtual string RNFBirthday_A { get; set; }

        public virtual string RNFRelation_A { get; set; }

        public virtual string RNFCell_A { get; set; }

        public virtual string RNFName_B { get; set; }

        public virtual string RNFID_1_B { get; set; }

        public virtual string RNFBirthday_B { get; set; }

        public virtual string RNFRelation_B { get; set; }

        public virtual string RNFCell_B { get; set; }

        public virtual string RNFName_C { get; set; }

        public virtual string RNFID_1_C { get; set; }

        public virtual string RNFBirthday_C { get; set; }

        public virtual string RNFRelation_C { get; set; }

        public virtual string RNFCell_C { get; set; }

        public virtual string RNFName_D { get; set; }

        public virtual string RNFID_1_D { get; set; }

        public virtual string RNFBirthday_D { get; set; }

        public virtual string RNFRelation_D { get; set; }

        public virtual string RNFCell_D { get; set; }

        public virtual string RNFName_E { get; set; }

        public virtual string RNFID_1_E { get; set; }

        public virtual string RNFBirthday_E { get; set; }

        public virtual string RNFRelation_E { get; set; }

        public virtual string RNFCell_E { get; set; }

        public virtual string RNFName_F { get; set; }

        public virtual string RNFID_1_F { get; set; }

        public virtual string RNFBirthday_F { get; set; }

        public virtual string RNFRelation_F { get; set; }

        public virtual string RNFCell_F { get; set; }

        public virtual string RNFName_G { get; set; }

        public virtual string RNFID_1_G { get; set; }

        public virtual string RNFBirthday_G { get; set; }

        public virtual string RNFRelation_G { get; set; }

        public virtual string RNFCell_G { get; set; }

        #endregion 家庭成員

        #region BZ欄位

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

        /// <summary>
        /// BZ030
        /// </summary>
        public virtual string BZ030 { get; set; }

        /// <summary>
        /// BZ031
        /// </summary>
        public virtual string BZ031 { get; set; }

        /// <summary>
        /// BZ032
        /// </summary>
        public virtual string BZ032 { get; set; }

        /// <summary>
        /// BZ033
        /// </summary>
        public virtual string BZ033 { get; set; }

        /// <summary>
        /// BZ034
        /// </summary>
        public virtual string BZ034 { get; set; }

        /// <summary>
        /// BZ035
        /// </summary>
        public virtual string BZ035 { get; set; }

        /// <summary>
        /// BZ036
        /// </summary>
        public virtual string BZ036 { get; set; }

        /// <summary>
        /// BZ037
        /// </summary>
        public virtual string BZ037 { get; set; }

        /// <summary>
        /// BZ038
        /// </summary>
        public virtual string BZ038 { get; set; }

        /// <summary>
        /// BZ039
        /// </summary>
        public virtual string BZ039 { get; set; }

        /// <summary>
        /// BZ040
        /// </summary>
        public virtual string BZ040 { get; set; }

        /// <summary>
        /// BZ041
        /// </summary>
        public virtual string BZ041 { get; set; }

        /// <summary>
        /// BZ042
        /// </summary>
        public virtual string BZ042 { get; set; }

        /// <summary>
        /// BZ043
        /// </summary>
        public virtual string BZ043 { get; set; }

        /// <summary>
        /// BZ044
        /// </summary>
        public virtual string BZ044 { get; set; }

        /// <summary>
        /// BZ045
        /// </summary>
        public virtual string BZ045 { get; set; }

        /// <summary>
        /// BZ046
        /// </summary>
        public virtual string BZ046 { get; set; }

        /// <summary>
        /// BZ047
        /// </summary>
        public virtual string BZ047 { get; set; }

        /// <summary>
        /// BZ048
        /// </summary>
        public virtual string BZ048 { get; set; }

        /// <summary>
        /// BZ049
        /// </summary>
        public virtual string BZ049 { get; set; }

        /// <summary>
        /// BZ050
        /// </summary>
        public virtual string BZ050 { get; set; }

        /// <summary>
        /// BZ051
        /// </summary>
        public virtual string BZ051 { get; set; }

        /// <summary>
        /// BZ052
        /// </summary>
        public virtual string BZ052 { get; set; }

        /// <summary>
        /// BZ053
        /// </summary>
        public virtual string BZ053 { get; set; }

        /// <summary>
        /// BZ054
        /// </summary>
        public virtual string BZ054 { get; set; }

        /// <summary>
        /// BZ055
        /// </summary>
        public virtual string BZ055 { get; set; }

        /// <summary>
        /// BZ056
        /// </summary>
        public virtual string BZ056 { get; set; }

        /// <summary>
        /// BZ057
        /// </summary>
        public virtual string BZ057 { get; set; }

        /// <summary>
        /// BZ058
        /// </summary>
        public virtual string BZ058 { get; set; }

        /// <summary>
        /// BZ059
        /// </summary>
        public virtual string BZ059 { get; set; }

        /// <summary>
        /// BZ060
        /// </summary>
        public virtual string BZ060 { get; set; }

        /// <summary>
        /// BZ061
        /// </summary>
        public virtual string BZ061 { get; set; }

        /// <summary>
        /// BZ062
        /// </summary>
        public virtual string BZ062 { get; set; }

        /// <summary>
        /// BZ063
        /// </summary>
        public virtual string BZ063 { get; set; }

        /// <summary>
        /// BZ064
        /// </summary>
        public virtual string BZ064 { get; set; }

        /// <summary>
        /// BZ065
        /// </summary>
        public virtual string BZ065 { get; set; }

        /// <summary>
        /// BZ066
        /// </summary>
        public virtual string BZ066 { get; set; }

        /// <summary>
        /// BZ067
        /// </summary>
        public virtual string BZ067 { get; set; }

        /// <summary>
        /// BZ068
        /// </summary>
        public virtual string BZ068 { get; set; }

        /// <summary>
        /// BZ069
        /// </summary>
        public virtual string BZ069 { get; set; }

        /// <summary>
        /// BZ070
        /// </summary>
        public virtual string BZ070 { get; set; }

        /// <summary>
        /// BZ071
        /// </summary>
        public virtual string BZ071 { get; set; }

        /// <summary>
        /// BZ072
        /// </summary>
        public virtual string BZ072 { get; set; }

        /// <summary>
        /// BZ073
        /// </summary>
        public virtual string BZ073 { get; set; }

        /// <summary>
        /// BZ074
        /// </summary>
        public virtual string BZ074 { get; set; }

        /// <summary>
        /// BZ075
        /// </summary>
        public virtual string BZ075 { get; set; }

        /// <summary>
        /// BZ076
        /// </summary>
        public virtual string BZ076 { get; set; }

        /// <summary>
        /// BZ077
        /// </summary>
        public virtual string BZ077 { get; set; }

        /// <summary>
        /// BZ078
        /// </summary>
        public virtual string BZ078 { get; set; }

        /// <summary>
        /// BZ079
        /// </summary>
        public virtual string BZ079 { get; set; }

        /// <summary>
        /// BZ080
        /// </summary>
        public virtual string BZ080 { get; set; }

        /// <summary>
        /// BZ081
        /// </summary>
        public virtual string BZ081 { get; set; }

        /// <summary>
        /// BZ082
        /// </summary>
        public virtual string BZ082 { get; set; }

        /// <summary>
        /// BZ083
        /// </summary>
        public virtual string BZ083 { get; set; }

        /// <summary>
        /// BZ084
        /// </summary>
        public virtual string BZ084 { get; set; }


        #endregion BZ欄位

        #region 簽名

        public string SignatureImgPath_1 { get; set; }


        #endregion 簽名
    }
}
