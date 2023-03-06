using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.Dtos
{
    public class PDFDataModel
    {
        //**********下方為正是欄位***************

        //----------------

        #region FirstData

        /// <summary>
        /// 二房東統編(兆基)
        /// </summary>
        public virtual string SLID { get; set; }

        /// <summary>
        /// 是否為無主表單
        /// </summary>
        public string IsNoKey { get; set; }

        /// <summary>
        /// 表單編號
        /// </summary>
        public virtual string FName { get; set; }

        /// <summary>
        /// 版本號
        /// </summary>
        public virtual string Vno { get; set; }

        /// <summary>
        /// TBNO 存放BZ欄位判斷
        /// </summary>
        public virtual string TBNO { get; set; }

        /// <summary>
        /// 物件編號
        /// </summary>
        public virtual string BID { get; set; }

        /// <summary>
        /// (自然人)姓名
        /// </summary>
        public virtual string LNName { get; set; }

        /// <summary>
        /// (自然人)性別
        /// </summary>
        public virtual string LNGender { get; set; }

        /// <summary>
        /// (自然人)性別男
        /// </summary>
        public virtual string LNGender1 { get; set; }

        /// <summary>
        /// (自然人)性別女
        /// </summary>
        public virtual string LNGender2 { get; set; }

        /// <summary>
        /// (自然人)身分證字號
        /// </summary>
        public virtual string LNID { get; set; }

        /// <summary>
        /// (自然人)出生年月日
        /// </summary>
        public virtual string LNBirthday { get; set; }

        /// <summary>
        /// (自然人)出生年月日(年)
        /// </summary>
        public virtual string LNBirthday_Y { get; set; }

        /// <summary>
        /// (自然人)出生年月日(月)
        /// </summary>
        public virtual string LNBirthday_M { get; set; }

        /// <summary>
        /// (自然人)出生年月日(日)
        /// </summary>
        public virtual string LNBirthday_D { get; set; }

        /// <summary>
        /// (自然人)電話日
        /// </summary>
        public virtual string LNTel { get; set; }

        /// <summary>
        /// (自然人)電話日區碼
        /// </summary>
        public virtual string LNTel_1 { get; set; }

        /// <summary>
        /// (自然人)電話日號碼
        /// </summary>
        public virtual string LNTel_2 { get; set; }

        /// <summary>
        /// (自然人)電話夜
        /// </summary>
        public virtual string LNTelN { get; set; }

        /// <summary>
        /// (自然人)電話夜區碼
        /// </summary>
        public virtual string LNTelN_1 { get; set; }

        /// <summary>
        /// (自然人)電話夜號碼
        /// </summary>
        public virtual string LNTelN_2 { get; set; }

        /// <summary>
        /// (自然人)手機
        /// </summary>
        public virtual string LNCell { get; set; }

        /// <summary>
        /// (自然人)電子信箱
        /// </summary>
        public virtual string LNMail { get; set; }

        /// <summary>
        /// (自然人)戶籍地址
        /// </summary>
        public virtual string LNAdd { get; set; }

        /// <summary>
        /// (自然人)戶籍地址(縣市)
        /// </summary>
        public virtual string LNAdd_1 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址(縣)
        /// </summary>
        public virtual string LNAdd_1_1 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址(市)
        /// </summary>
        public virtual string LNAdd_1_2 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址(鄉鎮市區)
        /// </summary>
        public virtual string LNAdd_2 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址(鄉)
        /// </summary>
        public virtual string LNAdd_2_1 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址(市)
        /// </summary>
        public virtual string LNAdd_2_2 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址(鎮)
        /// </summary>
        public virtual string LNAdd_2_3 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址(區)
        /// </summary>
        public virtual string LNAdd_2_4 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址(街路)
        /// </summary>
        public virtual string LNAdd_3 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址(街)
        /// </summary>
        public virtual string LNAdd_3_1 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址(路)
        /// </summary>
        public virtual string LNAdd_3_2 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址4(段)
        /// </summary>
        public virtual string LNAdd_4 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址5(巷)
        /// </summary>
        public virtual string LNAdd_5 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址6(弄)
        /// </summary>
        public virtual string LNAdd_6 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址7(號)
        /// </summary>
        public virtual string LNAdd_7 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址8(樓)
        /// </summary>
        public virtual string LNAdd_8 { get; set; }

        /// <summary>
        /// (自然人)戶籍地址9(之)
        /// </summary>
        public virtual string LNAdd_9 { get; set; }

        /// <summary>
        /// (自然人)同戶籍地址
        /// </summary>
        public virtual string LNAddSame { get; set; }

        /// <summary>
        /// (自然人)通訊地址
        /// </summary>
        public virtual string LNAddC { get; set; }

        /// <summary>
        /// (自然人)通訊地址(縣市)
        /// </summary>
        public virtual string LNAddC_1 { get; set; }

        /// <summary>
        /// (自然人)通訊地址(縣)
        /// </summary>
        public virtual string LNAddC_1_1 { get; set; }

        /// <summary>
        /// (自然人)通訊地址(市)
        /// </summary>
        public virtual string LNAddC_1_2 { get; set; }

        /// <summary>
        /// (自然人)通訊地址(鄉鎮市區)
        /// </summary>
        public virtual string LNAddC_2 { get; set; }

        /// <summary>
        /// (自然人)通訊地址(鄉)
        /// </summary>
        public virtual string LNAddC_2_1 { get; set; }

        /// <summary>
        /// (自然人)通訊地址(市)
        /// </summary>
        public virtual string LNAddC_2_2 { get; set; }

        /// <summary>
        /// (自然人)通訊地址(鎮)
        /// </summary>
        public virtual string LNAddC_2_3 { get; set; }

        /// <summary>
        /// (自然人)通訊地址(區)
        /// </summary>
        public virtual string LNAddC_2_4 { get; set; }

        /// <summary>
        /// (自然人)通訊地址(街路)
        /// </summary>
        public virtual string LNAddC_3 { get; set; }

        /// <summary>
        /// (自然人)通訊地址(街)
        /// </summary>
        public virtual string LNAddC_3_1 { get; set; }

        /// <summary>
        /// (自然人)通訊地址(路)
        /// </summary>
        public virtual string LNAddC_3_2 { get; set; }

        /// <summary>
        /// (自然人)通訊地址4(段)
        /// </summary>
        public virtual string LNAddC_4 { get; set; }

        /// <summary>
        /// (自然人)通訊地址5(巷)
        /// </summary>
        public virtual string LNAddC_5 { get; set; }

        /// <summary>
        /// (自然人)通訊地址6(弄)
        /// </summary>
        public virtual string LNAddC_6 { get; set; }

        /// <summary>
        /// (自然人)通訊地址7(號)
        /// </summary>
        public virtual string LNAddC_7 { get; set; }

        /// <summary>
        /// (自然人)通訊地址8(樓)
        /// </summary>
        public virtual string LNAddC_8 { get; set; }

        /// <summary>
        /// (自然人)通訊地址9(之)
        /// </summary>
        public virtual string LNAddC_9 { get; set; }

        /// <summary>
        /// (自然人)代理人1姓名
        /// </summary>
        public virtual string LNAGName_A { get; set; }

        /// <summary>
        /// (自然人)代理人1身分證字號
        /// </summary>
        public virtual string LNAGID_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址
        /// </summary>
        public virtual string LNAGAdd_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址(縣市)
        /// </summary>
        public virtual string LNAGAdd_1_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址(縣)
        /// </summary>
        public virtual string LNAGAdd_1_1_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址(市)
        /// </summary>
        public virtual string LNAGAdd_1_2_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址(鄉鎮市區)
        /// </summary>
        public virtual string LNAGAdd_2_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址(鄉)
        /// </summary>
        public virtual string LNAGAdd_2_1_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址(市)
        /// </summary>
        public virtual string LNAGAdd_2_2_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址(鎮)
        /// </summary>
        public virtual string LNAGAdd_2_3_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址(區)
        /// </summary>
        public virtual string LNAGAdd_2_4_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址(街路)
        /// </summary>
        public virtual string LNAGAdd_3_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址(街)
        /// </summary>
        public virtual string LNAGAdd_3_1_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址(路)
        /// </summary>
        public virtual string LNAGAdd_3_2_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址4(段)
        /// </summary>
        public virtual string LNAGAdd_4_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址5(巷)
        /// </summary>
        public virtual string LNAGAdd_5_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址6(弄)
        /// </summary>
        public virtual string LNAGAdd_6_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址7(號)
        /// </summary>
        public virtual string LNAGAdd_7_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址8(樓)
        /// </summary>
        public virtual string LNAGAdd_8_A { get; set; }

        /// <summary>
        /// (自然人)代理人1戶籍地址9(之)
        /// </summary>
        public virtual string LNAGAdd_9_A { get; set; }

        /// <summary>
        /// (自然人)代理人1手機
        /// </summary>
        public virtual string LNAGCell_A { get; set; }

        /// <summary>
        /// (自然人)代理人1電話
        /// </summary>
        public virtual string LNAGTel_A { get; set; }

        /// <summary>
        /// (自然人)代理人1電話區碼
        /// </summary>
        public virtual string LNAGTel_1_A { get; set; }

        /// <summary>
        /// (自然人)代理人1電話號碼
        /// </summary>
        public virtual string LNAGTel_2_A { get; set; }

        /// <summary>
        /// (自然人)代理人2姓名
        /// </summary>
        public virtual string LNAGName_B { get; set; }

        /// <summary>
        /// (自然人)代理人2身分證字號
        /// </summary>
        public virtual string LNAGID_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址
        /// </summary>
        public virtual string LNAGAdd_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址(縣市)
        /// </summary>
        public virtual string LNAGAdd_1_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址(縣)
        /// </summary>
        public virtual string LNAGAdd_1_1_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址(市)
        /// </summary>
        public virtual string LNAGAdd_1_2_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址(鄉鎮市區)
        /// </summary>
        public virtual string LNAGAdd_2_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址(鄉)
        /// </summary>
        public virtual string LNAGAdd_2_1_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址(市)
        /// </summary>
        public virtual string LNAGAdd_2_2_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址(鎮)
        /// </summary>
        public virtual string LNAGAdd_2_3_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址(區)
        /// </summary>
        public virtual string LNAGAdd_2_4_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址(街路)
        /// </summary>
        public virtual string LNAGAdd_3_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址(街)
        /// </summary>
        public virtual string LNAGAdd_3_1_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址(路)
        /// </summary>
        public virtual string LNAGAdd_3_2_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址4(段)
        /// </summary>
        public virtual string LNAGAdd_4_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址5(巷)
        /// </summary>
        public virtual string LNAGAdd_5_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址6(弄)
        /// </summary>
        public virtual string LNAGAdd_6_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址7(號)
        /// </summary>
        public virtual string LNAGAdd_7_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址8(樓)
        /// </summary>
        public virtual string LNAGAdd_8_B { get; set; }

        /// <summary>
        /// (自然人)代理人2戶籍地址9(之)
        /// </summary>
        public virtual string LNAGAdd_9_B { get; set; }

        /// <summary>
        /// (自然人)代理人2手機
        /// </summary>
        public virtual string LNAGCell_B { get; set; }

        /// <summary>
        /// (自然人)代理人2電話
        /// </summary>
        public virtual string LNAGTel_B { get; set; }

        /// <summary>
        /// (自然人)代理人2電話區碼
        /// </summary>
        public virtual string LNAGTel_1_B { get; set; }

        /// <summary>
        /// (自然人)代理人2電話號碼
        /// </summary>
        public virtual string LNAGTel_2_B { get; set; }

        /// <summary>
        /// (法人)法人名稱
        /// </summary>
        public virtual string LCName { get; set; }

        /// <summary>
        /// (法人)代表人
        /// </summary>
        public virtual string LCRep { get; set; }

        /// <summary>
        /// (法人)統一編號
        /// </summary>
        public virtual string LCID { get; set; }

        /// <summary>
        /// (法人)電話
        /// </summary>
        public virtual string LCTel { get; set; }

        /// <summary>
        /// (法人)電話區碼
        /// </summary>
        public virtual string LCTel_1 { get; set; }

        /// <summary>
        /// (法人)電話號碼
        /// </summary>
        public virtual string LCTel_2 { get; set; }

        /// <summary>
        /// (法人)公司登記地址
        /// </summary>
        public virtual string LCAdd { get; set; }

        /// <summary>
        /// (法人)公司登記地址(縣市)
        /// </summary>
        public virtual string LCAdd_1 { get; set; }

        /// <summary>
        /// (法人)公司登記地址(縣)
        /// </summary>
        public virtual string LCAdd_1_1 { get; set; }

        /// <summary>
        /// (法人)公司登記地址(市)
        /// </summary>
        public virtual string LCAdd_1_2 { get; set; }

        /// <summary>
        /// (法人)公司登記地址(鄉鎮市區)
        /// </summary>
        public virtual string LCAdd_2 { get; set; }

        /// <summary>
        /// (法人)公司登記地址(鄉)
        /// </summary>
        public virtual string LCAdd_2_1 { get; set; }

        /// <summary>
        /// (法人)公司登記地址(市)
        /// </summary>
        public virtual string LCAdd_2_2 { get; set; }

        /// <summary>
        /// (法人)公司登記地址(鎮)
        /// </summary>
        public virtual string LCAdd_2_3 { get; set; }

        /// <summary>
        /// (法人)公司登記地址(區)
        /// </summary>
        public virtual string LCAdd_2_4 { get; set; }

        /// <summary>
        /// (法人)公司登記地址(街路)
        /// </summary>
        public virtual string LCAdd_3 { get; set; }

        /// <summary>
        /// (法人)公司登記地址(街)
        /// </summary>
        public virtual string LCAdd_3_1 { get; set; }

        /// <summary>
        /// (法人)公司登記地址(路)
        /// </summary>
        public virtual string LCAdd_3_2 { get; set; }

        /// <summary>
        /// (法人)公司登記地址4(段)
        /// </summary>
        public virtual string LCAdd_4 { get; set; }

        /// <summary>
        /// (法人)公司登記地址5(巷)
        /// </summary>
        public virtual string LCAdd_5 { get; set; }

        /// <summary>
        /// (法人)公司登記地址6(弄)
        /// </summary>
        public virtual string LCAdd_6 { get; set; }

        /// <summary>
        /// (法人)公司登記地址7(號)
        /// </summary>
        public virtual string LCAdd_7 { get; set; }

        /// <summary>
        /// (法人)公司登記地址8(樓)
        /// </summary>
        public virtual string LCAdd_8 { get; set; }

        /// <summary>
        /// (法人)公司登記地址9(之)
        /// </summary>
        public virtual string LCAdd_9 { get; set; }

        /// <summary>
        /// (法人)代理人1姓名
        /// </summary>
        public virtual string LCAGName_A { get; set; }

        /// <summary>
        /// (法人)代理人1身分證字號
        /// </summary>
        public virtual string LCAGID_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址
        /// </summary>
        public virtual string LCAGAdd_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址(縣市)
        /// </summary>
        public virtual string LCAGAdd_1_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址(縣)
        /// </summary>
        public virtual string LCAGAdd_1_1_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址(市)
        /// </summary>
        public virtual string LCAGAdd_1_2_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址(鄉鎮市區)
        /// </summary>
        public virtual string LCAGAdd_2_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址(鄉)
        /// </summary>
        public virtual string LCAGAdd_2_1_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址(市)
        /// </summary>
        public virtual string LCAGAdd_2_2_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址(鎮)
        /// </summary>
        public virtual string LCAGAdd_2_3_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址(區)
        /// </summary>
        public virtual string LCAGAdd_2_4_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址(街路)
        /// </summary>
        public virtual string LCAGAdd_3_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址(街)
        /// </summary>
        public virtual string LCAGAdd_3_1_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址(路)
        /// </summary>
        public virtual string LCAGAdd_3_2_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址4(段)
        /// </summary>
        public virtual string LCAGAdd_4_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址5(巷)
        /// </summary>
        public virtual string LCAGAdd_5_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址6(弄)
        /// </summary>
        public virtual string LCAGAdd_6_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址7(號)
        /// </summary>
        public virtual string LCAGAdd_7_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址8(樓)
        /// </summary>
        public virtual string LCAGAdd_8_A { get; set; }

        /// <summary>
        /// (法人)代理人1戶籍地址9(之)
        /// </summary>
        public virtual string LCAGAdd_9_A { get; set; }

        /// <summary>
        /// (法人)代理人1手機
        /// </summary>
        public virtual string LCAGCell_A { get; set; }

        /// <summary>
        /// (法人)代理人1電話
        /// </summary>
        public virtual string LCAGTel_A { get; set; }

        /// <summary>
        /// (法人)代理人1電話區碼
        /// </summary>
        public virtual string LCAGTel_1_A { get; set; }

        /// <summary>
        /// (法人)代理人1電話號碼
        /// </summary>
        public virtual string LCAGTel_2_A { get; set; }

        /// <summary>
        /// (法人)代理人2姓名
        /// </summary>
        public virtual string LCAGName_B { get; set; }

        /// <summary>
        /// (法人)代理人2身分證字號
        /// </summary>
        public virtual string LCAGID_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址
        /// </summary>
        public virtual string LCAGAdd_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址(縣市)
        /// </summary>
        public virtual string LCAGAdd_1_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址(縣)
        /// </summary>
        public virtual string LCAGAdd_1_1_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址(市)
        /// </summary>
        public virtual string LCAGAdd_1_2_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址(鄉鎮市區)
        /// </summary>
        public virtual string LCAGAdd_2_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址(鄉)
        /// </summary>
        public virtual string LCAGAdd_2_1_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址(市)
        /// </summary>
        public virtual string LCAGAdd_2_2_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址(鎮)
        /// </summary>
        public virtual string LCAGAdd_2_3_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址(區)
        /// </summary>
        public virtual string LCAGAdd_2_4_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址(街路)
        /// </summary>
        public virtual string LCAGAdd_3_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址(街)
        /// </summary>
        public virtual string LCAGAdd_3_1_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址(路)
        /// </summary>
        public virtual string LCAGAdd_3_2_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址4(段)
        /// </summary>
        public virtual string LCAGAdd_4_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址5(巷)
        /// </summary>
        public virtual string LCAGAdd_5_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址6(弄)
        /// </summary>
        public virtual string LCAGAdd_6_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址7(號)
        /// </summary>
        public virtual string LCAGAdd_7_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址8(樓)
        /// </summary>
        public virtual string LCAGAdd_8_B { get; set; }

        /// <summary>
        /// (法人)代理人2戶籍地址9(之)
        /// </summary>
        public virtual string LCAGAdd_9_B { get; set; }

        /// <summary>
        /// (法人)代理人2手機
        /// </summary>
        public virtual string LCAGCell_B { get; set; }

        /// <summary>
        /// (法人)代理人2電話
        /// </summary>
        public virtual string LCAGTel_B { get; set; }

        /// <summary>
        /// (法人)代理人2電話區碼
        /// </summary>
        public virtual string LCAGTel_1_B { get; set; }

        /// <summary>
        /// (法人)代理人2電話號碼
        /// </summary>
        public virtual string LCAGTel_2_B { get; set; }
        /// <summary>
        /// 產權建物門牌地址：戶籍地址
        /// </summary>
        public virtual string BAdd { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(縣市)
        /// </summary>
        public virtual string BAdd_1 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(縣)
        /// </summary>
        public virtual string BAdd_1_1 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(市)
        /// </summary>
        public virtual string BAdd_1_2 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(鄉鎮市區)
        /// </summary>
        public virtual string BAdd_2 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(鄉)
        /// </summary>
        public virtual string BAdd_2_1 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(市)
        /// </summary>
        public virtual string BAdd_2_2 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(鎮)
        /// </summary>
        public virtual string BAdd_2_3 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(區)
        /// </summary>
        public virtual string BAdd_2_4 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(街路)
        /// </summary>
        public virtual string BAdd_3 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(街)
        /// </summary>
        public virtual string BAdd_3_1 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(路)
        /// </summary>
        public virtual string BAdd_3_2 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址4(段)
        /// </summary>
        public virtual string BAdd_4 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址5(巷)
        /// </summary>
        public virtual string BAdd_5 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址6(弄)
        /// </summary>
        public virtual string BAdd_6 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址7(號)
        /// </summary>
        public virtual string BAdd_7 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址8(樓)
        /// </summary>
        public virtual string BAdd_8 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址9(之)
        /// </summary>
        public virtual string BAdd_9 { get; set; }

        /// <summary>
        /// 建物座落建號(段)
        /// </summary>
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
        /// 建築完成日期
        /// </summary>
        public virtual string BCDate { get; set; }

        /// <summary>
        /// 建築完成日期（年）
        /// </summary>
        public virtual string BCDate_Y { get; set; }

        /// <summary>
        /// 建築完成日期（月）
        /// </summary>
        public virtual string BCDate_M { get; set; }

        /// <summary>
        /// 建築完成日期（日）
        /// </summary>
        public virtual string BCDate_D { get; set; }

        /// <summary>
        /// 樓層(層數)
        /// </summary>
        public virtual string BLevel { get; set; }

        /// <summary>
        /// 樓層(層次）
        /// </summary>
        public virtual string BFloor { get; set; }

        /// <summary>
        /// 謄本面積(平方公尺)1
        /// </summary>
        public virtual string BArea { get; set; }

        /// <summary>
        /// 謄本面積（坪）2
        /// </summary>
        public virtual string BPing { get; set; }

        /// <summary>
        /// 代租租金1年/月
        /// </summary>
        public virtual string BTRent { get; set; }

        /// <summary>
        /// 代租租金2包租代管
        /// </summary>
        public virtual string BR80C { get; set; }

        /// <summary>
        /// 代租租金3年/月
        /// </summary>
        public virtual string BR80V { get; set; }

        /// <summary>
        /// 代租租金4代租代管
        /// </summary>
        public virtual string BR90C { get; set; }

        /// <summary>
        /// 代租租金5年/月
        /// </summary>
        public virtual string BR90V { get; set; }

        /// <summary>
        /// 押金1
        /// </summary>
        public virtual string BDepositC { get; set; }

        /// <summary>
        /// 押金2
        /// </summary>
        public virtual string Bdeposit { get; set; }

        /// <summary>
        /// 帳戶名稱
        /// </summary>
        public virtual string LAName { get; set; }

        /// <summary>
        /// 身份證字號
        /// </summary>
        public virtual string LAID { get; set; }

        /// <summary>
        /// 金融機構名稱
        /// </summary>
        public virtual string LBankName { get; set; }

        /// <summary>
        /// 金融機構代碼
        /// </summary>
        public virtual string LBankNo { get; set; }

        /// <summary>
        /// 分行名稱3碼
        /// </summary>
        public virtual string LBranchName { get; set; }

        /// <summary>
        /// 分行代碼4碼
        /// </summary>
        public virtual string LBranchNo { get; set; }

        /// <summary>
        /// 帳戶號碼
        /// </summary>
        public virtual string LANo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RAName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RAID { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RBankName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RBankNo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RBranchName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RBranchNo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RANo { get; set; }



        #region 不知作用?

        /*

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

        */
        #endregion  不知作用?

        public virtual string BRoom { get; set; }
        public virtual string BLD { get; set; }
        public virtual string BBath { get; set; }
        public virtual string BKitchen { get; set; }
        public virtual string BBalcony { get; set; }

        /// <summary>
        /// 公寓
        /// </summary>
        public virtual string BTApartment { get; set; }
        public virtual string BTElevatorB { get; set; }
        public virtual string BTHouse { get; set; }
        public virtual string Belevator_Y { get; set; }
        public virtual string Belevator_N { get; set; }
        public virtual string BSuper_Y { get; set; }
        public virtual string BSuper_N { get; set; }
        public virtual string Bhousehold { get; set; }
        public virtual string Bddirection { get; set; }
        public virtual string BCarNo { get; set; }
        public virtual string BAboveG { get; set; }
        public virtual string BUnderG { get; set; }
        public virtual string BRampParking { get; set; }
        public virtual string BLiftParking { get; set; }
        public virtual string BCarFloor { get; set; }
        public virtual string BPParking { get; set; }
        public virtual string BMParking { get; set; }
        public virtual string BMFeeV { get; set; }
        public virtual string BCleaningFeeV { get; set; }
        public virtual string BCableTVFeeV { get; set; }
        public virtual string BInterNetFeeV { get; set; }
        public virtual string BWaterBillV { get; set; }
        public virtual string BElectricityBillV { get; set; }
        public virtual string BGasFeeV { get; set; }
        public virtual string BParkFee { get; set; }
        public virtual string BParkCFee { get; set; }
        public virtual string BMFee { get; set; }
        public virtual string BMiniLeaseP { get; set; }
        public virtual string BCook_Y { get; set; }
        public virtual string BCook_N { get; set; }
        public virtual string BPet_Y { get; set; }
        public virtual string BPet_N { get; set; }
        public virtual string BGodT_Y { get; set; }
        public virtual string BGodT_N { get; set; }
        public virtual string BOpen { get; set; }
        public virtual string BKeyFM { get; set; }
        public virtual string BKeyFR { get; set; }
        public virtual string BkeyCount { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string RNName { get; set; }

        /// <summary>
        /// 身分證號/居留證號
        /// </summary>
        public virtual string RNID { get; set; }

        /// <summary>
        /// 性別_男
        /// </summary>
        public virtual string RNGender1 { get; set; }

        /// <summary>
        /// 性別_女
        /// </summary>
        public virtual string RNGender2 { get; set; }

        /// <summary>
        /// 生日_年/月/日
        /// </summary>
        public virtual string RNBirthday { get; set; }

        /// <summary>
        /// 生日_年
        /// </summary>
        public virtual string RNBirthday_Y { get; set; }

        /// <summary>
        /// 生日_月
        /// </summary>
        public virtual string RNBirthday_M { get; set; }

        /// <summary>
        /// 生日_日
        /// </summary>
        public virtual string RNBirthday_D { get; set; }

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
        /// 申請人戶口名簿戶號_第一碼
        /// </summary>
        public virtual string RNMAccount_1 { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第二碼
        /// </summary>
        public virtual string RNMAccount_2 { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第三碼
        /// </summary>
        public virtual string RNMAccount_3 { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第四碼
        /// </summary>
        public virtual string RNMAccount_4 { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第五碼
        /// </summary>
        public virtual string RNMAccount_5 { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第六碼
        /// </summary>
        public virtual string RNMAccount_6 { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第七碼
        /// </summary>
        public virtual string RNMAccount_7 { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第八碼
        /// </summary>
        public virtual string RNMAccount_8 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號
        /// </summary>
        public virtual string RNMMAccount { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第一碼
        /// </summary>
        public virtual string RNMMAccount_1 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第二碼
        /// </summary>
        public virtual string RNMMAccount_2 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第三碼
        /// </summary>
        public virtual string RNMMAccount_3 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第四碼
        /// </summary>
        public virtual string RNMMAccount_4 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第五碼
        /// </summary>
        public virtual string RNMMAccount_5 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第六碼
        /// </summary>
        public virtual string RNMMAccount_6 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第七碼
        /// </summary>
        public virtual string RNMMAccount_7 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第八碼
        /// </summary>
        public virtual string RNMMAccount_8 { get; set; }

        /// <summary>
        /// 子女人數
        /// </summary>
        public virtual string RNChild { get; set; }

        /// <summary>
        /// 家庭成員1_姓名
        /// </summary>
        public virtual string RNFName_A { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號
        /// </summary>
        public virtual string RNFID_1_A { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_A_1 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_A_2 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_A_3 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_A_4 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_A_5 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_A_6 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_A_7 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_A_8 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_A_9 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_A_10 { get; set; }

        /// <summary>
        /// 家庭成員1_生日
        /// </summary>
        public virtual string RNFBirthday_A { get; set; }

        /// <summary>
        /// 家庭成員1_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_A { get; set; }

        /// <summary>
        /// 家庭成員1_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_A { get; set; }

        /// <summary>
        /// 家庭成員1_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_A { get; set; }

        /// <summary>
        /// 家庭成員1_稱謂
        /// </summary>
        public virtual string RNFTitle_A { get; set; }

        /// <summary>
        /// 家庭成員1_年所得
        /// </summary>
        public virtual string RNFIncome_A { get; set; }

        /// <summary>
        /// 家庭成員2_姓名
        /// </summary>
        public virtual string RNFName_B { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號
        /// </summary>
        public virtual string RNFID_1_B { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_B_1 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_B_2 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_B_3 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_B_4 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_B_5 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_B_6 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_B_7 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_B_8 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_B_9 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_B_10 { get; set; }

        /// <summary>
        /// 家庭成員2_生日
        /// </summary>
        public virtual string RNFBirthday_B { get; set; }

        /// <summary>
        /// 家庭成員2_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_B { get; set; }

        /// <summary>
        /// 家庭成員2_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_B { get; set; }

        /// <summary>
        /// 家庭成員2_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_B { get; set; }

        /// <summary>
        /// 家庭成員2_稱謂
        /// </summary>
        public virtual string RNFTitle_B { get; set; }

        /// <summary>
        /// 家庭成員2_年所得
        /// </summary>
        public virtual string RNFIncome_B { get; set; }

        /// <summary>
        /// 家庭成員3_姓名
        /// </summary>
        public virtual string RNFName_C { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號
        /// </summary>
        public virtual string RNFID_1_C { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_C_1 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_C_2 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_C_3 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_C_4 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_C_5 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_C_6 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_C_7 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_C_8 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_C_9 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_C_10 { get; set; }

        /// <summary>
        /// 家庭成員3_生日
        /// </summary>
        public virtual string RNFBirthday_C { get; set; }

        /// <summary>
        /// 家庭成員3_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_C { get; set; }

        /// <summary>
        /// 家庭成員3_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_C { get; set; }

        /// <summary>
        /// 家庭成員3_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_C { get; set; }

        /// <summary>
        /// 家庭成員3_稱謂
        /// </summary>
        public virtual string RNFTitle_C { get; set; }

        /// <summary>
        /// 家庭成員3_年所得
        /// </summary>
        public virtual string RNFIncome_C { get; set; }

        /// <summary>
        /// 居家安全保險費(包租包管適用)
        /// </summary>
        public virtual string HSI { get; set; }

        /// <summary>
        /// 居家安全保險費(包租包管適用)_實際保險金額
        /// </summary>
        public virtual string HSIRFee { get; set; }

        /// <summary>
        /// 居家安全保險費(包租包管適用)_申請金額
        /// </summary>
        public virtual string HSIAFee { get; set; }

        /// <summary>
        /// 公證費
        /// </summary>
        public virtual string SFee { get; set; }

        /// <summary>
        /// 公證費_實際保險金額
        /// </summary>
        public virtual string SFRFee { get; set; }

        /// <summary>
        /// 公證費_申請金額
        /// </summary>
        public virtual string SFAFee { get; set; }

        /// <summary>
        /// 住宅出租修繕費(包租包管與代租代管街適用)
        /// </summary>
        public virtual string BRFFee { get; set; }

        /// <summary>
        /// 住宅出租修繕費(包租包管與代租代管街適用)_實際修繕金額
        /// </summary>
        public virtual string BRFRFee { get; set; }

        /// <summary>
        /// 住宅出租修繕費(包租包管與代租代管街適用)_申請金額
        /// </summary>
        /// </summary>
        public virtual string BRFAFee { get; set; }

        /// <summary>
        /// 包租包管
        /// </summary>
        public virtual string BCharter { get; set; }

        /// <summary>
        /// 代租代管
        /// </summary>
        public virtual string BChangeRent { get; set; }

        /// <summary>
        /// 服務事業轉租
        /// </summary>
        public virtual string BSecondLandlord { get; set; }

        /// <summary>
        /// 服務事業協助出租
        /// </summary>
        public virtual string BManagement { get; set; }

        /// <summary>
        /// 公司名稱(中文)
        /// </summary>
        public string RCName { get; set; }

        /// <summary>
        /// 公司名稱(英文)
        /// </summary>
        public string RCEngName { get; set; }

        /// <summary>
        /// 負責人
        /// </summary>
        public string RCRep { get; set; }

        /// <summary>
        /// 統一編號
        /// </summary>
        public string RCID { get; set; }

        /// <summary>
        /// 統一編號_1
        /// </summary>
        public string RCID_1_1 { get; set; }

        /// <summary>
        /// --統一編號_2
        /// </summary>
        public string RCID_1_2 { get; set; }

        /// <summary>
        /// 統一編號_3
        /// </summary>
        public string RCID_1_3 { get; set; }

        /// <summary>
        /// 統一編號_4
        /// </summary>
        public string RCID_1_4 { get; set; }

        /// <summary>
        /// -統一編號_5
        /// </summary>
        public string RCID_1_5 { get; set; }

        /// <summary>
        /// 統一編號_6
        /// </summary>
        public string RCID_1_6 { get; set; }

        /// <summary>
        /// 統一編號_7
        /// </summary>
        public string RCID_1_7 { get; set; }

        /// <summary>
        /// 統一編號_8
        /// </summary>
        public string RCID_1_8 { get; set; }

        /// <summary>
        /// 電話(完整)
        /// </summary>
        public string RCTel { get; set; }

        /// <summary>
        /// 電話(區號)
        /// </summary>
        public string RCTel_1 { get; set; }

        /// <summary>
        /// 電話(號碼)
        /// </summary>
        public string RCTel_2 { get; set; }

        /// <summary>
        /// 手機
        /// </summary>
        public string RCCell { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        public string RCMail { get; set; }

        /// <summary>
        /// 登記地址
        /// </summary>
        public string RCAdd { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_1_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_1_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_2_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_2_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_2_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_2_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_3_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_3_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_9 { get; set; }

        /// <summary>
        /// 營業地址同登記地址
        /// </summary>
        public string RCAddSame { get; set; }

        /// <summary>
        /// 通訊地址
        /// </summary>
        public string RCAddC { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_1_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_1_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_2_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_2_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_2_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_2_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_3_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_3_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_9 { get; set; }

        /// <summary>
        /// 稱謂 一定是本人
        /// </summary>
        public string RCFRelation { get; set; }

        /// <summary>
        /// 同住者A_姓名
        /// </summary>
        public string RCFName_A { get; set; }

        /// <summary>
        /// 同住者A_稱謂
        /// </summary>
        public string RCFRelation_A { get; set; }

        /// <summary>
        /// 同住者A_連絡電話
        /// </summary>
        public string RCFCell_A { get; set; }

        /// <summary>
        /// 同住者B_姓名
        /// </summary>
        public string RCFName_B { get; set; }

        /// <summary>
        /// 同住者B_稱謂
        /// </summary>
        public string RCFRelation_B { get; set; }

        /// <summary>
        /// 同住者B_連絡電話
        /// </summary>
        public string RCFCell_B { get; set; }

        /// <summary>
        /// 同住者C_姓名
        /// </summary>
        public string RCFName_C { get; set; }

        /// <summary>
        /// 同住者C_稱謂
        /// </summary>
        public string RCFRelation_C { get; set; }

        /// <summary>
        /// 同住者C_連絡電話
        /// </summary>
        public string RCFCell_C { get; set; }

        /// <summary>
        /// 同住者A_身分證字號
        /// </summary>
        public string RCFID_1_A { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_9 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_10 { get; set; }

        /// <summary>
        /// 同住者A_生日
        /// </summary>
        public string RCFBirthday_A { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_Y_A { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_M_A { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D_A { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string CreatorUserOrgId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string CreatorUserDeptId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string LastModifyUserId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string DeleteUserId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_9 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_10 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_B { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_Y_B { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_M_B { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D_B { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_9 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_10 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_C { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_Y_C { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_M_C { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D_C { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFName_D { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFRelation_D { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFCell_D { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_9 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_10 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_Y_D { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_M_D { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D_D { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFName_E { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFCell_E { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_9 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_10 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_E { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_Y_E { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_M_E { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D_E { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFName_F { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFRelation_F { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFCell_F { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_9 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_10 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_F { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_Y_F { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_M_F { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D_F { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFName_G { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFRelation_G { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFCell_G { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_9 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_10 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_G { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_Y_G { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_M_G { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D_G { get; set; }

        /// <summary>
        /// 緊急聯絡人(親屬)_姓名
        /// </summary>
        public string RCECName { get; set; }

        /// <summary>
        /// 緊急聯絡人(親屬)_身分證字號
        /// </summary>
        public string RCECID { get; set; }

        /// <summary>
        /// 緊急聯絡人(親屬)性別_男
        /// </summary>
        public string RCECGender_1 { get; set; }

        /// <summary>
        /// 緊急聯絡人(親屬)性別_女
        /// </summary>
        public string RCECGender_2 { get; set; }

        /// <summary>
        /// 緊急聯絡人(親屬)_生日
        /// </summary>
        public string RCECBirthday { get; set; }

        /// <summary>
        /// 緊急聯絡人(親屬)_關係
        /// </summary>
        public string RCECRelation { get; set; }

        /// <summary>
        /// 緊急聯絡人(親屬)_連絡電話(手機)
        /// </summary>
        public string RCECCell { get; set; }

        /// <summary>
        /// 緊急聯絡人(親屬)_連絡電話(區號)
        /// </summary>
        public string RCECTel_1 { get; set; }

        /// <summary>
        /// 緊急聯絡人(親屬)_連絡電話(號碼)
        /// </summary>
        public string RCECTel_2 { get; set; }

        /// <summary>
        /// 緊急聯絡人(親屬)_戶籍地址
        /// </summary>
        public string RCECAdd { get; set; }

        /// <summary>
        /// 緊急聯絡人(親屬)_通訊地址同戶籍地址
        /// </summary>
        public string RCECAddSame { get; set; }

        /// <summary>
        /// 緊急聯絡人(親屬)_通訊地址
        /// </summary>
        public string RCECAddC { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_1_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_1_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_2_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_2_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_2_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_2_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_3_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_3_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_9 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_1_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_1_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_2_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_2_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_2_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_2_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_3_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_3_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_9 { get; set; }

        /// <summary>
        /// 緊急聯絡人(朋友)_姓名
        /// </summary>
        public string RCECFName { get; set; }

        /// <summary>
        /// 緊急聯絡人(朋友)_身分證字號
        /// </summary>
        public string RCECFID { get; set; }

        /// <summary>
        /// 緊急聯絡人(朋友)_性別_男
        /// </summary>
        public string RCECFGender_1 { get; set; }

        /// <summary>
        /// 緊急聯絡人(朋友)_性別_女
        /// </summary>
        public string RCECFGender_2 { get; set; }

        /// <summary>
        /// 緊急聯絡人(朋友)_生日
        /// </summary>
        public string RCECFBirthday { get; set; }

        /// <summary>
        /// 緊急聯絡人(朋友)_連絡電話
        /// </summary>
        public string RCECFCell { get; set; }

        /// <summary>
        /// 緊急聯絡人(朋友)_關係
        /// </summary>
        public string RCECFRelation { get; set; }

        /// <summary>
        /// 緊急聯絡人(朋友)_戶籍地址
        /// </summary>
        public string RCECFAdd { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_1_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_1_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_2_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_2_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_2_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_2_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_3_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_3_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_9 { get; set; }

        /// <summary>
        /// 緊急聯絡人(朋友)_通訊地址同戶籍地址
        /// </summary>
        public string RCECFAddSame { get; set; }

        /// <summary>
        /// 緊急聯絡人(朋友)_通訊地址
        /// </summary>
        public string RCECFAddC { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_1_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_1_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_2_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_2_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_2_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_2_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_3_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_3_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_9 { get; set; }

        /// <summary>
        /// 簽約代理人_姓名
        /// </summary>
        public string RCAGName { get; set; }

        /// <summary>
        /// 簽約代理人_身分證字號
        /// </summary>
        public string RCAGID { get; set; }

        /// <summary>
        /// 簽約代理人性別_男
        /// </summary>
        public string RCAGGender_1 { get; set; }

        /// <summary>
        /// 簽約代理人性別_女
        /// </summary>
        public string RCAGGender_2 { get; set; }

        /// <summary>
        /// 簽約代理人_生日
        /// </summary>
        public string RCAGBirthday { get; set; }

        /// <summary>
        /// 簽約代理人_戶籍地址
        /// </summary>
        public string RCAGAdd { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_1_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_1_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_2_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_2_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_2_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_2_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_3_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_3_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_9 { get; set; }

        /// <summary>
        /// 簽約代理人_通訊地址同戶籍地址
        /// </summary>
        public string RCAGAddSame { get; set; }

        /// <summary>
        /// 簽約代理人_通訊地址
        /// </summary>
        public string RCAGAddC { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_1_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_1_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_2_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_2_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_2_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_2_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_3_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_3_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_9 { get; set; }

        /// <summary>
        /// 簽約代理人_聯絡電話(手機)
        /// </summary>
        public string RCAGCell { get; set; }

        /// <summary>
        /// --簽約代理人_聯絡電話(區號)
        /// </summary>
        public string RCAGTel_1 { get; set; }

        /// <summary>
        /// -簽約代理人_聯絡電話(號碼)
        /// </summary>
        public string RCAGTel_2 { get; set; }

        /// <summary>
        /// 保證人_姓名
        /// </summary>
        public string RCGName { get; set; }

        /// <summary>
        /// 保證人_身分證字號
        /// </summary>
        public string RCGID { get; set; }

        /// <summary>
        /// 保證人性別_男
        /// </summary>
        public string RCGGender_1 { get; set; }

        /// <summary>
        /// 保證人性別_女
        /// </summary>
        public string RCGGender_2 { get; set; }

        /// <summary>
        /// 保證人_生日
        /// </summary>
        public string RCGBirthday { get; set; }

        /// <summary>
        /// 保證人_戶籍地址
        /// </summary>
        public string RCGAdd { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_1_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_1_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_2_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_2_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_2_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_2_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_3_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_3_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_9 { get; set; }

        /// <summary>
        /// 保證人_通訊地址同戶籍地址
        /// </summary>
        public string RCGAddSame { get; set; }

        /// <summary>
        /// 保證人_通訊地址
        /// </summary>
        public string RCGCAdd { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_1_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_1_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_2_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_2_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_2_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_2_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_3 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_3_1 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_3_2 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_4 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_5 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_6 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_7 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_9 { get; set; }

        /// <summary>
        /// 保證人_聯絡電話
        /// </summary>
        public string RCGCell { get; set; }

        /// <summary>
        /// 保證人_關係
        /// </summary>
        public string RCGRelation { get; set; }


        #endregion FirstData

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

        /// <summary>
        /// BZ104
        /// </summary>
        public virtual string BZ104 { get; set; }

        /// <summary>
        /// BZ105
        /// </summary>
        public virtual string BZ105 { get; set; }

        /// <summary>
        /// BZ106
        /// </summary>
        public virtual string BZ106 { get; set; }

        /// <summary>
        /// BZ107
        /// </summary>
        public virtual string BZ107 { get; set; }

        /// <summary>
        /// BZ108
        /// </summary>
        public virtual string BZ108 { get; set; }

        /// <summary>
        /// BZ109
        /// </summary>
        public virtual string BZ109 { get; set; }

        /// <summary>
        /// BZ110
        /// </summary>
        public virtual string BZ110 { get; set; }

        /// <summary>
        /// BZ111
        /// </summary>
        public virtual string BZ111 { get; set; }

        /// <summary>
        /// BZ112
        /// </summary>
        public virtual string BZ112 { get; set; }

        /// <summary>
        /// BZ113
        /// </summary>
        public virtual string BZ113 { get; set; }

        /// <summary>
        /// BZ114
        /// </summary>
        public virtual string BZ114 { get; set; }

        /// <summary>
        /// BZ115
        /// </summary>
        public virtual string BZ115 { get; set; }

        /// <summary>
        /// BZ116
        /// </summary>
        public virtual string BZ116 { get; set; }

        /// <summary>
        /// BZ117
        /// </summary>
        public virtual string BZ117 { get; set; }

        /// <summary>
        /// BZ118
        /// </summary>
        public virtual string BZ118 { get; set; }

        /// <summary>
        /// BZ119
        /// </summary>
        public virtual string BZ119 { get; set; }

        /// <summary>
        /// BZ120
        /// </summary>
        public virtual string BZ120 { get; set; }

        /// <summary>
        /// BZ121
        /// </summary>
        public virtual string BZ121 { get; set; }

        /// <summary>
        /// BZ122
        /// </summary>
        public virtual string BZ122 { get; set; }

        /// <summary>
        /// BZ123
        /// </summary>
        public virtual string BZ123 { get; set; }

        /// <summary>
        /// BZ124
        /// </summary>
        public virtual string BZ124 { get; set; }

        /// <summary>
        /// BZ125
        /// </summary>
        public virtual string BZ125 { get; set; }

        /// <summary>
        /// BZ126
        /// </summary>
        public virtual string BZ126 { get; set; }

        /// <summary>
        /// BZ127
        /// </summary>
        public virtual string BZ127 { get; set; }

        /// <summary>
        /// BZ128
        /// </summary>
        public virtual string BZ128 { get; set; }

        /// <summary>
        /// BZ129
        /// </summary>
        public virtual string BZ129 { get; set; }

        /// <summary>
        /// BZ130
        /// </summary>
        public virtual string BZ130 { get; set; }

        /// <summary>
        /// BZ131
        /// </summary>
        public virtual string BZ131 { get; set; }

        /// <summary>
        /// BZ132
        /// </summary>
        public virtual string BZ132 { get; set; }

        /// <summary>
        /// BZ133
        /// </summary>
        public virtual string BZ133 { get; set; }

        /// <summary>
        /// BZ134
        /// </summary>
        public virtual string BZ134 { get; set; }

        /// <summary>
        /// BZ135
        /// </summary>
        public virtual string BZ135 { get; set; }

        /// <summary>
        /// BZ136
        /// </summary>
        public virtual string BZ136 { get; set; }

        /// <summary>
        /// BZ137
        /// </summary>
        public virtual string BZ137 { get; set; }

        /// <summary>
        /// BZ138
        /// </summary>
        public virtual string BZ138 { get; set; }

        /// <summary>
        /// BZ139
        /// </summary>
        public virtual string BZ139 { get; set; }

        /// <summary>
        /// BZ140
        /// </summary>
        public virtual string BZ140 { get; set; }

        /// <summary>
        /// BZ141
        /// </summary>
        public virtual string BZ141 { get; set; }

        /// <summary>
        /// BZ142
        /// </summary>
        public virtual string BZ142 { get; set; }

        /// <summary>
        /// BZ143
        /// </summary>
        public virtual string BZ143 { get; set; }

        /// <summary>
        /// BZ144
        /// </summary>
        public virtual string BZ144 { get; set; }

        /// <summary>
        /// BZ145
        /// </summary>
        public virtual string BZ145 { get; set; }

        /// <summary>
        /// BZ146
        /// </summary>
        public virtual string BZ146 { get; set; }

        /// <summary>
        /// BZ147
        /// </summary>
        public virtual string BZ147 { get; set; }

        /// <summary>
        /// BZ148
        /// </summary>
        public virtual string BZ148 { get; set; }

        /// <summary>
        /// BZ149
        /// </summary>
        public virtual string BZ149 { get; set; }

        /// <summary>
        /// BZ150
        /// </summary>
        public virtual string BZ150 { get; set; }

        /// <summary>
        /// BZ151
        /// </summary>
        public virtual string BZ151 { get; set; }

        /// <summary>
        /// BZ152
        /// </summary>
        public virtual string BZ152 { get; set; }

        /// <summary>
        /// BZ153
        /// </summary>
        public virtual string BZ153 { get; set; }

        /// <summary>
        /// BZ154
        /// </summary>
        public virtual string BZ154 { get; set; }

        /// <summary>
        /// BZ155
        /// </summary>
        public virtual string BZ155 { get; set; }

        /// <summary>
        /// BZ156
        /// </summary>
        public virtual string BZ156 { get; set; }

        /// <summary>
        /// BZ157
        /// </summary>
        public virtual string BZ157 { get; set; }

        /// <summary>
        /// BZ158
        /// </summary>
        public virtual string BZ158 { get; set; }

        /// <summary>
        /// BZ159
        /// </summary>
        public virtual string BZ159 { get; set; }

        /// <summary>
        /// BZ160
        /// </summary>
        public virtual string BZ160 { get; set; }

        /// <summary>
        /// BZ161
        /// </summary>
        public virtual string BZ161 { get; set; }

        /// <summary>
        /// BZ162
        /// </summary>
        public virtual string BZ162 { get; set; }

        /// <summary>
        /// BZ163
        /// </summary>
        public virtual string BZ163 { get; set; }

        /// <summary>
        /// BZ164
        /// </summary>
        public virtual string BZ164 { get; set; }

        /// <summary>
        /// BZ165
        /// </summary>
        public virtual string BZ165 { get; set; }

        /// <summary>
        /// BZ166
        /// </summary>
        public virtual string BZ166 { get; set; }

        /// <summary>
        /// BZ167
        /// </summary>
        public virtual string BZ167 { get; set; }

        /// <summary>
        /// BZ168
        /// </summary>
        public virtual string BZ168 { get; set; }

        /// <summary>
        /// BZ169
        /// </summary>
        public virtual string BZ169 { get; set; }

        /// <summary>
        /// BZ170
        /// </summary>
        public virtual string BZ170 { get; set; }

        /// <summary>
        /// BZ171
        /// </summary>
        public virtual string BZ171 { get; set; }

        /// <summary>
        /// BZ172
        /// </summary>
        public virtual string BZ172 { get; set; }

        /// <summary>
        /// BZ173
        /// </summary>
        public virtual string BZ173 { get; set; }

        /// <summary>
        /// BZ174
        /// </summary>
        public virtual string BZ174 { get; set; }

        /// <summary>
        /// BZ175
        /// </summary>
        public virtual string BZ175 { get; set; }

        /// <summary>
        /// BZ176
        /// </summary>
        public virtual string BZ176 { get; set; }

        /// <summary>
        /// BZ177
        /// </summary>
        public virtual string BZ177 { get; set; }

        /// <summary>
        /// BZ178
        /// </summary>
        public virtual string BZ178 { get; set; }

        /// <summary>
        /// BZ179
        /// </summary>
        public virtual string BZ179 { get; set; }

        /// <summary>
        /// BZ180
        /// </summary>
        public virtual string BZ180 { get; set; }

        /// <summary>
        /// BZ181
        /// </summary>
        public virtual string BZ181 { get; set; }

        /// <summary>
        /// BZ182
        /// </summary>
        public virtual string BZ182 { get; set; }

        /// <summary>
        /// BZ183
        /// </summary>
        public virtual string BZ183 { get; set; }

        /// <summary>
        /// BZ184
        /// </summary>
        public virtual string BZ184 { get; set; }

        /// <summary>
        /// BZ185
        /// </summary>
        public virtual string BZ185 { get; set; }

        /// <summary>
        /// BZ186
        /// </summary>
        public virtual string BZ186 { get; set; }

        /// <summary>
        /// BZ187
        /// </summary>
        public virtual string BZ187 { get; set; }

        /// <summary>
        /// BZ188
        /// </summary>
        public virtual string BZ188 { get; set; }

        /// <summary>
        /// BZ189
        /// </summary>
        public virtual string BZ189 { get; set; }

        /// <summary>
        /// BZ190
        /// </summary>
        public virtual string BZ190 { get; set; }

        /// <summary>
        /// BZ191
        /// </summary>
        public virtual string BZ191 { get; set; }

        /// <summary>
        /// BZ192
        /// </summary>
        public virtual string BZ192 { get; set; }

        /// <summary>
        /// BZ193
        /// </summary>
        public virtual string BZ193 { get; set; }

        /// <summary>
        /// BZ194
        /// </summary>
        public virtual string BZ194 { get; set; }

        /// <summary>
        /// BZ195
        /// </summary>
        public virtual string BZ195 { get; set; }

        /// <summary>
        /// BZ196
        /// </summary>
        public virtual string BZ196 { get; set; }

        /// <summary>
        /// BZ197
        /// </summary>
        public virtual string BZ197 { get; set; }

        /// <summary>
        /// BZ198
        /// </summary>
        public virtual string BZ198 { get; set; }

        /// <summary>
        /// BZ199
        /// </summary>
        public virtual string BZ199 { get; set; }

        /// <summary>
        /// BZ200
        /// </summary>
        public virtual string BZ200 { get; set; }

        /// <summary>
        /// BZ201
        /// </summary>
        public virtual string BZ201 { get; set; }

        /// <summary>
        /// BZ202
        /// </summary>
        public virtual string BZ202 { get; set; }

        /// <summary>
        /// BZ203
        /// </summary>
        public virtual string BZ203 { get; set; }

        /// <summary>
        /// BZ204
        /// </summary>
        public virtual string BZ204 { get; set; }

        /// <summary>
        /// BZ205
        /// </summary>
        public virtual string BZ205 { get; set; }

        /// <summary>
        /// BZ206
        /// </summary>
        public virtual string BZ206 { get; set; }

        /// <summary>
        /// BZ207
        /// </summary>
        public virtual string BZ207 { get; set; }

        /// <summary>
        /// BZ208
        /// </summary>
        public virtual string BZ208 { get; set; }

        /// <summary>
        /// BZ209
        /// </summary>
        public virtual string BZ209 { get; set; }

        /// <summary>
        /// BZ210
        /// </summary>
        public virtual string BZ210 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ211 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ212 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ213 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ214 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ215 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ216 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ217 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ218 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ219 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ220 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ221 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ222 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ223 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ224 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ225 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ226 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ227 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ228 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ229 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ230 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ231 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ232 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ233 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ234 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ235 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ236 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ237 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ238 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ239 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ240 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ241 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ242 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ243 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ244 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ245 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ246 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ247 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ248 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ249 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ250 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ251 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ252 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ253 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ254 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ255 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ256 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ257 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ258 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ259 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ260 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ261 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ262 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ263 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ264 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ265 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ266 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ267 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ268 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ269 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ270 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ271 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ272 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ273 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ274 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ275 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ276 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ277 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ278 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ279 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ280 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ281 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ282 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ283 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ284 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ285 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ286 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ287 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ288 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ289 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ290 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ291 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ292 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ293 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ294 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ295 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ296 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ297 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ298 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ299 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ300 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ301 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ302 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ303 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ304 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ305 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ306 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ307 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ308 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ309 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ310 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ311 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ312 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ313 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ314 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ315 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ316 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ317 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ318 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ319 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ320 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ321 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ322 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ323 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ324 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ325 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ326 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ327 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ328 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ329 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ330 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ331 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ332 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ333 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ334 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ335 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ336 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ337 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ338 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ339 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ340 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ341 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ342 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ343 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ344 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ345 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ346 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ347 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ348 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ349 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ350 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ351 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ352 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ353 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ354 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ355 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ356 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ357 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ358 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ359 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ360 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ361 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ362 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ363 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ364 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ365 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ366 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ367 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ368 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ369 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ370 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ371 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ372 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ373 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ374 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ375 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ376 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ377 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ378 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ379 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ380 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ381 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ382 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ383 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ384 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ385 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ386 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ387 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ388 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ389 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ390 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ391 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ392 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ393 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ394 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ395 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ396 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ397 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ398 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ399 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ400 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ401 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ402 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ403 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ404 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ405 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ406 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ407 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ408 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ409 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ410 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ411 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ412 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ413 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ414 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ415 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ416 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ417 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ418 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ419 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ420 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ421 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ422 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ423 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ424 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ425 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ426 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ427 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ428 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ429 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ430 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ431 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ432 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ433 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ434 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ435 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ436 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ437 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ438 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ439 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ440 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ441 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ442 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ443 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ444 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ445 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ446 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ447 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ448 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ449 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ450 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ451 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ452 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ453 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ454 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ455 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ456 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ457 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ458 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ459 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ460 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ461 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ462 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ463 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ464 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ465 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ466 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ467 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ468 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ469 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ470 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ471 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ472 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ473 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ474 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ475 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ476 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ477 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ478 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ479 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ480 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ481 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ482 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ483 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ484 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ485 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ486 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ487 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ488 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ489 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ490 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ491 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ492 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ493 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ494 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ495 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ496 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ497 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ498 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ499 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ500 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ501 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ502 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ503 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ504 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ505 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ506 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ507 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ508 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ509 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ510 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ511 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ512 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ513 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ514 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ515 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ516 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ517 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ518 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ519 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ520 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ521 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ522 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ523 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ524 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ525 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ526 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ527 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ528 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ529 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ530 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ531 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ532 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ533 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ534 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ535 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ536 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ537 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ538 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ539 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ540 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ541 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ542 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ543 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ544 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ545 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ546 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ547 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ548 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ549 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BZ550 { get; set; }

        public virtual string SignatureImgPath_1 { get; set; }
        public virtual string SignatureImgPath_2 { get; set; }
        public virtual string SignatureImgPath_3 { get; set; }
        public virtual string SignatureImgPath_4 { get; set; }
        public virtual string SignatureImgPath_5 { get; set; }

        #endregion BZ欄位

        #region 領收據
        /// 領收據日期_年
        /// </summary>
        public string RDate_Y { get; set; }

        /// <summary>
        /// 領收據日期_月
        /// </summary>
        public string RDate_M { get; set; }

        /// <summary>
        /// 領收據日期_日
        /// </summary>
        public string RDate_D { get; set; }

        /// <summary>
        /// 設置或獲取收款方
        /// </summary>
        public string RPayee { get; set; }

        /// <summary>
        /// 設置或獲取收款方名字/名稱
        /// </summary>
        public string RPayeeName { get; set; }

        /// <summary>
        /// 設置或獲取兆基收款單位
        /// </summary>
        public string RPayeeUnit { get; set; }

        /// <summary>
        /// 設置或獲取收款方身份證字號
        /// </summary>
        public string RPayeeID { get; set; }

        /// <summary>
        /// 設置或獲取收款方統一編號
        /// </summary>
        public string RPayeeTaxID { get; set; }

        /// <summary>
        /// 設置或獲取收款方電話
        /// </summary>
        public string RPayeeTel { get; set; }

        /// <summary>
        /// 付款方名字/名稱
        /// </summary>
        public string RPayerName { get; set; }

        /// <summary>
        /// 付款方身份證字號
        /// </summary>
        public string RPayerID { get; set; }

        /// <summary>
        /// 設置或獲取兆基付款單位
        /// </summary>
        public string RPayerUnit { get; set; }

        /// <summary>
        /// 付款方統一編號
        /// </summary>
        public string RPayerTaxID { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址
        /// </summary>
        public string RAdd { get; set; }

        /// <summary>
        /// 收費項目-租金起日_年
        /// </summary>
        public string PIRentStartDate_Y { get; set; }

        /// <summary>
        /// 收費項目-租金起日_月
        /// </summary>
        public string PIRentStartDate_M { get; set; }

        /// <summary>
        /// 收費項目-租金起日_日
        /// </summary>
        public string PIRentStartDate_D { get; set; }

        /// <summary>
        /// 收費項目-租金迄日_年
        /// </summary>
        public string PIRentEndDate_Y { get; set; }

        /// <summary>
        /// 收費項目-租金迄日_月
        /// </summary>
        public string PIRentEndDate_M { get; set; }

        /// <summary>
        /// 收費項目-租金迄日_日
        /// </summary>
        public string PIRentEndDate_D { get; set; }

        /// <summary>
        /// 收費項目-租金
        /// </summary>
        public string PIRent { get; set; }

        /// <summary>
        /// 收費項目-租金月數
        /// </summary>
        public string PIRentMonth { get; set; }

        /// <summary>
        /// 領收據備註
        /// </summary>
        public string RMemo { get; set; }

        /// <summary>
        /// 領收據編號
        /// </summary>
        public string ReceiptId { get; set; }

        /// <summary>
        /// 收費項目總金額
        /// </summary>
        public string RTotalCost { get; set; }

        public string PIDetailCost { get; set; }

        public string PIDetail { get; set; }

        /// <summary>
        /// 設置或獲取收費方式
        /// </summary>
        public string RPaymentMethod { get; set; }
        #endregion 領收據

        #region 合約
        /// <summary>
        /// 設置或獲取合約編號
        /// </summary>
        public string CID { get; set; }

        /// <summary>
        /// 設置或獲取合約編號
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 設置或獲取合約種類
        /// </summary>
        public string CType { get; set; }

        /// <summary>
        /// 設置或獲取簽約日期
        /// </summary>
        public string ContractDate { get; set; }

        /// <summary>
        /// 簽約日期_年
        /// </summary>
        public string Contract_Y { get; set; }

        /// <summary>
        /// 簽約日期_月
        /// </summary>
        public string Contract_M { get; set; }

        /// <summary>
        /// 簽約日期_日
        /// </summary>
        public string Contract_D { get; set; }

        /// <summary>
        /// 設置或獲取社宅物件編號
        /// </summary>
        public string ObjectNo { get; set; }

        /// <summary>
        /// 設置或獲取兆基物件編號
        /// </summary>
        public string CCObjectNo { get; set; }

        /// <summary>
        /// 媒合編號
        /// </summary>
        public string MID { get; set; }

        /// <summary>
        /// 設置或獲取出租人-姓名/法人名稱
        /// </summary>
        public string LSName { get; set; }

        /// <summary>
        /// 設置或獲取出租人-負責人(法人)
        /// </summary>
        public string LSRep { get; set; }

        /// <summary>
        /// 設置或獲取出租人-完整通訊地址
        /// </summary>
        public string LSAddC { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_縣/市
        /// </summary>
        public string LSAddC_1 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_縣
        /// </summary>
        public string LSAddC_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_市
        /// </summary>
        public string LSAddC_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_鄉/鎮/市/區
        /// </summary>
        public string LSAddC_2 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_鄉
        /// </summary>
        public string LSAddC_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_鎮
        /// </summary>
        public string LSAddC_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_市
        /// </summary>
        public string LSAddC_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_區
        /// </summary>
        public string LSAddC_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_街/路
        /// </summary>
        public string LSAddC_3 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_街
        /// </summary>
        public string LSAddC_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_路
        /// </summary>
        public string LSAddC_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_段
        /// </summary>
        public string LSAddC_4 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_巷
        /// </summary>
        public string LSAddC_5 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_弄
        /// </summary>
        public string LSAddC_6 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_號
        /// </summary>
        public string LSAddC_7 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_樓
        /// </summary>
        public string LSAddC_8 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_樓
        /// </summary>
        public string LSAddC_9 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-聯絡電話
        /// </summary>
        public string LSTel { get; set; }

        /// <summary>
        /// 設置或獲取出租人-聯絡電話_區碼
        /// </summary>
        public string LSTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-聯絡電話_號碼
        /// </summary>
        public string LSTel_2 { get; set; }

        ///// <summary>
        ///// 設置或獲取建物坐落地號-地段
        ///// </summary>
        //public string BPNo_1 { get; set; }

        ///// <summary>
        ///// 設置或獲取建物坐落地號-小段
        ///// </summary>
        //public string BPNo_2 { get; set; }

        ///// <summary>
        ///// 設置或獲取建物坐落地號-地號
        ///// </summary>
        //public string BPNo_3 { get; set; }

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

        /// <summary>
        /// 建物坐落地號(地段)C
        /// </summary>
        public virtual string BPNo_1_C { get; set; }

        /// <summary>
        /// 建物坐落地號(小段)C
        /// </summary>
        public virtual string BPNo_2_C { get; set; }

        /// <summary>
        /// 建物坐落地號(地號)C
        /// </summary>
        public virtual string BPNo_3_C { get; set; }

        /// <summary>
        /// 設置或獲取有車位
        /// </summary>
        public string BCar_Y { get; set; }

        /// <summary>
        /// 設置或獲取無車位
        /// </summary>
        public string BCar_N { get; set; }

        /// <summary>
        /// 設置或獲取租金繳款日
        /// </summary>
        public string BTRent_Day { get; set; }

        /// <summary>
        /// 設置或獲取租賃起日_完整日期
        /// </summary>
        public string BRDStart { get; set; }

        /// <summary>
        /// 設置或獲取租賃起日_年
        /// </summary>
        public string BRDStart_Y { get; set; }

        /// <summary>
        /// 設置或獲取租賃起日_月
        /// </summary>
        public string BRDStart_M { get; set; }

        /// <summary>
        /// 設置或獲取租賃起日_日
        /// </summary>
        public string BRDStart_D { get; set; }

        /// <summary>
        /// 設置或獲取租賃迄日_完整日期
        /// </summary>
        public string BRDTEnd { get; set; }

        /// <summary>
        /// 設置或獲取租賃迄日_年
        /// </summary>
        public string BRDTEnd_Y { get; set; }

        /// <summary>
        /// 設置或獲取租賃迄日_月
        /// </summary>
        public string BRDTEnd_M { get; set; }

        /// <summary>
        /// 設置或獲取租賃迄日_日
        /// </summary>
        public string BRDTEnd_D { get; set; }

        /// <summary>
        /// 設置或獲取押金月數
        /// </summary>
        public string BDMon { get; set; }

        /// <summary>
        /// 設置或獲取二房東-公司名稱
        /// </summary>
        public string SLName { get; set; }

        /// <summary>
        /// 設置或獲取二房東-負責人姓名
        /// </summary>
        public string SLRep { get; set; }

        /// <summary>
        /// 設置或獲取二房東-許可字號/登記證字號
        /// </summary>
        public string SLLRNo { get; set; }

        /// <summary>
        /// 設置或獲取二房東-營業地址
        /// </summary>
        public string SLAdd { get; set; }

        /// <summary>
        /// 設置或獲取二房東-聯絡電話
        /// </summary>
        public string SLTel { get; set; }

        /// <summary>
        /// 設置或獲取二房東-聯絡電話_區碼
        /// </summary>
        public string SLTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取二房東-聯絡電話_號碼
        /// </summary>
        public string SLTel_2 { get; set; }

        /// <summary>
        /// 設置或獲取管理方-統一編號
        /// </summary>
        public string MAID { get; set; }

        /// <summary>
        /// 設置或獲取管理方-公司名稱
        /// </summary>
        public string MName { get; set; }

        /// <summary>
        /// 設置或獲取管理方-負責人姓名
        /// </summary>
        public string MRep { get; set; }

        /// <summary>
        /// 設置或獲取管理方-許可字號/登記證字號
        /// </summary>
        public string MLRNo { get; set; }

        /// <summary>
        /// 設置或獲取管理方-營業地址
        /// </summary>
        public string MAdd { get; set; }

        /// <summary>
        /// 設置或獲取管理方-聯絡電話
        /// </summary>
        public string MTel { get; set; }

        /// <summary>
        /// 設置或獲取管理方-聯絡電話_區碼
        /// </summary>
        public string MTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取管理方-聯絡電話_號碼
        /// </summary>
        public string MTel_2 { get; set; }

        /// <summary>
        /// 設置或獲取管理方-電子郵件信箱
        /// </summary>
        public string MEmail { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員
        /// </summary>
        public string SIName { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員-證書字號
        /// </summary>
        public string SILRNo { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員-通訊地址
        /// </summary>
        public string SIAdd { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員-聯絡電話
        /// </summary>
        public string SITel { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員-聯絡電話_區碼
        /// </summary>
        public string SITel_1 { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員-聯絡電話_號碼
        /// </summary>
        public string SITel_2 { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員-電子郵件信箱
        /// </summary>
        public string SIEmail { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人-統一編號(身分證明文件編號)
        /// </summary>
        public string AGID { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人
        /// </summary>
        public string AGName { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人-證書字號
        /// </summary>
        public string AGLRNo { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人-通訊地址
        /// </summary>
        public string AGAdd { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人-聯絡電話
        /// </summary>
        public string AGTel { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人-聯絡電話_區碼
        /// </summary>
        public string AGTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人-聯絡電話_號碼
        /// </summary>
        public string AGTel_2 { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人-電子郵件信箱
        /// </summary>
        public string AGEmail { get; set; }

        public string LSID { get; set; }


        #endregion

        #region 建物設備相關

        /// <summary>
        /// 點交日期_年
        /// </summary>
        public virtual string HandOver_Y { get; set; }

        /// <summary>
        /// 點交日期_月
        /// </summary>
        public virtual string HandOver_M { get; set; }

        /// <summary>
        /// 點交日期_日
        /// </summary>
        public virtual string HandOver_D { get; set; }

        /// <summary>
        /// 分離式冷氣
        /// </summary>
        public virtual string SAConditioner { get; set; }

        /// <summary>
        /// 分離式冷氣_數量
        /// </summary>
        public virtual string SAConditionerNo { get; set; }

        /// <summary>
        /// 分離式冷氣_品牌
        /// </summary>
        public virtual string SAConditionerBrand { get; set; }

        /// <summary>
        /// 窗型冷氣
        /// </summary>
        public virtual string WAConditioner { get; set; }

        /// <summary>
        /// 窗型冷氣_數量
        /// </summary>
        public virtual string WAConditionerNo { get; set; }

        /// <summary>
        /// 窗型冷氣_品牌
        /// </summary>
        public virtual string WAConditionerBrand { get; set; }

        /// <summary>
        /// 電視機
        /// </summary>
        public virtual string TVset { get; set; }

        /// <summary>
        /// 電視機_數量
        /// </summary>
        public virtual string TVsetNo { get; set; }

        /// <summary>
        /// 電視機_品牌
        /// </summary>
        public virtual string TVsetBrand { get; set; }

        /// <summary>
        /// 電冰箱
        /// </summary>
        public virtual string Refrigerator { get; set; }

        /// <summary>
        /// 電冰箱_數量
        /// </summary>
        public virtual string RefrigeratorNo { get; set; }

        /// <summary>
        /// 電冰箱_品牌
        /// </summary>
        public virtual string RefrigeratorBrand { get; set; }

        /// <summary>
        /// 瓦斯爐
        /// </summary>
        public virtual string GasStove { get; set; }

        /// <summary>
        /// 瓦斯爐_數量
        /// </summary>
        public virtual string GasStoveNo { get; set; }

        /// <summary>
        /// 瓦斯爐_品牌
        /// </summary>
        public virtual string GasStoveBrand { get; set; }

        /// <summary>
        /// 抽油煙機
        /// </summary>
        public virtual string RangeHood { get; set; }

        /// <summary>
        /// 抽油煙機_數量
        /// </summary>
        public virtual string RangeHoodNo { get; set; }

        /// <summary>
        /// 抽油煙機_品牌
        /// </summary>
        public virtual string RangeHoodBrand { get; set; }

        /// <summary>
        /// 電_熱水器
        /// </summary>
        public virtual string EWaterHeater { get; set; }

        /// <summary>
        /// 瓦斯_熱水器
        /// </summary>
        public virtual string GWaterHeater { get; set; }

        /// <summary>
        /// 電/瓦斯 熱水器_數量
        /// </summary>
        public virtual string WaterHeaterNo { get; set; }

        /// <summary>
        /// 電/瓦斯熱水器_品牌
        /// </summary>
        public virtual string WaterHeaterBrand { get; set; }

        /// <summary>
        /// 洗衣機
        /// </summary>
        public virtual string WashingMachine { get; set; }

        /// <summary>
        /// 洗衣機_數量
        /// </summary>
        public virtual string WashingMachineNo { get; set; }

        /// <summary>
        /// 洗衣機_品牌
        /// </summary>
        public virtual string WashingMachineBrand { get; set; }

        /// <summary>
        /// 一般馬桶
        /// </summary>
        public virtual string Toilet { get; set; }
        public virtual string ToiletNo { get; set; }

        /// <summary>
        /// 免治馬桶
        /// </summary>
        public virtual string Washlet { get; set; }
        public virtual string WashletNo { get; set; }

        /// <summary>
        /// 抽風機
        /// </summary>
        public virtual string ExhaustFan { get; set; }
        public virtual string ExhaustFanNo { get; set; }

        /// <summary>
        /// 暖風機
        /// </summary>
        public virtual string Heater { get; set; }
        public virtual string HeaterNo { get; set; }

        /// <summary>
        /// 水龍頭
        /// </summary>
        public virtual string Faucet { get; set; }
        public virtual string FaucetNo { get; set; }

        /// <summary>
        /// 洗臉盆
        /// </summary>
        public virtual string WashBasin { get; set; }
        public virtual string WashBasinNo { get; set; }

        /// <summary>
        /// 浴鏡組
        /// </summary>
        public virtual string BathMirror { get; set; }
        public virtual string BathMirrorNo { get; set; }

        /// <summary>
        /// 浴櫃
        /// </summary>
        public virtual string BathCabinet { get; set; }
        public virtual string BathCabinetNo { get; set; }

        /// <summary>
        /// 淋浴龍頭
        /// </summary>
        public virtual string Shower { get; set; }
        public virtual string ShowerNo { get; set; }

        /// <summary>
        /// 蓮蓬頭
        /// </summary>
        public virtual string ShowerHead { get; set; }
        public virtual string ShowerHeadNo { get; set; }

        /// <summary>
        /// 浴缸
        /// </summary>
        public virtual string Tub { get; set; }
        public virtual string TubNo { get; set; }

        /// <summary>
        /// 淋浴拉門
        /// </summary>
        public virtual string SlidingDoor { get; set; }
        public virtual string SlidingDoorNo { get; set; }

        /// <summary>
        /// 毛巾架
        /// </summary>
        public virtual string TowelRack { get; set; }
        public virtual string TowelRackNo { get; set; }

        /// <summary>
        /// 流理臺
        /// </summary>
        public virtual string FlowTable { get; set; }
        public virtual string FlowTableNo { get; set; }

        /// <summary>
        /// 水龍頭
        /// </summary>
        public virtual string KFaucet { get; set; }
        public virtual string KFaucetNo { get; set; }

        /// <summary>
        /// 電器櫃
        /// </summary>
        public virtual string ElectCabinet { get; set; }
        public virtual string ElectCabinetNo { get; set; }

        /// <summary>
        /// 廚櫃
        /// </summary>
        public virtual string KitchenCabinet { get; set; }
        public virtual string KitchenCabinetNo { get; set; }

        /// <summary>
        /// 電磁爐
        /// </summary>
        public virtual string InductionCooker { get; set; }
        public virtual string InductionCookerNo { get; set; }

        /// <summary>
        /// 微波爐
        /// </summary>
        public virtual string MicrowaveOven { get; set; }
        public virtual string MicrowaveOvenNo { get; set; }

        /// <summary>
        /// 烤箱
        /// </summary>
        public virtual string MicroOven { get; set; }
        public virtual string MicroOvenNo { get; set; }

        /// <summary>
        /// 電鍋
        /// </summary>
        public virtual string ElectricPot { get; set; }
        public virtual string ElectricPotNo { get; set; }

        /// <summary>
        /// 烘碗機
        /// </summary>
        public virtual string DishDryer { get; set; }
        public virtual string DishDryerNo { get; set; }

        /// <summary>
        /// 洗碗機
        /// </summary>
        public virtual string Dishwasher { get; set; }
        public virtual string DishwasherNo { get; set; }

        /// <summary>
        /// 淨水器
        /// </summary>
        public virtual string WaterPurifier { get; set; }
        public virtual string WaterPurifierNo { get; set; }

        /// <summary>
        /// 單人沙發
        /// </summary>
        public virtual string SingleSofa { get; set; }
        public virtual string SingleSofaNo { get; set; }

        /// <summary>
        /// 雙人沙發
        /// </summary>
        public virtual string DoubleSofa { get; set; }
        public virtual string DoubleSofaNo { get; set; }

        /// <summary>
        /// 三人沙發
        /// </summary>
        public virtual string Couch { get; set; }
        public virtual string CouchNo { get; set; }

        /// <summary>
        /// 茶几
        /// </summary>
        public virtual string LowStool { get; set; }
        public virtual string LowStoolNo { get; set; }

        /// <summary>
        /// 矮凳
        /// </summary>
        public virtual string CoffeeTable { get; set; }
        public virtual string CoffeeTableNo { get; set; }

        /// <summary>
        /// 鞋櫃
        /// </summary>
        public virtual string ShoeBox { get; set; }
        public virtual string ShoeBoxNo { get; set; }

        /// <summary>
        /// 電視櫃
        /// </summary>
        public virtual string TVCabinet { get; set; }
        public virtual string TVCabinetNo { get; set; }

        /// <summary>
        /// 餐桌
        /// </summary>
        public virtual string DiningTable { get; set; }
        public virtual string DiningTableNo { get; set; }

        /// <summary>
        /// 餐椅
        /// </summary>
        public virtual string DiningChair { get; set; }
        public virtual string DiningChairNo { get; set; }

        /// <summary>
        /// 室外大門
        /// </summary>
        public virtual string OutdoorGate { get; set; }
        public virtual string OutdoorGateNo { get; set; }

        /// <summary>
        /// 室內門
        /// </summary>
        public virtual string InteriorDoor { get; set; }
        public virtual string InteriorDoorNo { get; set; }

        /// <summary>
        /// 保全設施
        /// </summary>
        public virtual string SecurityFacilities { get; set; }
        public virtual string SecurityFacilitiesNo { get; set; }

        /// <summary>
        /// 室內照明(顆)
        /// </summary>
        public virtual string IndoorLighting { get; set; }
        public virtual string IndoorLightingNo { get; set; }

        /// <summary>
        /// 電風扇
        /// </summary>
        public virtual string ElectricFan { get; set; }
        public virtual string ElectricFanNo { get; set; }

        /// <summary>
        /// 窗簾
        /// </summary>
        public virtual string Curtain { get; set; }
        public virtual string CurtainNo { get; set; }

        /// <summary>
        /// 衣櫃
        /// </summary>
        public virtual string Wardrobe { get; set; }
        public virtual string WardrobeNo { get; set; }

        /// <summary>
        /// 置物櫃
        /// </summary>
        public virtual string Locker { get; set; }
        public virtual string LockerNo { get; set; }

        /// <summary>
        /// 床頭櫃
        /// </summary>
        public virtual string BedsideTable { get; set; }
        public virtual string BedsideTableNo { get; set; }

        /// <summary>
        /// 梳妝台
        /// </summary>
        public virtual string Dresser { get; set; }
        public virtual string DresserNo { get; set; }

        /// <summary>
        /// 書桌
        /// </summary>
        public virtual string Desk { get; set; }
        public virtual string DeskNo { get; set; }

        /// <summary>
        /// 椅子
        /// </summary>
        public virtual string Chair { get; set; }
        public virtual string ChairNo { get; set; }

        /// <summary>
        /// 單人床(組)
        /// </summary>
        public virtual string SingleBed { get; set; }
        public virtual string SingleBedNo { get; set; }

        /// <summary>
        /// 雙人床(組)
        /// </summary>
        public virtual string DoubleBed { get; set; }
        public virtual string DoubleBedNo { get; set; }

        /// <summary>
        /// 滅火器
        /// </summary>
        public virtual string BFireEX { get; set; }
        public virtual string BFireEXNo { get; set; }

        /// <summary>
        /// 偵煙警報器
        /// </summary>
        public virtual string BSmokeDE { get; set; }
        public virtual string BSmokeDENo { get; set; }

        /// <summary>
        /// 瓦斯警報器
        /// </summary>
        public virtual string GasAlarm { get; set; }
        public virtual string GasAlarmNo { get; set; }

        /// <summary>
        /// 緊急照明燈
        /// </summary>
        public virtual string EmergencyLights { get; set; }
        public virtual string EmergencyLightsNo { get; set; }

        /// <summary>
        /// 大樓鑰匙
        /// </summary>
        public virtual string BuildingKey { get; set; }
        public virtual string BuildingKeyNo { get; set; }

        /// <summary>
        /// 是外大門鑰匙
        /// </summary>
        public virtual string OutdoorGateKey { get; set; }
        public virtual string OutdoorGateKeyNo { get; set; }

        /// <summary>
        /// 室內門鑰匙
        /// </summary>
        public virtual string InteriorDoorKey { get; set; }
        public virtual string InteriorDoorKeyNo { get; set; }

        /// <summary>
        /// 信箱鑰匙
        /// </summary>
        public virtual string MailboxKey { get; set; }
        public virtual string MailboxKeyNo { get; set; }

        /// <summary>
        /// 磁扣卡
        /// </summary>
        public virtual string MagneticCard { get; set; }
        public virtual string MagneticCardNo { get; set; }

        /// <summary>
        /// 電子門鎖
        /// </summary>
        public virtual string ElectricDoorlock { get; set; }
        public virtual string ElectricDoorlockNo { get; set; }

        /// <summary>
        /// 電視遙控器
        /// </summary>
        public virtual string TVRemote { get; set; }
        public virtual string TVRemoteNo { get; set; }

        /// <summary>
        /// 冷氣遙控器
        /// </summary>
        public virtual string AirConditionerRemote { get; set; }
        public virtual string AirConditionerRemoteNo { get; set; }

        /// <summary>
        /// 網路分享器
        /// </summary>
        public virtual string IPSharingRouter { get; set; }
        public virtual string IPSharingRouterNo { get; set; }

        /// <summary>
        /// 數位盒
        /// </summary>
        public virtual string DigitalBox { get; set; }
        public virtual string DigitalBoxNo { get; set; }

        /// <summary>
        /// 其餘未記載項目
        /// </summary>
        public virtual string OtherDevices { get; set; }

        #endregion 建物設備相關

        #region 租屋公司基本資料

        /// <summary>
        /// 租屋公司名稱
        /// </summary>
        public virtual string ROName { get; set; }

        /// <summary>
        /// 租屋公司負責人
        /// </summary>
        public virtual string RORep { get; set; }

        /// <summary>
        /// 租屋公司地址
        /// </summary>
        public virtual string ROAdd { get; set; }

        /// <summary>
        /// 租屋公司傳真
        /// </summary>
        public virtual string ROFax { get; set; }

        /// <summary>
        /// 租屋公司電話  
        /// </summary>
        public virtual string ROTel { get; set; }

        /// <summary>
        /// 租屋公司營業登記證
        /// </summary>
        public virtual string ROLRNo { get; set; }

        /// <summary>
        /// 事業服務人員(當前USER)
        /// </summary>
        public virtual string ROUserName { get; set; }

        /// <summary>
        /// 住宅管理人員
        /// </summary>
        public virtual string RHMName { get; set; }

        /// <summary>
        /// 住宅管理員證書字號
        /// </summary>
        public virtual string RHMRNo { get; set; }

        /// <summary>
        /// 統一編號
        /// </summary>
        public virtual string ROID { get; set; }

        #endregion 租屋公司基本資料

        #region B030201 屋況及租屋安全檢核表

        /// <summary>
        /// 公寓大廈規約或其他住戶應遵循事項(無)
        /// </summary>
        public virtual string BConStature_N { get; set; }

        /// <summary>
        /// 公寓大廈規約或其他住戶應遵循事項(有)
        /// </summary>
        public virtual string BConStature_Y { get; set; }

        /// <summary>
        /// 火警警報器
        /// </summary>
        public virtual string BFireAL_Y { get; set; }

        /// <summary>
        /// 滅火器
        /// </summary>
        public virtual string BFireEX_Y { get; set; }

        /// <summary>
        /// 定期辦理消防安全檢查(無)
        /// </summary>
        public virtual string BFireSaIn_N { get; set; }

        /// <summary>
        /// 定期辦理消防安全檢查(有)
        /// </summary>
        public virtual string BFireSaIn_Y { get; set; }

        /// <summary>
        /// 機械平面
        /// </summary>
        public virtual string BMechFlatParking { get; set; }

        /// <summary>
        /// 機械機械
        /// </summary>
        public virtual string BMechMechParking { get; set; }

        /// <summary>
        /// 管理費(無)
        /// </summary>
        public virtual string BMFee_N { get; set; }

        /// <summary>
        /// 管理費(有)
        /// </summary>
        public virtual string BMFee_Y { get; set; }

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

        /// <summary>
        /// 曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事(無)
        /// </summary>
        public virtual string BMurder_N { get; set; }

        /// <summary>
        /// 曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事(有)
        /// </summary>
        public virtual string BMurder_Y { get; set; }

        /// <summary>
        /// 坡道平面
        /// </summary>
        public virtual string BRampFlatParking { get; set; }

        /// <summary>
        /// 坡道機械
        /// </summary>
        public virtual string BRampMechParking { get; set; }

        /// <summary>
        /// 獨立型偵煙器
        /// </summary>
        public virtual string BSmokeDE_Y { get; set; }

        /// <summary>
        /// 電梯大樓
        /// </summary>
        public virtual string BTElevator { get; set; }

        /// <summary>
        /// 農舍
        /// </summary>
        public virtual string BTFarmHouse { get; set; }

        /// <summary>
        /// 華廈
        /// </summary>
        public virtual string BTMansion { get; set; }

        /// <summary>
        /// 別墅
        /// </summary>
        public virtual string BTVilla { get; set; }

        #endregion B030201 屋況及租屋安全檢核表

        #region B030401 民眾(房客)承租住宅申請書

        /// <summary>
        /// 設籍於該市居民，有居住事實，或有就學就業需求者
        /// </summary>
        public virtual string RNQualify1C_1 { get; set; }

        /// <summary>
        /// 現任職務之最高職務列等在警正四階以下或相當職務列等之基層警察及消防人員
        /// </summary>
        public virtual string RNQualify1C_2 { get; set; }

        /// <summary>
        /// 申請人或家庭成員為身心障礙者或 65 歲以上老人，申請換居並將自
        /// 有住宅出租。(需同時檢附該申請人之表單 1 出租人出租住宅申請書影本)
        /// </summary>
        public virtual string RNQualify1C_3 { get; set; }

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

        /// <summary>
        /// 身分證號/居留證號(拆開)_1
        /// </summary>
        public virtual string RNID_1_1 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_2
        /// </summary>
        public virtual string RNID_1_2 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_3
        /// </summary>
        public virtual string RNID_1_3 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_4
        /// </summary>
        public virtual string RNID_1_4 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_5
        /// </summary>
        public virtual string RNID_1_5 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_6
        /// </summary>
        public virtual string RNID_1_6 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_7
        /// </summary>
        public virtual string RNID_1_7 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_8
        /// </summary>
        public virtual string RNID_1_8 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_9
        /// </summary>
        public virtual string RNID_1_9 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_10
        /// </summary>
        public virtual string RNID_1_10 { get; set; }

        #endregion B030401 民眾(房客)承租住宅申請書

        #region B030601 民眾(房客)承租住宅申請書(8格)

        /// <summary>
        /// 家庭成員4_姓名
        /// </summary>
        public virtual string RNFName_D { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_D_1 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_D_2 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_D_3 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_D_4 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_D_5 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_D_6 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_D_7 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_D_8 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_D_9 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_D_10 { get; set; }

        /// <summary>
        /// 家庭成員4_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_D { get; set; }

        /// <summary>
        /// 家庭成員4_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_D { get; set; }

        /// <summary>
        /// 家庭成員4_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_D { get; set; }


        /// <summary>
        /// 家庭成員5_姓名
        /// </summary>
        public virtual string RNFName_E { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_E_1 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_E_2 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_E_3 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_E_4 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_E_5 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_E_6 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_E_7 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_E_8 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_E_9 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_E_10 { get; set; }

        /// <summary>
        /// 家庭成員5_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_E { get; set; }

        /// <summary>
        /// 家庭成員5_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_E { get; set; }

        /// <summary>
        /// 家庭成員5_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_E { get; set; }


        /// <summary>
        /// 家庭成員6_姓名
        /// </summary>
        public virtual string RNFName_F { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_F_1 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_F_2 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_F_3 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_F_4 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_F_5 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_F_6 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_F_7 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_F_8 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_F_9 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_F_10 { get; set; }

        /// <summary>
        /// 家庭成員6_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_F { get; set; }

        /// <summary>
        /// 家庭成員6_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_F { get; set; }

        /// <summary>
        /// 家庭成員6_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_F { get; set; }

        /// <summary>
        /// 家庭成員7_姓名
        /// </summary>
        public virtual string RNFName_G { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_G_1 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_G_2 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_G_3 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_G_4 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_G_5 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_G_6 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_G_7 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_G_8 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_G_9 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_G_10 { get; set; }

        /// <summary>
        /// 家庭成員7_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_G { get; set; }

        /// <summary>
        /// 家庭成員7_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_G { get; set; }

        /// <summary>
        /// 家庭成員7_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_G { get; set; }

        /// <summary>
        /// 家庭成員8_姓名
        /// </summary>
        public virtual string RNFName_H { get; set; }

        /// <summary>
        /// 家庭成員8_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_H_1 { get; set; }

        /// <summary>
        /// 家庭成員8_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_H_2 { get; set; }

        /// <summary>
        /// 家庭成員8_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_H_3 { get; set; }

        /// <summary>
        /// 家庭成員8_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_H_4 { get; set; }

        /// <summary>
        /// 家庭成員8_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_H_5 { get; set; }

        /// <summary>
        /// 家庭成員8_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_H_6 { get; set; }

        /// <summary>
        /// 家庭成員8_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_H_7 { get; set; }

        /// <summary>
        /// 家庭成員8_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_H_8 { get; set; }

        /// <summary>
        /// 家庭成員8_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_H_9 { get; set; }

        /// <summary>
        /// 家庭成員8_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_H_10 { get; set; }

        /// <summary>
        /// 家庭成員8_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_H { get; set; }

        /// <summary>
        /// 家庭成員8_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_H { get; set; }

        /// <summary>
        /// 家庭成員8_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_H { get; set; }

        #endregion B030601 民眾(房客)承租住宅申請書(8格)

        #region B030701 承租人補助費用申請書

        /// <summary>
        /// 租金補助
        /// </summary>
        public virtual string RNRentSUB { get; set; }

        /// <summary>
        /// 租金補助 申請金額 新台幣
        /// </summary>
        public virtual string RNRentSUBAFee { get; set; }

        /// <summary>
        /// 簽 約 租 金  □無共住戶，簽約租金為新臺幣
        /// </summary>
        public virtual string RNRentSUBNoFee { get; set; }

        /// <summary>
        /// 簽 約 租 金  有共住戶，申請人負擔之租金為新臺幣
        /// </summary>
        public virtual string RNRentSUBYesFee { get; set; }

        /// <summary>
        /// 公證費 申請金額 新台幣
        /// </summary>
        public virtual string RNSFAFee { get; set; }

        /// <summary>
        /// 公證費
        /// </summary>
        public virtual string RNSFee { get; set; }

        /// <summary>
        /// 公證費 實際支付金額 新臺幣
        /// </summary>
        public virtual string RNSFRFee { get; set; }

        #endregion B030701 承租人補助費用申請書

        #region B031001 共有住宅授權人代表書

        /// <summary>
        /// 所有權人1姓名
        /// </summary>
        public virtual string B1OwnerName_A { get; set; }

        /// <summary>
        /// 所有權人1身分證字號
        /// </summary>
        public virtual string B1OwnerID_A { get; set; }

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

        #endregion B031001 共有住宅授權人代表書

        #region B031101 代墊租金申請書及還款計畫書

        /// <summary>
        /// 租金補貼新台幣
        /// </summary>
        public virtual string BTRentGrant { get; set; }

        #endregion B031101 代墊租金申請書及還款計畫書

        #region 6/17 MM 新增必要欄位


        /// <summary>
        /// 登記地址同營業地址
        /// </summary>
        public virtual string LCAddSame { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC { get; set; }

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

        /// <summary>
        /// 代理人手機
        /// </summary>
        public virtual string RNAGCell_A { get; set; }

        /// <summary>
        /// 代理人電話
        /// </summary>
        public virtual string RNAGTel_1_A { get; set; }
        public virtual string RNAGTel_2_A { get; set; }

        #endregion 6/17 MM 新增必要欄位

        #region C01030101


        /// <summary>
        /// 代管費
        /// </summary>
        public virtual string B1EscrowFee { get; set; }

        /// <summary>
        /// 權力範圍
        /// </summary>
        public string B1ExtentOfOwner { get; set; }

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

        /// <summary>
        /// 出租自然人身分證字號(分開)_1
        /// </summary>
        public virtual string LNID_1_1 { get; set; }

        /// <summary>
        /// 出租自然人身分證字號(分開)_2
        /// </summary>
        public virtual string LNID_1_2 { get; set; }

        /// <summary>
        /// 出租自然人身分證字號(分開)_3
        /// </summary>
        public virtual string LNID_1_3 { get; set; }

        /// <summary>
        /// 出租自然人身分證字號(分開)_4
        /// </summary>
        public virtual string LNID_1_4 { get; set; }

        /// <summary>
        /// 出租自然人身分證字號(分開)_5
        /// </summary>
        public virtual string LNID_1_5 { get; set; }

        /// <summary>
        /// 出租自然人身分證字號(分開)_6
        /// </summary>
        public virtual string LNID_1_6 { get; set; }

        /// <summary>
        /// 出租自然人身分證字號(分開)_7
        /// </summary>
        public virtual string LNID_1_7 { get; set; }

        /// <summary>
        /// 出租自然人身分證字號(分開)_8
        /// </summary>
        public virtual string LNID_1_8 { get; set; }

        /// <summary>
        /// 出租自然人身分證字號(分開)_9
        /// </summary>
        public virtual string LNID_1_9 { get; set; }

        /// <summary>
        /// 出租自然人身分證字號(分開)_10
        /// </summary>
        public virtual string LNID_1_10 { get; set; }

        /// <summary>
        /// 權利範圍
        /// </summary>
        public virtual string LNExtentOfOwner { get; set; }

        /// <summary>
        /// 屋齡
        /// </summary>
        public virtual string BAge { get; set; }

        /// <summary>
        /// 身障及長者換居專案
        /// </summary>
        public virtual string BChangeResidence { get; set; }

        /// <summary>
        /// 包租代管及租補整合專案
        /// </summary>
        public virtual string BChangeTrack { get; set; }

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
        /// 實際使用坪數
        /// </summary>
        public virtual string BRealPING { get; set; }

        /// <summary>
        /// 房屋類型_平房
        /// </summary>
        public virtual string BTBungalow { get; set; }

        /// <summary>
        /// (法人)統一編號(分開)_1
        /// </summary>
        public virtual string LCID_1_1 { get; set; }

        /// <summary>
        /// (法人)統一編號(分開)_2
        /// </summary>
        public virtual string LCID_1_2 { get; set; }

        /// <summary>
        /// (法人)統一編號(分開)_3
        /// </summary>
        public virtual string LCID_1_3 { get; set; }

        /// <summary>
        /// (法人)統一編號(分開)_4
        /// </summary>
        public virtual string LCID_1_4 { get; set; }

        /// <summary>
        /// (法人)統一編號(分開)_5
        /// </summary>
        public virtual string LCID_1_5 { get; set; }

        /// <summary>
        /// (法人)統一編號(分開)_6
        /// </summary>
        public virtual string LCID_1_6 { get; set; }

        /// <summary>
        /// (法人)統一編號(分開)_7
        /// </summary>
        public virtual string LCID_1_7 { get; set; }

        /// <summary>
        /// (法人)統一編號(分開)_8
        /// </summary>
        public virtual string LCID_1_8 { get; set; }
        #endregion C01030101

        #region C01030201 社會住宅包租代管第三期計畫_ 民眾承租住宅申請書

        /// <summary>
        /// □因懷孕或生育而遭遇困境之未成年
        /// </summary>
        public virtual string RNQualify2C_11 { get; set; }

        #endregion

        #region C01030701 社會住宅包出代管第三期計畫住宅出租修繕費申請書


        #endregion C01030701 社會住宅包出代管第三期計畫住宅出租修繕費申請書

        #region C01031001 共有住宅代表人授權書


        /// <summary>
        /// 所有權人1地址
        /// </summary>
        public virtual string B1OwnerAdd_A { get; set; }

        /// <summary>
        /// 所有權人2地址
        /// </summary>
        public virtual string B1OwnerAdd_B { get; set; }

        /// <summary>
        /// 所有權人3地址
        /// </summary>
        public virtual string B1OwnerAdd_C { get; set; }

        /// <summary>
        /// 所有權人4地址
        /// </summary>
        public virtual string B1OwnerAdd_D { get; set; }

        /// <summary>
        /// 所有權人5地址
        /// </summary>
        public virtual string B1OwnerAdd_E { get; set; }

        #endregion C01031001 共有住宅代表人授權書

        #region C02030201

        public virtual string RNFID_1_D { get; set; }
        public virtual string RNFBirthday_D { get; set; }
        public virtual string RNFID_1_E { get; set; }
        public virtual string RNFBirthday_E { get; set; }
        public virtual string RNFID_1_F { get; set; }
        public virtual string RNFBirthday_F { get; set; }
        public virtual string RNFID_1_G { get; set; }
        public virtual string RNFBirthday_G { get; set; }

        #endregion C02030201

        #region C06030101

        /// <summary>
        /// 其他所有權人權力範圍
        /// </summary>
        public string B1ExtentOfOwner_A { get; set; }

        #endregion

        #region 建物現況

        /// <summary>
        /// 租金支付_轉帳
        /// </summary>
        public virtual string BTransfer { get; set; }

        /// <summary>
        /// 租金支付_票據
        /// </summary>
        public virtual string BBill { get; set; }

        /// <summary>
        /// 租金支付_現金
        /// </summary>
        public virtual string BCash { get; set; }

        #endregion 建物現況

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

        #region LS

        /// <summary>
        /// 房東電話(夜)(不知法人還是自然人)
        /// </summary>
        public virtual string LSTelN { get; set; }

        /// <summary>
        /// 房東電話(夜)(不知法人還是自然人)_1
        /// </summary>
        public virtual string LSTelN_1 { get; set; }

        /// <summary>
        /// 房東電話(夜)(不知法人還是自然人)_2
        /// </summary>
        public virtual string LSTelN_2 { get; set; }

        /// <summary>
        /// 房東電子信箱(不知法人還是自然人)
        /// </summary>
        public virtual string LSMail { get; set; }

        /// <summary>
        /// 房東生日_D(不知法人還是自然人)
        /// </summary>
        public virtual string LSBirthday_D { get; set; }

        /// <summary>
        /// 房東生日_D(不知法人還是自然人)
        /// </summary>
        public virtual string LSBirthday_M { get; set; }

        /// <summary>
        /// 房東生日_D(不知法人還是自然人)
        /// </summary>
        public virtual string LSBirthday_Y { get; set; }

        /// <summary>
        /// 房東身份證字號或統一編號_分開_1
        /// </summary>
        public virtual string LSID_1_1 { get; set; }

        /// <summary>
        /// 房東身份證字號或統一編號_分開_2
        /// </summary>
        public virtual string LSID_1_2 { get; set; }

        /// <summary>
        /// 房東身份證字號或統一編號_分開_3
        /// </summary>
        public virtual string LSID_1_3 { get; set; }

        /// <summary>
        /// 房東身份證字號或統一編號_分開_4
        /// </summary>
        public virtual string LSID_1_4 { get; set; }

        /// <summary>
        /// 房東身份證字號或統一編號_分開_5
        /// </summary>
        public virtual string LSID_1_5 { get; set; }

        /// <summary>
        /// 房東身份證字號或統一編號_分開_6
        /// </summary>
        public virtual string LSID_1_6 { get; set; }

        /// <summary>
        /// 房東身份證字號或統一編號_分開_7
        /// </summary>
        public virtual string LSID_1_7 { get; set; }

        /// <summary>
        /// 房東身份證字號或統一編號_分開_8
        /// </summary>
        public virtual string LSID_1_8 { get; set; }

        /// <summary>
        /// 房東身份證字號或統一編號_分開_9
        /// </summary>
        public virtual string LSID_1_9 { get; set; }

        /// <summary>
        /// 房東身份證字號或統一編號_分開_10
        /// </summary>
        public virtual string LSID_1_10 { get; set; }

        /// <summary>
        /// 房東性別_男
        /// </summary>
        public virtual string LSGender1 { get; set; }

        /// <summary>
        /// 房東性別_女
        /// </summary>
        public virtual string LSGender2 { get; set; }

        /// <summary>
        /// 房東手機(不知法人還自然人)
        /// </summary>
        public virtual string LSCell { get; set; }

        /// <summary>
        /// 房東完整住址(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd { get; set; }

        /// <summary>
        /// 房東住址(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_1 { get; set; }

        /// <summary>
        /// 房東住址2(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_1_1 { get; set; }

        /// <summary>
        /// 房東住址2(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_1_2 { get; set; }

        /// <summary>
        /// 房東住址3(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_2 { get; set; }

        /// <summary>
        /// 房東住址4(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_2_1 { get; set; }

        /// <summary>
        /// 房東住址5(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_2_2 { get; set; }

        /// <summary>
        /// 房東住址6(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_2_3 { get; set; }

        /// <summary>
        /// 房東住址7(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_2_4 { get; set; }

        /// <summary>
        /// 房東住址8(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_3 { get; set; }

        /// <summary>
        /// 房東住址9(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_3_1 { get; set; }

        /// <summary>
        /// 房東住址10(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_3_2 { get; set; }

        /// <summary>
        /// 房東住址11(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_4 { get; set; }

        /// <summary>
        /// 房東住址12(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_5 { get; set; }

        /// <summary>
        /// 房東住址13(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_6 { get; set; }

        /// <summary>
        /// 房東住址14(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_7 { get; set; }

        /// <summary>
        /// 房東住址15(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_8 { get; set; }

        /// <summary>
        /// 房東住址16(不知法人還是自然人)
        /// </summary>
        public virtual string LSAdd_9 { get; set; }

        /// <summary>
        /// 房東通訊地址同戶籍地址(不知法人還是自然人)
        /// </summary>
        public virtual string LSAddSame { get; set; }

        /// <summary>
        /// 房東代理人1姓名(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGName_A { get; set; }

        /// <summary>
        /// 房東代理人1地址1(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_1_A { get; set; }

        /// <summary>
        /// 房東代理人1地址2(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_1_1_A { get; set; }

        /// <summary>
        /// 房東代理人1地址3(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_1_2_A { get; set; }

        /// <summary>
        /// 房東代理人1地址4(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_2_A { get; set; }

        /// <summary>
        /// 房東代理人1地址5(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_2_1_A { get; set; }

        /// <summary>
        /// 房東代理人1地址6(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_2_2_A { get; set; }

        /// <summary>
        /// 房東代理人1地址7(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_2_3_A { get; set; }

        /// <summary>
        /// 房東代理人1地址8(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_2_4_A { get; set; }

        /// <summary>
        /// 房東代理人1地址9(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_3_A { get; set; }

        /// <summary>
        /// 房東代理人1地址10(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_3_1_A { get; set; }

        /// <summary>
        /// 房東代理人1地址11(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_3_2_A { get; set; }

        /// <summary>
        /// 房東代理人1地址12(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_4_A { get; set; }

        /// <summary>
        /// 房東代理人1地址13(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_5_A { get; set; }

        /// <summary>
        /// 房東代理人1地址14(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_6_A { get; set; }

        /// <summary>
        /// 房東代理人1地址15(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_7_A { get; set; }

        /// <summary>
        /// 房東代理人1地址16(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_8_A { get; set; }

        /// <summary>
        /// 房東代理人1地址17(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_9_A { get; set; }

        /// <summary>
        /// 房東代理人1手機(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGCell_A { get; set; }

        /// <summary>
        /// 房東代理人1電話1(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGTel_1_A { get; set; }

        /// <summary>
        /// 房東代理人1電話2(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGTel_2_A { get; set; }

        public virtual string LSAGID_A { get; set; }

        /// <summary>
        /// 房東代理人2姓名(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGName_B { get; set; }

        /// <summary>
        /// 房東代理人2地址1(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_1_B { get; set; }

        /// <summary>
        /// 房東代理人2地址2(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_1_1_B { get; set; }

        /// <summary>
        /// 房東代理人2地址3(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_1_2_B { get; set; }

        /// <summary>
        /// 房東代理人2地址4(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_2_B { get; set; }

        /// <summary>
        /// 房東代理人2地址5(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_2_1_B { get; set; }

        /// <summary>
        /// 房東代理人2地址6(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_2_2_B { get; set; }

        /// <summary>
        /// 房東代理人2地址7(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_2_3_B { get; set; }

        /// <summary>
        /// 房東代理人2地址8(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_2_4_B { get; set; }

        /// <summary>
        /// 房東代理人2地址9(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_3_B { get; set; }

        /// <summary>
        /// 房東代理人2地址10(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_3_1_B { get; set; }

        /// <summary>
        /// 房東代理人2地址11(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_3_2_B { get; set; }

        /// <summary>
        /// 房東代理人2地址12(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_4_B { get; set; }

        /// <summary>
        /// 房東代理人2地址13(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_5_B { get; set; }

        /// <summary>
        /// 房東代理人2地址14(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_6_B { get; set; }

        /// <summary>
        /// 房東代理人2地址15(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_7_B { get; set; }

        /// <summary>
        /// 房東代理人2地址16(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_8_B { get; set; }

        /// <summary>
        /// 房東代理人2地址17(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGAdd_9_B { get; set; }

        /// <summary>
        /// 房東代理人2手機(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGCell_B { get; set; }

        /// <summary>
        /// 房東代理人2電話(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGTel_1_B { get; set; }

        /// <summary>
        /// 房東代理人2電話2(不知法人還是自然人)
        /// </summary>
        public virtual string LSAGTel_2_B { get; set; }

        public virtual string LSAGID_B { get; set; }


        #endregion LS

        #region RS

        #region D000601 印鑑使用授權書
        public string RSCell { get; set; }
        public string RSID { get; set; }
        public string RSName { get; set; }
        public string RSAdd { get; set; }
        public string RSAdd_1 { get; set; }
        public string RSAdd_1_1 { get; set; }
        public string RSAdd_1_2 { get; set; }
        public string RSAdd_2 { get; set; }
        public string RSAdd_2_1 { get; set; }
        public string RSAdd_2_2 { get; set; }
        public string RSAdd_2_3 { get; set; }
        public string RSAdd_2_4 { get; set; }
        public string RSAdd_3 { get; set; }
        public string RSAdd_3_1 { get; set; }
        public string RSAdd_3_2 { get; set; }
        public string RSAdd_4 { get; set; }
        public string RSAdd_5 { get; set; }
        public string RSAdd_6 { get; set; }
        public string RSAdd_7 { get; set; }
        public string RSAdd_8 { get; set; }
        public string RSAdd_9 { get; set; }
        public string RSAddC { get; set; }
        public string RSAddC_1 { get; set; }
        public string RSAddC_1_1 { get; set; }
        public string RSAddC_1_2 { get; set; }
        public string RSAddC_2 { get; set; }
        public string RSAddC_2_1 { get; set; }
        public string RSAddC_2_2 { get; set; }
        public string RSAddC_2_3 { get; set; }
        public string RSAddC_2_4 { get; set; }
        public string RSAddC_3 { get; set; }
        public string RSAddC_3_1 { get; set; }
        public string RSAddC_3_2 { get; set; }
        public string RSAddC_4 { get; set; }
        public string RSAddC_5 { get; set; }
        public string RSAddC_6 { get; set; }
        public string RSAddC_7 { get; set; }
        public string RSAddC_8 { get; set; }
        public string RSAddC_9 { get; set; }

        #endregion D000601 印鑑使用授權書


        #endregion

        #region 8/17 MM 欄位修改0817.docx

        /// <summary>
        /// 廠房
        /// </summary>
        public virtual string BTWorkshop { get; set; }

        /// <summary>
        /// 格局_4房以上
        /// </summary>
        public virtual string BPatten4Rup { get; set; }

        /// <summary>
        /// 第1層
        /// </summary>
        public virtual string BFloor_1 { get; set; }

        /// <summary>
        /// 第1層面積/平方公尺
        /// </summary>
        public virtual string BArea_1 { get; set; }

        /// <summary>
        /// 第1層坪數
        /// </summary>
        public virtual string BPing_1 { get; set; }

        /// <summary>
        /// 第二層
        /// </summary>
        public virtual string BFloor_2 { get; set; }

        /// <summary>
        /// 第2層面積/平方公尺
        /// </summary>
        public virtual string BArea_2 { get; set; }

        /// <summary>
        /// 第2層坪數
        /// </summary>
        public virtual string BPing_2 { get; set; }

        /// <summary>
        /// 第3層
        /// </summary>
        public virtual string BFloor_3 { get; set; }

        /// <summary>
        /// 第3層面積/平方公尺
        /// </summary>
        public virtual string BArea_3 { get; set; }

        /// <summary>
        /// 第3層坪數
        /// </summary>
        public virtual string BPing_3 { get; set; }

        /// <summary>
        /// 實際面積/平方公尺(換算成坪數對應到BRealPING)
        /// </summary>
        public virtual string BRealArea { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        public virtual string BUse { get; set; }

        /// <summary>
        /// 合約租金
        /// </summary>
        public virtual string BTCRent { get; set; }

        /// <summary>
        /// 每坪金額
        /// </summary>
        public virtual string BMFeePing { get; set; }
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
        #endregion 8/17 MM 欄位修改0817.docx

        #region 2022/12/05

        /// <summary>
        /// 公司
        /// </summary>
        public string LNCompany { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        public string LNJobtitle { get; set; }

        #endregion 2022/12/05

        #region 由WEB表單新增的欄位，但是後續出現在PDF表單

        #region WEB表單簽名檔64位元流
        public virtual string SignatureBase64_1 { get; set; }
        public virtual string SignatureBase64_2 { get; set; }
        public virtual string SignatureBase64_3 { get; set; }
        public virtual string SignatureBase64_4 { get; set; }
        public virtual string SignatureBase64_5 { get; set; }

        #endregion WEB表單簽名檔64位元流

        #region A000002 自然人房客資料卡

        /// <summary>
        /// 承租人LineID
        /// </summary>
        public virtual string RNLine { get; set; }

        /// <summary>
        /// 緊急聯絡人通訊地址
        /// </summary>
        public virtual string RNECAddC { get; set; }

        /// <summary>
        /// 緊急聯絡人通訊地址同戶籍地址
        /// </summary>
        public virtual string RNECAddSame { get; set; }

        /// <summary>
        /// 代理人生日
        /// </summary>
        public virtual string RNAGBirthday { get; set; }

        #endregion A000002 自然人房客資料卡

        #region  C03030101出租人住宅申請書-桃園市三期

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

        #endregion C03030101出租人住宅申請書-桃園市三期

        #region  C03030701 修繕費申請書-桃園市三期

        public virtual string LNAGAddC_A { get; set; }

        #endregion C03030701 修繕費申請書-桃園市三期

        #endregion 由WEB表單新增的欄位，但是後續出現在PDF表單
    }
}
