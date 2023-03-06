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
    public class CustomerLCOutputDto
    {

        #region Property Members
        public virtual string Id { get; set; }

        /// <summary>
        /// (法人)統一編號
        /// </summary>
        public virtual string LCID { get; set; }

        /// <summary>
        /// (法人)法人名稱
        /// </summary>
        public virtual string LCName { get; set; }

        /// <summary>
        /// (法人)代表人
        /// </summary>
        public virtual string LCRep { get; set; }

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
        /// 營業地址
        /// </summary>
        public virtual string LCAddC { get; set; }

        /// <summary>
        /// 登記地址同營業地址
        /// </summary>
        public virtual string LCAddSame { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_1 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_1_1 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_1_2 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_2 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_2_1 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_2_2 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_2_3 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_2_4 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_3 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_3_1 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_3_2 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_4 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_5 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_6 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_7 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_8 { get; set; }

        /// <summary>
        /// 營業地址
        /// </summary>
        public virtual string LCAddC_9 { get; set; }

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
        public DateTime? CreatorTime { get; set; }
        public string CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
        public string LastModifyUserId { get; set; }
        public DateTime? LastModifyTime { get; set; }
        #endregion

    }
}
