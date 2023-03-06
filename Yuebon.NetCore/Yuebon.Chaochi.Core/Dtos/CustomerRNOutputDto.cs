using System;

namespace Yuebon.Chaochi.Core.Dtos
{
    [Serializable]
    public class CustomerRNOutputDto
    {
        /// <summary>
        /// 主鍵
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string RNName { get; set; }

        /// <summary>
        /// 身分證號,居留證號
        /// </summary>
        public virtual string RNID { get; set; }

        /// <summary>
        /// LINEID
        /// </summary>
        public virtual string RNLine { get; set; }

        /// <summary>
        /// 承租地址
        /// </summary>
        public virtual string BAdd { get; set; }

        /// <summary>
        /// 性別_男
        /// </summary>
        public virtual string RNGender1 { get; set; }

        /// <summary>
        /// 性別_女
        /// </summary>
        public virtual string RNGender2 { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public virtual string RNBirthday { get; set; }

        /// <summary>
        /// 戶口名簿戶號
        /// </summary>
        public virtual string RNFAccount { get; set; }

        /// <summary>
        /// 完整電話號碼_日
        /// </summary>
        public virtual string RNTel { get; set; }

        /// <summary>
        /// 電話區碼_日
        /// </summary>
        public virtual string RNTel_1 { get; set; }

        /// <summary>
        /// 電話號碼_日
        /// </summary>
        public virtual string RNTel_2 { get; set; }

        /// <summary>
        /// 完整電話號碼_夜
        /// </summary>
        public virtual string RNTelN { get; set; }

        /// <summary>
        /// 電話區碼_夜
        /// </summary>
        public virtual string RNTelN_1 { get; set; }

        /// <summary>
        /// 電話號碼_夜
        /// </summary>
        public virtual string RNTelN_2 { get; set; }

        /// <summary>
        /// 行動電話號碼
        /// </summary>
        public virtual string RNCell { get; set; }

        /// <summary>
        /// 電子信箱
        /// </summary>
        public virtual string RNMail { get; set; }

        /// <summary>
        /// 完整戶籍地址
        /// </summary>
        public virtual string RNAdd { get; set; }

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
        /// 完整通訊地址
        /// </summary>
        public virtual string RNAddC { get; set; }

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

        /// <summary>
        /// 一般戶
        /// </summary>
        public virtual string RNQualify1C { get; set; }

        /// <summary>
        /// 第一類弱勢戶
        /// </summary>
        public virtual string RNQualify2C { get; set; }

        /// <summary>
        /// 第二類弱勢戶
        /// </summary>
        public virtual string RNQualify3C { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號
        /// </summary>
        public virtual string RNMMAccount { get; set; }

        /// <summary>
        /// 子女人數
        /// </summary>
        public virtual string RNChild { get; set; }

        #region 家庭成員(已廢除，合併至RNFDto)

        ///// <summary>
        ///// 家庭成員1_生日
        ///// </summary>
        //public virtual string RNFBirthday_A { get; set; }

        ///// <summary>
        ///// 家庭成員1_稱謂
        ///// </summary>
        //public virtual string RNFTitle_A { get; set; }

        ///// <summary>
        ///// 家庭成員1_年所得
        ///// </summary>
        //public virtual string RNFIncome_A { get; set; }

        ///// <summary>
        ///// 家庭成員2_姓名
        ///// </summary>
        //public virtual string RNFName_B { get; set; }

        ///// <summary>
        ///// 家庭成員2_身份證號/居留證號
        ///// </summary>
        //public virtual string RNFID_1_B { get; set; }

        ///// <summary>
        ///// 家庭成員2_生日
        ///// </summary>
        //public virtual string RNFBirthday_B { get; set; }

        ///// <summary>
        ///// 家庭成員2_稱謂
        ///// </summary>
        //public virtual string RNFTitle_B { get; set; }

        ///// <summary>
        ///// 家庭成員2_年所得
        ///// </summary>
        //public virtual string RNFIncome_B { get; set; }

        ///// <summary>
        ///// 家庭成員3_姓名
        ///// </summary>
        //public virtual string RNFName_C { get; set; }

        ///// <summary>
        ///// 家庭成員3_身份證號/居留證號
        ///// </summary>
        //public virtual string RNFID_1_C { get; set; }

        ///// <summary>
        ///// 家庭成員3_生日
        ///// </summary>
        //public virtual string RNFBirthday_C { get; set; }

        ///// <summary>
        ///// 家庭成員3_稱謂
        ///// </summary>
        //public virtual string RNFTitle_C { get; set; }

        ///// <summary>
        ///// 家庭成員3_年所得
        ///// </summary>
        //public virtual string RNFIncome_C { get; set; }

        ///// <summary>
        ///// 家戶年所得
        ///// </summary>
        //public virtual string RNFTSalary { get; set; }

        ///// <summary>
        ///// 家庭成員月收入
        ///// </summary>
        //public virtual string RNFMSalary { get; set; }

        #endregion 家庭成員(已廢除，合併至RNFDto)

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
        /// 緊急聯絡人電話
        /// </summary>
        public virtual string RNECTel_1 { get; set; }
        public virtual string RNECTel_2 { get; set; }

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

        /// <summary>
        /// 代理人姓名
        /// </summary>
        public virtual string RNAGName_A { get; set; }

        /// <summary>
        /// 代理人身份證字號/居留證號
        /// </summary>
        public virtual string RNAGID_A { get; set; }

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


        public virtual string CreatorUserName { get; set; }

        /// <summary>
        /// 代理人手機
        /// </summary>
        public virtual string RNAGCell_A { get; set; }

        /// <summary>
        /// 代理人電話
        /// </summary>
        public virtual string RNAGTel_1_A { get; set; }
        public virtual string RNAGTel_2_A { get; set; }

        #region 0726MM新增保證人欄位

        public virtual string RNGName { get; set; }
        public virtual string RNGID { get; set; }
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
        public virtual string RNGMail { get; set; }

        #endregion 0726MM新增保證人欄位

        #region 8/17 MM 欄位修改0817.docx

        /// <summary>
        /// 緊急連絡人身分證字號
        /// </summary>
        public virtual string RNECID { get; set; }

        /// <summary>
        /// 緊急連絡人生日
        /// </summary>
        public virtual string RNECBirthday { get; set; }

        /// <summary>
        /// 保證人生日
        /// </summary>
        public virtual string RNGBirthday { get; set; }

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
        /// □中低收入戶。RNFName_C
        /// </summary>
        public virtual string RNQualify3C_2 { get; set; }

        /// <summary>
        /// 申請人稱謂(值一定為"本人")
        /// </summary>
        public virtual string RNFRelation { get; set; }

        /// <summary>
        /// 租金補助 申請金額 新台幣
        /// </summary>
        public virtual string RNRentSUBAFee { get; set; }
        #endregion 8/17 MM 欄位修改0817.docx

        /// <summary>
        /// 同住人頁籤
        /// </summary>
        public virtual RNFOutputDto RNFOutputDto { get; set; }

        public DateTime? CreatorTime { get; set; }
        public string CreatorUserId { get; set; }
        public string LastModifyUserId { get; set; }
        public DateTime? LastModifyTime { get; set; }
    }
}
