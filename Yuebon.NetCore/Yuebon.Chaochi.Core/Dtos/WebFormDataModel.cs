using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.Dtos
{
    public class WebFormDataModel
    {
        #region A000002 自然人房客資料卡
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
        #endregion

        #region A000001 自然人房東資料卡

        #region 出租人

        public virtual string LNName { get; set; }
        public virtual string LNID { get; set; }
        public virtual string LNGender1 { get; set; }
        public virtual string LNGender2 { get; set; }
        public virtual string LNBirthday { get; set; }
        public virtual string LNTel_1 { get; set; }
        public virtual string LNTel_2 { get; set; }
        public virtual string LNCell { get; set; }
        public virtual string LNMail { get; set; }
        public virtual string LNLine { get; set; }
        public virtual string LNAdd { get; set; }
        public virtual string LNAddC { get; set; }
        /// <summary>
        /// 戶籍地址_縣/市
        /// </summary>
        public virtual string LNAdd_1 { get; set; }

        /// <summary>
        /// 戶籍地址_縣
        /// </summary>
        public virtual string LNAdd_1_1 { get; set; }

        /// <summary>
        /// 戶籍地址_市
        /// </summary>
        public virtual string LNAdd_1_2 { get; set; }

        /// <summary>
        /// 戶籍地址_鄉/鎮/市/區
        /// </summary>
        public virtual string LNAdd_2 { get; set; }

        /// <summary>
        /// 戶籍地址_鄉
        /// </summary>
        public virtual string LNAdd_2_1 { get; set; }

        /// <summary>
        /// 戶籍地址_市
        /// </summary>
        public virtual string LNAdd_2_2 { get; set; }

        /// <summary>
        /// 戶籍地址_鎮
        /// </summary>
        public virtual string LNAdd_2_3 { get; set; }

        /// <summary>
        /// 戶籍地址_區
        /// </summary>
        public virtual string LNAdd_2_4 { get; set; }

        /// <summary>
        /// 戶籍地址_街/路
        /// </summary>
        public virtual string LNAdd_3 { get; set; }

        /// <summary>
        /// 戶籍地址_街
        /// </summary>
        public virtual string LNAdd_3_1 { get; set; }

        /// <summary>
        /// 戶籍地址_路
        /// </summary>
        public virtual string LNAdd_3_2 { get; set; }

        /// <summary>
        /// 戶籍地址_段
        /// </summary>
        public virtual string LNAdd_4 { get; set; }

        /// <summary>
        /// 戶籍地址_巷
        /// </summary>
        public virtual string LNAdd_5 { get; set; }

        /// <summary>
        /// 戶籍地址_弄
        /// </summary>
        public virtual string LNAdd_6 { get; set; }

        /// <summary>
        /// 戶籍地址_號
        /// </summary>
        public virtual string LNAdd_7 { get; set; }

        /// <summary>
        /// 戶籍地址_樓
        /// </summary>
        public virtual string LNAdd_8 { get; set; }

        /// <summary>
        /// 戶籍地址_樓
        /// </summary>
        public virtual string LNAdd_9 { get; set; }
        /// <summary>
        /// 地址_同戶籍地址
        /// </summary>
        public virtual string LNAddSame { get; set; }

        /// <summary>
        /// 通訊地址_縣/市
        /// </summary>
        public virtual string LNAddC_1 { get; set; }

        /// <summary>
        /// 通訊地址_縣
        /// </summary>
        public virtual string LNAddC_1_1 { get; set; }

        /// <summary>
        /// 通訊地址_市
        /// </summary>
        public virtual string LNAddC_1_2 { get; set; }

        /// <summary>
        /// 通訊地址_鄉/鎮/市/區
        /// </summary>
        public virtual string LNAddC_2 { get; set; }

        /// <summary>
        /// 通訊地址_鄉
        /// </summary>
        public virtual string LNAddC_2_1 { get; set; }

        /// <summary>
        /// 通訊地址_市
        /// </summary>
        public virtual string LNAddC_2_2 { get; set; }

        /// <summary>
        /// 通訊地址_鎮
        /// </summary>
        public virtual string LNAddC_2_3 { get; set; }

        /// <summary>
        /// 通訊地址_區
        /// </summary>
        public virtual string LNAddC_2_4 { get; set; }

        /// <summary>
        /// 通訊地址_街/路
        /// </summary>
        public virtual string LNAddC_3 { get; set; }

        /// <summary>
        /// 通訊地址_街
        /// </summary>
        public virtual string LNAddC_3_1 { get; set; }

        /// <summary>
        /// 通訊地址_路
        /// </summary>
        public virtual string LNAddC_3_2 { get; set; }

        /// <summary>
        /// 通訊地址_段
        /// </summary>
        public virtual string LNAddC_4 { get; set; }

        /// <summary>
        /// 通訊地址_巷
        /// </summary>
        public virtual string LNAddC_5 { get; set; }

        /// <summary>
        /// 通訊地址_弄
        /// </summary>
        public virtual string LNAddC_6 { get; set; }

        /// <summary>
        /// 通訊地址_號
        /// </summary>
        public virtual string LNAddC_7 { get; set; }

        /// <summary>
        /// 通訊地址_樓
        /// </summary>
        public virtual string LNAddC_8 { get; set; }

        /// <summary>
        /// 通訊地址_樓
        /// </summary>
        public virtual string LNAddC_9 { get; set; }


        /// <summary>
        /// 公司
        /// </summary>
        public string LNCompany { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        public string LNJobtitle { get; set; }

        #endregion 出租人

        #region 代理人

        public virtual string LNAGName_A { get; set; }
        public virtual string LNAGID_A { get; set; }
        public virtual string LNAGCell_A { get; set; }
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


        #endregion

        #region 建物
        public virtual string BAdd { get; set; }
        public virtual string BAdd_1 { get; set; }
        public virtual string BAdd_1_1 { get; set; }
        public virtual string BAdd_1_2 { get; set; }
        public virtual string BAdd_2 { get; set; }
        public virtual string BAdd_2_1 { get; set; }
        public virtual string BAdd_2_2 { get; set; }
        public virtual string BAdd_2_3 { get; set; }
        public virtual string BAdd_2_4 { get; set; }
        public virtual string BAdd_3 { get; set; }
        public virtual string BAdd_3_1 { get; set; }
        public virtual string BAdd_3_2 { get; set; }
        public virtual string BAdd_4 { get; set; }
        public virtual string BAdd_5 { get; set; }
        public virtual string BAdd_6 { get; set; }
        public virtual string BAdd_7 { get; set; }
        public virtual string BAdd_8 { get; set; }
        public virtual string BAdd_9 { get; set; }
        public virtual string BNo_1 { get; set; }

        /// <summary>
        /// 建物座落建號（小段）
        /// </summary>
        public virtual string BNo_2 { get; set; }

        /// <summary>
        /// 建物座落建號（建號）
        /// </summary>
        public virtual string BNo_3 { get; set; }
        /// <summary>
        /// 建物坐落地號(地段)A
        /// </summary>
        public virtual string BPNo_1_A { get; set; }

        /// <summary>
        /// 建物坐落地號(小段)A
        /// </summary>
        public virtual string BPNo_2_A { get; set; }

        /// <summary>
        /// 建物坐落地號(地號)A
        /// </summary>
        public virtual string BPNo_3_A { get; set; }

        /// <summary>
        /// 建物坐落地號(地段)B
        /// </summary>
        public virtual string BPNo_1_B { get; set; }

        /// <summary>
        /// 建物坐落地號(小段)B
        /// </summary>
        public virtual string BPNo_2_B { get; set; }

        /// <summary>
        /// 建物坐落地號(地號)B
        /// </summary>
        public virtual string BPNo_3_B { get; set; }
        public virtual string BTHouse { get; set; }
        public virtual string BTApartment { get; set; }

        /// <summary>
        /// 華廈
        /// </summary>
        public virtual string BTMansion { get; set; }

        /// <summary>
        /// 電梯大樓
        /// </summary>
        public virtual string BTElevator { get; set; }

        /// <summary>
        /// 農舍
        /// </summary>
        public virtual string BTFarmHouse { get; set; }

        /// <summary>
        /// 廠房
        /// </summary>
        public virtual string BTWorkshop { get; set; }

        /// <summary>
        /// 建築完成日期
        /// </summary>
        public virtual string BCDate { get; set; }

        /// <summary>
        /// 樓層(層數)
        /// </summary>
        public virtual string BLevel { get; set; }

        /// <summary>
        /// 樓層(層次）
        /// </summary>
        public virtual string BFloor { get; set; }
        public virtual string BRoom { get; set; }
        public virtual string BLD { get; set; }
        public virtual string BBath { get; set; }

        /// <summary>
        /// 第1層
        /// </summary>
        public virtual string BFloor_1 { get; set; }

        /// <summary>
        /// 第1層面積/平方公尺
        /// </summary>
        public virtual string BArea_1 { get; set; }

        /// <summary>
        /// 第二層
        /// </summary>
        public virtual string BFloor_2 { get; set; }

        /// <summary>
        /// 第2層面積/平方公尺
        /// </summary>
        public virtual string BArea_2 { get; set; }

        /// <summary>
        /// 第3層
        /// </summary>
        public virtual string BFloor_3 { get; set; }

        /// <summary>
        /// 第3層面積/平方公尺
        /// </summary>
        public virtual string BArea_3 { get; set; }

        /// <summary>
        /// 謄本面積(平方公尺)1
        /// </summary>
        public virtual string BArea { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        public virtual string BUse { get; set; }

        /// <summary>
        /// 代租租金1年/月
        /// </summary>
        public virtual string BTRent { get; set; }

        /// <summary>
        /// 合約租金(自然人房東資料卡)10/14MM修改
        /// </summary>
        public virtual string BTCRent { get; set; }

        /// <summary>
        /// 押金2
        /// </summary>
        public virtual string Bdeposit { get; set; }
        public virtual string BSuper_Y { get; set; }
        public virtual string BSuper_N { get; set; }
        public virtual string BMFee_Y { get; set; }
        public virtual string BMFee_N { get; set; }

        /// <summary>
        /// 公寓大廈規約或其他住戶應遵循事項(無)
        /// </summary>
        public virtual string BConStature_N { get; set; }

        /// <summary>
        /// 公寓大廈規約或其他住戶應遵循事項(有)
        /// </summary>
        public virtual string BConStature_Y { get; set; }

        /// <summary>
        /// 定期辦理消防安全檢查(無)
        /// </summary>
        public virtual string BFireSaIn_N { get; set; }

        /// <summary>
        /// 定期辦理消防安全檢查(有)
        /// </summary>
        public virtual string BFireSaIn_Y { get; set; }

        /// <summary>
        /// 有無車位(有)
        /// </summary>
        public virtual string BCar_Y { get; set; }

        /// <summary>
        /// 有無車位(無)
        /// </summary>
        public virtual string BCar_N { get; set; }

        /// <summary>
        /// 坡道平面
        /// </summary>
        public virtual string BRampFlatParking { get; set; }

        /// <summary>
        /// 坡道機械
        /// </summary>
        public virtual string BRampMechParking { get; set; }

        /// <summary>
        /// 機械平面
        /// </summary>
        public virtual string BMechFlatParking { get; set; }

        /// <summary>
        /// 機械機械
        /// </summary>
        public virtual string BMechMechParking { get; set; }
        public virtual string BCarFloor { get; set; }
        public virtual string BCarNo { get; set; }

        /// <summary>
        /// 曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事(無)
        /// </summary>
        public virtual string BMurder_N { get; set; }

        /// <summary>
        /// 曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事(有)
        /// </summary>
        public virtual string BMurder_Y { get; set; }

        /// <summary>
        /// 於產權持有前出租人確認無_本建物(專有部分)是否曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事
        /// </summary>
        public virtual string BMurder_No_Check { get; set; }

        /// <summary>
        /// 於產權持有前出租人知道發生_本建物(專有部分)是否曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事
        /// </summary>
        public virtual string BMurder_Yes_Check { get; set; }

        /// <summary>
        /// 於產權持有前出租人不知曾經發生_本建物(專有部分)是否曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事
        /// </summary>
        public virtual string BMurder_Yes_Dont { get; set; }

        #region Building1

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

        #endregion

        #region 匯款資料
        public virtual bool? LCanModify { get; set; }

        public virtual string LAName { get; set; }
        public virtual string LBankName { get; set; }
        public virtual string LBankNo { get; set; }
        public virtual string LBranchName { get; set; }
        public virtual string LBranchNo { get; set; }
        public virtual string LANo { get; set; }

        #endregion

        #endregion A000001 自然人房東資料卡

        #region A000201 房屋委託租賃契約書

        /// <summary>
        /// 謄本面積（坪）2
        /// </summary>
        public virtual string BPing { get; set; }

        /// <summary>
        /// 廚房
        /// </summary>
        public virtual string BKitchen { get; set; }

        /// <summary>
        /// 陽台
        /// </summary>
        public virtual string BBalcony { get; set; }

        /// <summary>
        /// 有電梯
        /// </summary>
        public virtual string Belevator_Y { get; set; }

        /// <summary>
        /// 無電梯
        /// </summary>
        public virtual string Belevator_N { get; set; }

        /// <summary>
        /// 車位租金
        /// </summary>
        public virtual string BParkFee { get; set; }

        /// <summary>
        /// 大樓管理費
        /// </summary>
        public virtual string BMFee { get; set; }

        /// <summary>
        /// 可開火
        /// </summary>
        public virtual string BCook_Y { get; set; }

        /// <summary>
        /// 不可開伙
        /// </summary>
        public virtual string BCook_N { get; set; }

        /// <summary>
        /// 可寵物
        /// </summary>
        public virtual string BPet_Y { get; set; }

        /// <summary>
        /// 不可寵物
        /// </summary>
        public virtual string BPet_N { get; set; }

        /// <summary>
        /// 可神明桌
        /// </summary>
        public virtual string BGodT_Y { get; set; }

        /// <summary>
        /// 不可神明桌
        /// </summary>
        public virtual string BGodT_N { get; set; }

        /// <summary>
        /// 親自開門
        /// </summary>
        public virtual string BOpen { get; set; }

        #endregion

        #region A000203 房屋委託租賃契約書

        /// <summary>
        /// 實際使用坪數
        /// </summary>
        public virtual string BRealPING { get; set; }

        #endregion

        #region A000003 法人房東資料卡

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

        #endregion

        #region A000004 法人房客資料卡

        #region 承租人

        public virtual string RCID { get; set; }

        public virtual string RCName { get; set; }

        public virtual string RCEngName { get; set; }

        public virtual string RCTel_1 { get; set; }

        public virtual string RCTel_2 { get; set; }

        public virtual string RCRep { get; set; }

        public virtual string RCCell { get; set; }

        public virtual string RCMail { get; set; }

        public virtual string RCAdd { get; set; }

        public virtual string RCAdd_1 { get; set; }

        public virtual string RCAdd_1_1 { get; set; }

        public virtual string RCAdd_1_2 { get; set; }

        public virtual string RCAdd_2 { get; set; }

        public virtual string RCAdd_2_1 { get; set; }

        public virtual string RCAdd_2_2 { get; set; }

        public virtual string RCAdd_2_3 { get; set; }

        public virtual string RCAdd_2_4 { get; set; }

        public virtual string RCAdd_3 { get; set; }

        public virtual string RCAdd_3_1 { get; set; }

        public virtual string RCAdd_3_2 { get; set; }

        public virtual string RCAdd_4 { get; set; }

        public virtual string RCAdd_5 { get; set; }

        public virtual string RCAdd_6 { get; set; }

        public virtual string RCAdd_7 { get; set; }

        public virtual string RCAdd_8 { get; set; }

        public virtual string RCAdd_9 { get; set; }

        public virtual string RCAddSame { get; set; }

        public virtual string RCAddC_1 { get; set; }

        public virtual string RCAddC_1_1 { get; set; }

        public virtual string RCAddC_1_2 { get; set; }

        public virtual string RCAddC_2 { get; set; }

        public virtual string RCAddC_2_1 { get; set; }

        public virtual string RCAddC_2_2 { get; set; }

        public virtual string RCAddC_2_3 { get; set; }

        public virtual string RCAddC_2_4 { get; set; }

        public virtual string RCAddC_3 { get; set; }

        public virtual string RCAddC_3_1 { get; set; }

        public virtual string RCAddC_3_2 { get; set; }

        public virtual string RCAddC_4 { get; set; }

        public virtual string RCAddC_5 { get; set; }

        public virtual string RCAddC_6 { get; set; }

        public virtual string RCAddC_7 { get; set; }

        public virtual string RCAddC_8 { get; set; }

        public virtual string RCAddC_9 { get; set; }

        #endregion

        #region 緊急聯絡人(親屬)

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_姓名
        /// </summary>
        public string RCECName { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_身分證字號
        /// </summary>
        public string RCECID { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)性別_男
        /// </summary>
        public string RCECGender_1 { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)性別_女
        /// </summary>
        public string RCECGender_2 { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_生日
        /// </summary>
        public string RCECBirthday { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_關係
        /// </summary>
        public string RCECRelation { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_連絡電話(手機)
        /// </summary>
        public string RCECCell { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_連絡電話(區號)
        /// </summary>
        public string RCECTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_連絡電話(號碼)
        /// </summary>
        public string RCECTel_2 { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_戶籍地址
        /// </summary>
        public string RCECAdd { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_通訊地址同戶籍地址
        /// </summary>
        public string RCECAddSame { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_通訊地址
        /// </summary>
        public string RCECAddC { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_9 { get; set; }


        #endregion 急聯絡人(親屬)

        #region 緊急聯絡人(朋友)

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_姓名
        /// </summary>
        public string RCECFName { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_身分證字號
        /// </summary>
        public string RCECFID { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_性別_男
        /// </summary>
        public string RCECFGender_1 { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_性別_女
        /// </summary>
        public string RCECFGender_2 { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_生日
        /// </summary>
        public string RCECFBirthday { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_連絡電話
        /// </summary>
        public string RCECFCell { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_關係
        /// </summary>
        public string RCECFRelation { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_戶籍地址
        /// </summary>
        public string RCECFAdd { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_通訊地址同戶籍地址
        /// </summary>
        public string RCECFAddSame { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_通訊地址
        /// </summary>
        public string RCECFAddC { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_9 { get; set; }


        #endregion

        #region 簽約代理人

        /// <summary>
        /// 設置或獲取簽約代理人_姓名
        /// </summary>
        public string RCAGName { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人_身分證字號
        /// </summary>
        public string RCAGID { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人性別_男
        /// </summary>
        public string RCAGGender_1 { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人性別_女
        /// </summary>
        public string RCAGGender_2 { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人_生日
        /// </summary>
        public string RCAGBirthday { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人_戶籍地址
        /// </summary>
        public string RCAGAdd { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人_通訊地址同戶籍地址
        /// </summary>
        public string RCAGAddSame { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人_通訊地址
        /// </summary>
        public string RCAGAddC { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_9 { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人_聯絡電話(手機)
        /// </summary>
        public string RCAGCell { get; set; }

        /// <summary>
        /// 設置或獲取--簽約代理人_聯絡電話(區號)
        /// </summary>
        public string RCAGTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取-簽約代理人_聯絡電話(號碼)
        /// </summary>
        public string RCAGTel_2 { get; set; }


        #endregion

        #region 保證人

        /// <summary>
        /// 設置或獲取保證人_姓名
        /// </summary>
        public string RCGName { get; set; }

        /// <summary>
        /// 設置或獲取保證人_身分證字號
        /// </summary>
        public string RCGID { get; set; }

        /// <summary>
        /// 設置或獲取保證人性別_男
        /// </summary>
        public string RCGGender_1 { get; set; }

        /// <summary>
        /// 設置或獲取保證人性別_女
        /// </summary>
        public string RCGGender_2 { get; set; }

        /// <summary>
        /// 設置或獲取保證人_生日
        /// </summary>
        public string RCGBirthday { get; set; }

        /// <summary>
        /// 設置或獲取保證人_戶籍地址
        /// </summary>
        public string RCGAdd { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取保證人_通訊地址同戶籍地址
        /// </summary>
        public string RCGAddSame { get; set; }

        /// <summary>
        /// 設置或獲取保證人_通訊地址
        /// </summary>
        public string RCGCAdd { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取保證人_聯絡電話
        /// </summary>
        public string RCGCell { get; set; }

        /// <summary>
        /// 保證人_關係
        /// </summary>
        public string RCGRelation { get; set; }


        #endregion

        #region 匯款資料

        public virtual bool? RCanModify { get; set; }
        public virtual string RAName { get; set; }
        public virtual string RBankName { get; set; }
        public virtual string RBankNo { get; set; }
        public virtual string RBranchName { get; set; }
        public virtual string RBranchNo { get; set; }
        public virtual string RANo { get; set; }

        #endregion

        #region 家庭成員

        public virtual string RCFName_A { get; set; }

        public virtual string RCFID_1_A { get; set; }

        public virtual string RCFBirthday_A { get; set; }

        public virtual string RCFRelation_A { get; set; }

        public virtual string RCFCell_A { get; set; }

        public virtual string RCFName_B { get; set; }

        public virtual string RCFID_1_B { get; set; }

        public virtual string RCFBirthday_B { get; set; }

        public virtual string RCFRelation_B { get; set; }

        public virtual string RCFCell_B { get; set; }

        public virtual string RCFName_C { get; set; }

        public virtual string RCFID_1_C { get; set; }

        public virtual string RCFBirthday_C { get; set; }

        public virtual string RCFRelation_C { get; set; }

        public virtual string RCFCell_C { get; set; }

        public virtual string RCFName_D { get; set; }

        public virtual string RCFID_1_D { get; set; }

        public virtual string RCFBirthday_D { get; set; }

        public virtual string RCFRelation_D { get; set; }

        public virtual string RCFCell_D { get; set; }

        public virtual string RCFName_E { get; set; }

        public virtual string RCFID_1_E { get; set; }

        public virtual string RCFBirthday_E { get; set; }

        public virtual string RCFRelation_E { get; set; }

        public virtual string RCFCell_E { get; set; }

        public virtual string RCFName_F { get; set; }

        public virtual string RCFID_1_F { get; set; }

        public virtual string RCFBirthday_F { get; set; }

        public virtual string RCFRelation_F { get; set; }

        public virtual string RCFCell_F { get; set; }

        public virtual string RCFName_G { get; set; }

        public virtual string RCFID_1_G { get; set; }

        public virtual string RCFBirthday_G { get; set; }

        public virtual string RCFRelation_G { get; set; }

        public virtual string RCFCell_G { get; set; }

        #endregion 家庭成員

        #endregion A000004 法人房客資料卡

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

        /// <summary>
        /// BZ085
        /// </summary>
        public virtual string BZ085 { get; set; }

        /// <summary>
        /// BZ086
        /// </summary>
        public virtual string BZ086 { get; set; }

        /// <summary>
        /// BZ087
        /// </summary>
        public virtual string BZ087 { get; set; }

        /// <summary>
        /// BZ088
        /// </summary>
        public virtual string BZ088 { get; set; }

        /// <summary>
        /// BZ089
        /// </summary>
        public virtual string BZ089 { get; set; }

        /// <summary>
        /// BZ090
        /// </summary>
        public virtual string BZ090 { get; set; }

        /// <summary>
        /// BZ091
        /// </summary>
        public virtual string BZ091 { get; set; }

        /// <summary>
        /// BZ092
        /// </summary>
        public virtual string BZ092 { get; set; }

        /// <summary>
        /// BZ093
        /// </summary>
        public virtual string BZ093 { get; set; }

        /// <summary>
        /// BZ094
        /// </summary>
        public virtual string BZ094 { get; set; }

        /// <summary>
        /// BZ095
        /// </summary>
        public virtual string BZ095 { get; set; }

        /// <summary>
        /// BZ096
        /// </summary>
        public virtual string BZ096 { get; set; }

        /// <summary>
        /// BZ097
        /// </summary>
        public virtual string BZ097 { get; set; }

        /// <summary>
        /// BZ098
        /// </summary>
        public virtual string BZ098 { get; set; }

        /// <summary>
        /// BZ099
        /// </summary>
        public virtual string BZ099 { get; set; }

        /// <summary>
        /// BZ100
        /// </summary>
        public virtual string BZ100 { get; set; }

        /// <summary>
        /// BZ101
        /// </summary>
        public virtual string BZ101 { get; set; }

        /// <summary>
        /// BZ102
        /// </summary>
        public virtual string BZ102 { get; set; }

        /// <summary>
        /// BZ103
        /// </summary>
        public virtual string BZ103 { get; set; }



        #endregion BZ欄位

        #region RO欄位

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ROName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ROAdd { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ROTel { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string AGName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string AGLRNo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ROUserName { get; set; }

        #endregion

        #region LS欄位

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string LSName { get; set; }

        /// <summary>
        /// 身分證字號/統一編號
        /// </summary>
        public virtual string LSID { get; set; }

        /// <summary>
        /// 電話_區號
        /// </summary>
        public virtual string LSTel_1 { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public virtual string LSTel_2 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_1 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_1_1 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_1_2 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_2 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_2_1 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_2_2 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_2_3 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_2_4 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_3 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_3_1 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_3_2 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_4 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_5 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_6 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_7 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_8 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_9 { get; set; }

        #endregion

        #region 簽名

        /// <summary>
        /// 圖檔64位元流 用於inputdto
        /// </summary>
        public virtual string SignatureBase64_1 { get; set; }
        public virtual string SignatureBase64_2 { get; set; }
        public virtual string SignatureBase64_3 { get; set; }
        public virtual string SignatureBase64_4 { get; set; }
        public virtual string SignatureBase64_5 { get; set; }

        /// <summary>
        /// 圖檔檔案位置 用於outputdto
        /// </summary>
        public string SignatureImgPath_1 { get; set; }
        public string SignatureImgPath_2 { get; set; }
        public string SignatureImgPath_3 { get; set; }
        public string SignatureImgPath_4 { get; set; }
        public string SignatureImgPath_5 { get; set; }


        #endregion 簽名


        /// <summary>
        /// 是否為產生PDF按鈕
        /// </summary>
        public virtual string IsGenPDF { get; set; }
    }
}
